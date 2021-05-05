using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestTools.TypeSystem
{
    public class CompileTimePropertyDescription : PropertyDescription
    {
        public CompileTimePropertyDescription(IPropertySymbol propertySymbol)
        {
            PropertySymbol = propertySymbol;
        }

        public IPropertySymbol PropertySymbol { get; }

        public override bool CanRead => PropertySymbol.GetMethod != null;

        public override bool CanWrite => PropertySymbol.SetMethod != null;

        public override TypeDescription DeclaringType => new CompileTimeTypeDescription(PropertySymbol.ContainingType);

        public override MethodDescription GetMethod => PropertySymbol.GetMethod != null ? new CompileTimeMethodDescription(PropertySymbol.GetMethod) : null;

        public override string Name => PropertySymbol.Name;

        public override TypeDescription PropertyType => new CompileTimeTypeDescription(PropertySymbol.Type);

        public override MethodDescription SetMethod => PropertySymbol.SetMethod != null ? new CompileTimeMethodDescription(PropertySymbol.SetMethod) : null;
    }
}
