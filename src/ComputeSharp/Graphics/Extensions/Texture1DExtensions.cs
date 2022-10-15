using System;
using System.Runtime.InteropServices;
using CommunityToolkit.Diagnostics;
using ComputeSharp.Resources;

namespace ComputeSharp;

/// <summary>
/// A <see langword="class"/> that contains extension methods for the <see cref="Texture1D{T}"/> type.
/// </summary>
public static class Texture1DExtensions
{
    /// <summary>
    /// Reads the contents of the current <see cref="Texture1D{T}"/> instance and returns an array.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="Texture1D{T}"/> instance to read data from.</param>
    /// <returns>A <typeparamref name="T"/> array with the contents of the current texture.</returns>
    public static T[] ToArray<T>(this Texture1D<T> source)
        where T : unmanaged
    {
        Guard.IsNotNull(source);

        T[] data = new T[source.Width];

        source.CopyTo(data);

        return data;
    }

    /// <summary>
    /// Reads the contents of the current <see cref="Texture1D{T}"/> instance and returns an array.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="Texture1D{T}"/> instance to read data from.</param>
    /// <param name="x">The horizontal offset in the source texture.</param>
    /// <param name="width">The width of the memory area to copy.</param>
    /// <returns>A <typeparamref name="T"/> array with the contents of the current texture.</returns>
    public static unsafe T[] ToArray<T>(this Texture1D<T> source, int x, int width)
        where T : unmanaged
    {
        Guard.IsNotNull(source);
        Guard.IsGreaterThanOrEqualTo(width, 0);

        T[] data = new T[width];

        fixed (T* p = data)
        {
            source.CopyTo(new Span<T>(p, width), x, width);
        }

        return data;
    }

    /// <summary>
    /// Reads the contents of the specified range from the current <see cref="Texture1D{T}"/> instance and writes them into a target array.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="Texture1D{T}"/> instance to read data from.</param>
    /// <param name="destination">The input array to write data to.</param>
    public static void CopyTo<T>(this Texture1D<T> source, T[] destination)
        where T : unmanaged
    {
        Guard.IsNotNull(source);
        Guard.IsNotNull(destination);

        source.CopyTo(destination.AsSpan(), 0, source.Width);
    }

#if NET6_0_OR_GREATER
    /// <summary>
    /// Reads the contents of the specified range from the current <see cref="Texture1D{T}"/> instance and writes them into a target array.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="Texture1D{T}"/> instance to read data from.</param>
    /// <param name="destination">The input array to write data to.</param>
    /// <param name="x">The horizontal range of items to copy.</param>
    public static void CopyTo<T>(this Texture1D<T> source, T[] destination, Range x)
        where T : unmanaged
    {
        Guard.IsNotNull(source);
        Guard.IsNotNull(destination);

        source.CopyTo(destination.AsSpan(), x);
    }
#endif

    /// <summary>
    /// Reads the contents of the specified range from the current <see cref="Texture1D{T}"/> instance and writes them into a target array.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="Texture1D{T}"/> instance to read data from.</param>
    /// <param name="destination">The input array to write data to.</param>
    /// <param name="sourceOffsetX">The horizontal offset in the source texture.</param>
    /// <param name="width">The width of the memory area to copy.</param>
    public static void CopyTo<T>(this Texture1D<T> source, T[] destination, int sourceOffsetX, int width)
        where T : unmanaged
    {
        Guard.IsNotNull(source);
        Guard.IsNotNull(destination);

        source.CopyTo(destination.AsSpan(), sourceOffsetX, width);
    }

    /// <summary>
    /// Reads the contents of the specified range from the current <see cref="Texture1D{T}"/> instance and writes them into a target array.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="Texture1D{T}"/> instance to read data from.</param>
    /// <param name="destination">The input array to write data to.</param>
    /// <param name="destinationOffset">The starting offset within <paramref name="destination"/> to write data to.</param>
    public static void CopyTo<T>(this Texture1D<T> source, T[] destination, int destinationOffset)
        where T : unmanaged
    {
        Guard.IsNotNull(source);
        Guard.IsNotNull(destination);

        source.CopyTo(destination.AsSpan(destinationOffset), 0, source.Width);
    }

#if NET6_0_OR_GREATER
    /// <summary>
    /// Reads the contents of the specified range from the current <see cref="Texture1D{T}"/> instance and writes them into a target array.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="Texture1D{T}"/> instance to read data from.</param>
    /// <param name="destination">The input array to write data to.</param>
    /// <param name="destinationOffset">The starting offset within <paramref name="destination"/> to write data to.</param>
    /// <param name="x">The horizontal range of items to copy.</param>
    public static void CopyTo<T>(this Texture1D<T> source, T[] destination, int destinationOffset, Range x)
        where T : unmanaged
    {
        Guard.IsNotNull(source);
        Guard.IsNotNull(destination);

        source.CopyTo(destination.AsSpan(destinationOffset), x);
    }
#endif

