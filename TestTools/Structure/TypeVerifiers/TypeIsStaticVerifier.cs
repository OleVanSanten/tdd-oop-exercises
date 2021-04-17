using System;
using System.Collections.Generic;
using System.Text;

namespace TestTools.Structure
{
    public class TypeIsStaticVerifier : TypeVerifier
    {
        bool _isStatic;

        public TypeIsStaticVerifier(bool isStatic = true)
        {
            _isStatic = isStatic;
        }

        public override TypeVerificationAspect[] Aspects => new[] {
            TypeVerificationAspect.IsStatic 
        };

        public override void Verify(Type originalType, Type translatedType)
        {
            Verifier.VerifyIsStatic(translatedType, _isStatic);
        }
    }
}
