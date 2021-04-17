using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using TestTools.Helpers;

namespace TestTools.Structure
{
    public class MemberIsReadonlyVerifier : MemberVerifier
    {
        public override MemberVerificationAspect[] Aspects => new[] {
            MemberVerificationAspect.MemberType,
            MemberVerificationAspect.FieldAccessLevel
        };

        public override void Verify(MemberInfo originalMember, MemberInfo translatedMember)
        {
            Verifier.VerifyMemberType(translatedMember, new[] { MemberTypes.Field, MemberTypes.Property });
            
            if (translatedMember is FieldInfo fieldInfo)
            {
                Verifier.VerifyIsInitOnly(fieldInfo, true);
            }
            else if(translatedMember is PropertyInfo propertyInfo)
            {
                Verifier.VerifyIsReadonly(propertyInfo, ReflectionHelper.GetAccessLevel(propertyInfo));
            } 
        }
    }
}
