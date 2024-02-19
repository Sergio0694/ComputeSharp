// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace ComputeSharp.SourceGeneration.Helpers;

/// <summary>
/// A helper type to build hash sets of values with pooled buffers.
/// </summary>
/// <typeparam name="T">The type of items to create hash sets for.</typeparam>
internal struct ImmutableHashSetBuilder<T> : IDisposable
{
    /// <summary>
    /// The shared <see cref="ObjectPool{T}"/> instance to share <see cref="HashSet{T}"/> objects.
    /// </summary>
    private static readonly ObjectPool<HashSet<T>> SharedObjectPool = new(static () => []);

    /// <summary>
    /// The rented <see cref="HashSet{T}"/> instance to use.
    /// </summary>
    private HashSet<T>? set;

    /// <summary>
    /// Creates a new <see cref="ImmutableHashSetBuilder{T}"/> object.
    /// </summary>
    public ImmutableHashSetBuilder()
    {
        this.set = SharedObjectPool.Allocate();
    }

    /// <summary>
    /// Gets the number of elements currently written in the current instance.
    /// </summary>
    public readonly int Count
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => this.set!.Count;
    }

    /// <inheritdoc cref="ImmutableArray{T}.Builder.Add(T)"/>
    public readonly bool Add(T item)
    {
        return this.set!.Add(item);
    }

    /// <summary>
    /// Gets an <see cref="IEnumerable{T}"/> instance for the current builder.
    /// </summary>
    /// <returns>An <see cref="IEnumerable{T}"/> instance for the current builder.</returns>
    /// <remarks>
    /// The builder should not be mutated while an enumerator is in use.
    /// </remarks>
    public readonly IEnumerable<T> AsEnumerable()
    {
        return this.set!;
    }

    /// <inheritdoc cref="ImmutableArray{T}.Builder.ToImmutable"/>
    public readonly ImmutableArray<T> ToImmutable()
    {
        T[] array = [.. this.set!];

        return Unsafe.As<T[], ImmutableArray<T>>(ref array);
    }

    /// <inheritdoc cref="ImmutableArray{T}.Builder.ToArray"/>
    public readonly T[] ToArray()
    {
        return [.. this.set!];
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        HashSet<T>? writer = this.set;

        this.set = null;

        if (writer is not null)
        {
            writer.Clear();

            SharedObjectPool.Free(writer);
        }
    }
}