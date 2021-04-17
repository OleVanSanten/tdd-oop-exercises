using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using TestTools.Structure;
using System.Linq.Expressions;
using System.Reflection;
using static TestTools_Tests.TestHelper;

namespace TestTools_Tests.Structure
{
    [TestClass]
    public class TypeVisitorTests
    {
        class ClassA
        {
            public int Field;
            public int Property { get; set; }
            public event EventHandler Event;
            public void VoidMethod()
            {
            }
        }

        class ClassB
        {
            public int Field;
            public int Property { get; set; }
            public event EventHandler Event;
            public void VoidMethod()
            {
            }
        }

        ConstructorInfo ClassAConstructor = typeof(ClassA).GetConstructor(new Type[0]);
        FieldInfo ClassAField = typeof(ClassA).GetField("Field");
        EventInfo ClassAEvent = typeof(ClassA).GetEvent("Event");
        MethodInfo ClassAVoidMethod = typeof(ClassA).GetMethod("VoidMethod", new Type[0]);
        PropertyInfo ClassAProperty = typeof(ClassA).GetProperty("Property");

        ConstructorInfo ClassBConstructor = typeof(ClassB).GetConstructor(new Type[0]);
        FieldInfo ClassBField = typeof(ClassB).GetField("Field");
        EventInfo ClassBEvent = typeof(ClassB).GetEvent("Event");
        MethodInfo ClassBVoidMethod = typeof(ClassB).GetMethod("VoidMethod", new Type[0]);
        PropertyInfo ClassBProperty = typeof(ClassB).GetProperty("Property");

        [TestMethod("Visit correctly transforms ParameterExpression")]
        public void VisitCorrectlyTransformsParameterExpression()
        {
            IStructureService service = Substitute.For<IStructureService>();
            service.TranslateType(typeof(ClassA)).Returns(typeof(ClassB));
            TypeVisitor visitor = new TypeVisitor(service)
            {
                TypeVerifiers = new ITypeVerifier[0]
            };

            Expression input = Expression.Parameter(typeof(ClassA));
            Expression expected = Expression.Parameter(typeof(ClassB));

            Expression actual = visitor.Visit(input);

            AssertAreEqualExpressions(expected, actual);
        }

        [TestMethod("Visit correctly transforms NewExpression")]
        public void VisitCorrectlyTransformsNewExpression()
        {
            IStructureService service = Substitute.For<IStructureService>();
            service.TranslateType(typeof(ClassA)).Returns(typeof(ClassB));
            service.TranslateMember(ClassAConstructor).Returns(ClassBConstructor);
            TypeVisitor visitor = new TypeVisitor(service)
            {
                TypeVerifiers = new ITypeVerifier[0],
                MemberVerifiers = new IMemberVerifier[0]
            };

            Expression input = Expression.New(ClassAConstructor);
            Expression expected = Expression.New(ClassBConstructor);

            Expression actual = visitor.Visit(input);

            AssertAreEqualExpressions(expected, actual);
        }

        [TestMethod("Visit correctly transforms MethodCallExpression")]
        public void VisitCorrectlyTransformsMethodCallExpression()
        {
            IStructureService service = Substitute.For<IStructureService>();
            service.TranslateType(typeof(ClassA)).Returns(typeof(ClassB));
            service.TranslateMember(ClassAVoidMethod).Returns(ClassBVoidMethod);
            TypeVisitor visitor = new TypeVisitor(service)
            {
                TypeVerifiers = new ITypeVerifier[0],
                MemberVerifiers = new IMemberVerifier[0]
            };

            Expression input = Expression.Call(Expression.Parameter(typeof(ClassA)), ClassAVoidMethod);
            Expression expected = Expression.Call(Expression.Parameter(typeof(ClassB)), ClassBVoidMethod);

            Expression actual = visitor.Visit(input);

            AssertAreEqualExpressions(expected, actual);
        }

        [TestMethod("Visit correctly transforms MemberExpression for field")]
        public void VisitCorrectlyTransformMemberExpressionForField()
        {
            IStructureService service = Substitute.For<IStructureService>();
            service.TranslateType(typeof(ClassA)).Returns(typeof(ClassB));
            service.TranslateMember(ClassAField).Returns(ClassBField);
            TypeVisitor visitor = new TypeVisitor(service)
            {
                TypeVerifiers = new ITypeVerifier[0],
                MemberVerifiers = new IMemberVerifier[0]
            };

            Expression input = Expression.Field(Expression.Parameter(typeof(ClassA)), ClassAField);
            Expression expected = Expression.Field(Expression.Parameter(typeof(ClassB)), ClassBField);

            Expression actual = visitor.Visit(input);

            AssertAreEqualExpressions(expected, actual);
        }

        [TestMethod("Visit correctly transforms MemberExpression for property")]
        public void VisitCorrectlyTransformMemberExpressionForProperty()
        {
            IStructureService service = Substitute.For<IStructureService>();
            service.TranslateType(typeof(ClassA)).Returns(typeof(ClassB));
            service.TranslateMember(ClassAProperty).Returns(ClassBProperty);
            TypeVisitor visitor = new TypeVisitor(service)
            {
                TypeVerifiers = new ITypeVerifier[0],
                MemberVerifiers = new IMemberVerifier[0]
            };

            Expression input = Expression.Property(Expression.Parameter(typeof(ClassA)), ClassAProperty);
            Expression expected = Expression.Property(Expression.Parameter(typeof(ClassB)), ClassBProperty);

            Expression actual = visitor.Visit(input);

            AssertAreEqualExpressions(expected, actual);
        }
        
        //TODO write tests to verify verifications
    }
}
