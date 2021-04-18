using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TestTools.Structure
{
    public class CompilationTypeTranslator : TypeTranslator
    {
        private Compilation _compilation;

        public CompilationTypeTranslator(Compilation compilation)
        {
            _compilation = compilation;
        }

        public override Type Translate(Type type)
        {
            var assemblyPath = _compilation.References.First().Display;
            var assembly = GetAssembly(assemblyPath);
            var types = assembly.GetModules().SelectMany(m => m.GetTypes());

            var translatedType = types.FirstOrDefault(t => t.Name == type.Name);

            if (translatedType == null)
                Verifier.FailTypeNotFound(TargetNamespace, type);

            return translatedType;
        }

        private Assembly GetAssembly(string assemblyPath)
        {
            return Assembly.LoadFrom(assemblyPath);
        }
    }
}
