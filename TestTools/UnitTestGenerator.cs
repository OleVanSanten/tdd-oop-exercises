using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using TestTools.Structure;
using TestTools.TypeSystem;

namespace TestTools.Templates
{
    // Based on TestTools.Structure.TypeVisitor
    [Generator]
    public class UnitTestGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
#if DEBUG
             Debugger.Launch();
#endif
            context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
        }

        public void Execute(GeneratorExecutionContext context)
        {
            SyntaxReceiver syntaxReceiver = context.SyntaxReceiver as SyntaxReceiver;

            if (syntaxReceiver == null)
                return;

            var attributeMetadata = context.Compilation.GetTypeByMetadataName("TestTools.Structure.TemplatedAttribute");

            var globalNamespace = new CompileTimeNamespaceDescription(context.Compilation.GlobalNamespace);
            var sourceNamespace = globalNamespace.GetNamespace("Lecture_2_Solutions");
            var targetNamespace = globalNamespace.GetNamespace("Lecture_2");
            var service = new StructureService(sourceNamespace, targetNamespace)
            {
                StructureVerifier = new VerifierService()
            };
            var rewriter = new TypeRewriter(service, context.Compilation);

            foreach (var node in syntaxReceiver.CandidateSyntax)
            {
                var model = context.Compilation.GetSemanticModel(node.SyntaxTree);
                var symbol = model.GetDeclaredSymbol(node, context.CancellationToken) as ITypeSymbol;
                var @namespace = symbol.ContainingNamespace;

                var attributeData = symbol?.GetAttributes().FirstOrDefault(x => x.AttributeClass.BaseType.Equals(attributeMetadata, SymbolEqualityComparer.Default));

                if (attributeData == null)
                    continue;

                var fileName = $"{symbol?.ToDisplayString()}.g.cs";
                var rewrittenNode = rewriter.Visit(node.SyntaxTree.GetRoot(context.CancellationToken));
                var source = SourceText.From(rewrittenNode.NormalizeWhitespace().ToFullString(), Encoding.UTF8);
                context.AddSource(fileName, source);
            }
        }

        private class SyntaxReceiver : ISyntaxReceiver
        {
            private readonly List<SyntaxNode> _candidateSyntax = new List<SyntaxNode>();

            public IEnumerable<SyntaxNode> CandidateSyntax => _candidateSyntax;

            public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
            {
                if (syntaxNode is ClassDeclarationSyntax classDeclarationSyntax)
                    _candidateSyntax.Add(classDeclarationSyntax);

                //if (syntaxNode is MemberDeclarationSyntax memberDeclarationSyntax)
                //    _candidateSyntax.Add(memberDeclarationSyntax);
            }
        }
    }
}