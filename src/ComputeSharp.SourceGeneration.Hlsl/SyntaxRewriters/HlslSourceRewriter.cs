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
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace ComputeSharp.SourceGeneration.SyntaxRewriters;

/// <summary>
/// A base <see cref="CSharpSyntaxRewriter"/> type that processes C# source to convert to HLSL compliant code.
/// This class contains only the shared logic for all derived HLSL source rewriters.
/// </summary>
/// <param name="semanticModel">The <see cref="Microsoft.CodeAnalysis.SemanticModel"/> instance for the target syntax tree.</param>
/// <param name="discoveredTypes">The set of discovered custom types.</param>
/// <param name="constantDefinitions">The collection of discovered constant definitions.</param>
/// <param name="staticFieldDefinitions">The collection of discovered static field definitions.</param>
/// <param name="diagnostics">The collection of produced <see cref="DiagnosticInfo"/> instances.</param>
/// <param name="token">The <see cref="System.Threading.CancellationToken"/> value for the current operation.</param>
internal abstract partial class HlslSourceRewriter(
    SemanticModelProvider semanticModel,
    ICollection<INamedTypeSymbol> discoveredTypes,
    IDictionary<IFieldSymbol, string> constantDefinitions,
    IDictionary<IFieldSymbol, HlslStaticField> staticFieldDefinitions,
    ImmutableArrayBuilder<DiagnosticInfo> diagnostics,
    CancellationToken token) : CSharpSyntaxRewriter
{
    /// <summary>
    /// An array with the <c>'.'</c> and <c>'E'</c> characters.
    /// </summary>
    private static readonly char[] FloatLiteralSpecialCharacters = ['.', 'E'];

    /// <summary>
    /// Gets the <see cref="SemanticModelProvider"/> instance with semantic info on the target syntax tree.
    /// </summary>
    protected SemanticModelProvider SemanticModel { get; } = semanticModel;

    /// <summary>
    /// Gets the collection of discovered custom types.
    /// </summary>
    protected ICollection<INamedTypeSymbol> DiscoveredTypes { get; } = discoveredTypes;

    /// <summary>
    /// Gets the collection of discovered constant definitions.
    /// </summary>
    protected IDictionary<IFieldSymbol, string> ConstantDefinitions { get; } = constantDefinitions;

    /// <summary>
    /// Gets the collection of discovered static field definitions.
    /// </summary>
    protected IDictionary<IFieldSymbol, HlslStaticField> StaticFieldDefinitions { get; } = staticFieldDefinitions;

    /// <summary>
    /// Gets the collection of produced <see cref="DiagnosticInfo"/> instances.
    /// </summary>
    protected ImmutableArrayBuilder<DiagnosticInfo> Diagnostics { get; } = diagnostics;

    /// <summary>
    /// Gets the <see cref="System.Threading.CancellationToken"/> value for the current operation.
    /// </summary>
    protected CancellationToken CancellationToken { get; } = token;

    /// <inheritdoc/>
    public sealed override SyntaxNode VisitCastExpression(CastExpressionSyntax node)
    {
        CastExpressionSyntax updatedNode = (CastExpressionSyntax)base.VisitCastExpression(node)!;

        return ReplaceAndTrackType(updatedNode, updatedNode.Type, node.Type, SemanticModel.For(node));
    }

    /// <inheritdoc/>
    public sealed override SyntaxNode? VisitVariableDeclaration(VariableDeclarationSyntax node)
    {
        VariableDeclarationSyntax updatedNode = (VariableDeclarationSyntax)base.VisitVariableDeclaration(node)!;

        if (SemanticModel.For(node).GetTypeInfo(node.Type, CancellationToken).Type is ITypeSymbol { IsUnmanagedType: false } type)
        {
            Diagnostics.Add(InvalidObjectDeclaration, node, type);
        }

        // If var is used, replace it with the explicit type
        if (updatedNode.Type is IdentifierNameSyntax { IsVar: true })
        {
            updatedNode = ReplaceAndTrackType(updatedNode, updatedNode.Type, node.Type, SemanticModel.For(node));
        }

        return updatedNode;
    }

    /// <inheritdoc/>
    public sealed override SyntaxNode VisitObjectCreationExpression(ObjectCreationExpressionSyntax node)
    {
        ObjectCreationExpressionSyntax updatedNode = (ObjectCreationExpressionSyntax)base.VisitObjectCreationExpression(node)!;

        updatedNode = ReplaceAndTrackType(updatedNode, updatedNode.Type, node, SemanticModel.For(node));

        return VisitObjectCreationExpression(node, updatedNode, updatedNode.Type);
    }

    /// <inheritdoc/>
    public sealed override SyntaxNode VisitImplicitObjectCreationExpression(ImplicitObjectCreationExpressionSyntax node)
    {
        ImplicitObjectCreationExpressionSyntax updatedNode = (ImplicitObjectCreationExpressionSyntax)base.VisitImplicitObjectCreationExpression(node)!;
        TypeSyntax explicitType = ReplaceAndTrackType(IdentifierName(""), node, SemanticModel.For(node));

        return VisitObjectCreationExpression(node, updatedNode, explicitType);
    }

    /// <inheritdoc cref="VisitObjectCreationExpression(ObjectCreationExpressionSyntax)"/>
    /// <param name="node">The original input <see cref="BaseObjectCreationExpressionSyntax"/> instance.</param>
    /// <param name="updatedNode">The updated <see cref="BaseObjectCreationExpressionSyntax"/> instance with tweaked syntax.</param>
    /// <param name="targetType">The <see cref="TypeSyntax"/> for the object being created.</param>
    /// <returns>The rewritten <see cref="SyntaxNode"/> for the object creation expression.</returns>
    private SyntaxNode VisitObjectCreationExpression(BaseObjectCreationExpressionSyntax node, BaseObjectCreationExpressionSyntax updatedNode, TypeSyntax targetType)
    {
        CancellationToken.ThrowIfCancellationRequested();

        ITypeSymbol? typeSymbol = SemanticModel.For(node).GetTypeInfo(node, CancellationToken).Type;

        // Handle the edge case of the type being null (shouldn't really happen)
        if (typeSymbol is null)
        {
            Diagnostics.Add(InvalidObjectCreationExpression, node, "<invalid>");

            return CastExpression(targetType, LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0)));
        }

        // Emit a diagnostic if the object being created is not valid (ie. it's a managed type)
        if (!typeSymbol.IsUnmanagedType)
        {
            Diagnostics.Add(InvalidObjectCreationExpression, node, typeSymbol);

            return CastExpression(targetType, LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0)));
        }

        // Mutate the syntax like with explicit object creation expressions. This also handles object
        // initializer expressions. If those are used, the HLSL will just contain a default expression.
        // There is a diagnostic being emitted to inform the users if that path is hit. If users want
        // to create an object and immediately set some values, they should use a factory method.
        if (updatedNode is not { ArgumentList.Arguments.Count: >= 0, Initializer: null })
        {
            return CastExpression(targetType, LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0)));
        }

        string typeName = typeSymbol.GetFullyQualifiedMetadataName();

        if (HlslKnownTypes.IsKnownHlslType(typeName))
        {
            // Special when we have no arguments, ie. we're calling the default parameterless constructor.
            // We need to add this check here because for user defined types, there may be explicit constructors.
            if (updatedNode.ArgumentList.Arguments.Count == 0)
            {
                return CastExpression(targetType, LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0)));
            }

            // Add explicit casts to each individual argument
            if (HlslKnownTypes.IsMatrixType(typeName))
            {
                for (int i = 0; i < node.ArgumentList!.Arguments.Count; i++)
                {
                    IArgumentOperation argumentOperation = (IArgumentOperation)SemanticModel.For(node).GetOperation(node.ArgumentList.Arguments[i], CancellationToken)!;
                    INamedTypeSymbol elementType = (INamedTypeSymbol)argumentOperation.Parameter!.Type;

                    updatedNode = updatedNode.ReplaceNode(
                        updatedNode.ArgumentList!.Arguments[i].Expression,
                        CastExpression(IdentifierName(HlslKnownTypes.GetMappedName(elementType)), updatedNode.ArgumentList.Arguments[i].Expression));
                }
            }

            // In either case, for built-in HLSL types, we can just invoke the constructor directly
            return InvocationExpression(targetType, updatedNode.ArgumentList!);
        }

        // We're invoking a user defined constructor
        return VisitUserDefinedObjectCreationExpression(node, updatedNode, targetType);
    }

    /// <inheritdoc/>
    public sealed override SyntaxNode VisitDefaultExpression(DefaultExpressionSyntax node)
    {
        DefaultExpressionSyntax updatedNode = (DefaultExpressionSyntax)base.VisitDefaultExpression(node)!;

        updatedNode = ReplaceAndTrackType(updatedNode, updatedNode.Type, node.Type, SemanticModel.For(node));

        // A default expression becomes (T)0 in HLSL
        return CastExpression(updatedNode.Type, LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0)));
    }

    /// <inheritdoc/>
    public sealed override SyntaxNode? VisitLiteralExpression(LiteralExpressionSyntax node)
    {
        CancellationToken.ThrowIfCancellationRequested();

        LiteralExpressionSyntax updatedNode = (LiteralExpressionSyntax)base.VisitLiteralExpression(node)!;

        if (updatedNode.IsKind(SyntaxKind.DefaultLiteralExpression))
        {
            TypeSyntax type = TrackType(node, SemanticModel.For(node));

            // Same HLSL-style expression in the form (T)0
            return CastExpression(type, LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0)));
        }
        else if (updatedNode.IsKind(SyntaxKind.NumericLiteralExpression) &&
                 SemanticModel.For(node).GetOperation(node, CancellationToken) is ILiteralOperation operation &&
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
    public sealed override SyntaxNode? VisitElementAccessExpression(ElementAccessExpressionSyntax node)
    {
        CancellationToken.ThrowIfCancellationRequested();

        ElementAccessExpressionSyntax updatedNode = (ElementAccessExpressionSyntax)base.VisitElementAccessExpression(node)!;

        if (SemanticModel.For(node).GetOperation(node, CancellationToken) is IPropertyReferenceOperation operation)
        {
            string propertyName = operation.Property.GetFullyQualifiedMetadataName();

            // Rewrite texture resource indices taking individual indices a vector argument, as per HLSL spec.
            // For instance: texture[ThreadIds.X, ThreadIds.Y] will be rewritten as texture[int2(ThreadIds.X, ThreadIds.Y)].
            if (HlslKnownProperties.TryGetMappedResourceIndexerTypeName(propertyName, out string? mapping))
            {
                InvocationExpressionSyntax index = InvocationExpression(IdentifierName(mapping!), ArgumentList(updatedNode.ArgumentList.Arguments));

                return updatedNode.WithArgumentList(BracketedArgumentList(SingletonSeparatedList(Argument(index))));
            }

            // If the current property is a swizzled matrix indexer, ensure all the arguments are constants, and rewrite
            // the property access to the corresponding HLSL syntax. For instance, m[M11, M12] will become m._m00_m01.
            if (HlslKnownProperties.IsKnownMatrixIndexer(propertyName))
            {
                using ImmutableArrayBuilder<string> hlslPropertyParts = new();

                // Validate the arguments and gather all the indexer parts in a single step (without using LINQ).
                // This prepares all the individual parts to combine into the HLSL property name when rewriting.
                foreach (ArgumentSyntax argument in node.ArgumentList.Arguments)
                {
                    if (SemanticModel.For(node).GetOperation(argument.Expression, CancellationToken) is not IFieldReferenceOperation fieldReference ||
                        !HlslKnownProperties.IsKnownMatrixIndex(fieldReference.Field.GetFullyQualifiedMetadataName()))
                    {
                        Diagnostics.Add(NonConstantMatrixSwizzledIndex, argument);

                        hlslPropertyParts.Clear();

                        break;
                    }

                    // We have a valid field reference, we can process it
                    string fieldName = fieldReference.Field.Name;
                    char row = (char)(fieldName[1] - 1);
                    char column = (char)(fieldName[2] - 1);

                    hlslPropertyParts.Add($"_m{row}{column}");
                }

                // If we have any property parts, it means the property access is valid.
                // So we can create the combined HLSL property and rewrite the node.
                if (hlslPropertyParts.Count > 0)
                {
                    string hlslPropertyName = string.Join("", hlslPropertyParts.AsEnumerable());

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
    public sealed override SyntaxNode? VisitAssignmentExpression(AssignmentExpressionSyntax node)
    {
        CancellationToken.ThrowIfCancellationRequested();

        AssignmentExpressionSyntax updatedNode = (AssignmentExpressionSyntax)base.VisitAssignmentExpression(node)!;

        if (SemanticModel.For(node).GetOperation(node, CancellationToken) is ICompoundAssignmentOperation { OperatorMethod: { ContainingType.ContainingNamespace.Name: "ComputeSharp" } method })
        {
            // If the compound assignment is using an HLSL operator, replace the expression with an invocation and assignment.
            // That is, do the following transformation:
            //
            // x *= y => x = <INTRINSIC>(x, y)
            if (HlslKnownOperators.TryGetMappedName(method.GetFullyQualifiedMetadataName(), method.Parameters.Select(static p => p.Type.Name), out string? mapped))
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
    public sealed override SyntaxNode? VisitBinaryExpression(BinaryExpressionSyntax node)
    {
        CancellationToken.ThrowIfCancellationRequested();

        BinaryExpressionSyntax updatedNode = (BinaryExpressionSyntax)base.VisitBinaryExpression(node)!;

        // Process binary operations to see if the target operator method is an intrinsic
        if (SemanticModel.For(node).GetOperation(node, CancellationToken) is IBinaryOperation { OperatorMethod: { ContainingType.ContainingNamespace.Name: "ComputeSharp" } method })
        {
            // If the operator is indeed an HLSL overload, replace the expression with an invocation.
            // That is, do the following transformation:
            //
            // x * y => <INTRINSIC>(x, y)
            if (HlslKnownOperators.TryGetMappedName(method.GetFullyQualifiedMetadataName(), method.Parameters.Select(static p => p.Type.Name), out string? mapped))
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
        IdentifierNameSyntax updatedNode = (IdentifierNameSyntax)base.VisitIdentifierName(node)!;

        // Only gather constants directly accessed by name. We can also pre-filter to exclude invocations
        // and member access expressions, as those will be handled separately. Doing so avoids unnecessarily
        // retrieving semantic information for every identifier, which would otherwise be fairly expensive.
        if (node.Parent is not (InvocationExpressionSyntax or MemberAccessExpressionSyntax) &&
            SemanticModel.For(node).GetOperation(node, CancellationToken) is IFieldReferenceOperation operation &&
            operation.Field.IsConst &&
            operation.Type!.TypeKind != TypeKind.Enum)
        {
            ConstantDefinitions[operation.Field] = ((IFormattable)operation.Field.ConstantValue!).ToString(null, CultureInfo.InvariantCulture);

            string ownerTypeName = ((INamedTypeSymbol)operation.Field.ContainingSymbol).ToDisplayString().ToHlslIdentifierName();
            string constantName = $"__{ownerTypeName}__{operation.Field.Name}";

            return IdentifierName(constantName);
        }

        return updatedNode;
    }

    /// <inheritdoc/>
    public sealed override SyntaxToken VisitToken(SyntaxToken token)
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

    /// <inheritdoc cref="VisitObjectCreationExpression(ObjectCreationExpressionSyntax)"/>
    /// <param name="node">The original input <see cref="BaseObjectCreationExpressionSyntax"/> instance.</param>
    /// <param name="updatedNode">The updated <see cref="BaseObjectCreationExpressionSyntax"/> instance with tweaked syntax.</param>
    /// <param name="targetType">The <see cref="TypeSyntax"/> for the object being created.</param>
    /// <returns>The rewritten <see cref="SyntaxNode"/> for the object creation expression.</returns>
    protected virtual SyntaxNode VisitUserDefinedObjectCreationExpression(
        BaseObjectCreationExpressionSyntax node,
        BaseObjectCreationExpressionSyntax updatedNode,
        TypeSyntax targetType)
    {
        // By default, constructors are not supported, so just return an empty value
        return CastExpression(targetType, LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0)));
    }

    /// <summary>
    /// Visits a known named intrinsic invocation expression.
    /// </summary>
    /// <param name="node">The original input <see cref="BaseObjectCreationExpressionSyntax"/> instance.</param>
    /// <param name="updatedNode">The updated <see cref="BaseObjectCreationExpressionSyntax"/> instance with tweaked syntax.</param>
    /// <param name="intrinsicName">The name of the intrinsic method being invoked.</param>
    /// <returns>The rewritten <see cref="SyntaxNode"/> for the invocation expression, if valid.</returns>
    /// <exception cref="NotSupportedException">Thrown if the named intrinsic is not recognized.</exception>
    protected static SyntaxNode? VisitKnownNamedIntrinsicInvocationExpression(
        InvocationExpressionSyntax node,
        InvocationExpressionSyntax updatedNode,
        string? intrinsicName)
    {
        // All named intrinsic methods start with a leading "__" prefix
        if (intrinsicName.AsSpan() is not ['_', '_', .. ReadOnlySpan<char> method])
        {
            return null;
        }

        // Handle and rewrite the current known named intrinsic.
        // This path should only ever be reached for valid ones.
        switch (method)
        {
            // 'And' invocations are rewritten as follows:
            //
            // C#:          Hlsl.And(left, right)
            // HLSL (DX12): and(left, right)
            // HLSL (D2D1): (left && right)
            case "And":
#if D3D12_SOURCE_GENERATOR
                return updatedNode.WithExpression(IdentifierName("and"));
#else
                return
                    ParenthesizedExpression(
                        BinaryExpression(
                            SyntaxKind.LogicalAndExpression,
                            updatedNode.ArgumentList.Arguments[0].Expression,
                            updatedNode.ArgumentList.Arguments[1].Expression));
#endif
            // 'Or' invocations are rewritten as follows:
            //
            // C#:          Hlsl.Or(left, right)
            // HLSL (DX12): or(left, right)
            // HLSL (D2D1): (left || right)
            case "Or":
#if D3D12_SOURCE_GENERATOR
                return updatedNode.WithExpression(IdentifierName("or"));
#else
                return
                    ParenthesizedExpression(
                        BinaryExpression(
                            SyntaxKind.LogicalOrExpression,
                            updatedNode.ArgumentList.Arguments[0].Expression,
                            updatedNode.ArgumentList.Arguments[1].Expression));
#endif
            // 'Select' invocations are rewritten as follows:
            //
            // C#:          Hlsl.Select(mask, left, right)
            // HLSL (DX12): select(mask, left, right)
            // HLSL (D2D1): (mask ? left : right)
            case "Select":
#if D3D12_SOURCE_GENERATOR
                return updatedNode.WithExpression(IdentifierName("select"));
#else
                return
                    ParenthesizedExpression(
                        ConditionalExpression(
                            updatedNode.ArgumentList.Arguments[0].Expression,
                            updatedNode.ArgumentList.Arguments[1].Expression,
                            updatedNode.ArgumentList.Arguments[2].Expression));
#endif
            default:
                throw new NotSupportedException($"""Unrecognized intrinsic "{intrinsicName}".""");
        }
    }
}