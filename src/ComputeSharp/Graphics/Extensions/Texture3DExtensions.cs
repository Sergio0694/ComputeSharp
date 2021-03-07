using System;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using ComputeSharp.Resources;
using Microsoft.Toolkit.Diagnostics;

namespace ComputeSharp
{
    /// <summary>
    /// A <see langword="class"/> that contains extension methods for the <see cref="Texture3D{T}"/> type.
    /// </summary>
    public static class Texture3DExtensions
    {
        /// <summary>
        /// Reads the contents of the current <see cref="Texture3D{T}"/> instance and returns an array.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the texture.</typeparam>
        /// <param name="texture">The input <see cref="Texture3D{T}"/> instance to read data from.</param>
        /// <returns>A <typeparamref name="T"/> array with the contents of the current buffer.</returns>
        /// <remarks>
        /// The returned array will be using the same memory layout as the texture, that is, each 2D plane
        /// in the 3D volume represented by the texture is contiguous in memory, and planes are stacked in
        /// the depth dimension. This means that the resulting 3D array will have a size of [D, H, W].
        /// </remarks>
        [Pure]
        public static T[,,] ToArray<T>(this Texture3D<T> texture)
            where T : unmanaged
        {
            T[,,] data = new T[texture.Depth, texture.Height, texture.Width];

            texture.CopyTo(data);

            return data;
        }

        /// <summary>
        /// Reads the contents of the current <see cref="Texture3D{T}"/> instance and writes them into a target array.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the texture.</typeparam>
        /// <param name="texture">The input <see cref="Texture3D{T}"/> instance to read data from.</param>
        /// <param name="destination">The input array to write data to.</param>
        /// <remarks>
        /// The input 3D array needs to have each 2D plane stacked on the depth axis. That is, the expected
        /// layout of the input array has to be of shape [depth, height, width].
        /// </remarks>
        public static void CopyTo<T>(this Texture3D<T> texture, T[,,] destination)
            where T : unmanaged
        {
            Guard.IsEqualTo(destination.GetLength(0), texture.Depth, nameof(destination));
            Guard.IsEqualTo(destination.GetLength(1), texture.Height, nameof(destination));
            Guard.IsEqualTo(destination.GetLength(2), texture.Width, nameof(destination));

            texture.CopyTo(ref destination[0, 0, 0], destination.Length, 0, 0, 0, texture.Width, texture.Height, texture.Depth);
        }

        /// <summary>
        /// Reads the contents of the specified range from the current <see cref="Texture3D{T}"/> instance and writes them into a target array.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the texture.</typeparam>
        /// <param name="texture">The input <see cref="Texture3D{T}"/> instance to read data from.</param>
        /// <param name="destination">The input array to write data to.</param>
        /// <param name="offset">The starting offset within <paramref name="destination"/> to write data to.</param>
        public static void CopyTo<T>(this Texture3D<T> texture, T[] destination, int offset)
            where T : unmanaged
        {
            texture.CopyTo(destination.AsSpan(offset), 0, 0, 0, texture.Width, texture.Height, texture.Depth);
        }

        /// <summary>
        /// Reads the contents of the specified range from the current <see cref="Texture3D{T}"/> instance and writes them into a target array.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the texture.</typeparam>
        /// <param name="texture">The input <see cref="Texture3D{T}"/> instance to read data from.</param>
        /// <param name="destination">The input array to write data to.</param>
        /// <param name="offset">The starting offset within <paramref name="destination"/> to write data to.</param>
        /// <param name="x">The horizontal range of items to copy.</param>
        /// <param name="y">The vertical range of items to copy.</param>
        /// <param name="z">The depthwise range of items to copy.</param>
        public static void CopyTo<T>(this Texture3D<T> texture, T[] destination, int offset, Range x, Range y, Range z)
            where T : unmanaged
        {
            texture.CopyTo(destination.AsSpan(offset), x, y, z);
        }

