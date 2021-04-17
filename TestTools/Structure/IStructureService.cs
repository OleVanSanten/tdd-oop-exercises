using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace TestTools.Structure
{
    public interface IStructureService
    {
        Type TranslateType(Type type);

        MemberInfo TranslateMember(MemberInfo memberInfo);

        void VerifyType(Type original, ITypeVerifier[] verifiers);

        void VerifyMember(MemberInfo original, IMemberVerifier[] verifiers);
    }
}
