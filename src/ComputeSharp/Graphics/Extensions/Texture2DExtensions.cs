using System;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using ComputeSharp.Graphics.Helpers;
using ComputeSharp.Resources;
using Microsoft.Toolkit.Diagnostics;

namespace ComputeSharp;

/// <summary>
/// A <see langword="class"/> that contains extension methods for the <see cref="Texture2D{T}"/> type.
/// </summary>
public static class Texture2DExtensions
{
    /// <summary>
    /// Reads the contents of the current <see cref="Texture2D{T}"/> instance and returns an array.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="Texture2D{T}"/> instance to read data from.</param>
    /// <returns>A <typeparamref name="T"/> array with the contents of the current texture.</returns>
    [Pure]
    public static T[,] ToArray<T>(this Texture2D<T> source)
        where T : unmanaged
    {
        T[,] data = new T[source.Height, source.Width];

        source.CopyTo(data);

        return data;
    }

    /// <summary>
    /// Reads the contents of the current <see cref="Texture2D{T}"/> instance and returns an array.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="Texture2D{T}"/> instance to read data from.</param>
    /// <param name="x">The horizontal offset in the source texture.</param>
    /// <param name="y">The vertical offset in the source texture.</param>
    /// <param name="width">The width of the memory area to copy.</param>
    /// <param name="height">The height of the memory area to copy.</param>
    /// <returns>A <typeparamref name="T"/> array with the contents of the current texture.</returns>
    [Pure]
    public static unsafe T[,] ToArray<T>(this Texture2D<T> source, int x, int y, int width, int height)
        where T : unmanaged
    {
        Guard.IsGreaterThanOrEqualTo(width, 0, nameof(width));
        Guard.IsGreaterThanOrEqualTo(height, 0, nameof(height));

        T[,] data = new T[height, width];

        fixed (T* p = data)
        {
            source.CopyTo(new Span<T>(p, width * height), x, y, width, height);
        }

        return data;
    }

    /// <summary>
    /// Reads the contents of the current <see cref="Texture2D{T}"/> instance and writes them into a target array.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="Texture2D{T}"/> instance to read data from.</param>
    /// <param name="destination">The input array to write data to.</param>
    public static void CopyTo<T>(this Texture2D<T> source, T[,] destination)
        where T : unmanaged
    {
        Guard.IsEqualTo(destination.GetLength(0), source.Height, nameof(destination));
        Guard.IsEqualTo(destination.GetLength(1), source.Width, nameof(destination));

        source.CopyTo(ref destination[0, 0], destination.Length, 0, 0, source.Width, source.Height);
    }

    /// <summary>
    /// Reads the contents of the specified range from the current <see cref="Texture2D{T}"/> instance and writes them into a target array.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="Texture2D{T}"/> instance to read data from.</param>
    /// <param name="destination">The input array to write data to.</param>
    public static void CopyTo<T>(this Texture2D<T> source, T[] destination)
        where T : unmanaged
    {
        source.CopyTo(destination.AsSpan(), 0, 0, source.Width, source.Height);
    }

#if NET6_0_OR_GREATER
    /// <summary>
    /// Reads the contents of the specified range from the current <see cref="Texture2D{T}"/> instance and writes them into a target array.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="Texture2D{T}"/> instance to read data from.</param>
    /// <param name="destination">The input array to write data to.</param>
    /// <param name="x">The horizontal range of items to copy.</param>
    /// <param name="y">The vertical range of items to copy.</param>
    public static void CopyTo<T>(this Texture2D<T> source, T[] destination, Range x, Range y)
        where T : unmanaged
    {
        source.CopyTo(destination.AsSpan(), x, y);
    }
#endif

