using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TestTools.Structure;
using TestTools.Templates;

namespace TestTools.MSTest
{
    public class TemplatedTestMethod : TemplatedAttribute
    {
        readonly bool _displayNameSet;
        readonly string _displayName;

        public TemplatedTestMethod()
        {
            _displayNameSet = false;
        }

        public TemplatedTestMethod(string displayName)
        {
            _displayName = displayName;
            _displayNameSet = true;
        }

        [TestMethod]
        public override string MakeConcrete()
        {
            if (_displayNameSet)
            {
                return $"[TestMethod({_displayName})]";
            }
            else return "[TestMethod]";
        }
    }
}
