using System;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using ComputeSharp.Resources;
using Microsoft.Toolkit.Diagnostics;

namespace ComputeSharp;

/// <summary>
/// A <see langword="class"/> that contains extension methods for the <see cref="Texture3D{T}"/> type.
/// </summary>
public static class Texture3DExtensions
{
    /// <summary>
    /// Reads the contents of the current <see cref="Texture3D{T}"/> instance and returns an array.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="Texture3D{T}"/> instance to read data from.</param>
    /// <returns>A <typeparamref name="T"/> array with the contents of the current buffer.</returns>
    /// <remarks>
    /// The returned array will be using the same memory layout as the texture, that is, each 2D plane
    /// in the 3D volume represented by the texture is contiguous in memory, and planes are stacked in
    /// the depth dimension. This means that the resulting 3D array will have a size of [D, H, W].
    /// </remarks>
    [Pure]
    public static T[,,] ToArray<T>(this Texture3D<T> source)
        where T : unmanaged
    {
        T[,,] data = new T[source.Depth, source.Height, source.Width];

        source.CopyTo(data);

        return data;
    }

    /// <summary>
    /// Reads the contents of the current <see cref="Texture3D{T}"/> instance and returns an array.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="Texture3D{T}"/> instance to read data from.</param>
    /// <param name="x">The horizontal offset in the source texture.</param>
    /// <param name="y">The vertical offset in the source texture.</param>
    /// <param name="z">The depthwise offset in the source texture.</param>
    /// <param name="width">The width of the memory area to copy.</param>
    /// <param name="height">The height of the memory area to copy.</param>
    /// <param name="depth">The depth of the memory area to copy.</param>
    /// <returns>A <typeparamref name="T"/> array with the contents of the current texture.</returns>
    public static unsafe T[,,] ToArray<T>(this Texture3D<T> source, int x, int y, int z, int width, int height, int depth)
        where T : unmanaged
    {
        Guard.IsGreaterThanOrEqualTo(width, 0, nameof(width));
        Guard.IsGreaterThanOrEqualTo(height, 0, nameof(height));
        Guard.IsGreaterThanOrEqualTo(depth, 0, nameof(depth));

        T[,,] data = new T[depth, height, width];

        fixed (T* p = data)
        {
            source.CopyTo(new Span<T>(p, width * height * depth), x, y, z, width, height, depth);
        }

        return data;
    }

    /// <summary>
    /// Reads the contents of the current <see cref="Texture3D{T}"/> instance and writes them into a target array.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="Texture3D{T}"/> instance to read data from.</param>
    /// <param name="destination">The input array to write data to.</param>
    /// <remarks>
    /// The input 3D array needs to have each 2D plane stacked on the depth axis. That is, the expected
    /// layout of the input array has to be of shape [depth, height, width].
    /// </remarks>
    public static void CopyTo<T>(this Texture3D<T> source, T[,,] destination)
        where T : unmanaged
    {
        Guard.IsEqualTo(destination.GetLength(0), source.Depth, nameof(destination));
        Guard.IsEqualTo(destination.GetLength(1), source.Height, nameof(destination));
        Guard.IsEqualTo(destination.GetLength(2), source.Width, nameof(destination));

        source.CopyTo(ref destination[0, 0, 0], destination.Length, 0, 0, 0, source.Width, source.Height, source.Depth);
    }

    /// <summary>
    /// Reads the contents of the specified range from the current <see cref="Texture3D{T}"/> instance and writes them into a target array.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="Texture3D{T}"/> instance to read data from.</param>
    /// <param name="destination">The input array to write data to.</param>
    /// <param name="destinationOffset">The starting offset within <paramref name="destination"/> to write data to.</param>
    public static void CopyTo<T>(this Texture3D<T> source, T[] destination, int destinationOffset)
        where T : unmanaged
    {
        source.CopyTo(destination.AsSpan(destinationOffset), 0, 0, 0, source.Width, source.Height, source.Depth);
    }

#if NET6_0_OR_GREATER
    /// <summary>
    /// Reads the contents of the specified range from the current <see cref="Texture3D{T}"/> instance and writes them into a target array.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="Texture3D{T}"/> instance to read data from.</param>
    /// <param name="destination">The input array to write data to.</param>
    /// <param name="destinationOffset">The starting offset within <paramref name="destination"/> to write data to.</param>
    /// <param name="x">The horizontal range of items to copy.</param>
    /// <param name="y">The vertical range of items to copy.</param>
    /// <param name="z">The depthwise range of items to copy.</param>
    public static void CopyTo<T>(this Texture3D<T> source, T[] destination, int destinationOffset, Range x, Range y, Range z)
        where T : unmanaged
    {
        source.CopyTo(destination.AsSpan(destinationOffset), x, y, z);
    }
#endif