    /// <summary>
    /// Reads the contents of the specified range from the current <see cref="Texture2D{T}"/> instance and writes them into a target array.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="Texture2D{T}"/> instance to read data from.</param>
    /// <param name="destination">The input array to write data to.</param>
    /// <param name="sourceOffsetX">The horizontal offset in the source texture.</param>
    /// <param name="sourceOffsetY">The vertical offset in the source texture.</param>
    /// <param name="width">The width of the memory area to copy.</param>
    /// <param name="height">The height of the memory area to copy.</param>
    public static void CopyTo<T>(this Texture2D<T> source, T[] destination, int sourceOffsetX, int sourceOffsetY, int width, int height)
        where T : unmanaged
    {
        source.CopyTo(destination.AsSpan(), sourceOffsetX, sourceOffsetY, width, height);
    }

    /// <summary>
    /// Reads the contents of the specified range from the current <see cref="Texture2D{T}"/> instance and writes them into a target array.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="Texture2D{T}"/> instance to read data from.</param>
    /// <param name="destination">The input array to write data to.</param>
    /// <param name="destinationOffset">The starting offset within <paramref name="destination"/> to write data to.</param>
    public static void CopyTo<T>(this Texture2D<T> source, T[] destination, int destinationOffset)
        where T : unmanaged
    {
        source.CopyTo(destination.AsSpan(destinationOffset), 0, 0, source.Width, source.Height);
    }

#if NET6_0_OR_GREATER
    /// <summary>
    /// Reads the contents of the specified range from the current <see cref="Texture2D{T}"/> instance and writes them into a target array.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="Texture2D{T}"/> instance to read data from.</param>
    /// <param name="destination">The input array to write data to.</param>
    /// <param name="destinationOffset">The starting offset within <paramref name="destination"/> to write data to.</param>
    /// <param name="x">The horizontal range of items to copy.</param>
    /// <param name="y">The vertical range of items to copy.</param>
    public static void CopyTo<T>(this Texture2D<T> source, T[] destination, int destinationOffset, Range x, Range y)
        where T : unmanaged
    {
        source.CopyTo(destination.AsSpan(destinationOffset), x, y);
    }
#endif

    /// <summary>
    /// Reads the contents of the specified range from the current <see cref="Texture2D{T}"/> instance and writes them into a target array.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="Texture2D{T}"/> instance to read data from.</param>
    /// <param name="destination">The input array to write data to.</param>
    /// <param name="destinationOffset">The starting offset within <paramref name="destination"/> to write data to.</param>
    /// <param name="sourceOffsetX">The horizontal offset in the source texture.</param>
    /// <param name="sourceOffsetY">The vertical offset in the source texture.</param>
    /// <param name="width">The width of the memory area to copy.</param>
    /// <param name="height">The height of the memory area to copy.</param>
    public static void CopyTo<T>(this Texture2D<T> source, T[] destination, int destinationOffset, int sourceOffsetX, int sourceOffsetY, int width, int height)
        where T : unmanaged
    {
        source.CopyTo(destination.AsSpan(destinationOffset), sourceOffsetX, sourceOffsetY, width, height);
    }

    /// <summary>
    /// Reads the contents of the current <see cref="Texture2D{T}"/> instance and writes them into a target <see cref="Span{T}"/>.
    /// The input data will be read from the start of the texture.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="Texture2D{T}"/> instance to read data from.</param>
    /// <param name="destination">The input <see cref="Span{T}"/> to write data to.</param>
    public static void CopyTo<T>(this Texture2D<T> source, Span<T> destination)
        where T : unmanaged
    {
        source.CopyTo(destination, 0, 0, source.Width, source.Height);
    }

#if NET6_0_OR_GREATER
    /// <summary>
    /// Reads the contents of the specified range from the current <see cref="Texture2D{T}"/> instance and writes them into a target <see cref="Span{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="Texture2D{T}"/> instance to read data from.</param>
    /// <param name="destination">The input <see cref="Span{T}"/> to write data to.</param>
    /// <param name="x">The horizontal range of items to copy.</param>
    /// <param name="y">The vertical range of items to copy.</param>
    public static void CopyTo<T>(this Texture2D<T> source, Span<T> destination, Range x, Range y)
        where T : unmanaged
    {
        var (offsetX, width) = x.GetOffsetAndLength(source.Width);
        var (offsetY, height) = y.GetOffsetAndLength(source.Height);

        source.CopyTo(destination, offsetX, offsetY, width, height);
    }
#endif

