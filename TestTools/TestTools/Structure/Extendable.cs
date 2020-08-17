﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using TestTools.Structure;
using TestTools.Structure.Generic;

namespace TestTools.Helpers
{
    public static class Extendable
    {
        public static FieldElement Field(IExtendable instance, string fieldName, Type fieldType, FieldOptions options)
        {
            FieldInfo fieldInfo = ReflectionHelper.GetFieldInfo(instance.Type, fieldName, fieldType, options, isStatic: false);
            return new FieldElement(fieldInfo) { PreviousElement = instance };
        }
        public static FieldStaticElement StaticField(IExtendable instance, string fieldName, Type fieldType, FieldOptions options)
        {
            FieldInfo fieldInfo = ReflectionHelper.GetFieldInfo(instance.Type, fieldName, fieldType, options, isStatic: true);
            return new FieldStaticElement(fieldInfo) { PreviousElement = instance };
        }

        public static PropertyElement Property(IExtendable instance, string propertyName, Type propertyType, AccessorOptions get, AccessorOptions set)
        {
            PropertyInfo propertyInfo = ReflectionHelper.GetPropertyInfo(instance.Type, propertyName, propertyType, get, set, isStatic: false);
            return new PropertyElement(propertyInfo) { PreviousElement = instance };
        }
        public static PropertyStaticElement StaticProperty(IExtendable instance, string propertyName, Type propertyType, AccessorOptions get, AccessorOptions set)
        {
            PropertyInfo propertyInfo = ReflectionHelper.GetPropertyInfo(instance.Type, propertyName, propertyType, get, set, isStatic: true);
            return new PropertyStaticElement(propertyInfo) { PreviousElement = instance };
        }
        
        public static ActionMethodElement ActionMethod(IExtendable instance, string methodName, Type[] parameterTypes, MethodOptions options)
        {
            MethodInfo methodInfo = ReflectionHelper.GetMethodInfo(instance.Type, methodName, typeof(void), parameterTypes, options, isStatic: false);
            return new ActionMethodElement(methodInfo) { PreviousElement = instance };
        }
        public static ActionMethodStaticElement StaticActionMethod(IExtendable instance, string methodName, Type[] parameterTypes, MethodOptions options)
        {
            MethodInfo methodInfo = ReflectionHelper.GetMethodInfo(instance.Type, methodName, typeof(void), parameterTypes, options, isStatic: true);
            return new ActionMethodStaticElement(methodInfo) { PreviousElement = instance };
        }

        public static FuncMethodElement FuncMethod(IExtendable instance, string methodName, Type returnType, Type[] parameterTypes, MethodOptions options)
        {
            if (returnType == typeof(void))
                throw new ArgumentException("INTERNAL: FuncMethod is not intended for void methods. Use ActionMethod instead");

            MethodInfo methodInfo = ReflectionHelper.GetMethodInfo(instance.Type, methodName, returnType, parameterTypes, options, isStatic: false);
            return new FuncMethodElement(methodInfo) { PreviousElement = instance };
        }
        public static FuncMethodStaticElement StaticFuncMethod(IExtendable instance, string methodName, Type returnType, Type[] parameterTypes, MethodOptions options)
        {
            if (returnType == typeof(void))
                throw new ArgumentException("INTERNAL: StaticFuncMethod is not intended for void return type. Use StaticActionMethod instead");

            MethodInfo methodInfo = ReflectionHelper.GetMethodInfo(instance.Type, methodName, returnType, parameterTypes, options, isStatic: true);
            return new FuncMethodStaticElement(methodInfo) { PreviousElement = instance };
        }
    }
}
