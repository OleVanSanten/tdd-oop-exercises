using System;
using System.Collections.Generic;
using System.Text;
using TestTools.Structure;
using TestTools.Templates;

namespace TestTools.MSTest
{
    public class TemplatedTestClass : TemplatedAttribute
    {
        public override string MakeConcrete()
        {
            return "[TestClass]";
        }
    }
}