    /// <summary>
    /// Reads the contents of the specified range from the current <see cref="Texture3D{T}"/> instance and writes them into a target array.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="Texture3D{T}"/> instance to read data from.</param>
    /// <param name="destination">The input array to write data to.</param>
    /// <param name="sourceOffsetX">The horizontal offset in the source texture.</param>
    /// <param name="sourceOffsetY">The vertical offset in the source texture.</param>
    /// <param name="sourceOffsetZ">The depthwise offset in the source texture.</param>
    /// <param name="destinationOffset">The starting offset within <paramref name="destination"/> to write data to.</param>
    /// <param name="width">The width of the memory area to copy.</param>
    /// <param name="height">The height of the memory area to copy.</param>
    /// <param name="depth">The depth of the memory area to copy.</param>
    public static void CopyTo<T>(this Texture3D<T> source, T[] destination, int sourceOffsetX, int sourceOffsetY, int sourceOffsetZ, int destinationOffset, int width, int height, int depth)
        where T : unmanaged
    {
        source.CopyTo(destination.AsSpan(destinationOffset), sourceOffsetX, sourceOffsetY, sourceOffsetZ, width, height, depth);
    }

    /// <summary>
    /// Reads the contents of the specified range from the current <see cref="Texture3D{T}"/> instance and writes them into a target <see cref="Span{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="Texture3D{T}"/> instance to read data from.</param>
    /// <param name="destination">The input <see cref="Span{T}"/> to write data to.</param>
    public static void CopyTo<T>(this Texture3D<T> source, Span<T> destination)
        where T : unmanaged
    {
        source.CopyTo(destination, 0, 0, 0, source.Width, source.Height, source.Depth);
    }

#if NET6_0_OR_GREATER
    /// <summary>
    /// Reads the contents of the specified range from the current <see cref="Texture3D{T}"/> instance and writes them into a target <see cref="Span{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="Texture3D{T}"/> instance to read data from.</param>
    /// <param name="destination">The input <see cref="Span{T}"/> to write data to.</param>
    /// <param name="x">The horizontal range in the source texture.</param>
    /// <param name="y">The vertical range in the source texture.</param>
    /// <param name="z">The depthwise range in the source texture.</param>
    public static void CopyTo<T>(this Texture3D<T> source, Span<T> destination, Range x, Range y, Range z)
        where T : unmanaged
    {
        var (offsetX, width) = x.GetOffsetAndLength(source.Width);
        var (offsetY, height) = y.GetOffsetAndLength(source.Height);
        var (offsetZ, depth) = z.GetOffsetAndLength(source.Depth);

        source.CopyTo(destination, offsetX, offsetY, offsetZ, width, height, depth);
    }
#endif

    /// <summary>
    /// Reads the contents of the specified range from the current <see cref="Texture3D{T}"/> instance and writes them into a target <see cref="Span{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="Texture3D{T}"/> instance to read data from.</param>
    /// <param name="destination">The input <see cref="Span{T}"/> to write data to.</param>
    /// <param name="sourceOffsetX">The horizontal offset in the source texture.</param>
    /// <param name="sourceOffsetY">The vertical offset in the source texture.</param>
    /// <param name="sourceOffsetZ">The depthwise offset in the source texture.</param>
    /// <param name="width">The width of the memory area to copy.</param>
    /// <param name="height">The height of the memory area to copy.</param>
    /// <param name="depth">The depth of the memory area to copy.</param>
    public static void CopyTo<T>(this Texture3D<T> source, Span<T> destination, int sourceOffsetX, int sourceOffsetY, int sourceOffsetZ, int width, int height, int depth)
        where T : unmanaged
    {
        source.CopyTo(ref MemoryMarshal.GetReference(destination), destination.Length, sourceOffsetX, sourceOffsetY, sourceOffsetZ, width, height, depth);
    }

