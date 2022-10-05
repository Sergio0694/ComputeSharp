// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ComputeSharp.SourceGeneration.Helpers;

/// <summary>
/// A helper type to build <see cref="ImmutableArray{T}"/> instances with pooled buffers.
/// </summary>
/// <typeparam name="T">The type of items to create arrays for.</typeparam>
internal ref struct ImmutableArrayBuilder<T>
{
    /// <summary>
    /// The shared <see cref="ObjectPool{T}"/> instance to share <see cref="ImmutableArray{T}.Builder"/> objects.
    /// </summary>
    private static readonly ObjectPool<ImmutableArray<T>.Builder> sharedObjectPool = new(ImmutableArray.CreateBuilder<T>);

    /// <summary>
    /// The owner <see cref="ObjectPool{T}"/> instance.
    /// </summary>
    private readonly ObjectPool<ImmutableArray<T>.Builder> objectPool;

    /// <summary>
    /// The rented <see cref="ImmutableArray{T}.Builder"/> instance to use.
    /// </summary>
    private ImmutableArray<T>.Builder? builder;

    /// <summary>
    /// Rents a new pooled <see cref="ImmutableArray{T}.Builder"/> instance through a new <see cref="ImmutableArrayBuilder{T}"/> value.
    /// </summary>
    /// <returns>A <see cref="ImmutableArrayBuilder{T}"/> to interact with the underlying <see cref="ImmutableArray{T}.Builder"/> instance.</returns>
    public static ImmutableArrayBuilder<T> Rent()
    {
        return new(sharedObjectPool, sharedObjectPool.Allocate());
    }

    /// <summary>
    /// Creates a new <see cref="ImmutableArrayBuilder{T}"/> object with the specified parameters.
    /// </summary>
    /// <param name="objectPool"></param>
    /// <param name="builder"></param>
    private ImmutableArrayBuilder(ObjectPool<ImmutableArray<T>.Builder> objectPool, ImmutableArray<T>.Builder builder)
    {
        this.objectPool = objectPool;
        this.builder = builder;
    }

    /// <inheritdoc cref="ImmutableArray{T}.Builder.Count"/>
    public readonly int Count
    {
        get => this.builder!.Count;
    }

    /// <inheritdoc cref="ImmutableArray{T}.Builder.Add(T)"/>
    public readonly void Add(T item)
    {
        this.builder!.Add(item);
    }

    /// <summary>
    /// Adds the specified items to the end of the array.
    /// </summary>
    /// <param name="items">The items to add at the end of the array.</param>
    public readonly unsafe void AddRange(ReadOnlySpan<T> items)
    {
        if (items.IsEmpty)
        {
            return;
        }

        int offset = this.builder!.Count;

        this.builder!.Count += items.Length;

        ref T firstItem = ref Unsafe.AsRef(in this.builder!.ItemRef(offset));

        if (typeof(T) == typeof(char))
        {
            int sizeInBytes = checked(items.Length * Unsafe.SizeOf<T>());

            fixed (void* source = &Unsafe.As<T, byte>(ref MemoryMarshal.GetReference(items)))
            fixed (void* destination = &Unsafe.As<T, byte>(ref firstItem))
            {
                new ReadOnlySpan<byte>(source, sizeInBytes).CopyTo(new Span<byte>(destination, sizeInBytes));
            }
        }
        else
        {
            ref T lastItem = ref Unsafe.Add(ref firstItem, items.Length);
            ref T sourceItem = ref MemoryMarshal.GetReference(items);

            while (Unsafe.IsAddressLessThan(ref firstItem, ref lastItem))
            {
                firstItem = sourceItem;

                firstItem = ref Unsafe.Add(ref firstItem, 1);
                sourceItem = ref Unsafe.Add(ref sourceItem, 1);
            }
        }
    }

    /// <inheritdoc cref="ImmutableArray{T}.Builder.ToImmutable"/>
    public readonly ImmutableArray<T> ToImmutable()
    {
        return this.builder!.ToImmutable();
    }

    /// <inheritdoc cref="ImmutableArray{T}.Builder.ToArray"/>
    public readonly T[] ToArray()
    {
        return this.builder!.ToArray();
    }

    /// <inheritdoc/>
    public override readonly unsafe string ToString()
    {
        if (typeof(T) == typeof(char) &&
            this.builder!.Count > 0)
        {
            fixed (char* p = &Unsafe.As<T, char>(ref Unsafe.AsRef(in this.builder!.ItemRef(0))))
            {
                return new ReadOnlySpan<char>(p, this.builder!.Count).ToString();
            }
        }

        return this.builder!.ToString();
    }

    /// <inheritdoc cref="IDisposable.Dispose"/>
    public void Dispose()
    {
        ImmutableArray<T>.Builder? builder = this.builder;

        this.builder = null;

        if (builder is not null)
        {
            builder.Clear();

            this.objectPool.Free(builder);
        }
    }
}