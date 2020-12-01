using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;

namespace ComputeSharp.SourceGenerators.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="ISymbol"/> types.
    /// </summary>
    internal static class ISymbolExtensions
    {
        /// <summary>
        /// A custom <see cref="SymbolDisplayFormat"/> instance with fully qualified style, without global:: and parameters.
        /// </summary>
        private static readonly SymbolDisplayFormat FullyQualifiedWithoutGlobalAndParametersFormat = new(
                SymbolDisplayGlobalNamespaceStyle.Omitted,
                SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces,
                SymbolDisplayGenericsOptions.IncludeTypeParameters,
                memberOptions: SymbolDisplayMemberOptions.IncludeContainingType,
                parameterOptions: SymbolDisplayParameterOptions.None);

        /// <summary>
        /// Gets the full metadata name for a given <see cref="INamedTypeSymbol"/> instance.
        /// </summary>
        /// <param name="symbol">The input <see cref="INamedTypeSymbol"/> instance.</param>
        /// <returns>The full metadata name for <paramref name="symbol"/>.</returns>
        [Pure]
        public static string GetFullMetadataName(this INamedTypeSymbol symbol)
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

            return BuildFrom(symbol, new StringBuilder(256)).ToString();
        }

        /// <summary>
        /// Gets the full metadata name for a given <see cref="IMethodSymbol"/> instance.
        /// </summary>
        /// <param name="symbol">The input <see cref="IMethodSymbol"/> instance.</param>
        /// <param name="includeParameters">Whether or not to also include parameter types.</param>
        /// <returns>The full metadata name for <paramref name="symbol"/>.</returns>
        [Pure]
        public static string GetFullMetadataName(this IMethodSymbol symbol, bool includeParameters = false)
        {
            if (includeParameters)
            {
                var parameters = $"{string.Join(", ", symbol.Parameters.Select(static p => ((INamedTypeSymbol)p.Type).GetFullMetadataName()))}";

                return $"{symbol.ToDisplayString(FullyQualifiedWithoutGlobalAndParametersFormat)}({parameters})";
            }

            return symbol.ToDisplayString(FullyQualifiedWithoutGlobalAndParametersFormat);
        }

        /// <summary>
        /// Gets a valid filename for a target symbol and generator type.
        /// </summary>
        /// <typeparam name="TGenerator">The generator type processing the input symbol.</typeparam>
        /// <param name="symbol">The symbol being processed.</param>
        /// <returns>A filename in the form "[ComputeSharp]_[&lt;TGENERATOR&gt;]_[&lt;SYMBOL_FULLNAME&gt;]"</returns>
        [Pure]
        public static string GetGeneratedFileName<TGenerator>(this INamedTypeSymbol symbol)
        {
            string
                metadataName = symbol.GetFullMetadataName(),
                fixedName = metadataName.Replace('`', '-').Replace('+', '.');

            return $"[{nameof(ComputeSharp)}]_[{typeof(TGenerator).Name}]_[{fixedName}]";
        }

        /// <summary>
        /// Gets a valid filename for a target symbol and generator type.
        /// </summary>
        /// <typeparam name="TGenerator">The generator type processing the input symbol.</typeparam>
        /// <param name="symbol">The symbol being processed.</param>
        /// <returns>A filename in the form "[ComputeSharp]_[&lt;TGENERATOR&gt;]_[&lt;SYMBOL_FULLNAME&gt;]"</returns>
        [Pure]
        public static string GetGeneratedFileName<TGenerator>(this IMethodSymbol symbol)
        {
            string
                metadataName = symbol.GetFullMetadataName(true),
                fixedName = metadataName.Replace('`', '-').Replace('+', '.');

            return $"[{nameof(ComputeSharp)}]_[{typeof(TGenerator).Name}]_[{fixedName}]";
        }
    }
}
