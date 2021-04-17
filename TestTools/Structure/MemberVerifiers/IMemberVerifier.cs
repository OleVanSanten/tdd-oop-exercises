using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace TestTools.Structure
{
    public interface IMemberVerifier
    {
        MemberVerificationAspect[] Aspects { get; }
        
        VerifierServiceBase Verifier { get; set; }

        IStructureService Service { get; set; }

        void Verify(MemberInfo originalMember, MemberInfo translatedMember);
    }
}
