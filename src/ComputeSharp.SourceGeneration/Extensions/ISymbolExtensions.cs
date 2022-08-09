using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;

namespace ComputeSharp.SourceGeneration.Extensions;

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
        genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeParameters,
        miscellaneousOptions: SymbolDisplayMiscellaneousOptions.EscapeKeywordIdentifiers);

    /// <summary>
    /// A custom <see cref="SymbolDisplayFormat"/> instance with fully qualified style, without global:: and parameters.
    /// </summary>
    private static readonly SymbolDisplayFormat FullyQualifiedWithoutGlobalAndParametersFormat = new(
        globalNamespaceStyle: SymbolDisplayGlobalNamespaceStyle.Omitted,
        typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces,
        genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeParameters,
        miscellaneousOptions: SymbolDisplayMiscellaneousOptions.EscapeKeywordIdentifiers,
        memberOptions: SymbolDisplayMemberOptions.IncludeContainingType,
        parameterOptions: SymbolDisplayParameterOptions.None);

    /// <summary>
    /// Gets the fully qualified name for a given symbol.
    /// </summary>
    /// <param name="symbol">The input <see cref="ISymbol"/> instance.</param>
    /// <returns>The fully qualified name for <paramref name="symbol"/>.</returns>
    public static string GetFullyQualifiedName(this ISymbol symbol)
    {
        return symbol.ToDisplayString(FullyQualifiedWithoutGlobalFormat);
    }

    /// <summary>
    /// Checks whether or not a given type symbol has a specified full name.
    /// </summary>
    /// <param name="symbol">The input <see cref="ISymbol"/> instance to check.</param>
    /// <param name="name">The full name to check.</param>
    /// <returns>Whether <paramref name="symbol"/> has a full name equals to <paramref name="name"/>.</returns>
    public static bool HasFullyQualifiedName(this ISymbol symbol, string name)
    {
        return symbol.ToDisplayString(FullyQualifiedWithoutGlobalFormat) == name;
    }

    /// <summary>
    /// Gets a valid filename for a given <see cref="ITypeSymbol"/> instance.
    /// </summary>
    /// <param name="symbol">The input <see cref="ITypeSymbol"/> instance.</param>
    /// <returns>The full metadata name for <paramref name="symbol"/> that is also a valid filename.</returns>
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
    /// Checks whether or not a given symbol has an attribute with the specified full name.
    /// </summary>
    /// <param name="symbol">The input <see cref="ISymbol"/> instance to check.</param>
    /// <param name="name">The attribute name to look for.</param>
    /// <returns>Whether or not <paramref name="symbol"/> has an attribute with the specified name.</returns>
    public static bool HasAttributeWithFullyQualifiedName(this ISymbol symbol, string name)
    {
        ImmutableArray<AttributeData> attributes = symbol.GetAttributes();

        foreach (AttributeData attribute in attributes)
        {
            if (attribute.AttributeClass?.HasFullyQualifiedName(name) == true)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Tries to get an attribute with the specified full metadata name.
    /// </summary>
    /// <param name="symbol">The input <see cref="ISymbol"/> instance to check.</param>
    /// <param name="name">The attribute name to look for.</param>
    /// <param name="attributeData">The resulting attribute data, if found.</param>
    /// <returns>Whether or not <paramref name="symbol"/> has an attribute with the specified name.</returns>
    public static bool TryGetAttributeWithFullMetadataName(this ISymbol symbol, string name, out AttributeData? attributeData)
    {
        ImmutableArray<AttributeData> attributes = symbol.GetAttributes();

        foreach (AttributeData attribute in attributes)
        {
            if (attribute.AttributeClass?.GetFullMetadataName() == name)
            {
                attributeData = attribute;

                return true;
            }
        }

        attributeData = null;

        return false;
    }

    /// <summary>
    /// Calculates the effective accessibility for a given symbol.
    /// </summary>
    /// <param name="symbol">The <see cref="ISymbol"/> instance to check.</param>
    /// <returns>The effective accessibility for <paramref name="symbol"/>.</returns>
    public static Accessibility GetEffectiveAccessibility(this ISymbol symbol)
    {
        // Start by assuming it's visible
        Accessibility visibility = Accessibility.Public;

        // Handle special cases
        switch (symbol.Kind)
        {
            case SymbolKind.Alias: return Accessibility.Private;
            case SymbolKind.Parameter: return GetEffectiveAccessibility(symbol.ContainingSymbol);
            case SymbolKind.TypeParameter: return Accessibility.Private;
        }

        // Traverse the symbol hierarchy to determine the effective accessibility
        while (symbol is not null && symbol.Kind != SymbolKind.Namespace)
        {
            switch (symbol.DeclaredAccessibility)
            {
                case Accessibility.NotApplicable:
                case Accessibility.Private:
                    return Accessibility.Private;
                case Accessibility.Internal:
                case Accessibility.ProtectedAndInternal:
                    visibility = Accessibility.Internal;
                    break;
            }

            symbol = symbol.ContainingSymbol;
        }

        return visibility;
    }

    /// <summary>
    /// Checks whether or not a given symbol can be accessed from a specified assembly.
    /// </summary>
    /// <param name="symbol">The input <see cref="ISymbol"/> instance to check.</param>
    /// <param name="assembly">The assembly to check the accessibility of <paramref name="symbol"/> for.</param>
    /// <returns>Whether <paramref name="assembly"/> can access <paramref name="symbol"/>.</returns>
    public static bool CanBeAccessedFrom(this ISymbol symbol, IAssemblySymbol assembly)
    {
        Accessibility accessibility = symbol.GetEffectiveAccessibility();

        return
            accessibility == Accessibility.Public ||
            accessibility == Accessibility.Internal && symbol.ContainingAssembly.GivesAccessTo(assembly);
    }
}
