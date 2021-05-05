using System;
using System.Collections.Generic;
using System.Text;
using TestTools.TypeSystem;

namespace TestTools.Structure
{
    public class TypeIsInterfaceVerifier : TypeVerifier
    {
        public override TypeVerificationAspect[] Aspects => new[] 
        {
            TypeVerificationAspect.IsInterface 
        };

        public override void Verify(TypeDescription originalType, TypeDescription translatedType)
        {
            Verifier.VerifyIsInterface(translatedType);
        }
    }
}
