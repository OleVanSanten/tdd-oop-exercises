﻿using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using TestTools.Helpers;


namespace TestTools.Structure.Generic
{
    public class ClassElement<TRoot> : ClassElement, IExtendable<TRoot>
    {
        public ClassElement(ClassOptions options = null) : base(typeof(TRoot), options)
        {
        }

        public FieldElement<TRoot, T> Field<T>(FieldOptions options) => Extendable.Field<TRoot, T>(this, options);
        public FieldElement<TRoot, T> StaticField<T>(FieldOptions options) 
        {
            options.FieldType = typeof(T);
            FieldInfo fieldInfo = ReflectionHelper.GetFieldInfo(typeof(TRoot), options, isStatic: true);
            return new FieldElement<TRoot, T>(fieldInfo) { PreviousElement = this };
        }

        public PropertyElement<TRoot, T> Property<T>(PropertyOptions options) => Extendable.Property<TRoot, T>(this, options);
        public PropertyElement<TRoot, T> StaticProperty<T>(PropertyOptions options) 
        {
            options.PropertyType = typeof(T);
            PropertyInfo propertyInfo = ReflectionHelper.GetPropertyInfo(typeof(TRoot), options, isStatic: true);
            return new PropertyElement<TRoot, T>(propertyInfo) { PreviousElement = this };
        }

        public new ActionMethodElement<TRoot> ActionMethod(MethodOptions options) => Extendable.ActionMethod<TRoot>(this, options);
        public ActionMethodElement<TRoot, T1> ActionMethod<T1>(MethodOptions options) => Extendable.ActionMethod<TRoot, T1>(this, options);
        public ActionMethodElement<TRoot, T1, T2> ActionMethod<T1, T2>(MethodOptions options) => Extendable.ActionMethod<TRoot, T1, T2>(this, options);
        public ActionMethodElement<TRoot, T1, T2, T3> ActionMethod<T1, T2, T3>(MethodOptions options) => Extendable.ActionMethod<TRoot, T1, T2, T3>(this, options);
        public ActionMethodElement<TRoot, T1, T2, T3, T4> ActionMethod<T1, T2, T3, T4>(MethodOptions options) => Extendable.ActionMethod<TRoot, T1, T2, T3, T4>(this, options);
        public ActionMethodElement<TRoot, T1, T2, T3, T4, T5> ActionMethod<T1, T2, T3, T4, T5>(MethodOptions options) => Extendable.ActionMethod<TRoot, T1, T2, T3, T4, T5>(this, options);

        public FuncMethodElement<TRoot, TResult> FuncMethod<TResult>(MethodOptions options) => Extendable.FuncMethod<TRoot, TResult>(this, options);
        public FuncMethodElement<TRoot, T1, TResult> FuncMethod<T1, TResult>(MethodOptions options) => Extendable.FuncMethod<TRoot, T1, TResult>(this, options);
        public FuncMethodElement<TRoot, T1, T2, TResult> FuncMethod<T1, T2, TResult>(MethodOptions options) => Extendable.FuncMethod<TRoot, T1, T2, TResult>(this, options);
        public FuncMethodElement<TRoot, T1, T2, T3, TResult> FuncMethod<T1, T2, T3, TResult>(MethodOptions options) => Extendable.FuncMethod<TRoot, T1, T2, T3, TResult>(this, options);
        public FuncMethodElement<TRoot, T1, T2, T3, T4, TResult> FuncMethod<T1, T2, T3, T4, TResult>(MethodOptions options) => Extendable.FuncMethod<TRoot, T1, T2, T3, T4, TResult>(this, options);
        public FuncMethodElement<TRoot, T1, T2, T3, T4, T5, TResult> FuncMethod<T1, T2, T3, T4, T5, TResult>(MethodOptions options) => Extendable.FuncMethod<TRoot, T1, T2, T3, T4, T5, TResult>(this, options);

