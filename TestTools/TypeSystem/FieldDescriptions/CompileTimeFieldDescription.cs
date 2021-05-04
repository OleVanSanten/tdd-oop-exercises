using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestTools.TypeSystem
{
    public class CompileTimeFieldDescription : FieldDescription
    {
        IFieldSymbol _fieldSymbol;

        public CompileTimeFieldDescription(IFieldSymbol fieldSymbol)
        {
            _fieldSymbol = fieldSymbol;
        }

        public override TypeDescription DeclaringType => new CompileTimeTypeDescription(_fieldSymbol.ContainingType);

        public override TypeDescription FieldType => new CompileTimeTypeDescription(_fieldSymbol.Type);

        public override bool IsAssembly
        {
            get
            {
                bool isInternal = _fieldSymbol.DeclaredAccessibility == Accessibility.Internal;
                bool isProtectedAndInternal = _fieldSymbol.DeclaredAccessibility == Accessibility.ProtectedAndInternal;
                bool isProtectedOrInternal = _fieldSymbol.DeclaredAccessibility == Accessibility.ProtectedOrInternal;

                return isInternal || isProtectedAndInternal || isProtectedOrInternal;
            }
        }

        public override bool IsFamily
        {
            get
            {
                bool isProtected = _fieldSymbol.DeclaredAccessibility == Accessibility.Protected;
                bool isProtectedAndFriend = _fieldSymbol.DeclaredAccessibility == Accessibility.ProtectedAndFriend;

                return isProtected || isProtectedAndFriend;
            }
        }

        public override bool IsInitOnly => _fieldSymbol.IsReadOnly;

        public override bool IsPrivate
        {
            get
            {
                bool isNotApplicable = _fieldSymbol.DeclaredAccessibility == Accessibility.NotApplicable;
                bool isPrivate = _fieldSymbol.DeclaredAccessibility == Accessibility.Private;

                return isNotApplicable || isPrivate;
            }
        }

        public override bool IsPublic => _fieldSymbol.DeclaredAccessibility == Accessibility.Public;

        public override bool IsStatic => _fieldSymbol.IsStatic;

        public override string Name => _fieldSymbol.Name;
    }
}
