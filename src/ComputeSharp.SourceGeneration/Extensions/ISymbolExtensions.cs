using System.Diagnostics.CodeAnalysis;
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
    /// Checks whether a given symbol is accessible from its containing assembly (including eg. through nested types).
    /// </summary>
    /// <param name="symbol">The input <see cref="ISymbol"/> instance.</param>
    /// <param name="compilation">The <see cref="Compilation"/> instance currently in use.</param>
    /// <returns>Whether <paramref name="symbol"/> is accessible from its containing assembly.</returns>
    public static bool IsAccessibleFromContainingAssembly(this ISymbol symbol, Compilation compilation)
    {
        // If the symbol is associated across multiple assemblies, it must be accessible
        if (symbol.ContainingAssembly is not IAssemblySymbol assemblySymbol)
        {
            return true;
        }

        return compilation.IsSymbolAccessibleWithin(symbol, assemblySymbol);
    }

    /// <summary>
    /// Checks whether a given symbol is accessible from the assembly of a given compilation (including eg. through nested types).
    /// </summary>
    /// <param name="symbol">The input <see cref="ISymbol"/> instance.</param>
    /// <param name="compilation">The <see cref="Compilation"/> instance currently in use.</param>
    /// <returns>Whether <paramref name="symbol"/> is accessible from the assembly for <paramref name="compilation"/>.</returns>
    public static bool IsAccessibleFromCompilationAssembly(this ISymbol symbol, Compilation compilation)
    {
        return compilation.IsSymbolAccessibleWithin(symbol, compilation.Assembly);
    }

    /// <summary>
    /// Gets the fully qualified name for a given symbol.
    /// </summary>
    /// <param name="symbol">The input <see cref="ISymbol"/> instance.</param>
    /// <param name="includeGlobal">Whether to include the <c>global::</c> prefix.</param>
    /// <returns>The fully qualified name for <paramref name="symbol"/>.</returns>
    public static string GetFullyQualifiedName(this ISymbol symbol, bool includeGlobal = false)
    {
        return includeGlobal
            ? symbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)
            : symbol.ToDisplayString(FullyQualifiedWithoutGlobalFormat);
    }

    /// <summary>
    /// Checks whether or not a given symbol has an attribute with the specified type.
    /// </summary>
    /// <param name="symbol">The input <see cref="ISymbol"/> instance to check.</param>
    /// <param name="typeSymbol">The <see cref="ITypeSymbol"/> instance for the attribute type to look for.</param>
    /// <returns>Whether or not <paramref name="symbol"/> has an attribute with the specified type.</returns>
    public static bool HasAttributeWithType(this ISymbol symbol, ITypeSymbol typeSymbol)
    {
        return TryGetAttributeWithType(symbol, typeSymbol, out _);
    }

    /// <summary>
    /// Tries to get an attribute with the specified type.
    /// </summary>
    /// <param name="symbol">The input <see cref="ISymbol"/> instance to check.</param>
    /// <param name="typeSymbol">The <see cref="ITypeSymbol"/> instance for the attribute type to look for.</param>
    /// <param name="attributeData">The resulting attribute, if it was found.</param>
    /// <returns>Whether or not <paramref name="symbol"/> has an attribute with the specified name.</returns>
    public static bool TryGetAttributeWithType(this ISymbol symbol, ITypeSymbol typeSymbol, [NotNullWhen(true)] out AttributeData? attributeData)
    {
        foreach (AttributeData attribute in symbol.GetAttributes())
        {
            if (SymbolEqualityComparer.Default.Equals(attribute.AttributeClass, typeSymbol))
            {
                attributeData = attribute;

                return true;
            }
        }

        attributeData = null;

        return false;
    }

    /// <summary>
    /// Tries to get an attribute with the specified fully qualified metadata name.
    /// </summary>
    /// <param name="symbol">The input <see cref="ISymbol"/> instance to check.</param>
    /// <param name="name">The attribute name to look for.</param>
    /// <param name="attributeData">The resulting attribute data, if found.</param>
    /// <returns>Whether or not <paramref name="symbol"/> has an attribute with the specified name.</returns>
    public static bool TryGetAttributeWithFullyQualifiedMetadataName(this ISymbol symbol, string name, [NotNullWhen(true)] out AttributeData? attributeData)
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
}