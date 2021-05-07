﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TestTools.Structure;
using TestTools.Templates;

namespace TestTools.MSTest
{
    [AttributeEquivalent("Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod")]
    public class TemplatedTestMethod : Attribute
    {
        public TemplatedTestMethod(string message)
        {
        }
    }
}
