using System;
using System.Collections.Generic;
using System.Text;
using TestTools.TypeSystem;

namespace TestTools.Structure
{
    public class EventHandlerTypeVerifier : MemberVerifier
    {
        Type _type;

        public EventHandlerTypeVerifier(Type type)
        {
            _type = type;
        }

        public override MemberVerificationAspect[] Aspects => new[]
        {
            MemberVerificationAspect.EventHandlerType
        };

        public override void Verify(MemberDescription originalMember, MemberDescription translatedMember)
        {
            Verifier.VerifyMemberType(translatedMember, new[] { MemberTypes.Event });
            Verifier.VerifyEventHandlerType((EventDescription)translatedMember, new RuntimeTypeDescription(_type));
        }
    }
}
