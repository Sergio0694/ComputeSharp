using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace ComputeSharp.SourceGeneration.Helpers;

/// <summary>
/// A type providing <see cref="SemanticModel"/> instances for nodes.
/// </summary>
/// <param name="baseSemanticModel">A <see cref="SemanticModel"/> whose <see cref="SemanticModel.Compilation"/>
/// will be used to get <see cref="SemanticModel"/> instances for other syntax trees.</param>
internal sealed class SemanticModelProvider(SemanticModel baseSemanticModel)
{
    /// <summary>
    /// The map of loaded <see cref="SemanticModel"/> instances for syntax trees other than
    /// the one for <see cref="baseSemanticModel"/>.
    /// </summary>
    private Dictionary<SyntaxTree, SemanticModel>? additionalSemanticModels = null;

    /// <summary>
    /// Gets a <see cref="SemanticModel"/> instance with info on a given <see cref="SyntaxNode"/>.
    /// </summary>
    /// <param name="syntaxNode">The input <see cref="SyntaxNode"/> to get info for.</param>
    /// <returns>A <see cref="SemanticModel"/> instance containing info on <paramref name="syntaxNode"/>.</returns>
    public SemanticModel For(SyntaxNode syntaxNode)
    {
        // Reuse the base semantic model if the syntax node belongs to the same tree.
        // This will avoid creating new semantic models if the entire type's definition is in the same file.
        if (syntaxNode.SyntaxTree == baseSemanticModel.SyntaxTree)
        {
            return baseSemanticModel;
        }

        this.additionalSemanticModels ??= [];

        if (!this.additionalSemanticModels.TryGetValue(syntaxNode.SyntaxTree, out SemanticModel semanticModel))
        {
            semanticModel = baseSemanticModel.Compilation.GetSemanticModel(syntaxNode.SyntaxTree);

            this.additionalSemanticModels.Add(syntaxNode.SyntaxTree, semanticModel);
        }

        return semanticModel;
    }
}