    /// <summary>
    /// Reads the contents of the specified range from the current <see cref="Texture1D{T}"/> instance and writes them into a target array.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="Texture1D{T}"/> instance to read data from.</param>
    /// <param name="destination">The input array to write data to.</param>
    /// <param name="destinationOffset">The starting offset within <paramref name="destination"/> to write data to.</param>
    /// <param name="sourceOffsetX">The horizontal offset in the source texture.</param>
    /// <param name="width">The width of the memory area to copy.</param>
    public static void CopyTo<T>(this Texture1D<T> source, T[] destination, int destinationOffset, int sourceOffsetX, int width)
        where T : unmanaged
    {
        Guard.IsNotNull(source);
        Guard.IsNotNull(destination);

        source.CopyTo(destination.AsSpan(destinationOffset), sourceOffsetX, width);
    }

    /// <summary>
    /// Reads the contents of the current <see cref="Texture1D{T}"/> instance and writes them into a target <see cref="Span{T}"/>.
    /// The input data will be read from the start of the texture.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="Texture1D{T}"/> instance to read data from.</param>
    /// <param name="destination">The input <see cref="Span{T}"/> to write data to.</param>
    public static void CopyTo<T>(this Texture1D<T> source, Span<T> destination)
        where T : unmanaged
    {
        Guard.IsNotNull(source);

        source.CopyTo(destination, 0, source.Width);
    }

#if NET6_0_OR_GREATER
    /// <summary>
    /// Reads the contents of the specified range from the current <see cref="Texture1D{T}"/> instance and writes them into a target <see cref="Span{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="Texture1D{T}"/> instance to read data from.</param>
    /// <param name="destination">The input <see cref="Span{T}"/> to write data to.</param>
    /// <param name="x">The horizontal range of items to copy.</param>
    public static void CopyTo<T>(this Texture1D<T> source, Span<T> destination, Range x)
        where T : unmanaged
    {
        Guard.IsNotNull(source);

        (int offsetX, int width) = x.GetOffsetAndLength(source.Width);

        source.CopyTo(destination, offsetX, width);
    }
#endif

    /// <summary>
    /// Reads the contents of the specified range from the current <see cref="Texture1D{T}"/> instance and writes them into a target <see cref="Span{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="Texture1D{T}"/> instance to read data from.</param>
    /// <param name="destination">The input <see cref="Span{T}"/> to write data to.</param>
    /// <param name="sourceOffsetX">The horizontal offset in the source texture.</param>
    /// <param name="width">The width of the memory area to copy.</param>
    public static void CopyTo<T>(this Texture1D<T> source, Span<T> destination, int sourceOffsetX, int width)
        where T : unmanaged
    {
        Guard.IsNotNull(source);

        source.CopyTo(ref MemoryMarshal.GetReference(destination), destination.Length, sourceOffsetX, width);
    }

    /// <summary>
    /// Reads the contents of a <see cref="Texture1D{T}"/> instance and writes them into a target <see cref="Texture1D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="Texture1D{T}"/> instance to read data from.</param>
    /// <param name="destination">The target <see cref="Texture1D{T}"/> instance to write data to.</param>
    public static void CopyTo<T>(this Texture1D<T> source, Texture1D<T> destination)
        where T : unmanaged
    {
        Guard.IsNotNull(source);
        Guard.IsNotNull(destination);

        source.CopyTo(destination, 0, 0, source.Width);
    }

    /// <summary>
    /// Reads the contents of a <see cref="Texture1D{T}"/> instance and writes them into a target <see cref="Texture1D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="Texture1D{T}"/> instance to read data from.</param>
    /// <param name="destination">The target <see cref="Texture1D{T}"/> instance to write data to.</param>
    /// <param name="sourceOffsetX">The horizontal offset in the source texture.</param>
    /// <param name="width">The width of the memory area to copy.</param>
    public static void CopyTo<T>(this Texture1D<T> source, Texture1D<T> destination, int sourceOffsetX, int width)
        where T : unmanaged
    {
        Guard.IsNotNull(source);
        Guard.IsNotNull(destination);

        source.CopyTo(destination, sourceOffsetX, 0, width);
    }

