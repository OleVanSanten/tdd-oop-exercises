using System;
using System.Collections.Generic;
using System.Text;

namespace TestTools.Structure
{
    public class UnchangedTypeIsAbstractVerifier : TypeVerifier
    {
        public override TypeVerificationAspect[] Aspects => new[] { 
            TypeVerificationAspect.IsAbstract
        };

        public override void Verify(Type originalType, Type translatedType)
        {
            Verifier.VerifyIsAbstract(translatedType, originalType.IsAbstract);
        }
    }
}
