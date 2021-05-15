using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TestTools.TypeSystem;

namespace TestTools_Tests.TypeSystem
{
    [TestClass]
    public class RuntimeTypeDescriptionTests
    {
        [TestMethod("GetElementType correctly returns for array type")]
        public void GetElementType_CorrectlyReturnsForArrayType()
        {
            var type = new RuntimeTypeDescription(typeof(int[]));
            var elementType = new RuntimeTypeDescription(typeof(int));

            Assert.AreEqual(elementType, type.GetElementType());
        }

        [TestMethod("GetElementType returns null for non-array type")]
        public void GetElementType_ReturnsNullForNonArrayType()
        {
            var type = new RuntimeTypeDescription(typeof(int));
            Assert.IsNull(type.GetElementType());
        }

        [TestMethod("IsArray returns false for non-array type")]
        public void IsArray_ReturnsFalseForNonArrayTypes()
        {
            var type = new RuntimeTypeDescription(typeof(int));
            Assert.IsFalse(type.IsArray);
        }

        [TestMethod("IsArray returns true for array type")]
        public void IsArray_ReturnsTrueForArrayTypes()
        {
            var type = new RuntimeTypeDescription(typeof(int[]));
            Assert.IsTrue(type.IsArray);
        }

        [TestMethod("MakeArrayType returns correctly")]
        public void MakeArrayType_ReturnsCorrectly()
        {
            var type = new RuntimeTypeDescription(typeof(int));
            var arrayType = new RuntimeTypeDescription(typeof(int[]));

            Assert.AreEqual(arrayType, type.MakeArrayType());
        }
    }
}
