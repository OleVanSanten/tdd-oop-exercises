using Lecture_7_Solutions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestTools.Expressions;
using TestTools.MSTest;
using TestTools.Structure;
using static Lecture_7_Tests.TestHelper;
using static TestTools.Expressions.TestExpression;

namespace Lecture_7_Tests
{
    [TestClass]
    public class Exercise_3_Tests {
        #region Exercise 3A
        [TestMethod("a. Dog has a public default constructor"), TestCategory("Exercise 3A")]
        public void DogHasPublicDefaultConstructor()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicConstructor<Dog>(() => new Dog());
            test.Execute();
        }

        [TestMethod("b. Dog.ID is public property"), TestCategory("Exercise 3A")]
        public void DogIDIsPublicProperty()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicProperty<Dog, int>(d => d.ID);
            test.Execute();
        }

        [TestMethod("c. Dog.Name has is public property"), TestCategory("Exercise 3A")]
        public void DogNameIsPublicProperty()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicProperty<Dog, string>(d => d.Name);
            test.Execute();
        }

        [TestMethod("d. Dog.Breed has is public property"), TestCategory("Exercise 3A")]
        public void DogBreedIsPublicProperty()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicProperty<Dog, string>(d => d.Breed);
            test.Execute();
        }

        [TestMethod("e. Dog.Age has is public property"), TestCategory("Exercise 3A")]
        public void DogAgeIsPublicProperty()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicProperty<Dog, int>(d => d.Age);
            test.Execute();
        }

        [TestMethod("f. Dog.ID = -1 throws ArgumentException"), TestCategory("Exercise 3A")]
        public void DogIDAssignmentOfMinus1ThrowsArgumentException()
        {
            Dog dog = new Dog();
            Assert.ThrowsException<ArgumentException>(() => dog.ID = -1);

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Dog> _dog = test.CreateVariable<Dog>();
            test.Arrange(_dog, Expr(() => new Dog()));
            test.Assert.ThrowsExceptionOn<ArgumentException>(Expr(_dog, d => d.SetID(-1)));
            test.Execute();
        }

        [TestMethod("g. Dog.Age = -1 throws ArgumentException"), TestCategory("Exercise 3A")]
        public void DogAgeAssignmentOfMinus1ThrowsArgumentException()
        {
            Dog dog = new Dog();
            Assert.ThrowsException<ArgumentException>(() => dog.Age = -1);

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Dog> _dog = test.CreateVariable<Dog>();
            test.Arrange(_dog, Expr(() => new Dog()));
            test.Assert.ThrowsExceptionOn<ArgumentException>(Expr(_dog, d => d.SetAge(-1)));
            test.Execute();
        }
        #endregion

        #region Exercise 3B
        [TestMethod("a. Dog implements ICloneable"), TestCategory("Exercise 3B")]
        public void MyQueueCountIsReadOnlyIntProoerty()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertClass<Dog>(new TypeIsSubclassOfVerifier(typeof(ICloneable)));
            test.Execute();
        }

        [TestMethod("b. Dog.Clone() clones fields"), TestCategory("Exercise 3B")]
        public void MyQueueCountIsInitializedAs0()
        {
            Dog dog = new Dog()
            {
                ID = 5,
                Name = "Buddy",
                Breed = "Labrador",
                Age = 4
            };

            Dog clonedDog = (Dog)dog.Clone();

            Assert.AreEqual(5, clonedDog.ID);
            Assert.AreEqual("Buddy", clonedDog.Name);
            Assert.AreEqual("Labrador", clonedDog.Breed);
            Assert.AreEqual(4, clonedDog.Age);

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Dog> _dog1 = test.CreateVariable<Dog>();
            TestVariable<Dog> _dog2 = test.CreateVariable<Dog>();
            test.Arrange(_dog1, Expr(() => new Dog() { ID = 5, Name = "Buddy", Breed = "Labrador", Age = 4 }));
            test.Arrange(_dog2, Expr(_dog1, d => (Dog)d.Clone()));
            test.Assert.AreEqual(Const(5), Expr(_dog2, d => d.ID));
            test.Assert.AreEqual(Const("Buddy"), Expr(_dog2, d => d.Name));
            test.Assert.AreEqual(Const("Labrador"), Expr(_dog2, d => d.Breed));
            test.Assert.AreEqual(Const(4), Expr(_dog2, d => d.Age));
            test.Execute();
        }
        #endregion

        #region Exercise 3C
        [TestMethod("a. Dog.Equals(Dog dog) does not equate dogs with different IDs"), TestCategory("Exercise 3C")]
        public void DogEqualsDoesNotEquateDogsWithDifferentIDs()
        {
            Dog dog1 = new Dog() { ID = 4 };
            Dog dog2 = new Dog() { ID = 5 };

            Assert.IsFalse(dog1.Equals(dog2));

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Dog> _dog1 = test.CreateVariable<Dog>();
            TestVariable<Dog> _dog2 = test.CreateVariable<Dog>();
            test.Arrange(_dog1, Expr(() => new Dog() { ID = 4 }));
            test.Arrange(_dog2, Expr(() => new Dog() { ID = 5 }));
            test.Assert.IsFalse(Expr(_dog1, _dog2, (d1, d2) => d1.Equals(d2)));
            test.Execute();
        }

        [TestMethod("b. Dog.Equals(Dog dog) equates dogs with same IDs"), TestCategory("Exercise 3C")]
        public void DogEqualsEquatesDogsWithSameIDs()
        {
            Dog dog1 = new Dog() { ID = 5 };
            Dog dog2 = new Dog() { ID = 5 };

            Assert.IsTrue(dog1.Equals(dog2));

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Dog> _dog1 = test.CreateVariable<Dog>();
            TestVariable<Dog> _dog2 = test.CreateVariable<Dog>();
            test.Arrange(_dog1, Expr(() => new Dog() { ID = 5 }));
            test.Arrange(_dog2, Expr(() => new Dog() { ID = 5 }));
            test.Assert.IsTrue(Expr(_dog1, _dog2, (d1, d2) => d1.Equals(d2)));
            test.Execute();
        }

        [TestMethod("c. Dog.GetHashCode() does not equate dogs with different IDs"), TestCategory("Exercise 3C")]
        public void DogGetHashCodeDoesNotEquateDogsWithDifferentIDs()
        {
            Dog dog1 = new Dog() { ID = 4 };
            Dog dog2 = new Dog() { ID = 5 };

            Assert.IsTrue(dog1.GetHashCode() != dog2.GetHashCode());

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Dog> _dog1 = test.CreateVariable<Dog>();
            TestVariable<Dog> _dog2 = test.CreateVariable<Dog>();
            test.Arrange(_dog1, Expr(() => new Dog() { ID = 4 }));
            test.Arrange(_dog2, Expr(() => new Dog() { ID = 5 }));
            test.Assert.IsTrue(Expr(_dog1, _dog2, (d1, d2) => d1.GetHashCode() != d2.GetHashCode()));
            test.Execute();
        }

        [TestMethod("d. Dog.GetHashCode() equates dogs with same IDs"), TestCategory("Exercise 3C")]
        public void DogGetHashCodeEquatesDogsWithSameIDs()
        {
            Dog dog1 = new Dog() { ID = 5 };
            Dog dog2 = new Dog() { ID = 5 };

            Assert.IsTrue(dog1.GetHashCode() == dog2.GetHashCode());

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Dog> _dog1 = test.CreateVariable<Dog>();
            TestVariable<Dog> _dog2 = test.CreateVariable<Dog>();
            test.Arrange(_dog1, Expr(() => new Dog() { ID = 5 }));
            test.Arrange(_dog2, Expr(() => new Dog() { ID = 5 }));
            test.Assert.IsTrue(Expr(_dog1, _dog2, (d1, d2) => d1.GetHashCode() == d2.GetHashCode()));
            test.Execute();
        }
        #endregion

        #region Exercise 3D
        [TestMethod("a. Dog.ToString() returns expected result"), TestCategory("Exercise 3D")]
        public void DogToStringReturnsExpectedResult()
        {
            Dog dog = new Dog()
            {
                ID = 3,
                Name = "Bella"
            };
            Assert.AreEqual("Dog Bella (3)", dog.ToString());

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Dog> _dog = test.CreateVariable<Dog>();
            test.Arrange(_dog, Expr(() => new Dog() { ID = 3, Name = "Bella" }));
            test.Assert.AreEqual(Const("Dog Bella (3)"), Expr(_dog, d => d.ToString()));
            test.Execute();
        }
        #endregion
    }
}
