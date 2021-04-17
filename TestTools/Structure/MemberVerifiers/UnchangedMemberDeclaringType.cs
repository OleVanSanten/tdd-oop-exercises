using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace TestTools.Structure
{
    public class UnchangedMemberDeclaringType : MemberVerifier
    {
        public override MemberVerificationAspect[] Aspects => new[] {
            MemberVerificationAspect.MethodDeclaringType,
            MemberVerificationAspect.PropertyGetDeclaringType,
            MemberVerificationAspect.PropertySetDeclaringType
        };

        public override void Verify(MemberInfo originalMember, MemberInfo translatedMember)
        {
            if (originalMember is MethodInfo originalMethod)
            {
                Type type = Service.TranslateType(originalMethod.DeclaringType);
                Verifier.VerifyMemberType(translatedMember, new[] { MemberTypes.Method });
                Verifier.VerifyDeclaringType((MethodInfo)translatedMember, type);
            }
            else if (originalMember is PropertyInfo originalProperty)
            {
                Type type1 = originalProperty.CanRead ? Service.TranslateType(originalProperty.GetMethod.DeclaringType) : null;
                Type type2 = originalProperty.CanWrite ? Service.TranslateType(originalProperty.SetMethod.DeclaringType) : null;

                Verifier.VerifyMemberType(translatedMember, new[] { MemberTypes.Field, MemberTypes.Property });

                if (translatedMember is FieldInfo translatedField)
                {
                    Verifier.VerifyDeclaringType(translatedField, type1 ?? type2);
                }
                else if (translatedMember is PropertyInfo translatedProperty)
                {
                    if (type1 != null)
                        Verifier.VerifyDeclaringType(translatedProperty, type1, GetMethod: true);
                    
                    if (type2 != null)
                        Verifier.VerifyDeclaringType(translatedProperty, type2, SetMethod: true);
                }
            }
            else throw new NotImplementedException();
        }
    }
}
