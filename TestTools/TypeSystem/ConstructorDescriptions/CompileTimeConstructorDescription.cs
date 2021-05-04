using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestTools.TypeSystem
{
    public class CompileTimeConstructorDescription : ConstructorDescription
    {
        IMethodSymbol _methodSymbol;

        public CompileTimeConstructorDescription(IMethodSymbol methodSymbol)
        {
            _methodSymbol = methodSymbol;
        }

        public override TypeDescription DeclaringType => new CompileTimeTypeDescription(_methodSymbol.ContainingType);

        public override bool IsAssembly
        {
            get
            {
                bool isInternal = _methodSymbol.DeclaredAccessibility == Accessibility.Internal;
                bool isProtectedAndInternal = _methodSymbol.DeclaredAccessibility == Accessibility.ProtectedAndInternal;
                bool isProtectedOrInternal = _methodSymbol.DeclaredAccessibility == Accessibility.ProtectedOrInternal;

                return isInternal || isProtectedAndInternal || isProtectedOrInternal;
            }
        }

        public override bool IsFamily
        {
            get
            {
                bool isProtected = _methodSymbol.DeclaredAccessibility == Accessibility.Protected;
                bool isProtectedAndFriend = _methodSymbol.DeclaredAccessibility == Accessibility.ProtectedAndFriend;

                return isProtected || isProtectedAndFriend;
            }
        }

        public override bool IsPrivate
        {
            get
            {
                bool isNotApplicable = _methodSymbol.DeclaredAccessibility == Accessibility.NotApplicable;
                bool isPrivate = _methodSymbol.DeclaredAccessibility == Accessibility.Private;

                return isNotApplicable || isPrivate;
            }
        }

        public override bool IsPublic => _methodSymbol.DeclaredAccessibility == Accessibility.Public;

        public override string Name => _methodSymbol.Name;

        public override bool IsAbstract => _methodSymbol.IsAbstract;

        public override bool IsStatic => _methodSymbol.IsStatic;

        public override bool IsVirtual => _methodSymbol.IsVirtual;

        public override ParameterDescription[] GetParameters()
        {
            var parameters = _methodSymbol.Parameters;
            var output = new List<CompileTimeParameterDescription>();

            for (int i = 0; i < parameters.Length; i++)
            {
                output.Add(new CompileTimeParameterDescription(parameters[i]));
            }

            return output.ToArray();
        }
    }
}
