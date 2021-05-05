using System;
using System.Collections.Generic;
using System.Text;
using TestTools.TypeSystem;

namespace TestTools.Structure
{
    public interface ITypeVerifier
    {
        VerifierServiceBase Verifier { get; set; }
        IStructureService Service { get; set; }
        TypeVerificationAspect[] Aspects { get; }
        void Verify(TypeDescription originalType, TypeDescription translatedType);
    }
}
