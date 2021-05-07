using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TestTools.Helpers;
using TestTools.TypeSystem;

namespace TestTools.Structure
{
    // Source Generator of TypeVisitor
    public class TypeRewriter : CSharpSyntaxRewriter
    {
        private IStructureService _structureService;
        private Compilation _compilation;

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
            // Only classes marked with an attritubes that are marked with TemplatedAttribute should be rewritten
            var type = GetTypeDescription(node);
            var attributesOfAttributes = type.GetCustomAttributeTypes().SelectMany(t => t.GetCustomAttributes());
            if (!attributesOfAttributes.Any(a => a is AttributeEquivalentAttribute))
                return node;

            // Potentially rewritting the class name by removing _Templated from it
            var newClassName = node.Identifier.Text.Replace("_Template", "");
            var newIdentifier = SyntaxFactory.IdentifierName(newClassName).Identifier;

            // Potentially rewritting the class members
            var newMembers = new SyntaxList<SyntaxNode>(node.Members.Select(Visit));

            // Potentially rewritting attributes
            var newAttributeLists = new SyntaxList<AttributeListSyntax>(node.AttributeLists.Select(Visit).OfType<AttributeListSyntax>());

            return node.WithIdentifier(newIdentifier).WithMembers(newMembers).WithAttributeLists(newAttributeLists);
        }

        public override SyntaxNode VisitAttribute(AttributeSyntax node)
        {
            var templatedAttribute = GetTemplatedAttribute(node);

            if (templatedAttribute == null)
                return node;

            // Rewritting templated-attribute type to non-templated-attribute type
            var newName = SyntaxFactory.IdentifierName(templatedAttribute.EquavilentAttribute);
            
            return node.WithName(newName);
        }

        public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            // Only methods marked with an attritubes that are marked with TemplatedAttribute should be rewritten
            var method = GetMethodDescription(node);
            var attributesOfAttributes = method.GetCustomAttributeTypes().SelectMany(t => t.GetCustomAttributes());
            if (!attributesOfAttributes.Any(a => a is AttributeEquivalentAttribute))
                return node;

            // Rewritting the method body to switch out all FromNamespace members with ToNamespace members
            // If the rewrite is unsuccessful due to validation errors, the entire method body is replaced
            // with an exception 
            // and if the rewrite validation fails replacing the entire body with an exception
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
            var originalType = GetTypeDescription(node);
            var originalConstructor = GetConstructorDescription(node);

            _structureService.VerifyType(originalType, TypeVerifiers);
            _structureService.VerifyMember(
                originalConstructor,
                MemberVerifiers,
                MemberVerificationAspect.MemberType,
                MemberVerificationAspect.ConstructorAccessLevel);

