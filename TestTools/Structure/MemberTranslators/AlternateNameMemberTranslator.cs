using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using TestTools.Helpers;

namespace TestTools.Structure
{
    public class AlternateNameMemberTranslator : MemberTranslator
    {
        string[] _alternateNames;

        public AlternateNameMemberTranslator(string[] alternateNames)
        {
            _alternateNames = alternateNames;
        }

        public override MemberInfo Translate(MemberInfo member)
        {
            string[] names = _alternateNames.Union(new[] { member.Name }).ToArray();

            IEnumerable<MemberInfo> members = TargetType.GetAllMembers().Where(m => names.Contains(m.Name));

            if (!members.Any())
                Verifier.FailMemberNotFound(TargetType, names);

            // Multiple MethodBase members may have the same name and only differ in argument list
            if (member is MethodBase methodBase1)
            {
                foreach (var methodBase2 in members.OfType<MethodBase>())
                {
                    var parameterTypes1 = methodBase1.GetParameters().Select(p => p.ParameterType);
                    var parameterTypes2 = methodBase2.GetParameters().Select(p => p.ParameterType);

                    if (parameterTypes1.SequenceEqual(parameterTypes2))
                        return methodBase2;
                }
                throw new NotImplementedException();
            }
            return members.First();
        }
    }
}
