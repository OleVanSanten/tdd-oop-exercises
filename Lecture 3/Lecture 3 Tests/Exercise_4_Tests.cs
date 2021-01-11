﻿using Lecture_3_Solutions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestTools.Integrated;

namespace Lecture_3_Tests
{
    [TestClass]
    public class Exercise_4_Tests
    {
        TestFactory factory = new TestFactory("Lecture_3");

        /* Exercise 4A */
        [TestMethod("a. Employee.ToString() returns expected output"), TestCategory("Exercise 4A")]
        public void EmployeeToStringReturnsExpectedOutput()
        {
            UnitTest test = factory.CreateTest();
            UnitTestObject<Employee> employee = test.Create<Employee>();

            test.Arrange(employee, () => new Employee("Joe Stevens") { Title = "Programmer" });
            test.Assert(employee, e => e.ToString() == "Employee Joe Stevens (Programmer)");

            test.Execute();
        }

        /* Exercise 4B */
        [TestMethod("a. Manager.ToString() returns expected output"), TestCategory("Exercise 4B")]
        public void ManagerToStringReturnsExpectedOutput()
        {
            UnitTest test = factory.CreateTest();
            UnitTestObject<Manager> manager = test.Create<Manager>();

            test.Arrange(manager, () => new Manager("Mary Stevens") { Title = "Software Engineer" });
            test.Assert(manager, m => m.ToString() == "Manager Mary Stevens (Software Engineer)");

            test.Execute();
        }
    }
}
