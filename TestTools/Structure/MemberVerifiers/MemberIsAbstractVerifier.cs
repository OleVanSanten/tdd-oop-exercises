using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace TestTools.Structure
{
    public class MemberIsAbstractVerifier : MemberVerifier
    {
        bool _isAbstract;

        public MemberIsAbstractVerifier(bool isAbstract = true)
        {
            _isAbstract = isAbstract;
        }

        public override MemberVerificationAspect[] Aspects => new[] {
            MemberVerificationAspect.PropertyGetIsAbstract,
            MemberVerificationAspect.PropertySetIsAbstract,
            MemberVerificationAspect.MethodIsAbstract
        };

        public override void Verify(MemberInfo originalMember, MemberInfo translatedMember)
        {
            Verifier.VerifyMemberType(translatedMember, new[] { MemberTypes.Property, MemberTypes.Method });

            if (translatedMember is PropertyInfo translatedProperty)
            {
                Verifier.VerifyIsAbstract(translatedProperty, _isAbstract);
            }
            else if (translatedMember is MethodInfo translatedMethod)
            {
                Verifier.VerifyIsAbstract(translatedMethod, _isAbstract);
            }
            else throw new NotImplementedException();
        }
    }
}
