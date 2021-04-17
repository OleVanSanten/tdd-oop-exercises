﻿using TestTools.MSTest;

namespace Lecture_8_Tests
{
    public static class TestHelper
    {
        static TestHelper()
        {
            // To force the inclusion of the Lecture 7 Exercises assembly
            // a class from this assembly is referenced 
            Lecture_8.Program.Main(new string[0]);
        }

        // public static TestFactory Factory { get; } = new TestFactory("Lecture_8_Solutions", "Lecture_8_Solutions");
        public static TestFactory Factory { get; } = new TestFactory("Lecture_8_Solutions", "Lecture_8");
    }
}
