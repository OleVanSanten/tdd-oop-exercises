using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using TestTools.Helpers;
using TestTools.Structure;

namespace TestTools.Structure
{
    public class StructureTest
    {
        Action _executeAction;
        IStructureService _structureService;

        public StructureTest(IStructureService structureService) 
        {
            _structureService = structureService;
        }

        public void AssertType(Type type, params ITypeVerifier[] verifiers)
        {
            _executeAction += () => _structureService.VerifyType(type, verifiers);
        }

        public void AssertMember(MemberInfo memberInfo, params IMemberVerifier[] verifiers)
        {
            _executeAction += () => _structureService.VerifyMember(memberInfo, verifiers);
        }

        public void Execute()
        {
            _executeAction?.Invoke();
        }
    }
}
