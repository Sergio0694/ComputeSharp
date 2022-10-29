using System;
using System.Linq;
using ComputeSharp.SourceGeneration.Helpers;
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
    /// Checks whether or not a given type symbol has a specified fully qualified metadata name.
    /// </summary>
    /// <param name="symbol">The input <see cref="ITypeSymbol"/> instance to check.</param>
    /// <param name="name">The full name to check.</param>
    /// <returns>Whether <paramref name="symbol"/> has a full name equals to <paramref name="name"/>.</returns>
    public static bool HasFullyQualifiedMetadataName(this ITypeSymbol symbol, string name)
    {
        using ImmutableArrayBuilder<char> builder = ImmutableArrayBuilder<char>.Rent();

        AppendFullyQualifiedMetadataName(symbol, in builder);

        return builder.WrittenSpan.SequenceEqual(name.AsSpan());
    }

    /// <summary>
    /// Gets the fully qualified metadata name for a given <see cref="ITypeSymbol"/> instance.
    /// </summary>
    /// <param name="symbol">The input <see cref="ITypeSymbol"/> instance.</param>
    /// <returns>The fully qualified metadata name for <paramref name="symbol"/>.</returns>
    public static string GetFullyQualifiedMetadataName(this ITypeSymbol symbol)
    {
        using ImmutableArrayBuilder<char> builder = ImmutableArrayBuilder<char>.Rent();

        AppendFullyQualifiedMetadataName(symbol, in builder);

        return builder.ToString();
    }

    /// <summary>
    /// Gets the fully qualified metadata name for a given <see cref="IMethodSymbol"/> instance.
    /// </summary>
    /// <param name="symbol">The input <see cref="IMethodSymbol"/> instance.</param>
    /// <param name="includeParameters">Whether or not to also include parameter types.</param>
    /// <returns>The fully qualified metadata name for <paramref name="symbol"/>.</returns>
    public static string GetFullyQualifiedMetadataName(this IMethodSymbol symbol, bool includeParameters = false)
    {
        using ImmutableArrayBuilder<char> builder = ImmutableArrayBuilder<char>.Rent();

        AppendFullyQualifiedMetadataName(symbol.ContainingType!, in builder);

        // Always add the method name after the containing type
        builder.Add('.');
        builder.AddRange(symbol.Name.AsSpan());

        if (includeParameters)
        {
            builder.Add('(');

            bool isFirstParameter = true;

            foreach (IParameterSymbol parameter in symbol.Parameters)
            {
                // Append the leading ', ' if needed
                if (isFirstParameter)
                {
                    isFirstParameter = false;
                }
                else
                {
                    builder.AddRange(", ".AsSpan());
                }

                AppendFullyQualifiedMetadataName(parameter.Type, in builder);
            }

            builder.Add(')');
        }

        return builder.ToString();
    }

    /// <summary>
    /// Gets the fully qualified metadata name for a given <see cref="IPropertySymbol"/> instance.
    /// </summary>
    /// <param name="symbol">The input <see cref="IPropertySymbol"/> instance.</param>
    /// <returns>The fully qualified metadata name for <paramref name="symbol"/>.</returns>
    public static string GetFullyQualifiedMetadataName(this IPropertySymbol symbol)
    {
        using ImmutableArrayBuilder<char> builder = ImmutableArrayBuilder<char>.Rent();

        AppendFullyQualifiedMetadataName(symbol.ContainingType!, in builder);

        if (symbol.IsIndexer)
        {
            // Add the ".this[...]" suffix (parameters will be added below)
            builder.AddRange(".this[".AsSpan());

            bool isFirstParameter = true;

            // If the property is an indexer, also includes the parameters
            foreach (IParameterSymbol parameter in symbol.Parameters)
            {
                // Append the leading ', ' if needed
                if (isFirstParameter)
                {
                    isFirstParameter = false;
                }
                else
                {
                    builder.AddRange(", ".AsSpan());
                }

                AppendFullyQualifiedMetadataName(parameter.Type, in builder);
            }

            // Add the closing bracket for ".this[...]"
            builder.Add(']');
        }
        else
        {
            // For normal properties, just append the name after the containing type
            builder.Add('.');
            builder.AddRange(symbol.Name.AsSpan());
        }

        return builder.ToString();
    }

    /// <summary>
    /// Gets the fully qualified metadata name for a given <see cref="IFieldSymbol"/> instance.
    /// </summary>
    /// <param name="symbol">The input <see cref="IFieldSymbol"/> instance.</param>
    /// <returns>The fully qualified metadata name for <paramref name="symbol"/>.</returns>
    public static string GetFullyQualifiedMetadataName(this IFieldSymbol symbol)
    {
        using ImmutableArrayBuilder<char> builder = ImmutableArrayBuilder<char>.Rent();

        AppendFullyQualifiedMetadataName(symbol.ContainingType!, in builder);

        builder.Add('.');
        builder.AddRange(symbol.Name.AsSpan());

        return builder.ToString();
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

    /// <summary>
    /// Appends the fully qualified metadata name for a given symbol to a target builder.
    /// </summary>
    /// <param name="symbol">The input <see cref="ITypeSymbol"/> instance.</param>
    /// <param name="builder">The target <see cref="ImmutableArrayBuilder{T}"/> instance.</param>
    private static void AppendFullyQualifiedMetadataName(this ITypeSymbol symbol, in ImmutableArrayBuilder<char> builder)
    {
        static void BuildFrom(ISymbol? symbol, in ImmutableArrayBuilder<char> builder)
        {
            switch (symbol)
            {
                // Namespaces that are nested also append a leading '.'
                case INamespaceSymbol { ContainingNamespace.IsGlobalNamespace: false }:
                    BuildFrom(symbol.ContainingNamespace, in builder);
                    builder.Add('.');
                    builder.AddRange(symbol.MetadataName.AsSpan());
                    break;

                // Other namespaces (ie. the one right before global) skip the leading '.'
                case INamespaceSymbol { IsGlobalNamespace: false }:
                    builder.AddRange(symbol.MetadataName.AsSpan());
                    break;

                // Types with no namespace just have their metadata name directly written
                case ITypeSymbol { ContainingSymbol: INamespaceSymbol { IsGlobalNamespace: true } }:
                    builder.AddRange(symbol.MetadataName.AsSpan());
                    break;

                // Types with a containing non-global namespace also append a leading '.'
                case ITypeSymbol { ContainingSymbol: INamespaceSymbol namespaceSymbol }:
                    BuildFrom(namespaceSymbol, in builder);
                    builder.Add('.');
                    builder.AddRange(symbol.MetadataName.AsSpan());
                    break;

                // Nested types append a leading '+'
                case ITypeSymbol { ContainingSymbol: ITypeSymbol typeSymbol }:
                    BuildFrom(typeSymbol, in builder);
                    builder.Add('+');
                    builder.AddRange(symbol.MetadataName.AsSpan());
                    break;
                default:
                    break;
            }
        }

        BuildFrom(symbol, in builder);
    }
}