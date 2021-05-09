using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestTools.TypeSystem
{
    public class CompileTimeNamespaceDescription : NamespaceDescription
    {
        INamespaceSymbol _namespaceSymbol;

        public CompileTimeNamespaceDescription(INamespaceSymbol namespaceSymbol)
        {
            _namespaceSymbol = namespaceSymbol;
        }

        public override NamespaceDescription ContainingNamespace => new CompileTimeNamespaceDescription(_namespaceSymbol.ContainingNamespace);

        public override string Name => GetFullName(_namespaceSymbol);

        public override NamespaceDescription[] GetNamespaces()
        {
            var namespaces = _namespaceSymbol.GetNamespaceMembers();
            return namespaces.Select(n => new CompileTimeNamespaceDescription(n)).ToArray();
        }

        public override TypeDescription[] GetTypes()
        {
            var types = _namespaceSymbol.GetTypeMembers();
            var output = new List<TypeDescription>();

            for(int i = 0; i < types.Length; i++)
            {
                output.Add(new CompileTimeTypeDescription(types[i]));
            }
            return output.ToArray();
        }

        private string GetFullName(INamespaceSymbol namespaceSymbol)
        {
            string output = namespaceSymbol.Name;
            INamespaceSymbol superNamespace = namespaceSymbol.ContainingNamespace;

            while (superNamespace != null)
            {
                if (superNamespace.IsGlobalNamespace)
                    break;

                output = superNamespace.Name + "." + output;
                superNamespace = superNamespace.ContainingNamespace;
            }

            return output;
        }
    }
}
