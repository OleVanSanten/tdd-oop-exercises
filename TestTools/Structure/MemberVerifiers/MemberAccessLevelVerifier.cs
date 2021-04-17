using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace TestTools.Structure
{
    public class MemberAccessLevelVerifier : MemberVerifier
    {
        readonly AccessLevels[] _accessLevels;

        public MemberAccessLevelVerifier(AccessLevels accessLevel) : this(new[] { accessLevel })
        {
        }

        public MemberAccessLevelVerifier(AccessLevels[] accessLevels)
        {
            _accessLevels = accessLevels;
        }

        public override MemberVerificationAspect[] Aspects => new[] {
            MemberVerificationAspect.ConstructorAccessLevel,
            MemberVerificationAspect.EventAddAccessLevel,
            MemberVerificationAspect.EventRemoveAccessLevel,
            MemberVerificationAspect.FieldAccessLevel,
            MemberVerificationAspect.PropertyGetAccessLevel,
            MemberVerificationAspect.PropertySetAccessLevel,
            MemberVerificationAspect.MethodAccessLevel
        };

        public override void Verify(MemberInfo originalMember, MemberInfo translatedMember)
        {
            if (translatedMember is ConstructorInfo translatedConstructor)
            {
                Verifier.VerifyAccessLevel(translatedConstructor, _accessLevels);
            }
            else if (translatedMember is EventInfo translatedEvent)
            {
                Verifier.VerifyAccessLevel(translatedEvent, _accessLevels, AddMethod: true, RemoveMethod: true);
            }
            else if (translatedMember is FieldInfo translatedField)
            {
                Verifier.VerifyAccessLevel(translatedField, _accessLevels);
            }
            else if (translatedMember is PropertyInfo translatedProperty)
            {
                Verifier.VerifyAccessLevel(translatedProperty, _accessLevels, GetMethod: true, SetMethod: true);
            }
            else if (translatedMember is MethodInfo translatedMethod)
            {
                Verifier.VerifyAccessLevel(translatedMethod, _accessLevels);
            }
            else throw new NotImplementedException();
        }
    }
}
