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

    /// <summary>
    /// Creates a new read-only span for a <see langword="null"/>-terminated UTF-8 string.
    /// </summary>
    /// <param name="value">The pointer to the <see langword="null"/>-terminated string of bytes.</param>
    /// <returns>A read-only span representing the specified <see langword="null"/>-terminated string, or an empty span if the pointer is <see langword="null"/>.</returns>
    /// <remarks>The returned span does not include the <see langword="null"/> terminator, nor does it validate the well-formedness of the UTF-8 data.</remarks>
    /// <exception cref="ArgumentException">The string is longer than <see cref="int.MaxValue"/>.</exception>
    public static unsafe ReadOnlySpan<byte> CreateReadOnlySpanFromNullTerminated(byte* value)
    {
        for (int i = 0; i < int.MaxValue; i++)
        {
            // Stop when the null-terminator has been found
            if (value[i] == 0)
            {
                return new(value, i);
            }
        }

        throw new ArgumentException("The string must be null-terminated.");
    }
}