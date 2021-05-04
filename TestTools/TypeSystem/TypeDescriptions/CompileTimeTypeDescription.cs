using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestTools.TypeSystem
{
    public class CompileTimeTypeDescription : TypeDescription
    {
        ITypeSymbol _typeSymbol;

        public CompileTimeTypeDescription(ITypeSymbol typeSymbol)
        {
            _typeSymbol = typeSymbol;
        }

        public override TypeDescription BaseType => new CompileTimeTypeDescription(_typeSymbol.BaseType);

        public override string Name => _typeSymbol.Name;

        public override string Namespace
        {
            get
            {
                INamespaceSymbol namespaceSymbol = _typeSymbol.ContainingNamespace;

                if (namespaceSymbol == null)
                    throw new NotImplementedException("Types in global namespaces are not supported");

                return namespaceSymbol.Name;
            }
        }

        public override bool IsAbstract => _typeSymbol.IsAbstract;

        public override bool IsArray => _typeSymbol.TypeKind == TypeKind.Array;

        public override bool IsClass => _typeSymbol.TypeKind == TypeKind.Class;

        public override bool IsEnum => _typeSymbol.TypeKind == TypeKind.Enum;

        public override bool IsInterface => _typeSymbol.TypeKind == TypeKind.Interface;

        public override bool IsNotPublic => throw new NotImplementedException();

        public override bool IsPublic => throw new NotImplementedException();

        public override bool IsSealed => _typeSymbol.IsSealed;

        public override ConstructorDescription[] GetConstructors()
        {
            var members = _typeSymbol.GetMembers();
            var output = new List<CompileTimeConstructorDescription>();

            for (int i = 0; i < members.Length; i++)
            {
                if (members[i] is IMethodSymbol method && method.MethodKind == MethodKind.Constructor)
                    output.Add(new CompileTimeConstructorDescription(method));
            }

            return output.ToArray();
        }

        public override EventDescription[] GetEvents()
        {
            var members = _typeSymbol.GetMembers();
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
            var members = _typeSymbol.GetMembers();
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
            var interfaces = _typeSymbol.AllInterfaces;
            var output = new List<CompileTimeTypeDescription>();

            for (int i = 0; i < interfaces.Length; i++)
            {
                output.Add(new CompileTimeTypeDescription(interfaces[i]));
            }

            return output.ToArray();
        }

        public override MethodDescription[] GetMethods()
        {
            var members = _typeSymbol.GetMembers();
            var output = new List<CompileTimeMethodDescription>();

            for (int i = 0; i < members.Length; i++)
            {
                if (members[i] is IMethodSymbol method && method.MethodKind == MethodKind.DeclareMethod)
                    output.Add(new CompileTimeMethodDescription(method));
            }

            return output.ToArray();
        }

        public override TypeDescription[] GetNestedTypes()
        {
            var nestedTypes = _typeSymbol.GetTypeMembers();
            var output = new List<CompileTimeTypeDescription>();

            for(int i = 0; i < nestedTypes.Length; i++)
            {
                output.Add(new CompileTimeTypeDescription(nestedTypes[i]));
            }

            return output.ToArray();
        }

        public override PropertyDescription[] GetProperties()
        {
            var members = _typeSymbol.GetMembers();
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
