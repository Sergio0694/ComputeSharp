using System.Diagnostics.CodeAnalysis;
using CommunityToolkit.Diagnostics;
using ComputeSharp.Resources;

namespace ComputeSharp;

/// <summary>
/// A <see langword="class"/> that contains extension methods for the <see cref="ReadBackBuffer{T}"/> type.
/// </summary>
public static class ReadBackBufferExtensions
{
    /// <summary>
    /// Reads the contents of a <see cref="StructuredBuffer{T}"/> instance and writes them into a target <see cref="ReadBackBuffer{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the buffer.</typeparam>
    /// <param name="destination">The target <see cref="StructuredBuffer{T}"/> instance to write data to.</param>
    /// <param name="source">The input <see cref="UploadBuffer{T}"/> instance to read data from.</param>
    public static void CopyFrom<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.NonPublicFields)] T>(this ReadBackBuffer<T> destination, StructuredBuffer<T> source)
        where T : unmanaged
    {
        Guard.IsNotNull(destination);
        Guard.IsNotNull(source);

        source.CopyTo(destination, 0, 0, source.Length);
    }

    /// <summary>
    /// Reads the contents of a <see cref="StructuredBuffer{T}"/> instance and writes them into a target <see cref="ReadBackBuffer{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the buffer.</typeparam>
    /// <param name="destination">The target <see cref="StructuredBuffer{T}"/> instance to write data to.</param>
    /// <param name="source">The input <see cref="UploadBuffer{T}"/> instance to read data from.</param>
    /// <param name="sourceOffset">The offset to start reading data from.</param>
    /// <param name="destinationOffset">The starting offset within <paramref name="destination"/> to write data to.</param>
    /// <param name="count">The number of items to read.</param>
    public static void CopyFrom<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.NonPublicFields)] T>(this ReadBackBuffer<T> destination, StructuredBuffer<T> source, int sourceOffset, int destinationOffset, int count)
        where T : unmanaged
    {
        Guard.IsNotNull(destination);
        Guard.IsNotNull(source);

        source.CopyTo(destination, sourceOffset, destinationOffset, count);
    }
}
