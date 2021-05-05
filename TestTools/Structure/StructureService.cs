﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using TestTools.Helpers;
using TestTools.TypeSystem;

namespace TestTools.Structure
{
    public class StructureService : IStructureService
    {
        NamespaceDescription FromNamespace { get; set; }

        NamespaceDescription ToNamespace { get; set; }

        public VerifierServiceBase StructureVerifier { get; set; }

        public ITypeTranslator TypeTranslator { get; set; } = new SameNameTypeTranslator();

        public IMemberTranslator MemberTranslator { get; set; } = new SameNameMemberTranslator();

        public ICollection<TypeVerificationAspect> TypeVerificationOrder { get; set; } = new[]
        {
            TypeVerificationAspect.IsInterface,
            TypeVerificationAspect.IsDelegate,
            TypeVerificationAspect.IsSubclassOf,
            TypeVerificationAspect.DelegateSignature,
            TypeVerificationAspect.IsStatic,
            TypeVerificationAspect.IsAbstract,
            TypeVerificationAspect.AccessLevel
        };

        public ICollection<MemberVerificationAspect> MemberVerificationOrder { get; set; } = new[]
        {
            MemberVerificationAspect.MemberType,
            MemberVerificationAspect.FieldType,
            MemberVerificationAspect.FieldAccessLevel,
            MemberVerificationAspect.FieldWriteability,
            MemberVerificationAspect.PropertyType,
            MemberVerificationAspect.PropertyIsStatic,
            MemberVerificationAspect.PropertyGetIsAbstract,
            MemberVerificationAspect.PropertyGetIsVirtual,
            MemberVerificationAspect.PropertyGetDeclaringType,
            MemberVerificationAspect.PropertyGetAccessLevel,
            MemberVerificationAspect.PropertySetIsAbstract,
            MemberVerificationAspect.PropertySetIsVirtual,
            MemberVerificationAspect.PropertySetDeclaringType,
            MemberVerificationAspect.PropertySetAccessLevel,
            MemberVerificationAspect.MethodReturnType,
            MemberVerificationAspect.MethodIsAbstract,
            MemberVerificationAspect.MethodIsVirtual,
            MemberVerificationAspect.MethodDeclaringType,
            MemberVerificationAspect.MethodAccessLevel
        };

        public StructureService(NamespaceDescription fromNamespace, NamespaceDescription toNamespace)
        {
            if (fromNamespace == null)
                throw new ArgumentNullException("fromNamespace cannot be null");
            if (toNamespace == null)
                throw new ArgumentNullException("toNamespace cannot be null");

            FromNamespace = fromNamespace;
            ToNamespace = toNamespace;
        }

        public TypeDescription TranslateType(TypeDescription type)
        {
            TypeDescription translatedType;

            if (type.IsArray)
            {
                throw new NotImplementedException();
                //Type elementType = TranslateType(type.GetElementType());
                //return elementType.MakeArrayType();
            }

            if (type.Namespace == FromNamespace.Name)
            {
                ITypeTranslator translator = /*type.GetCustomTranslator() ??*/ TypeTranslator;
                translator.TargetNamespace = ToNamespace;
                translator.Verifier = StructureVerifier;

                translatedType = translator.Translate(type);
            }
            else translatedType = type;

            /*if (type.IsGenericType)
            {
                // TODO add validation so that TypeArguments must match
                Type[] typeArguments = type.GetGenericArguments().Select(TranslateType).ToArray();
                return translatedType.GetGenericTypeDefinition().MakeGenericType(typeArguments);
            }*/
            return translatedType;
        }

        public MemberDescription TranslateMember(MemberDescription memberInfo)
        {
            if (memberInfo.DeclaringType.Namespace != FromNamespace.Name)
                return memberInfo;
            
            IMemberTranslator translator = /*memberInfo.GetCustomTranslator() ??*/ MemberTranslator;

            translator.Verifier = StructureVerifier;
            translator.TargetType = TranslateType(memberInfo.DeclaringType);

            return translator.Translate(memberInfo);
        }

        public MemberDescription TranslateMember(TypeDescription targetType, MemberDescription memberInfo)
        {
            if (memberInfo.DeclaringType.Namespace != FromNamespace.Name)
                return memberInfo;

            IMemberTranslator translator = /*memberInfo.GetCustomTranslator() ??*/ MemberTranslator;

            translator.Verifier = StructureVerifier;
            translator.TargetType = targetType;

            return translator.Translate(memberInfo);
        }

        public void VerifyType(TypeDescription original, ITypeVerifier[] verifiers)
        {
            TypeDescription translated = TranslateType(original);

            foreach (TypeVerificationAspect aspect in TypeVerificationOrder)
            {
                ITypeVerifier defaultVerifier = verifiers.FirstOrDefault(ver => ver.Aspects.Contains(aspect));
                ITypeVerifier verifier = /*original.GetCustomVerifier(aspect) ??*/defaultVerifier;

                if (verifier != null)
                {
                    verifier.Verifier = StructureVerifier;
                    verifier.Service = this;
                    verifier.Verify(original, translated);
                }
            }
        }

        public void VerifyMember(MemberDescription original, IMemberVerifier[] verifiers)
        {
            TypeDescription translatedType = TranslateType(original.DeclaringType);
            MemberDescription translatedMember = TranslateMember(translatedType, original);

            foreach (MemberVerificationAspect aspect in MemberVerificationOrder)
            {
                IMemberVerifier defaultVerifier = verifiers.FirstOrDefault(ver => ver.Aspects.Contains(aspect));
                IMemberVerifier verifier = /*original.GetCustomVerifier(aspect) ??*/ defaultVerifier;

                if (verifier != null)
                {
                    verifier.Verifier = StructureVerifier;
                    verifier.Service = this;
                    verifier.Verify(original, translatedMember);
                }
            }
        }
    }
}
