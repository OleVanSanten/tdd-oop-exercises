using Lecture_8_Solutions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TestTools.Expressions;
using TestTools.MSTest;
using TestTools.Structure;
using static TestTools.Expressions.TestExpression;
using static Lecture_8_Tests.TestHelper;
using static TestTools.Helpers.StructureHelper;

namespace Lecture_8_Tests
{
    [TestClass]
    public class Exercise_3_Tests
    {
        #region Exercise 3A
        [TestMethod("a. ConsoleView.Run() is a public method"), TestCategory("Exercise 3A")]
        public void ConsoleViewRunIsAPublicMethod()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicMethod<ConsoleView>(v => v.Run());
            test.Execute();
        }

        [TestMethod("b. ConsoleView.Run() returns on empty line input"), TestCategory("Exercise 3A")]
        public void ConsoleViewRunReturnsOnEmptyLineInput()
        {
            // MSTest Extended
            ConsoleView view = new ConsoleView();

            ConsoleInputter.Clear();
            ConsoleInputter.WriteLine();
            view.Run();

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<ConsoleView> _view = test.CreateVariable<ConsoleView>();
            test.Arrange(_view, Expr(() => new ConsoleView()));
            test.Act(Expr(() => ConsoleInputter.Clear()));
            test.Act(Expr(() => ConsoleInputter.WriteLine()));
            test.Act(Expr(_view, v => v.Run()));
            test.Execute();
        }
        #endregion

        #region exercise 3B
        [TestMethod("a. InputHandler is public delegate"), TestCategory("Exercise 3B")]
        public void InputHandlerIsPublicDelegate()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicDelegate<InputHandler, Action<string>>();
            test.Execute();
        }
        #endregion

        #region Exercise 3C
        [TestMethod("a. ConsoleView.Input is public event"), TestCategory("Exercise 3C")]
        public void ConsoleViewInputIsPublicEvent()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertEvent(
                typeof(ConsoleView).GetEvent("Input"),
                new MemberAccessLevelVerifier(AccessLevels.Public),
                new EventHandlerTypeVerifier(typeof(InputHandler)));
            test.Execute();
        }

        [TestMethod("b. ConsoleView.Run emits Input on non-empty-line input"), TestCategory("Exercise 3C")]
        public void ConsoleViewRunEmitsInputOnNonEmptyLineInput()
        {
            bool isCalled = false;
            ConsoleView view = new ConsoleView();
            view.Input += (input) => isCalled = true;

            ConsoleInputter.Clear();
            ConsoleInputter.WriteLine("User input");
            ConsoleInputter.WriteLine();
            view.Run();

            Assert.IsTrue(isCalled, "The ConsoleView.Run event was never emitted");

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<ConsoleView> _view = test.CreateVariable<ConsoleView>();
            test.Arrange(_view, Expr(() => new ConsoleView()));
            test.DelegateAssert.IsInvoked(Lambda<InputHandler>(handler => Expr(_view, v => v.AddInput(handler))));
            test.Act(Expr(() => ConsoleInputter.Clear()));
            test.Act(Expr(() => ConsoleInputter.WriteLine("User input")));
            test.Act(Expr(() => ConsoleInputter.WriteLine()));
            test.Act(Expr(_view, v => v.Run()));
            test.Execute();
        }

        [TestMethod("c. ConsoleView.Run does not emit Input on empty-line input"), TestCategory("Exercise 3C")]
        public void ConsoleViewRunDoesNotEmitInputOnEmptyLineInput()
        {
            bool isCalled = false;
            ConsoleView view = new ConsoleView();
            view.Input += (input) => isCalled = true;

            ConsoleInputter.Clear();
            ConsoleInputter.WriteLine();
            view.Run();

            Assert.IsFalse(isCalled, "The ConsoleView.Run event was emitted");

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<ConsoleView> _view = test.CreateVariable<ConsoleView>();
            test.Arrange(_view, Expr(() => new ConsoleView()));
            test.DelegateAssert.IsNotInvoked(Lambda<InputHandler>(handler => Expr(_view, v => v.AddInput(handler))));
            test.Act(Expr(() => ConsoleInputter.Clear()));
            test.Act(Expr(() => ConsoleInputter.WriteLine()));
            test.Act(Expr(_view, v => v.Run()));
            test.Execute();
        }
        #endregion
    }
}
