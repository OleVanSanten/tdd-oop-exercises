using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace TestTools.Structure
{
    public class DelegateSignatureVerifier : TypeVerifier
    {
        Type _delegateType; 

        public DelegateSignatureVerifier(Type delegateType) 
        {
            _delegateType = delegateType;
        }

        public override TypeVerificationAspect[] Aspects => new[]
        {
            TypeVerificationAspect.DelegateSignature
        };

        public override void Verify(Type originalType, Type translatedType)
        {
            Verifier.VerifyIsDelegate(translatedType);
            Verifier.VerifyDelegateSignature(translatedType, _delegateType.GetMethod("Invoke"));
        }
    }
}
