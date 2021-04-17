using Lecture_6_Solutions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestTools.Expressions;
using TestTools.MSTest;
using TestTools.Structure;
using static Lecture_6_Tests.TestHelper;
using static TestTools.Expressions.TestExpression;

namespace Lecture_6_Tests
{
    [TestClass]
    public class Exercise_4_Tests 
    {
        #region Exercise 4A
        [TestMethod("a. Die constructor takes IRandom"), TestCategory("Exercise 4A")]
        public void DieConstructorTakesIRandom()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicConstructor<IRandom, Die>(r => new Die(r));
            test.Execute();
        }
        #endregion

        #region Exercise 4B
        [TestMethod("a. Die constructor takes IRandom and int"), TestCategory("Exercise 4B")]
        public void DieConstructorTakesIRandomAndInt()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicConstructor<IRandom, int, Die>((r, s) => new Die(r, s));
            test.Execute();
        }
        #endregion

        #region Exercise 4C
        [TestMethod("a. Die.Roll returns 5 if constructed PredictablyRandom(5)"), TestCategory("Exercise 4C")]
        public void DieRollReturns5()
        {
            PredictableRandom random = new PredictableRandom(5);
            Die die = new Die(random, 6);

            Assert.AreEqual(5, die.Roll());

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<PredictableRandom> _random = test.CreateVariable<PredictableRandom>();
            TestVariable<Die> _die = test.CreateVariable<Die>();
            test.Arrange(_random, Expr(() => new PredictableRandom(5)));
            test.Arrange(_die, Expr(_random, (r) => new Die(r, 6)));
            test.Assert.AreEqual(Const(5), Expr(_die, d => d.Roll()));
            test.Execute();
        }

        [TestMethod("b. Die.Roll returns a number between 1 and 6 if constructed with 6 sides and MyRandom"), TestCategory("Exercise 4C")]
        public void DieRollReturnsANumberBetween1And6()
        {
            MyRandom random = new MyRandom();
            Die die = new Die(random, 6);

            int value = die.Roll();

            Assert.IsTrue(1 <= value && value <= 6);

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<MyRandom> _random = test.CreateVariable<MyRandom>();
            TestVariable<Die> _die = test.CreateVariable<Die>();
            TestVariable<int> _value = test.CreateVariable<int>();
            test.Arrange(_random, Expr(() => new MyRandom()));
            test.Arrange(_die, Expr(_random, (r) => new Die(r, 6)));
            test.Arrange(_value, Expr(_die, d => d.Roll()));
            test.Assert.IsTrue(Expr(_value, v => 1 <= v && v <= 6));
            test.Execute();
        }
        #endregion
    }
}
