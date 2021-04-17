using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace TestTools.Structure
{
    public class UnchangedPropertyTypeVerifier : MemberVerifier
    {
        public override MemberVerificationAspect[] Aspects => new[] { MemberVerificationAspect.PropertyType };

        public override void Verify(MemberInfo originalMember, MemberInfo translatedMember)
        {
            PropertyInfo originalProperty = originalMember as PropertyInfo;

            Verifier.VerifyMemberType(translatedMember, new MemberTypes[] { MemberTypes.Field, MemberTypes.Property });

            if (translatedMember is FieldInfo translatedField)
                Verifier.VerifyFieldType(translatedField, originalProperty.PropertyType);
            else if (translatedMember is PropertyInfo translatedProperty)
                Verifier.VerifyPropertyType(translatedProperty, originalProperty.PropertyType);
            else throw new NotImplementedException();
        }
    }
}
