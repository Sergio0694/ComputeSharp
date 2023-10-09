using System;
using ComputeSharp.SourceGeneration.Helpers;
using Microsoft.CodeAnalysis;

namespace ComputeSharp.SourceGeneration.Extensions;

/// <summary>
/// Extension methods for <see cref="IMethodSymbol"/> types.
/// </summary>
internal static class IMethodSymbolExtensions
{
    /// <summary>
    /// Gets the fully qualified metadata name for a given <see cref="IMethodSymbol"/> instance.
    /// </summary>
    /// <param name="symbol">The input <see cref="IMethodSymbol"/> instance.</param>
    /// <param name="includeParameters">Whether or not to also include parameter types.</param>
    /// <returns>The fully qualified metadata name for <paramref name="symbol"/>.</returns>
    public static string GetFullyQualifiedMetadataName(this IMethodSymbol symbol, bool includeParameters = false)
    {
        using ImmutableArrayBuilder<char> builder = new();

        symbol.ContainingType!.AppendFullyQualifiedMetadataName(in builder);

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

                parameter.Type.AppendFullyQualifiedMetadataName(in builder);
            }

            builder.Add(')');
        }

        return builder.ToString();
    }
}