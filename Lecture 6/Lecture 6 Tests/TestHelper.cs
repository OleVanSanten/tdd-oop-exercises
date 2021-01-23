﻿using System;
using System.Collections.Generic;
using System.Text;
using TestTools.Integrated;

namespace Lecture_6_Tests
{
    public static class TestHelper
    {
        public static TestFactory Factory { get; } = new TestFactory("Lecture_6")
        {
            DefaultConfiguration = new UnitTestConfiguration()
            {
                FloatPrecision = 0.001F,
                DecimalPrecision = 0.001M,
                DoublePrecision = 0.001
            }
        };
    }
}