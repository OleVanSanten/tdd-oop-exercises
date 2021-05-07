using System;
using System.Collections.Generic;
using System.Text;
using TestTools.Structure;
using TestTools.Templates;

namespace TestTools.MSTest
{
    [AttributeEquivalent("Microsoft.VisualStudio.TestTools.UnitTesting.TestClass")]
    public class TemplatedTestClass : Attribute
    {
    }
}