            var translatedType = (CompileTimeTypeDescription)_structureService.TranslateType(originalType);
            var newType = SyntaxFactory.ParseTypeName(translatedType.FullName);
            return node.WithType(newType);
        }

        public override SyntaxNode VisitInvocationExpression(InvocationExpressionSyntax node)
        {
            var memberExpression = node.Expression as MemberAccessExpressionSyntax;

            if (memberExpression == null)
                return node;

            var originalType = GetTypeDescription(memberExpression.Expression);
            var originalMethod = GetMethodDescription(node);

            _structureService.VerifyType(originalType, TypeVerifiers);
            _structureService.VerifyMember(
                originalMethod,
                MemberVerifiers,
                MemberVerificationAspect.MemberType,
                MemberVerificationAspect.MethodDeclaringType,
                MemberVerificationAspect.MethodReturnType,
                MemberVerificationAspect.MethodIsStatic,
                MemberVerificationAspect.MethodIsAbstract,
                MemberVerificationAspect.MethodIsVirtual,
                MemberVerificationAspect.MethodAccessLevel);

            // Potentially rewritting expression and method name 
            var translatedMethod = (CompileTimeMethodDescription)_structureService.TranslateMember(originalMethod);
            var newName = SyntaxFactory.IdentifierName(translatedMethod.Name);
            var newMemberExpression = (MemberAccessExpressionSyntax)Visit(memberExpression);
            var newExpression = newMemberExpression.WithName(newName);

            // Potentially rewritting method arguments
            var newArguments = SyntaxFactory.SeparatedList(node.ArgumentList.Arguments.Select(Visit).OfType<ArgumentSyntax>());
            var newArgumentList = node.ArgumentList.WithArguments(newArguments);
            
            return node.WithExpression(newExpression).WithArgumentList(newArgumentList);
        }

        public override SyntaxNode VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
        {
            var originalType = GetTypeDescription(node.Expression);
            var originalMember = GetFieldOrPropertyDescription(node);

            _structureService.VerifyType(originalType, TypeVerifiers);
            if (originalMember is FieldDescription)
            {
                _structureService.VerifyMember(
                    originalMember,
                    MemberVerifiers,
                    MemberVerificationAspect.FieldType,
                    MemberVerificationAspect.FieldIsStatic,
                    MemberVerificationAspect.FieldWriteability,
                    MemberVerificationAspect.FieldAccessLevel);
            }
            else if (originalMember is PropertyDescription)
            {
                _structureService.VerifyMember(
                       originalMember,
                       MemberVerifiers,
                       MemberVerificationAspect.PropertyType,
                       MemberVerificationAspect.PropertyIsStatic,
                       MemberVerificationAspect.PropertySetDeclaringType,
                       MemberVerificationAspect.PropertySetIsAbstract,
                       MemberVerificationAspect.PropertySetIsVirtual,
                       MemberVerificationAspect.PropertySetAccessLevel);
            }

            // Potentially rewritting member name
            var translatedMember = _structureService.TranslateMember(originalMember);
            var newName = SyntaxFactory.IdentifierName(translatedMember.Name);

            // Potentially rewritting expression
            var newExpression = (ExpressionSyntax)Visit(node.Expression);

            return node.WithName(newName).WithExpression(newExpression);
        }

        public override SyntaxNode VisitVariableDeclaration(VariableDeclarationSyntax node)
        {
            var originalType = GetTypeDescription(node.Type);

            _structureService.VerifyType(originalType, TypeVerifiers);

            // Potentially rewritting type
            var translatedType = (CompileTimeTypeDescription)_structureService.TranslateType(originalType);
            var newType = SyntaxFactory.ParseTypeName(translatedType.TypeSymbol.ToDisplayString());

            // Potentially rewritting variable declators
            var newVariables = SyntaxFactory.SeparatedList(node.Variables.Select(Visit).OfType<VariableDeclaratorSyntax>());
            
            return node.WithType(newType).WithVariables(newVariables);
        }

        TypeDescription GetTypeDescription(SyntaxNode node)
        {
            var semanticModel = _compilation.GetSemanticModel(node.SyntaxTree, ignoreAccessibility: true);
            var typeSymbol = semanticModel.GetTypeInfo(node).Type;

            return new CompileTimeTypeDescription(typeSymbol);
        }

        TypeDescription GetTypeDescription(ClassDeclarationSyntax node)
        {
            var semanticModel = _compilation.GetSemanticModel(node.SyntaxTree, ignoreAccessibility: true);
            var typeModel = semanticModel.GetDeclaredSymbol(node);

            return new CompileTimeTypeDescription(typeModel);
        }

        // Returns TemplatedAttribute object if attribute class is marked with [TemplatedAttribute]
        AttributeEquivalentAttribute GetTemplatedAttribute(AttributeSyntax node)
        {
            var semanticModel = _compilation.GetSemanticModel(node.SyntaxTree, ignoreAccessibility: true);
            var attributeSymbol = (IMethodSymbol)semanticModel.GetSymbolInfo(node).Symbol;
            var attributeClass = attributeSymbol.ContainingType;
            var attributeDescription = new CompileTimeTypeDescription(attributeClass);

            // Check if it contains any TemplateEquivalentAttribute at all
            var targetAttribute = new RuntimeTypeDescription(typeof(AttributeEquivalentAttribute));
            if (!attributeDescription.GetCustomAttributeTypes().Contains(targetAttribute))
                return null;

            return attributeDescription.GetCustomAttributes().OfType<AttributeEquivalentAttribute>().FirstOrDefault();
        }

        ConstructorDescription GetConstructorDescription(ObjectCreationExpressionSyntax node)
        {
            var semanticModel = _compilation.GetSemanticModel(node.SyntaxTree, ignoreAccessibility: true);
            var methodSymbol = (IMethodSymbol)semanticModel.GetSymbolInfo(node).Symbol;

            return new CompileTimeConstructorDescription(methodSymbol);
        }
        
        MemberDescription GetFieldOrPropertyDescription(MemberAccessExpressionSyntax node)
        {
            
            var semanticModel = _compilation.GetSemanticModel(node.SyntaxTree, ignoreAccessibility: true);
            var memberSymbol = semanticModel.GetSymbolInfo(node).Symbol;

            if (memberSymbol is IFieldSymbol fieldSymbol)
                return new CompileTimeFieldDescription(fieldSymbol);

            if (memberSymbol is IPropertySymbol propertySymbol)
                return new CompileTimePropertyDescription(propertySymbol);

            if (memberSymbol is IMethodSymbol methodSymbol)
                return new CompileTimeMethodDescription(methodSymbol);

            throw new NotImplementedException();
        }

        MethodDescription GetMethodDescription(InvocationExpressionSyntax node)
        {
            var semanticModel = _compilation.GetSemanticModel(node.SyntaxTree, ignoreAccessibility: true);
            var methodSymbol = (IMethodSymbol)semanticModel.GetSymbolInfo(node).Symbol;

            return new CompileTimeMethodDescription(methodSymbol);
        }

        MethodDescription GetMethodDescription(MethodDeclarationSyntax node)
        {
            var semanticModel = _compilation.GetSemanticModel(node.SyntaxTree, ignoreAccessibility: true);
            var methodSymbol = semanticModel.GetDeclaredSymbol(node);

            return new CompileTimeMethodDescription(methodSymbol);
        }
    }
}