    /// <summary>
    /// Reads the contents of a <see cref="Texture3D{T}"/> instance and writes them into a target <see cref="Texture3D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="Texture3D{T}"/> instance to read data from.</param>
    /// <param name="destination">The target <see cref="Texture3D{T}"/> instance to write data to.</param>
    public static void CopyTo<T>(this Texture3D<T> source, Texture3D<T> destination)
        where T : unmanaged
    {
        source.CopyTo(destination, 0, 0, 0, 0, 0, 0, source.Width, source.Height, source.Depth);
    }

    /// <summary>
    /// Reads the contents of a <see cref="Texture3D{T}"/> instance and writes them into a target <see cref="Texture3D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="Texture3D{T}"/> instance to read data from.</param>
    /// <param name="destination">The target <see cref="Texture3D{T}"/> instance to write data to.</param>
    /// <param name="sourceOffsetX">The horizontal offset in the source texture.</param>
    /// <param name="sourceOffsetY">The vertical offset in the source texture.</param>
    /// <param name="sourceOffsetZ">The depthwise offset in the source texture.</param>
    /// <param name="width">The width of the memory area to copy.</param>
    /// <param name="height">The height of the memory area to copy.</param>
    /// <param name="depth">The depth of the memory area to copy.</param>
    public static void CopyTo<T>(this Texture3D<T> source, Texture3D<T> destination, int sourceOffsetX, int sourceOffsetY, int sourceOffsetZ, int width, int height, int depth)
        where T : unmanaged
    {
        source.CopyTo(destination, sourceOffsetX, sourceOffsetY, sourceOffsetZ, 0, 0, 0, width, height, depth);
    }

    /// <summary>
    /// Reads the contents of a <see cref="Texture3D{T}"/> instance and writes them into a target <see cref="Texture3D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="Texture3D{T}"/> instance to read data from.</param>
    /// <param name="destination">The target <see cref="Texture3D{T}"/> instance to write data to.</param>
    /// <param name="sourceOffsetX">The horizontal offset in the source texture.</param>
    /// <param name="sourceOffsetY">The vertical offset in the source texture.</param>
    /// <param name="sourceOffsetZ">The depthwise offset in the source texture.</param>
    /// <param name="destinationOffsetX">The horizontal offset in the destination texture.</param>
    /// <param name="destinationOffsetY">The vertical offset in the destination texture.</param>
    /// <param name="destinationOffsetZ">The depthwise offset in the destination texture.</param>
    /// <param name="width">The width of the memory area to copy.</param>
    /// <param name="height">The height of the memory area to copy.</param>
    /// <param name="depth">The depth of the memory area to copy.</param>
    public static void CopyTo<T>(this Texture3D<T> source, Texture3D<T> destination, int sourceOffsetX, int sourceOffsetY, int sourceOffsetZ, int destinationOffsetX, int destinationOffsetY, int destinationOffsetZ, int width, int height, int depth)
        where T : unmanaged
    {
        source.CopyTo(destination, sourceOffsetX, sourceOffsetY, sourceOffsetZ, destinationOffsetX, destinationOffsetY, destinationOffsetZ, width, height, depth);
    }

    /// <summary>
    /// Reads the contents of a <see cref="Texture3D{T}"/> instance and writes them into a target <see cref="ReadBackTexture3D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="Texture3D{T}"/> instance to read data from.</param>
    /// <param name="destination">The target <see cref="ReadBackTexture3D{T}"/> instance to write data to.</param>
    public static void CopyTo<T>(this Texture3D<T> source, ReadBackTexture3D<T> destination)
        where T : unmanaged
    {
        source.CopyTo(destination, 0, 0, 0, 0, 0, 0, source.Width, source.Height, source.Depth);
    }

