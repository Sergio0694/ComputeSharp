using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace ComputeSharp.SourceGeneration.Helpers;

/// <summary>
/// A type providing <see cref="SemanticModel"/> instances for nodes.
/// </summary>
/// <param name="compilation">The source <see cref="Compilation"/> instance.</param>
internal sealed class SemanticModelProvider(Compilation compilation)
{
    /// <summary>
    /// The map of loaded <see cref="SemanticModel"/> instances.
    /// </summary>
    private readonly Dictionary<SyntaxTree, SemanticModel> semanticModelsMap = [];

    /// <summary>
    /// Gets a <see cref="SemanticModel"/> instance with info on a given <see cref="SyntaxNode"/>.
    /// </summary>
    /// <param name="syntaxNode">The input <see cref="SyntaxNode"/> to get info for.</param>
    /// <returns>A <see cref="SemanticModel"/> instance containing info on <paramref name="syntaxNode"/>.</returns>
    public SemanticModel For(SyntaxNode syntaxNode)
    {
        if (!this.semanticModelsMap.TryGetValue(syntaxNode.SyntaxTree, out SemanticModel semanticModel))
        {
            semanticModel = compilation.GetSemanticModel(syntaxNode.SyntaxTree);

            this.semanticModelsMap.Add(syntaxNode.SyntaxTree, semanticModel);
        }

        return semanticModel;
    }
}