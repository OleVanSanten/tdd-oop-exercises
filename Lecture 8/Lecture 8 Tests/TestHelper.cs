﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using TestTools;
using TestTools.Unit;

namespace Lecture_8_Tests
{
    public static class TestHelper
    {
        public static TestFactory Factory { get; } = new TestFactory("Lecture_8_Solutions", "Lecture_8")
        {
            DefaultConfiguration = new UnitTestConfiguration()
            {
            }
        };
    }
}
