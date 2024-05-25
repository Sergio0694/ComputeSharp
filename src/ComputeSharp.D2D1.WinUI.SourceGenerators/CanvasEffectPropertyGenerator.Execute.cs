using System.Threading;
using ComputeSharp.SourceGeneration.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ComputeSharp.D2D1.WinUI.SourceGenerators;

/// <inheritdoc/>
partial class CanvasEffectPropertyGenerator
{
    /// <summary>
    /// A container for all the logic for <see cref="CanvasEffectPropertyGenerator"/>.
    /// </summary>
    private static partial class Execute
    {
        /// <summary>
        /// Checks whether an input syntax node is a candidate property declaration for the generator.
        /// </summary>
        /// <param name="node">The input syntax node to check.</param>
        /// <param name="token">The <see cref="CancellationToken"/> used to cancel the operation, if needed.</param>
        /// <returns>Whether <paramref name="node"/> is a candidate property declaration.</returns>
        public static bool IsCandidatePropertyDeclaration(SyntaxNode node, CancellationToken token)
        {
            // The node must be a property declaration with two accessors
            if (node is not PropertyDeclarationSyntax { AccessorList.Accessors: { Count: 2 } accessors })
            {
                return false;
            }

            // The accessors must be a get and a set (with any accessibility)
            if (accessors[0].Kind() is not (SyntaxKind.GetAccessorDeclaration or SyntaxKind.SetAccessorDeclaration) ||
                accessors[1].Kind() is not (SyntaxKind.GetAccessorDeclaration or SyntaxKind.SetAccessorDeclaration))
            {
                return false;
            }

            // The property must be in a type with a base type (as it must derive from CanvasEffect)
            return node.Parent?.IsTypeDeclarationWithOrPotentiallyWithBaseTypes<ClassDeclarationSyntax>() == true;
        }
    }
}