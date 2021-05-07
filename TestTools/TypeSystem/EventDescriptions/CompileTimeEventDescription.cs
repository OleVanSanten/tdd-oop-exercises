using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestTools.Helpers;

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

        // Please note, GetCustomAttributes might return fewer results than
        // GetCustomAttributeTypes, because attributes cannot be loaded from 
        // non .netstandard2.0 targeting assemblies
        public override Attribute[] GetCustomAttributes()
        {
            return EventSymbol
                .GetAttributes()
                .Select(attributeData => attributeData.ConvertToAttribute())
                .Where(attribute => attribute != null)
                .ToArray();
        }

        public override TypeDescription[] GetCustomAttributeTypes()
        {
            var attributes = EventSymbol.GetAttributes();
            var output = new List<TypeDescription>();

            for (int i = 0; i < attributes.Length; i++)
            {
                output.Add(new CompileTimeTypeDescription(attributes[i].AttributeClass));
            }

            return output.ToArray();
        }
    }
}
