using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using TestTools.Helpers;

namespace TestTools.Structure
{
    public class PropertyIsReadonlyVerifier : MemberVerifier
    {
        public override MemberVerificationAspect[] Aspects => new[] {
            MemberVerificationAspect.MemberType,
            MemberVerificationAspect.PropertyGetAccessLevel,
            MemberVerificationAspect.PropertySetAccessLevel
        };

        public override void Verify(MemberInfo originalMember, MemberInfo translatedMember)
        {
            Verifier.VerifyMemberType(translatedMember, new[] { MemberTypes.Property });

            if (translatedMember is PropertyInfo propertyInfo)
            {
                Verifier.VerifyIsReadonly(propertyInfo, ReflectionHelper.GetAccessLevel(((PropertyInfo)originalMember).GetMethod));
            }
            else throw new NotImplementedException();
        }
    }
}
