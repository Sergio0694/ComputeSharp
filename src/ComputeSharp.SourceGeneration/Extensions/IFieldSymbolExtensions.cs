using System;
using ComputeSharp.SourceGeneration.Helpers;
using Microsoft.CodeAnalysis;

namespace ComputeSharp.SourceGeneration.Extensions;

/// <summary>
/// Extension methods for <see cref="IFieldSymbol"/> types.
/// </summary>
internal static class IFieldSymbolExtensions
{
    /// <summary>
    /// Gets the fully qualified metadata name for a given <see cref="IFieldSymbol"/> instance.
    /// </summary>
    /// <param name="symbol">The input <see cref="IFieldSymbol"/> instance.</param>
    /// <returns>The fully qualified metadata name for <paramref name="symbol"/>.</returns>
    public static string GetFullyQualifiedMetadataName(this IFieldSymbol symbol)
    {
        using ImmutableArrayBuilder<char> builder = ImmutableArrayBuilder<char>.Rent();

        symbol.ContainingType!.AppendFullyQualifiedMetadataName(in builder);

        builder.Add('.');
        builder.AddRange(symbol.Name.AsSpan());

        return builder.ToString();
    }
}