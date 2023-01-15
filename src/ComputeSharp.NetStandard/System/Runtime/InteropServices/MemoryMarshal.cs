using System;
using System.Runtime.CompilerServices;

namespace ComputeSharp.NetStandard;

/// <summary>
/// A polyfill type that mirrors some methods from <see cref="MemoryMarshal"/> on .NET 6.
/// </summary>
internal static class MemoryMarshal
{
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
}