using Lecture_9_Solutions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using TestTools.Expressions;
using TestTools.MSTest;
using TestTools.Structure;
using static Lecture_9_Tests.TestHelper;
using static TestTools.Helpers.StructureHelper;
using static TestTools.Expressions.TestExpression;

namespace Lecture_9_Tests
{
    [TestClass]
    public class Exercise_1_Tests
    {
        #region Exercise 1A
        [TestMethod("SortedCollection<T> implements ICollection<T>"), TestCategory("Exercise 1A")]
        public void SortedCollectionImplementsICollections()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertClass<SortedCollection<int>>(new TypeIsSubclassOfVerifier(typeof(ICollection<int>)));
            test.AssertClass<SortedCollection<double>>(new TypeIsSubclassOfVerifier(typeof(ICollection<double>)));
            test.Execute();
        }

        [TestMethod("SortedCollection<T>.Add(T elem) adds element in correct order"), TestCategory("Exercise 1A")]
        public void SortedCollectionAddAddsElementInOrder()
        {
            SortedCollection<int> collection = new SortedCollection<int>();

            collection.Add(3);
            collection.Add(5);
            collection.Add(2);

            Assert.IsTrue(collection.SequenceEqual(new int[] { 2, 3, 5 }));

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<SortedCollection<int>> _collection = test.CreateVariable<SortedCollection<int>>();
            test.Arrange(_collection, Expr(() => new SortedCollection<int>()));
            test.Act(Expr(_collection, c => c.Add(3)));
            test.Act(Expr(_collection, c => c.Add(5)));
            test.Act(Expr(_collection, c => c.Add(2)));
            test.Assert.IsTrue(Expr(_collection, c => c.SequenceEqual(new[] { 2, 3, 5 })));
            test.Execute();
        }

        [TestMethod("SortedCollection<T>.Clear() removes all elements"), TestCategory("Exercise 1A")]
        public void SortedCollectionClearRemovesAllElements()
        {
            SortedCollection<int> collection = new SortedCollection<int>() { 1, 2, 3 };

            collection.Clear();

            Assert.IsFalse(collection.Any());

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<SortedCollection<int>> _collection = test.CreateVariable<SortedCollection<int>>();
            test.Arrange(_collection, Expr(() => new SortedCollection<int>() { 1, 2, 3 }));
            test.Act(Expr(_collection, c => c.Clear()));
            test.Assert.IsFalse(Expr(_collection, c => c.Any()));
            test.Execute();
        }

        [TestMethod("SortedCollection<T>.Contains(T elem) returns true if element in collection"), TestCategory("Exercise 1A")]
        public void SortedCollectionContainsReturnsTrue()
        {
            SortedCollection<int> collection = new SortedCollection<int>() { 1, 2, 3 };
            Assert.IsTrue(collection.Contains(2));

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<SortedCollection<int>> _collection = test.CreateVariable<SortedCollection<int>>();
            test.Arrange(_collection, Expr(() => new SortedCollection<int>() { 1, 2, 3 }));
            test.Assert.IsTrue(Expr(_collection, c => c.Contains(2)));
            test.Execute();
        }

        [TestMethod("SortedCollection<T>.Contains(T elem) returns false if element is not in collection"), TestCategory("Exercise 1A")]
        public void SortedCollectionContainsReturnsFalse()
        {
            SortedCollection<int> collection = new SortedCollection<int>() { 1, 2, 3 };
            Assert.IsFalse(collection.Contains(4));

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<SortedCollection<int>> _collection = test.CreateVariable<SortedCollection<int>>();
            test.Arrange(_collection, Expr(() => new SortedCollection<int>() { 1, 2, 3 }));
            test.Assert.IsFalse(Expr(_collection, c => c.Contains(4)));
            test.Execute();
        }

        [TestMethod("SortedCollection<T>.CopyTo(T[] arr, int i) copies elements to array"), TestCategory("Exercise 1A")]
        public void SortedCollectionCopyToCopiesElementsToArray()
        {
            SortedCollection<int> collection = new SortedCollection<int>() { 2, 3, 5 };
            int[] array = new int[3];

            collection.CopyTo(array, 0);

            Assert.IsTrue(array.SequenceEqual(new int[] { 2, 3, 5 }));

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<SortedCollection<int>> _collection = test.CreateVariable<SortedCollection<int>>();
            TestVariable<int[]> _array = test.CreateVariable<int[]>();
            test.Arrange(_collection, Expr(() => new SortedCollection<int>() { 2, 3, 5 }));
            test.Arrange(_array, Expr(() => new int[3]));
            test.Act(Expr(_collection, _array, (c, a) => c.CopyTo(a, 0)));
            test.Assert.IsTrue(Expr(_array, a => a.SequenceEqual(new[] { 2, 3, 5 })));
            test.Execute();
        }