        public new ActionMethodElement<TRoot> StaticActionMethod(MethodOptions options)
        {
            options.OverwriteTypes(typeof(void), new Type[0]);
            MethodInfo methodInfo = ReflectionHelper.GetMethodInfo(typeof(TRoot), options, isStatic: true);
            return new ActionMethodElement<TRoot>(methodInfo) { PreviousElement = this };
        }
        public ActionMethodElement<TRoot, T1> StaticActionMethod<T1>(MethodOptions options)
        {
            options.OverwriteTypes(typeof(void), new Type[] { typeof(T1) });
            MethodInfo methodInfo = ReflectionHelper.GetMethodInfo(typeof(TRoot), options, isStatic: true);
            return new ActionMethodElement<TRoot, T1>(methodInfo) { PreviousElement = this };
        }
        public ActionMethodElement<TRoot, T1, T2> StaticActionMethod<T1, T2>(MethodOptions options)
        {
            options.OverwriteTypes(typeof(void), new Type[] { typeof(T1), typeof(T2) });
            MethodInfo methodInfo = ReflectionHelper.GetMethodInfo(typeof(TRoot), options, isStatic: true);
            return new ActionMethodElement<TRoot, T1, T2>(methodInfo) { PreviousElement = this };
        }
        public ActionMethodElement<TRoot, T1, T2, T3> StaticActionMethod<T1, T2, T3>(MethodOptions options) 
        {
            options.OverwriteTypes(typeof(void), new Type[] { typeof(T1), typeof(T2), typeof(T3) });
            MethodInfo methodInfo = ReflectionHelper.GetMethodInfo(typeof(TRoot), options, isStatic: true);
            return new ActionMethodElement<TRoot, T1, T2, T3>(methodInfo) { PreviousElement = this };
        }
        public ActionMethodElement<TRoot, T1, T2, T3, T4> StaticActionMethod<T1, T2, T3, T4>(MethodOptions options)
        {
            options.OverwriteTypes(typeof(void), new Type[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4) });
            MethodInfo methodInfo = ReflectionHelper.GetMethodInfo(typeof(TRoot), options, isStatic: true);
            return new ActionMethodElement<TRoot, T1, T2, T3, T4>(methodInfo) { PreviousElement = this };
        }
        public ActionMethodElement<TRoot, T1, T2, T3, T4, T5> StaticActionMethod<T1, T2, T3, T4, T5>(MethodOptions options)
        {
            options.OverwriteTypes(typeof(void), new Type[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5) });
            MethodInfo methodInfo = ReflectionHelper.GetMethodInfo(typeof(TRoot), options, isStatic: true);
            return new ActionMethodElement<TRoot, T1, T2, T3, T4, T5>(methodInfo) { PreviousElement = this };
        }

        public FuncMethodElement<TRoot, TResult> StaticFuncMethod<TResult>(MethodOptions options) 
        {
            options.OverwriteTypes(typeof(TResult), new Type[0]);
            MethodInfo methodInfo = ReflectionHelper.GetMethodInfo(typeof(TRoot), options, isStatic: true);
            return new FuncMethodElement<TRoot, TResult>(methodInfo) { PreviousElement = this };
        }
        public FuncMethodElement<TRoot, T1, TResult> StaticFuncMethod<T1, TResult>(MethodOptions options)
        {
            options.OverwriteTypes(typeof(TResult), new Type[] { typeof(T1) });
            MethodInfo methodInfo = ReflectionHelper.GetMethodInfo(typeof(TRoot), options, isStatic: true);
            return new FuncMethodElement<TRoot, T1, TResult>(methodInfo) { PreviousElement = this };
        }
        public FuncMethodElement<TRoot, T1, T2, TResult> StaticFuncMethod<T1, T2, TResult>(MethodOptions options)
        {
            options.OverwriteTypes(typeof(TResult), new Type[] { typeof(T1), typeof(T2) });
            MethodInfo methodInfo = ReflectionHelper.GetMethodInfo(typeof(TRoot), options, isStatic: true);
            return new FuncMethodElement<TRoot, T1, T2, TResult>(methodInfo) { PreviousElement = this };
        }
        public FuncMethodElement<TRoot, T1, T2, T3, TResult> StaticFuncMethod<T1, T2, T3, TResult>(MethodOptions options)
        {
            options.OverwriteTypes(typeof(TResult), new Type[] { typeof(T1), typeof(T2), typeof(T3) });
            MethodInfo methodInfo = ReflectionHelper.GetMethodInfo(typeof(TRoot), options, isStatic: true);
            return new FuncMethodElement<TRoot, T1, T2, T3, TResult>(methodInfo) { PreviousElement = this };
        }
        public FuncMethodElement<TRoot, T1, T2, T3, T4, TResult> StaticFuncMethod<T1, T2, T3, T4, TResult>(MethodOptions options)
        {
            options.OverwriteTypes(typeof(TResult), new Type[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4) });
            MethodInfo methodInfo = ReflectionHelper.GetMethodInfo(typeof(TRoot), options, isStatic: true);
            return new FuncMethodElement<TRoot, T1, T2, T3, T4, TResult>(methodInfo) { PreviousElement = this };

        }
        public FuncMethodElement<TRoot, T1, T2, T3, T4, T5, TResult> StaticFuncMethod<T1, T2, T3, T4, T5, TResult>(MethodOptions options) 
        {
            options.OverwriteTypes(typeof(TResult), new Type[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5) });
            MethodInfo methodInfo = ReflectionHelper.GetMethodInfo(typeof(TRoot), options, isStatic: true);
            return new FuncMethodElement<TRoot, T1, T2, T3, T4, T5, TResult>(methodInfo) { PreviousElement = this };
        }

        public new ConstructorElement<TRoot> Constructor(ConstructorOptions options)
        {
            options.OverwriteTypes(new Type[0]);
            ConstructorInfo constructorInfo = ReflectionHelper.GetConstructorInfo(typeof(TRoot), options);
            return new ConstructorElement<TRoot>(constructorInfo);
        }
        public ConstructorElement<TRoot, T1> Constructor<T1>(ConstructorOptions options)
        {
            options.OverwriteTypes( new Type[] { typeof(T1) });
            ConstructorInfo constructorInfo = ReflectionHelper.GetConstructorInfo(Type, options);
            return new ConstructorElement<TRoot, T1>(constructorInfo);
        }
        public ConstructorElement<TRoot, T1, T2> Constructor<T1, T2>(ConstructorOptions options)
        {
            options.OverwriteTypes(new Type[] { typeof(T1), typeof(T2) });
            ConstructorInfo constructorInfo = ReflectionHelper.GetConstructorInfo(Type, options);
            return new ConstructorElement<TRoot, T1, T2>(constructorInfo);
        }
        public ConstructorElement<TRoot, T1, T2, T3> Constructor<T1, T2, T3>(ConstructorOptions options)
        {
            options.OverwriteTypes(new Type[] { typeof(T1), typeof(T2), typeof(T3) });
            ConstructorInfo constructorInfo = ReflectionHelper.GetConstructorInfo(Type, options);
            return new ConstructorElement<TRoot, T1, T2, T3>(constructorInfo);
        }
        public ConstructorElement<TRoot, T1, T2, T3, T4> Constructor<T1, T2, T3, T4>(ConstructorOptions options)
        {
            options.OverwriteTypes(new Type[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4) });
            ConstructorInfo constructorInfo = ReflectionHelper.GetConstructorInfo(Type, options);
            return new ConstructorElement<TRoot, T1, T2, T3, T4>(constructorInfo);
        }
        public ConstructorElement<TRoot, T1, T2, T3, T4, T5> Constructor<T1, T2, T3, T4, T5>(ConstructorOptions options)
        {
            options.OverwriteTypes(new Type[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5) });
            ConstructorInfo constructorInfo = ReflectionHelper.GetConstructorInfo(Type, options);
            return new ConstructorElement<TRoot, T1, T2, T3, T4, T5>(constructorInfo);
        }
    }
}
