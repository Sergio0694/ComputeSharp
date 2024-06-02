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

        /// <summary>
        /// Checks whether the generated code has to directly reference the old property value.
        /// </summary>
        /// <param name="propertySymbol">The input <see cref="IPropertySymbol"/> instance to process.</param>
        /// <returns>Whether the generated code needs direct access to the old property value.</returns>
        public static bool IsOldPropertyValueDirectlyReferenced(IPropertySymbol propertySymbol)
        {
            // Check On<PROPERTY_NAME>Changing(<PROPERTY_TYPE> oldValue, <PROPERTY_TYPE> newValue) first
            foreach (ISymbol symbol in propertySymbol.ContainingType.GetMembers($"On{propertySymbol.Name}Changing"))
            {
                // No need to be too specific as we're not expecting false positives (which also wouldn't really
                // cause any problems anyway, just produce slightly worse codegen). Just checking the number of
                // parameters is good enough, and keeps the code very simple and cheap to run.
                if (symbol is IMethodSymbol { Parameters.Length: 2 })
                {
                    return true;
                }
            }

            // Do the same for On<PROPERTY_NAME>Changed(<PROPERTY_TYPE> oldValue, <PROPERTY_TYPE> newValue)
            foreach (ISymbol symbol in propertySymbol.ContainingType.GetMembers($"On{propertySymbol.Name}Changed"))
            {
                if (symbol is IMethodSymbol { Parameters.Length: 2 })
                {
                    return true;
                }
            }

            return false;
        }
    }
}