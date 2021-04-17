using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

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

        public override void Verify(MemberInfo originalMember, MemberInfo translatedMember)
        {
            Verifier.VerifyMemberType(translatedMember, new[] { MemberTypes.Event });
            Verifier.VerifyEventHandlerType((EventInfo)translatedMember, _type);
        }
    }
}
