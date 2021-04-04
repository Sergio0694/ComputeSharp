using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ComputeSharp.SourceGenerators
{
    /// <inheritdoc/>
    public sealed partial class AutoConstructorGenerator
    {
        /// <summary>
        /// An <see cref="ISyntaxContextReceiver"/> that selects candidate nodes to process.
        /// </summary>
        private sealed class SyntaxReceiver : ISyntaxContextReceiver
        {
            /// <summary>
            /// The list of info gathered during exploration.
            /// </summary>
            private readonly List<Item> gatheredInfo = new();

            /// <summary>
            /// Gets the collection of gathered info to process.
            /// </summary>
            public IReadOnlyCollection<Item> GatheredInfo => this.gatheredInfo;

            /// <inheritdoc/>
            public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
            {
                if (context.Node is StructDeclarationSyntax { AttributeLists: { Count: > 0 } } structDeclaration &&
                    context.SemanticModel.GetDeclaredSymbol(structDeclaration) is INamedTypeSymbol structSymbol &&
                    context.SemanticModel.Compilation.GetTypeByMetadataName(typeof(AutoConstructorAttribute).FullName) is INamedTypeSymbol attributeSymbol &&
                    structSymbol.GetAttributes().FirstOrDefault(a => SymbolEqualityComparer.Default.Equals(a.AttributeClass, attributeSymbol)) is AttributeData attributeData &&
                    attributeData.ApplicationSyntaxReference is SyntaxReference syntaxReference &&
                    syntaxReference.GetSyntax() is AttributeSyntax attributeSyntax)
                {
                    this.gatheredInfo.Add(new Item(structDeclaration, structSymbol, attributeSyntax, attributeData));
                }
            }

            /// <summary>
            /// A model for a group of item representing a discovered type to process.
            /// </summary>
            /// <param name="StructDeclaration">The <see cref="StructDeclarationSyntax"/> instance for the target struct declaration.</param>
            /// <param name="StructSymbol">The <see cref="INamedTypeSymbol"/> instance for <paramref name="StructDeclaration"/>.</param>
            /// <param name="AttributeSyntax">The <see cref="AttributeSyntax"/> instance for the target attribute over <paramref name="StructDeclaration"/>.</param>
            /// <param name="AttributeData">The <see cref="AttributeData"/> instance for <paramref name="AttributeSyntax"/>.</param>
            public sealed record Item(
                StructDeclarationSyntax StructDeclaration,
                INamedTypeSymbol StructSymbol,
                AttributeSyntax AttributeSyntax,
                AttributeData AttributeData);
        }
    }
}

