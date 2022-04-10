﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace ComputeSharp.SourceGeneration.Extensions;

/// <summary>
/// Extension methods for <see cref="HashCode"/>.
/// </summary>
internal static class HashCodeExtensions
{
    /// <summary>
    /// Adds all items from a given <see cref="ImmutableArray{T}"/> instance to an hashcode.
    /// </summary>
    /// <typeparam name="T">The type of items to hash.</typeparam>
    /// <param name="hashCode">The target <see cref="HashCode"/> instance.</param>
    /// <param name="items">The input items to hash.</param>
    public static void AddRange<T>(this ref HashCode hashCode, ImmutableArray<T> items)
    {
        foreach (T item in items)
        {
            hashCode.Add(item);
        }
    }

    /// <summary>
    /// Adds all items from a given <see cref="ImmutableArray{T}"/> instance to an hashcode.
    /// </summary>
    /// <typeparam name="T">The type of items to hash.</typeparam>
    /// <param name="hashCode">The target <see cref="HashCode"/> instance.</param>
    /// <param name="comparer">A comparer to get hashcodes for <typeparamref name="T"/> items.</param>
    /// <param name="items">The input items to hash.</param>
    public static void AddRange<T>(this ref HashCode hashCode, ImmutableArray<T> items, IEqualityComparer<T> comparer)
    {
        foreach (T item in items)
        {
            hashCode.Add(item, comparer);
        }
    }
}
