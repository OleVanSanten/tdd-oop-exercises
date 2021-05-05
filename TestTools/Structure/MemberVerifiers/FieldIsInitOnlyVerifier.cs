using System;
using System.Collections.Generic;
using System.Text;
using TestTools.TypeSystem;

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

        public override void Verify(MemberDescription originalMember, MemberDescription translatedMember)
        {
            Verifier.VerifyMemberType(translatedMember, new[] { MemberTypes.Field });
            Verifier.VerifyIsInitOnly((FieldDescription)translatedMember, _isInitOnly);
        }
    }
}
