using Lecture_7_Solutions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TestTools.Expressions;
using TestTools.MSTest;
using TestTools.Structure;
using static Lecture_7_Tests.TestHelper;
using static TestTools.Expressions.TestExpression;

namespace Lecture_7_Tests
{
    [TestClass]
    public class Exercise_4_Tests
    {
        #region Exercise 4A
        [TestMethod("a. Repository<T> has a default constructor"), TestCategory("Exercise 4A")]
        public void RepositoryHasPublicDefaultConstructor()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicConstructor<Repository<ICloneable>>(() => new Repository<ICloneable>());
            test.Execute();
        }
        #endregion

        #region Exercise 4B
        [TestMethod("a. Repositoty<T>.Add(T entity) is a public method"), TestCategory("Exercise 4B")]
        public void RepositoryAddIsAPublicMethod()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicMethod<Repository<ICloneable>, ICloneable>((r, e) => r.Add(e));
            test.AssertPublicMethod<Repository<Dog>, Dog>((r, e) => r.Add(e));
            test.Execute();
        }

        [TestMethod("b. Repository<T>.Add(T entity) takes TEntity and adds it to list of entities"), TestCategory("Exercise 4B")]
        public void RepositoryAddTakesTEntityAndAddsToEntities()
        {
            Repository<Dog> repository = new Repository<Dog>();
            
            repository.Add(new Dog());

            Assert.AreEqual(1, repository.GetAll().Count);

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Repository<Dog>> _repository = test.CreateVariable<Repository<Dog>>();
            test.Arrange(_repository, Expr(() => new Repository<Dog>()));
            test.Act(Expr(_repository, r => r.Add(new Dog())));
            test.Assert.AreEqual(Const(1), Expr(_repository, r => r.GetAll().Count));
            test.Execute();
        }

        [TestMethod("c. Repository<T>.Add(T entity) adds entity, which cannot be affected"), TestCategory("Exercise 4B")]
        public void RepositoryAddAddsEntityWhichCannotBeAffected()
        {
            Dog dog = new Dog() { Name = "Name" };
            Repository<Dog> repository = new Repository<Dog>();
            repository.Add(dog);

            dog.Name = "NewName";

            Assert.AreEqual("Name", repository.GetAll()[0].Name);

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Dog> _dog = test.CreateVariable<Dog>();
            TestVariable<Repository<Dog>> _reposity = test.CreateVariable<Repository<Dog>>();
            test.Arrange(_dog, Expr(() => new Dog() { Name = "Name" }));
            test.Arrange(_reposity, Expr(() => new Repository<Dog>()));
            test.Act(Expr(_reposity, _dog, (r, d) => r.Add(d)));
            test.Act(Expr(_dog, d => d.SetName("NewName")));
            test.Assert.AreEqual(Const("Name"), Expr(_reposity, r => r.GetAll()[0].Name));
            test.Execute();
        }
        #endregion

        #region Exercise 4C
        [TestMethod("a. Repositoty<T>.GetAll() is a public method"), TestCategory("Exercise 4C")]
        public void RepositoryGetAllIsAPublicMethod()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicMethod<Repository<ICloneable>, List<ICloneable>>(r => r.GetAll());
            test.AssertPublicMethod<Repository<Dog>, List<Dog>>(r => r.GetAll());
            test.Execute();
        }

        [TestMethod("b. Repository<T>.GetAll() takes nothing and returns list of all entities"), TestCategory("Exercise 4C")]
        public void RepositoryGetAllReturnsListOfEntities()
        {
            Repository<Dog> repository = new Repository<Dog>();

            repository.Add(new Dog() { ID = 0 });
            repository.Add(new Dog() { ID = 1 });

            Assert.AreEqual(2, repository.GetAll().Count);

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Repository<Dog>> _repository = test.CreateVariable<Repository<Dog>>();
            test.Arrange(_repository, Expr(() => new Repository<Dog>()));
            test.Act(Expr(_repository, r => r.Add(new Dog() { ID = 0 })));
            test.Act(Expr(_repository, r => r.Add(new Dog() { ID = 1 })));
            test.Assert.AreEqual(Const(2), Expr(_repository, r => r.GetAll().Count));
            test.Execute();
        }
        #endregion

        #region Exercise 4D
        [TestMethod("a. Repositoty<T>.Update(T entity) is a public method"), TestCategory("Exercise 4D")]
        public void RepositoryUpdateIsAPublicMethod()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicMethod<Repository<ICloneable>, ICloneable>((r, e) => r.Update(e));
            test.AssertPublicMethod<Repository<Dog>, Dog>((r, e) => r.Update(e));
            test.Execute();
        }

        [TestMethod("b. Repository<T>.Update(T entity) updates an entity"), TestCategory("Exercise 4D")]
        public void RepositoryUpdateUpdatesAnEntity()
        {
            Dog dog = new Dog() 
            {
                Name = "Name"
            };
            Repository<Dog> repository = new Repository<Dog>();
            repository.Add(dog);

            dog.Name = "NewName";
            repository.Update(dog);

            Assert.AreEqual("NewName", repository.GetAll()[0].Name);

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Repository<Dog>> _repository = test.CreateVariable<Repository<Dog>>();
            TestVariable<Dog> _dog = test.CreateVariable<Dog>();
            test.Arrange(_repository, Expr(() => new Repository<Dog>()));
            test.Arrange(_dog, Expr(() => new Dog() { ID = 2, Name = "Name" }));
            test.Act(Expr(_repository, _dog, (r, d) => r.Add(d)));
            test.Act(Expr(_dog, d => d.SetName("NewName")));
            test.Act(Expr(_repository, _dog, (r, d) => r.Update(d)));
            test.Assert.AreEqual(Const("NewName"), Expr(_repository, r => r.GetAll()[0].Name));
            test.Execute();
        }
        #endregion

        #region Exercise 4E
        [TestMethod("a. Repositoty<T>.Delete(T entity) is a public method"), TestCategory("Exercise 4E")]
        public void RepositoryDeleteIsAPublicMethod()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicMethod<Repository<ICloneable>, ICloneable>((r, e) => r.Update(e));
            test.AssertPublicMethod<Repository<Dog>, Dog>((r, e) => r.Update(e));
            test.Execute();
        }

        [TestMethod("a. Repository<T>.Delete(TEntity) deletes an entity"), TestCategory("Exercise 4E")]
        public void RepositoryDeleteDeletesAnEntity()
        {
            Repository<Dog> repository = new Repository<Dog>();
            Dog dog = new Dog();

            repository.Add(dog);
            repository.Delete(dog);

            Assert.AreEqual(0, repository.GetAll().Count);

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Repository<Dog>> _repository = test.CreateVariable<Repository<Dog>>();
            TestVariable<Dog> _dog = test.CreateVariable<Dog>();
            test.Arrange(_repository, Expr(() => new Repository<Dog>()));
            test.Arrange(_dog, Expr(() => new Dog() { ID = 1 }));
            test.Act(Expr(_repository, _dog, (r, d) => r.Add(d)));
            test.Act(Expr(_repository, _dog, (r, d) => r.Delete(d)));
            test.Assert.AreEqual(Const(0), Expr(_repository, r => r.GetAll().Count));
            test.Execute();
        }
        #endregion
    }
}
