using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace TestTools.TypeSystem
{
    public class CompileTimeDescriptionFactory
    {
        public MemberDescription Create(ISymbol symbol)
        {
            if (symbol is IMethodSymbol methodSymbol)
            {
                switch (methodSymbol.MethodKind)
                {
                    case MethodKind.Ordinary:
                    case MethodKind.PropertyGet:
                    case MethodKind.PropertySet:
                        return new CompileTimeMethodDescription(methodSymbol);
                    case MethodKind.Constructor: 
                        return new CompileTimeConstructorDescription(methodSymbol);
                    default: 
                        throw new NotImplementedException();
                }
            }
            if (symbol is IFieldSymbol fieldSymbol)
            {
                return new CompileTimeFieldDescription(fieldSymbol);
            }
            if (symbol is IPropertySymbol propertySymbol)
            {
                return new CompileTimePropertyDescription(propertySymbol);
            }
            throw new NotImplementedException();
        }
    }
}
