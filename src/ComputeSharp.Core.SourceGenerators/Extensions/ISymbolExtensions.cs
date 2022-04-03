using System.Text;
using Microsoft.CodeAnalysis;

namespace ComputeSharp.Core.SourceGenerators.Extensions;

/// <summary>
/// Extension methods for <see cref="ISymbol"/> types.
/// </summary>
internal static class ISymbolExtensions
{
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
    /// Gets the full metadata name for a given <see cref="ITypeSymbol"/> instance.
    /// </summary>
    /// <param name="symbol">The input <see cref="ITypeSymbol"/> instance.</param>
    /// <returns>The full metadata name for <paramref name="symbol"/>.</returns>
    private static string GetFullMetadataName(this ITypeSymbol symbol)
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
}