    /// <summary>
    /// Reads the contents of a <see cref="Texture1D{T}"/> instance and writes them into a target <see cref="Texture1D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="Texture1D{T}"/> instance to read data from.</param>
    /// <param name="destination">The target <see cref="Texture1D{T}"/> instance to write data to.</param>
    /// <param name="sourceOffsetX">The horizontal offset in the source texture.</param>
    /// <param name="destinationOffsetX">The horizontal offset in the destination texture.</param>
    /// <param name="width">The width of the memory area to copy.</param>
    public static void CopyTo<T>(this Texture1D<T> source, Texture1D<T> destination, int sourceOffsetX, int destinationOffsetX, int width)
        where T : unmanaged
    {
        Guard.IsNotNull(source);
        Guard.IsNotNull(destination);

        source.CopyTo(destination, sourceOffsetX, destinationOffsetX, width);
    }

    /// <summary>
    /// Reads the contents of a <see cref="Texture1D{T}"/> instance and writes them into a target <see cref="ReadBackTexture1D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="Texture1D{T}"/> instance to read data from.</param>
    /// <param name="destination">The target <see cref="ReadBackTexture1D{T}"/> instance to write data to.</param>
    public static void CopyTo<T>(this Texture1D<T> source, ReadBackTexture1D<T> destination)
        where T : unmanaged
    {
        Guard.IsNotNull(source);
        Guard.IsNotNull(destination);

        source.CopyTo(destination, 0, 0, source.Width);
    }

    /// <summary>
    /// Reads the contents of a <see cref="Texture1D{T}"/> instance and writes them into a target <see cref="ReadBackTexture1D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="Texture1D{T}"/> instance to read data from.</param>
    /// <param name="destination">The target <see cref="ReadBackTexture1D{T}"/> instance to write data to.</param>
    /// <param name="sourceOffsetX">The horizontal offset in the source texture.</param>
    /// <param name="width">The width of the memory area to copy.</param>
    public static void CopyTo<T>(this Texture1D<T> source, ReadBackTexture1D<T> destination, int sourceOffsetX, int width)
        where T : unmanaged
    {
        Guard.IsNotNull(source);
        Guard.IsNotNull(destination);

        source.CopyTo(destination, sourceOffsetX, 0, width);
    }

    /// <summary>
    /// Reads the contents of a <see cref="Texture1D{T}"/> instance and writes them into a target <see cref="ReadBackTexture1D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="Texture1D{T}"/> instance to read data from.</param>
    /// <param name="destination">The target <see cref="ReadBackTexture1D{T}"/> instance to write data to.</param>
    /// <param name="sourceOffsetX">The horizontal offset in the source texture.</param>
    /// <param name="destinationOffsetX">The horizontal offset in the destination texture.</param>
    /// <param name="width">The width of the memory area to copy.</param>
    public static void CopyTo<T>(this Texture1D<T> source, ReadBackTexture1D<T> destination, int sourceOffsetX, int destinationOffsetX, int width)
        where T : unmanaged
    {
        Guard.IsNotNull(source);
        Guard.IsNotNull(destination);

        source.CopyTo(destination, sourceOffsetX, destinationOffsetX, width);
    }

    /// <summary>
    /// Writes the contents of a given <typeparamref name="T"/> array to the current <see cref="Texture1D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="destination">The target <see cref="Texture1D{T}"/> instance to write data to.</param>
    /// <param name="source">The input <typeparamref name="T"/> array to read data from.</param>
    public static void CopyFrom<T>(this Texture1D<T> destination, T[] source)
        where T : unmanaged
    {
        Guard.IsNotNull(destination);
        Guard.IsNotNull(source);

        destination.CopyFrom(source.AsSpan(), 0, destination.Width);
    }

#if NET6_0_OR_GREATER
    /// <summary>
    /// Writes the contents of a given <typeparamref name="T"/> array to a specified area of the current <see cref="Texture1D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="destination">The target <see cref="Texture1D{T}"/> instance to write data to.</param>
    /// <param name="source">The input <typeparamref name="T"/> array to read data from.</param>
    /// <param name="x">The horizontal range of items to write.</param>
    public static void CopyFrom<T>(this Texture1D<T> destination, T[] source, Range x)
        where T : unmanaged
    {
        Guard.IsNotNull(destination);
        Guard.IsNotNull(source);

        destination.CopyFrom(source.AsSpan(), x);
    }
#endif

