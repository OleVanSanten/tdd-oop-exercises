using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using TestTools.Structure;
using NSubstitute;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestTools_Tests.Structure
{
    [TestClass]
    public class IStructureServiceExtensionsTests
    {
        [TestMethod("VerifyType(Type, ITypeVerifier[], TypeVerificationAspect[]) uses subset of verifiers")]
        public void VerifyTypeOverloadUsesSubsetOfAllTypeVerifiers()
        {
            Type typeToVerify = typeof(object);

            ITypeVerifier verifier1 = Substitute.For<ITypeVerifier>();
            verifier1.Aspects.Returns(new[] { TypeVerificationAspect.AccessLevel });
            ITypeVerifier verifier2 = Substitute.For<ITypeVerifier>();
            verifier2.Aspects.Returns(new[] { TypeVerificationAspect.IsSubclassOf });
            IStructureService service = Substitute.For<IStructureService>();

            service.VerifyType(typeToVerify, new[] { verifier1, verifier2 }, TypeVerificationAspect.AccessLevel);

            service.Received().VerifyType(typeToVerify, new[] { verifier1 });
        }

        [TestMethod("VerifyMember(MemberInfo, IMemberVerifier[], MemberVerificationAspect[]) uses subset of verifiers")]
        public void VerifyMemberOverloadUsesSubsetOfAllTypeVerifiers()
        {
            PropertyInfo propertyToVerify = typeof(string).GetProperty("Length");

            IMemberVerifier verifier1 = Substitute.For<IMemberVerifier>();
            verifier1.Aspects.Returns(new[] { MemberVerificationAspect.PropertyType });
            IMemberVerifier verifier2 = Substitute.For<IMemberVerifier>();
            verifier2.Aspects.Returns(new[] { MemberVerificationAspect.PropertyIsStatic });
            IStructureService service = Substitute.For<IStructureService>();

            service.VerifyMember(propertyToVerify, new[] { verifier1, verifier2 }, MemberVerificationAspect.PropertyType);

            service.Received().VerifyMember(propertyToVerify, new[] { verifier1 });
        }
    }
}
