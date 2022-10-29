using System;
using ComputeSharp.SourceGeneration.Helpers;
using Microsoft.CodeAnalysis;

namespace ComputeSharp.SourceGeneration.Extensions;

/// <summary>
/// Extension methods for <see cref="IPropertySymbol"/> types.
/// </summary>
internal static class IPropertySymbolExtensions
{
    /// <summary>
    /// Gets the fully qualified metadata name for a given <see cref="IPropertySymbol"/> instance.
    /// </summary>
    /// <param name="symbol">The input <see cref="IPropertySymbol"/> instance.</param>
    /// <returns>The fully qualified metadata name for <paramref name="symbol"/>.</returns>
    public static string GetFullyQualifiedMetadataName(this IPropertySymbol symbol)
    {
        using ImmutableArrayBuilder<char> builder = ImmutableArrayBuilder<char>.Rent();

        symbol.ContainingType!.AppendFullyQualifiedMetadataName(in builder);

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

                parameter.Type.AppendFullyQualifiedMetadataName(in builder);
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
}