    /// <summary>
    /// Writes the contents of a given <typeparamref name="T"/> array to a specified area of the current <see cref="Texture1D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="destination">The target <see cref="Texture1D{T}"/> instance to write data to.</param>
    /// <param name="source">The input <typeparamref name="T"/> array to read data from.</param>
    /// <param name="destinationOffsetX">The horizontal offset in the destination texture.</param>
    /// <param name="width">The width of the memory area to write to.</param>
    public static void CopyFrom<T>(this Texture1D<T> destination, T[] source, int destinationOffsetX, int width)
        where T : unmanaged
    {
        Guard.IsNotNull(destination);
        Guard.IsNotNull(source);

        destination.CopyFrom(source.AsSpan(), destinationOffsetX, width);
    }

#if NET6_0_OR_GREATER
    /// <summary>
    /// Writes the contents of a given <typeparamref name="T"/> array to a specified area of the current <see cref="Texture1D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="destination">The target <see cref="Texture1D{T}"/> instance to write data to.</param>
    /// <param name="source">The input <typeparamref name="T"/> array to read data from.</param>
    /// <param name="sourceOffset">The starting offset within <paramref name="source"/> to read data from.</param>
    /// <param name="x">The horizontal range of items to write.</param>
    public static void CopyFrom<T>(this Texture1D<T> destination, T[] source, int sourceOffset, Range x)
        where T : unmanaged
    {
        Guard.IsNotNull(destination);
        Guard.IsNotNull(source);

        destination.CopyFrom(source.AsSpan(sourceOffset), x);
    }
#endif

    /// <summary>
    /// Writes the contents of a given <typeparamref name="T"/> array to a specified area of the current <see cref="Texture1D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="destination">The target <see cref="Texture1D{T}"/> instance to write data to.</param>
    /// <param name="source">The input <typeparamref name="T"/> array to read data from.</param>
    /// <param name="sourceOffset">The starting offset within <paramref name="source"/> to read data from.</param>
    /// <param name="destinationOffsetX">The horizontal offset in the destination texture.</param>
    /// <param name="width">The width of the memory area to write to.</param>
    public static void CopyFrom<T>(this Texture1D<T> destination, T[] source, int sourceOffset, int destinationOffsetX, int width)
        where T : unmanaged
    {
        Guard.IsNotNull(destination);
        Guard.IsNotNull(source);

        destination.CopyFrom(source.AsSpan(sourceOffset), destinationOffsetX, width);
    }

    /// <summary>
    /// Writes the contents of a given <see cref="ReadOnlySpan{T}"/> to the current <see cref="Texture1D{T}"/> instance.
    /// The input data will be written to the start of the texture, and all input items will be copied.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="destination">The target <see cref="Texture1D{T}"/> instance to write data to.</param>
    /// <param name="source">The input <see cref="ReadOnlySpan{T}"/> to read data from.</param>
    public static void CopyFrom<T>(this Texture1D<T> destination, ReadOnlySpan<T> source)
        where T : unmanaged
    {
        Guard.IsNotNull(destination);

        destination.CopyFrom(source, 0, destination.Width);
    }

#if NET6_0_OR_GREATER
    /// <summary>
    /// Writes the contents of a given <see cref="ReadOnlySpan{T}"/> to a specified area of the current <see cref="Texture1D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="destination">The target <see cref="Texture1D{T}"/> instance to write data to.</param>
    /// <param name="source">The input <see cref="ReadOnlySpan{T}"/> to read data from.</param>
    /// <param name="x">The horizontal range of items to write.</param>
    public static void CopyFrom<T>(this Texture1D<T> destination, ReadOnlySpan<T> source, Range x)
        where T : unmanaged
    {
        Guard.IsNotNull(destination);

        (int offsetX, int width) = x.GetOffsetAndLength(destination.Width);

        destination.CopyFrom(source, offsetX, width);
    }
#endif

    /// <summary>
    /// Writes the contents of a given <see cref="ReadOnlySpan{T}"/> to a specified area of the current <see cref="Texture1D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="destination">The target <see cref="Texture1D{T}"/> instance to write data to.</param>
    /// <param name="source">The input <see cref="ReadOnlySpan{T}"/> to read data from.</param>
    /// <param name="destinationOffsetX">The horizontal offset in the destination texture.</param>
    /// <param name="width">The width of the memory area to write to.</param>
    public static void CopyFrom<T>(this Texture1D<T> destination, ReadOnlySpan<T> source, int destinationOffsetX, int width)
        where T : unmanaged
    {
        Guard.IsNotNull(destination);

        destination.CopyFrom(ref MemoryMarshal.GetReference(source), source.Length, destinationOffsetX, width);
    }