    /// <summary>
    /// Reads the contents of a <see cref="Texture3D{T}"/> instance and writes them into a target <see cref="ReadBackTexture3D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="Texture3D{T}"/> instance to read data from.</param>
    /// <param name="destination">The target <see cref="ReadBackTexture3D{T}"/> instance to write data to.</param>
    /// <param name="sourceOffsetX">The horizontal offset in the source texture.</param>
    /// <param name="sourceOffsetY">The vertical offset in the source texture.</param>
    /// <param name="sourceOffsetZ">The depthwise offset in the source texture.</param>
    /// <param name="width">The width of the memory area to copy.</param>
    /// <param name="height">The height of the memory area to copy.</param>
    /// <param name="depth">The depth of the memory area to copy.</param>
    public static void CopyTo<T>(this Texture3D<T> source, ReadBackTexture3D<T> destination, int sourceOffsetX, int sourceOffsetY, int sourceOffsetZ, int width, int height, int depth)
        where T : unmanaged
    {
        source.CopyTo(destination, sourceOffsetX, sourceOffsetY, sourceOffsetZ, 0, 0, 0, width, height, depth);
    }

    /// <summary>
    /// Reads the contents of a <see cref="Texture3D{T}"/> instance and writes them into a target <see cref="ReadBackTexture3D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="Texture3D{T}"/> instance to read data from.</param>
    /// <param name="destination">The target <see cref="ReadBackTexture3D{T}"/> instance to write data to.</param>
    /// <param name="sourceOffsetX">The horizontal offset in the source texture.</param>
    /// <param name="sourceOffsetY">The vertical offset in the source texture.</param>
    /// <param name="sourceOffsetZ">The depthwise offset in the source texture.</param>
    /// <param name="destinationOffsetX">The horizontal offset in the destination texture.</param>
    /// <param name="destinationOffsetY">The vertical offset in the destination texture.</param>
    /// <param name="destinationOffsetZ">The depthwise offset in the destination texture.</param>
    /// <param name="width">The width of the memory area to copy.</param>
    /// <param name="height">The height of the memory area to copy.</param>
    /// <param name="depth">The depth of the memory area to copy.</param>
    public static void CopyTo<T>(this Texture3D<T> source, ReadBackTexture3D<T> destination, int sourceOffsetX, int sourceOffsetY, int sourceOffsetZ, int destinationOffsetX, int destinationOffsetY, int destinationOffsetZ, int width, int height, int depth)
        where T : unmanaged
    {
        source.CopyTo(destination, sourceOffsetX, sourceOffsetY, sourceOffsetZ, destinationOffsetX, destinationOffsetY, destinationOffsetZ, width, height, depth);
    }

    /// <summary>
    /// Writes the contents of a given <typeparamref name="T"/> array to the current <see cref="Texture3D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="destination">The target <see cref="Texture3D{T}"/> instance to write data to.</param>
    /// <param name="source">The input <typeparamref name="T"/> array to read data from.</param>
    /// <remarks>
    /// The source 3D array needs to have each 2D plane stacked on the depth axis. That is, the expected
    /// layout of the input array has to be of shape [depth, height, width].
    /// </remarks>
    public static void CopyFrom<T>(this Texture3D<T> destination, T[,,] source)
        where T : unmanaged
    {
        Guard.IsEqualTo(source.GetLength(0), destination.Depth, nameof(source));
        Guard.IsEqualTo(source.GetLength(1), destination.Height, nameof(source));
        Guard.IsEqualTo(source.GetLength(2), destination.Width, nameof(source));

        destination.CopyFrom(ref source[0, 0, 0], source.Length, 0, 0, 0, destination.Width, destination.Height, destination.Depth);
    }

    /// <summary>
    /// Writes the contents of a given <typeparamref name="T"/> array to the current <see cref="Texture3D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="destination">The target <see cref="Texture3D{T}"/> instance to write data to.</param>
    /// <param name="source">The input <typeparamref name="T"/> array to read data from.</param>
    public static void CopyFrom<T>(this Texture3D<T> destination, T[] source)
        where T : unmanaged
    {
        destination.CopyFrom(source.AsSpan(), 0, 0, 0, destination.Width, destination.Height, destination.Depth);
    }

#if NET6_0_OR_GREATER
    /// <summary>
    /// Writes the contents of a given <typeparamref name="T"/> array to a specified area of the current <see cref="Texture3D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="destination">The target <see cref="Texture3D{T}"/> instance to write data to.</param>
    /// <param name="source">The input <typeparamref name="T"/> array to read data from.</param>
    /// <param name="x">The horizontal range of items to write.</param>
    /// <param name="y">The vertical range of items to write.</param>
    /// <param name="z">The depthwise range of items to write.</param>
    public static void CopyFrom<T>(this Texture3D<T> destination, T[] source, Range x, Range y, Range z)
        where T : unmanaged
    {
        destination.CopyFrom(source.AsSpan(), x, y, z);
    }
#endif

