using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using TestTools.Structure;

namespace TestTools_Tests.Structure
{
    public class TestTypeWithoutCustomTranslator
    {
        public int FieldWithoutCustomTranslator;

        [AlternateNames("AlternateField")]
        public int FieldWithCustomTranslator;
    }

    [AlternateNames("AlternateTestType")]
    public class TestTypeWithCustomTranslator
    {
    }

    [TestClass]
    public class StructureServiceTests
    {
        [TestMethod("TranslateType uses TypeTranslator if no custom translator is defined on type")]
        public void TranslateTypeUsesTypeTranslatorIfNoCustomTranslatorIsDefinedOnType()
        {
            ITypeTranslator translator = Substitute.For<ITypeTranslator>();
            StructureService service = new StructureService("TestTools_Tests.Structure", "TestTools_Tests.Structure")
            {
                TypeTranslator = translator
            };

            service.TranslateType(typeof(TestTypeWithoutCustomTranslator));

            translator.Received().Translate(typeof(TestTypeWithoutCustomTranslator));
        }

        [TestMethod("TranslateType does not use TypeTranslator if type is not in FromNamespace")]
        public void TranslateTypeDoesNotUseTypeTranslatorIfTypeIsNotInFromNamespace()
        {
            ITypeTranslator translator = Substitute.For<ITypeTranslator>();
            StructureService service = new StructureService("TestTools_Tests.Structure", "TestTools_Tests.Structure")
            {
                TypeTranslator = translator
            };

            service.TranslateType(typeof(object));

            translator.DidNotReceive().Translate(typeof(object));
        }

        [TestMethod("TranslateType does not use TypeTranslator if custom translator is defined on type")]
        public void TranslateTypeDoesNotUseTypeTranslatorIfCustomTranslatorIsDefinedOnType()
        {
            ITypeTranslator translator = Substitute.For<ITypeTranslator>();
            StructureService service = new StructureService("TestTools_Tests.Structure", "TestTools_Tests.Structure")
            {
                TypeTranslator = translator
            };

            service.TranslateType(typeof(TestTypeWithCustomTranslator));

            translator.DidNotReceive().Translate(typeof(TestTypeWithCustomTranslator));
        }

        [TestMethod("VerifyType(Type, ITypeVerifier[]) uses all type verifiers")]
        public void VerifyTypeOverloadUsesAllTypeVerifiers()
        {
            Type typeToVerify = typeof(object);

            ITypeVerifier verifier1 = Substitute.For<ITypeVerifier>();
            verifier1.Aspects.Returns(new[] { TypeVerificationAspect.AccessLevel });
            ITypeVerifier verifier2 = Substitute.For<ITypeVerifier>();
            verifier2.Aspects.Returns(new[] { TypeVerificationAspect.IsSubclassOf });
            StructureService service = new StructureService("TestTools_Tests.Structure", "TestTools_Tests.Structure");

            service.VerifyType(typeToVerify, new[] { verifier1, verifier2 });

            verifier1.Received().Verify(typeToVerify, typeToVerify);
            verifier2.Received().Verify(typeToVerify, typeToVerify);
        }

        [TestMethod("TranslateMember uses MemberTranslator if no custom translator is defined on member")]
        public void TranslateMemberUsesMemberTranslatorIfNoCustomTranslatorIsDefinedOnMember()
        {
            Type typeToTranslate = typeof(TestTypeWithoutCustomTranslator);
            FieldInfo fieldToTranslate = typeToTranslate.GetField("FieldWithoutCustomTranslator");

            ITypeTranslator typeTranslator = Substitute.For<ITypeTranslator>();
            typeTranslator.Translate(typeToTranslate).Returns(typeToTranslate);
            IMemberTranslator memberTranslator = Substitute.For<IMemberTranslator>();
            StructureService service = new StructureService("TestTools_Tests.Structure", "TestTools_Tests.Structure")
            {
                TypeTranslator = typeTranslator,
                MemberTranslator = memberTranslator
            };

            service.TranslateMember(fieldToTranslate);

            memberTranslator.Received().Translate(fieldToTranslate);
        }

        [TestMethod("TranslateMember does not use MemberTranslator if member's DeclaringType is not within FromNamespace")]
        public void TranslateMemberDoesNotUseMemberTranslatorIfMemberDeclaringTypeIsNotWithinFromNamespace()
        {
            PropertyInfo propertyToVerify = typeof(string).GetProperty("Length");

            IMemberTranslator memberTranslator = Substitute.For<IMemberTranslator>();
            StructureService service = new StructureService("TestTools_Tests.Structure", "TestTools_Tests.Structure")
            {
                MemberTranslator = memberTranslator
            };

            service.TranslateMember(propertyToVerify);

            memberTranslator.DidNotReceive().Translate(propertyToVerify);
        }

        [TestMethod("TranslateMember does not use MemberTranslator if custom translator is defined on member")]
        public void TranslateMemberDoesNotUseMemberTranslatorIfCustomerTranslatorIsDefinedOnMember()
        {
            Type typeToTranslate = typeof(TestTypeWithoutCustomTranslator);
            FieldInfo fieldToTranslate = typeToTranslate.GetField("FieldWithCustomTranslator");

            ITypeTranslator typeTranslator = Substitute.For<ITypeTranslator>();
            typeTranslator.Translate(typeToTranslate).Returns(typeToTranslate);
            IMemberTranslator memberTranslator = Substitute.For<IMemberTranslator>();
            StructureService service = new StructureService("TestTools_Tests.Structure", "TestTools_Tests.Structure")
            {
                TypeTranslator = typeTranslator,
                MemberTranslator = memberTranslator
            };

            service.TranslateMember(fieldToTranslate);

            memberTranslator.DidNotReceive().Translate(fieldToTranslate);
        }

        [TestMethod("VerifyMember(MemberInfo, IMemberVerifier[]) uses all member verifiers")]
        public void VerifyMemberOverloadUsesAllMemberVerifiers()
        {
            PropertyInfo propertyToVerify = typeof(string).GetProperty("Length");
            // The verifiers must have aspects as StructureService depends on aspect to use the verifier
            IMemberVerifier verifier1 = Substitute.For<IMemberVerifier>();
            verifier1.Aspects.Returns(new[] { MemberVerificationAspect.PropertyType });
            IMemberVerifier verifier2 = Substitute.For<IMemberVerifier>();
            verifier2.Aspects.Returns(new[] { MemberVerificationAspect.PropertyIsStatic });
            StructureService service = new StructureService("TestTools_Tests.Structure", "TestTools_Tests.Structure");

            service.VerifyMember(propertyToVerify, new[] { verifier1, verifier2 });

            verifier1.Received().Verify(propertyToVerify, propertyToVerify);
            verifier2.Received().Verify(propertyToVerify, propertyToVerify);
        }
    }
}
