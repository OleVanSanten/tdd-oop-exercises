using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;

namespace TestTools.Structure
{
    public class SameNameTypeTranslator : TypeTranslator
    {
        public override Type Translate(Type type)
        {
            foreach(Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                Type translatedType = assembly.GetTypes().SingleOrDefault(t => t.Namespace == TargetNamespace && t.Name == type.Name);

                if (translatedType != null)
                    return translatedType;
            }

            // TODO fix the following lines as they give an unclear program flow
            Verifier.FailTypeNotFound(TargetNamespace, type);

            // Should never get to here as FailTypeNotFound() should throw an exception
            throw new NotImplementedException();
        }
    }
}
