using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using TestTools.Helpers;

namespace TestTools.Structure
{
    public class StructureService : IStructureService
    {
        string FromNamespace { get; set; }

        string ToNamespace { get; set; }

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

        public StructureService(string fromNamespace, string toNamespace)
        {
            FromNamespace = fromNamespace;
            ToNamespace = toNamespace;
        }

        public Type TranslateType(Type type)
        {
            Type translatedType;

            if (type.IsArray)
            {
                Type elementType = TranslateType(type.GetElementType());
                return elementType.MakeArrayType();
            }

            if (type.Namespace == FromNamespace)
            {
                ITypeTranslator translator = type.GetCustomTranslator() ?? TypeTranslator;
                translator.TargetNamespace = ToNamespace;
                translator.Verifier = StructureVerifier;

                translatedType = translator.Translate(type);
            }
            else translatedType = type;

            if (type.IsGenericType)
            {
                // TODO add validation so that TypeArguments must match
                Type[] typeArguments = type.GetGenericArguments().Select(TranslateType).ToArray();
                return translatedType.GetGenericTypeDefinition().MakeGenericType(typeArguments);
            }
            return translatedType;
        }

        public MemberInfo TranslateMember(MemberInfo memberInfo)
        {
            if (memberInfo.DeclaringType.Namespace != FromNamespace)
                return memberInfo;
            
            IMemberTranslator translator = memberInfo.GetCustomTranslator() ?? MemberTranslator;

            translator.Verifier = StructureVerifier;
            translator.TargetType = TranslateType(memberInfo.DeclaringType);

            return translator.Translate(memberInfo);
        }

        public MemberInfo TranslateMember(Type targetType, MemberInfo memberInfo)
        {
            if (memberInfo.DeclaringType.Namespace != FromNamespace)
                return memberInfo;

            IMemberTranslator translator = memberInfo.GetCustomTranslator() ?? MemberTranslator;

            translator.Verifier = StructureVerifier;
            translator.TargetType = targetType;

            return translator.Translate(memberInfo);
        }

        public void VerifyType(Type original, ITypeVerifier[] verifiers)
        {
            Type translated = TranslateType(original);

            foreach (TypeVerificationAspect aspect in TypeVerificationOrder)
            {
                ITypeVerifier defaultVerifier = verifiers.FirstOrDefault(ver => ver.Aspects.Contains(aspect));
                ITypeVerifier verifier = original.GetCustomVerifier(aspect) ?? defaultVerifier;

                if (verifier != null)
                {
                    verifier.Verifier = StructureVerifier;
                    verifier.Service = this;
                    verifier.Verify(original, translated);
                }
            }
        }

        public void VerifyMember(MemberInfo original, IMemberVerifier[] verifiers)
        {
            Type translatedType = TranslateType(original.DeclaringType);
            MemberInfo translatedMember = TranslateMember(translatedType, original);

            foreach (MemberVerificationAspect aspect in MemberVerificationOrder)
            {
                IMemberVerifier defaultVerifier = verifiers.FirstOrDefault(ver => ver.Aspects.Contains(aspect));
                IMemberVerifier verifier = original.GetCustomVerifier(aspect) ?? defaultVerifier;

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
