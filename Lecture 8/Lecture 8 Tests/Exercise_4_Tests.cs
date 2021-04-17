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
    public class Exercise_4_Tests
    {
        #region Exercise 4B
        [TestMethod("a. ConsoleController.HandleInput(string input) is a public method"), TestCategory("Exercise 4B")]
        public void ConsoleViewRunIsAPublicMethod()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicMethod<ConsoleController, string>((c, s) => c.HandleInput(s));
            test.Execute();
        }

        [TestMethod("b. ConsoleController.HandleInput(\"Echo Hello world\") echos"), TestCategory("Exercise 4B")]
        public void ConsoleControllerHandleInputEchos()
        {
            // MSTest Extended
            ConsoleController controller = new ConsoleController();
            ConsoleAssert.WritesOut(() => controller.HandleInput("Echo Hello World"), "Hello World");

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<ConsoleController> _controller = test.CreateVariable<ConsoleController>();
            test.Arrange(_controller, Expr(() => new ConsoleController()));
            test.ConsoleAssert.WritesOut(
                Lambda(Expr(_controller, c => c.HandleInput("Echo Hello world"))),
                Const("Hello world"));
            test.Execute();
        }

        [TestMethod("c. ConsoleController.HandleInput(\"Reverse Hello world\") reverses"), TestCategory("Exercise 4B")]
        public void ConsoleControllerHandleInputReverses()
        {
            // MSTest Extended
            ConsoleController constroller = new ConsoleController();
            ConsoleAssert.WritesOut(() => constroller.HandleInput("Reverse Hello world"), "dlrow olleH");

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<ConsoleController> _controller = test.CreateVariable<ConsoleController>();
            test.Arrange(_controller, Expr(() => new ConsoleController()));
            test.ConsoleAssert.WritesOut(
                Lambda(Expr(_controller, c => c.HandleInput("Reverse Hello world"))),
                Const("dlrow olleH"));
            test.Execute();
        }

        [TestMethod("d. ConsoleController.HandleInput(\"Greet world\") greets"), TestCategory("Exercise 4B")]
        public void ConsoleControllerHandleInputGreets()
        {
            // MSTest Extended
            ConsoleController controller = new ConsoleController();
            ConsoleAssert.WritesOut(() => controller.HandleInput("Greet World"), "Hello World");

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<ConsoleController> _controller = test.CreateVariable<ConsoleController>();
            test.Arrange(_controller, Expr(() => new ConsoleController()));
            test.ConsoleAssert.WritesOut(
                Lambda(Expr(_controller, c => c.HandleInput("Greet World"))),
                Const("Hello World"));
            test.Execute();
        }

        [TestMethod("e. ConsoleController.HandleInput(\"NonExistentCommand Hello World\") errors"), TestCategory("Exercise 4B")]
        public void ConsoleControllerHandleInputErrors()
        {
            ConsoleController controller = new ConsoleController();
            ConsoleAssert.WritesOut(
                () => controller.HandleInput("NonExistentCommand Hello World"),
                "Command NonExistentCommand not found");

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<ConsoleController> _controller = test.CreateVariable<ConsoleController>();
            test.Arrange(_controller, Expr(() => new ConsoleController()));
            test.ConsoleAssert.WritesOut(
                Lambda(Expr(_controller, c => c.HandleInput("NonExistentCommand Hello World"))),
                Const("Command NonExistentCommand not found"));
            test.Execute();
        }

        [TestMethod("f. ConsoleController.HandleInput(\"Greet\") errors"), TestCategory("Exercise 4B")]
        public void ConsoleControllerHandleInputErrors2()
        {
            // MSTest Extended
            ConsoleController controller = new ConsoleController();
            ConsoleAssert.WritesOut(
                () => controller.HandleInput("Greet"),
                "Please provide a command argument");

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<ConsoleController> _controller = test.CreateVariable<ConsoleController>();
            test.Arrange(_controller, Expr(() => new ConsoleController()));
            test.ConsoleAssert.WritesOut(
               Lambda(Expr(_controller, c => c.HandleInput("Greet"))),
               Const("Please provide a command argument"));
            test.Execute();
        }

        [TestMethod("g. ConsoleController.HandleInput(\"\") errors"), TestCategory("Exercise 4B")]
        public void ConsoleControllerHandleInputErrors3()
        {
            // MSTest Extended
            ConsoleController controller = new ConsoleController();
            ConsoleAssert.WritesOut(
                () => controller.HandleInput(""),
                "Please provide a command");

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<ConsoleController> _controller = test.CreateVariable<ConsoleController>();
            test.Arrange(_controller, Expr(() => new ConsoleController()));
            test.ConsoleAssert.WritesOut(
                Lambda(Expr(_controller, c => c.HandleInput(""))),
                Const("Please provide a command"));
            test.Execute();
        }
        #endregion

        #region exercise 4C
        [TestMethod("a. ConsoleController.AddCommand(string name, Action<string> action) is a public method"), TestCategory("Exercise 4C")]
        public void ConsoleControllerAddCommand()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicMethod<ConsoleController, string, Action<string>>((c, s, a) => c.AddCommand(s, a));
            test.Execute();
        }

        [TestMethod("b. ConsoleController.AddCommand(\"SayGoodbye\", (s) => ...) adds command"), TestCategory("Exercise 4C")]
        public void ConsoleConstrollerAddCommandAddsCommand()
        {
            // MSTest Extended
            ConsoleController controller = new ConsoleController();

            controller.AddCommand("SayGoodbye", s => Console.WriteLine("Goodbye " + s));

            ConsoleAssert.WritesOut(
                () => controller.HandleInput("SayGoodbye World"),
                "Goodbye World");

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<ConsoleController> _controller = test.CreateVariable<ConsoleController>();
            test.Arrange(_controller, Expr(() => new ConsoleController()));
            test.Act(Expr(_controller, (c) => c.AddCommand("SayGoodbye", s => Console.WriteLine("Goodbye " + s))));
            test.ConsoleAssert.WritesOut(
                Lambda(Expr(_controller, c => c.HandleInput("SayGoodbye World"))),
                Const("Goodbye World"));
            test.Execute();
        }
        #endregion

        #region Exercise 4D
        public void ConsoleControllerRemoveCommand()
        {
            // TestTools Code
            StructureTest test = Factory.CreateStructureTest();
            test.AssertPublicMethod<ConsoleController, string>((c, s) => c.RemoveCommand(s));
            test.Execute();
        }

        [TestMethod("a. ConsoleController.RemoveCommand(\"SayGoodbye\") removes command again"), TestCategory("Exercise 4D")]
        public void ConsoleControllerRemoveCommandRemovesCommand()
        {
            // MSTest Extended
            ConsoleController controller = new ConsoleController();

            controller.AddCommand("SayGoodbye", s => Console.WriteLine("Goodbye " + s));
            controller.RemoveCommand("SayGoodbye");

            ConsoleAssert.WritesOut(
                () => controller.HandleInput("SayGoodbye World"),
                "Command SayGoodbye not found");

            // TestTools Code
            UnitTest test = Factory.CreateTest();
            TestVariable<ConsoleController> _controller = test.CreateVariable<ConsoleController>();
            test.Arrange(_controller, Expr(() => new ConsoleController()));
            test.Act(Expr(_controller, c => c.AddCommand("SayGoodbye", s => Console.WriteLine(s))));
            test.Act(Expr(_controller, c => c.RemoveCommand("SayGoodbye")));
            test.ConsoleAssert.WritesOut(
                Lambda(Expr(_controller, c => c.HandleInput("SayGoodbye World"))),
                Const("Command SayGoodbye not found"));
            test.Execute();
        }
        #endregion
    }
}
