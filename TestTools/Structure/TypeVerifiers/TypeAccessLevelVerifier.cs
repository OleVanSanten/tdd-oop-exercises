using System;
using System.Collections.Generic;
using System.Text;

namespace TestTools.Structure
{
    public class TypeAccessLevelVerifier : TypeVerifier
    {
        AccessLevels[] _accessLevels;

        public TypeAccessLevelVerifier(AccessLevels accessLevel) : this(new[] { accessLevel })
        {
        }

        public TypeAccessLevelVerifier(AccessLevels[] accessLevels)
        {
            _accessLevels = accessLevels;
        }

        public override TypeVerificationAspect[] Aspects => new[]
        {
            TypeVerificationAspect.AccessLevel
        };

        public override void Verify(Type originalType, Type translatedType)
        {
            Verifier.VerifyAccessLevel(translatedType, _accessLevels);
        }
    }
}
