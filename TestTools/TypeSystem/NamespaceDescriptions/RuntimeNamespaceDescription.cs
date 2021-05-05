using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TestTools.TypeSystem
{
    public class RuntimeNamespaceDescription : NamespaceDescription
    {
        public RuntimeNamespaceDescription(string @namespace)
        {
            Name = @namespace;
        }

        public override NamespaceDescription ContainingNamespace
        {
            get
            {
                if (!Name.Contains("."))
                    throw new NotImplementedException("Global namespace is not yet supported");

                string baseNamespace = Name.Substring(0, Name.LastIndexOf(".") - 1);
                return new RuntimeNamespaceDescription(baseNamespace);
            }
        }

        public override string Name { get; }

        public override NamespaceDescription[] GetNamespaces()
        {
            var namespaces = GetTypes().Select(t => t.Namespace).Distinct();
            var subNamespaces = namespaces.Where(t => t.StartsWith(Name));

            return namespaces.Select(t => new RuntimeNamespaceDescription(t)).ToArray();
        }

        public override TypeDescription[] GetTypes()
        {
            var output = new List<TypeDescription>();

            // The try-catch block is a little hack for accessing as many assemblies as possible,
            // because some assemblies always fail to load, like Microsoft.CodeAnalysis assemblies.
            // This might be a result of an unresolved configuration issues in one of the projects.
            var assemblyEnumerator = AppDomain.CurrentDomain.GetAssemblies().Reverse().GetEnumerator();
            while (assemblyEnumerator.MoveNext())
            {
                try
                {
                    var assembly = assemblyEnumerator.Current;
                    var typesInNamespace = assembly.GetTypes().Where(t => t.Namespace == Name);
                    var typeDescriptions = typesInNamespace.Select(t => new RuntimeTypeDescription(t));
                    output.AddRange(typeDescriptions);
                }
                catch (ReflectionTypeLoadException) { }
            }
            return output.ToArray();
        }
    }
}
