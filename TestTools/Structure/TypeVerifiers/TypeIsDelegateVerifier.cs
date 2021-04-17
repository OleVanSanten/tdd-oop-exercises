using System;
using System.Collections.Generic;
using System.Text;

namespace TestTools.Structure
{
    public class TypeIsDelegateVerifier : TypeVerifier
    {
        public override TypeVerificationAspect[] Aspects => new[]
        {
            TypeVerificationAspect.IsDelegate
        };

        public override void Verify(Type originalType, Type translatedType)
        {
            Verifier.VerifyIsDelegate(translatedType);
        }
    }
}
