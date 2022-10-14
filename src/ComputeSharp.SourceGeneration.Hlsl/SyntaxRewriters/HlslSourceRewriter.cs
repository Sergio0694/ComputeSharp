using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Mappings;
using ComputeSharp.SourceGeneration.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Operations;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace ComputeSharp.SourceGeneration.SyntaxRewriters;

/// <summary>
/// A base <see cref="CSharpSyntaxRewriter"/> type that processes C# source to convert to HLSL compliant code.
/// This class contains only the shared logic for all derived HLSL source rewriters.
/// </summary>
internal abstract partial class HlslSourceRewriter : CSharpSyntaxRewriter
{
    /// <summary>
    /// An array with the <c>'.'</c> and <c>'E'</c> characters.
    /// </summary>
    private static readonly char[] FloatLiteralSpecialCharacters = { '.', 'E' };

    /// <summary>
    /// The <see cref="SemanticModelProvider"/> instance with semantic info on the target syntax tree.
    /// </summary>
    protected readonly SemanticModelProvider SemanticModel;

    /// <summary>
    /// The collection of discovered custom types.
    /// </summary>
    protected readonly ICollection<INamedTypeSymbol> DiscoveredTypes;

    /// <summary>
    /// The collection of discovered constant definitions.
    /// </summary>
    protected readonly IDictionary<IFieldSymbol, string> ConstantDefinitions;

    /// <summary>
    /// The collection of produced <see cref="DiagnosticInfo"/> instances.
    /// </summary>
    protected readonly ImmutableArrayBuilder<DiagnosticInfo> Diagnostics;

    /// <summary>
    /// Creates a new <see cref="HlslSourceRewriter"/> instance with the specified parameters.
    /// </summary>
    /// <param name="semanticModel">The <see cref="Microsoft.CodeAnalysis.SemanticModel"/> instance for the target syntax tree.</param>
    /// <param name="discoveredTypes">The set of discovered custom types.</param>
    /// <param name="constantDefinitions">The collection of discovered constant definitions.</param>
    /// <param name="diagnostics">The collection of produced <see cref="DiagnosticInfo"/> instances.</param>
    protected HlslSourceRewriter(
        SemanticModelProvider semanticModel,
        ICollection<INamedTypeSymbol> discoveredTypes,
        IDictionary<IFieldSymbol, string> constantDefinitions,
        ImmutableArrayBuilder<DiagnosticInfo> diagnostics)
    {
        SemanticModel = semanticModel;
        DiscoveredTypes = discoveredTypes;
        ConstantDefinitions = constantDefinitions;
        Diagnostics = diagnostics;
    }

    /// <inheritdoc/>
    public override SyntaxNode VisitCastExpression(CastExpressionSyntax node)
    {
        var updatedNode = (CastExpressionSyntax)base.VisitCastExpression(node)!;

        return updatedNode.ReplaceAndTrackType(updatedNode.Type, node.Type, SemanticModel.For(node), DiscoveredTypes);
    }

    /// <inheritdoc/>
    public override SyntaxNode? VisitVariableDeclaration(VariableDeclarationSyntax node)
    {
        var updatedNode = (VariableDeclarationSyntax)base.VisitVariableDeclaration(node)!;

        if (SemanticModel.For(node).GetTypeInfo(node.Type).Type is ITypeSymbol { IsUnmanagedType: false } type)
        {
            Diagnostics.Add(InvalidObjectDeclaration, node, type);
        }

        // If var is used, replace it with the explicit type
        if (updatedNode.Type is IdentifierNameSyntax { IsVar: true })
        {
            updatedNode = updatedNode.ReplaceAndTrackType(updatedNode.Type, node.Type, SemanticModel.For(node), DiscoveredTypes);
        }

        return updatedNode;
    }

