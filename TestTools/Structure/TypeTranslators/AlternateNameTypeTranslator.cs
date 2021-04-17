using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;

namespace TestTools.Structure
{
    public class AlternateNameTypeTranslator : TypeTranslator
    {
        string[] _alternateNames;

        public AlternateNameTypeTranslator(params string[] alternateNames)
        {
            _alternateNames = alternateNames;
        }

        public override Type Translate(Type type)
        {
            string[] names = _alternateNames.Union(new[] { type.Name }).ToArray();

            foreach(Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                Type translatedType = assembly.GetTypes().SingleOrDefault(t => t.Namespace == TargetNamespace && names.Contains(t.Name));

                if (translatedType != null)
                    return translatedType;
            }

            // TODO fix the following lines as they give an unclear program flow
            Verifier.FailTypeNotFound(TargetNamespace, names);
            
            // Should never get to here as FailTypeNotFound() should throw an exception
            throw new NotImplementedException(); 
        }
    }
}
