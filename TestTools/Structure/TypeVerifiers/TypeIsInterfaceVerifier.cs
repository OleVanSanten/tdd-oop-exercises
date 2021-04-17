using System;
using System.Collections.Generic;
using System.Text;

namespace TestTools.Structure
{
    public class TypeIsInterfaceVerifier : TypeVerifier
    {
        public override TypeVerificationAspect[] Aspects => new[] 
        {
            TypeVerificationAspect.IsInterface 
        };


        public override void Verify(Type originalType, Type translatedType)
        {
            Verifier.VerifyIsInterface(translatedType);
        }
    }
}
