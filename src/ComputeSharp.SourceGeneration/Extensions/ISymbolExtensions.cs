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
    /// Gets the fully qualified name for a given symbol.
    /// </summary>
    /// <param name="symbol">The input <see cref="ISymbol"/> instance.</param>
    /// <returns>The fully qualified name for <paramref name="symbol"/>.</returns>
    public static string GetFullyQualifiedName(this ISymbol symbol)
    {
        return symbol.ToDisplayString(FullyQualifiedWithoutGlobalFormat);
    }

    /// <summary>
    /// Tries to get an attribute with the specified fully qualified metadata name.
    /// </summary>
    /// <param name="symbol">The input <see cref="ISymbol"/> instance to check.</param>
    /// <param name="name">The attribute name to look for.</param>
    /// <param name="attributeData">The resulting attribute data, if found.</param>
    /// <returns>Whether or not <paramref name="symbol"/> has an attribute with the specified name.</returns>
    public static bool TryGetAttributeWithFullyQualifiedMetadataName(this ISymbol symbol, string name, out AttributeData? attributeData)
    {
        foreach (AttributeData attribute in symbol.GetAttributes())
        {
            if (attribute.AttributeClass is INamedTypeSymbol attributeSymbol &&
                attributeSymbol.HasFullyQualifiedMetadataName(name))
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
            (accessibility == Accessibility.Internal && symbol.ContainingAssembly.GivesAccessTo(assembly));
    }
}