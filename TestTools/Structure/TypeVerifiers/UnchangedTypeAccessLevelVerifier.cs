using System;
using System.Collections.Generic;
using System.Text;
using TestTools.Helpers;

namespace TestTools.Structure
{
    public class UnchangedTypeAccessLevelVerifier : TypeVerifier
    {
        public override TypeVerificationAspect[] Aspects => new[] { 
            TypeVerificationAspect.AccessLevel 
        };

        public override void Verify(Type originalType, Type translatedType)
        {
            AccessLevels accessLevel = ReflectionHelper.GetAccessLevel(originalType);
            Verifier.VerifyAccessLevel(translatedType, new[] { accessLevel });
        }
    }
}
