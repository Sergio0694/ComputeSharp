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
        public static T[,,] GetData<T>(this Texture3D<T> texture)
            where T : unmanaged
        {
            T[,,] data = new T[texture.Depth, texture.Height, texture.Width];

            texture.GetData(data);

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
        /// layout of the input array has to be [<see cref="Depth"/>, <see cref="Height"/>, <see cref="Width"/>].
        /// </remarks>
        public static void GetData<T>(this Texture3D<T> texture, T[,,] destination)
            where T : unmanaged
        {
            Guard.IsEqualTo(destination.GetLength(0), texture.Depth, nameof(destination));
            Guard.IsEqualTo(destination.GetLength(1), texture.Height, nameof(destination));
            Guard.IsEqualTo(destination.GetLength(2), texture.Width, nameof(destination));

            texture.GetData(ref destination[0, 0, 0], destination.Length, 0, 0, 0, texture.Width, texture.Height, texture.Depth);
        }

        /// <summary>
        /// Reads the contents of the specified range from the current <see cref="Texture3D{T}"/> instance and writes them into a target array.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the texture.</typeparam>
        /// <param name="texture">The input <see cref="Texture3D{T}"/> instance to read data from.</param>
        /// <param name="destination">The input array to write data to.</param>
        /// <param name="offset">The starting offset within <paramref name="source"/> to write data to.</param>
        public static void GetData<T>(this Texture3D<T> texture, T[] destination, int offset)
            where T : unmanaged
        {
            texture.GetData(destination.AsSpan(offset), 0, 0, 0, texture.Width, texture.Height, texture.Depth);
        }

#if NET5_0
        /// <summary>
        /// Reads the contents of the specified range from the current <see cref="Texture3D{T}"/> instance and writes them into a target array.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the texture.</typeparam>
        /// <param name="texture">The input <see cref="Texture3D{T}"/> instance to read data from.</param>
        /// <param name="destination">The input array to write data to.</param>
        /// <param name="offset">The starting offset within <paramref name="source"/> to write data to.</param>
        /// <param name="x">The horizontal range of items to copy.</param>
        /// <param name="y">The vertical range of items to copy.</param>
        /// <param name="z">The depthwise range of items to copy.</param>
        public static void GetData<T>(this Texture3D<T> texture, T[] destination, int offset, Range x, Range y, Range z)
            where T : unmanaged
        {
            texture.GetData(destination.AsSpan(offset), x, y, z);
        }
#endif

        /// <summary>
        /// Reads the contents of the specified range from the current <see cref="Texture3D{T}"/> instance and writes them into a target array.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the texture.</typeparam>
        /// <param name="texture">The input <see cref="Texture3D{T}"/> instance to read data from.</param>
        /// <param name="destination">The input array to write data to.</param>
        /// <param name="offset">The starting offset within <paramref name="source"/> to write data to.</param>
        /// <param name="x">The horizontal offset in the source texture.</param>
        /// <param name="y">The vertical offset in the source texture.</param>
        /// <param name="z">The depthwise offset in the source texture.</param>
        /// <param name="width">The width of the memory area to copy.</param>
        /// <param name="height">The height of the memory area to copy.</param>
        /// <param name="depth">The depth of the memory area to copy.</param>
        public static void GetData<T>(this Texture3D<T> texture, T[] destination, int offset, int x, int y, int z, int width, int height, int depth)
            where T : unmanaged
        {
            texture.GetData(destination.AsSpan(offset), x, y, z, width, height, depth);
        }

        /// <summary>
        /// Reads the contents of the specified range from the current <see cref="Texture3D{T}"/> instance and writes them into a target <see cref="Span{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the texture.</typeparam>
        /// <param name="texture">The input <see cref="Texture3D{T}"/> instance to read data from.</param>
        /// <param name="destination">The input <see cref="Span{T}"/> to write data to.</param>
        public static void GetData<T>(this Texture3D<T> texture, Span<T> destination)
            where T : unmanaged
        {
            texture.GetData(destination, 0, 0, 0, texture.Width, texture.Height, texture.Depth);
        }

#if NET5_0
        /// <summary>
        /// Reads the contents of the specified range from the current <see cref="Texture3D{T}"/> instance and writes them into a target <see cref="Span{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the texture.</typeparam>
        /// <param name="texture">The input <see cref="Texture3D{T}"/> instance to read data from.</param>
        /// <param name="destination">The input <see cref="Span{T}"/> to write data to.</param>
        /// <param name="x">The horizontal range in the source texture.</param>
        /// <param name="y">The vertical range in the source texture.</param>
        /// <param name="z">The depthwise range in the source texture.</param>
        public static void GetData<T>(this Texture3D<T> texture, Span<T> destination, Range x, Range y, Range z)
            where T : unmanaged
        {
            var (offsetX, width) = x.GetOffsetAndLength(texture.Width);
            var (offsetY, height) = y.GetOffsetAndLength(texture.Height);
            var (offsetZ, depth) = z.GetOffsetAndLength(texture.Depth);

            texture.GetData(destination, offsetX, offsetY, offsetZ, width, height, depth);
        }
#endif

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
        public static void GetData<T>(this Texture3D<T> texture, Span<T> destination, int x, int y, int z, int width, int height, int depth)
            where T : unmanaged
        {
            texture.GetData(ref MemoryMarshal.GetReference(destination), destination.Length, x, y, z, width, height, depth);
        }

        /// <summary>
        /// Writes the contents of a given <typeparamref name="T"/> array to the current <see cref="Texture3D{T}"/> instance.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the texture.</typeparam>
        /// <param name="texture">The target <see cref="Texture3D{T}"/> instance to write data to.</param>
        /// <param name="source">The input <typeparamref name="T"/> array to read data from.</param>
        /// <remarks>
        /// The source 3D array needs to have each 2D plane stacked on the depth axis. That is, the expected
        /// layout of the input array has to be [<see cref="Depth"/>, <see cref="Height"/>, <see cref="Width"/>].
        /// </remarks>
        public static void SetData<T>(this Texture3D<T> texture, T[,,] source)
            where T : unmanaged
        {
            Guard.IsEqualTo(source.GetLength(0), texture.Depth, nameof(source));
            Guard.IsEqualTo(source.GetLength(1), texture.Height, nameof(source));
            Guard.IsEqualTo(source.GetLength(2), texture.Width, nameof(source));

            texture.SetData(ref source[0, 0, 0], source.Length, 0, 0, 0, texture.Width, texture.Height, texture.Depth);
        }

        /// <summary>
        /// Writes the contents of a given <typeparamref name="T"/> array to the current <see cref="Texture3D{T}"/> instance.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the texture.</typeparam>
        /// <param name="texture">The target <see cref="Texture3D{T}"/> instance to write data to.</param>
        /// <param name="source">The input <typeparamref name="T"/> array to read data from.</param>
        public static void SetData<T>(this Texture3D<T> texture, T[] source)
            where T : unmanaged
        {
            texture.SetData(source.AsSpan(), 0, 0, 0, texture.Width, texture.Height, texture.Depth);
        }

#if NET5_0
        /// <summary>
        /// Writes the contents of a given <typeparamref name="T"/> array to a specified area of the current <see cref="Texture3D{T}"/> instance.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the texture.</typeparam>
        /// <param name="texture">The target <see cref="Texture3D{T}"/> instance to write data to.</param>
        /// <param name="source">The input <typeparamref name="T"/> array to read data from.</param>
        /// <param name="x">The horizontal range of items to write.</param>
        /// <param name="y">The vertical range of items to write.</param>
        /// <param name="z">The depthwise range of items to write.</param>
        public static void SetData<T>(this Texture3D<T> texture, T[] source, Range x, Range y, Range z)
            where T : unmanaged
        {
            texture.SetData(source.AsSpan(), x, y, z);
        }
#endif

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
        public static void SetData<T>(this Texture3D<T> texture, T[] source, int x, int y, int z, int width, int height, int depth)
            where T : unmanaged
        {
            texture.SetData(source.AsSpan(), x, y, z, width, height, depth);
        }

#if NET5_0
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
        public static void SetData<T>(this Texture3D<T> texture, T[] source, int offset, Range x, Range y, Range z)
            where T : unmanaged
        {
            texture.SetData(source.AsSpan(offset), x, y, z);
        }
#endif

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
        public static void SetData<T>(this Texture3D<T> texture, T[] source, int offset, int x, int y, int z, int width, int height, int depth)
            where T : unmanaged
        {
            texture.SetData(source.AsSpan(offset), x, y, z, width, height, depth);
        }

        /// <summary>
        /// Writes the contents of a given <see cref="ReadOnlySpan{T}"/> to the current <see cref="Texture3D{T}"/> instance.
        /// The input data will be written to the start of the texture, and all input items will be copied.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the texture.</typeparam>
        /// <param name="texture">The target <see cref="Texture3D{T}"/> instance to write data to.</param>
        /// <param name="source">The input <see cref="ReadOnlySpan{T}"/> to read data from.</param>
        public static void SetData<T>(this Texture3D<T> texture, ReadOnlySpan<T> source)
            where T : unmanaged
        {
            texture.SetData(source, 0, 0, 0, texture.Width, texture.Height, texture.Depth);
        }

#if NET5_0
        /// <summary>
        /// Writes the contents of a given <see cref="ReadOnlySpan{T}"/> to a specified area of the current <see cref="Texture3D{T}"/> instance.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the texture.</typeparam>
        /// <param name="texture">The target <see cref="Texture3D{T}"/> instance to write data to.</param>
        /// <param name="source">The input <see cref="ReadOnlySpan{T}"/> to read data from.</param>
        /// <param name="x">The horizontal range of items to write.</param>
        /// <param name="y">The vertical range of items to write.</param>
        /// <param name="z">The depthwise range of items to write.</param>
        public static void SetData<T>(this Texture3D<T> texture, ReadOnlySpan<T> source, Range x, Range y, Range z)
            where T : unmanaged
        {
            var (offsetX, width) = x.GetOffsetAndLength(texture.Width);
            var (offsetY, height) = y.GetOffsetAndLength(texture.Height);
            var (offsetZ, depth) = z.GetOffsetAndLength(texture.Depth);

            texture.SetData(source, offsetX, offsetY, offsetZ, width, height, depth);
        }
#endif

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
        public static void SetData<T>(this Texture3D<T> texture, ReadOnlySpan<T> source, int x, int y, int z, int width, int height, int depth)
            where T : unmanaged
        {
            texture.SetData(ref MemoryMarshal.GetReference(source), source.Length, x, y, z, width, height, depth);
        }
    }
}
