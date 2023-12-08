using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Mappings;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace ComputeSharp.SourceGeneration.SyntaxRewriters;

/// <inheritdoc cref="HlslSourceRewriter"/>
partial class HlslSourceRewriter
{
    /// <summary>
    /// Tracks the associated type for a <see cref="SyntaxNode"/> value and returns the HLSL compatible <see cref="TypeSyntax"/>.
    /// </summary>
    /// <param name="node">The input <see cref="SyntaxNode"/> to check.</param>
    /// <param name="semanticModel">The <see cref="Microsoft.CodeAnalysis.SemanticModel"/> instance with info on the input tree.</param>
    /// <returns>A <see cref="SyntaxNode"/> instance that represents a type compatible with HLSL.</returns>
    protected TypeSyntax TrackType(SyntaxNode node, SemanticModel semanticModel)
    {
        ITypeSymbol typeSymbol = semanticModel.GetTypeInfo(node, CancellationToken).Type!;

        return TrackType(typeSymbol);
    }

    /// <summary>
    /// Tracks an <see cref="ITypeSymbol"/> instance and returns the HLSL compatible <see cref="TypeSyntax"/>.
    /// </summary>
    /// <param name="typeSymbol">The input <see cref="ITypeSymbol"/> instance to process.</param>
    /// <returns>A <see cref="SyntaxNode"/> instance that represents a type compatible with HLSL.</returns>
    protected TypeSyntax TrackType(ITypeSymbol typeSymbol)
    {
        string typeName = typeSymbol.GetFullyQualifiedName();

        DiscoveredTypes.Add((INamedTypeSymbol)typeSymbol);

        if (HlslKnownTypes.TryGetMappedName(typeName, out string? mappedName))
        {
            return ParseTypeName(mappedName);
        }

        return ParseTypeName(typeName.ToHlslIdentifierName());
    }

    /// <summary>
    /// Checks a <see cref="SyntaxNode"/> value and replaces the value type to be HLSL compatible, if needed.
    /// </summary>
    /// <typeparam name="TRoot">The type of the input <see cref="TypeSyntax"/> instance.</typeparam>
    /// <param name="node">The input <see cref="SyntaxNode"/> to check and modify if needed.</param>
    /// <param name="sourceType">The source <see cref="SyntaxNode"/> to use to get type into from <paramref name="semanticModel"/>.</param>
    /// <param name="semanticModel">The <see cref="Microsoft.CodeAnalysis.SemanticModel"/> instance with info on the input tree.</param>
    /// <returns>A <see cref="SyntaxNode"/> instance that represents a type compatible with HLSL.</returns>
    protected TRoot ReplaceAndTrackType<TRoot>(TRoot node, SyntaxNode sourceType, SemanticModel semanticModel)
        where TRoot : TypeSyntax
    {
        return ReplaceAndTrackType(node, node, sourceType, semanticModel);
    }

    /// <summary>
    /// Checks a <see cref="SyntaxNode"/> value and replaces the value type to be HLSL compatible, if needed.
    /// </summary>
    /// <typeparam name="TRoot">The type of the input <see cref="SyntaxNode"/> instance.</typeparam>
    /// <param name="node">The input <see cref="SyntaxNode"/> to check and modify if needed.</param>
    /// <param name="targetType">The target <see cref="TypeSyntax"/> node to replace.</param>
    /// <param name="sourceType">The source <see cref="SyntaxNode"/> to use to get type into from <paramref name="semanticModel"/>.</param>
    /// <param name="semanticModel">The <see cref="Microsoft.CodeAnalysis.SemanticModel"/> instance with info on the input tree.</param>
    /// <returns>A <see cref="SyntaxNode"/> instance that represents a type compatible with HLSL.</returns>
    protected TRoot ReplaceAndTrackType<TRoot>(TRoot node, TypeSyntax targetType, SyntaxNode sourceType, SemanticModel semanticModel)
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
            RefTypeSyntax refType => semanticModel.GetTypeInfo(refType.Type, CancellationToken).Type!,
            PointerTypeSyntax pointerType => semanticModel.GetTypeInfo(pointerType.ElementType, CancellationToken).Type!,
            ArrayTypeSyntax arrayType => semanticModel.GetTypeInfo(arrayType.ElementType, CancellationToken).Type!,
            _ => semanticModel.GetTypeInfo(sourceType, CancellationToken).Type!
        };

        // Do nothing if the type is just void
        if (typeSymbol.SpecialType == SpecialType.System_Void)
        {
            return node;
        }

        string typeName = typeSymbol.GetFullyQualifiedName();

        DiscoveredTypes.Add((INamedTypeSymbol)typeSymbol);

        if (HlslKnownTypes.TryGetMappedName(typeName, out string? mappedName))
        {
            TypeSyntax newType = ParseTypeName(mappedName);

            return node.ReplaceNode(targetType, newType);
        }

        return node.ReplaceNode(targetType, ParseTypeName(typeName.ToHlslIdentifierName()));
    }
}