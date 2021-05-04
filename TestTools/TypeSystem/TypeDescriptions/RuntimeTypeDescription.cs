using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TestTools.TypeSystem
{
    public class RuntimeTypeDescription : TypeDescription
    {
        Type _type;

        public RuntimeTypeDescription(Type type)
        {
            _type = type;
        }

        public override TypeDescription BaseType => new RuntimeTypeDescription(_type.BaseType);

        public override string Name => _type.Name;

        public override bool IsAbstract => _type.IsAbstract;

        public override bool IsArray => _type.IsArray;

        public override bool IsClass => _type.IsClass;

        public override bool IsEnum => _type.IsEnum;

        public override bool IsInterface => _type.IsInterface;

        public override bool IsNotPublic => _type.IsNotPublic;

        public override bool IsPublic => _type.IsPublic;

        public override bool IsSealed => _type.IsSealed;

        public override string Namespace => _type.Namespace;

        public override ConstructorDescription[] GetConstructors()
        {
            var allConstructors = _type.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            return allConstructors.Select(c => new RuntimeConstructorDescription(c)).ToArray();
        }

        public override EventDescription[] GetEvents()
        {
            var allEvents = _type.GetEvents(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
            return allEvents.Select(e => new RuntimeEventDescription(e)).ToArray();
        }

        public override FieldDescription[] GetFields()
        {
            var allFields = _type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
            return allFields.Select(f => new RuntimeFieldDescription(f)).ToArray();
        }

        public override TypeDescription[] GetInterfaces()
        {
            return _type.GetInterfaces().Select(t => new RuntimeTypeDescription(t)).ToArray();
        }

        public override MethodDescription[] GetMethods()
        {
            var allMethods = _type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
            return allMethods.Select(m => new RuntimeMethodDescription(m)).ToArray();
        }

        public override TypeDescription[] GetNestedTypes()
        {
            var allNestedTypes = _type.GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
            return allNestedTypes.Select(t => new RuntimeTypeDescription(t)).ToArray();
        }

        public override PropertyDescription[] GetProperties()
        {
            var allProperties = _type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Instance);
            return allProperties.Select(p => new RuntimePropertyDescription(p)).ToArray();
        }
    }
}
