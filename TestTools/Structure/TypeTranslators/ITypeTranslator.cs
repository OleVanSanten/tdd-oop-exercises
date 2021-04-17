using System;
using System.Collections.Generic;
using System.Text;

namespace TestTools.Structure
{
    public interface ITypeTranslator
    {
        string TargetNamespace { get; set; }
        VerifierServiceBase Verifier { get; set; }
        Type Translate(Type type);
    }
}
