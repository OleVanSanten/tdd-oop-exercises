using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TestTools.TypeSystem
{
    public class RuntimeConstructorDescription : ConstructorDescription
    {
        ConstructorInfo _constructorInfo;

        public RuntimeConstructorDescription(ConstructorInfo constructorInfo)
        {
            _constructorInfo = constructorInfo;
        }

        public override TypeDescription DeclaringType => new RuntimeTypeDescription(_constructorInfo.DeclaringType);

        public override bool IsAssembly => _constructorInfo.IsAssembly;

        public override bool IsFamily => _constructorInfo.IsFamily;

        public override bool IsPrivate => _constructorInfo.IsPrivate;

        public override bool IsPublic => _constructorInfo.IsPublic;

        public override string Name => _constructorInfo.Name;

        public override bool IsAbstract => _constructorInfo.IsAbstract;

        public override bool IsStatic => _constructorInfo.IsStatic;

        public override bool IsVirtual => _constructorInfo.IsVirtual;

        public override ParameterDescription[] GetParameters()
        {
            var parameters = _constructorInfo.GetParameters();
            return parameters.Select(p => new RuntimeParameterDescription(p)).ToArray();
        }
    }
}
