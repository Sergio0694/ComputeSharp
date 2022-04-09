using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using ComputeSharp.D2D1Interop.SourceGenerators.Mappings;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace ComputeSharp.D2D1Interop.SourceGenerators.Extensions;

/// <summary>
/// Extension methods for <see cref="ISymbol"/> types.
/// </summary>
internal static class ISymbolExtensions
{
    /// <summary>
    /// A custom <see cref="SymbolDisplayFormat"/> instance with fully qualified style, without global::.
    /// </summary>
    public static readonly SymbolDisplayFormat FullyQualifiedWithoutGlobalFormat = new(
        globalNamespaceStyle: SymbolDisplayGlobalNamespaceStyle.Omitted,
        typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces,
        genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeParameters);

    /// <summary>
    /// A custom <see cref="SymbolDisplayFormat"/> instance with fully qualified style, without global:: and parameters.
    /// </summary>
    private static readonly SymbolDisplayFormat FullyQualifiedWithoutGlobalAndParametersFormat =
        FullyQualifiedWithoutGlobalFormat
        .WithMemberOptions(SymbolDisplayMemberOptions.IncludeContainingType)
        .WithParameterOptions(SymbolDisplayParameterOptions.None);

    /// <summary>
    /// Checks whether or not a given type symbol has a specified full name.
    /// </summary>
    /// <param name="symbol">The input <see cref="ISymbol"/> instance to check.</param>
    /// <param name="name">The full name to check.</param>
    /// <returns>Whether <paramref name="symbol"/> has a full name equals to <paramref name="name"/>.</returns>
    public static bool HasFullyQualifiedName(this ISymbol symbol, string name)
    {
        return symbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat) == name;
    }

    /// <summary>
    /// Gets the full metadata name for a given <see cref="ITypeSymbol"/> instance.
    /// </summary>
    /// <param name="symbol">The input <see cref="ITypeSymbol"/> instance.</param>
    /// <returns>The full metadata name for <paramref name="symbol"/>.</returns>
    public static string GetFullMetadataName(this ITypeSymbol symbol)
    {
        static StringBuilder BuildFrom(ISymbol? symbol, StringBuilder builder)
        {
            return symbol switch
            {
                INamespaceSymbol ns when ns.IsGlobalNamespace => builder,
                INamespaceSymbol ns when ns.ContainingNamespace is { IsGlobalNamespace: false }
                    => BuildFrom(ns.ContainingNamespace, builder.Insert(0, $".{ns.MetadataName}")),
                ITypeSymbol ts when ts.ContainingType is ISymbol containingType
                    => BuildFrom(containingType, builder.Insert(0, $"+{ts.MetadataName}")),
                ITypeSymbol ts when ts.ContainingNamespace is ISymbol containingNamespace and not INamespaceSymbol { IsGlobalNamespace: true }
                    => BuildFrom(containingNamespace, builder.Insert(0, $".{ts.MetadataName}")),
                ISymbol => BuildFrom(symbol.ContainingSymbol, builder.Insert(0, symbol.MetadataName)),
                _ => builder
            };
        }

        return BuildFrom(symbol, new StringBuilder(256)).ToString();
    }

    /// <summary>
    /// Gets the full metadata name for a given <see cref="IMethodSymbol"/> instance.
    /// </summary>
    /// <param name="symbol">The input <see cref="IMethodSymbol"/> instance.</param>
    /// <param name="includeParameters">Whether or not to also include parameter types.</param>
    /// <returns>The full metadata name for <paramref name="symbol"/>.</returns>
    public static string GetFullMetadataName(this IMethodSymbol symbol, bool includeParameters = false)
    {
        if (includeParameters)
        {
            var parameters = string.Join(", ", symbol.Parameters.Select(static p => ((INamedTypeSymbol)p.Type).GetFullMetadataName()));

            return $"{symbol.ToDisplayString(FullyQualifiedWithoutGlobalAndParametersFormat)}({parameters})";
        }

        return symbol.ToDisplayString(FullyQualifiedWithoutGlobalAndParametersFormat);
    }

    /// <summary>
    /// Gets the full metadata name for a given <see cref="IPropertySymbol"/> instance.
    /// </summary>
    /// <param name="symbol">The input <see cref="IPropertySymbol"/> instance.</param>
    /// <returns>The full metadata name for <paramref name="symbol"/>.</returns>
    public static string GetFullMetadataName(this IPropertySymbol symbol)
    {
        string declaringTypeName = symbol.ContainingType.GetFullMetadataName();

        if (symbol.IsIndexer)
        {
            var parameters = string.Join(", ", symbol.Parameters.Select(static p => ((INamedTypeSymbol)p.Type).GetFullMetadataName()));

            return $"{declaringTypeName}.this[{parameters}]";
        }

        return $"{declaringTypeName}.{symbol.Name}";
    }

    /// <summary>
    /// Gets the full metadata name for a given <see cref="IFieldSymbol"/> instance.
    /// </summary>
    /// <param name="symbol">The input <see cref="IFieldSymbol"/> instance.</param>
    /// <returns>The full metadata name for <paramref name="symbol"/>.</returns>
    public static string GetFullMetadataName(this IFieldSymbol symbol)
    {
        return $"{symbol.ContainingType.GetFullMetadataName()}.{symbol.Name}";
    }

    /// <summary>
    /// Gets a valid filename for a target symbol and generator type.
    /// </summary>
    /// <param name="symbol">The symbol being processed.</param>
    /// <returns>A filename in the form "&lt;SYMBOL_FULLNAME&gt;"</returns>
    public static string GetGeneratedFileName(this INamedTypeSymbol symbol)
    {
        return symbol.GetFullMetadataName().Replace('`', '-').Replace('+', '.');
    }

    /// <summary>
    /// Tracks an <see cref="ITypeSymbol"/> instance and returns an HLSL compatible <see cref="TypeSyntax"/> object.
    /// </summary>
    /// <param name="typeSymbol">The input <see cref="ITypeSymbol"/> instance to process.</param>
    /// <param name="discoveredTypes">The collection of currently discovered types.</param>
    /// <returns>A <see cref="SyntaxNode"/> instance that represents a type compatible with HLSL.</returns>
    public static TypeSyntax TrackType(this ITypeSymbol typeSymbol, ICollection<INamedTypeSymbol> discoveredTypes)
    {
        string typeName = typeSymbol.ToDisplayString(FullyQualifiedWithoutGlobalFormat);

        discoveredTypes.Add((INamedTypeSymbol)typeSymbol);

        if (HlslKnownTypes.TryGetMappedName(typeName, out string? mappedName))
        {
            return ParseTypeName(mappedName!);
        }

        return ParseTypeName(typeName.ToHlslIdentifierName());
    }

    /// <summary>
    /// Tries to get an attribute with the specified full name.
    /// </summary>
    /// <param name="symbol">The input <see cref="ISymbol"/> instance to check.</param>
    /// <param name="name">The attribute name to look for.</param>
    /// <param name="attributeData">The resulting attribute data, if found.</param>
    /// <returns>Whether or not <paramref name="symbol"/> has an attribute with the specified name.</returns>
    public static bool TryGetAttributeWithFullyQualifiedName(this ISymbol symbol, string name, out AttributeData? attributeData)
    {
        ImmutableArray<AttributeData> attributes = symbol.GetAttributes();

        foreach (AttributeData attribute in attributes)
        {
            if (attribute.AttributeClass?.HasFullyQualifiedName(name) == true)
            {
                attributeData = attribute;

                return true;
            }
        }

        attributeData = null;

        return false;
    }
}
