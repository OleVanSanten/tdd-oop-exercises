using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace TestTools.Structure
{
    public class MemberIsStaticVerifier : MemberVerifier
    {
        bool _isStatic;

        public MemberIsStaticVerifier(bool isStatic = true)
        {
            _isStatic = isStatic;
        }

        public override MemberVerificationAspect[] Aspects => new[] {
            MemberVerificationAspect.FieldIsStatic,
            MemberVerificationAspect.MethodIsStatic,
            MemberVerificationAspect.PropertyIsStatic
        };

        public override void Verify(MemberInfo originalMember, MemberInfo translatedMember)
        {
            Verifier.VerifyMemberType(translatedMember, new[] { MemberTypes.Field, MemberTypes.Property, MemberTypes.Method });

            if (translatedMember is FieldInfo translatedField)
            {
                Verifier.VerifyIsStatic(translatedField, _isStatic);
            }
            else if (translatedMember is PropertyInfo translatedProperty)
            {
                Verifier.VerifyIsStatic(translatedProperty, _isStatic);
            }
            else if (translatedMember is MethodInfo translatedMethod)
            {
                Verifier.VerifyIsStatic(translatedMethod, _isStatic);
            }
            else throw new NotImplementedException();
        }
    }
}
