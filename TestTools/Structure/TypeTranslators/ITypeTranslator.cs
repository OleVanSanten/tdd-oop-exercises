using System;
using System.Collections.Generic;
using System.Text;
using TestTools.TypeSystem;

namespace TestTools.Structure
{
    public interface ITypeTranslator
    {
        NamespaceDescription TargetNamespace { get; set; }
        VerifierServiceBase Verifier { get; set; }
        TypeDescription Translate(TypeDescription type);
    }
}
