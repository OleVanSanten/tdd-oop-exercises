using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;
using TestTools.Helpers;

namespace TestTools.Structure
{
    public abstract class VerifierServiceBase
    {
        // Fail is implemented in test-framework-specific projects, 
        // because the test-failed exceptions, like AssertFailedException,
        // are framework-dependent
        public abstract void Fail(string message);

        public virtual void VerifyAccessLevel(Type type, AccessLevels[] accessLevels)
        {
            if (accessLevels.Contains(ReflectionHelper.GetAccessLevel(type)))
                return;

            string message = string.Format(
                "{0} is not {1}",
                FormatHelper.FormatType(type),
                FormatHelper.FormatOrList(accessLevels.Select(FormatHelper.FormatAccessLevel)));

            Fail(message);
        }

        public virtual void FailMemberNotFound(Type targetType, string[] vs)
        {
            if (vs.Length == 1)
            {
                string message = string.Format(
                    "{0} does not contain member {1}",
                    FormatHelper.FormatType(targetType),
                    vs[0]);
                Fail(message);
            }
            else throw new NotImplementedException();
        }

        public virtual void FailMethodNotFound(Type targetType, MethodInfo methodInfo)
        {
            string message = string.Format(
                "{0} does not contain member {1}",
                FormatHelper.FormatType(targetType),
                FormatHelper.FormatMethod(methodInfo));
            Fail(message);
        }

        public virtual void FailConstructorNotFound(Type targetType, ConstructorInfo constructorInfo)
        {
            string message = string.Format(
                "{0} does not contain member {1}",
                FormatHelper.FormatType(targetType),
                FormatHelper.FormatConstructor(constructorInfo));
            Fail(message);
        }

        public virtual void FailTypeNotFound(string @namespace, string[] name)
        {
            if (name.Length == 1)
                Fail($"Namespace {@namespace} does not contain type {name[0]}");
            else throw new NotImplementedException();
        }

        public virtual void FailTypeNotFound(string @namespace, Type type)
        {
            string message = string.Format(
                "Namespace {0} does not contain the type {1}",
                @namespace,
                FormatHelper.FormatType(type));
            Fail(message);
        }

        public virtual void VerifyAccessLevel(ConstructorInfo constructorInfo, AccessLevels[] accessLevels)
        {
            if (accessLevels.Contains(ReflectionHelper.GetAccessLevel(constructorInfo)))
                return;

            string message = string.Format(
                "{0} is not {1}",
                FormatHelper.FormatConstructor(constructorInfo),
                FormatHelper.FormatOrList(accessLevels.Select(FormatHelper.FormatAccessLevel)));

            Fail(message);
        }

        public virtual void VerifyAccessLevel(EventInfo eventInfo, AccessLevels[] accessLevels, bool AddMethod = false, bool RemoveMethod = false)
        {
            string message;

            if (AddMethod && !accessLevels.Contains(ReflectionHelper.GetAccessLevel(eventInfo.AddMethod)))
            {
                message = string.Format(
                    "{0}.{1} add accessor is not {2}",
                    FormatHelper.FormatType(eventInfo.AddMethod.DeclaringType),
                    eventInfo.Name,
                    FormatHelper.FormatOrList(accessLevels.Select(FormatHelper.FormatAccessLevel)));

                Fail(message);
            }
            else if (RemoveMethod && !accessLevels.Contains(ReflectionHelper.GetAccessLevel(eventInfo.RemoveMethod)))
            {
                message = string.Format(
                    "{0}.{1} set accessor is not {2}",
                    FormatHelper.FormatType(eventInfo.RemoveMethod.DeclaringType),
                    eventInfo.Name,
                    FormatHelper.FormatOrList(accessLevels.Select(FormatHelper.FormatAccessLevel)));

                Fail(message);
            }
        }

        public virtual void VerifyAccessLevel(FieldInfo fieldInfo, AccessLevels[] accessLevels)
        {
            if (accessLevels.Contains(ReflectionHelper.GetAccessLevel(fieldInfo)))
                return;

            string message = string.Format(
                "{0}.{1} is not {2}",
                FormatHelper.FormatType(fieldInfo.DeclaringType),
                fieldInfo.Name,
                FormatHelper.FormatOrList(accessLevels.Select(FormatHelper.FormatAccessLevel)));

            Fail(message);
        }

