using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace TestTools.Structure
{
    public class FieldIsInitOnlyVerifier : MemberVerifier
    {
        bool _isInitOnly;

        public FieldIsInitOnlyVerifier(bool isInitOnly = true)
        {
            _isInitOnly = isInitOnly;
        }

        public override MemberVerificationAspect[] Aspects => new[] {
            MemberVerificationAspect.MemberType,
            MemberVerificationAspect.FieldAccessLevel
        };

        public override void Verify(MemberInfo originalMember, MemberInfo translatedMember)
        {
            Verifier.VerifyMemberType(translatedMember, new[] { MemberTypes.Field });
            Verifier.VerifyIsInitOnly((FieldInfo)translatedMember, _isInitOnly);
        }
    }
}
