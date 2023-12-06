using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace ComputeSharp.SourceGeneration.Extensions;

/// <summary>
/// A <see langword="class"/> with some extension methods for C# syntax nodes.
/// </summary>
internal static class SyntaxNodeExtensions
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

    /// <summary>
    /// Returns a <see cref="MethodDeclarationSyntax"/> with a block body.
    /// </summary>
    /// <param name="node">The input <see cref="MethodDeclarationSyntax"/> node.</param>
    /// <returns>A node like the one in input, but always with a block body.</returns>
    public static MethodDeclarationSyntax WithBlockBody(this MethodDeclarationSyntax node)
    {
        if (node.ExpressionBody is ArrowExpressionClauseSyntax arrow)
        {
            StatementSyntax statement = node.ReturnType switch
            {
                PredefinedTypeSyntax pts when pts.Keyword.IsKind(SyntaxKind.VoidKeyword) => ExpressionStatement(arrow.Expression),
                _ => ReturnStatement(arrow.Expression)
            };

            return node
                .WithBody(Block(statement))
                .WithExpressionBody(null)
                .WithSemicolonToken(MissingToken(SyntaxKind.SemicolonToken));
        }

        return node;
    }

    /// <summary>
    /// Returns a <see cref="MethodDeclarationSyntax"/> as a method definition.
    /// </summary>
    /// <param name="node">The input <see cref="MethodDeclarationSyntax"/> node.</param>
    /// <returns>A node like the one in input, but just as a definition.</returns>
    public static MethodDeclarationSyntax AsDefinition(this MethodDeclarationSyntax node)
    {
        if (node.ExpressionBody is not null)
        {
            return node.WithExpressionBody(null).WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
        }

        return node.WithBody(null).WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
    }

    /// <summary>
    /// Returns a <see cref="LocalFunctionStatementSyntax"/> with a block body.
    /// </summary>
    /// <param name="node">The input <see cref="LocalFunctionStatementSyntax"/> node.</param>
    /// <returns>A node like the one in input, but always with a block body.</returns>
    /// <remarks>
    /// This method is the same as <see cref="WithBlockBody(MethodDeclarationSyntax)"/>, but it is necessary to
    /// duplicate the code because the two types don't have a common base type or interface that can be leveraged.
    /// </remarks>
    public static LocalFunctionStatementSyntax WithBlockBody(this LocalFunctionStatementSyntax node)
    {
        if (node.ExpressionBody is ArrowExpressionClauseSyntax arrow)
        {
            StatementSyntax statement = node.ReturnType switch
            {
                PredefinedTypeSyntax pts when pts.Keyword.IsKind(SyntaxKind.VoidKeyword) => ExpressionStatement(arrow.Expression),
                _ => ReturnStatement(arrow.Expression)
            };

            return node
                .WithBody(Block(statement))
                .WithExpressionBody(null)
                .WithSemicolonToken(MissingToken(SyntaxKind.SemicolonToken));
        }

        return node;
    }

    /// <summary>
    /// Returns a <see cref="LocalFunctionStatementSyntax"/> as a method definition.
    /// </summary>
    /// <param name="node">The input <see cref="LocalFunctionStatementSyntax"/> node.</param>
    /// <returns>A node like the one in input, but just as a definition.</returns>
    public static LocalFunctionStatementSyntax AsDefinition(this LocalFunctionStatementSyntax node)
    {
        if (node.ExpressionBody is not null)
        {
            return node.WithExpressionBody(null).WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
        }

        return node.WithBody(null).WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
    }

    /// <summary>
    /// Returns a <see cref="MethodDeclarationSyntax"/> instance with no invalid HLSL modifiers.
    /// </summary>
    /// <param name="node">The input <see cref="MethodDeclarationSyntax"/> node.</param>
    /// <returns>A node just like <paramref name="node"/> but with no invalid HLSL modifiers.</returns>
    /// <remarks>This method will only strip modifiers that are expected and allowed on HLSL methods.</remarks>
    public static MethodDeclarationSyntax WithoutInvalidHlslModifiers(this MethodDeclarationSyntax node)
    {
        static bool IsAllowedHlslModifier(SyntaxToken syntaxToken)
        {
            return syntaxToken.Kind() is not (
                SyntaxKind.PublicKeyword or
                SyntaxKind.PrivateKeyword or
                SyntaxKind.ProtectedKeyword or
                SyntaxKind.InternalKeyword or
                SyntaxKind.ReadOnlyKeyword);
        }

        return node.WithModifiers(TokenList(node.Modifiers.Where(IsAllowedHlslModifier)));
    }
}