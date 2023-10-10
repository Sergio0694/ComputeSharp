using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using ComputeSharp.D2D1.Interop;

namespace ComputeSharp.D2D1.WinUI.Buffers;

/// <summary>
/// A fixed buffer type containing 16 <see cref="D2D1ResourceTextureManager"/> fields.
/// </summary>
internal struct ResourceTextureManagerBuffer
{
    /// <summary>
    /// The <see cref="D2D1ResourceTextureManager"/> instance at index 0.
    /// </summary>
    public D2D1ResourceTextureManager? ResourceTextureManager0;

    /// <summary>
    /// The <see cref="D2D1ResourceTextureManager"/> instance at index 1.
    /// </summary>
    public D2D1ResourceTextureManager? ResourceTextureManager1;

    /// <summary>
    /// The <see cref="D2D1ResourceTextureManager"/> instance at index 2.
    /// </summary>
    public D2D1ResourceTextureManager? ResourceTextureManager2;

    /// <summary>
    /// The <see cref="D2D1ResourceTextureManager"/> instance at index 3.
    /// </summary>
    public D2D1ResourceTextureManager? ResourceTextureManager3;

    /// <summary>
    /// The <see cref="D2D1ResourceTextureManager"/> instance at index 4.
    /// </summary>
    public D2D1ResourceTextureManager? ResourceTextureManager4;

    /// <summary>
    /// The <see cref="D2D1ResourceTextureManager"/> instance at index 5.
    /// </summary>
    public D2D1ResourceTextureManager? ResourceTextureManager5;

    /// <summary>
    /// The <see cref="D2D1ResourceTextureManager"/> instance at index 6.
    /// </summary>
    public D2D1ResourceTextureManager? ResourceTextureManager6;

    /// <summary>
    /// The <see cref="D2D1ResourceTextureManager"/> instance at index 7.
    /// </summary>
    public D2D1ResourceTextureManager? ResourceTextureManager7;

    /// <summary>
    /// The <see cref="D2D1ResourceTextureManager"/> instance at index 8.
    /// </summary>
    public D2D1ResourceTextureManager? ResourceTextureManager8;

    /// <summary>
    /// The <see cref="D2D1ResourceTextureManager"/> instance at index 9.
    /// </summary>
    public D2D1ResourceTextureManager? ResourceTextureManager9;

    /// <summary>
    /// The <see cref="D2D1ResourceTextureManager"/> instance at index 10.
    /// </summary>
    public D2D1ResourceTextureManager? ResourceTextureManager10;

    /// <summary>
    /// The <see cref="D2D1ResourceTextureManager"/> instance at index 11.
    /// </summary>
    public D2D1ResourceTextureManager? ResourceTextureManager11;

    /// <summary>
    /// The <see cref="D2D1ResourceTextureManager"/> instance at index 12.
    /// </summary>
    public D2D1ResourceTextureManager? ResourceTextureManager12;

    /// <summary>
    /// The <see cref="D2D1ResourceTextureManager"/> instance at index 13.
    /// </summary>
    public D2D1ResourceTextureManager? ResourceTextureManager13;

    /// <summary>
    /// The <see cref="D2D1ResourceTextureManager"/> instance at index 14.
    /// </summary>
    public D2D1ResourceTextureManager? ResourceTextureManager14;

    /// <summary>
    /// The <see cref="D2D1ResourceTextureManager"/> instance at index 15.
    /// </summary>
    public D2D1ResourceTextureManager? ResourceTextureManager15;

    /// <summary>
    /// Gets a reference to the <see cref="D2D1ResourceTextureManager"/> value at a given index.
    /// </summary>
    /// <param name="index">The index of the <see cref="D2D1ResourceTextureManager"/> value to get a reference to.</param>
    /// <returns>A reference to the <see cref="D2D1ResourceTextureManager"/> value at a given index.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="index"/> is not a valid index for the current effect.</exception>
    [UnscopedRef]
    public ref D2D1ResourceTextureManager? this[int index]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            default(ArgumentOutOfRangeException).ThrowIfNotInRange(index, 0, 16);

            ref D2D1ResourceTextureManager? r0 = ref Unsafe.As<ResourceTextureManagerBuffer, D2D1ResourceTextureManager?>(ref this);
            ref D2D1ResourceTextureManager? r1 = ref Unsafe.Add(ref r0, index);

            return ref r1;
        }
    }
}