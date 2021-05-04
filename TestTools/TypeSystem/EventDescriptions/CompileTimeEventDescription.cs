using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestTools.TypeSystem
{
    public class CompileTimeEventDescription : EventDescription
    {
        IEventSymbol _eventSymbol;

        public CompileTimeEventDescription(IEventSymbol eventSymbol)
        {
            _eventSymbol = eventSymbol;
        }

        public override MethodDescription AddMethod => new CompileTimeMethodDescription(_eventSymbol.AddMethod);

        public override TypeDescription DeclaringType => new CompileTimeTypeDescription(_eventSymbol.ContainingType);

        public override TypeDescription EventHandlerType => new CompileTimeTypeDescription(_eventSymbol.Type);

        public override string Name => _eventSymbol.Name;

        public override MethodDescription RemoveMethod => new CompileTimeMethodDescription(_eventSymbol.RemoveMethod);
    }
}
