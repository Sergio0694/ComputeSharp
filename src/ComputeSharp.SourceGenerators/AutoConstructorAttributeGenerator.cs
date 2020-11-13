using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Mustache
{
    [Generator]
    public class AutoConstructorAttributeGenerator : ISourceGenerator
    {
        /// <inheritdoc/>
        public void Initialize(GeneratorInitializationContext context)
        {
            //context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
        }

        /// <inheritdoc/>
        public void Execute(GeneratorExecutionContext context)
        {
            string attributeSource = @"
                using System;

                namespace ComputeSharp
                {
                    /// <summary>
                    /// A shader that indicates that a target shader type should get an automatic constructor for all fields.
                    /// </summary>
                    [AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
                    internal sealed class AutoConstructorAttribute : Attribute
                    {
                    }
                }";

            // Add the GenerateConstructor attribute
            context.AddSource("__ComputeSharp_AutoConstructorAttribute", SourceText.From(attributeSource, Encoding.UTF8));

            // Ensure we have access to the local syntax receiver
            //if (context.SyntaxReceiver is not SyntaxReceiver syntaxReceiver) return;

            // Create a new compilation with the new custom attribute
            //CSharpParseOptions options = (CSharpParseOptions)((CSharpCompilation)context.Compilation).SyntaxTrees[0].Options;
            //Compilation compilation = context.Compilation.AddSyntaxTrees(CSharpSyntaxTree.ParseText(SourceText.From(attributeSource, Encoding.UTF8), options));

            // Get the new attribute from the compilation
            //INamedTypeSymbol attributeSymbol = compilation.GetTypeByMetadataName("ComputeSharp.AutoConstructorAttribute")!;

            // Loop over the candidate struct declarations, and keep the ones that are actually annotated
            foreach (var structSymbol in
                //from structDeclaration in syntaxReceiver.CandidateTypes
                from tree in context.Compilation.SyntaxTrees
                from node in tree.GetRoot().DescendantNodes()
                where node.IsKind(SyntaxKind.Attribute) &&
                      node is
                let semanticModel = compilation.GetSemanticModel(structDeclaration.SyntaxTree)
                let symbol = semanticModel.GetDeclaredSymbol(structDeclaration)
                let attributes = symbol.GetAttributes()
                where attributes.Any(attribute => attribute.AttributeClass!.Equals(attributeSymbol, SymbolEqualityComparer.Default))
                select symbol)
            {
                string namespaceName = structSymbol.ContainingNamespace.ToDisplayString();

                // Begin constructing the type
                StringBuilder source = new();
                source.AppendLine($"namespace {namespaceName}");
                source.AppendLine("{");

                // Visibility
                source.Append(structSymbol.DeclaredAccessibility switch
                {
                    Accessibility.Private => "private",
                    Accessibility.ProtectedAndInternal => "protected internal",
                    Accessibility.Protected => "protected",
                    Accessibility.Internal => "internal",
                    Accessibility.ProtectedOrInternal => "private protected",
                    Accessibility.Public => "public",
                    _ => throw new ArgumentException(),
                });
                source.Append(" ");

                // Readonly
                if (structSymbol.IsReadOnly) source.Append("readonly ");

                // End the type declaration
                source.Append("partial struct");
                source.AppendLine("{");

                // Constructor
                source.AppendLine($"public {structSymbol.Name}(");

                // Parameters
                var fieldSymbols = structSymbol.GetMembers().Where(symbol => symbol.Kind == SymbolKind.Field).Cast<IFieldSymbol>().ToImmutableArray();

                source.AppendLine(string.Join($",{Environment.NewLine}", fieldSymbols.Select(field => $"{field.Type} {field.Name}")));

                source.AppendLine(") { }");
            }
        }

        /// <summary>
        /// Created on demand before each generation pass
        /// </summary>
        private sealed class SyntaxReceiver : ISyntaxReceiver
        {
            /// <summary>
            /// Gets the list of candidate struct declarations.
            /// </summary>
            public List<StructDeclarationSyntax> CandidateTypes { get; } = new();

            /// <inheritdoc/>
            public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
            {
                // Track all structs with at least one attribute
                if (syntaxNode is StructDeclarationSyntax structDeclarationSyntax &&
                    structDeclarationSyntax.AttributeLists.Count > 0)
                {
                    CandidateTypes.Add(structDeclarationSyntax);
                }
            }
        }
    }
}

