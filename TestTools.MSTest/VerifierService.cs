using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestTools.Helpers;
using TestTools.Structure;

namespace TestTools.MSTest
{
    public class VerifierService : VerifierServiceBase
    {
        public override void Fail(string message)
        {
            throw new AssertFailedException(message);
        }
    }
}
