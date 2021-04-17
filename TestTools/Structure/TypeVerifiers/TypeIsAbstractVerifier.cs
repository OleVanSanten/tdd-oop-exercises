using System;
using System.Collections.Generic;
using System.Text;

namespace TestTools.Structure
{
    public class TypeIsAbstractVerifier : TypeVerifier
    {
        bool _isAbstract;

        public TypeIsAbstractVerifier(bool isAbstract = true)
        {
            _isAbstract = isAbstract;
        }

        public override TypeVerificationAspect[] Aspects => new[] {
            TypeVerificationAspect.IsAbstract
        };

        public override void Verify(Type originalType, Type translatedType)
        {
            Verifier.VerifyIsAbstract(translatedType, _isAbstract);
        }
    }
}
