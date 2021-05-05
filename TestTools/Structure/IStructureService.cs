using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using TestTools.TypeSystem;

namespace TestTools.Structure
{
    public interface IStructureService
    {
        TypeDescription TranslateType(TypeDescription type);

        MemberDescription TranslateMember(MemberDescription memberInfo);

        void VerifyType(TypeDescription original, ITypeVerifier[] verifiers);

        void VerifyMember(MemberDescription original, IMemberVerifier[] verifiers);
    }
}
