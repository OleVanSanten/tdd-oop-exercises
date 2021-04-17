using System;
using System.Collections.Generic;
using System.Text;

namespace TestTools.Structure
{
    public class TypeBaseClassVerifier : TypeVerifier
    {
        Type _type;

        public TypeBaseClassVerifier(Type type)
        {
            _type = type;
        }

        public override TypeVerificationAspect[] Aspects => new[]
        {
            TypeVerificationAspect.IsSubclassOf
        };

        public override void Verify(Type originalType, Type translatedType)
        {
            Verifier.VerifyBaseType(translatedType, _type);
        }
    }
}
