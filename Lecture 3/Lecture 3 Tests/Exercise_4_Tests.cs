﻿using Lecture_1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestTools.Structure.Generic;

namespace Lecture_1_Tests
{
    [TestClass]
    public class Exercise_4_Tests
    {
        private ClassDefinition<Number> number => new ClassDefinition<Number>();
        private MethodDefinition<Number, bool, object> numberEquals => number.Method<bool, object>("Equals");
        private MethodDefinition<Number, int> numberGetHashCode => number.Method<int>("GetHashCode");
        private Number CreateNumber(int value) => number.Constructor<int>().Invoke(value);

        /* Exercise 4B */
        [TestMethod("a. Equals does not equate 4 and 5"), TestCategory("Exercise 4B")]
        public void EqualsDoesNotEquateFourAndFive()
        {
            Number four = CreateNumber(4);
            Number five = CreateNumber(5);

            bool isEquated = numberEquals.Invoke(four, five);

            if (isEquated)
                Assert.Fail("Equals equates 4 and 5");
        }

        [TestMethod("b. Equals equates 5 and 5"), TestCategory("Exercise 4B")]
        public void EqualsEquatesFiveAndFive()
        {
            Number five1 = CreateNumber(5);
            Number five2 = CreateNumber(5);

            bool isEquated = numberEquals.Invoke(five1, five2);

            if (!isEquated)
                Assert.Fail("Equals does not equate 5 and 5");
        }

        /* Exercise 4C */
        [TestMethod("a. GetHashCode does not equate 4 and 5"), TestCategory("Exercise 4C")]
        public void GetHashCodeDoesNotEquateFourAndFive()
        {
            Number four = CreateNumber(4);
            Number five = CreateNumber(5);

            bool isEquated = numberGetHashCode.Invoke(four) == numberGetHashCode.Invoke(five);

            if (isEquated)
                Assert.Fail("Equals equates 4 and 5");
        }

        [TestMethod("b. GetHashCode equates 5 and 5"), TestCategory("Exercise 4C")]
        public void GetHashCodeEquatesFiveAndFice()
        {
            Number five1 = CreateNumber(5);
            Number five2 = CreateNumber(5);

            bool isEquated = numberGetHashCode.Invoke(five1) == numberGetHashCode.Invoke(five2);

            if (!isEquated)
                Assert.Fail("GetHashCode does not equate 5 and 5");
        }
    }
}