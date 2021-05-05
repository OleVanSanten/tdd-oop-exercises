﻿using System;
using System.Collections.Generic;
using System.Text;
using TestTools.Helpers;
using TestTools.TypeSystem;

namespace TestTools.Structure
{
    public class PropertyIsReadonlyVerifier : MemberVerifier
    {
        public override MemberVerificationAspect[] Aspects => new[] {
            MemberVerificationAspect.MemberType,
            MemberVerificationAspect.PropertyGetAccessLevel,
            MemberVerificationAspect.PropertySetAccessLevel
        };

        public override void Verify(MemberDescription originalMember, MemberDescription translatedMember)
        {
            Verifier.VerifyMemberType(translatedMember, new[] { MemberTypes.Property });

            if (translatedMember is PropertyDescription propertyInfo)
            {
                Verifier.VerifyIsReadonly(propertyInfo, DescriptionHelper.GetAccessLevel(((PropertyDescription)originalMember).GetMethod));
            }
            else throw new NotImplementedException();
        }
    }
}
