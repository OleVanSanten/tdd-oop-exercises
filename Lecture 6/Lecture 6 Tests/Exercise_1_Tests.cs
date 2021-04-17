using Lecture_6_Solutions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TestTools.Expressions;
using TestTools.Structure;
using TestTools.MSTest;
using static TestTools.Expressions.TestExpression;
using static Lecture_6_Tests.TestHelper;
using static TestTools.Helpers.StructureHelper;

namespace Lecture_6_Tests
{
    [TestClass]
    public class Exercise_1_Tests
    {
        #region Exercise 1A
        [TestMethod("a. Temperature.Celcius is public double property"), TestCategory("Exercise 1A")]
        public void TemperatureCelciusIsPublicDoubleProperty()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicProperty<Temperature, double>(t => t.Celcius);
            test.Execute();
        }

        [TestMethod("b. Temperature.Fahrenheit is public double property"), TestCategory("Exercise 1A")]
        public void TemperatureFahrenheitIsPublicDoubleProperty()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicProperty<Temperature, double>(t => t.Fahrenheit);
            test.Execute();
        }

        [TestMethod("c. Temperature.Kelvin is public double property"), TestCategory("Exercise 1A")]
        public void TemperatureKelvinIsPublicDoubleProperty()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicProperty<Temperature, double>(t => t.Kelvin);
            test.Execute();
        }

        [TestMethod("d. Temperature.Celcius = -276.0 throws ArgumentException"), TestCategory("Exercise 1A")]
        public void TemperatureCelciusAssignmentOfMinus276ThrowsArgumentException()
        {
            Temperature temperature = new Temperature();
            Assert.ThrowsException<ArgumentException>(() => temperature.Celcius = -276.0);

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Temperature> _temperature = test.CreateVariable<Temperature>();
            test.Arrange(_temperature, Expr(() => new Temperature()));
            test.Assert.ThrowsExceptionOn<ArgumentException>(Expr(_temperature, t => t.SetCelcius(-276.0)));
            test.Execute();
        }

        [TestMethod("e. Temperature.Fahrenheit = -460.0 throws ArgumentException"), TestCategory("Exercise 1A")]
        public void TemperatureFahrenheitAssignmentOfMinus460ThrowsArgumentException()
        {
            Temperature temperature = new Temperature();
            Assert.ThrowsException<ArgumentException>(() => temperature.Fahrenheit = -476.0);

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Temperature> _temperature = test.CreateVariable<Temperature>();
            test.Arrange(_temperature, Expr(() => new Temperature()));
            test.Assert.ThrowsExceptionOn<ArgumentException>(Expr(_temperature, t => t.SetFahrenheit(-476.0)));
            test.Execute();
        }

        [TestMethod("f. Temperature.Kelvin = -1 throws ArgumentException"), TestCategory("Exercise 1A")]
        public void TemperatureKelvinAssignmentOfMinus1ThrowsArgumentException()
        {
            Temperature temperature = new Temperature();
            Assert.ThrowsException<ArgumentException>(() => temperature.Kelvin = -1.0);

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Temperature> _temperature = test.CreateVariable<Temperature>();
            test.Arrange(_temperature, Expr(() => new Temperature()));
            test.Assert.ThrowsExceptionOn<ArgumentException>(Expr(_temperature, t => t.SetKelvin(-1.0)));
            test.Execute();
        }

        [TestMethod("g. Temperature.Kelvin equals 0 after Temperature.Celcius = -273.15"), TestCategory("Exercise 1A")]
        public void TemperatureKelvinEquals0AfterTemperatureCelciusAssignmentOfMinus275()
        {
            Temperature temperature = new Temperature();

            temperature.Celcius = -273.15;

            Assert.AreEqual(0.0, temperature.Kelvin, 0.001);

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Temperature> _temperature = test.CreateVariable<Temperature>();
            test.Arrange(_temperature, Expr(() => new Temperature()));
            test.Act(Expr(_temperature, t => t.SetCelcius(-273.15)));
            test.Assert.AreEqual(Const(0.0), Expr(_temperature, t => t.Kelvin), 0.001);
            test.Execute();
        }

        [TestMethod("h. Temperature.Kelvin equals 0 after Temperature.Fahrenheit = -459.67"), TestCategory("Exercise 1A")]
        public void TemperatureKelvinEquals0AfterTemperatureFahrenheitAssignmentOfMinus459()
        {
            Temperature temperature = new Temperature();

            temperature.Fahrenheit = -459.67;

            Assert.AreEqual(0.0, temperature.Kelvin, 0.001);

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Temperature> _temperature = test.CreateVariable<Temperature>();
            test.Arrange(_temperature, Expr(() => new Temperature()));
            test.Act(Expr(_temperature, t => t.SetFahrenheit(-459.67)));
            test.Assert.AreEqual(Const(0.0), Expr(_temperature, t => t.Kelvin),  0.001);
            test.Execute();
        }
        #endregion

        #region Exercise 1B
        [TestMethod("a. Temperature implements IComparable"), TestCategory("Exercise 1B")]
        public void TemperatureImplementsIComparable()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertClass<Temperature>(new TypeIsSubclassOfVerifier(typeof(IComparable)));
            test.Execute();
        }

        [TestMethod("b. Temperature.CompareTo sorts null first"), TestCategory("Exercise 1B")]
        public void TemperatureCompareToSortsNullFirst()
        {
            Temperature temperature = new Temperature();
            Assert.IsTrue(temperature.CompareTo(null) < 0);

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Temperature> _temperature = test.CreateVariable<Temperature>();
            test.Arrange(_temperature, Expr(() => new Temperature()));
            test.Assert.IsTrue(Expr(_temperature, t => t.CompareTo(null) < 0));
            test.Execute();
        }

        [TestMethod("c. Temperature.CompareTo sorts higher temperature first"), TestCategory("Exercise 1B")]
        public void TemperatureCompareToSortsHigherTemperatureFirst()
        {
            Temperature temperature1 = new Temperature() { Kelvin = 0 };
            Temperature temperature2 = new Temperature() { Kelvin = 1 };

            Assert.IsTrue(temperature1.CompareTo(temperature2) > 0);

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Temperature> _temperature1 = test.CreateVariable<Temperature>();
            TestVariable<Temperature> _temperature2 = test.CreateVariable<Temperature>();
            test.Arrange(_temperature1, Expr(() => new Temperature() { Kelvin = 0 }));
            test.Arrange(_temperature2, Expr(() => new Temperature() { Kelvin = 1 }));
            test.Assert.IsTrue(Expr(_temperature1, _temperature2, (t1, t2) => t1.CompareTo(t2) > 0));
            test.Execute();
        }

        [TestMethod("d. Temperature.CompareTo does not sort equal temperatures"), TestCategory("Exercise 1B")]
        public void Test1B4()
        {
            Temperature temperature1 = new Temperature() { Kelvin = 0 };
            Temperature temperature2 = new Temperature() { Kelvin = 0 };

            Assert.IsTrue(temperature1.CompareTo(temperature2) == 0);

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Temperature> _temperature1 = test.CreateVariable<Temperature>();
            TestVariable<Temperature> _temperature2 = test.CreateVariable<Temperature>();
            test.Arrange(_temperature1, Expr(() => new Temperature() { Kelvin = 0 }));
            test.Arrange(_temperature2, Expr(() => new Temperature() { Kelvin = 0 }));
            test.Assert.IsTrue(Expr(_temperature1, _temperature2, (t1, t2) => t1.CompareTo(t2) == 0));
            test.Execute();
        }
        #endregion
    }
}
