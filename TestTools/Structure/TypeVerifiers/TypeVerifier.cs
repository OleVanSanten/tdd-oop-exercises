using System;
using System.Collections.Generic;
using System.Text;
using TestTools.TypeSystem;

namespace TestTools.Structure
{
    public abstract class TypeVerifier : ITypeVerifier
    {
        public VerifierServiceBase Verifier { get; set; }

        public IStructureService Service { get; set; }

        public abstract TypeVerificationAspect[] Aspects { get; }

        public abstract void Verify(TypeDescription originalType, TypeDescription translatedType);
    }
}