    /// <summary>
    /// Reads the contents of the specified range from the current <see cref="Texture2D{T}"/> instance and writes them into a target <see cref="Span{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="Texture2D{T}"/> instance to read data from.</param>
    /// <param name="destination">The input <see cref="Span{T}"/> to write data to.</param>
    /// <param name="sourceOffsetX">The horizontal offset in the source texture.</param>
    /// <param name="sourceOffsetY">The vertical offset in the source texture.</param>
    /// <param name="width">The width of the memory area to copy.</param>
    /// <param name="height">The height of the memory area to copy.</param>
    public static void CopyTo<T>(this Texture2D<T> source, Span<T> destination, int sourceOffsetX, int sourceOffsetY, int width, int height)
        where T : unmanaged
    {
        source.CopyTo(ref MemoryMarshal.GetReference(destination), destination.Length, sourceOffsetX, sourceOffsetY, width, height);
    }

    /// <summary>
    /// Reads the contents of a <see cref="Texture2D{T}"/> instance and writes them into a target <see cref="Texture2D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="Texture2D{T}"/> instance to read data from.</param>
    /// <param name="destination">The target <see cref="Texture2D{T}"/> instance to write data to.</param>
    public static void CopyTo<T>(this Texture2D<T> source, Texture2D<T> destination)
        where T : unmanaged
    {
        source.CopyTo(destination, 0, 0, 0, 0, source.Width, source.Height);
    }

    /// <summary>
    /// Reads the contents of a <see cref="Texture2D{T}"/> instance and writes them into a target <see cref="Texture2D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="Texture2D{T}"/> instance to read data from.</param>
    /// <param name="destination">The target <see cref="Texture2D{T}"/> instance to write data to.</param>
    /// <param name="sourceOffsetX">The horizontal offset in the source texture.</param>
    /// <param name="sourceOffsetY">The vertical offset in the source texture.</param>
    /// <param name="width">The width of the memory area to copy.</param>
    /// <param name="height">The height of the memory area to copy.</param>
    public static void CopyTo<T>(this Texture2D<T> source, Texture2D<T> destination, int sourceOffsetX, int sourceOffsetY, int width, int height)
        where T : unmanaged
    {
        source.CopyTo(destination, sourceOffsetX, sourceOffsetY, 0, 0, width, height);
    }

    /// <summary>
    /// Reads the contents of a <see cref="Texture2D{T}"/> instance and writes them into a target <see cref="Texture2D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="Texture2D{T}"/> instance to read data from.</param>
    /// <param name="destination">The target <see cref="Texture2D{T}"/> instance to write data to.</param>
    /// <param name="sourceOffsetX">The horizontal offset in the source texture.</param>
    /// <param name="sourceOffsetY">The vertical offset in the source texture.</param>
    /// <param name="destinationOffsetX">The horizontal offset in the destination texture.</param>
    /// <param name="destinationOffsetY">The vertical offset in the destination texture.</param>
    /// <param name="width">The width of the memory area to copy.</param>
    /// <param name="height">The height of the memory area to copy.</param>
    public static void CopyTo<T>(this Texture2D<T> source, Texture2D<T> destination, int sourceOffsetX, int sourceOffsetY, int destinationOffsetX, int destinationOffsetY, int width, int height)
        where T : unmanaged
    {
        source.CopyTo(destination, sourceOffsetX, sourceOffsetY, destinationOffsetX, destinationOffsetY, width, height);
    }

