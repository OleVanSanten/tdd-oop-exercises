using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TestTools.Helpers;

namespace TestTools.Structure
{
    public class SameNameMemberTranslator : MemberTranslator
    {
        public override MemberInfo Translate(MemberInfo member)
        {
            IEnumerable<MemberInfo> members = TargetType.GetAllMembers().Where(m => m.Name == member.Name);

            if (!members.Any())
                Verifier.FailMemberNotFound(TargetType, new[] { member.Name });

            // Multiple MethodBase members may have the same name and only differ in argument list
            if (member is MethodBase methodBase1)
            {
                foreach (var methodBase2 in members.OfType<MethodBase>())
                {
                    // TODO Try to do this somewhat simpler 
                    var methodBase3 = methodBase2;

                    if (methodBase2.IsGenericMethod)
                        methodBase3 = ((MethodInfo)methodBase2).MakeGenericMethod(methodBase1.GetGenericArguments());

                    var parameterTypes1 = methodBase1.GetParameters().Select(p => p.ParameterType);
                    var parameterTypes2 = methodBase3.GetParameters().Select(p => p.ParameterType);

                    if (parameterTypes1.SequenceEqual(parameterTypes2))
                        return methodBase2;
                }

                // Fail if no matching method is found
                if (methodBase1 is MethodInfo methodInfo)
                    Verifier.FailMethodNotFound(TargetType, methodInfo);
                if (methodBase1 is ConstructorInfo constructorInfo)
                    Verifier.FailConstructorNotFound(TargetType, constructorInfo);
            }
            return members.First();
        }
    }
}
