using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace ComputeSharp.Core.SourceGenerators.Extensions;

/// <summary>
/// Extension methods for <see cref="ImmutableArray{T}"/>.
/// </summary>
internal static class ImmutableArrayExtensions
{
    /// <summary>
    /// Compares two sequences for equality.
    /// </summary>
    /// <typeparam name="T">The items to compare.</typeparam>
    /// <param name="left">The left sequence of items.</param>
    /// <param name="right">The right sequence of items.</param>
    /// <returns>Whether or not <paramref name="left"/> equals <paramref name="right"/>.</returns>
    public static bool SequenceEqual<T>(this ImmutableArray<T> left, ImmutableArray<T> right)
        where T : IEquatable<T>
    {
        return left.AsSpan().SequenceEqual(right.AsSpan());
    }

    /// <summary>
    /// Compares two sequences for equality.
    /// </summary>
    /// <typeparam name="T">The items to compare.</typeparam>
    /// <param name="left">The left sequence of items.</param>
    /// <param name="right">The right sequence of items.</param>
    /// <param name="comparer">The <see cref="IEqualityComparer{T}"/> instance used to compare <typeparamref name="T"/> items.</param>
    /// <returns>Whether or not <paramref name="left"/> equals <paramref name="right"/>.</returns>
    public static bool SequenceEqual<T>(this ImmutableArray<T> left, ImmutableArray<T> right, IEqualityComparer<T> comparer)
    {
        if (left.Length != right.Length)
        {
            return false;
        }

        for (int i = 0; i < left.Length; i++)
        {
            if (!comparer.Equals(left[i], right[i]))
            {
                return false;
            }
        }

        return true;
    }
}
