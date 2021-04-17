using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace TestTools.Structure
{
    public interface IMemberTranslator
    {
        Type TargetType { get; set; }
        VerifierServiceBase Verifier { get; set; }
        MemberInfo Translate(MemberInfo member);
    }
}
