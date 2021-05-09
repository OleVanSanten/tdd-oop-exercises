using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestTools.Helpers;

namespace TestTools.TypeSystem
{
    public class CompileTimeFieldDescription : FieldDescription
    {
        public CompileTimeFieldDescription(IFieldSymbol fieldSymbol)
        {
            FieldSymbol = fieldSymbol;
        }

        public IFieldSymbol FieldSymbol { get; }

        public override TypeDescription DeclaringType => new CompileTimeTypeDescription(FieldSymbol.ContainingType);

        public override TypeDescription FieldType => new CompileTimeTypeDescription(FieldSymbol.Type);

        public override bool IsAssembly
        {
            get
            {
                bool isInternal = FieldSymbol.DeclaredAccessibility == Accessibility.Internal;
                bool isProtectedAndInternal = FieldSymbol.DeclaredAccessibility == Accessibility.ProtectedAndInternal;
                bool isProtectedOrInternal = FieldSymbol.DeclaredAccessibility == Accessibility.ProtectedOrInternal;

                return isInternal || isProtectedAndInternal || isProtectedOrInternal;
            }
        }

        public override bool IsFamily
        {
            get
            {
                bool isProtected = FieldSymbol.DeclaredAccessibility == Accessibility.Protected;
                bool isProtectedAndFriend = FieldSymbol.DeclaredAccessibility == Accessibility.ProtectedAndFriend;

                return isProtected || isProtectedAndFriend;
            }
        }

        public override bool IsInitOnly => FieldSymbol.IsReadOnly;

        public override bool IsPrivate
        {
            get
            {
                bool isNotApplicable = FieldSymbol.DeclaredAccessibility == Accessibility.NotApplicable;
                bool isPrivate = FieldSymbol.DeclaredAccessibility == Accessibility.Private;

                return isNotApplicable || isPrivate;
            }
        }

        public override bool IsPublic => FieldSymbol.DeclaredAccessibility == Accessibility.Public;

        public override bool IsStatic => FieldSymbol.IsStatic;

        public override string Name => FieldSymbol.Name;

        // Please note, GetCustomAttributes might return fewer results than
        // GetCustomAttributeTypes, because attributes cannot be loaded from 
        // non .netstandard2.0 targeting assemblies
        public override Attribute[] GetCustomAttributes()
        {
            return FieldSymbol
                .GetAttributes()
                .Select(attributeData => attributeData.ConvertToAttribute())
                .Where(attribute => attribute != null)
                .ToArray();
        }

        public override TypeDescription[] GetCustomAttributeTypes()
        {
            var attributes = FieldSymbol.GetAttributes();
            var output = new List<TypeDescription>();

            for (int i = 0; i < attributes.Length; i++)
            {
                output.Add(new CompileTimeTypeDescription(attributes[i].AttributeClass));
            }

            return output.ToArray();
        }
    }
}
