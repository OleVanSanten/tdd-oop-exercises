using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace TestTools.Structure
{
    public class UnchangedMemberIsVirtualVerifier : MemberVerifier
    {
        public override MemberVerificationAspect[] Aspects => new[] {
            MemberVerificationAspect.PropertyGetIsVirtual,
            MemberVerificationAspect.PropertySetIsVirtual,
            MemberVerificationAspect.MethodIsVirtual
        };

        public override void Verify(MemberInfo originalMember, MemberInfo translatedMember)
        {
            if (originalMember is PropertyInfo originalProperty)
            {
                Verifier.VerifyMemberType(translatedMember, new[] { MemberTypes.Property });

                if (originalProperty.CanRead)
                    Verifier.VerifyIsVirtual((PropertyInfo)translatedMember, originalProperty.GetMethod.IsVirtual, GetMethod: true);

                if (originalProperty.CanWrite)
                    Verifier.VerifyIsVirtual((PropertyInfo)translatedMember, originalProperty.SetMethod.IsVirtual, SetMethod: true);
            }
            else if (translatedMember is MethodInfo originalMethod)
            {
                Verifier.VerifyMemberType(translatedMember, new[] { MemberTypes.Method });
                Verifier.VerifyIsStatic((MethodInfo)translatedMember, originalMethod.IsStatic);
            }
            else throw new NotImplementedException();
        }
    }
}