    /// <summary>
    /// Writes the contents of a given <typeparamref name="T"/> array to a specified area of the current <see cref="Texture3D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="destination">The target <see cref="Texture3D{T}"/> instance to write data to.</param>
    /// <param name="source">The input <typeparamref name="T"/> array to read data from.</param>
    /// <param name="destinationOffsetX">The horizontal offset in the destination texture.</param>
    /// <param name="destinationOffsetY">The vertical offset in the destination texture.</param>
    /// <param name="destinationOffsetZ">The depthwise offset in the destination texture.</param>
    /// <param name="width">The width of the memory area to write to.</param>
    /// <param name="height">The height of the memory area to write to.</param>
    /// <param name="depth">The depth of the memory area to write to.</param>
    public static void CopyFrom<T>(this Texture3D<T> destination, T[] source, int destinationOffsetX, int destinationOffsetY, int destinationOffsetZ, int width, int height, int depth)
        where T : unmanaged
    {
        destination.CopyFrom(source.AsSpan(), destinationOffsetX, destinationOffsetY, destinationOffsetZ, width, height, depth);
    }

#if NET6_0_OR_GREATER
    /// <summary>
    /// Writes the contents of a given <typeparamref name="T"/> array to a specified area of the current <see cref="Texture3D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="destination">The target <see cref="Texture3D{T}"/> instance to write data to.</param>
    /// <param name="source">The input <typeparamref name="T"/> array to read data from.</param>
    /// <param name="sourceOffset">The starting offset within <paramref name="source"/> to read data from.</param>
    /// <param name="x">The horizontal range of items to write.</param>
    /// <param name="y">The vertical range of items to write.</param>
    /// <param name="z">The depthwise range of items to write.</param>
    public static void CopyFrom<T>(this Texture3D<T> destination, T[] source, int sourceOffset, Range x, Range y, Range z)
        where T : unmanaged
    {
        destination.CopyFrom(source.AsSpan(sourceOffset), x, y, z);
    }
#endif

    /// <summary>
    /// Writes the contents of a given <typeparamref name="T"/> array to a specified area of the current <see cref="Texture3D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="destination">The target <see cref="Texture3D{T}"/> instance to write data to.</param>
    /// <param name="source">The input <typeparamref name="T"/> array to read data from.</param>
    /// <param name="sourceOffset">The starting offset within <paramref name="source"/> to read data from.</param>
    /// <param name="destinationOffsetX">The horizontal offset in the destination texture.</param>
    /// <param name="destinationOffsetY">The vertical offset in the destination texture.</param>
    /// <param name="destinationOffsetZ">The depthwise offset in the destination texture.</param>
    /// <param name="width">The width of the memory area to write to.</param>
    /// <param name="height">The height of the memory area to write to.</param>
    /// <param name="depth">The depth of the memory area to write to.</param>
    public static void CopyFrom<T>(this Texture3D<T> destination, T[] source, int sourceOffset, int destinationOffsetX, int destinationOffsetY, int destinationOffsetZ, int width, int height, int depth)
        where T : unmanaged
    {
        destination.CopyFrom(source.AsSpan(sourceOffset), destinationOffsetX, destinationOffsetY, destinationOffsetZ, width, height, depth);
    }

    /// <summary>
    /// Writes the contents of a given <see cref="ReadOnlySpan{T}"/> to the current <see cref="Texture3D{T}"/> instance.
    /// The input data will be written to the start of the texture, and all input items will be copied.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="destination">The target <see cref="Texture3D{T}"/> instance to write data to.</param>
    /// <param name="source">The input <see cref="ReadOnlySpan{T}"/> to read data from.</param>
    public static void CopyFrom<T>(this Texture3D<T> destination, ReadOnlySpan<T> source)
        where T : unmanaged
    {
        destination.CopyFrom(source, 0, 0, 0, destination.Width, destination.Height, destination.Depth);
    }

#if NET6_0_OR_GREATER
    /// <summary>
    /// Writes the contents of a given <see cref="ReadOnlySpan{T}"/> to a specified area of the current <see cref="Texture3D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="destination">The target <see cref="Texture3D{T}"/> instance to write data to.</param>
    /// <param name="source">The input <see cref="ReadOnlySpan{T}"/> to read data from.</param>
    /// <param name="x">The horizontal range of items to write.</param>
    /// <param name="y">The vertical range of items to write.</param>
    /// <param name="z">The depthwise range of items to write.</param>
    public static void CopyFrom<T>(this Texture3D<T> destination, ReadOnlySpan<T> source, Range x, Range y, Range z)
        where T : unmanaged
    {
        var (offsetX, width) = x.GetOffsetAndLength(destination.Width);
        var (offsetY, height) = y.GetOffsetAndLength(destination.Height);
        var (offsetZ, depth) = z.GetOffsetAndLength(destination.Depth);

        destination.CopyFrom(source, offsetX, offsetY, offsetZ, width, height, depth);
    }
#endif

