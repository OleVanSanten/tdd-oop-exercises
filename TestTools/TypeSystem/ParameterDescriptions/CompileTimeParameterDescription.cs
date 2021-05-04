using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestTools.TypeSystem
{
    public class CompileTimeParameterDescription : ParameterDescription
    {
        IParameterSymbol _parameterSymbol;

        public CompileTimeParameterDescription(IParameterSymbol parameterSymbol)
        {
            _parameterSymbol = parameterSymbol;
        }

        public override object DefaultValue => _parameterSymbol.ExplicitDefaultValue;

        public override bool HasDefaultValue => _parameterSymbol.HasExplicitDefaultValue;

        public override bool IsIn => _parameterSymbol.RefKind == RefKind.In;

        public override bool IsOut => _parameterSymbol.RefKind == RefKind.Out;

        public override string Name => _parameterSymbol.Name;

        public override TypeDescription ParameterType => new CompileTimeTypeDescription(_parameterSymbol.Type);
    }
}
