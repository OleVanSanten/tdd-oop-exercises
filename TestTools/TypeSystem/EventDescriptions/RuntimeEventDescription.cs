﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace TestTools.TypeSystem
{
    public class RuntimeEventDescription : EventDescription
    {
        public RuntimeEventDescription(EventInfo eventInfo)
        {
            EventInfo = eventInfo;
        }

        public EventInfo EventInfo { get; }

        public override MethodDescription AddMethod => new RuntimeMethodDescription(EventInfo.AddMethod);

        public override TypeDescription DeclaringType => new RuntimeTypeDescription(EventInfo.DeclaringType);

        public override TypeDescription EventHandlerType => new RuntimeTypeDescription(EventInfo.EventHandlerType);

        public override string Name => EventInfo.Name;

        public override MethodDescription RemoveMethod => new RuntimeMethodDescription(EventInfo.RemoveMethod);
    }
}