    /// <summary>
    /// Writes the contents of a given <see cref="ReadOnlySpan{T}"/> to a specified area of the current <see cref="Texture3D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="destination">The target <see cref="Texture3D{T}"/> instance to write data to.</param>
    /// <param name="source">The input <see cref="ReadOnlySpan{T}"/> to read data from.</param>
    /// <param name="destinationOffsetX">The horizontal offset in the destination texture.</param>
    /// <param name="destinationOffsetY">The vertical offset in the destination texture.</param>
    /// <param name="destinationOffsetZ">The depthwise offseet in the destination texture.</param>
    /// <param name="width">The width of the memory area to write to.</param>
    /// <param name="height">The height of the memory area to write to.</param>
    /// <param name="depth">The depth of the memory area to write to.</param>
    public static void CopyFrom<T>(this Texture3D<T> destination, ReadOnlySpan<T> source, int destinationOffsetX, int destinationOffsetY, int destinationOffsetZ, int width, int height, int depth)
        where T : unmanaged
    {
        destination.CopyFrom(ref MemoryMarshal.GetReference(source), source.Length, destinationOffsetX, destinationOffsetY, destinationOffsetZ, width, height, depth);
    }

    /// <summary>
    /// Reads the contents of a <see cref="Texture3D{T}"/> instance and writes them into a target <see cref="Texture3D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="destination">The target <see cref="Texture3D{T}"/> instance to write data to.</param>
    /// <param name="source">The input <see cref="Texture3D{T}"/> instance to read data from.</param>
    public static void CopyFrom<T>(this Texture3D<T> destination, Texture3D<T> source)
        where T : unmanaged
    {
        source.CopyTo(destination, 0, 0, 0, 0, 0, 0, source.Width, source.Height, source.Depth);
    }

    /// <summary>
    /// Reads the contents of a <see cref="Texture3D{T}"/> instance and writes them into a target <see cref="Texture3D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="destination">The target <see cref="Texture3D{T}"/> instance to write data to.</param>
    /// <param name="source">The input <see cref="Texture3D{T}"/> instance to read data from.</param>
    /// <param name="sourceOffsetX">The horizontal offset in the source texture.</param>
    /// <param name="sourceOffsetY">The vertical offset in the source texture.</param>
    /// <param name="sourceOffsetZ">The depthwise offset in the source texture.</param>
    /// <param name="width">The width of the memory area to copy.</param>
    /// <param name="height">The height of the memory area to copy.</param>
    /// <param name="depth">The depth of the memory area to copy.</param>
    public static void CopyFrom<T>(this Texture3D<T> destination, Texture3D<T> source, int sourceOffsetX, int sourceOffsetY, int sourceOffsetZ, int width, int height, int depth)
        where T : unmanaged
    {
        source.CopyTo(destination, sourceOffsetX, sourceOffsetY, sourceOffsetZ, 0, 0, 0, width, height, depth);
    }

