using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestTools.TypeSystem
{
    public class CompileTimeEventDescription : EventDescription
    {
        public CompileTimeEventDescription(IEventSymbol eventSymbol)
        {
            EventSymbol = eventSymbol;
        }

        public IEventSymbol EventSymbol { get; }

        public override MethodDescription AddMethod => new CompileTimeMethodDescription(EventSymbol.AddMethod);

        public override TypeDescription DeclaringType => new CompileTimeTypeDescription(EventSymbol.ContainingType);

        public override TypeDescription EventHandlerType => new CompileTimeTypeDescription(EventSymbol.Type);

        public override string Name => EventSymbol.Name;

        public override MethodDescription RemoveMethod => new CompileTimeMethodDescription(EventSymbol.RemoveMethod);
    }
}
