using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace TestTools.TypeSystem
{
    public class RuntimeParameterDescription : ParameterDescription
    {
        ParameterInfo _parameterInfo;

        public RuntimeParameterDescription(ParameterInfo parameterInfo)
        {
            _parameterInfo = parameterInfo;
        }

        public override object DefaultValue => _parameterInfo.DefaultValue;

        public override bool HasDefaultValue => _parameterInfo.HasDefaultValue;

        public override bool IsIn => _parameterInfo.IsIn;

        public override bool IsOut => _parameterInfo.IsOut;

        public override string Name => _parameterInfo.Name;

        public override TypeDescription ParameterType => new RuntimeTypeDescription(_parameterInfo.ParameterType);
    }
}