        /// <summary>
        /// Reads the contents of the specified range from the current <see cref="Texture3D{T}"/> instance and writes them into a target array.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the texture.</typeparam>
        /// <param name="texture">The input <see cref="Texture3D{T}"/> instance to read data from.</param>
        /// <param name="destination">The input array to write data to.</param>
        /// <param name="offset">The starting offset within <paramref name="destination"/> to write data to.</param>
        /// <param name="x">The horizontal offset in the source texture.</param>
        /// <param name="y">The vertical offset in the source texture.</param>
        /// <param name="z">The depthwise offset in the source texture.</param>
        /// <param name="width">The width of the memory area to copy.</param>
        /// <param name="height">The height of the memory area to copy.</param>
        /// <param name="depth">The depth of the memory area to copy.</param>
        public static void CopyTo<T>(this Texture3D<T> texture, T[] destination, int offset, int x, int y, int z, int width, int height, int depth)
            where T : unmanaged
        {
            texture.CopyTo(destination.AsSpan(offset), x, y, z, width, height, depth);
        }

        /// <summary>
        /// Reads the contents of the specified range from the current <see cref="Texture3D{T}"/> instance and writes them into a target <see cref="Span{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the texture.</typeparam>
        /// <param name="texture">The input <see cref="Texture3D{T}"/> instance to read data from.</param>
        /// <param name="destination">The input <see cref="Span{T}"/> to write data to.</param>
        public static void CopyTo<T>(this Texture3D<T> texture, Span<T> destination)
            where T : unmanaged
        {
            texture.CopyTo(destination, 0, 0, 0, texture.Width, texture.Height, texture.Depth);
        }

        /// <summary>
        /// Reads the contents of the specified range from the current <see cref="Texture3D{T}"/> instance and writes them into a target <see cref="Span{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the texture.</typeparam>
        /// <param name="texture">The input <see cref="Texture3D{T}"/> instance to read data from.</param>
        /// <param name="destination">The input <see cref="Span{T}"/> to write data to.</param>
        /// <param name="x">The horizontal range in the source texture.</param>
        /// <param name="y">The vertical range in the source texture.</param>
        /// <param name="z">The depthwise range in the source texture.</param>
        public static void CopyTo<T>(this Texture3D<T> texture, Span<T> destination, Range x, Range y, Range z)
            where T : unmanaged
        {
            var (offsetX, width) = x.GetOffsetAndLength(texture.Width);
            var (offsetY, height) = y.GetOffsetAndLength(texture.Height);
            var (offsetZ, depth) = z.GetOffsetAndLength(texture.Depth);

            texture.CopyTo(destination, offsetX, offsetY, offsetZ, width, height, depth);
        }

        /// <summary>
        /// Reads the contents of the specified range from the current <see cref="Texture3D{T}"/> instance and writes them into a target <see cref="Span{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the texture.</typeparam>
        /// <param name="texture">The input <see cref="Texture3D{T}"/> instance to read data from.</param>
        /// <param name="destination">The input <see cref="Span{T}"/> to write data to.</param>
        /// <param name="x">The horizontal offset in the source texture.</param>
        /// <param name="y">The vertical offset in the source texture.</param>
        /// <param name="z">The depthwise offset in the source texture.</param>
        /// <param name="width">The width of the memory area to copy.</param>
        /// <param name="height">The height of the memory area to copy.</param>
        /// <param name="depth">The depth of the memory area to copy.</param>
        public static void CopyTo<T>(this Texture3D<T> texture, Span<T> destination, int x, int y, int z, int width, int height, int depth)
            where T : unmanaged
        {
            texture.CopyTo(ref MemoryMarshal.GetReference(destination), destination.Length, x, y, z, width, height, depth);
        }

        /// <summary>
        /// Reads the contents of a <see cref="Texture3D{T}"/> instance and writes them into a target <see cref="ReadBackTexture3D{T}"/> instance.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the texture.</typeparam>
        /// <param name="texture">The input <see cref="Texture3D{T}"/> instance to read data from.</param>
        /// <param name="destination">The target <see cref="ReadBackTexture3D{T}"/> instance to write data to.</param>
        public static void CopyTo<T>(this Texture3D<T> texture, ReadBackTexture3D<T> destination)
            where T : unmanaged
        {
            texture.CopyTo(destination, 0, 0, 0, 0, 0, 0, texture.Width, texture.Height, texture.Depth);
        }

        /// <summary>
        /// Reads the contents of a <see cref="Texture3D{T}"/> instance and writes them into a target <see cref="ReadBackTexture3D{T}"/> instance.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the texture.</typeparam>
        /// <param name="texture">The input <see cref="Texture3D{T}"/> instance to read data from.</param>
        /// <param name="destination">The target <see cref="ReadBackTexture3D{T}"/> instance to write data to.</param>
        /// <param name="x">The horizontal offset in the source texture.</param>
        /// <param name="y">The vertical offset in the source texture.</param>
        /// <param name="z">The depthwise offset in the source texture.</param>
        /// <param name="width">The width of the memory area to copy.</param>
        /// <param name="height">The height of the memory area to copy.</param>
        /// <param name="depth">The depth of the memory area to copy.</param>
        public static void CopyTo<T>(this Texture3D<T> texture, ReadBackTexture3D<T> destination, int x, int y, int z, int width, int height, int depth)
            where T : unmanaged
        {
            texture.CopyTo(destination, 0, 0, 0, x, y, z, width, height, depth);
        }

