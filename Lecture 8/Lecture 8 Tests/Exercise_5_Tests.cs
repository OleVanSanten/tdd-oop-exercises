using Lecture_8_Solutions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel;
using TestTools.Expressions;
using TestTools.MSTest;
using TestTools.Structure;
using static Lecture_8_Tests.TestHelper;
using static TestTools.Expressions.TestExpression;

namespace Lecture_8_Tests
{
    [TestClass]
    public class Exercise_5_Tests
    {
        #region Exercise 5A
        [TestMethod("a. Customer.ID is a public property"), TestCategory("Exercise 5A")]
        public void CustomerIDIsAPublicProperty()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicProperty<Customer, int>(c => c.ID);
            test.Execute();
        }

        [TestMethod("b. Customer.FirstName is a public property"), TestCategory("Exercise 5A")]
        public void CustomerFirstNameIsAPublicProperty()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicProperty<Customer, string>(c => c.FirstName);
            test.Execute();
        }

        [TestMethod("c. Customer.LastName is a public property"), TestCategory("Exercise 5A")]
        public void CustomerLastNameIsAPublicProperty()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicProperty<Customer, string>(c => c.LastName);
            test.Execute();
        }
        #endregion

        #region exercise 5B
        [TestMethod("a. Customer.ID emits PropertyChanged event on new value"), TestCategory("Exercise 5B")]
        public void CustomerIDEmitsPropertyChangedEvent()
        {
            bool isCalled = false;
            Customer customer = new Customer()
            {
                ID = 0
            };
            customer.PropertyChanged += (sender, e) => isCalled = true;

            customer.ID = 1;

            Assert.IsTrue(isCalled, "The Customer.ID event is never emitted");

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Customer> _customer = test.CreateVariable<Customer>();
            test.Arrange(_customer, Expr(() => new Customer() { ID = 0 }));
            test.DelegateAssert.IsInvoked(Lambda<PropertyChangedEventHandler>(handler => Expr(_customer, c => c.AddPropertyChanged(handler))));
            test.Act(Expr(_customer, c => c.SetID(1)));
            test.Execute();
        }

        [TestMethod("b. Customer.ID does not emit PropertyChanged event on same value"), TestCategory("Exercise 5B")]
        public void CustomerIDDoesNotEmitPropertyChangedEvent()
        {
            bool isCalled = false;
            Customer customer = new Customer()
            {
                ID = 0
            };
            customer.PropertyChanged += (sender, e) => isCalled = true;

            customer.ID = 0;

            Assert.IsFalse(isCalled, "The Customer.ID event is emitted");

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Customer> _customer = test.CreateVariable<Customer>();
            test.Arrange(_customer, Expr(() => new Customer() { ID = 0 }));
            test.DelegateAssert.IsNotInvoked(Lambda<PropertyChangedEventHandler>(handler => Expr(_customer, c => c.AddPropertyChanged(handler))));
            test.Act(Expr(_customer, c => c.SetID(0)));
            test.Execute();
        }

        [TestMethod("c. Customer.FirstName emits PropertyChanged event on new value"), TestCategory("Exercise 5B")]
        public void CustomerFirstNameEmitsPropertyChangedEvent()
        {
            bool isCalled = false;
            Customer customer = new Customer()
            {
                FirstName = "abc"
            };
            customer.PropertyChanged += (sender, e) => isCalled = true;

            customer.FirstName = "bcd";

            Assert.IsTrue(isCalled, "The Customer.ID event is never emitted");

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Customer> _customer = test.CreateVariable<Customer>();
            test.Arrange(_customer, Expr(() => new Customer() { FirstName = "abc" }));
            test.DelegateAssert.IsInvoked(Lambda<PropertyChangedEventHandler>(handler => Expr(_customer, c => c.AddPropertyChanged(handler))));
            test.Act(Expr(_customer, c => c.SetFirstName("bcd")));
            test.Execute();
        }

        [TestMethod("d. Customer.FirstName does not emit PropertyChanged event on same value"), TestCategory("Exercise 5B")]
        public void CustomerFirstNameDoesNotEmitPropertyChangedEvent()
        {
            bool isCalled = false;
            Customer customer = new Customer()
            {
                FirstName = "abc"
            };
            customer.PropertyChanged += (sender, e) => isCalled = true;

            customer.FirstName = "abc";

            Assert.IsFalse(isCalled, "The Customer.ID event is emitted");

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Customer> _customer = test.CreateVariable<Customer>();
            test.Arrange(_customer, Expr(() => new Customer() { FirstName = "abc" }));
            test.DelegateAssert.IsNotInvoked(Lambda<PropertyChangedEventHandler>(handler => Expr(_customer, c => c.AddPropertyChanged(handler))));
            test.Act(Expr(_customer, c => c.SetFirstName("abc")));
            test.Execute();
        }

        [TestMethod("e. Customer.LastName emits PropertyChanged event on new value"), TestCategory("Exercise 5B")]
        public void CustomerLastNameEmitsPropertyChangedEvent()
        {
            bool isCalled = false;
            Customer customer = new Customer()
            {
                LastName = "abc"
            };
            customer.PropertyChanged += (sender, e) => isCalled = true;

            customer.LastName = "bcd";

            Assert.IsTrue(isCalled, "The Customer.ID event is never emitted");

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Customer> _customer = test.CreateVariable<Customer>();
            test.Arrange(_customer, Expr(() => new Customer() { LastName = "abc" }));
            test.DelegateAssert.IsInvoked(Lambda<PropertyChangedEventHandler>(handler => Expr(_customer, c => c.AddPropertyChanged(handler))));
            test.Act(Expr(_customer, c => c.SetLastName("bcd")));
            test.Execute();
        }

        [TestMethod("f. Customer.LastName does not emit PropertyChanged event on same value"), TestCategory("Exercise 5B")]
        public void CustomerLastNameDoesNotEmitPropertyChangedEvent()
        {
            bool isCalled = false;
            Customer customer = new Customer()
            {
                LastName = "abc"
            };
            customer.PropertyChanged += (sender, e) => isCalled = true;

            customer.LastName = "abc";

            Assert.IsFalse(isCalled, "The Customer.ID event is emitted");

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Customer> _customer = test.CreateVariable<Customer>();
            test.Arrange(_customer, Expr(() => new Customer() { LastName = "abc" }));
            test.DelegateAssert.IsNotInvoked(Lambda<PropertyChangedEventHandler>(handler => Expr(_customer, c => c.AddPropertyChanged(handler))));
            test.Act(Expr(_customer, c => c.SetLastName("abc")));
            test.Execute();
        } 
        #endregion
    }
}
