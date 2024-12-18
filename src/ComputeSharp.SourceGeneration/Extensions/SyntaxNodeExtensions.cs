using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ComputeSharp.SourceGeneration.Extensions;

/// <summary>
/// A <see langword="class"/> with some extension methods for C# syntax nodes.
/// </summary>
internal static partial class SyntaxNodeExtensions
{
    /// <summary>
    /// Checks whether a given <see cref="SyntaxNode"/> is a given type declaration with or potentially with any base types, using only syntax.
    /// </summary>
    /// <typeparam name="T">The type of declaration to check for.</typeparam>
    /// <param name="node">The input <see cref="SyntaxNode"/> to check.</param>
    /// <returns>Whether <paramref name="node"/> is a given type declaration with or potentially with any base types.</returns>
    public static bool IsTypeDeclarationWithOrPotentiallyWithBaseTypes<T>(this SyntaxNode node)
        where T : TypeDeclarationSyntax
    {
        // Immediately bail if the node is not a type declaration of the specified type
        if (node is not T typeDeclaration)
        {
            return false;
        }

        // If the base types list is not empty, the type can definitely has implemented interfaces
        if (typeDeclaration.BaseList is { Types.Count: > 0 })
        {
            return true;
        }

        // If the base types list is empty, check if the type is partial. If it is, it means
        // that there could be another partial declaration with a non-empty base types list.
        return typeDeclaration.Modifiers.Any(SyntaxKind.PartialKeyword);
    }
}