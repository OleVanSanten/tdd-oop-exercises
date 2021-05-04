using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TestTools.TypeSystem
{
    public class RuntimeMethodDescription : MethodDescription
    {
        MethodInfo _methodInfo;

        public RuntimeMethodDescription(MethodInfo methodInfo)
        {
            _methodInfo = methodInfo;
        }

        public override TypeDescription DeclaringType => new RuntimeTypeDescription(_methodInfo.DeclaringType);

        public override bool IsAbstract => _methodInfo.IsAbstract;

        public override bool IsAssembly => _methodInfo.IsAssembly;

        public override bool IsFamily => _methodInfo.IsFamily;

        public override bool IsPrivate => _methodInfo.IsPrivate;

        public override bool IsPublic => _methodInfo.IsPublic;

        public override bool IsStatic => _methodInfo.IsStatic;

        public override bool IsVirtual => _methodInfo.IsVirtual;

        public override string Name => _methodInfo.Name;

        public override TypeDescription ReturnType => new RuntimeTypeDescription(_methodInfo.ReturnType);

        public override ParameterDescription[] GetParameters()
        {
            return _methodInfo.GetParameters().Select(i => new RuntimeParameterDescription(i)).ToArray();
        }
    }
}