        public virtual void VerifyAccessLevel(PropertyInfo propertyInfo, AccessLevels[] accessLevels, bool GetMethod = false, bool SetMethod = false)
        {
            string message;

            if (GetMethod && !accessLevels.Contains(ReflectionHelper.GetAccessLevel(propertyInfo.GetMethod)))
            {
                message = string.Format(
                    "{0}.{1} get accessor is not {2}",
                    FormatHelper.FormatType(propertyInfo.GetMethod.DeclaringType),
                    propertyInfo.Name,
                    FormatHelper.FormatOrList(accessLevels.Select(FormatHelper.FormatAccessLevel)));

                Fail(message);
            }
            else if (SetMethod && !accessLevels.Contains(ReflectionHelper.GetAccessLevel(propertyInfo.SetMethod)))
            {
                message = string.Format(
                    "{0}.{1} set accessor is not {2}",
                    FormatHelper.FormatType(propertyInfo.GetMethod.DeclaringType),
                    propertyInfo.Name,
                    FormatHelper.FormatOrList(accessLevels.Select(FormatHelper.FormatAccessLevel)));

                Fail(message);
            }
        }

        public virtual void VerifyAccessLevel(MethodInfo methodInfo, AccessLevels[] accessLevels)
        {
            if (accessLevels.Contains(ReflectionHelper.GetAccessLevel(methodInfo)))
                return;

            string message = string.Format(
                "{0}.{1} is not {2}",
                FormatHelper.FormatType(methodInfo.DeclaringType),
                FormatHelper.FormatMethod(methodInfo),
                FormatHelper.FormatOrList(accessLevels.Select(FormatHelper.FormatAccessLevel)));

            Fail(message);
        }

        public virtual void VerifyBaseType(Type type, Type baseType)
        {
            if (type.BaseType == baseType)
                return;

            string message = string.Format(
                "The baseclass of {0} is not {1}",
                FormatHelper.FormatType(type),
                FormatHelper.FormatType(baseType));

            Fail(message);
        }

        public virtual void VerifyDeclaringType(FieldInfo fieldInfo, Type declaringType)
        {
            if (fieldInfo.DeclaringType == declaringType)
                return;

            string message = string.Format(
                "{0} does not declare the field {1}",
                FormatHelper.FormatType(declaringType),
                fieldInfo.Name);

            Fail(message);
        }

        public virtual void VerifyDeclaringType(MethodInfo methodInfo, Type declaringType)
        {
            if (methodInfo.DeclaringType == declaringType)
                return;

            string message = string.Format(
                "{0} does not declare the method {1}",
                FormatHelper.FormatType(declaringType),
                FormatHelper.FormatMethod(methodInfo));

            Fail(message);
        }

        public virtual void VerifyDeclaringType(PropertyInfo propertyInfo, Type declaringType, bool GetMethod = false, bool SetMethod = false)
        {
            string message;

            if (GetMethod && propertyInfo.GetMethod.DeclaringType != declaringType)
            {
                message = string.Format(
                    "{0} does not declare property {1} get accessor",
                    FormatHelper.FormatType(declaringType),
                    propertyInfo.Name);

                Fail(message);
            }
            else if (SetMethod && propertyInfo.SetMethod.DeclaringType != declaringType)
            {
                message = string.Format(
                    "{0} does not declare property {1} set accessor",
                    FormatHelper.FormatType(declaringType),
                    propertyInfo.Name);

                Fail(message);
            }
        }

        public virtual void VerifyDelegateSignature(Type delegateType, MethodInfo methodInfo)
        {
            if (delegateType.IsDelegateOf(methodInfo))
                return;

            string message = string.Format(
                "{0} does not match signature {1}",
                FormatHelper.FormatType(delegateType),
                FormatHelper.FormatSignature(methodInfo.ReturnType, delegateType.Name, methodInfo.GetParameters()));

            Fail(message);
        }

        public virtual void VerifyEventHandlerType(EventInfo eventInfo, Type type)
        {
            if (eventInfo.EventHandlerType == type)
                return;

            string message = string.Format(
                "{0}.{1} is not of type {2}",
                FormatHelper.FormatType(eventInfo.DeclaringType),
                eventInfo.Name,
                FormatHelper.FormatType(type));

            Fail(message);
        }

        public virtual void VerifyFieldType(FieldInfo fieldInfo, Type type)
        {
            if (fieldInfo.FieldType == type)
                return;

            string message = string.Format(
                "{0}.{1} is not of type {2}",
                FormatHelper.FormatType(fieldInfo.DeclaringType),
                fieldInfo.Name,
                FormatHelper.FormatType(type));

            Fail(message);
        }

