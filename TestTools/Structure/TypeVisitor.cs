using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Linq;
using TestTools.Helpers;

namespace TestTools.Structure
{
    public class TypeVisitor : ExpressionVisitor
    {
        IStructureService _structureService;

        Dictionary<ParameterExpression, ParameterExpression> _variableCache = new Dictionary<ParameterExpression, ParameterExpression>();

        public ITypeVerifier[] TypeVerifiers { get; set; } = new ITypeVerifier[]
        {
            new UnchangedTypeAccessLevelVerifier(),
            new UnchangedTypeIsAbstractVerifier(),
            new UnchangedTypeIsStaticVerifier()
        };

        public IMemberVerifier[] MemberVerifiers { get; set; } = new IMemberVerifier[]
        {
            new UnchangedFieldTypeVerifier(),
            new UnchangedMemberAccessLevelVerifier(),
            new UnchangedMemberDeclaringType(),
            new UnchangedMemberIsStaticVerifier(),
            new UnchangedMemberIsVirtualVerifier(),
            new UnchangedMemberTypeVerifier(),
            new UnchangedPropertyTypeVerifier()
        };

        public TypeVisitor(IStructureService structureService)
        {
            _structureService = structureService;
        }

        protected override Expression VisitNew(NewExpression node)
        {
            _structureService.VerifyType(node.Type, TypeVerifiers);
            _structureService.VerifyMember(
                node.Constructor,
                MemberVerifiers,
                MemberVerificationAspect.MemberType,
                MemberVerificationAspect.ConstructorAccessLevel);

            MemberInfo memberInfo = _structureService.TranslateMember(node.Constructor);
            return Expression.New((ConstructorInfo)memberInfo, node.Arguments.Select(Visit));
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            _structureService.VerifyType(node.Type, TypeVerifiers);
            _structureService.VerifyMember(
                node.Method,
                MemberVerifiers,
                MemberVerificationAspect.MemberType,
                MemberVerificationAspect.MethodDeclaringType,
                MemberVerificationAspect.MethodReturnType,
                MemberVerificationAspect.MethodIsStatic,
                MemberVerificationAspect.MethodIsAbstract,
                MemberVerificationAspect.MethodIsVirtual,
                MemberVerificationAspect.MethodAccessLevel);

            MethodInfo methodInfo = (MethodInfo)_structureService.TranslateMember(node.Method);
            ParameterInfo[] methodPars = methodInfo.GetParameters();
            Expression[] methodArgs = node.Arguments.Select(Visit).ToArray();
            
            // Constructing generic methods need to be constructed based on argument types
            if (methodInfo.IsGenericMethod)
            {
                Type[] typeArguments = methodInfo.GetGenericArguments();

                for (int i = 0; i < typeArguments.Length; i++) { 
                    for (int j = 0; j < methodArgs.Length; j++)
                    {
                        Type temp = methodArgs[j].Type.GetGenericArguments(methodPars[j].ParameterType, typeArguments[i]);

                        if (temp != null)
                        {
                            typeArguments[i] = temp;
                            break;
                        }
                    }
                }

                methodInfo = methodInfo.MakeGenericMethod(typeArguments);
            }

            return Expression.Call(Visit(node.Object), methodInfo, methodArgs);
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            _structureService.VerifyType(node.Type, TypeVerifiers);
            _structureService.VerifyMember(node.Member, MemberVerifiers, MemberVerificationAspect.MemberType);

            MemberInfo memberInfo = _structureService.TranslateMember(node.Member);
            
            if (memberInfo is FieldInfo fieldInfo)
            {
                _structureService.VerifyMember(
                    node.Member,
                    MemberVerifiers,
                    MemberVerificationAspect.FieldType,
                    MemberVerificationAspect.FieldIsStatic,
                    MemberVerificationAspect.FieldAccessLevel);
                return Expression.Field(Visit(node.Expression), fieldInfo);
            }
            else if (memberInfo is PropertyInfo propertyInfo)
            {
                _structureService.VerifyMember(
                       node.Member,
                       MemberVerifiers,
                       MemberVerificationAspect.PropertyType,
                       MemberVerificationAspect.PropertyIsStatic,
                       MemberVerificationAspect.PropertyGetDeclaringType,
                       MemberVerificationAspect.PropertyGetIsAbstract,
                       MemberVerificationAspect.PropertyGetIsVirtual,
                       MemberVerificationAspect.PropertyGetAccessLevel);
                return Expression.Property(Visit(node.Expression), propertyInfo);
            }
            else throw new ArgumentException("Member was not translated to FieldInfo or PropertyInfo");
        }

        protected override MemberAssignment VisitMemberAssignment(MemberAssignment node)
        {
            _structureService.VerifyType(node.Member.DeclaringType, TypeVerifiers);
            _structureService.VerifyMember(node.Member, MemberVerifiers, MemberVerificationAspect.MemberType);

            MemberInfo memberInfo = _structureService.TranslateMember(node.Member);
            if (memberInfo is FieldInfo fieldInfo)
            {
                _structureService.VerifyMember(
                    node.Member,
                    MemberVerifiers,
                    MemberVerificationAspect.FieldType,
                    MemberVerificationAspect.FieldIsStatic,
                    MemberVerificationAspect.FieldWriteability,
                    MemberVerificationAspect.FieldAccessLevel);
                return Expression.Bind(fieldInfo, node.Expression);
            }
            else if (memberInfo is PropertyInfo propertyInfo)
            {
                _structureService.VerifyMember(
                       node.Member,
                       MemberVerifiers,
                       MemberVerificationAspect.PropertyType,
                       MemberVerificationAspect.PropertyIsStatic,
                       MemberVerificationAspect.PropertySetDeclaringType,
                       MemberVerificationAspect.PropertySetIsAbstract,
                       MemberVerificationAspect.PropertySetIsVirtual,
                       MemberVerificationAspect.PropertySetAccessLevel);
                return Expression.Bind(propertyInfo, node.Expression);
            }
            else throw new ArgumentException("Member was not translated to FieldInfo or PropertyInfo");
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            _structureService.VerifyType(node.Type, TypeVerifiers);

            // To preserve the referential equality of parameter expressions 
            // the function must return the exact same output for the same input.
            // Not doing this would result in the expression not being compile-able.
            if (!_variableCache.ContainsKey(node))
            {
                Random random = new Random();
                Type translatedType = _structureService.TranslateType(node.Type);
                ParameterExpression newParameter = Expression.Parameter(translatedType, random.Next().ToString());

                _variableCache.Add(node, newParameter);

                return newParameter;
            }
            return _variableCache[node];
        }
    }
}
