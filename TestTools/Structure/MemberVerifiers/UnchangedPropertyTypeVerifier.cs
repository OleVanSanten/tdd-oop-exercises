using System;
using System.Collections.Generic;
using System.Text;
using TestTools.TypeSystem;

namespace TestTools.Structure
{
    public class UnchangedPropertyTypeVerifier : MemberVerifier
    {
        public override MemberVerificationAspect[] Aspects => new[] { MemberVerificationAspect.PropertyType };

        public override void Verify(MemberDescription originalMember, MemberDescription translatedMember)
        {
            PropertyDescription originalProperty = originalMember as PropertyDescription;

            Verifier.VerifyMemberType(translatedMember, new MemberTypes[] { MemberTypes.Field, MemberTypes.Property });

            if (translatedMember is FieldDescription translatedField)
                Verifier.VerifyFieldType(translatedField, originalProperty.PropertyType);
            else if (translatedMember is PropertyDescription translatedProperty)
                Verifier.VerifyPropertyType(translatedProperty, originalProperty.PropertyType);
            else throw new NotImplementedException();
        }
    }
}
