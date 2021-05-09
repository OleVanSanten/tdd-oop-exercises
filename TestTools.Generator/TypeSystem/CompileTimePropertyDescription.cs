using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestTools.Helpers;

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

        // Please note, GetCustomAttributes might return fewer results than
        // GetCustomAttributeTypes, because attributes cannot be loaded from 
        // non .netstandard2.0 targeting assemblies
        public override Attribute[] GetCustomAttributes()
        {
            return PropertySymbol
               .GetAttributes()
               .Select(attributeData => attributeData.ConvertToAttribute())
               .Where(attribute => attribute != null)
               .ToArray();
        }


        public override TypeDescription[] GetCustomAttributeTypes()
        {
            var attributes = PropertySymbol.GetAttributes();
            var output = new List<TypeDescription>();

            for (int i = 0; i < attributes.Length; i++)
            {
                output.Add(new CompileTimeTypeDescription(attributes[i].AttributeClass));
            }

            return output.ToArray();
        }
    }
}
