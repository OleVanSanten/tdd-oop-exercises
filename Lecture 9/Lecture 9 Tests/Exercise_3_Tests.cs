using Lecture_9_Solutions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using TestTools.Expressions;
using TestTools.MSTest;
using TestTools.Structure;
using static Lecture_9_Tests.TestHelper;
using static TestTools.Expressions.TestExpression;

namespace Lecture_9_Tests
{
    [TestClass]
    public class Exercise_3_Tests
    {
        #region Exercise 3A
        [TestMethod("a. Product.ID is a public property"), TestCategory("Exercise 3A")]
        public void ProductIDISAPublicProperty()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicProperty<Product, int>(s => s.ID);
            test.Execute();
        }

        [TestMethod("b. Product.Name is a public property"), TestCategory("Exercise 3A")]
        public void ProductTitleIsAPublicProperty()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicProperty<Product, string>(s => s.Name);
            test.Execute();
        }

        [TestMethod("c. Product.Category is a public property"), TestCategory("Exercise 3A")]
        public void ProductCategoryIsAPublicProperty()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicProperty<Product, string>(s => s.Category);
            test.Execute();
        }

        [TestMethod("d. Product.Price is a public property"), TestCategory("Exercise 3A")]
        public void StudentAgeIsAPublicProperty()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicProperty<Product, decimal>(s => s.Price);
            test.Execute();
        }

        [TestMethod("e. Product.Equals equates products with same ID"), TestCategory("Exercise 3A")]
        public void ProductEqualsEquatesProductsWithSameID()
        {
            Product product1 = new Product() { ID = 5 };
            Product product2 = new Product() { ID = 5 };

            Assert.IsTrue(product1.Equals(product2));

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Product> _product1 = test.CreateVariable<Product>();
            TestVariable<Product> _product2 = test.CreateVariable<Product>();
            test.Arrange(_product1, Expr(() => new Product() { ID = 5 }));
            test.Arrange(_product2, Expr(() => new Product() { ID = 5 }));
            test.Assert.IsTrue(Expr(_product1, _product2, (p1, p2) => p1.Equals(p2)));
            test.Execute();
        }

        [TestMethod("f. Product.Equals does not equate products with different ID"), TestCategory("Exercise 3A")]
        public void ProductEqualsDoesNotEquateProductsWithDifferentID()
        {
            Product product1 = new Product() { ID = 4 };
            Product product2 = new Product() { ID = 5 };

            Assert.IsFalse(product1.Equals(product2));

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Product> _product1 = test.CreateVariable<Product>();
            TestVariable<Product> _product2 = test.CreateVariable<Product>();
            test.Arrange(_product1, Expr(() => new Product() { ID = 4 }));
            test.Arrange(_product2, Expr(() => new Product() { ID = 5 }));
            test.Assert.IsFalse(Expr(_product1, _product2, (p1, p2) => p1.Equals(p2)));
            test.Execute();
        }

        [TestMethod("g. Product.GetHashCode equates products with same ID"), TestCategory("Exercise 3A")]
        public void ProductGetHashCodeEquatesProductsWithSameID()
        {
            Product product1 = new Product() { ID = 5 };
            Product product2 = new Product() { ID = 5 };

            Assert.IsTrue(product1.GetHashCode() == product2.GetHashCode());

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Product> _product1 = test.CreateVariable<Product>();
            TestVariable<Product> _product2 = test.CreateVariable<Product>();
            test.Arrange(_product1, Expr(() => new Product() { ID = 5 }));
            test.Arrange(_product2, Expr(() => new Product() { ID = 5 }));
            test.Assert.IsTrue(Expr(_product1, _product2, (p1, p2) => p1.GetHashCode() == p2.GetHashCode()));
            test.Execute();
        }

        [TestMethod("h. Product.GetHashCode does not equate products with different ID"), TestCategory("Exercise 3A")]
        public void ProductGetHashCodeDoesNotEquateProductsWithDifferentID()
        {
            Product product1 = new Product() { ID = 4 };
            Product product2 = new Product() { ID = 5 };

            Assert.IsFalse(product1.GetHashCode() == product2.GetHashCode());

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<Product> _product1 = test.CreateVariable<Product>();
            TestVariable<Product> _product2 = test.CreateVariable<Product>();
            test.Arrange(_product1, Expr(() => new Product() { ID = 4 }));
            test.Arrange(_product2, Expr(() => new Product() { ID = 5 }));
            test.Assert.IsFalse(Expr(_product1, _product2, (p1, p2) => p1.GetHashCode() == p2.GetHashCode()));
            test.Execute();
        }
        #endregion

        #region Exercise 3B
        [TestMethod("a. ProductRepository implements IEnumerable<T>"), TestCategory("Exercise 3B")]
        public void ProductRepositoryImplementsIEnumerable()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertClass<ProductRepository>(new TypeIsSubclassOfVerifier(typeof(IEnumerable<Product>)));
            test.Execute();
        }
        #endregion

        #region Exercise 3C
        [TestMethod("a. ProductRepository.Add(Product p) is a public method"), TestCategory("Exercise 3C")]
        public void ProductRepositoryAddIsAPublicMethod()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicMethod<ProductRepository, Product>((r, p) => r.Add(p));
            test.Execute();
        }

        [TestMethod("b. ProductRepository.Update(Product p) is a public method"), TestCategory("Exercise 3C")]
        public void ProductRepositoryUpdateIsAPublicMethod()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicMethod<ProductRepository, Product>((r, p) => r.Update(p));
            test.Execute();
        }

        [TestMethod("c. ProductRepository.Delete(Product p) is a public method"), TestCategory("Exercise 3C")]
        public void ProductRepositoryDeleteIsAPublicMethod()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicMethod<ProductRepository, Product>((r, p) => r.Update(p));
            test.Execute();
        }

        [TestMethod("d. ProductRepository.Add(Product p) adds product"), TestCategory("Exercise 3C")]
        public void ProductRepositoryAddAddsProduct()
        {
            ProductRepository repository = new ProductRepository();
            Product product = new Product();

            repository.Add(product);

            Assert.IsTrue(repository.SequenceEqual(new Product[] { product }));

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<ProductRepository> _repository = test.CreateVariable<ProductRepository>();
            TestVariable<Product> _product = test.CreateVariable<Product>();
            test.Arrange(_repository, Expr(() => new ProductRepository()));
            test.Arrange(_product, Expr(() => new Product()));
            test.Act(Expr(_repository, _product, (r, p) => r.Add(p)));
            test.Assert.IsTrue(Expr(_repository, _product, (r, p) => r.SequenceEqual(new[] { p })));
            test.Execute();
        }

        [TestMethod("e. ProductRepository.Add(Product p) adds product, which cannot be affected"), TestCategory("Exercise 3C")]
        public void ProductRepositoryAddAddsProduct2()
        {
            Product product = new Product() { Name = "Name" };
            ProductRepository repository = new ProductRepository() { product };

            product.Name = "NewName";

            Assert.AreEqual("Name", repository.First().Name);

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<ProductRepository> _repository = test.CreateVariable<ProductRepository>();
            TestVariable<Product> _product = test.CreateVariable<Product>();
            test.Arrange(_product, Expr(() => new Product() { Name = "Name" }));
            test.Arrange(_repository, Expr(_product, p => new ProductRepository() { p }));
            test.Act(Expr(_product, p => p.SetName("NewName")));
            test.Assert.AreEqual(Const("Name"), Expr(_repository, r => r.First().Name));
            test.Execute();
        }

        [TestMethod("f. ProductRepository.Update(Product p) updates product, which cannot be affected"), TestCategory("Exercise 3C")]
        public void ProductRepositoryUpdatesProduct()
        {
            Product product = new Product() { Name = "Name" };
            ProductRepository repository = new ProductRepository() { product };

            product.Name = "NewName";
            repository.Update(product);

            Assert.AreEqual("NewName", repository.First().Name);

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<ProductRepository> _repository = test.CreateVariable<ProductRepository>();
            TestVariable<Product> _product = test.CreateVariable<Product>();
            test.Arrange(_product, Expr(() => new Product() { Name = "Name" }));
            test.Arrange(_repository, Expr(_product, p => new ProductRepository() { p }));
            test.Act(Expr(_product, p => p.SetName("NewName")));
            test.Act(Expr(_repository, _product, (r, p) => r.Update(p)));
            test.Assert.AreEqual(Const("NewName"), Expr(_repository, r => r.First().Name));
            test.Execute();
        }

        [TestMethod("g. ProductRepository.Delete(Product p) removes product again"), TestCategory("Exercise 3C")]
        public void ProductRepositoryDeleteRemovesProductAgain()
        {
            ProductRepository repository = new ProductRepository();
            Product product = new Product();

            repository.Add(product);
            repository.Delete(product);

            Assert.IsFalse(repository.Any());

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<ProductRepository> _repository = test.CreateVariable<ProductRepository>();
            TestVariable<Product> _product = test.CreateVariable<Product>();
            test.Arrange(_repository, Expr(() => new ProductRepository()));
            test.Arrange(_product, Expr(() => new Product()));
            test.Act(Expr(_repository, _product, (r, p) => r.Add(p)));
            test.Act(Expr(_repository, _product, (r, p) => r.Delete(p)));
            test.Assert.IsFalse(Expr(_repository, r => r.Any()));
            test.Execute();
        }
        #endregion

        #region Exercise 3D
        [TestMethod("a. ProductRepository.GetProductByID(int id) is a public method"), TestCategory("Exercise 3D")]
        public void ProductRepositoryGetProductByIDIsAPublicMethod()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicMethod<ProductRepository, int, Product>((r, i) => r.GetProductByID(i));
            test.Execute();
        }

        [TestMethod("b. ProductRepository.GetLeastExpensiveProduct() is a public method"), TestCategory("Exercise 3D")]
        public void ProductRepositoryGetLeastExpensiveProductIsAPublicMethod()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicMethod<ProductRepository, Product>(r => r.GetLeastExpensiveProduct());
            test.Execute();
        }

        [TestMethod("c. ProductRepository.GetMostExpensiveProduct() is a public method"), TestCategory("Exercise 3D")]
        public void ProductRepositoryGetMostExpensiveProductIsAPublicMethod()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicMethod<ProductRepository, Product>(r => r.GetMostExpensiveProduct());
            test.Execute();
        }

        [TestMethod("d. ProductRepository.GetAverageProductPrice() is a public method"), TestCategory("Exercise 3D")]
        public void ProductRepositoryGetAverageProductPriceIsAPublicMethod()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicMethod<ProductRepository, decimal>(r => r.GetAverageProductPrice());
            test.Execute();
        }

        [TestMethod("e. ProductRepository.GetProductsInCategory(string category) is a public method"), TestCategory("Exercise 3D")]
        public void ProductRepositoryGetProductsInCategoryIsAPublicMethod()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicMethod<ProductRepository, string, IEnumerable<Product>>((r, s) => r.GetProductsInCategory(s));
            test.Execute();
        }

        [TestMethod("f. ProductRepository.GetProductCategories() is a public method"), TestCategory("Exercise 3D")]
        public void ProductRepositoryGetProductCategoriesIsAPublicMethod()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicMethod<ProductRepository, IEnumerable<string>>(r => r.GetProductCategories());
            test.Execute();
        }

        [TestMethod("g. ProductRepository.GetProductByID(int id) returns correctly"), TestCategory("Exercise 3D")]
        public void ProductRepositoryGetProductByIDReturnsCorrectly()
        {
            Product product = new Product() { ID = 5 };
            ProductRepository repository = new ProductRepository() { product };

            Assert.AreEqual(product, repository.GetProductByID(5));

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<ProductRepository> _repository = test.CreateVariable<ProductRepository>();
            TestVariable<Product> _product = test.CreateVariable<Product>();
            test.Arrange(_product, Expr(() => new Product() { ID = 5 }));
            test.Arrange(_repository, Expr(_product, p => new ProductRepository() { p }));
            test.Assert.AreEqual(Expr(_product, p => p), Expr(_repository, r => r.GetProductByID(5)));
            test.Execute();
        }

        [TestMethod("h. ProductRepository.GetLeastExpensiveProduct() returns correctly"), TestCategory("Exercise 3D")]
        public void ProductRepositoryGetLeastExpensiveProductReturnsCorrectly()
        {
            Product leastExpensiveProduct = new Product() { ID = 4, Price = 10M };
            Product mostExpensiveProduct = new Product() { ID = 5, Price = 20M };
            ProductRepository repository = new ProductRepository()
            {
                leastExpensiveProduct,
                mostExpensiveProduct
            };

            Assert.AreEqual(leastExpensiveProduct, repository.GetLeastExpensiveProduct());

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<ProductRepository> _repository = test.CreateVariable<ProductRepository>();
            TestVariable<Product> _leastExpensiveProduct = test.CreateVariable<Product>();
            TestVariable<Product> _mostExpensiveProduct = test.CreateVariable<Product>();
            test.Arrange(_repository, Expr(() => new ProductRepository()));
            test.Arrange(_leastExpensiveProduct, Expr(() => new Product() { ID = 4, Price = 10M }));
            test.Arrange(_mostExpensiveProduct, Expr(() => new Product() { ID = 5, Price = 20M }));
            test.Act(Expr(_repository, _leastExpensiveProduct, (r, p) => r.Add(p)));
            test.Act(Expr(_repository, _mostExpensiveProduct, (r, p) => r.Add(p)));
            test.Assert.AreEqual(Expr(_leastExpensiveProduct, p => p), Expr(_repository, r => r.GetLeastExpensiveProduct()));
            test.Execute();
        }

        [TestMethod("i. ProductRepository.GetMostExpensiveProduct() returns correctly"), TestCategory("Exercise 3D")]
        public void ProductRepositoryGetMostExpensiveProductReturnsCorrectly()
        {
            Product leastExpensiveProduct = new Product() { ID = 4, Price = 10M };
            Product mostExpensiveProduct = new Product() { ID = 5, Price = 20M };
            ProductRepository repository = new ProductRepository()
            {
                leastExpensiveProduct,
                mostExpensiveProduct
            };

            Assert.AreEqual(mostExpensiveProduct, repository.GetMostExpensiveProduct());

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<ProductRepository> _repository = test.CreateVariable<ProductRepository>();
            TestVariable<Product> _leastExpensiveProduct = test.CreateVariable<Product>();
            TestVariable<Product> _mostExpensiveProduct = test.CreateVariable<Product>();
            test.Arrange(_repository, Expr(() => new ProductRepository()));
            test.Arrange(_leastExpensiveProduct, Expr(() => new Product() { ID = 4, Price = 10M }));
            test.Arrange(_mostExpensiveProduct, Expr(() => new Product() { ID = 5, Price = 20M }));
            test.Act(Expr(_repository, _leastExpensiveProduct, (r, p) => r.Add(p)));
            test.Act(Expr(_repository, _mostExpensiveProduct, (r, p) => r.Add(p)));
            test.Assert.AreEqual(Expr(_mostExpensiveProduct, p => p), Expr(_repository, r => r.GetMostExpensiveProduct()));
            test.Execute();
        }

        [TestMethod("i. ProductRepository.GetAverageProductPrice() returns correctly"), TestCategory("Exercise 3D")]
        public void ProductRepositoryGetAverageProductPriceReturnsCorrectly()
        {
            Product leastExpensiveProduct = new Product() { ID = 4, Price = 10M };
            Product mostExpensiveProduct = new Product() { ID = 5, Price = 20M };
            ProductRepository repository = new ProductRepository()
            {
                leastExpensiveProduct,
                mostExpensiveProduct
            };

            Assert.AreEqual(15M, repository.GetAverageProductPrice());

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<ProductRepository> _repository = test.CreateVariable<ProductRepository>();
            TestVariable<Product> _product1 = test.CreateVariable<Product>();
            TestVariable<Product> _product2 = test.CreateVariable<Product>();
            test.Arrange(_repository, Expr(() => new ProductRepository()));
            test.Arrange(_product1, Expr(() => new Product() { ID = 4, Price = 10M }));
            test.Arrange(_product2, Expr(() => new Product() { ID = 5, Price = 20M }));
            test.Act(Expr(_repository, _product1, (r, p) => r.Add(p)));
            test.Act(Expr(_repository, _product2, (r, p) => r.Add(p)));
            test.Assert.AreEqual(Const(15M), Expr(_repository, r => r.GetAverageProductPrice()));
            test.Execute();
        }

        [TestMethod("j. ProductRepository.GetProductsInCategory(string category) returns correctly"), TestCategory("Exercise 3D")]
        public void ProductRepositoryGetProductsInCategoryReturnsCorrectly()
        {
            Product product1 = new Product() { ID = 4, Category = "Food" };
            Product product2 = new Product() { ID = 5, Category = "Food" };
            Product product3 = new Product() { ID = 6, Category = "Electronics" };
            ProductRepository repository = new ProductRepository()
            {
                product1,
                product2,
                product3
            };

            Assert.IsTrue(repository.GetProductsInCategory("Electronics").SequenceEqual(new Product[] { product3 }));

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<ProductRepository> _repository = test.CreateVariable<ProductRepository>();
            TestVariable<Product> _product1 = test.CreateVariable<Product>();
            TestVariable<Product> _product2 = test.CreateVariable<Product>();
            TestVariable<Product> _product3 = test.CreateVariable<Product>();
            test.Arrange(_repository, Expr(() => new ProductRepository()));
            test.Arrange(_product1, Expr(() => new Product() { ID = 4, Category = "Food" }));
            test.Arrange(_product2, Expr(() => new Product() { ID = 5, Category = "Food" }));
            test.Arrange(_product3, Expr(() => new Product() { ID = 6, Category = "Electronics" }));
            test.Act(Expr(_repository, _product1, (r, p) => r.Add(p)));
            test.Act(Expr(_repository, _product2, (r, p) => r.Add(p)));
            test.Act(Expr(_repository, _product3, (r, p) => r.Add(p)));
            test.Assert.IsTrue(Expr(_repository, _product3, (r, p3) => r.GetProductsInCategory("Electronics").SequenceEqual(new[] { p3 })));
            test.Execute();
        }

        [TestMethod("k. ProductRepository.GetProductCategories() returns correctly"), TestCategory("Exercise 3D")]
        public void ProductRepositoryGetProductCategoriesReturnsCorrectly()
        {
            Product product1 = new Product() { ID = 4, Category = "Food" };
            Product product2 = new Product() { ID = 5, Category = "Electronics" };
            ProductRepository repository = new ProductRepository()
            {
                product1,
                product2
            };

            Assert.IsTrue(repository.GetProductCategories().SequenceEqual(new string[] { "Food", "Electronics" }));

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<ProductRepository> _repository = test.CreateVariable<ProductRepository>();
            TestVariable<Product> _product1 = test.CreateVariable<Product>();
            TestVariable<Product> _product2 = test.CreateVariable<Product>();
            test.Arrange(_repository, Expr(() => new ProductRepository()));
            test.Arrange(_product1, Expr(() => new Product() { ID = 4, Category = "Food" }));
            test.Arrange(_product2, Expr(() => new Product() { ID = 5, Category = "Electronics" }));
            test.Act(Expr(_repository, _product1, (r, p) => r.Add(p)));
            test.Act(Expr(_repository, _product2, (r, p) => r.Add(p)));
            test.Assert.IsTrue(Expr(_repository, _product1, _product2, (r, p1, p2) => r.GetProductCategories().SequenceEqual(new[] { "Food", "Electronics" })));
            test.Execute();
        }
        #endregion    
    }
}
