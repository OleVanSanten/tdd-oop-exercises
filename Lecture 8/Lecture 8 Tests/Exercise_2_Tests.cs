using Lecture_8_Solutions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestTools.Expressions;
using TestTools.MSTest;
using TestTools.Structure;
using static Lecture_8_Tests.TestHelper;
using static TestTools.Expressions.TestExpression;

namespace Lecture_8_Tests
{
    [TestClass]
    public class Exercise_2_Tests
    {
        #region Exercise 2A
        [TestMethod("a. BankAccount.Balance is a public read-only Balance"), TestCategory("Exercise 2A")]
        public void BankAccountBalanceIsAPublicReadonlyBalance()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicReadonlyProperty<BankAccount, decimal>(b => b.Balance);
            test.Execute();
        }

        [TestMethod("b. BankAccount.Balance is initialized as 0M"), TestCategory("Exercise 2A")]
        public void BankBalanceIsInitializedAs0M()
        {
            BankAccount account = new BankAccount();
            Assert.AreEqual(account.Balance, 0M);

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<BankAccount> _account = test.CreateVariable<BankAccount>();
            test.Arrange(_account, Expr(() => new BankAccount()));
            test.Assert.AreEqual(Expr(_account, a => a.Balance), Const(0M));
            test.Execute();
        }
        #endregion

