using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Mappings;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Operations;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace ComputeSharp.D2D1.SourceGenerators.SyntaxRewriters;

/// <summary>
/// A custom <see cref="CSharpSyntaxRewriter"/> type that processes C# static field to convert to HLSL static fields (possibly constant).
/// </summary>
internal sealed class StaticFieldRewriter : HlslSourceRewriter
{
    /// <summary>
    /// Creates a new <see cref="StaticFieldRewriter"/> instance with the specified parameters.
    /// </summary>
    /// <param name="semanticModel">The <see cref="SemanticModelProvider"/> instance for the target syntax tree.</param>
    /// <param name="discoveredTypes">The set of discovered custom types.</param>
    /// <param name="constantDefinitions">The collection of discovered constant definitions.</param>
    /// <param name="diagnostics">The collection of produced <see cref="Diagnostic"/> instances.</param>
    public StaticFieldRewriter(
        SemanticModelProvider semanticModel,
        ICollection<INamedTypeSymbol> discoveredTypes,
        IDictionary<IFieldSymbol, string> constantDefinitions,
        ImmutableArray<Diagnostic>.Builder diagnostics)
        : base(semanticModel, discoveredTypes, constantDefinitions, diagnostics)
    {
    }

    /// <inheritdoc cref="CSharpSyntaxRewriter.Visit(SyntaxNode?)"/>
    public ExpressionSyntax? Visit(VariableDeclaratorSyntax? node)
    {
        if (node?.Initializer is EqualsValueClauseSyntax fieldInitializer)
        {
            return ((EqualsValueClauseSyntax)Visit(fieldInitializer))!.Value;
        }

        return null;
    }

    /// <inheritdoc/>
    public override SyntaxNode VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
    {
        var updatedNode = (MemberAccessExpressionSyntax)base.VisitMemberAccessExpression(node)!;

        if (node.IsKind(SyntaxKind.SimpleMemberAccessExpression) &&
            SemanticModel.For(node).GetOperation(node) is IMemberReferenceOperation operation)
        {
            // Track and replace constants
            if (operation is IFieldReferenceOperation fieldOperation &&
                fieldOperation.Field.IsConst &&
                fieldOperation.Type!.TypeKind != TypeKind.Enum)
            {
                if (HlslKnownFields.TryGetMappedName(fieldOperation.Member.ToDisplayString(), out string? constantLiteral))
                {
                    return ParseExpression(constantLiteral!);
                }

                ConstantDefinitions[fieldOperation.Field] = ((IFormattable)fieldOperation.Field.ConstantValue!).ToString(null, CultureInfo.InvariantCulture);

                var ownerTypeName = ((INamedTypeSymbol)fieldOperation.Field.ContainingSymbol).ToDisplayString().ToHlslIdentifierName();
                var constantName = $"__{ownerTypeName}__{fieldOperation.Field.Name}";

                return IdentifierName(constantName);
            }

            if (HlslKnownProperties.TryGetMappedName(operation.Member.ToDisplayString(), out string? mapping))
            {
                // Rewrite static and instance mapped members
                return operation.Member.IsStatic switch
                {
                    true => ParseExpression(mapping!),
                    false => updatedNode.WithName(IdentifierName(mapping!))
                };
            }
        }

        return updatedNode;
    }

    /// <inheritdoc/>
    public override SyntaxNode? VisitInvocationExpression(InvocationExpressionSyntax node)
    {
        var updatedNode = (InvocationExpressionSyntax)base.VisitInvocationExpression(node)!;

        if (SemanticModel.For(node).GetOperation(node) is IInvocationOperation operation &&
            operation.TargetMethod is IMethodSymbol method &&
            method.IsStatic)
        {
            string metadataName = method.GetFullMetadataName();

            // Rewrite HLSL intrinsic methods
            if (HlslKnownMethods.TryGetMappedName(metadataName, out string? mapping))
            {
                if (HlslKnownMethods.NeedsD2DRequiresScenePositionAttribute(metadataName))
                {
                    NeedsD2DRequiresScenePositionAttribute = true;
                }

                return updatedNode.WithExpression(ParseExpression(mapping!));
            }
        }

        return updatedNode;
    }

    /// <inheritdoc/>
    public override SyntaxNode? VisitArgument(ArgumentSyntax node)
    {
        var updatedNode = (ArgumentSyntax)base.VisitArgument(node)!;

        updatedNode = updatedNode.WithRefKindKeyword(Token(SyntaxKind.None));

        return updatedNode;
    }
}
