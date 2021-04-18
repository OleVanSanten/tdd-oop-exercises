using System;
using System.Collections.Generic;
using System.Text;

namespace TestTools.Structure
{
    public abstract class TemplatedAttribute : Attribute
    {
        // Generates the attribute that the TemplatedAttribute
        // templates for. Output example: TestMethod("Test name")
        public abstract string MakeConcrete();
    }
}
