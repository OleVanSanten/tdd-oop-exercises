using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;
using TestTools.Structure;
using TestTools.TypeSystem;

namespace TestTools.Templates
{
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

            var templatedAttributeType = new RuntimeTypeDescription(typeof(AttributeEquivalentAttribute));

            var configuration = GetConfiguration(context);
            var structureService = ConfigureStructureService(context.Compilation, configuration);
            var typeRewriter = ConfigureTypeRewriter(structureService, context.Compilation, configuration);

            foreach (var node in syntaxReceiver.CandidateSyntax)
            {
                var model = context.Compilation.GetSemanticModel(node.SyntaxTree);
                var symbol = (ITypeSymbol)model.GetDeclaredSymbol(node, context.CancellationToken);
                var typeDescription = new CompileTimeTypeDescription(symbol);
                var attributeOfAttributes = typeDescription.GetCustomAttributeTypes().SelectMany(t => t.GetCustomAttributeTypes());

                if (!attributeOfAttributes.Contains(templatedAttributeType))
                    continue;

                var fileName = $"{symbol?.ToDisplayString()}.g.cs";
                var rewrittenNode = typeRewriter.Visit(node.SyntaxTree.GetRoot(context.CancellationToken));
                var source = SourceText.From(rewrittenNode.NormalizeWhitespace().ToFullString(), Encoding.UTF8);
                context.AddSource(fileName, source);
            }
        }

        private XMLConfiguration GetConfiguration(GeneratorExecutionContext context)
        {
            var xmlFiles = context.AdditionalFiles.Where(at => at.Path.EndsWith(".xml"));
            var rawConfig = xmlFiles.FirstOrDefault()?.GetText()?.ToString() ?? throw new ArgumentException("Configuration file is missing");
            return new XMLConfiguration(rawConfig);

            //foreach (var xmlFile in xmlFiles)
            //{
            //    if (context.AnalyzerConfigOptions.GetOptions(xmlFile).TryGetValue("build_metadata.AdditionalFiles.UnitTestGenerator_IsConfig", out var isConfig))
            //    {
            //        if(isConfig.Equals("true", StringComparison.OrdinalIgnoreCase))
            //            return xmlFile.GetText()?.ToString();
            //    }
            //}
            //return null;
        }

        private IStructureService ConfigureStructureService(Compilation compilation, XMLConfiguration config)
        {
            var globalNamespace = new CompileTimeNamespaceDescription(compilation.GlobalNamespace);
            var fromNamespace = config.GetFromNamespace(globalNamespace);
            var toNamespace = config.GetToNamespace(globalNamespace);

            var typeTranslator = config.GetTypeTranslator();
            var memberTranslator = config.GetMemberTranslator();

            var structureService = new StructureService(fromNamespace, toNamespace)
            {
                StructureVerifier = new VerifierService()
            };

            if (typeTranslator != null)
                structureService.TypeTranslator = typeTranslator;

            if (memberTranslator != null)
                structureService.MemberTranslator = memberTranslator;

            return structureService;
        }

        private TypeRewriter ConfigureTypeRewriter(IStructureService structureService, Compilation compilation, XMLConfiguration config)
        {
            var typeVerifiers = config.GetTypeVerifiers();
            var memberVerifiers = config.GetMemberVerifiers();

            var typeRewritter = new TypeRewriter(structureService, compilation);

            if (typeVerifiers != null)
                typeRewritter.TypeVerifiers = typeVerifiers;

            if (memberVerifiers != null)
                typeRewritter.MemberVerifiers = memberVerifiers;

            return typeRewritter;
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