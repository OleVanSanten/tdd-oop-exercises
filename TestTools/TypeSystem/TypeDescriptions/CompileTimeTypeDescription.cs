using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestTools.Helpers;

namespace TestTools.TypeSystem
{
    public class CompileTimeTypeDescription : TypeDescription
    {
        public CompileTimeTypeDescription(ITypeSymbol typeSymbol)
        {
            if (typeSymbol == null)
                throw new ArgumentNullException();

            TypeSymbol = typeSymbol;
        }

        public ITypeSymbol TypeSymbol { get; }

        public override TypeDescription BaseType => new CompileTimeTypeDescription(TypeSymbol.BaseType);

        public override string Name => TypeSymbol.Name;

        public override string Namespace
        {
            get
            {
                INamespaceSymbol namespaceSymbol = TypeSymbol.ContainingNamespace;

                if (namespaceSymbol == null)
                    throw new NotImplementedException("Types in global namespaces are not supported");

                return namespaceSymbol.Name;
            }
        }

        public override bool IsAbstract => TypeSymbol.IsAbstract;

        public override bool IsArray => TypeSymbol.TypeKind == TypeKind.Array;

        public override bool IsClass => TypeSymbol.TypeKind == TypeKind.Class;

        public override bool IsEnum => TypeSymbol.TypeKind == TypeKind.Enum;

        public override bool IsInterface => TypeSymbol.TypeKind == TypeKind.Interface;

        public override bool IsNotPublic => !IsPublic;

        public override bool IsPublic => TypeSymbol.DeclaredAccessibility == Accessibility.Public;

        public override bool IsSealed => TypeSymbol.IsSealed;

        public override ConstructorDescription[] GetConstructors()
        {
            var members = TypeSymbol.GetMembers();
            var output = new List<CompileTimeConstructorDescription>();

            for (int i = 0; i < members.Length; i++)
            {
                if (members[i] is IMethodSymbol method && method.MethodKind == MethodKind.Constructor)
                    output.Add(new CompileTimeConstructorDescription(method));
            }

            return output.ToArray();
        }

        // Please note, GetCustomAttributes might return fewer results than
        // GetCustomAttributeTypes, because attributes cannot be loaded from 
        // non .netstandard2.0 targeting assemblies
        public override Attribute[] GetCustomAttributes()
        {
            return TypeSymbol
               .GetAttributes()
               .Select(attributeData => attributeData.ConvertToAttribute())
               .Where(attribute => attribute != null)
               .ToArray();
        }

        public override TypeDescription[] GetCustomAttributeTypes()
        {
            var attributes = TypeSymbol.GetAttributes();
            var output = new List<TypeDescription>();

            for(int i = 0; i < attributes.Length; i++)
            {
                output.Add(new CompileTimeTypeDescription(attributes[i].AttributeClass));
            }

            return output.ToArray();
        }

        public override EventDescription[] GetEvents()
        {
            var members = TypeSymbol.GetMembers();
            var output = new List<CompileTimeEventDescription>();

            for (int i = 0; i < members.Length; i++)
            {
                if (members[i] is IEventSymbol @event)
                    output.Add(new CompileTimeEventDescription(@event));
            }

            return output.ToArray();
        }

        public override FieldDescription[] GetFields()
        {
            var members = TypeSymbol.GetMembers();
            var output = new List<CompileTimeFieldDescription>();

            for (int i = 0; i < members.Length; i++)
            {
                if (members[i] is IFieldSymbol field)
                    output.Add(new CompileTimeFieldDescription(field));
            }

            return output.ToArray();
        }

        public override TypeDescription[] GetInterfaces()
        {
            var interfaces = TypeSymbol.AllInterfaces;
            var output = new List<CompileTimeTypeDescription>();

            for (int i = 0; i < interfaces.Length; i++)
            {
                output.Add(new CompileTimeTypeDescription(interfaces[i]));
            }

            return output.ToArray();
        }

        public override MethodDescription[] GetMethods()
        {
            var members = TypeSymbol.GetMembers();
            var output = new List<CompileTimeMethodDescription>();

            for (int i = 0; i < members.Length; i++)
            {
                if (members[i] is IMethodSymbol method && method.MethodKind == MethodKind.Ordinary)
                    output.Add(new CompileTimeMethodDescription(method));
            }

            return output.ToArray();
        }

        public override TypeDescription[] GetNestedTypes()
        {
            var nestedTypes = TypeSymbol.GetTypeMembers();
            var output = new List<CompileTimeTypeDescription>();

            for(int i = 0; i < nestedTypes.Length; i++)
            {
                output.Add(new CompileTimeTypeDescription(nestedTypes[i]));
            }

            return output.ToArray();
        }

        public override PropertyDescription[] GetProperties()
        {
            var members = TypeSymbol.GetMembers();
            var output = new List<CompileTimePropertyDescription>();

            for(int i = 0; i < members.Length; i++)
            {
                if (members[i] is IPropertySymbol property)
                    output.Add(new CompileTimePropertyDescription(property));
            }

            return output.ToArray();
        }
    }
}