    /// <summary>
    /// Reads the contents of an <see cref="Texture1D{T}"/> instance and writes them into a target <see cref="Texture1D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="destination">The target <see cref="Texture1D{T}"/> instance to write data to.</param>
    /// <param name="source">The input <see cref="Texture1D{T}"/> instance to read data from.</param>
    public static void CopyFrom<T>(this Texture1D<T> destination, Texture1D<T> source)
        where T : unmanaged
    {
        Guard.IsNotNull(destination);
        Guard.IsNotNull(source);

        destination.CopyFrom(source, 0, 0, source.Width);
    }

    /// <summary>
    /// Reads the contents of an <see cref="Texture1D{T}"/> instance and writes them into a target <see cref="Texture1D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="destination">The target <see cref="Texture1D{T}"/> instance to write data to.</param>
    /// <param name="source">The input <see cref="Texture1D{T}"/> instance to read data from.</param>
    /// <param name="sourceOffsetX">The horizontal offset in the source texture.</param>
    /// <param name="width">The width of the memory area to copy.</param>
    public static void CopyFrom<T>(this Texture1D<T> destination, Texture1D<T> source, int sourceOffsetX, int width)
        where T : unmanaged
    {
        Guard.IsNotNull(destination);
        Guard.IsNotNull(source);

        source.CopyTo(destination, sourceOffsetX, 0, width);
    }

    /// <summary>
    /// Reads the contents of an <see cref="Texture1D{T}"/> instance and writes them into a target <see cref="Texture1D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="destination">The target <see cref="Texture1D{T}"/> instance to write data to.</param>
    /// <param name="source">The input <see cref="Texture1D{T}"/> instance to read data from.</param>
    /// <param name="sourceOffsetX">The horizontal offset in the source texture.</param>
    /// <param name="destinationOffsetX">The horizontal offset in the destination texture.</param>
    /// <param name="width">The width of the memory area to copy.</param>
    public static void CopyFrom<T>(this Texture1D<T> destination, Texture1D<T> source, int sourceOffsetX, int destinationOffsetX, int width)
        where T : unmanaged
    {
        Guard.IsNotNull(destination);
        Guard.IsNotNull(source);

        source.CopyTo(destination, sourceOffsetX, destinationOffsetX, width);
    }

    /// <summary>
    /// Reads the contents of an <see cref="UploadTexture1D{T}"/> instance and writes them into a target <see cref="Texture1D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="destination">The target <see cref="Texture1D{T}"/> instance to write data to.</param>
    /// <param name="source">The input <see cref="UploadTexture1D{T}"/> instance to read data from.</param>
    public static void CopyFrom<T>(this Texture1D<T> destination, UploadTexture1D<T> source)
        where T : unmanaged
    {
        Guard.IsNotNull(destination);
        Guard.IsNotNull(source);

        source.CopyTo(destination, 0, 0, source.Width);
    }

    /// <summary>
    /// Reads the contents of an <see cref="UploadTexture1D{T}"/> instance and writes them into a target <see cref="Texture1D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="destination">The target <see cref="Texture1D{T}"/> instance to write data to.</param>
    /// <param name="source">The input <see cref="UploadTexture1D{T}"/> instance to read data from.</param>
    /// <param name="sourceOffsetX">The horizontal offset in the source texture.</param>
    /// <param name="width">The width of the memory area to copy.</param>
    public static void CopyFrom<T>(this Texture1D<T> destination, UploadTexture1D<T> source, int sourceOffsetX, int width)
        where T : unmanaged
    {
        Guard.IsNotNull(destination);
        Guard.IsNotNull(source);

        destination.CopyFrom(source, sourceOffsetX, 0, width);
    }

    /// <summary>
    /// Reads the contents of an <see cref="UploadTexture1D{T}"/> instance and writes them into a target <see cref="Texture1D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="destination">The target <see cref="Texture1D{T}"/> instance to write data to.</param>
    /// <param name="source">The input <see cref="UploadTexture1D{T}"/> instance to read data from.</param>
    /// <param name="sourceOffsetX">The horizontal offset in the source texture.</param>
    /// <param name="destinationOffsetX">The horizontal offset in the destination texture.</param>
    /// <param name="width">The width of the memory area to copy.</param>
    public static void CopyFrom<T>(this Texture1D<T> destination, UploadTexture1D<T> source, int sourceOffsetX, int destinationOffsetX, int width)
        where T : unmanaged
    {
        Guard.IsNotNull(destination);
        Guard.IsNotNull(source);

        destination.CopyFrom(source, sourceOffsetX, destinationOffsetX, width);
    }
}