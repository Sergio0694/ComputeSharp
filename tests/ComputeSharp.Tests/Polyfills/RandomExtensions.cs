#if !NETCOREAPP3_1_OR_GREATER

namespace System;

/// <summary>
/// A type with some helper methods for the <see cref="Random"/> class.
/// </summary>
internal static class RandomExtensions
{
    /// <summary>
    /// Fills the elements of a specified span of bytes with random numbers.
    /// </summary>
    /// <param name="random">The target random instance.</param>
    /// <param name="buffer">The array to be filled with random numbers.</param>
    public static void NextBytes(this Random random, Span<byte> buffer)
    {
        byte[] array = new byte[buffer.Length];

        random.NextBytes(array);

        array.AsSpan().CopyTo(buffer);
    }
}

#endif
