using System;
using System.Collections.Generic;
using System.Text;
using TestTools.TypeSystem;

namespace TestTools.Structure
{
    public class TypeIsDelegateVerifier : TypeVerifier
    {
        public override TypeVerificationAspect[] Aspects => new[]
        {
            TypeVerificationAspect.IsDelegate
        };

        public override void Verify(TypeDescription originalType, TypeDescription translatedType)
        {
            Verifier.VerifyIsDelegate(translatedType);
        }
    }
}