        public virtual void VerifyIsAbstract(Type type, bool isAbstract)
        {
            if (type.IsAbstract == isAbstract)
                return;

            string template = isAbstract ? "{0} must be abstract" : "{0} cannot be abstract";
            string message = string.Format(
                    template,
                    FormatHelper.FormatType(type));

            Fail(message);
        }

        public virtual void VerifyIsAbstract(MethodInfo methodInfo, bool isAbstract)
        {
            if (methodInfo.IsAbstract == isAbstract)
                return;

            string template = isAbstract ? "{0}.{1} must be abstract" : "{0}.{1} cannot be abstract";
            string message = string.Format(
                    template,
                    FormatHelper.FormatType(methodInfo.DeclaringType),
                    FormatHelper.FormatMethod(methodInfo));

            Fail(message);
        }

        public virtual void VerifyIsAbstract(PropertyInfo propertyInfo, bool isAbstract, bool GetMethod = false, bool SetMethod = false)
        {
            string template, message;

            if (GetMethod && propertyInfo.GetMethod.IsAbstract != isAbstract)
            {
                template = isAbstract ? "{0}.{1} get accessor must be abstract" : "{0}.{1} get accessor cannot be abstract";
                message = string.Format(
                        template,
                        FormatHelper.FormatType(propertyInfo.DeclaringType),
                        propertyInfo.Name);

                Fail(message);
            }
            else if (SetMethod && propertyInfo.SetMethod.IsAbstract != isAbstract)
            {
                template = isAbstract ? "{0}.{1} set accessor must be abstract" : "{0}.{1} set accessor cannot be abstract";
                message = string.Format(
                        template,
                        FormatHelper.FormatType(propertyInfo.DeclaringType),
                        propertyInfo.Name);

                Fail(message);
            }
        }

        public virtual void VerifyIsDelegate(Type type)
        {
            if (type.IsSubclassOf(typeof(Delegate)))
                return;

            string message = string.Format(
                "{0} is not a delegate type",
                FormatHelper.FormatType(type));

            Fail(message);
        }

        public virtual void VerifyIsHideBySig(MethodInfo methodInfo, bool isHideBySig)
        {
            if (methodInfo.IsVirtual == isHideBySig)
                return;

            string template = isHideBySig ? "{0}.{1} cannot be new" : "{0}.{1} must be new";
            string message = string.Format(
                template,
                FormatHelper.FormatType(methodInfo.DeclaringType),
                FormatHelper.FormatMethod(methodInfo));

            Fail(message);
        }

        public virtual void VerifyIsHideBySig(PropertyInfo propertyInfo, bool isHideBySig, bool GetMethod = false, bool SetMethod = false)
        {
            string template, message;

            if (GetMethod && propertyInfo.GetMethod.IsAbstract != isHideBySig)
            {
                template = isHideBySig ? "{0}.{1} get accessor cannot be new" : "{0}.{1} get accessor must be new";
                message = string.Format(
                        template,
                        FormatHelper.FormatType(propertyInfo.DeclaringType),
                        propertyInfo.Name);

                Fail(message);
            }
            else if (SetMethod && propertyInfo.SetMethod.IsAbstract != isHideBySig)
            {
                template = isHideBySig ? "{0}.{1} set accessor cannot be new" : "{0}.{1} set accessor must be new";
                message = string.Format(
                        template,
                        FormatHelper.FormatType(propertyInfo.DeclaringType),
                        propertyInfo.Name);

                Fail(message);
            }
        }

        public virtual void VerifyIsInitOnly(FieldInfo fieldInfo, bool isInitOnly)
        {
            if (fieldInfo.IsInitOnly == isInitOnly)
                return;

            string template = isInitOnly ? "{0}.{1} must be readonly" : "{0}.{1} cannot be readonly";
            string message = string.Format(
                template,
                FormatHelper.FormatType(fieldInfo.DeclaringType),
                fieldInfo.Name);

            Fail(message);
        }

        public virtual void VerifyIsInterface(Type type)
        {
            if (type.IsInterface)
                return;

            string message = string.Format(
                "{0} is not an interface type",
                FormatHelper.FormatType(type));

            Fail(message);
        }

        public virtual void VerifyIsStatic(Type type, bool isStatic)
        {
            if (type.IsStatic() == isStatic)
                return;

            string template = isStatic ? "{0} must be static" : "{0} cannot be static";
            string message = String.Format(
                template,
                FormatHelper.FormatType(type));

            Fail(message);
        }

