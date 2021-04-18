using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TestTools.Helpers;

namespace TestTools.Structure
{
    // Source Generator of TypeVisitor
    public class TypeRewriter : CSharpSyntaxRewriter
    {
        private IStructureService _structureService;
        private Compilation _compilation;
        private Dictionary<string, Type> _cache = new Dictionary<string, Type>();

        public TypeRewriter(IStructureService structureService, Compilation compilation)
        {
            _structureService = structureService;
            _compilation = compilation;
        }

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

        public override SyntaxNode VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            var newClassName = node.Identifier.Text.Replace("_Template", "");
            var newIdentifier = SyntaxFactory.IdentifierName(newClassName).Identifier;

            var newMembers = new SyntaxList<SyntaxNode>(node.Members.Select(Visit));
            var newAttributeLists = new SyntaxList<AttributeListSyntax>(node.AttributeLists.Select(Visit).OfType<AttributeListSyntax>());

            return node.WithIdentifier(newIdentifier).WithMembers(newMembers).WithAttributeLists(newAttributeLists);
        }

        public override SyntaxNode VisitAttribute(AttributeSyntax node)
        {
            var newAttributeName = node.Name.ToString().Replace("Templated", "");
            var newName = SyntaxFactory.IdentifierName(newAttributeName);

            return node.WithName(newName);
        }

        public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            // Only methods marked with [Templated*] should be rewritten
            if (!node.AttributeLists.SelectMany(l => l.Attributes).Any(a => a.ToString().Contains("Templated")))
                return node;

            BlockSyntax newBody;
            var newAttributeLists = new SyntaxList<AttributeListSyntax>(node.AttributeLists.Select(Visit).OfType<AttributeListSyntax>());

            try
            {
                var newStatements = new SyntaxList<StatementSyntax>(node.Body.Statements.Select(Visit).OfType<StatementSyntax>());
                newBody = SyntaxFactory.Block(newStatements);
            }
            catch (VerifierServiceException ex)
            {
                var exceptionType = $"Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException";
                var throwStatement = $"throw new {exceptionType}(\"{ex.Message} (AUTO)\");";
                newBody = (BlockSyntax)SyntaxFactory.ParseStatement("{" + throwStatement + "}");
            }

            return node.WithBody(newBody).WithAttributeLists(newAttributeLists);
        }

        public override SyntaxNode VisitObjectCreationExpression(ObjectCreationExpressionSyntax node)
        {
            Type objectType = GetType(node);

            // objectType is not part of the solution assembly
            if (objectType == null)
                return node;

            // Finding matching constructor, only based on number of parameters for now
            ConstructorInfo[] objectConstructors = objectType.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            ConstructorInfo objectConstructor = objectConstructors.First(s => s.GetParameters().Length == node.ArgumentList.Arguments.Count());

            _structureService.VerifyType(objectType, TypeVerifiers);
            _structureService.VerifyMember(
                objectConstructor,
                MemberVerifiers,
                MemberVerificationAspect.MemberType,
                MemberVerificationAspect.ConstructorAccessLevel);

            return node.WithType(GetTypeSyntax(_structureService.TranslateType(objectType)));
        }

        public override SyntaxNode VisitInvocationExpression(InvocationExpressionSyntax node)
        {
            var memberExpression = node.Expression as MemberAccessExpressionSyntax;

            if (memberExpression == null)
                return node;

            Type objectType = GetType(memberExpression.Expression);
            string objectMethodName = memberExpression.Name.ToString();

            // objectType is not part of the solution assembly
            if (objectType != null)
            {
                // Finding matching method, only based on number of parameters for now
                var objectMethods = objectType.GetAllMembers().OfType<MethodInfo>();
                MethodInfo objectMethod = objectMethods.First(s => s.Name == objectMethodName && s.GetParameters().Length == node.ArgumentList.Arguments.Count());

                _structureService.VerifyType(objectType, TypeVerifiers);
                _structureService.VerifyMember(
                    objectMethod,
                    MemberVerifiers,
                    MemberVerificationAspect.MemberType,
                    MemberVerificationAspect.MethodDeclaringType,
                    MemberVerificationAspect.MethodReturnType,
                    MemberVerificationAspect.MethodIsStatic,
                    MemberVerificationAspect.MethodIsAbstract,
                    MemberVerificationAspect.MethodIsVirtual,
                    MemberVerificationAspect.MethodAccessLevel);
            }

            var newArguments = SyntaxFactory.SeparatedList(node.ArgumentList.Arguments.Select(Visit).OfType<ArgumentSyntax>());
            var newArgumentList = node.ArgumentList.WithArguments(newArguments);
            return node.WithArgumentList(newArgumentList);
        }

        public override SyntaxNode VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
        {
            Type type = GetType(node.Expression);

            // objectType is not part of the solution assembly
            if (type == null)
                return node;

            string memberName = node.Name.ToString();
            MemberInfo member = type.GetAllMembers().First(m => m.Name == memberName);

            _structureService.VerifyType(type, TypeVerifiers);
            if (member is FieldInfo)
            {
                _structureService.VerifyMember(
                    member,
                    MemberVerifiers,
                    MemberVerificationAspect.FieldType,
                    MemberVerificationAspect.FieldIsStatic,
                    MemberVerificationAspect.FieldWriteability,
                    MemberVerificationAspect.FieldAccessLevel);
            }
            else if (member is PropertyInfo)
            {
                _structureService.VerifyMember(
                       member,
                       MemberVerifiers,
                       MemberVerificationAspect.PropertyType,
                       MemberVerificationAspect.PropertyIsStatic,
                       MemberVerificationAspect.PropertySetDeclaringType,
                       MemberVerificationAspect.PropertySetIsAbstract,
                       MemberVerificationAspect.PropertySetIsVirtual,
                       MemberVerificationAspect.PropertySetAccessLevel);
            }
            return base.VisitMemberAccessExpression(node);
        }

        public override SyntaxNode VisitVariableDeclaration(VariableDeclarationSyntax node)
        {
            Type type = GetType(node.Type);

            // objectType is not part of the solution assembly
            if (type == null)
                return node;

            _structureService.VerifyType(type, TypeVerifiers);

            var newType = GetTypeSyntax(_structureService.TranslateType(type));
            var newVariables = SyntaxFactory.SeparatedList(node.Variables.Select(Visit).OfType<VariableDeclaratorSyntax>());
            return node.WithType(newType).WithVariables(newVariables);
        }

        private TypeSyntax GetTypeSyntax(Type type)
        {
            return SyntaxFactory.ParseTypeName(type.Namespace + "." + type.Name);
        }

        private Type GetType(SyntaxNode node)
        {
            var semanticModel = _compilation.GetSemanticModel(node.SyntaxTree, ignoreAccessibility: true);
            var typeInfo = semanticModel.GetTypeInfo(node);
            var typeName = typeInfo.Type.Name;

            if (_cache.ContainsKey(typeName))
                return _cache[typeName];

            var assemblyPath = _compilation.References.Skip(1).First().Display;
            var assembly = GetAssembly(assemblyPath);

            foreach (var type in assembly.GetModules().SelectMany(m => m.GetTypes()))
                _cache[type.Name] = type;

            return _cache.ContainsKey(typeName) ? _cache[typeName] : null;
        } 

        private Assembly GetAssembly(string assemblyPath)
        {
            return Assembly.LoadFrom(assemblyPath);
        }
    }
}