    /// <summary>
    /// Reads the contents of a <see cref="Texture2D{T}"/> instance and writes them into a target <see cref="ReadBackTexture2D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="Texture2D{T}"/> instance to read data from.</param>
    /// <param name="destination">The target <see cref="ReadBackTexture2D{T}"/> instance to write data to.</param>
    public static void CopyTo<T>(this Texture2D<T> source, ReadBackTexture2D<T> destination)
        where T : unmanaged
    {
        source.CopyTo(destination, 0, 0, 0, 0, source.Width, source.Height);
    }

    /// <summary>
    /// Reads the contents of a <see cref="Texture2D{T}"/> instance and writes them into a target <see cref="ReadBackTexture2D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="Texture2D{T}"/> instance to read data from.</param>
    /// <param name="destination">The target <see cref="ReadBackTexture2D{T}"/> instance to write data to.</param>
    /// <param name="sourceOffsetX">The horizontal offset in the source texture.</param>
    /// <param name="sourceOffsetY">The vertical offset in the source texture.</param>
    /// <param name="width">The width of the memory area to copy.</param>
    /// <param name="height">The height of the memory area to copy.</param>
    public static void CopyTo<T>(this Texture2D<T> source, ReadBackTexture2D<T> destination, int sourceOffsetX, int sourceOffsetY, int width, int height)
        where T : unmanaged
    {
        source.CopyTo(destination, sourceOffsetX, sourceOffsetY, 0, 0, width, height);
    }

    /// <summary>
    /// Reads the contents of a <see cref="Texture2D{T}"/> instance and writes them into a target <see cref="ReadBackTexture2D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="Texture2D{T}"/> instance to read data from.</param>
    /// <param name="destination">The target <see cref="ReadBackTexture2D{T}"/> instance to write data to.</param>
    /// <param name="sourceOffsetX">The horizontal offset in the source texture.</param>
    /// <param name="sourceOffsetY">The vertical offset in the source texture.</param>
    /// <param name="destinationOffsetX">The horizontal offset in the destination texture.</param>
    /// <param name="destinationOffsetY">The vertical offset in the destination texture.</param>
    /// <param name="width">The width of the memory area to copy.</param>
    /// <param name="height">The height of the memory area to copy.</param>
    public static void CopyTo<T>(this Texture2D<T> source, ReadBackTexture2D<T> destination, int sourceOffsetX, int sourceOffsetY, int destinationOffsetX, int destinationOffsetY, int width, int height)
        where T : unmanaged
    {
        source.CopyTo(destination, sourceOffsetX, sourceOffsetY, destinationOffsetX, destinationOffsetY, width, height);
    }

    /// <summary>
    /// Writes the contents of a given <typeparamref name="T"/> array to the current <see cref="Texture2D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="destination">The target <see cref="Texture2D{T}"/> instance to write data to.</param>
    /// <param name="source">The input <typeparamref name="T"/> array to read data from.</param>
    public static unsafe void CopyFrom<T>(this Texture2D<T> destination, T[,] source)
        where T : unmanaged
    {
        Guard.IsEqualTo(source.GetLength(0), destination.Height, nameof(source));
        Guard.IsEqualTo(source.GetLength(1), destination.Width, nameof(source));

        destination.CopyFrom(ref source[0, 0], source.Length, 0, 0, destination.Width, destination.Height);
    }