    /// <summary>
    /// Reads the contents of a <see cref="Texture3D{T}"/> instance and writes them into a target <see cref="Texture3D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="destination">The target <see cref="Texture3D{T}"/> instance to write data to.</param>
    /// <param name="source">The input <see cref="Texture3D{T}"/> instance to read data from.</param>
    /// <param name="sourceOffsetX">The horizontal offset in the source texture.</param>
    /// <param name="sourceOffsetY">The vertical offset in the source texture.</param>
    /// <param name="sourceOffsetZ">The depthwise offset in the source texture.</param>
    /// <param name="destinationOffsetX">The horizontal offset in the destination texture.</param>
    /// <param name="destinationOffsetY">The vertical offset in the destination texture.</param>
    /// <param name="destinationOffsetZ">The depthwise offset in the destination texture.</param>
    /// <param name="width">The width of the memory area to copy.</param>
    /// <param name="height">The height of the memory area to copy.</param>
    /// <param name="depth">The depth of the memory area to copy.</param>
    public static void CopyFrom<T>(this Texture3D<T> destination, Texture3D<T> source, int sourceOffsetX, int sourceOffsetY, int sourceOffsetZ, int destinationOffsetX, int destinationOffsetY, int destinationOffsetZ, int width, int height, int depth)
        where T : unmanaged
    {
        source.CopyTo(destination, sourceOffsetX, sourceOffsetY, sourceOffsetZ, destinationOffsetX, destinationOffsetY, destinationOffsetZ, width, height, depth);
    }

    /// <summary>
    /// Reads the contents of an <see cref="UploadTexture3D{T}"/> instance and writes them into a target <see cref="Texture3D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="destination">The target <see cref="Texture3D{T}"/> instance to write data to.</param>
    /// <param name="source">The input <see cref="UploadTexture3D{T}"/> instance to read data from.</param>
    public static void CopyFrom<T>(this Texture3D<T> destination, UploadTexture3D<T> source)
        where T : unmanaged
    {
        destination.CopyFrom(source, 0, 0, 0, 0, 0, 0, source.Width, source.Height, source.Depth);
    }

    /// <summary>
    /// Reads the contents of an <see cref="UploadTexture3D{T}"/> instance and writes them into a target <see cref="Texture3D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="destination">The target <see cref="Texture3D{T}"/> instance to write data to.</param>
    /// <param name="source">The input <see cref="UploadTexture3D{T}"/> instance to read data from.</param>
    /// <param name="sourceOffsetX">The horizontal offset in the source texture.</param>
    /// <param name="sourceOffsetY">The vertical offset in the source texture.</param>
    /// <param name="sourceOffsetZ">The depthwise offset in the source texture.</param>
    /// <param name="width">The width of the memory area to copy.</param>
    /// <param name="height">The height of the memory area to copy.</param>
    /// <param name="depth">The depth of the memory area to copy.</param>
    public static void CopyFrom<T>(this Texture3D<T> destination, UploadTexture3D<T> source, int sourceOffsetX, int sourceOffsetY, int sourceOffsetZ, int width, int height, int depth)
        where T : unmanaged
    {
        destination.CopyFrom(source, sourceOffsetX, sourceOffsetY, sourceOffsetZ, 0, 0, 0, width, height, depth);
    }

    /// <summary>
    /// Reads the contents of an <see cref="UploadTexture3D{T}"/> instance and writes them into a target <see cref="Texture3D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="destination">The target <see cref="Texture3D{T}"/> instance to write data to.</param>
    /// <param name="source">The input <see cref="UploadTexture3D{T}"/> instance to read data from.</param>
    /// <param name="sourceOffsetX">The horizontal offset in the source texture.</param>
    /// <param name="sourceOffsetY">The vertical offset in the source texture.</param>
    /// <param name="sourceOffsetZ">The depthwise offset in the source texture.</param>
    /// <param name="destinationOffsetX">The horizontal offset in the destination texture.</param>
    /// <param name="destinationOffsetY">The vertical offset in the destination texture.</param>
    /// <param name="destinationOffsetZ">The depthwise offset in the destination texture.</param>
    /// <param name="width">The width of the memory area to copy.</param>
    /// <param name="height">The height of the memory area to copy.</param>
    /// <param name="depth">The depth of the memory area to copy.</param>
    public static void CopyFrom<T>(this Texture3D<T> destination, UploadTexture3D<T> source, int sourceOffsetX, int sourceOffsetY, int sourceOffsetZ, int destinationOffsetX, int destinationOffsetY, int destinationOffsetZ, int width, int height, int depth)
        where T : unmanaged
    {
        destination.CopyFrom(source, sourceOffsetX, sourceOffsetY, sourceOffsetZ, destinationOffsetX, destinationOffsetY, destinationOffsetZ, width, height, depth);
    }
}