        /// <summary>
        /// Writes the contents of a given <typeparamref name="T"/> array to the current <see cref="Texture3D{T}"/> instance.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the texture.</typeparam>
        /// <param name="texture">The target <see cref="Texture3D{T}"/> instance to write data to.</param>
        /// <param name="source">The input <typeparamref name="T"/> array to read data from.</param>
        /// <remarks>
        /// The source 3D array needs to have each 2D plane stacked on the depth axis. That is, the expected
        /// layout of the input array has to be of shape [depth, height, width].
        /// </remarks>
        public static void CopyFrom<T>(this Texture3D<T> texture, T[,,] source)
            where T : unmanaged
        {
            Guard.IsEqualTo(source.GetLength(0), texture.Depth, nameof(source));
            Guard.IsEqualTo(source.GetLength(1), texture.Height, nameof(source));
            Guard.IsEqualTo(source.GetLength(2), texture.Width, nameof(source));

            texture.CopyFrom(ref source[0, 0, 0], source.Length, 0, 0, 0, texture.Width, texture.Height, texture.Depth);
        }

        /// <summary>
        /// Writes the contents of a given <typeparamref name="T"/> array to the current <see cref="Texture3D{T}"/> instance.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the texture.</typeparam>
        /// <param name="texture">The target <see cref="Texture3D{T}"/> instance to write data to.</param>
        /// <param name="source">The input <typeparamref name="T"/> array to read data from.</param>
        public static void CopyFrom<T>(this Texture3D<T> texture, T[] source)
            where T : unmanaged
        {
            texture.CopyFrom(source.AsSpan(), 0, 0, 0, texture.Width, texture.Height, texture.Depth);
        }

        /// <summary>
        /// Writes the contents of a given <typeparamref name="T"/> array to a specified area of the current <see cref="Texture3D{T}"/> instance.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the texture.</typeparam>
        /// <param name="texture">The target <see cref="Texture3D{T}"/> instance to write data to.</param>
        /// <param name="source">The input <typeparamref name="T"/> array to read data from.</param>
        /// <param name="x">The horizontal range of items to write.</param>
        /// <param name="y">The vertical range of items to write.</param>
        /// <param name="z">The depthwise range of items to write.</param>
        public static void CopyFrom<T>(this Texture3D<T> texture, T[] source, Range x, Range y, Range z)
            where T : unmanaged
        {
            texture.CopyFrom(source.AsSpan(), x, y, z);
        }

        /// <summary>
        /// Writes the contents of a given <typeparamref name="T"/> array to a specified area of the current <see cref="Texture3D{T}"/> instance.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the texture.</typeparam>
        /// <param name="texture">The target <see cref="Texture3D{T}"/> instance to write data to.</param>
        /// <param name="source">The input <typeparamref name="T"/> array to read data from.</param>
        /// <param name="x">The horizontal offset in the destination texture.</param>
        /// <param name="y">The vertical offset in the destination texture.</param>
        /// <param name="z">The depthwise offset in the destination texture.</param>
        /// <param name="width">The width of the memory area to write to.</param>
        /// <param name="height">The height of the memory area to write to.</param>
        /// <param name="depth">The depth of the memory area to write to.</param>
        public static void CopyFrom<T>(this Texture3D<T> texture, T[] source, int x, int y, int z, int width, int height, int depth)
            where T : unmanaged
        {
            texture.CopyFrom(source.AsSpan(), x, y, z, width, height, depth);
        }

        /// <summary>
        /// Writes the contents of a given <typeparamref name="T"/> array to a specified area of the current <see cref="Texture3D{T}"/> instance.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the texture.</typeparam>
        /// <param name="texture">The target <see cref="Texture3D{T}"/> instance to write data to.</param>
        /// <param name="source">The input <typeparamref name="T"/> array to read data from.</param>
        /// <param name="offset">The starting offset within <paramref name="source"/> to read data from.</param>
        /// <param name="x">The horizontal range of items to write.</param>
        /// <param name="y">The vertical range of items to write.</param>
        /// <param name="z">The depthwise range of items to write.</param>
        public static void CopyFrom<T>(this Texture3D<T> texture, T[] source, int offset, Range x, Range y, Range z)
            where T : unmanaged
        {
            texture.CopyFrom(source.AsSpan(offset), x, y, z);
        }