    /// <summary>
    /// Writes the contents of a given <typeparamref name="T"/> array to the current <see cref="Texture2D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="destination">The target <see cref="Texture2D{T}"/> instance to write data to.</param>
    /// <param name="source">The input <typeparamref name="T"/> array to read data from.</param>
    public static void CopyFrom<T>(this Texture2D<T> destination, T[] source)
        where T : unmanaged
    {
        destination.CopyFrom(source.AsSpan(), 0, 0, destination.Width, destination.Height);
    }

#if NET6_0_OR_GREATER
    /// <summary>
    /// Writes the contents of a given <typeparamref name="T"/> array to a specified area of the current <see cref="Texture2D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="destination">The target <see cref="Texture2D{T}"/> instance to write data to.</param>
    /// <param name="source">The input <typeparamref name="T"/> array to read data from.</param>
    /// <param name="x">The horizontal range of items to write.</param>
    /// <param name="y">The vertical range of items to write.</param>
    public static void CopyFrom<T>(this Texture2D<T> destination, T[] source, Range x, Range y)
        where T : unmanaged
    {
        destination.CopyFrom(source.AsSpan(), x, y);
    }
#endif

    /// <summary>
    /// Writes the contents of a given <typeparamref name="T"/> array to a specified area of the current <see cref="Texture2D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="destination">The target <see cref="Texture2D{T}"/> instance to write data to.</param>
    /// <param name="source">The input <typeparamref name="T"/> array to read data from.</param>
    /// <param name="destinationOffsetX">The horizontal offset in the destination texture.</param>
    /// <param name="destinationOffsetY">The vertical offset in the destination texture.</param>
    /// <param name="width">The width of the memory area to write to.</param>
    /// <param name="height">The height of the memory area to write to.</param>
    public static void CopyFrom<T>(this Texture2D<T> destination, T[] source, int destinationOffsetX, int destinationOffsetY, int width, int height)
        where T : unmanaged
    {
        destination.CopyFrom(source.AsSpan(), destinationOffsetX, destinationOffsetY, width, height);
    }

#if NET6_0_OR_GREATER
    /// <summary>
    /// Writes the contents of a given <typeparamref name="T"/> array to a specified area of the current <see cref="Texture2D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="destination">The target <see cref="Texture2D{T}"/> instance to write data to.</param>
    /// <param name="source">The input <typeparamref name="T"/> array to read data from.</param>
    /// <param name="sourceOffset">The starting offset within <paramref name="source"/> to read data from.</param>
    /// <param name="x">The horizontal range of items to write.</param>
    /// <param name="y">The vertical range of items to write.</param>
    public static void CopyFrom<T>(this Texture2D<T> destination, T[] source, int sourceOffset, Range x, Range y)
        where T : unmanaged
    {
        destination.CopyFrom(source.AsSpan(sourceOffset), x, y);
    }
#endif

    /// <summary>
    /// Writes the contents of a given <typeparamref name="T"/> array to a specified area of the current <see cref="Texture2D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="destination">The target <see cref="Texture2D{T}"/> instance to write data to.</param>
    /// <param name="source">The input <typeparamref name="T"/> array to read data from.</param>
    /// <param name="sourceOffset">The starting offset within <paramref name="source"/> to read data from.</param>
    /// <param name="destinationOffsetX">The horizontal offset in the destination texture.</param>
    /// <param name="destinationOffsetY">The vertical offset in the destination texture.</param>
    /// <param name="width">The width of the memory area to write to.</param>
    /// <param name="height">The height of the memory area to write to.</param>
    public static void CopyFrom<T>(this Texture2D<T> destination, T[] source, int sourceOffset, int destinationOffsetX, int destinationOffsetY, int width, int height)
        where T : unmanaged
    {
        destination.CopyFrom(source.AsSpan(sourceOffset), destinationOffsetX, destinationOffsetY, width, height);
    }

