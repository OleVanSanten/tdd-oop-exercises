using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TestTools.TypeSystem;

namespace TestTools_Tests.TypeSystem
{
    [TestClass]
    public class CompileTimeTypeDescriptionTests
    {
        [TestMethod("GetElementType correctly returns for array type")]
        public void GetElementType_CorrectlyReturnsForArrayType()
        {
            var type = GetTypeDescription("int[]");
            var elementType = GetTypeDescription("int");

            Assert.AreEqual(elementType, type.GetElementType());
        }

        [TestMethod("GetElementType returns null for non-array type")]
        public void GetElementType_ReturnsNullForNonArrayType()
        {
            var type = GetTypeDescription("int");

            Assert.IsNull(type.GetElementType());
        }

        [TestMethod("IsArray returns false for non-array type")]
        public void IsArray_ReturnsFalseForNonArrayTypes()
        {
            var type = GetTypeDescription("int");

            Assert.IsFalse(type.IsArray);
        }

        [TestMethod("IsArray returns true for array type")]
        public void IsArray_ReturnsTrueForArrayTypes()
        {
            var type = GetTypeDescription("int[]");

            Assert.IsTrue(type.IsArray);
        }

        [TestMethod("MakeArrayType returns correctly")]
        public void MakeArrayType_ReturnsCorrectly()
        {
            var type = GetTypeDescription("int");
            var arrayType = GetTypeDescription("int[]");

            Assert.AreEqual(arrayType, type.MakeArrayType());
        }

        private TypeDescription GetTypeDescription(string typeName)
        {
            // Compile typeName as variable type
            var source = $"{typeName} variable";
            var compilation = CSharpCompilation.Create("compilation",
                new[] { CSharpSyntaxTree.ParseText(source) },
                new[] { MetadataReference.CreateFromFile(typeof(Binder).GetTypeInfo().Assembly.Location) },
                new CSharpCompilationOptions(OutputKind.ConsoleApplication));
           
            // Acquiring variable node
            var syntaxTree = compilation.SyntaxTrees.First();
            var root = syntaxTree.GetRoot();
            var node = root.DescendantNodes().OfType<VariableDeclarationSyntax>().First();

            // Acquiring type info
            var semanticModel = compilation.GetSemanticModel(syntaxTree);
            var typeSymbol = semanticModel.GetTypeInfo(node.Type).Type ?? throw new ArgumentException("Type is not predefined");
            
            return new CompileTimeTypeDescription(compilation, typeSymbol);
        }
    }
}