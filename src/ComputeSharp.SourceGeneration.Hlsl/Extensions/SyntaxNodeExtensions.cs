using System.Collections.Generic;
using System.Linq;
using ComputeSharp.SourceGeneration.Mappings;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace ComputeSharp.SourceGeneration.Extensions;

/// <inheritdoc/>
internal static partial class SyntaxNodeExtensions
{
    /// <summary>
    /// Checks a <see cref="SyntaxNode"/> value and replaces the value type to be HLSL compatible, if needed.
    /// </summary>
    /// <typeparam name="TRoot">The type of the input <see cref="TypeSyntax"/> instance.</typeparam>
    /// <param name="node">The input <see cref="SyntaxNode"/> to check and modify if needed.</param>
    /// <param name="sourceType">The source <see cref="SyntaxNode"/> to use to get type into from <paramref name="semanticModel"/>.</param>
    /// <param name="semanticModel">The <see cref="SemanticModel"/> instance with info on the input tree.</param>
    /// <param name="discoveredTypes">The collection of currently discovered types.</param>
    /// <returns>A <see cref="SyntaxNode"/> instance that represents a type compatible with HLSL.</returns>
    public static TRoot ReplaceAndTrackType<TRoot>(this TRoot node, SyntaxNode sourceType, SemanticModel semanticModel, ICollection<INamedTypeSymbol> discoveredTypes)
        where TRoot : TypeSyntax
    {
        return node.ReplaceAndTrackType(node, sourceType, semanticModel, discoveredTypes);
    }

    /// <summary>
    /// Checks a <see cref="SyntaxNode"/> value and replaces the value type to be HLSL compatible, if needed.
    /// </summary>
    /// <typeparam name="TRoot">The type of the input <see cref="SyntaxNode"/> instance.</typeparam>
    /// <param name="node">The input <see cref="SyntaxNode"/> to check and modify if needed.</param>
    /// <param name="targetType">The target <see cref="TypeSyntax"/> node to replace.</param>
    /// <param name="sourceType">The source <see cref="SyntaxNode"/> to use to get type into from <paramref name="semanticModel"/>.</param>
    /// <param name="semanticModel">The <see cref="SemanticModel"/> instance with info on the input tree.</param>
    /// <param name="discoveredTypes">The collection of currently discovered types.</param>
    /// <returns>A <see cref="SyntaxNode"/> instance that represents a type compatible with HLSL.</returns>
    public static TRoot ReplaceAndTrackType<TRoot>(this TRoot node, TypeSyntax targetType, SyntaxNode sourceType, SemanticModel semanticModel, ICollection<INamedTypeSymbol> discoveredTypes)
        where TRoot : SyntaxNode
    {
        // Skip immediately for function pointers
        if (sourceType is FunctionPointerTypeSyntax)
        {
            return node.ReplaceNode(targetType, ParseTypeName("void*"));
        }

        // Handle the various possible type kinds
        ITypeSymbol typeSymbol = sourceType switch
        {
            RefTypeSyntax refType => semanticModel.GetTypeInfo(refType.Type).Type!,
            PointerTypeSyntax pointerType => semanticModel.GetTypeInfo(pointerType.ElementType).Type!,
            _ => semanticModel.GetTypeInfo(sourceType).Type!
        };

        // Do nothing if the type is just void
        if (typeSymbol.SpecialType == SpecialType.System_Void)
        {
            return node;
        }

        string typeName = typeSymbol.GetFullyQualifiedName();

        discoveredTypes.Add((INamedTypeSymbol)typeSymbol);

        if (HlslKnownTypes.TryGetMappedName(typeName, out string? mappedName))
        {
            TypeSyntax newType = ParseTypeName(mappedName!);

            return node.ReplaceNode(targetType, newType);
        }

        return node.ReplaceNode(targetType, ParseTypeName(typeName.ToHlslIdentifierName()));
    }

    /// <summary>
    /// Tracks the associated type for a <see cref="SyntaxNode"/> value and returns the HLSL compatible <see cref="TypeSyntax"/>.
    /// </summary>
    /// <param name="node">The input <see cref="SyntaxNode"/> to check.</param>
    /// <param name="semanticModel">The <see cref="SemanticModel"/> instance with info on the input tree.</param>
    /// <param name="discoveredTypes">The collection of currently discovered types.</param>
    /// <returns>A <see cref="SyntaxNode"/> instance that represents a type compatible with HLSL.</returns>
    public static TypeSyntax TrackType(this SyntaxNode node, SemanticModel semanticModel, ICollection<INamedTypeSymbol> discoveredTypes)
    {
        ITypeSymbol typeSymbol = semanticModel.GetTypeInfo(node).Type!;
        string typeName = typeSymbol.GetFullyQualifiedName();

        discoveredTypes.Add((INamedTypeSymbol)typeSymbol);

        if (HlslKnownTypes.TryGetMappedName(typeName, out string? mappedName))
        {
            return ParseTypeName(mappedName!);
        }

        return ParseTypeName(typeName.ToHlslIdentifierName());
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