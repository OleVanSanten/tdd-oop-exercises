using System;
using System.Collections.Generic;
using System.Text;
using TestTools.Structure;

namespace TestTools.MSTest
{
    public class TestFactory
    {
        public IStructureService StructureService { get; set; }

        public TestFactory(string fromNamespace, string toNamespace)
        {
            StructureService = new StructureService(fromNamespace, toNamespace)
            {
                StructureVerifier = new VerifierService()
            };
        }

        public UnitTest CreateTest()
        {
            return new UnitTest(StructureService);
        }

        public StructureTest CreateStructureTest()
        {
            return new StructureTest(StructureService);
        }
    }
}
