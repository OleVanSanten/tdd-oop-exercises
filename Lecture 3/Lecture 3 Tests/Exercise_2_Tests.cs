using Lecture_3_Solutions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TestTools.Expressions;
using TestTools.MSTest;
using TestTools.Structure;
using static Lecture_3_Tests.TestHelper;
using static TestTools.Expressions.TestExpression;

namespace Lecture_3_Tests
{
    [TestClass]
    public class Exercise_2_Tests 
    {   
        #region Exercise 2A
        [TestMethod("a. Name is public read-only string property"), TestCategory("Exercise 2A")]
        public void NameIsPublicStringProperty()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicReadonlyProperty<Employee, string>(e => e.Name);
            test.Execute();
        }

        [TestMethod("b. Title is public string property"), TestCategory("Exercise 2A")]
        public void TitleIsPublicStringProperty()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicProperty<Employee, string>(e => e.Title);
            test.Execute();
        }

        [TestMethod("c. MonthlySalary is public decimal property"), TestCategory("Exercise 2A")]
        public void MonthlySalaryIsPublicDecimalProperty()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicProperty<Employee, decimal>(e => e.MonthlySalary);
            test.Execute();
        }

        [TestMethod("d. Seniority is public int property"), TestCategory("Exercise 2A")]
        public void SeniorityIsPublicIntProperty()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicProperty<Employee, int>(e => e.Seniority);
            test.Execute();
        }

        [TestMethod("e. Employee constructor takes string as argument"), TestCategory("Exercise 2A")]
        public void EmployeeConstructorTakesStringAsArgument()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicConstructor<string, Employee>(name => new Employee(name));
            test.Execute();
        }

        [TestMethod("f. Employee constructor(string name) sets name property"), TestCategory("Exercise 2A")]
        public void EmployeeConstructorNameSetsNameProperty()
        {
            Employee employee = new Employee("abc");
            Assert.AreEqual("abc", employee.Name);

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Employee> _employee = test.CreateVariable<Employee>(nameof(_employee));
            test.Arrange(_employee, Expr(() => new Employee("abc")));
            test.Assert.AreEqual(Const("abc"), Expr(_employee, e => e.Name));
            test.Execute();
        }

        [TestMethod("g. Employee.MonthlySalary is initialized as 0"), TestCategory("Exercise 2A")]
        public void MonthlySalaryIsInitializedAs0()
        {
            Employee employee = new Employee("abc");
            Assert.AreEqual(0M, employee.MonthlySalary);

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Employee> _employee = test.CreateVariable<Employee>(nameof(_employee));
            test.Arrange(_employee, Expr(() => new Employee("abc")));
            test.Assert.AreEqual(Const(0M), Expr(_employee, e => e.MonthlySalary)); ;
            test.Execute();
        }

        [TestMethod("h. Employee.Seniority is initialized as 1"), TestCategory("Exercise 2A")]
        public void SenorityIsInitializedAs1()
        {
            Employee employee = new Employee("abc");
            Assert.AreEqual(1, employee.Seniority);

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Employee> _employee = test.CreateVariable<Employee>(nameof(_employee));
            test.Arrange(_employee, Expr(() => new Employee("abc")));
            test.Assert.AreEqual(Const(1), Expr(_employee, e => e.Seniority));
            test.Execute();
        }

        [TestMethod("i. Title ignores assignment of null"), TestCategory("Exercise 2A")]
        public void TitleIgnoresAssignmentOfNull()
        {
            Employee employee = new Employee("abc");
            Assert.AreEqual("Unknown", employee.Title);

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Employee> _employee = test.CreateVariable<Employee>(nameof(_employee));
            test.Arrange(_employee, Expr(() => new Employee("abc")));
            test.Act(Expr(_employee, e => e.SetTitle(null)));
            test.Assert.AreEqual(Const("Unknown"), Expr(_employee, e => e.Title)); ;
            test.Execute();
        }
       
        [TestMethod("j. Title ignores assignment of empty string"), TestCategory("Exercise 2A")]
        public void TitleIgnoresAssignmentOfEmptyString()
        {
            Employee employee = new Employee("abc");
            
            employee.Title = "";
            
            Assert.AreEqual("Unknown", employee.Title);

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Employee> _employee = test.CreateVariable<Employee>(nameof(_employee));
            test.Arrange(_employee, Expr(() => new Employee("abc")));
            test.Act(Expr(_employee, e => e.SetTitle("")));
            test.Assert.AreEqual(Const("Unknown"), Expr(_employee, e => e.Title)); ;
            test.Execute();
        }
        
        [TestMethod("k. MonthlySalary ignores assignment of -1M"), TestCategory("Exercise 2A")]
        public void MonthlySalaryIgnoresAssignmentOfMinusOne()
        {
            Employee employee = new Employee("abc");

            employee.MonthlySalary = -1;

            Assert.AreEqual(0M, employee.MonthlySalary);

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Employee> _employee = test.CreateVariable<Employee>(nameof(_employee));
            test.Arrange(_employee, Expr(() => new Employee("abc")));
            test.Act(Expr(_employee, e => e.SetMonthlySalary(-1M)));
            test.Assert.AreEqual(Const(0M), Expr(_employee, e => e.MonthlySalary)); ;
            test.Execute();
        }

        [TestMethod("l. Seniority ignores assignment of 0"), TestCategory("Exercise 2A")]
        public void SeniorityIgnoresAssignmentOfZero()
        {
            Employee employee = new Employee("abc");

            employee.Seniority = 0;

            Assert.AreEqual(1, employee.Seniority);

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Employee> _employee = test.CreateVariable<Employee>(nameof(_employee));
            test.Arrange(_employee, Expr(() => new Employee("abc")));
            test.Act(Expr(_employee, e => e.SetSeniority(0)));
            test.Assert.AreEqual(Const(1), Expr(_employee, e => e.Seniority)); ;
            test.Execute();
        }

        [TestMethod("m. Seniority ignores assigment of 11"), TestCategory("Exercise 2A")]
        public void SeniorityIgnoresAssignmentOfEleven()
        {
            Employee employee = new Employee("abc");

            employee.Seniority = 11;

            Assert.AreEqual(1, employee.Seniority);

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Employee> _employee = test.CreateVariable<Employee>(nameof(_employee));
            test.Arrange(_employee, Expr(() => new Employee("abc")));
            test.Act(Expr(_employee, e => e.SetSeniority(11)));
            test.Assert.AreEqual(Const(1), Expr(_employee, e => e.Seniority)); ;
            test.Execute();
        }
        #endregion

        #region Exercise 2B
        [TestMethod("a. Employee.CalculateYearlySalary() returns expected output for seniority 1"), TestCategory("Exercise 2B")]
        public void EmployeeCalculateYearlySalaryAddsTenProcentForSeniorityLevelOne()
        {
            Employee employee = new Employee("abc") 
            { 
                MonthlySalary = 34000M, 
                Seniority = 1 
            };
            Assert.AreEqual(40800M, employee.CalculateYearlySalary());

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Employee> _employee = test.CreateVariable<Employee>(nameof(_employee));
            test.Arrange(_employee, Expr(() => new Employee("abc") { MonthlySalary = 34000M, Seniority = 1 }));
            test.Assert.AreEqual(Const(40800M), Expr(_employee, e => e.CalculateYearlySalary())); ;
            test.Execute();
        }

        [TestMethod("b. Employee.CalculateYearlySalary() returns expected output for seniority 2"), TestCategory("Exercise 2B")]
        public void EmployeeCalculateYearlySalaryAddsTenProcentForSeniorityLevelTwo()
        {
            Employee employee = new Employee("abc")
            {
                MonthlySalary = 15340,
                Seniority = 2
            };
            Assert.AreEqual(18408M, employee.CalculateYearlySalary());

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Employee> _employee = test.CreateVariable<Employee>(nameof(_employee));
            test.Arrange(_employee, Expr(() => new Employee("abc") { MonthlySalary = 15340, Seniority = 2 }));
            test.Assert.AreEqual(Const(18408M), Expr(_employee, e => e.CalculateYearlySalary())); ;
            test.Execute();
        }

        [TestMethod("c. Employee.CalculateYearlySalary() returns expected output for seniority 3"), TestCategory("Exercise 2B")]
        public void EmployeeCalculateYearlySalaryAddsTenProcentForSeniorityLevelThree()
        {
            Employee employee = new Employee("abc")
            {
                MonthlySalary = 20000,
                Seniority = 3
            };
            Assert.AreEqual(24000M, employee.CalculateYearlySalary());

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Employee> _employee = test.CreateVariable<Employee>(nameof(_employee));
            test.Arrange(_employee, Expr(() => new Employee("abc") { MonthlySalary = 20000, Seniority = 3 }));
            test.Assert.AreEqual(Const(24000M), Expr(_employee, e => e.CalculateYearlySalary())); ;
            test.Execute();
        }

        [TestMethod("d. Employee.CalculateYearlySalary() returns expected output for seniority 6"), TestCategory("Exercise 2B")]
        public void EmployeeCalculateYearlySalaryAddsTenProcentForSeniorityLevelSix()
        {
            Employee employee = new Employee("abc")
            {
                MonthlySalary = 20000,
                Seniority = 6
            };
            Assert.AreEqual(96000M, employee.CalculateYearlySalary());

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Employee> _employee = test.CreateVariable<Employee>(nameof(_employee));
            test.Arrange(_employee, Expr(() => new Employee("abc") { MonthlySalary = 20000, Seniority = 6 }));
            test.Assert.AreEqual(Const(96000M), Expr(_employee, e => e.CalculateYearlySalary())); ;
            test.Execute();
        }

        [TestMethod("e. Employee.CalculateYearlySalary() returns expected output for seniority 7"), TestCategory("Exercise 2B")]
        public void EmployeeCalculateYearlySalaryAddsTenProcentForSeniorityLevelSeven()
        {
            Employee employee = new Employee("abc")
            {
                MonthlySalary = 12300,
                Seniority = 7
            };
            Assert.AreEqual(103320M, employee.CalculateYearlySalary());

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Employee> _employee = test.CreateVariable<Employee>(nameof(_employee));
            test.Arrange(_employee, Expr(() => new Employee("abc") { MonthlySalary = 12300, Seniority = 7 }));
            test.Assert.AreEqual(Const(103320M), Expr(_employee, e => e.CalculateYearlySalary()));
            test.Execute();
        }

        [TestMethod("f. Employee.CalculateYearlySalary() returns expected output for seniority 10"), TestCategory("Exercise 2B")]
        public void EmployeeCalculateYearlySalaryAddsTenProcentForSeniorityLevelTen()
        {
            Employee employee = new Employee("abc")
            {
                MonthlySalary = 35250,
                Seniority = 10
            };
            Assert.AreEqual(296100M, employee.CalculateYearlySalary());

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Employee> _employee = test.CreateVariable<Employee>(nameof(_employee));
            test.Arrange(_employee, Expr(() => new Employee("abc") { MonthlySalary = 35250, Seniority = 10 }));
            test.Assert.AreEqual(Const(296100M), Expr(_employee, e => e.CalculateYearlySalary()));
            test.Execute();
        }
        #endregion

        #region Exercise 2C
        [TestMethod("a. Manager is subclass of Employee"), TestCategory("Exercise 2C")]
        public void ManagerIsSubclassOfEmployee()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertClass<Manager>(new TypeBaseClassVerifier(typeof(Employee)));
            test.Execute();
        }

        [TestMethod("b. Bonus is public decimal property"), TestCategory("Exercise 2C")]
        public void BonusIsPublicDecimalProperty() 
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicProperty<Manager, decimal>(e => e.Bonus);
            test.Execute();
        }

        [TestMethod("c. Manager.Bonus is initialized as 0"), TestCategory("Exercise 2A")]
        public void BonusIsInitializedAs0()
        {
            Manager manager = new Manager("abc");
            Assert.AreEqual(0M, manager.Bonus);

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Manager> _manager = test.CreateVariable<Manager>(nameof(_manager));
            test.Arrange(_manager, Expr(() => new Manager("abc")));
            test.Assert.AreEqual(Const(0M), Expr(_manager, e => e.Bonus));
            test.Execute();
        }

        [TestMethod("d. Bonus ignores assignment of -1M"), TestCategory("Exercise 2C")]
        public void BonusIgnoresAssignmentOfMinusOne()
        {
            Manager manager = new Manager("abc");

            manager.Bonus = -1;

            Assert.AreEqual(0M, manager.Bonus);

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Manager> _manager = test.CreateVariable<Manager>(nameof(_manager));
            test.Arrange(_manager, Expr(() => new Manager("abc")));
            test.Act(Expr(_manager, e => e.SetBonus(-1M)));
            test.Assert.AreEqual(Const(0M), Expr(_manager, e => e.Bonus)); ;
            test.Execute();
        }
        #endregion

        #region Exercise 2D
        [TestMethod("a. Manager.CalculateYearlySalary() returns expected output for seniority 1"), TestCategory("Exercise 2D")]
        public void ManagerCalculateYearlySalaryAddsTenProcentForSeniorityLevelOne()
        {
            Manager manager = new Manager("abc")
            {
                MonthlySalary = 35250,
                Bonus = 0,
                Seniority = 1
            };
            Assert.AreEqual(42300M, manager.CalculateYearlySalary());

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Manager> _manager = test.CreateVariable<Manager>(nameof(_manager));
            test.Arrange(_manager, Expr(() => new Manager("abc") { MonthlySalary = 35250, Bonus = 0, Seniority = 1 }));
            test.Assert.AreEqual(Const(42300M), Expr(_manager, e => e.CalculateYearlySalary())); ;
            test.Execute();
        }

        [TestMethod("b. Manager.CalculateYearlySalary() returns expected output for seniority 2"), TestCategory("Exercise 2D")]
        public void ManagerCalculateYearlySalaryAddsTenProcentForSeniorityLevelTwo()
        {
            Manager manager = new Manager("abc")
            {
                MonthlySalary = 15340,
                Bonus = 500,
                Seniority = 2
            };
            Assert.AreEqual(18908M, manager.CalculateYearlySalary());

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Manager> _manager = test.CreateVariable<Manager>(nameof(_manager));
            test.Arrange(_manager, Expr(() => new Manager("abc") { MonthlySalary = 15340, Bonus = 500, Seniority = 2 }));
            test.Assert.AreEqual(Const(18908M), Expr(_manager, e => e.CalculateYearlySalary())); ;
            test.Execute();
        }

        [TestMethod("c. Manager.CalculateYearlySalary() returns expected output for seniority 3"), TestCategory("Exercise 2D")]
        public void ManagerCalculateYearlySalaryAddsTenProcentForSeniorityLevelThree()
        {
            Manager manager = new Manager("abc")
            {
                MonthlySalary = 26500,
                Bonus = 1000,
                Seniority = 3
            };
            Assert.AreEqual(32800M, manager.CalculateYearlySalary());

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Manager> _manager = test.CreateVariable<Manager>(nameof(_manager));
            test.Arrange(_manager, Expr(() => new Manager("abc") { MonthlySalary = 26500, Bonus = 1000, Seniority = 3 }));
            test.Assert.AreEqual(Const(32800M), Expr(_manager, e => e.CalculateYearlySalary())); ;
            test.Execute();
        }

        [TestMethod("d. Manager.CalculateYearlySalary() returns expected output for seniority 6"), TestCategory("Exercise 2D")]
        public void ManagerCalculateYearlySalaryAddsTenProcentForSeniorityLevelSix()
        {
            Manager manager = new Manager("abc")
            {
                MonthlySalary = 20000,
                Bonus = 300,
                Seniority = 6
            };
            Assert.AreEqual(96300M, manager.CalculateYearlySalary());


            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Manager> _manager = test.CreateVariable<Manager>(nameof(_manager));
            test.Arrange(_manager, Expr(() => new Manager("abc") { MonthlySalary = 20000, Bonus = 300, Seniority = 6 }));
            test.Assert.AreEqual(Const(96300M), Expr(_manager, e => e.CalculateYearlySalary())); ;
            test.Execute();
        }

        [TestMethod("e. Manager.CalculateYearlySalary() returns expected output for seniority 7"), TestCategory("Exercise 2D")]
        public void ManagerCalculateYearlySalaryAddsTenProcentForSeniorityLevelSeven()
        {
            Manager manager = new Manager("abc")
            {
                MonthlySalary = 12300,
                Bonus = 3000,
                Seniority = 7
            };
            Assert.AreEqual(106320M, manager.CalculateYearlySalary());

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Manager> _manager = test.CreateVariable<Manager>(nameof(_manager));
            test.Arrange(_manager, Expr(() => new Manager("abc") { MonthlySalary = 12300, Bonus = 3000, Seniority = 7 }));
            test.Assert.AreEqual(Const(106320M), Expr(_manager, e => e.CalculateYearlySalary())); ;
            test.Execute();
        }

        [TestMethod("f. Employee.CalculateYearlySalary() returns expected output for seniority 10"), TestCategory("Exercise 2D")]
        public void ManagerCalculateYearlySalaryAddsTenProcentForSeniorityLevelTen()
        {
            Manager manager = new Manager("abc")
            {
                MonthlySalary = 35250,
                Bonus = 34000,
                Seniority = 10
            };
            Assert.AreEqual(330100M, manager.CalculateYearlySalary());

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Manager> _manager = test.CreateVariable<Manager>(nameof(_manager));
            test.Arrange(_manager, Expr(() => new Manager("abc") { MonthlySalary = 35250, Bonus = 34000, Seniority = 10 }));
            test.Assert.AreEqual(Const(330100M), Expr(_manager, e => e.CalculateYearlySalary())); ;
            test.Execute();
        }
        #endregion

        #region Exercise 2E
        [TestMethod("a. Company.Employees is a public read-only List<Employee> property"), TestCategory("Exercise 2E")]
        public void EmployeesIsPublicListEmployeeProperty() {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicReadonlyProperty<Company, List<Employee>>(c => c.Employees);
            test.Execute();
        }

        [TestMethod("b. Company.Hire(Employee e) adds employee to Employees"), TestCategory("Exercise 2E")]
        public void CompanyHireAddsEmployeeToEmployees()
        {
            Company company = new Company();
            Employee employee = new Employee("Ellen Stevens");

            company.Hire(employee);

            Assert.AreEqual(1, company.Employees.Count);

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Employee> _employee = test.CreateVariable<Employee>(nameof(_employee));
            TestVariable<Company> _company = test.CreateVariable<Company>(nameof(_company));
            test.Arrange(_employee, Expr(() => new Employee("Ellen Stevens")));
            test.Arrange(_company, Expr(() => new Company()));
            test.Act(Expr(_company, _employee, (c, e) => c.Hire(e)));
            test.Assert.AreEqual(Const(1), Expr(_company, c => c.Employees.Count));
            test.Execute();
        }

        [TestMethod("c. Company.Fire(Employee e) removes employee from Employees"), TestCategory("Exercise 2E")]
        public void CompanyFireAddsEmployeeToEmployees()
        {
            Company company = new Company();
            Employee employee = new Employee("Ellen Stevens");

            company.Hire(employee);
            company.Fire(employee);

            Assert.AreEqual(0, company.Employees.Count);

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Employee> _employee = test.CreateVariable<Employee>(nameof(_employee));
            TestVariable<Company> _company = test.CreateVariable<Company>(nameof(_company));
            test.Arrange(_employee, Expr(() => new Employee("Ellen Stevens")));
            test.Arrange(_company, Expr(() => new Company()));
            test.Act(Expr(_company, _employee, (c, e) => c.Hire(e)));
            test.Act(Expr(_company, _employee, (c, e) => c.Fire(e)));
            test.Assert.AreEqual(Const(0), Expr(_company, c => c.Employees.Count));
            test.Execute();
        }

        [TestMethod("d. Company.Hire(Employee e) adds manager to Employees"), TestCategory("Exercise 2E")]
        public void CompanyHireAddsManagerToEmployees()
        {
            Company company = new Company();
            Employee employee = new Employee("Katja Holmes");

            company.Hire(employee);

            Assert.AreEqual(1, company.Employees.Count);

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Manager> _manager = test.CreateVariable<Manager>(nameof(_manager));
            TestVariable<Company> _company = test.CreateVariable<Company>(nameof(_company));
            test.Arrange(_manager, Expr(() => new Manager("Katja Holmes")));
            test.Arrange(_company, Expr(() => new Company()));
            test.Act(Expr(_company, _manager, (c, m) => c.Hire(m)));
            test.Assert.AreEqual(Const(1), Expr(_company, c => c.Employees.Count));
            test.Execute();
        }

        [TestMethod("e. Company.Fire(Employee e) removes manager to Employees"), TestCategory("Exercise 2E")]
        public void CompanyFireAddsManagerToEmployees()
        {
            Company company = new Company();
            Manager manager = new Manager("Katja Holmes");

            company.Hire(manager);
            company.Fire(manager);

            Assert.AreEqual(0, company.Employees.Count);

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Manager> _manager = test.CreateVariable<Manager>(nameof(_manager));
            TestVariable<Company> _company = test.CreateVariable<Company>(nameof(_company));
            test.Arrange(_manager, Expr(() => new Manager("Katja Holmes")));
            test.Arrange(_company, Expr(() => new Company()));
            test.Act(Expr(_company, _manager, (c, m) => c.Hire(m)));
            test.Act(Expr(_company, _manager, (c, m) => c.Fire(m)));
            test.Assert.AreEqual(Const(0), Expr(_company, c => c.Employees.Count));
            test.Execute();
        }
        #endregion

        #region Exercise 2F
        [TestMethod("a. Company.CalculateYearlySalaryCosts() returns 0 for company without employees"), TestCategory("Exercise 2F")]
        public void CompanyCalculateYearlySalaryCostsReturnsZeroForCompanyWithoutEmployees()
        {
            Company company = new Company();
            Assert.AreEqual(0M, company.CalculateYearlySalaryCosts());

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Company> _company = test.CreateVariable<Company>(nameof(_company));
            test.Arrange(_company, Expr(() => new Company()));
            test.Assert.AreEqual(Const(0M), Expr(_company, c => c.CalculateYearlySalaryCosts()));
            test.Execute();
        }
        
        [TestMethod("b. Company.CalculateYearlySalaryCosts() returns expected output for company with 1 Employee"), TestCategory("Exercise 2F")]
        public void CompanyCalculateYearlySalaryCostsReturnsExpectedOutputForCompanyWithOneEmployee()
        {
            Company company = new Company();
            Employee employee = new Employee("Allan Walker") 
            { 
                MonthlySalary = 30000M, 
                Seniority = 4 
            };

            company.Hire(employee);

            Assert.AreEqual(144000M, company.CalculateYearlySalaryCosts());

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Company> _company = test.CreateVariable<Company>(nameof(_company));
            TestVariable<Employee> _employee = test.CreateVariable<Employee>(nameof(_employee));
            test.Arrange(_company, Expr(() => new Company()));
            test.Arrange(_employee, Expr(() => new Employee("Allan Walker") { MonthlySalary = 30000M, Seniority = 4 }));
            test.Act(Expr(_company, _employee, (c, e) => c.Hire(e)));
            test.Assert.AreEqual(Const(144000M), Expr(_company, c => c.CalculateYearlySalaryCosts())); ;
            test.Execute();
        }

    [TestMethod("c. Company.CalculateYearlySalaryCosts() returns expected output for company with 2 Employees"), TestCategory("Exercise 2F")]
        public void CompanyCalculateYearlySalaryCostsReturnsExpectedOutputForCompanyWithTwoEmployee() 
        {
            Company company = new Company();
            Employee employee1 = new Employee("Allan Walker")
            {
                MonthlySalary = 30000M,
                Seniority = 4
            };
            Employee employee2 = new Employee("Amy Walker")
            {
                MonthlySalary = 30000M,
                Seniority = 7
            };

            company.Hire(employee1);
            company.Hire(employee2);

            Assert.AreEqual(396000M, company.CalculateYearlySalaryCosts());

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Company> _company = test.CreateVariable<Company>(nameof(_company));
            TestVariable<Employee> _employee1 = test.CreateVariable<Employee>(nameof(_employee1));
            TestVariable<Employee> _employee2 = test.CreateVariable<Employee>(nameof(_employee1));
            test.Arrange(_company, Expr(() => new Company()));
            test.Arrange(_employee1, Expr(() => new Employee("Allan Walker") { MonthlySalary = 30000M, Seniority = 4 }));
            test.Arrange(_employee2, Expr(() => new Employee("Amy Walker") { MonthlySalary = 30000M, Seniority = 7 }));
            test.Act(Expr(_company, _employee1, (c, e) => c.Hire(e)));
            test.Act(Expr(_company, _employee2, (c, e) => c.Hire(e)));
            test.Assert.AreEqual(Const(396000M), Expr(_company, c => c.CalculateYearlySalaryCosts()));
            test.Execute();
        }
        #endregion
    }
}
