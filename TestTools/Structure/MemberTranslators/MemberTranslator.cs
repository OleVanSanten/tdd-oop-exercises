using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using TestTools.TypeSystem;

namespace TestTools.Structure
{
    public abstract class MemberTranslator : IMemberTranslator
    {
        public TypeDescription TargetType { get; set; }

        public VerifierServiceBase Verifier { get; set; }

        public abstract MemberDescription Translate(MemberDescription member);
    }
}
