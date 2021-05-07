using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestTools.Structure;
using TestTools.TypeSystem;

namespace TestTools.Helpers
{
    public static class MemberDescriptionExtensions
    {
        public static IMemberTranslator GetCustomTranslator(this MemberDescription memberInfo)
        {
            return memberInfo.GetCustomAttributes().OfType<IMemberTranslator>().FirstOrDefault();
        }

        public static IMemberVerifier GetCustomVerifier(this MemberDescription memberInfo, MemberVerificationAspect aspect)
        {
            return memberInfo.GetCustomAttributes().OfType<IMemberVerifier>().FirstOrDefault(ver => ver.Aspects.Contains(aspect));
        }
    }
}
