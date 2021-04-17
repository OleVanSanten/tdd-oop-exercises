using System;
using System.Collections.Generic;
using System.Text;
using TestTools.Expressions;
using static TestTools.Expressions.TestExpression;

namespace TestTools.MSTest
{
    public static class UnitTestExtensions
    {
        public static void ThrowsExceptionOn<TException>(this UnitTest.AssertObject assertObject, TestExpression expression) where TException : Exception
        {
            assertObject.ThrowsException<TException>(Lambda(expression));
        }

        public static void ThrowsExceptionOn<TException, T>(this UnitTest.AssertObject assertObject, TestExpression<T> expression) where TException : Exception
        {
            ThrowsExceptionOn<TException>(assertObject, expression);
        }
    }
}