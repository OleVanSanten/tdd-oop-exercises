using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestTools.TypeSystem
{
    public class CompileTimeMethodDescription : MethodDescription
    {
        public CompileTimeMethodDescription(IMethodSymbol methodSymbol)
        {
            MethodSymbol = methodSymbol;
        }

       public IMethodSymbol MethodSymbol { get; }

        public override TypeDescription DeclaringType => new CompileTimeTypeDescription(MethodSymbol.ContainingType);

        public override bool IsAbstract => MethodSymbol.IsAbstract;

        public override bool IsAssembly
        {
            get
            {
                bool isInternal = MethodSymbol.DeclaredAccessibility == Accessibility.Internal;
                bool isProtectedAndInternal = MethodSymbol.DeclaredAccessibility == Accessibility.ProtectedAndInternal;
                bool isProtectedOrInternal = MethodSymbol.DeclaredAccessibility == Accessibility.ProtectedOrInternal;

                return isInternal || isProtectedAndInternal || isProtectedOrInternal;
            }
        }

        public override bool IsFamily
        {
            get
            {
                bool isProtected = MethodSymbol.DeclaredAccessibility == Accessibility.Protected;
                bool isProtectedAndFriend = MethodSymbol.DeclaredAccessibility == Accessibility.ProtectedAndFriend;

                return isProtected || isProtectedAndFriend;
            }
        }

        public override bool IsPrivate
        {
            get
            {
                bool isNotApplicable = MethodSymbol.DeclaredAccessibility == Accessibility.NotApplicable;
                bool isPrivate = MethodSymbol.DeclaredAccessibility == Accessibility.Private;

                return isNotApplicable || isPrivate;
            }
        }

        public override bool IsPublic => MethodSymbol.DeclaredAccessibility == Accessibility.Public;

        public override bool IsStatic => MethodSymbol.IsStatic;

        public override bool IsVirtual => MethodSymbol.IsVirtual;

        public override string Name => MethodSymbol.Name;

        public override TypeDescription ReturnType => new CompileTimeTypeDescription(MethodSymbol.ReturnType);

        public override ParameterDescription[] GetParameters()
        {
            var parameters = MethodSymbol.Parameters;
            var output = new List<CompileTimeParameterDescription>();

            for(int i = 0; i < parameters.Length; i++)
            {
                output.Add(new CompileTimeParameterDescription(parameters[i]));
            }

            return output.ToArray();
        }
    }
}
