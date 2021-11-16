using System;
using System.Runtime.CompilerServices;

namespace ComputeSharp.NetStandard.System.Runtime.InteropServices;

/// <summary>
/// A polyfill type that mirrors some methods from <see cref="MemoryMarshal"/> on .NET 6.
/// </summary>
internal static class MemoryMarshal
{
    /// <inheritdoc cref="global::System.Runtime.InteropServices.MemoryMarshal.GetReference{T}(Span{T})"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref T GetReference<T>(Span<T> span)
    {
        return ref global::System.Runtime.InteropServices.MemoryMarshal.GetReference(span);
    }

    /// <inheritdoc cref="global::System.Runtime.InteropServices.MemoryMarshal.GetReference{T}(ReadOnlySpan{T})"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref T GetReference<T>(ReadOnlySpan<T> span)
    {
        return ref global::System.Runtime.InteropServices.MemoryMarshal.GetReference(span);
    }

    /// <summary>
    /// Returns a reference to the 0th element of <paramref name="array"/>. If the array is empty, returns a reference
    /// to where the 0th element would have been stored. Such a reference may be used for pinning but must never be dereferenced.
    /// </summary>
    /// <exception cref="NullReferenceException"><paramref name="array"/> is <see langword="null"/>.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref T GetArrayDataReference<T>(T[] array)
    {
        return ref global::System.Runtime.InteropServices.MemoryMarshal.GetReference(array.AsSpan());
    }

    /// <summary>
    /// Creates a new <see cref="Span{T}"/> from a given reference.
    /// </summary>
    /// <typeparam name="T">The type of reference to wrap.</typeparam>
    /// <param name="value">The target reference.</param>
    /// <param name="length">The length of the <see cref="Span{T}"/> to create.</param>
    /// <returns>A new <see cref="Span{T}"/> wrapping <paramref name="value"/>.</returns>
    public static unsafe Span<T> CreateSpan<T>(ref T value, int length)
    {
        return new(Unsafe.AsPointer(ref value), length);
    }
}