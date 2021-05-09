using System;
using System.Collections.Generic;
using System.Text;
using TestTools.Structure;

namespace TestTools.MSTest
{
    [AttributeEquivalent("Microsoft.VisualStudio.TestTools.UnitTesting.TestClass")]
    public class TemplatedTestClass : Attribute
    {
    }
}
