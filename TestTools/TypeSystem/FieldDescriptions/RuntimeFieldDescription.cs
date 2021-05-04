using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace TestTools.TypeSystem
{
    public class RuntimeFieldDescription : FieldDescription
    {
        FieldInfo _fieldInfo;

        public RuntimeFieldDescription(FieldInfo fieldInfo)
        {
            _fieldInfo = fieldInfo;
        }

        public override TypeDescription DeclaringType => new RuntimeTypeDescription(_fieldInfo.DeclaringType);

        public override TypeDescription FieldType => new RuntimeTypeDescription(_fieldInfo.FieldType);

        public override bool IsAssembly => _fieldInfo.IsAssembly;

        public override bool IsFamily => _fieldInfo.IsFamily;

        public override bool IsInitOnly => _fieldInfo.IsInitOnly;

        public override bool IsPrivate => _fieldInfo.IsPrivate;

        public override bool IsPublic => _fieldInfo.IsPublic;

        public override bool IsStatic => _fieldInfo.IsStatic;

        public override string Name => _fieldInfo.Name;
    }
}
