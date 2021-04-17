using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace TestTools.Structure
{
    public abstract class MemberTranslator : IMemberTranslator
    {
        public Type TargetType { get; set; }

        public VerifierServiceBase Verifier { get; set; }

        public abstract MemberInfo Translate(MemberInfo member);
    }
}
