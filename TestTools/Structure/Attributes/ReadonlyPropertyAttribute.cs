using System;
using System.Collections.Generic;
using System.Text;
using TestTools.Helpers;
using TestTools.TypeSystem;

namespace TestTools.Structure.Attributes
{
    public class ReadonlyPropertyAttribute : Attribute, IMemberVerifier
    {
        public VerifierServiceBase Verifier { get; set; }

        public IStructureService Service { get; set; }

        public MemberVerificationAspect[] Aspects => new[] {  
            MemberVerificationAspect.MemberType,
            MemberVerificationAspect.PropertyGetAccessLevel 
        };

        public void Verify(MemberDescription originalMember, MemberDescription translatedMember)
        {
            Verifier.VerifyMemberType(translatedMember, new[] { MemberTypes.Property });

            var originalProperty = (PropertyDescription)originalMember;
            var translatedProperty = (PropertyDescription)translatedMember;
            Verifier.VerifyIsReadonly(translatedProperty, DescriptionHelper.GetAccessLevel(originalProperty.GetMethod));
        }
    }
}
