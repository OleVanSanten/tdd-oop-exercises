using Lecture_4_Solutions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestTools.Expressions;
using TestTools.Structure;
using TestTools.MSTest;
using static Lecture_4_Tests.TestHelper;
using static TestTools.Expressions.TestExpression;
using static TestTools.Helpers.StructureHelper;

namespace Lecture_4_Tests
{
    [TestClass]
    public class Exercise_7_Tests
    {
        #region Exercise 7A
        [TestMethod("a. NotOldEnoughException's base class is Exception"), TestCategory("Exercise 7A")]
        public void NotOldEnoughExceptionIsSubclassOfException()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertClass<NotOldEnoughException>(new TypeBaseClassVerifier(typeof(Exception)));
            test.Execute();
        }
        #endregion

        #region Exercise 7B
        [TestMethod("a. NotOldEnoughException() results in Message = \"Person is too young\""), TestCategory("Exercise 7B")]
        public void ParameterlessPersonConstructorAssignsMessageProperty()
        {
            NotOldEnoughException exception = new NotOldEnoughException();
            Assert.AreEqual("Person is too young", exception.Message);

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<NotOldEnoughException> _exception = test.CreateVariable<NotOldEnoughException>();
            test.Arrange(_exception, Expr(() => new NotOldEnoughException()));
            test.Assert.AreEqual(Const("Person is too young"), Expr(_exception, e => e.Message));
            test.Execute();
        }
        #endregion

        #region Exercise 7C
        [TestMethod("d. NotOldEnoughException(string activity) results in Message = \"Person is too young to [activity]\""), TestCategory("Exercise 7C")]
        public void PersonConstructorAssignsMessageProperty()
        {
            NotOldEnoughException exception = new NotOldEnoughException("do something");
            Assert.AreEqual("Person is too young to do something", exception.Message);

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<NotOldEnoughException> _exception = test.CreateVariable<NotOldEnoughException>();
            test.Arrange(_exception, Expr(() => new NotOldEnoughException("do something")));
            test.Assert.AreEqual(Const("Person is too young to do something"), Expr(_exception, e => e.Message));
            test.Execute();
        }
        #endregion

        #region Exercise 7D
        [TestMethod("a. Person.Age is public int property"), TestCategory("Exercise 7D")]
        public void PersonAgeIsPublicIntProperty()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicProperty<Person, int>(p => p.Age);
            test.Execute();
        }

        [TestMethod("b. Person.CalculateBMI() throws NotOldEnoughException for Age = 15"), TestCategory("Exercise 7D")]
        public void PersonCalculateBMIThrowsNotOldEnoughException()
        {
            Person person = new Person("abc")
            {
                Age = 15
            };
            Assert.ThrowsException<NotOldEnoughException>(() => person.CalculateBMI());

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Person> _person = test.CreateVariable<Person>();
            test.Arrange(_person, Expr(() => new Person("abc") { Age = 15 }));
            test.Assert.ThrowsExceptionOn<NotOldEnoughException>(Expr(_person, p => p.CalculateBMI()));
            test.Execute();
        }
        #endregion
    }
}