        public virtual void VerifyIsStatic(FieldInfo fieldInfo, bool isStatic)
        {
            if (fieldInfo.IsStatic == isStatic)
                return;

            string template = isStatic ? "{0}.{1} is an instance member instead a static member" : "{0}.{1} is a static member instead an instance member";
            string message = string.Format(
                template,
                FormatHelper.FormatType(fieldInfo.DeclaringType),
                fieldInfo.Name);

            Fail(message);
        }

        public virtual void VerifyIsStatic(MethodInfo methodInfo, bool isStatic)
        {
            if (methodInfo.IsStatic == isStatic)
                return;

            string template = isStatic ? "{0}.{1} is an instance member instead a static member" : "{0}.{1} is a static member instead an instance member";
            string message = string.Format(
                    template,
                    FormatHelper.FormatType(methodInfo.DeclaringType),
                    FormatHelper.FormatMethod(methodInfo));

            Fail(message);
        }

        public virtual void VerifyIsStatic(PropertyInfo propertyInfo, bool isStatic)
        {
            if ((propertyInfo.GetMethod ?? propertyInfo.SetMethod).IsStatic == isStatic)
                return;

            string template = isStatic ? "{0}.{1} is an instance member instead a static member" : "{0}.{1} is a static member instead an instance member";
            string message = string.Format(
                template,
                FormatHelper.FormatType(propertyInfo.DeclaringType),
                propertyInfo.Name);

            Fail(message);
        }

        public virtual void VerifyIsSubclassOf(Type type, Type baseType)
        {
            if (baseType.IsClass)
            {
                if (type.IsSubclassOf(baseType))
                    return;

                string message = string.Format(
                        "{0} is not a subclass of {1}",
                        FormatHelper.FormatType(type),
                        FormatHelper.FormatType(baseType));

                Fail(message);
            }
            else if (baseType.IsInterface)
            {
                if (type.IsImplementationOf(baseType))
                    return;

                string message = string.Format(
                        "{0} is not an implementation of {1}",
                        FormatHelper.FormatType(type),
                        FormatHelper.FormatType(baseType));

                Fail(message);
            }
            else throw new NotImplementedException();
        }

        public virtual void VerifyIsVirtual(MethodInfo methodInfo, bool isVirtual)
        {
            if (methodInfo.IsVirtual == isVirtual)
                return;

            string template = isVirtual ? "{0}.{1} must be virtual" : "{0}.{1} cannot be virtual";
            string message = string.Format(
                template,
                FormatHelper.FormatType(methodInfo.DeclaringType),
                FormatHelper.FormatMethod(methodInfo));

            Fail(message);
        }

        public virtual void VerifyIsVirtual(PropertyInfo propertyInfo, bool isVirtual, bool GetMethod = false, bool SetMethod = false)
        {
            string template, message;

            if (GetMethod && propertyInfo.GetMethod.IsVirtual != isVirtual)
            {
                template = isVirtual ? "{0}.{1} get accessor must be virtual" : "{0}.{1} get accessor cannot be virtual";
                message = string.Format(
                    template,
                    FormatHelper.FormatType(propertyInfo.DeclaringType),
                    propertyInfo.Name);

                Fail(message);
            }
            else if (SetMethod && propertyInfo.SetMethod.IsVirtual != isVirtual)
            {
                template = isVirtual ? "{0}.{1} set accessor must be virtual" : "{0}.{1} set accessor cannot be virtual";
                message = string.Format(
                    template,
                    FormatHelper.FormatType(propertyInfo.DeclaringType),
                    propertyInfo.Name);

                Fail(message);
            }
        }

        public virtual void VerifyMemberType(MemberInfo memberInfo, MemberTypes[] memberTypes)
        {
            if (memberTypes.Contains(memberInfo.MemberType))
                return;

            string message = string.Format(
                "{0}.{1} is {2} instead of {3}",
                FormatHelper.FormatType(memberInfo.DeclaringType),
                memberInfo.Name,
                FormatHelper.FormatMemberType(memberInfo.MemberType),
                FormatHelper.FormatOrList(memberTypes.Select(FormatHelper.FormatMemberType)));

            Fail(message);
        }

        public virtual void VerifyPropertyType(PropertyInfo propertyInfo, Type type)
        {
            if (propertyInfo.PropertyType == type)
                return;

            string message = string.Format(
                "{0}.{1} is not of type {2}",
                FormatHelper.FormatType(propertyInfo.DeclaringType),
                propertyInfo.Name,
                FormatHelper.FormatType(type));

            Fail(message);
        }

