using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestTools.TypeSystem
{
    // Unifying class for wrappers of System.Type and Microsoft.CodeAnalysis.ITypeSymbol 
    public abstract class TypeDescription
    {
        public abstract TypeDescription BaseType { get; }

        public abstract ConstructorDescription[] GetConstructors();

        public abstract EventDescription[] GetEvents();

        public abstract FieldDescription[] GetFields();

        public abstract TypeDescription[] GetInterfaces();

        public virtual MemberDescription[] GetMembers()
        {
            return Enumerable
                .Empty<MemberDescription>()
                .Union(GetEvents())
                .Union(GetFields())
                .Union(GetMethods())
                .Union(GetProperties())
                .ToArray();
        }

        public abstract MethodDescription[] GetMethods();

        public abstract TypeDescription[] GetNestedTypes();

        public abstract PropertyDescription[] GetProperties();

        public abstract string Name { get; }

        public abstract string Namespace { get; }

        public abstract bool IsAbstract { get; }

        public abstract bool IsArray { get; }

        public abstract bool IsClass { get; }

        public abstract bool IsEnum { get; }

        public abstract bool IsInterface { get; }

        public abstract bool IsNotPublic { get; }

        public abstract bool IsPublic { get; }

        public abstract bool IsSealed { get; }
    }
}
