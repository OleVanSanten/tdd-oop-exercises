using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TestTools.TypeSystem
{
    public class RuntimeConstructorDescription : ConstructorDescription
    {
        public RuntimeConstructorDescription(ConstructorInfo constructorInfo)
        {
            ConstructorInfo = constructorInfo;
        }

        public ConstructorInfo ConstructorInfo { get; }

        public override TypeDescription DeclaringType => new RuntimeTypeDescription(ConstructorInfo.DeclaringType);

        public override bool IsAssembly => ConstructorInfo.IsAssembly;

        public override bool IsFamily => ConstructorInfo.IsFamily;

        public override bool IsPrivate => ConstructorInfo.IsPrivate;

        public override bool IsPublic => ConstructorInfo.IsPublic;

        public override string Name => ConstructorInfo.Name;

        public override bool IsAbstract => ConstructorInfo.IsAbstract;

        public override bool IsStatic => ConstructorInfo.IsStatic;

        public override bool IsVirtual => ConstructorInfo.IsVirtual;

        public override ParameterDescription[] GetParameters()
        {
            var parameters = ConstructorInfo.GetParameters();
            return parameters.Select(p => new RuntimeParameterDescription(p)).ToArray();
        }
    }
}