    /// <inheritdoc/>
    public override SyntaxNode VisitObjectCreationExpression(ObjectCreationExpressionSyntax node)
    {
        var updatedNode = (ObjectCreationExpressionSyntax)base.VisitObjectCreationExpression(node)!;

        if (SemanticModel.For(node).GetTypeInfo(node).Type is ITypeSymbol { IsUnmanagedType: false } type)
        {
            Diagnostics.Add(InvalidObjectCreationExpression, node, type);
        }

        updatedNode = updatedNode.ReplaceAndTrackType(updatedNode.Type, node, SemanticModel.For(node), DiscoveredTypes);

        // New objects use the default HLSL cast syntax, eg. (float4)0
        if (updatedNode.ArgumentList!.Arguments.Count == 0)
        {
            return CastExpression(updatedNode.Type, LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0)));
        }

        // Add explicit casts for matrix constructors to help the overload resolution
        if (SemanticModel.For(node).GetTypeInfo(node).Type is ITypeSymbol matrixType &&
            HlslKnownTypes.IsMatrixType(matrixType.GetFullMetadataName()))
        {
            for (int i = 0; i < node.ArgumentList!.Arguments.Count; i++)
            {
                IArgumentOperation argumentOperation = (IArgumentOperation)SemanticModel.For(node).GetOperation(node.ArgumentList.Arguments[i])!;
                INamedTypeSymbol elementType = (INamedTypeSymbol)argumentOperation.Parameter!.Type;

                updatedNode = updatedNode.ReplaceNode(
                    updatedNode.ArgumentList!.Arguments[i].Expression,
                    CastExpression(IdentifierName(HlslKnownTypes.GetMappedName(elementType)), updatedNode.ArgumentList.Arguments[i].Expression));
            }
        }

        return InvocationExpression(updatedNode.Type, updatedNode.ArgumentList!);
    }

    /// <inheritdoc/>
    public override SyntaxNode VisitImplicitObjectCreationExpression(ImplicitObjectCreationExpressionSyntax node)
    {
        var updatedNode = (ImplicitObjectCreationExpressionSyntax)base.VisitImplicitObjectCreationExpression(node)!;

        if (SemanticModel.For(node).GetTypeInfo(node).Type is ITypeSymbol { IsUnmanagedType: false } type)
        {
            Diagnostics.Add(InvalidObjectCreationExpression, node, type);
        }

        TypeSyntax explicitType = IdentifierName("").ReplaceAndTrackType(node, SemanticModel.For(node), DiscoveredTypes);

        // Mutate the syntax like with explicit object creation expressions
        if (updatedNode.ArgumentList!.Arguments.Count == 0)
        {
            return CastExpression(explicitType, LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0)));
        }

        // Add explicit casts like with the explicit object creation expressions above
        if (SemanticModel.For(node).GetTypeInfo(node).Type is ITypeSymbol matrixType &&
            HlslKnownTypes.IsMatrixType(matrixType.GetFullMetadataName()))
        {
            for (int i = 0; i < node.ArgumentList.Arguments.Count; i++)
            {
                IArgumentOperation argumentOperation = (IArgumentOperation)SemanticModel.For(node).GetOperation(node.ArgumentList.Arguments[i])!;
                INamedTypeSymbol elementType = (INamedTypeSymbol)argumentOperation.Parameter!.Type;

                updatedNode = updatedNode.ReplaceNode(
                    updatedNode.ArgumentList.Arguments[i].Expression,
                    CastExpression(IdentifierName(HlslKnownTypes.GetMappedName(elementType)), updatedNode.ArgumentList.Arguments[i].Expression));
            }
        }

        return InvocationExpression(explicitType, updatedNode.ArgumentList);
    }

    /// <inheritdoc/>
    public override SyntaxNode VisitDefaultExpression(DefaultExpressionSyntax node)
    {
        var updatedNode = (DefaultExpressionSyntax)base.VisitDefaultExpression(node)!;

        updatedNode = updatedNode.ReplaceAndTrackType(updatedNode.Type, node.Type, SemanticModel.For(node), DiscoveredTypes);

        // A default expression becomes (T)0 in HLSL
        return CastExpression(updatedNode.Type, LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0)));
    }

    /// <inheritdoc/>
    public override SyntaxNode? VisitLiteralExpression(LiteralExpressionSyntax node)
    {
        var updatedNode = (LiteralExpressionSyntax)base.VisitLiteralExpression(node)!;

        if (updatedNode.IsKind(SyntaxKind.DefaultLiteralExpression))
        {
            TypeSyntax type = node.TrackType(SemanticModel.For(node), DiscoveredTypes);

            // Same HLSL-style expression in the form (T)0
            return CastExpression(type, LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0)));
        }
        else if (updatedNode.IsKind(SyntaxKind.NumericLiteralExpression) &&
                 SemanticModel.For(node).GetOperation(node) is ILiteralOperation operation &&
                 operation.Type is INamedTypeSymbol type)
        {
            // If the expression is a literal floating point value, we need to ensure the proper suffixes are
            // used in the HLSL representation. Floating point values accept either f or F, but they don't work
            // when the literal doesn't contain a decimal point. Since 32 bit floating point values are the default
            // in HLSL, we can remove the suffix entirely. As for 64 bit values, we simply use the 'L' suffix.
            if (type.SpecialType == SpecialType.System_Single)
            {
                string literal = updatedNode.Token.ValueText;

                // If the numeric literal is neither a decimal nor an exponential, add the ".0" suffix
                if (literal.IndexOfAny(FloatLiteralSpecialCharacters) == -1)
                {
                    literal += ".0";
                }

                return updatedNode.WithToken(Literal(literal, 0f));
            }
            else if (type.SpecialType == SpecialType.System_Double)
            {
                string literal = updatedNode.Token.ValueText;

                // If the numeric literal is neither a decimal nor an exponential, add the ".0L" suffix.
                // This is necessary because otherwise integer literals would actually be of type long.
                if (literal.IndexOfAny(FloatLiteralSpecialCharacters) == -1)
                {
                    literal += ".0L";
                }
                else
                {
                    literal += "L";
                }

                return updatedNode.WithToken(Literal(literal, 0d));
            }
        }
        else if (updatedNode.IsKind(SyntaxKind.StringLiteralExpression))
        {
            Diagnostics.Add(StringLiteralExpression, node);
        }

        return updatedNode;
    }

    /// <inheritdoc/>
    public override SyntaxNode? VisitElementAccessExpression(ElementAccessExpressionSyntax node)
    {
        var updatedNode = (ElementAccessExpressionSyntax)base.VisitElementAccessExpression(node)!;

        if (SemanticModel.For(node).GetOperation(node) is IPropertyReferenceOperation operation)
        {
            string propertyName = operation.Property.GetFullMetadataName();

            // Rewrite texture resource indices taking individual indices a vector argument, as per HLSL spec.
            // For instance: texture[ThreadIds.X, ThreadIds.Y] will be rewritten as texture[int2(ThreadIds.X, ThreadIds.Y)].
            if (HlslKnownProperties.TryGetMappedResourceIndexerTypeName(propertyName, out string? mapping))
            {
                var index = InvocationExpression(IdentifierName(mapping!), ArgumentList(updatedNode.ArgumentList.Arguments));

                return updatedNode.WithArgumentList(BracketedArgumentList(SingletonSeparatedList(Argument(index))));
            }

            // If the current property is a swizzled matrix indexer, ensure all the arguments are constants, and rewrite
            // the property access to the corresponding HLSL syntax. For instance, m[M11, M12] will become m._m00_m01.
            if (HlslKnownProperties.IsKnownMatrixIndexer(propertyName))
            {
                bool isValid = true;

                // Validate the arguments
                foreach (ArgumentSyntax argument in node.ArgumentList.Arguments)
                {
                    if (SemanticModel.For(node).GetOperation(argument.Expression) is not IFieldReferenceOperation fieldReference ||
                        !HlslKnownProperties.IsKnownMatrixIndex(fieldReference.Field.GetFullMetadataName()))
                    {
                        Diagnostics.Add(NonConstantMatrixSwizzledIndex, argument);

                        isValid = false;
                    }
                }

                if (isValid)
                {
                    // Rewrite the indexer as a property access
                    string hlslPropertyName = string.Join("",
                        from argument in node.ArgumentList.Arguments
                        let fieldReference = (IFieldReferenceOperation)SemanticModel.For(node).GetOperation(argument.Expression)!
                        let fieldName = fieldReference.Field.Name
                        let row = (char)(fieldName[1] - 1)
                        let column = (char)(fieldName[2] - 1)
                        select $"_m{row}{column}");

                    return MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        updatedNode.Expression,
                        IdentifierName(hlslPropertyName));
                }
            }
        }

        return updatedNode;
    }

    /// <inheritdoc/>
    public override SyntaxNode? VisitAssignmentExpression(AssignmentExpressionSyntax node)
    {
        var updatedNode = (AssignmentExpressionSyntax)base.VisitAssignmentExpression(node)!;

        if (SemanticModel.For(node).GetOperation(node) is ICompoundAssignmentOperation { OperatorMethod: { ContainingType.ContainingNamespace.Name: "ComputeSharp" } method })
        {
            // If the compound assignment is using an HLSL operator, replace the expression with an invocation and assignment.
            // That is, do the following transformation:
            //
            // x *= y => x = <INTRINSIC>(x, y)
            if (HlslKnownOperators.TryGetMappedName(method.GetFullMetadataName(), method.Parameters.Select(static p => p.Type.Name), out string? mapped))
            {
                return
                    AssignmentExpression(
                        SyntaxKind.SimpleAssignmentExpression,
                        updatedNode.Left,
                        InvocationExpression(IdentifierName(mapped!))
                        .AddArgumentListArguments(
                            Argument(updatedNode.Left),
                            Argument(updatedNode.Right)));
            }
        }

        return updatedNode;
    }

    /// <inheritdoc/>
    public override SyntaxNode? VisitBinaryExpression(BinaryExpressionSyntax node)
    {
        var updatedNode = (BinaryExpressionSyntax)base.VisitBinaryExpression(node)!;

        // Process binary operations to see if the target operator method is an intrinsic
        if (SemanticModel.For(node).GetOperation(node) is IBinaryOperation { OperatorMethod: { ContainingType.ContainingNamespace.Name: "ComputeSharp" } method })
        {
            // If the operator is indeed an HLSL overload, replace the expression with an invocation.
            // That is, do the following transformation:
            //
            // x * y => <INTRINSIC>(x, y)
            if (HlslKnownOperators.TryGetMappedName(method.GetFullMetadataName(), method.Parameters.Select(static p => p.Type.Name), out string? mapped))
            {
                return
                    InvocationExpression(IdentifierName(mapped!))
                    .AddArgumentListArguments(
                        Argument(updatedNode.Left),
                        Argument(updatedNode.Right));
            }
        }

        return updatedNode;
    }

    /// <inheritdoc/>
    public override SyntaxNode? VisitIdentifierName(IdentifierNameSyntax node)
    {
        var updatedNode = (IdentifierNameSyntax)base.VisitIdentifierName(node)!;

        if (SemanticModel.For(node).GetOperation(node) is IFieldReferenceOperation operation &&
            operation.Field.IsConst &&
            operation.Type!.TypeKind != TypeKind.Enum)
        {
            ConstantDefinitions[operation.Field] = ((IFormattable)operation.Field.ConstantValue!).ToString(null, CultureInfo.InvariantCulture);

            var ownerTypeName = ((INamedTypeSymbol)operation.Field.ContainingSymbol).ToDisplayString().ToHlslIdentifierName();
            var constantName = $"__{ownerTypeName}__{operation.Field.Name}";

            return IdentifierName(constantName);
        }

        return updatedNode;
    }

    /// <inheritdoc/>
    public override SyntaxToken VisitToken(SyntaxToken token)
    {
        SyntaxToken updatedToken = base.VisitToken(token);

        // Replace all identifier tokens when needed, to avoid colliding with HLSL keywords
        if (updatedToken.IsKind(SyntaxKind.IdentifierToken) &&
            HlslKnownKeywords.TryGetMappedName(updatedToken.Text, out string? mapped))
        {
            return ParseToken(mapped!);
        }

        return updatedToken.WithoutTrivia();
    }
}