    /// <summary>
    /// Writes the contents of a given <see cref="ReadOnlySpan{T}"/> to the current <see cref="Texture2D{T}"/> instance.
    /// The input data will be written to the start of the texture, and all input items will be copied.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="destination">The target <see cref="Texture2D{T}"/> instance to write data to.</param>
    /// <param name="source">The input <see cref="ReadOnlySpan{T}"/> to read data from.</param>
    public static void CopyFrom<T>(this Texture2D<T> destination, ReadOnlySpan<T> source)
        where T : unmanaged
    {
        destination.CopyFrom(source, 0, 0, destination.Width, destination.Height);
    }

#if NET6_0_OR_GREATER
    /// <summary>
    /// Writes the contents of a given <see cref="ReadOnlySpan{T}"/> to a specified area of the current <see cref="Texture2D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="destination">The target <see cref="Texture2D{T}"/> instance to write data to.</param>
    /// <param name="source">The input <see cref="ReadOnlySpan{T}"/> to read data from.</param>
    /// <param name="x">The horizontal range of items to write.</param>
    /// <param name="y">The vertical range of items to write.</param>
    public static void CopyFrom<T>(this Texture2D<T> destination, ReadOnlySpan<T> source, Range x, Range y)
        where T : unmanaged
    {
        var (offsetX, width) = x.GetOffsetAndLength(destination.Width);
        var (offsetY, height) = y.GetOffsetAndLength(destination.Height);

        destination.CopyFrom(source, offsetX, offsetY, width, height);
    }
#endif

    /// <summary>
    /// Writes the contents of a given <see cref="ReadOnlySpan{T}"/> to a specified area of the current <see cref="Texture2D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="destination">The target <see cref="Texture2D{T}"/> instance to write data to.</param>
    /// <param name="source">The input <see cref="ReadOnlySpan{T}"/> to read data from.</param>
    /// <param name="destinationOffsetX">The horizontal offset in the destination texture.</param>
    /// <param name="destinationOffsetY">The vertical offset in the destination texture.</param>
    /// <param name="width">The width of the memory area to write to.</param>
    /// <param name="height">The height of the memory area to write to.</param>
    public static void CopyFrom<T>(this Texture2D<T> destination, ReadOnlySpan<T> source, int destinationOffsetX, int destinationOffsetY, int width, int height)
        where T : unmanaged
    {
        destination.CopyFrom(ref MemoryMarshal.GetReference(source), source.Length, destinationOffsetX, destinationOffsetY, width, height);
    }

    /// <summary>
    /// Reads the contents of an <see cref="Texture2D{T}"/> instance and writes them into a target <see cref="Texture2D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="destination">The target <see cref="Texture2D{T}"/> instance to write data to.</param>
    /// <param name="source">The input <see cref="Texture2D{T}"/> instance to read data from.</param>
    public static void CopyFrom<T>(this Texture2D<T> destination, Texture2D<T> source)
        where T : unmanaged
    {
        destination.CopyFrom(source, 0, 0, 0, 0, source.Width, source.Height);
    }

    /// <summary>
    /// Reads the contents of an <see cref="Texture2D{T}"/> instance and writes them into a target <see cref="Texture2D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="destination">The target <see cref="Texture2D{T}"/> instance to write data to.</param>
    /// <param name="source">The input <see cref="Texture2D{T}"/> instance to read data from.</param>
    /// <param name="sourceOffsetX">The horizontal offset in the source texture.</param>
    /// <param name="sourceOffsetY">The vertical offset in the source texture.</param>
    /// <param name="width">The width of the memory area to copy.</param>
    /// <param name="height">The height of the memory area to copy.</param>
    public static void CopyFrom<T>(this Texture2D<T> destination, Texture2D<T> source, int sourceOffsetX, int sourceOffsetY, int width, int height)
        where T : unmanaged
    {
        source.CopyTo(destination, sourceOffsetX, sourceOffsetY, 0, 0, width, height);
    }

