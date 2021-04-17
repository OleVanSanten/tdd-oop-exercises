using Lecture_2_Solutions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestTools.Expressions;
using TestTools.MSTest;
using TestTools.Structure;
using static Lecture_2_Tests.TestHelper;
using static TestTools.Expressions.TestExpression;

namespace Lecture_2_Tests
{
    [TestClass]
    public class Exercise_2_Tests
    {
        #region Exercise 2A
        [TestMethod("Number.Value is public readonly int property"), TestCategory("Exercise 2A")]
        public void ValueIsPublicReadonlyIntProperty()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicReadonlyProperty<Number, int>(n => n.Value);
            test.Execute();
        }
        #endregion

        #region Exercise 2B
        [TestMethod("a. Number constructor takes int as argument"), TestCategory("Exercise 2B")]
        public void NumberConstructorTakesIntAsArgument() {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicConstructor<int, Number>(i => new Number(i));
            test.Execute();
        }

        [TestMethod("b. Number constructor with int as argument sets value property"), TestCategory("Exercise 2B")]
        public void NumberConstructorWithIntAsArgumentSetsValueProperty()
        {
            Number number = new Number(2);
            Assert.AreEqual(number.Value, 2);

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Number> _number = test.CreateVariable<Number>(nameof(_number));
            test.Arrange(_number, Expr(() => new Number(2)));
            test.Assert.AreEqual(Expr(_number, n => n.Value), Const(2));
            test.Execute();
        }
        #endregion

        #region Exercise 2C
        [TestMethod("a. Number.Add takes Number as argument and returns nothing"), TestCategory("Exercise 2C")]
        public void AddTakesNumberAsArgumentsAndReturnsNothing() {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicMethod<Number, Number>((n1, n2) => n1.Add(n2));
            test.Execute();
        }

        [TestMethod("b. Number.Add performs 1 + 2 = 3"), TestCategory("Exercise 2C")]
        public void AddProducesExpectedResult() 
        {
            Number number1 = new Number(1);
            Number number2 = new Number(2);

            number1.Add(number2);

            Assert.AreEqual(3, number1.Value);
            
            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Number> _number1 = test.CreateVariable<Number>(nameof(_number1));
            TestVariable<Number> _number2 = test.CreateVariable<Number>(nameof(_number2));
            test.Arrange(_number1, Expr(() => new Number(1)));
            test.Arrange(_number2, Expr(() => new Number(2)));
            test.Act(Expr(_number1, _number2, (n1, n2) => n1.Add(n2)));
            test.Assert.AreEqual(Const(3), Expr(_number1, n => n.Value));
            test.Execute();
        }

        [TestMethod("c. Number.Subtract takes Number as argument and returns nothing"), TestCategory("Exercise 2C")]
        public void SubtractTakesNumberAsArgumentAndReturnsNothing()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicMethod<Number, Number>((n1, n2) => n1.Subtract(n2));
            test.Execute();
        }

        [TestMethod("d. Number.Subtract performs 8 - 3 = 5"), TestCategory("Exercise 2C")]
        public void SubtractProducesExpectedResult() 
        {
            Number number1 = new Number(8);
            Number number2 = new Number(3);

            number1.Subtract(number2);

            Assert.AreEqual(5, number1.Value);
            
            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Number> _number1 = test.CreateVariable<Number>(nameof(_number1));
            TestVariable<Number> _number2 = test.CreateVariable<Number>(nameof(_number2));
            test.Arrange(_number1, Expr(() => new Number(8)));
            test.Arrange(_number2, Expr(() => new Number(3)));
            test.Act(Expr(_number1, _number2, (n1, n2) => n1.Subtract(n2)));
            test.Assert.AreEqual(Const(5), Expr(_number1, n => n.Value));
            test.Execute();
        }

        [TestMethod("e. Number.Multiply takes Number as argument and returns nothing"), TestCategory("Exercise 2C")]
        public void MultiplyTakesNumberAsArgumentAndReturnsNothing()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicMethod<Number, Number>((n1, n2) => n1.Multiply(n2));
            test.Execute();
        }

        [TestMethod("f. Number.Multiply performs 2 * 3 = 6"), TestCategory("Exercise 2C")]
        public void MultiplyProducesExpectedResult()
        {
            Number number1 = new Number(2);
            Number number2 = new Number(3);

            number1.Multiply(number2);

            Assert.AreEqual(6, number1.Value);

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Number> _number1 = test.CreateVariable<Number>(nameof(_number1));
            TestVariable<Number> _number2 = test.CreateVariable<Number>(nameof(_number2));
            test.Arrange(_number1, Expr(() => new Number(2)));
            test.Arrange(_number2, Expr(() => new Number(3)));
            test.Act(Expr(_number1, _number2, (n1, n2) => n1.Multiply(n2)));
            test.Assert.AreEqual(Const(6), Expr(_number1, n => n.Value));
            test.Execute();
        }
        #endregion
    }
}
