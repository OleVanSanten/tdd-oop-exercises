using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
