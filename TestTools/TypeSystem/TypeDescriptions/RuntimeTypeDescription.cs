﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TestTools.TypeSystem
{
    public class RuntimeTypeDescription : TypeDescription
    {
        public RuntimeTypeDescription(Type type)
        {
            Type = type;
        }

        public Type Type { get; }

        public override TypeDescription BaseType => new RuntimeTypeDescription(Type.BaseType);

        public override string Name => Type.Name;

        public override bool IsAbstract => Type.IsAbstract;

        public override bool IsArray => Type.IsArray;

        public override bool IsClass => Type.IsClass;

        public override bool IsEnum => Type.IsEnum;

        public override bool IsInterface => Type.IsInterface;

        public override bool IsNotPublic => Type.IsNotPublic;

        public override bool IsPublic => Type.IsPublic;

        public override bool IsSealed => Type.IsSealed;

        public override string Namespace => Type.Namespace;

        public override ConstructorDescription[] GetConstructors()
        {
            var allConstructors = Type.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            return allConstructors.Select(c => new RuntimeConstructorDescription(c)).ToArray();
        }

        public override EventDescription[] GetEvents()
        {
            var allEvents = Type.GetEvents(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
            return allEvents.Select(e => new RuntimeEventDescription(e)).ToArray();
        }

        public override FieldDescription[] GetFields()
        {
            var allFields = Type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
            return allFields.Select(f => new RuntimeFieldDescription(f)).ToArray();
        }

        public override TypeDescription[] GetInterfaces()
        {
            return Type.GetInterfaces().Select(t => new RuntimeTypeDescription(t)).ToArray();
        }

        public override MethodDescription[] GetMethods()
        {
            var allMethods = Type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
            return allMethods.Select(m => new RuntimeMethodDescription(m)).ToArray();
        }

        public override TypeDescription[] GetNestedTypes()
        {
            var allNestedTypes = Type.GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
            return allNestedTypes.Select(t => new RuntimeTypeDescription(t)).ToArray();
        }

        public override PropertyDescription[] GetProperties()
        {
            var allProperties = Type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Instance);
            return allProperties.Select(p => new RuntimePropertyDescription(p)).ToArray();
        }
    }
}