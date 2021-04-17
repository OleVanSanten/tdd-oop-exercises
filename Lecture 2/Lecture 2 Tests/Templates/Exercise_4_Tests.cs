using Lecture_2_Solutions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestTools.Expressions;
using TestTools.MSTest;
using static Lecture_2_Tests.TestHelper;
using static TestTools.Expressions.TestExpression;

namespace Lecture_2_Tests
{
    [TestClass]
    public class Exercise_4_Tests
    {
        #region Exercise 4B
        [TestMethod("a. Number.Equals does not equate 4 and 5"), TestCategory("Exercise 4B")]
        public void EqualsDoesNotEquateFourAndFive()
        {
            Number number1 = new Number(4);
            Number number2 = new Number(5);

            Assert.IsFalse(number1.Equals(number2));

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Number> _number1 = test.CreateVariable<Number>(nameof(_number1));
            TestVariable<Number> _number2 = test.CreateVariable<Number>(nameof(_number2));
            test.Arrange(_number1, Expr(() => new Number(4)));
            test.Arrange(_number2, Expr(() => new Number(5)));
            test.Assert.IsFalse(Expr(_number1, _number2, (n1, n2) => n1.Equals(n2)));
            test.Execute();
        }

        [TestMethod("b. Number.Equals equates 5 and 5"), TestCategory("Exercise 4B")]
        public void EqualsEquatesFiveAndFive()
        {
            Number number1 = new Number(5);
            Number number2 = new Number(5);

            Assert.IsTrue(number1.Equals(number2));

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Number> _number1 = test.CreateVariable<Number>(nameof(_number1));
            TestVariable<Number> _number2 = test.CreateVariable<Number>(nameof(_number2));
            test.Arrange(_number1, Expr(() => new Number(5)));
            test.Arrange(_number2, Expr(() => new Number(5)));
            test.Assert.IsTrue(Expr(_number1, _number2, (n1, n2) => n1.Equals(n2)));
            test.Execute();
        }
        #endregion

        #region Exercise 4C
        [TestMethod("a. Number.GetHashCode does not equate 4 and 5"), TestCategory("Exercise 4C")]
        public void GetHashCodeDoesNotEquateFourAndFive()
        {
            Number number1 = new Number(4);
            Number number2 = new Number(5);

            Assert.IsFalse(number1.GetHashCode() == number2.GetHashCode());

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Number> _number1 = test.CreateVariable<Number>(nameof(_number1));
            TestVariable<Number> _number2 = test.CreateVariable<Number>(nameof(_number2));
            test.Arrange(_number1, Expr(() => new Number(4)));
            test.Arrange(_number2, Expr(() => new Number(5)));
            test.Assert.IsFalse(Expr(_number1, _number2, (n1, n2) => n1.GetHashCode() == n2.GetHashCode()));
            test.Execute();
        }

        [TestMethod("b. Number.GetHashCode equates 5 and 5"), TestCategory("Exercise 4C")]
        public void GetHashCodeEquatesFiveAndFice()
        {
            Number number1 = new Number(5);
            Number number2 = new Number(5);

            Assert.IsTrue(number1.GetHashCode() == number2.GetHashCode());

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Number> _number1 = test.CreateVariable<Number>(nameof(_number1));
            TestVariable<Number> _number2 = test.CreateVariable<Number>(nameof(_number2));
            test.Arrange(_number1, Expr(() => new Number(5)));
            test.Arrange(_number2, Expr(() => new Number(5)));
            test.Assert.IsTrue(Expr(_number1, _number2, (n1, n2) => n1.GetHashCode() == n2.GetHashCode()));
            test.Execute();
        }
        #endregion
    }
}
