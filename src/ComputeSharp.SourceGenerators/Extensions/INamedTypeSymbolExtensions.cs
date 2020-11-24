using System.Diagnostics.Contracts;
using System.Text;
using Microsoft.CodeAnalysis;

namespace ComputeSharp.SourceGenerators.Extensions
{
    /// <summary>
    /// Extension methods for the <see cref="INamedTypeSymbol"/> type.
    /// </summary>
    internal static class INamedTypeSymbolExtensions
    {
        /// <summary>
        /// Gets the full metadata name for a given name symbol.
        /// </summary>
        /// <param name="namedTypeSymbol">The input <see cref="INamedTypeSymbol"/> instance.</param>
        /// <returns>The full metadata name for <paramref name="namedTypeSymbol"/>.</returns>
        [Pure]
        public static string GetFullMetadataName(this INamedTypeSymbol namedTypeSymbol)
        {
            static StringBuilder BuildFrom(ISymbol? symbol, StringBuilder builder)
            {
                return symbol switch
                {
                    INamespaceSymbol ns when ns.IsGlobalNamespace => builder,
                    INamespaceSymbol ns when ns.ContainingNamespace is { IsGlobalNamespace: false }
                        => BuildFrom(ns.ContainingNamespace, builder.Insert(0, $".{ns.MetadataName}")),
                    ITypeSymbol ts when ts.ContainingType is ISymbol pt => BuildFrom(pt, builder.Insert(0, $"+{ts.MetadataName}")),
                    ITypeSymbol ts when ts.ContainingNamespace is ISymbol pn => BuildFrom(pn, builder.Insert(0, $".{ts.MetadataName}")),
                    ISymbol => BuildFrom(symbol.ContainingSymbol, builder.Insert(0, symbol.MetadataName)),
                    _ => builder
                };
            }

            return BuildFrom(namedTypeSymbol, new StringBuilder(256)).ToString();
        }
    }
}
