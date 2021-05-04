using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace TestTools.TypeSystem
{
    public class RuntimePropertyDescription : PropertyDescription
    {
        PropertyInfo _propertyInfo;

        public RuntimePropertyDescription(PropertyInfo propertyInfo)
        {
            _propertyInfo = propertyInfo;
        }

        public override bool CanRead => _propertyInfo.CanRead;

        public override bool CanWrite => _propertyInfo.CanWrite;

        public override TypeDescription DeclaringType => new RuntimeTypeDescription(_propertyInfo.DeclaringType);

        public override MethodDescription GetMethod => throw new NotImplementedException();

        public override string Name => _propertyInfo.Name;

        public override TypeDescription PropertyType => new RuntimeTypeDescription(_propertyInfo.PropertyType);

        public override MethodDescription SetMethod => throw new NotImplementedException();
    }
}
