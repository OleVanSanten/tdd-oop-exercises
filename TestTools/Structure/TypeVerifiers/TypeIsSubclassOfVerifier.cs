using System;
using System.Collections.Generic;
using System.Text;

namespace TestTools.Structure
{
    public class TypeIsSubclassOfVerifier : TypeVerifier
    {
        Type _type;

        public TypeIsSubclassOfVerifier(Type type)
        {
            _type = type;
        }

        public override TypeVerificationAspect[] Aspects => new[]
        {
            TypeVerificationAspect.IsSubclassOf
        };

        public override void Verify(Type originalType, Type translatedType)
        {
            Type baseType = Service.TranslateType(_type);
            Verifier.VerifyIsSubclassOf(translatedType, baseType);
        }
    }
}
