using Lecture_6_Solutions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using TestTools.Expressions;
using TestTools.MSTest;
using TestTools.Structure;
using static Lecture_6_Tests.TestHelper;
using static TestTools.Expressions.TestExpression;

namespace Lecture_6_Tests
{
    [TestClass]
    public class Exercise_5_Tests 
    {
        #region Exercise 5A
        [TestMethod("a. CarListSorter.Comparer is a publuc IComparer<Car> property"), TestCategory("Exercise 5A")]
        public void CarListSorterComparerIsPublicProperty()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicProperty<CarSorter, IComparer<Car>>(c => c.Comparer);
            test.Execute();
        }

        [TestMethod("b. CarSorter.Comparer initializes to null"), TestCategory("Exercise 5A")]
        public void CarSorterComparerInitializesToNull()
        {
            CarSorter sorter = new CarSorter();
            Assert.IsNull(sorter.Comparer);

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<CarSorter> _sorter = test.CreateVariable<CarSorter>();
            test.Arrange(_sorter, Expr(() => new CarSorter()));
            test.Assert.IsNull(Expr(_sorter, s => s.Comparer));
            test.Execute();
        }
        #endregion

        #region Exercise 5B
        [TestMethod("a. CarListSorter.Sort takes an array of cars"), TestCategory("Exercise 5B")]
        public void DieConstructorTakesIRandomAndInt()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicMethod<CarSorter, Car[]>((c1, c2) => c1.Sort(c2));
            test.Execute();
        }

        [TestMethod("b. CarListSorter.Sort does not sort array if Comparer = null"), TestCategory("Exercise 5B")]
        public void CarListSorterSortDoesNotSort()
        {
            CarSorter sorter = new CarSorter();
            Car[] cars = new Car[]
            {
                new Car() { ID = 0, Make = "Audi", Model = "S3", Price = 20M},
                new Car() { ID = 1, Make = "Audi", Model = "S4", Price = 30M },
                new Car() { ID = 2, Make = "Suzuki", Model = "Splash", Price = 10M }
            };

            sorter.Sort(cars);

            Assert.IsTrue(cars.Select(c => c.ID).SequenceEqual(new[] { 0, 1, 2 }));

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<CarSorter> _sorter = test.CreateVariable<CarSorter>();
            TestVariable<Car[]> _cars = test.CreateVariable<Car[]>();
            test.Arrange(_sorter, Expr(() => new CarSorter()));
            test.Arrange(_cars, Expr(() => new Car[]
            {
                new Car() { ID = 0, Make = "Audi", Model = "S3", Price = 20M},
                new Car() { ID = 1, Make = "Audi", Model = "S4", Price = 30M },
                new Car() { ID = 2, Make = "Suzuki", Model = "Splash", Price = 10M }
            }));
            test.Act(Expr(_sorter, _cars, (s, c) => s.Sort(c)));
            test.Assert.IsTrue(Expr(_cars, (c1) => c1.Select(c => c.ID).SequenceEqual(new[] { 0, 1, 2 })));
            test.Execute();
        }

        [TestMethod("c. CarListSorter.Sort sorts according to price if Comparer = new CarPriceComparer()"), TestCategory("Exercise 5B")]
        public void CarListSorterSorts()
        {
            CarSorter sorter = new CarSorter() 
            {
                Comparer = new CarPriceComparer()
            };
            Car[] cars = new Car[]
            {
                new Car() { ID = 0, Make = "Audi", Model = "S3", Price = 20M },
                new Car() { ID = 1, Make = "Audi", Model = "S4", Price = 30M },
                new Car() { ID = 2, Make = "Suzuki", Model = "Splash", Price = 10M }
            };

            sorter.Sort(cars);

            Assert.IsTrue(cars.Select(c => c.ID).SequenceEqual(new[] { 2, 0, 1 }));

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<CarSorter> _sorter = test.CreateVariable<CarSorter>();
            TestVariable<Car[]> _carsBefore = test.CreateVariable<Car[]>();
            test.Arrange(_sorter, Expr(() => new CarSorter() { Comparer = new CarPriceComparer() }));
            test.Arrange(_carsBefore, Expr(() => new Car[]
            {
                new Car() { ID = 0, Make = "Audi", Model = "S3", Price = 20M},
                new Car() { ID = 1, Make = "Audi", Model = "S4", Price = 30M },
                new Car() { ID = 2, Make = "Suzuki", Model = "Splash", Price = 10M }
            }));
            test.Act(Expr(_sorter, _carsBefore, (s, c) => s.Sort(c)));
            test.Assert.IsTrue(Expr(_carsBefore, c1 => c1.Select(c => c.ID).SequenceEqual(new[] { 2, 0, 1 })));
            test.Execute();
        }
        #endregion
    }
}
