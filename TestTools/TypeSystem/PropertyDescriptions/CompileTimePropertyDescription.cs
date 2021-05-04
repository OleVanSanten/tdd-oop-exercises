using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestTools.TypeSystem
{
    public class CompileTimePropertyDescription : PropertyDescription
    {
        IPropertySymbol _propertySymbol;

        public CompileTimePropertyDescription(IPropertySymbol propertySymbol)
        {
            _propertySymbol = propertySymbol;
        }

        public override bool CanRead => _propertySymbol.GetMethod != null;

        public override bool CanWrite => _propertySymbol.SetMethod != null;

        public override TypeDescription DeclaringType => new CompileTimeTypeDescription(_propertySymbol.ContainingType);

        public override MethodDescription GetMethod => throw new NotImplementedException();

        public override string Name => _propertySymbol.Name;

        public override TypeDescription PropertyType => new CompileTimeTypeDescription(_propertySymbol.Type);

        public override MethodDescription SetMethod => throw new NotImplementedException();
    }
}