    /// <summary>
    /// Reads the contents of an <see cref="Texture2D{T}"/> instance and writes them into a target <see cref="Texture2D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="destination">The target <see cref="Texture2D{T}"/> instance to write data to.</param>
    /// <param name="source">The input <see cref="Texture2D{T}"/> instance to read data from.</param>
    /// <param name="sourceOffsetX">The horizontal offset in the source texture.</param>
    /// <param name="sourceOffsetY">The vertical offset in the source texture.</param>
    /// <param name="destinationOffsetX">The horizontal offset in the destination texture.</param>
    /// <param name="destinationOffsetY">The vertical offset in the destination texture.</param>
    /// <param name="width">The width of the memory area to copy.</param>
    /// <param name="height">The height of the memory area to copy.</param>
    public static void CopyFrom<T>(this Texture2D<T> destination, Texture2D<T> source, int sourceOffsetX, int sourceOffsetY, int destinationOffsetX, int destinationOffsetY, int width, int height)
        where T : unmanaged
    {
        source.CopyTo(destination, sourceOffsetX, sourceOffsetY, destinationOffsetX, destinationOffsetY, width, height);
    }

    /// <summary>
    /// Reads the contents of an <see cref="UploadTexture2D{T}"/> instance and writes them into a target <see cref="Texture2D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="destination">The target <see cref="Texture2D{T}"/> instance to write data to.</param>
    /// <param name="source">The input <see cref="UploadTexture2D{T}"/> instance to read data from.</param>
    public static void CopyFrom<T>(this Texture2D<T> destination, UploadTexture2D<T> source)
        where T : unmanaged
    {
        source.CopyTo(destination, 0, 0, 0, 0, source.Width, source.Height);
    }

    /// <summary>
    /// Reads the contents of an <see cref="UploadTexture2D{T}"/> instance and writes them into a target <see cref="Texture2D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="destination">The target <see cref="Texture2D{T}"/> instance to write data to.</param>
    /// <param name="source">The input <see cref="UploadTexture2D{T}"/> instance to read data from.</param>
    /// <param name="sourceOffsetX">The horizontal offset in the source texture.</param>
    /// <param name="sourceOffsetY">The vertical offset in the source texture.</param>
    /// <param name="width">The width of the memory area to copy.</param>
    /// <param name="height">The height of the memory area to copy.</param>
    public static void CopyFrom<T>(this Texture2D<T> destination, UploadTexture2D<T> source, int sourceOffsetX, int sourceOffsetY, int width, int height)
        where T : unmanaged
    {
        destination.CopyFrom(source, sourceOffsetX, sourceOffsetY, 0, 0, width, height);
    }

    /// <summary>
    /// Reads the contents of an <see cref="UploadTexture2D{T}"/> instance and writes them into a target <see cref="Texture2D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="destination">The target <see cref="Texture2D{T}"/> instance to write data to.</param>
    /// <param name="source">The input <see cref="UploadTexture2D{T}"/> instance to read data from.</param>
    /// <param name="sourceOffsetX">The horizontal offset in the source texture.</param>
    /// <param name="sourceOffsetY">The vertical offset in the source texture.</param>
    /// <param name="destinationOffsetX">The horizontal offset in the destination texture.</param>
    /// <param name="destinationOffsetY">The vertical offset in the destination texture.</param>
    /// <param name="width">The width of the memory area to copy.</param>
    /// <param name="height">The height of the memory area to copy.</param>
    public static void CopyFrom<T>(this Texture2D<T> destination, UploadTexture2D<T> source, int sourceOffsetX, int sourceOffsetY, int destinationOffsetX, int destinationOffsetY, int width, int height)
        where T : unmanaged
    {
        destination.CopyFrom(source, sourceOffsetX, sourceOffsetY, destinationOffsetX, destinationOffsetY, width, height);
    }

    /// <summary>
    /// Saves a texture to a specified file.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <param name="texture">The texture to save to an image.</param>
    /// <param name="filename">The filename of the image file to save.</param>
    public static void Save<T>(this Texture2D<T> texture, ReadOnlySpan<char> filename)
        where T : unmanaged
    {
        using ReadBackTexture2D<T> readback = texture.GraphicsDevice.AllocateReadBackTexture2D<T>(texture.Width, texture.Height);

        texture.CopyTo(readback);

        WICHelper.Instance.SaveTexture(readback.View, filename);
    }
}