        public virtual void VerifyCanRead(PropertyInfo propertyInfo, bool canRead)
        {
            if (propertyInfo.CanRead == canRead)
                return;

            string template = canRead ? "{0}.{1} must have a get accessor" : "{0}.{1} cannot have a get accessor";
            string message = string.Format(
                template,
                FormatHelper.FormatType(propertyInfo.DeclaringType),
                propertyInfo.Name);

            Fail(message);
        }

        public virtual void VerifyIsReadonly(PropertyInfo propertyInfo, AccessLevels accessLevel)
        {
            VerifyCanRead(propertyInfo, canRead: true);
            VerifyAccessLevel(propertyInfo, new[] { accessLevel }, GetMethod: true);

            if (!propertyInfo.CanWrite)
                return;

            AccessLevels setMethodAccessLevel = ReflectionHelper.GetAccessLevel(propertyInfo.SetMethod);

            if (accessLevel == AccessLevels.Protected)
            {
                if (setMethodAccessLevel != AccessLevels.Private)
                {
                    string message = string.Format(
                    "{0}.{1} should not have set accessor or the set accessor should be private",
                    FormatHelper.FormatType(propertyInfo.DeclaringType),
                    propertyInfo.Name);
                    Fail(message);
                }

            }
            else if (accessLevel == AccessLevels.Public)
            {
                if (setMethodAccessLevel != AccessLevels.Private && setMethodAccessLevel != AccessLevels.Protected)
                {
                    string message = string.Format(
                    "{0}.{1} should not have set accessor or the set accessor should be private or protected",
                    FormatHelper.FormatType(propertyInfo.DeclaringType),
                    propertyInfo.Name);
                    Fail(message);
                }
            }
            else throw new NotImplementedException();
        }

        public virtual void VerifyIsWriteonly(PropertyInfo propertyInfo, AccessLevels accessLevel)
        {
            VerifyCanWrite(propertyInfo, canWrite: true);
            VerifyAccessLevel(propertyInfo, new[] { accessLevel }, SetMethod: true);

            if (!propertyInfo.CanWrite)
                return;

            AccessLevels getMethodAccessLevel = ReflectionHelper.GetAccessLevel(propertyInfo.GetMethod);

            if (accessLevel == AccessLevels.Protected)
            {
                if (getMethodAccessLevel != AccessLevels.Private)
                {
                    string message = string.Format(
                    "{0}.{1} should not have get accessor or the get accessor should be private",
                    FormatHelper.FormatType(propertyInfo.DeclaringType),
                    propertyInfo.Name);
                    Fail(message);
                }

            }
            else if (accessLevel == AccessLevels.Public)
            {
                if (getMethodAccessLevel != AccessLevels.Private && getMethodAccessLevel != AccessLevels.Protected)
                {
                    string message = string.Format(
                    "{0}.{1} should not have get accessor or the get accessor should be private or protected",
                    FormatHelper.FormatType(propertyInfo.DeclaringType),
                    propertyInfo.Name);
                    Fail(message);
                }
            }
            else throw new NotImplementedException();
        }

        public virtual void VerifyCanWrite(PropertyInfo propertyInfo, bool canWrite)
        {
            if (propertyInfo.CanWrite == canWrite)
                return;

            string template = canWrite ? "{0}.{1} must have a set accessor" : "{0}.{1} cannot have a set accessor";
            string message = string.Format(
                template,
                FormatHelper.FormatType(propertyInfo.DeclaringType),
                propertyInfo.Name);

            Fail(message);
        }

        public virtual void VerifyTypeHasMember(Type targetType, string[] memberNames)
        {
            string message;
            MemberInfo[] memberInfos = targetType.GetAllMembers();

            if (memberInfos.Any(info => memberNames.Contains(info.Name)))
                return;

            if (memberNames.Length == 1)
            {
                message = string.Format(
                    "{0} does not contain member {1}",
                    FormatHelper.FormatType(targetType),
                    memberNames[0]);
            }
            else
            {
                message = string.Format(
                    "{0} does not contain member {1} (or alternatively {2})",
                    FormatHelper.FormatType(targetType),
                    memberNames[0],
                    FormatHelper.FormatAndList(memberNames.Skip(1))
                );
            }
            Fail(message);
        }
    }
}
