using Lecture_3_Solutions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestTools.Expressions;
using TestTools.MSTest;
using TestTools.Structure;
using static Lecture_3_Tests.TestHelper;
using static TestTools.Expressions.TestExpression;

namespace Lecture_3_Tests
{
    [TestClass]
    public class Exercise_5_Tests
    {
        #region Exercise 5A
        [TestMethod("a. BankAccount.Balance is public read-only decimal property"), TestCategory("Exercise 5A")]
        public void BankAccountBalanceIsDecimalProperty()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicReadonlyProperty<BankAccount, decimal>(b => b.Balance);
            test.Execute();
        }

        [TestMethod("b. BankAccount.BorrowingRate is public decimal property"), TestCategory("Exercise 5A")]
        public void BankAccountBorrowingRateIsPublicDecimalProperty() 
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicProperty<BankAccount, decimal>(b => b.BorrowingRate);
            test.Execute();
        }

        [TestMethod("c. BankAccount.SavingsRate is public decimal property"), TestCategory("Exercise 5A")]
        public void BankAccountSavingsRateIsPublicDecimalProperty() 
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicProperty<BankAccount, decimal>(b => b.SavingsRate);
            test.Execute();
        }

        [TestMethod("d. BankAccount.Balance is initalized as 0M"), TestCategory("Exercise 5A")]
        public void BalanceIsInitilizedAs0()
        {
            BankAccount account = new BankAccount();
            Assert.AreEqual(0M, account.Balance);

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<BankAccount> _account = test.CreateVariable<BankAccount>(nameof(_account));
            test.Arrange(_account, Expr(() => new BankAccount()));
            test.Assert.AreEqual(Const(0M), Expr(_account, a => a.Balance));
            test.Execute();
        }

        [TestMethod("e. BankAccount.BorrowingRate is initalized as 0.06M"), TestCategory("Exercise 5A")]
        public void BorrowingRateIsInitilizedAs0Point6()
        {
            BankAccount account = new BankAccount();
            Assert.AreEqual(0.06M, account.BorrowingRate);

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<BankAccount> _account = test.CreateVariable<BankAccount>(nameof(_account));
            test.Arrange(_account, Expr(() => new BankAccount()));
            test.Assert.AreEqual(Const(0.06M), Expr(_account, a => a.BorrowingRate));
            test.Execute();
        }

        [TestMethod("f. BankAccount.SavingsRate is initalized as 0.02M"), TestCategory("Exercise 5A")]
        public void SavingsRateIsInitilizedAs0Point2()
        {
            BankAccount account = new BankAccount();
            Assert.AreEqual(0.02M, account.SavingsRate);

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<BankAccount> _account = test.CreateVariable<BankAccount>(nameof(_account));
            test.Arrange(_account, Expr(() => new BankAccount()));
            test.Assert.AreEqual(Const(0.02M), Expr(_account, a => a.SavingsRate));
            test.Execute();
        }

        [TestMethod("g. BankAccount.BorrowingRate ignores assignment of 0.05M"), TestCategory("Exercise 5A")]
        public void BankAccountBorrowingRateIgnoresAssignmentOfFivePercent() 
        {
            BankAccount account = new BankAccount();
            
            account.BorrowingRate = 0.05M;

            Assert.AreEqual(0.06M, account.BorrowingRate);

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<BankAccount> _account = test.CreateVariable<BankAccount>(nameof(_account));
            test.Arrange(_account, Expr(() => new BankAccount()));
            test.Act(Expr(_account, a => a.SetBorrowingRate(0.05M)));
            test.Assert.AreEqual(Const(0.06M), Expr(_account, a => a.BorrowingRate));
            test.Execute();
        }

        [TestMethod("h. BankAccount.SavingsRate ignores assignment of 0.03M"), TestCategory("Exercise 5A")]
        public void BankAccountSavingsRateIgnoresAssignmentOfThreePercent()
        {
            BankAccount account = new BankAccount();

            account.SavingsRate = 0.03M;

            Assert.AreEqual(0.02M, account.SavingsRate);

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<BankAccount> _account = test.CreateVariable<BankAccount>(nameof(_account));
            test.Arrange(_account, Expr(() => new BankAccount()));
            test.Act(Expr(_account, a => a.SetSavingsRate(0.03M)));
            test.Assert.AreEqual(Const(0.02M), Expr(_account, a => a.SavingsRate));
            test.Execute();
        }
        #endregion

        #region Exercise 5B
        [TestMethod("a. BankAccount.Deposit(int amount) adds amount to Balance"), TestCategory("Exercise 5B")]
        public void BankAccountDepositAddsAmountToBalance()
        {
            BankAccount account = new BankAccount();

            account.Deposit(50M);

            Assert.AreEqual(50M, account.Balance);

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<BankAccount> _account = test.CreateVariable<BankAccount>(nameof(_account));
            test.Arrange(_account, Expr(() => new BankAccount()));
            test.Act(Expr(_account, a => a.Deposit(50M)));
            test.Assert.AreEqual(Const(50M), Expr(_account, a => a.Balance));
            test.Execute();
        }

        [TestMethod("b. BankAccount.Deposit(int amount) does not change balance on negative amount"), TestCategory("Exercise 5B")]
        public void BankAccountDepositDoesNotChangeBalanceOnNegativeAmount()
        {
            BankAccount account = new BankAccount();

            account.Deposit(-1M);

            Assert.AreEqual(0, account.Balance);

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<BankAccount> _account = test.CreateVariable<BankAccount>(nameof(_account));
            test.Arrange(_account, Expr(() => new BankAccount()));
            test.Act(Expr(_account, a => a.Deposit(-1M)));
            test.Assert.AreEqual(Const(0M), Expr(_account, a => a.Balance));
            test.Execute();
        }

        [TestMethod("c. BankAccount.Withdraw(int amount) subtracts amount of Balance"), TestCategory("Exercise 5B")]
        public void BankAccountWithdrawSubtractsAmountOfBalance()
        {
            BankAccount account = new BankAccount();

            account.Withdraw(50M);

            Assert.AreEqual(-50M, account.Balance);

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<BankAccount> _account = test.CreateVariable<BankAccount>(nameof(_account));
            test.Arrange(_account, Expr(() => new BankAccount()));
            test.Act(Expr(_account, a => a.Withdraw(50M)));
            test.Assert.AreEqual(Const(-50M), Expr(_account, a => a.Balance));
            test.Execute();
        }

        [TestMethod("d. BankAccount.Withdraw(int amount) does not change Balance on negative amount"), TestCategory("Exercise 5B")]
        public void BankAccountWithdrawDoesNotChangeBalanceOnNegativeAmount()
        {
            BankAccount account = new BankAccount();

            account.Deposit(-1M);

            Assert.AreEqual(0, account.Balance);

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<BankAccount> _account = test.CreateVariable<BankAccount>(nameof(_account));
            test.Arrange(_account, Expr(() => new BankAccount()));
            test.Act(Expr(_account, a => a.Withdraw(-1M)));
            test.Assert.AreEqual(Const(0M), Expr(_account, a => a.Balance));
            test.Execute();
        }
        #endregion

        #region Exercise 5C
        [TestMethod("a. BankAccount.AccrueOrChargeInterest() is a public method"), TestCategory("Exercise 5C")]
        public void BankAccountAccrueOrChargeInterestIsAPublicMethod()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicMethod<BankAccount>(b => b.AccrueOrChargeInterest());
            test.Execute();
        }

        [TestMethod("b. BankAccount.AccrueOrChargeInterest() charges BorrowingRate if Balance < 0"), TestCategory("Exercise 5C")]
        public void BankAccountAccrueOrChargeInterestChargesBorrowingRateIfBalanceIsNegative()
        {
            BankAccount account = new BankAccount() { BorrowingRate = 0.1M };
            account.Withdraw(50);

            account.AccrueOrChargeInterest();

            Assert.AreEqual(-45.0M, account.Balance);

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<BankAccount> _account = test.CreateVariable<BankAccount>();
            test.Arrange(_account, Expr(() => new BankAccount() { BorrowingRate = 0.1M }));
            test.Act(Expr(_account, a => a.Withdraw(50M)));
            test.Act(Expr(_account, a => a.AccrueOrChargeInterest()));
            test.Assert.AreEqual(Const(-45.0M), Expr(_account, a => a.Balance));
            test.Execute();
        }

        [TestMethod("c. BankAccount.AccrueOrChargeInterest() does not affect Balance if Balance = 0"), TestCategory("Exercise 5C")]
        public void BankAccountAccrueOrChargeInterestDoesNotAffectBalanceIfBalanceEqualsZero()
        {
            BankAccount account = new BankAccount();

            account.AccrueOrChargeInterest();

            Assert.AreEqual(0M, account.Balance);

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<BankAccount> _account = test.CreateVariable<BankAccount>();
            test.Arrange(_account, Expr(() => new BankAccount()));
            test.Act(Expr(_account, a => a.AccrueOrChargeInterest()));
            test.Assert.AreEqual(Const(0M), Expr(_account, a => a.Balance));
            test.Execute();
        }

        [TestMethod("d. BankAccount.AccrueOrChargeInterest() accrue SavingsRate if Balance > 0"), TestCategory("Exercise 5C")]
        public void BankAccountAccrueOrChargeInterestAccrueSavingsRateIfBalanceIsPositive()
        {
            BankAccount account = new BankAccount() { SavingsRate = 0.01M };
            account.Deposit(50);

            account.AccrueOrChargeInterest();

            Assert.AreEqual(50.5M, account.Balance);
             
            UnitTest test = Factory.CreateTest();
            TestVariable<BankAccount> _account = test.CreateVariable<BankAccount>();
            test.Arrange(_account, Expr(() => new BankAccount() { SavingsRate = 0.01M }));
            test.Act(Expr(_account, a => a.Deposit(50M)));
            test.Act(Expr(_account, a => a.AccrueOrChargeInterest()));
            test.Assert.AreEqual(Const(50.5M), Expr(_account, a => a.Balance));
            test.Execute();
        }
        #endregion
    }
}
