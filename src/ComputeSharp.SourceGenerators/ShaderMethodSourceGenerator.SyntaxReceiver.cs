using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ComputeSharp.SourceGenerators
{
    /// <inheritdoc/>
    public sealed partial class ShaderMethodSourceGenerator
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
                if (context.Node is MethodDeclarationSyntax { AttributeLists: { Count: > 0 } } methodDeclaration &&
                    context.SemanticModel.GetDeclaredSymbol(methodDeclaration) is IMethodSymbol methodSymbol &&
                    context.SemanticModel.Compilation.GetTypeByMetadataName(typeof(ShaderMethodAttribute).FullName) is INamedTypeSymbol attributeSymbol &&
                    methodSymbol.GetAttributes().Any(a => SymbolEqualityComparer.Default.Equals(a.AttributeClass, attributeSymbol)))
                {
                    this.gatheredInfo.Add(new Item(methodDeclaration, methodSymbol));
                }
            }

            /// <summary>
            /// A model for a group of item representing a discovered method to process.
            /// </summary>
            /// <param name="MethodDeclaration">The <see cref="MethodDeclarationSyntax"/> instance for the target method declaration.</param>
            /// <param name="MethodSymbol">The <see cref="IMethodSymbol"/> instance for <paramref name="MethodDeclaration"/>.</param>
            public sealed record Item(MethodDeclarationSyntax MethodDeclaration, IMethodSymbol MethodSymbol);
        }
    }
}

