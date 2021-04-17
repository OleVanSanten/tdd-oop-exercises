using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TestTools.Structure;

namespace TestTools.Helpers
{
    public static class ReflectionHelper
    {
        public static AccessLevels GetAccessLevel(Type type)
        {
            return type.IsPublic ? AccessLevels.Public : AccessLevels.Private;
        }

        public static AccessLevels GetAccessLevel(MemberInfo memberInfo)
        {
            if (memberInfo is ConstructorInfo constructorInfo)
            {
                if (constructorInfo.IsPrivate)
                {
                    return constructorInfo.IsAssembly ? AccessLevels.InternalPrivate : AccessLevels.Private;
                }
                else if (constructorInfo.IsFamily)
                {
                    return constructorInfo.IsAssembly ? AccessLevels.InternalProtected : AccessLevels.Protected;
                }
                else return constructorInfo.IsAssembly ? AccessLevels.InternalPublic : AccessLevels.Public;
            }
            else if (memberInfo is EventInfo)
            {
                return AccessLevels.Public;
            }
            else if (memberInfo is FieldInfo fieldInfo)
            {
                if (fieldInfo.IsPrivate)
                {
                    return fieldInfo.IsAssembly ? AccessLevels.InternalPrivate : AccessLevels.Private;
                }
                else if (fieldInfo.IsFamily)
                {
                    return fieldInfo.IsAssembly ? AccessLevels.InternalProtected : AccessLevels.Protected;
                }
                else return fieldInfo.IsAssembly ? AccessLevels.InternalPublic : AccessLevels.Public;
            }
            else if (memberInfo is MethodInfo methodInfo)
            {
                if (methodInfo.IsPrivate)
                {
                    return methodInfo.IsAssembly ? AccessLevels.InternalPrivate : AccessLevels.Private;
                }
                else if (methodInfo.IsFamily)
                {
                    return methodInfo.IsAssembly ? AccessLevels.InternalProtected : AccessLevels.Protected;
                }
                else return methodInfo.IsAssembly ? AccessLevels.InternalPublic : AccessLevels.Public;
            }
            throw new NotImplementedException();
        }
    }
}
