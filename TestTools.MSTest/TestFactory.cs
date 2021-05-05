using System;
using System.Collections.Generic;
using System.Text;
using TestTools.Structure;
using TestTools.TypeSystem;

namespace TestTools.MSTest
{
    public class TestFactory
    {
        public IStructureService StructureService { get; set; }

        public TestFactory(string fromNamespace, string toNamespace)
            : this(new RuntimeNamespaceDescription(fromNamespace), new RuntimeNamespaceDescription(toNamespace))
        {
        }

        public TestFactory(NamespaceDescription fromNamespace, NamespaceDescription toNamespace)
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
