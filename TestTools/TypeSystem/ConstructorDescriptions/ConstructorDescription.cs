using System;
using System.Collections.Generic;
using System.Text;

namespace TestTools.TypeSystem
{
    public abstract class ConstructorDescription : MethodBaseDescription
    {
        // TODO add Attributes

        public override MemberTypes MemberType => MemberTypes.Constructor;
    }
}
