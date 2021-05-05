using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using TestTools.TypeSystem;

namespace TestTools.Structure
{
    public interface IMemberTranslator
    {
        TypeDescription TargetType { get; set; }
        VerifierServiceBase Verifier { get; set; }
        MemberDescription Translate(MemberDescription member);
    }
}
