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
    public class Exercise_3_Tests
    {
        #region Exercise 3A
        [TestMethod("a. ImmutableNumber.Value is public readonly int property"), TestCategory("Exercise 3A")]
        public void ValueIsPublicReadonlyProperty()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicReadonlyProperty<ImmutableNumber, int>(n => n.Value);
            test.Execute();
        }
        #endregion

        #region Exercise 3B
        [TestMethod("a. ImmutableNumber.Number constructor takes int as argument"), TestCategory("Exercise 3B")]
        public void ImmutableNumberConstructorTakesIntAsArgument() 
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicConstructor<int, ImmutableNumber>(i => new ImmutableNumber(i));
            test.Execute();
        }

        [TestMethod("b. ImmutableNumber constructor with int as argument sets value property"), TestCategory("Exercise 3B")]
        public void ImmutableNumberConstructorWithIntAsArgumentSetsValueProperty()
        {
            ImmutableNumber number = new ImmutableNumber(2);
            Assert.AreEqual(2, number.Value);

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<ImmutableNumber> _number = test.CreateVariable<ImmutableNumber>(nameof(_number));
            test.Arrange(_number, Expr(() => new ImmutableNumber(2)));
            test.Assert.AreEqual(Const(2), Expr(_number, n => n.Value));
            test.Execute();
        }
        #endregion

        #region Exercise 2C
        [TestMethod("a. ImmutableNumber.Add takes ImmutableNumber as argument and returns ImmutableNumber"), TestCategory("Exercise 3C")]
        public void AddTakesImmutableNumberAsArgumentAndReturnsImmutableNumber()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicMethod<ImmutableNumber, ImmutableNumber, ImmutableNumber>((n1, n2) => n1.Add(n2));
            test.Execute();
        }

        [TestMethod("b. ImmutableNumber.Add performs 1 + 2 = 3"), TestCategory("Exercise 3C")]
        public void AddProducesExpectedResult() 
        {
            ImmutableNumber number1 = new ImmutableNumber(1);
            ImmutableNumber number2 = new ImmutableNumber(2);

            ImmutableNumber number3 = number1.Add(number2);

            Assert.AreEqual(3, number3.Value);
            
            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<ImmutableNumber> _number1 = test.CreateVariable<ImmutableNumber>(nameof(_number1));
            TestVariable<ImmutableNumber> _number2 = test.CreateVariable<ImmutableNumber>(nameof(_number2));
            TestVariable<ImmutableNumber> _number3 = test.CreateVariable<ImmutableNumber>(nameof(_number3));
            test.Arrange(_number1, Expr(() => new ImmutableNumber(1)));
            test.Arrange(_number2, Expr(() => new ImmutableNumber(2)));
            test.Arrange(_number3, Expr(_number1, _number2, (n1, n2) => n1.Add(n2)));
            test.Assert.AreEqual(Const(3), Expr(_number3, n => n.Value));
            test.Execute();
        }

        [TestMethod("c. ImmutableNumber.Subtract takes ImmutableNumber as argument and returns ImmutableNumber"), TestCategory("Exercise 3C")]
        public void SubtractTakesImmutableNumberAsArgumentAndReturnsImmutableNumber() 
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicMethod<ImmutableNumber, ImmutableNumber, ImmutableNumber>((n1, n2) => n1.Subtract(n2));
            test.Execute();
        }

        [TestMethod("d. ImmutableNumber.Subtract performs 8 - 3 = 5"), TestCategory("Exercise 3C")]
        public void SubstractProducesExpectedResult() 
        {
            ImmutableNumber number1 = new ImmutableNumber(8);
            ImmutableNumber number2 = new ImmutableNumber(3);

            ImmutableNumber number3 = number1.Subtract(number2);

            Assert.AreEqual(5, number3.Value);
            
            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<ImmutableNumber> _number1 = test.CreateVariable<ImmutableNumber>(nameof(_number1));
            TestVariable<ImmutableNumber> _number2 = test.CreateVariable<ImmutableNumber>(nameof(_number2));
            TestVariable<ImmutableNumber> _number3 = test.CreateVariable<ImmutableNumber>(nameof(_number3));
            test.Arrange(_number1, Expr(() => new ImmutableNumber(8)));
            test.Arrange(_number2, Expr(() => new ImmutableNumber(3)));
            test.Arrange(_number3, Expr(_number1, _number2, (n1, n2) => n1.Subtract(n2)));
            test.Assert.AreEqual(Const(5), Expr(_number3, n => n.Value));
            test.Execute();
        }

        [TestMethod("e. ImmutableNumber.Multiply takes ImmutableNumber as argument and returns ImmutableNumber"), TestCategory("Exercise 3C")]
        public void MultiplyTakesImmutableAsArgumentAndReturnsNothing()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicMethod<ImmutableNumber, ImmutableNumber, ImmutableNumber>((n1, n2) => n1.Multiply(n2));
            test.Execute();
        }

        [TestMethod("f. ImmutableNumber.Multiply performs 2 * 3 = 6"), TestCategory("Exercise 3C")]
        public void MultiplyProducesExpectedResult() {
            ImmutableNumber number1 = new ImmutableNumber(2);
            ImmutableNumber number2 = new ImmutableNumber(3);

            ImmutableNumber number3 = number1.Multiply(number2);

            Assert.AreEqual(6, number3.Value);
            
            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<ImmutableNumber> _number1 = test.CreateVariable<ImmutableNumber>(nameof(_number1));
            TestVariable<ImmutableNumber> _number2 = test.CreateVariable<ImmutableNumber>(nameof(_number2));
            TestVariable<ImmutableNumber> _number3 = test.CreateVariable<ImmutableNumber>(nameof(_number3));
            test.Arrange(_number1, Expr(() => new ImmutableNumber(2)));
            test.Arrange(_number2, Expr(() => new ImmutableNumber(3)));
            test.Arrange(_number3, Expr(_number1, _number2, (n1, n2) => n1.Multiply(n2)));
            test.Assert.AreEqual(Const(6), Expr(_number3, n => n.Value));
            test.Execute();
        }
        #endregion
    }
}
