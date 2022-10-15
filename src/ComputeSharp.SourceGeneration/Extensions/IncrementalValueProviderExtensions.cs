// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;

namespace ComputeSharp.SourceGeneration.Extensions;

/// <summary>
/// Extension methods for the <see cref="IncrementalValueProvider{TValue}"/> type.
/// </summary>
internal static class IncrementalValueProviderExtensions
{
    /// <summary>
    /// Combines three <see cref="IncrementalValueProvider{TValue}"/> instances.
    /// </summary>
    /// <typeparam name="T1">The type of values produced by the first <see cref="IncrementalValueProvider{TValue}"/> input.</typeparam>
    /// <typeparam name="T2">The type of values produced by the second <see cref="IncrementalValueProvider{TValue}"/> input.</typeparam>
    /// <typeparam name="T3">The type of values produced by the third <see cref="IncrementalValueProvider{TValue}"/> input.</typeparam>
    /// <param name="source1">The first <see cref="IncrementalValueProvider{TValue}"/> input.</param>
    /// <param name="source2">The second <see cref="IncrementalValueProvider{TValue}"/> input.</param>
    /// <param name="source3">The third <see cref="IncrementalValueProvider{TValue}"/> input.</param>
    /// <returns>The resulting combined <see cref="IncrementalValueProvider{TValue}"/> result.</returns>
    public static IncrementalValueProvider<(T1, T2, T3)> Combine<T1, T2, T3>(
        this IncrementalValueProvider<T1> source1,
        IncrementalValueProvider<T2> source2,
        IncrementalValueProvider<T3> source3)
    {
        return
            source1
            .Combine(source2)
            .Combine(source3)
            .Select(static (items, _) => (items.Left.Left, items.Left.Right, items.Right));
    }
}