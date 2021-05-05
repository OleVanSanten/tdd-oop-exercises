using System;
using System.Collections.Generic;
using System.Text;
using TestTools.TypeSystem;

namespace TestTools.Structure
{
    public abstract class TypeTranslator : ITypeTranslator
    {
        public NamespaceDescription TargetNamespace { get; set; }

        public VerifierServiceBase Verifier { get; set; }

        public abstract TypeDescription Translate(TypeDescription type);
    }
}
