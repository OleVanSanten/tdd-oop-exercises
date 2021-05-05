using System;
using System.Collections.Generic;
using System.Text;
using TestTools.TypeSystem;

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

        public override void Verify(TypeDescription originalType, TypeDescription translatedType)
        {
            Verifier.VerifyBaseType(translatedType, new RuntimeTypeDescription(_type));
        }
    }
}