        #region Exercise 2B
        [TestMethod("a. BankAccount.LowBalanceThreshold is a public property"), TestCategory("Exercise 2B")]
        public void BankAccountLowBalanceThresholdsIsAPublicProperty()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicProperty<BankAccount, decimal>(b => b.LowBalanceThreshold);
            test.Execute();
        }

        [TestMethod("b. BankAccount.HighBalanceThreshold is a public property"), TestCategory("Exercise 2B")]
        public void BankAccountHighBalanceThresholdsIsAPublicProperty()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicProperty<BankAccount, decimal>(b => b.HighBalanceThreshold);
            test.Execute();
        }

        [TestMethod("c. BankAccount.LowBalanceThreshold assigned above HighBalanceThreshold throws ArgumentException"), TestCategory("Exercise 2B")]
        public void BankAccountLowBalanceThresholdBelowHighBalanceThresholdThrowsArgumentException()
        {
            BankAccount account = new BankAccount()
            {
                HighBalanceThreshold = 0
            };
            Assert.ThrowsException<ArgumentException>(() => account.LowBalanceThreshold = 1M);

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<BankAccount> _account = test.CreateVariable<BankAccount>();
            test.Arrange(_account, Expr(() => new BankAccount() { HighBalanceThreshold = 0 }));
            test.Assert.ThrowsExceptionOn<ArgumentException>(Expr(_account, a => a.SetLowBalanceThreshold(1M)));
            test.Execute();
        }

        [TestMethod("d. BankAccount.HighBalanceThreshold assigned below LowBalanceThreshold throws ArgumentException"), TestCategory("Exercise 2B")]
        public void BankAccountHighBalanceThresholdBelowLowBalanceThresholdThrowsArgumentException()
        {
            BankAccount account = new BankAccount()
            {
                LowBalanceThreshold = 0
            };
            Assert.ThrowsException<ArgumentException>(() => account.HighBalanceThreshold = -1M);

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<BankAccount> _account = test.CreateVariable<BankAccount>();
            test.Arrange(_account, Expr(() => new BankAccount() { LowBalanceThreshold = 0 }));
            test.Assert.ThrowsExceptionOn<ArgumentException>(Expr(_account, a => a.SetHighBalanceThreshold(-1M)));
            test.Execute();
        }
        #endregion

        #region exercise 2C
        [TestMethod("a. BankAccount.Deposit(decimal amount) is a public method"), TestCategory("Exercise 2C")]
        public void BankAccountDepositIsAPublicMehtod()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicMethod<BankAccount, decimal>((b, d) => b.Deposit(d));
            test.Execute();
        }

        [TestMethod("b. BankAccount.Withdraw(decimal amount) is a public method"), TestCategory("Exercise 2C")]
        public void BankAccountWithdrawIsAPublicMehtod()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicMethod<BankAccount, decimal>((b, d) => b.Withdraw(d));
            test.Execute();
        }

        [TestMethod("c. BankAccount.Deposit(50) adds 50 to Balance"), TestCategory("Exercise 2C")]
        public void BankAccountDepositAddsToBalance()
        {
            BankAccount account = new BankAccount();

            account.Deposit(50M);

            Assert.AreEqual(50M, account.Balance);

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<BankAccount> _account = test.CreateVariable<BankAccount>();
            test.Arrange(_account, Expr(() => new BankAccount()));
            test.Act(Expr(_account, a => a.Deposit(50M)));
            test.Assert.AreEqual(Const(50M), Expr(_account, a => a.Balance));
            test.Execute();
        }

        [TestMethod("d. BankAccount.Withdraw(50) takes 50 from Balance"), TestCategory("Exercise 2C")]
        public void BankAccountWithdrawTakesFromBalance()
        {
            BankAccount account = new BankAccount();

            account.Withdraw(50M);

            Assert.AreEqual(-50M, account.Balance);

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<BankAccount> _account = test.CreateVariable<BankAccount>();
            test.Arrange(_account, Expr(() => new BankAccount()));
            test.Act(Expr(_account, a => a.Withdraw(50M)));
            test.Assert.AreEqual(Const(-50M), Expr(_account, a => a.Balance));
            test.Execute();
        }
        #endregion

        #region Exercise 2D
        [TestMethod("a. BalanceChangedHandler is public delegate"), TestCategory("Exercise 2D")]
        public void BalanceChangeHandlerIsPublicDelegate()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicDelegate<BalanceChangeHandler, Action<decimal>>();
            test.Execute();
        }
        #endregion

        #region Exercise 2E
        [TestMethod("a. BankAccount.LowBalance is public event"), TestCategory("Exercise 2E")]
        public void BankAccountMinEventIsPublicEvent()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertEvent(
                typeof(BankAccount).GetEvent("LowBalance"),
                new MemberAccessLevelVerifier(AccessLevels.Public),
                new EventHandlerTypeVerifier(typeof(BalanceChangeHandler)));
            test.Execute();
        }

        [TestMethod("b. BankAccount.HighBalance is public event"), TestCategory("Exercise 2E")]
        public void BankAccountHighBalanceMinEvent()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertEvent(
                typeof(BankAccount).GetEvent("HighBalance"),
                new MemberAccessLevelVerifier(AccessLevels.Public),
                new EventHandlerTypeVerifier(typeof(BalanceChangeHandler)));
            test.Execute();
        }

        [TestMethod("c. BankAccount.Withdraw(decimal amount) emits LowBalance event if Balance goes below threshold"), TestCategory("Exercise 2E")]
        public void BankAccountWithdrawEmitsLowBalance()
        {
            bool isCalled = false;
            BankAccount account = new BankAccount()
            {
                LowBalanceThreshold = 0
            };
            account.LowBalance += (currentBalance) => isCalled = true;

            account.Withdraw(50);

            Assert.IsTrue(isCalled, "The BankAccout.LowBalance event was never emitted");

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<BankAccount> _account = test.CreateVariable<BankAccount>();
            test.Arrange(_account, Expr(() => new BankAccount() { LowBalanceThreshold = 0 }));
            test.DelegateAssert.IsInvoked(Lambda<BalanceChangeHandler>(handler => Expr(_account, a => a.AddLowBalance(handler))));
            test.Act(Expr(_account, a => a.Withdraw(50M)));
            test.Execute();
        }

        [TestMethod("d. BankAccount.Deposit(decimal amount) emits HighBalance event if Balance goes below threshold"), TestCategory("Exercise 2E")]
        public void BankAccountDepositEmitsHighBalance()
        {
            // MSTest Extended
            bool isCalled = false;
            BankAccount account = new BankAccount()
            {
                HighBalanceThreshold = 0
            };
            account.HighBalance += (currentBalance) => isCalled = true; 

            account.Deposit(50);

            Assert.IsTrue(isCalled, "The BankAccout.HighBalance event was never emitted");

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<BankAccount> _account = test.CreateVariable<BankAccount>();
            test.Arrange(_account, Expr(() => new BankAccount() { HighBalanceThreshold = 0 }));
            test.DelegateAssert.IsInvoked(Lambda<BalanceChangeHandler>(handler => Expr(_account, a => a.AddHighBalance(handler))));
            test.Act(Expr(_account, a => a.Deposit(50)));
            test.Execute();
        }
        #endregion
    }
}
