using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace TestTools.TypeSystem
{
    public class RuntimeEventDescription : EventDescription
    {
        EventInfo _eventInfo;

        public RuntimeEventDescription(EventInfo eventInfo)
        {
            _eventInfo = eventInfo;
        }

        public override MethodDescription AddMethod => new RuntimeMethodDescription(_eventInfo.AddMethod);

        public override TypeDescription DeclaringType => new RuntimeTypeDescription(_eventInfo.DeclaringType);

        public override TypeDescription EventHandlerType => new RuntimeTypeDescription(_eventInfo.EventHandlerType);

        public override string Name => _eventInfo.Name;

        public override MethodDescription RemoveMethod => new RuntimeMethodDescription(_eventInfo.RemoveMethod);
    }
}
