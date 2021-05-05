using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestTools.TypeSystem
{
    // A wrapper for Microsoft.CodeAnalysis.INameSpaceSymbol and AppDomain.GetAssemblies().SelectMany(t => t.GetTypes())
    public abstract class NamespaceDescription
    {
        public abstract NamespaceDescription ContainingNamespace { get; }

        public abstract string Name { get; }

        public virtual NamespaceDescription GetNamespace(string name)
        {
            // Handling global namespace
            if (string.IsNullOrEmpty(Name))
                return GetNamespaces().First(n => n.Name == name);

            return GetNamespaces().First(n => n.Name == $"{Name}.{name}");
        }

        public abstract NamespaceDescription[] GetNamespaces();

        public virtual TypeDescription GetType(string name)
        {
            return GetTypes().First(t => t.Name == name);
        }

        public abstract TypeDescription[] GetTypes();
    }
}
