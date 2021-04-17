using Lecture_3_Solutions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestTools.Expressions;
using TestTools.MSTest;
using static Lecture_3_Tests.TestHelper;
using static TestTools.Expressions.TestExpression;

namespace Lecture_3_Tests
{
    [TestClass]
    public class Exercise_4_Tests
    {
        #region Exercise 4B
        [TestMethod("a. Employee.ToString() returns expected output"), TestCategory("Exercise 4B")]
        public void EmployeeToStringReturnsExpectedOutput()
        {
            Employee employee = new Employee("Joe Stevens")
            {
                Title = "Programmer"
            };
            Assert.AreEqual("Employee Joe Stevens (Programmer)", employee.ToString());

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Employee> _employee = test.CreateVariable<Employee>(nameof(_employee));
            test.Arrange(_employee, Expr(() => new Employee("Joe Stevens") { Title = "Programmer" }));
            test.Assert.AreEqual(Const("Employee Joe Stevens (Programmer)"), Expr(_employee, e => e.ToString()));
            test.Execute();
        }
        #endregion

        #region Exercise 4C
        [TestMethod("a. Manager.ToString() returns expected output"), TestCategory("Exercise 4C")]
        public void ManagerToStringReturnsExpectedOutput()
        {
            Manager manager = new Manager("Mary Stevens")
            {
                Title = "Software Engineer"
            };
            Assert.AreEqual("Manager Mary Stevens (Software Engineer)", manager.ToString());

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Manager> _manager = test.CreateVariable<Manager>(nameof(_manager));
            test.Arrange(_manager, Expr(() => new Manager("Mary Stevens") { Title = "Software Engineer" }));
            test.Assert.AreEqual(Const("Manager Mary Stevens (Software Engineer)"), Expr(_manager, m => m.ToString()));
            test.Execute();
        }
        #endregion
    }
}
