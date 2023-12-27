using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Mappings;
using ComputeSharp.SourceGeneration.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Operations;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

#pragma warning disable IDE0051

namespace ComputeSharp.SourceGeneration.SyntaxRewriters;

/// <summary>
/// A custom <see cref="CSharpSyntaxRewriter"/> type that processes C# static field to convert to HLSL static fields (possibly constant).
/// </summary>
/// <param name="semanticModel">The <see cref="SemanticModelProvider"/> instance for the target syntax tree.</param>
/// <param name="discoveredTypes">The set of discovered custom types.</param>
/// <param name="constantDefinitions">The collection of discovered constant definitions.</param>
/// <param name="staticFieldDefinitions">The collection of discovered static field definitions.</param>
/// <param name="diagnostics">The collection of produced <see cref="DiagnosticInfo"/> instances.</param>
/// <param name="token">The <see cref="CancellationToken"/> value for the current operation.</param>
internal sealed partial class StaticFieldRewriter(
    SemanticModelProvider semanticModel,
    ICollection<INamedTypeSymbol> discoveredTypes,
    IDictionary<IFieldSymbol, string> constantDefinitions,
    IDictionary<IFieldSymbol, HlslStaticField> staticFieldDefinitions,
    ImmutableArrayBuilder<DiagnosticInfo> diagnostics,
    CancellationToken token)
    : HlslSourceRewriter(semanticModel, discoveredTypes, constantDefinitions, staticFieldDefinitions, diagnostics, token)
{
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
        CancellationToken.ThrowIfCancellationRequested();

        MemberAccessExpressionSyntax updatedNode = (MemberAccessExpressionSyntax)base.VisitMemberAccessExpression(node)!;

        if (node.IsKind(SyntaxKind.SimpleMemberAccessExpression) &&
            SemanticModel.For(node).GetOperation(node, CancellationToken) is IMemberReferenceOperation operation)
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

                string ownerTypeName = ((INamedTypeSymbol)fieldOperation.Field.ContainingSymbol).ToDisplayString().ToHlslIdentifierName();
                string constantName = $"__{ownerTypeName}__{fieldOperation.Field.Name}";

                return IdentifierName(constantName);
            }

            if (HlslKnownProperties.TryGetMappedName(operation.Member.ToDisplayString(), out string? mapping))
            {
                // Allow specialized types to track the member access, if needed
                TrackKnownPropertyAccess(operation, node);

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
        InvocationExpressionSyntax updatedNode = (InvocationExpressionSyntax)base.VisitInvocationExpression(node)!;

        if (SemanticModel.For(node).GetOperation(node, CancellationToken) is IInvocationOperation operation &&
            operation.TargetMethod is IMethodSymbol method &&
            method.IsStatic)
        {
            // Rewrite HLSL intrinsic methods
            if (HlslKnownMethods.TryGetMappedName(method.GetFullyQualifiedMetadataName(), out string? mapping, out bool requiresParametersMapping))
            {
                if (requiresParametersMapping)
                {
                    mapping = HlslKnownMethods.GetMappedNameWithParameters(method.Name, method.Parameters.Select(static p => p.Type.Name));
                }

                // Handle named intrinsics (see ShaderSourceRewriter for more info)
                if (VisitKnownNamedIntrinsicInvocationExpression(node, updatedNode, mapping) is SyntaxNode namedIntrinsic)
                {
                    return namedIntrinsic;
                }

                return updatedNode.WithExpression(IdentifierName(mapping!));
            }
        }

        return updatedNode;
    }

    /// <inheritdoc/>
    public override SyntaxNode? VisitArgument(ArgumentSyntax node)
    {
        ArgumentSyntax updatedNode = (ArgumentSyntax)base.VisitArgument(node)!;

        updatedNode = updatedNode.WithRefKindKeyword(Token(SyntaxKind.None));

        return updatedNode;
    }

    /// <summary>
    /// Tracks a property access to a known HLSL property.
    /// </summary>
    /// <param name="operation">The <see cref="IMemberReferenceOperation"/> instance for the operation.</param>
    /// <param name="node">The <see cref="MemberAccessExpressionSyntax"/> instance for the operation.</param>
    private partial void TrackKnownPropertyAccess(IMemberReferenceOperation operation, MemberAccessExpressionSyntax node);

    /// <summary>
    /// Tracks a method invocation for a known HLSL method.
    /// </summary>
    /// <param name="metadataName">The metadata name of the method being invoked.</param>
    private partial void TrackKnownMethodInvocation(string metadataName);
}