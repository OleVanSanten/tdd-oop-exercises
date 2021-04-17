using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using TestTools.Helpers;

namespace TestTools.Structure
{
    public class UnchangedMemberAccessLevelVerifier : MemberVerifier
    {
        public override MemberVerificationAspect[] Aspects => new[] {
            MemberVerificationAspect.ConstructorAccessLevel,
            MemberVerificationAspect.FieldAccessLevel,
            MemberVerificationAspect.PropertyGetAccessLevel,
            MemberVerificationAspect.PropertySetAccessLevel,
            MemberVerificationAspect.MethodAccessLevel
        };

        public override void Verify(MemberInfo originalMember, MemberInfo translatedMember)
        {
            if (originalMember is ConstructorInfo originalConstructor)
            {
                AccessLevels accessLevel = ReflectionHelper.GetAccessLevel(originalConstructor);
                Verifier.VerifyMemberType(translatedMember, new[] { MemberTypes.Constructor });
                Verifier.VerifyAccessLevel((ConstructorInfo)translatedMember, new[] { accessLevel });
            }
            else if (originalMember is FieldInfo originalField)
            {
                AccessLevels accessLevel = ReflectionHelper.GetAccessLevel(originalField);

                Verifier.VerifyMemberType(translatedMember, new[] { MemberTypes.Field, MemberTypes.Property });

                if (translatedMember is FieldInfo translatedField)
                {
                    Verifier.VerifyAccessLevel(translatedField, new[] { accessLevel });
                }
                else if (translatedMember is PropertyInfo translatedProperty)
                {
                    Verifier.VerifyAccessLevel(translatedProperty, new[] { accessLevel }, GetMethod: true, SetMethod: true);
                }   
            }
            else if (originalMember is PropertyInfo originalProperty)
            {
                AccessLevels? accessLevel1 = originalProperty.CanRead ? ReflectionHelper.GetAccessLevel(originalProperty.GetMethod) : (AccessLevels?)null;
                AccessLevels? accessLevel2 = originalProperty.CanWrite ? ReflectionHelper.GetAccessLevel(originalProperty.SetMethod) : (AccessLevels?)null;

                Verifier.VerifyMemberType(translatedMember, new[] { MemberTypes.Field, MemberTypes.Property });

                if (translatedMember is FieldInfo translatedField)
                {
                    if (accessLevel1 != null && accessLevel2 != null)
                    {
                        Verifier.VerifyAccessLevel(translatedField, new[] { (AccessLevels)accessLevel1, (AccessLevels)accessLevel2 });
                    }
                    else if (accessLevel1 != null)
                    {
                        Verifier.VerifyAccessLevel(translatedField, new[] { (AccessLevels)accessLevel1 });
                    }
                    else if (accessLevel2 != null)
                    {
                        Verifier.VerifyAccessLevel(translatedField, new[] { (AccessLevels)accessLevel2 });
                    }
                }
                else if (translatedMember is PropertyInfo translatedProperty)
                {
                    if (accessLevel1 != null)
                        Verifier.VerifyAccessLevel(translatedProperty, new[] { (AccessLevels)accessLevel1 }, GetMethod: true);
                    if (accessLevel2 != null)
                    Verifier.VerifyAccessLevel(translatedProperty, new[] { (AccessLevels)accessLevel2 }, SetMethod: true);
                }
            }
            else if (translatedMember is MethodInfo originalMethod)
            {
                AccessLevels accessLevel = ReflectionHelper.GetAccessLevel(originalMethod);
                Verifier.VerifyMemberType(translatedMember, new[] { MemberTypes.Method });
                Verifier.VerifyAccessLevel((MethodInfo)translatedMember, new[] { accessLevel });
            }
            else throw new NotImplementedException();
        }
    }
}
