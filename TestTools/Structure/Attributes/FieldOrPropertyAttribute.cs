using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace TestTools.Structure.Attributes
{
    public class FieldOrPropertyAttribute : Attribute, IMemberVerifier
    {
        public VerifierServiceBase Verifier { get; set; }

        public MemberVerificationAspect[] Aspects => new[] { MemberVerificationAspect.MemberType };

        public IStructureService Service { get; set; }

        public void Verify( MemberInfo originalMember, MemberInfo translatedMember)
        {
            Verifier.VerifyMemberType(translatedMember, new[] { MemberTypes.Field, MemberTypes.Property });
        }
    }
}
