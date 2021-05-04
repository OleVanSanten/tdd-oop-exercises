using System;
using System.Collections.Generic;
using System.Text;

namespace TestTools.TypeSystem
{
    public abstract class MemberDescription
    {
        // Attributes should be runtime, because then they make sense xD

        public abstract TypeDescription DeclaringType { get; }

        public abstract MemberTypes MemberType { get; }

        public abstract string Name { get; }
    }
}