        [TestMethod("SortedCollection<T>.Remove(T elem) removes element"), TestCategory("Exercise 1A")]
        public void SortedCollectionRemoveRemovesElement()
        {
            SortedCollection<int> collection = new SortedCollection<int>() { 1, 2, 3 };

            collection.Remove(1);

            Assert.IsTrue(collection.SequenceEqual(new int[] { 2, 3 }));

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<SortedCollection<int>> _collection = test.CreateVariable<SortedCollection<int>>();
            test.Arrange(_collection, Expr(() => new SortedCollection<int>() { 1, 2, 3 }));
            test.Act(Expr(_collection, c => c.Remove(1)));
            test.Assert.IsTrue(Expr(_collection, c => c.SequenceEqual(new[] { 2, 3 })));
            test.Execute();
        }
        #endregion

        #region Exercise 1B
        [TestMethod("SortedCollection<T>[int index] is a readonly property"), TestCategory("Exercise 1B")]
        public void SortedCollectionIsAReadonlyProperty()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicReadonlyProperty(GetIndexProperty<SortedCollection<int>>());
            test.Execute();
        }

        [TestMethod("SortedCollection<T>[int index] returns correct element"), TestCategory("Exercise 1B")]
        public void SortedCollectionReturnsCorrectElement()
        {
            SortedCollection<int> collection = new SortedCollection<int>() { 1, 2, 3 };
            Assert.AreEqual(collection[1], 2);

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<SortedCollection<int>> _collection = test.CreateVariable<SortedCollection<int>>();
            test.Arrange(_collection, Expr(() => new SortedCollection<int>() { 1, 2, 3 }));
            test.Assert.AreEqual(Expr(_collection, c => c[1]), Const(2));

            test.Execute();
        }
        #endregion

        #region Exercise 1C
        [TestMethod("SortedCollection<T>.GetAll() returns enumeration of collection"), TestCategory("Exercise 1C")]
        public void SortCollectionGetAllReturnsEnumerationOfCollection()
        {
            SortedCollection<int> collection = new SortedCollection<int>() { 1, 2, 3 };
            Assert.IsTrue(collection.GetAll().SequenceEqual(new int[] { 1, 2, 3 }));

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<SortedCollection<int>> _collection = test.CreateVariable<SortedCollection<int>>();
            test.Arrange(_collection, Expr(() => new SortedCollection<int>() { 1, 2, 3 }));
            test.Assert.IsTrue(Expr(_collection, c => c.GetAll().SequenceEqual(new[] { 1, 2, 3 })));
            test.Execute();
        }

        [TestMethod("SortedCollection<T>.GetAllReversed() returns reversed enumeration of collection"), TestCategory("Exercise 1C")]
        public void SortedCollectionGetAllReversedReturnsReversedEnumerationOfCollection()
        {
            SortedCollection<int> collection = new SortedCollection<int>() { 1, 2, 3 };
            Assert.IsTrue(collection.GetAllReversed().SequenceEqual(new int[] { 3, 2, 1 }));

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<SortedCollection<int>> _collection = test.CreateVariable<SortedCollection<int>>();
            test.Arrange(_collection, Expr(() => new SortedCollection<int>() { 1, 2, 3 }));
            test.Assert.IsTrue(Expr(_collection, c => c.GetAllReversed().SequenceEqual(new[] { 3, 2, 1 })));
            test.Execute();
        }
        #endregion

        #region Exercise 1D
        [TestMethod("SortedCollection<T>.GetAll(Predicate<T> p) returns enumeration of collection"), TestCategory("Exercise 1D")]
        public void SortCollectionGetAllReturnsEnumerationOfCollection2()
        {
            SortedCollection<int> collection = new SortedCollection<int>() { 1, 2, 3, 4 };
            Assert.IsTrue(collection.GetAll(x => x % 2 == 0).SequenceEqual(new int[] { 2, 4 }));

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<SortedCollection<int>> _collection = test.CreateVariable<SortedCollection<int>>();
            test.Arrange(_collection, Expr(() => new SortedCollection<int>() { 1, 2, 3, 4 }));
            test.Assert.IsTrue(Expr(_collection, c => c.GetAll(x => x % 2 == 0).SequenceEqual(new[] { 2, 4 })));
            test.Execute();
        }

        [TestMethod("SortedCollection<T>.GetAllReversed(Predicate<T> p) returns reversed enumeration of collection"), TestCategory("Exercise 1D")]
        public void SortedCollectionGetAllReversedReturnsReversedEnumerationOfCollection2()
        {
            SortedCollection<int> collection = new SortedCollection<int>() { 1, 2, 3, 4 };
            Assert.IsTrue(collection.GetAllReversed(x => x % 2 == 0).SequenceEqual(new int[] { 4, 2 }));

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<SortedCollection<int>> _collection = test.CreateVariable<SortedCollection<int>>();
            test.Arrange(_collection, Expr(() => new SortedCollection<int>() { 1, 2, 3, 4 }));
            test.Assert.IsTrue(Expr(_collection, c => c.GetAllReversed(x => x % 2 == 0).SequenceEqual(new[] { 4, 2 })));
            test.Execute();
        }
        #endregion
    }
}