        /// <summary>
        /// Writes the contents of a given <typeparamref name="T"/> array to a specified area of the current <see cref="Texture3D{T}"/> instance.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the texture.</typeparam>
        /// <param name="texture">The target <see cref="Texture3D{T}"/> instance to write data to.</param>
        /// <param name="source">The input <typeparamref name="T"/> array to read data from.</param>
        /// <param name="offset">The starting offset within <paramref name="source"/> to read data from.</param>
        /// <param name="x">The horizontal offset in the destination texture.</param>
        /// <param name="y">The vertical offset in the destination texture.</param>
        /// <param name="z">The depthwise offset in the destination texture.</param>
        /// <param name="width">The width of the memory area to write to.</param>
        /// <param name="height">The height of the memory area to write to.</param>
        /// <param name="depth">The depth of the memory area to write to.</param>
        public static void CopyFrom<T>(this Texture3D<T> texture, T[] source, int offset, int x, int y, int z, int width, int height, int depth)
            where T : unmanaged
        {
            texture.CopyFrom(source.AsSpan(offset), x, y, z, width, height, depth);
        }

        /// <summary>
        /// Writes the contents of a given <see cref="ReadOnlySpan{T}"/> to the current <see cref="Texture3D{T}"/> instance.
        /// The input data will be written to the start of the texture, and all input items will be copied.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the texture.</typeparam>
        /// <param name="texture">The target <see cref="Texture3D{T}"/> instance to write data to.</param>
        /// <param name="source">The input <see cref="ReadOnlySpan{T}"/> to read data from.</param>
        public static void CopyFrom<T>(this Texture3D<T> texture, ReadOnlySpan<T> source)
            where T : unmanaged
        {
            texture.CopyFrom(source, 0, 0, 0, texture.Width, texture.Height, texture.Depth);
        }

        /// <summary>
        /// Writes the contents of a given <see cref="ReadOnlySpan{T}"/> to a specified area of the current <see cref="Texture3D{T}"/> instance.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the texture.</typeparam>
        /// <param name="texture">The target <see cref="Texture3D{T}"/> instance to write data to.</param>
        /// <param name="source">The input <see cref="ReadOnlySpan{T}"/> to read data from.</param>
        /// <param name="x">The horizontal range of items to write.</param>
        /// <param name="y">The vertical range of items to write.</param>
        /// <param name="z">The depthwise range of items to write.</param>
        public static void CopyFrom<T>(this Texture3D<T> texture, ReadOnlySpan<T> source, Range x, Range y, Range z)
            where T : unmanaged
        {
            var (offsetX, width) = x.GetOffsetAndLength(texture.Width);
            var (offsetY, height) = y.GetOffsetAndLength(texture.Height);
            var (offsetZ, depth) = z.GetOffsetAndLength(texture.Depth);

            texture.CopyFrom(source, offsetX, offsetY, offsetZ, width, height, depth);
        }

        /// <summary>
        /// Writes the contents of a given <see cref="ReadOnlySpan{T}"/> to a specified area of the current <see cref="Texture3D{T}"/> instance.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the texture.</typeparam>
        /// <param name="texture">The target <see cref="Texture3D{T}"/> instance to write data to.</param>
        /// <param name="source">The input <see cref="ReadOnlySpan{T}"/> to read data from.</param>
        /// <param name="x">The horizontal offset in the destination texture.</param>
        /// <param name="y">The vertical offset in the destination texture.</param>
        /// <param name="z">The depthwise offseet in the destination texture.</param>
        /// <param name="width">The width of the memory area to write to.</param>
        /// <param name="height">The height of the memory area to write to.</param>
        /// <param name="depth">The depth of the memory area to write to.</param>
        public static void CopyFrom<T>(this Texture3D<T> texture, ReadOnlySpan<T> source, int x, int y, int z, int width, int height, int depth)
            where T : unmanaged
        {
            texture.CopyFrom(ref MemoryMarshal.GetReference(source), source.Length, x, y, z, width, height, depth);
        }
    }
}
