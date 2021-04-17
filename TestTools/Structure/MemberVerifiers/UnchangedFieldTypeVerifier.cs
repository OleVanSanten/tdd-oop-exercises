using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace TestTools.Structure
{
    public class UnchangedFieldTypeVerifier : MemberVerifier
    {
        public override MemberVerificationAspect[] Aspects => new[] { MemberVerificationAspect.FieldType };

        public override void Verify(MemberInfo originalMember, MemberInfo translatedMember)
        {
            FieldInfo translatedField = translatedMember as FieldInfo;

            Verifier.VerifyMemberType(translatedMember, new MemberTypes[] { MemberTypes.Field, MemberTypes.Property });

            if (originalMember is FieldInfo originalField)
                Verifier.VerifyFieldType(translatedField, originalField.FieldType);
            else if (originalMember is PropertyInfo originalProperty)
                Verifier.VerifyFieldType(translatedField, originalProperty.PropertyType);
            else throw new NotImplementedException();
        }
    }
}
