using System;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using ComputeSharp.Resources;
using Microsoft.Toolkit.Diagnostics;

namespace ComputeSharp
{
    /// <summary>
    /// A <see langword="class"/> that contains extension methods for the <see cref="Texture2D{T}"/> type.
    /// </summary>
    public static class Texture2DExtensions
    {
        /// <summary>
        /// Reads the contents of the current <see cref="Texture2D{T}"/> instance and returns an array.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the texture.</typeparam>
        /// <param name="texture">The input <see cref="Texture2D{T}"/> instance to read data from.</param>
        /// <returns>A <typeparamref name="T"/> array with the contents of the current texture.</returns>
        [Pure]
        public static T[,] ToArray<T>(this Texture2D<T> texture)
            where T : unmanaged
        {
            T[,] data = new T[texture.Height, texture.Width];

            texture.CopyTo(data);

            return data;
        }

        /// <summary>
        /// Reads the contents of the current <see cref="Texture2D{T}"/> instance and writes them into a target array.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the texture.</typeparam>
        /// <param name="texture">The input <see cref="Texture2D{T}"/> instance to read data from.</param>
        /// <param name="destination">The input array to write data to.</param>
        public static void CopyTo<T>(this Texture2D<T> texture, T[,] destination)
            where T : unmanaged
        {
            Guard.IsEqualTo(destination.GetLength(0), texture.Height, nameof(destination));
            Guard.IsEqualTo(destination.GetLength(1), texture.Width, nameof(destination));

            texture.CopyTo(ref destination[0, 0], destination.Length, 0, 0, texture.Width, texture.Height);
        }

        /// <summary>
        /// Reads the contents of the specified range from the current <see cref="Texture2D{T}"/> instance and writes them into a target array.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the texture.</typeparam>
        /// <param name="texture">The input <see cref="Texture2D{T}"/> instance to read data from.</param>
        /// <param name="destination">The input array to write data to.</param>
        public static void CopyTo<T>(this Texture2D<T> texture, T[] destination)
            where T : unmanaged
        {
            texture.CopyTo(destination.AsSpan(), 0, 0, texture.Width, texture.Height);
        }

        /// <summary>
        /// Reads the contents of the specified range from the current <see cref="Texture2D{T}"/> instance and writes them into a target array.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the texture.</typeparam>
        /// <param name="texture">The input <see cref="Texture2D{T}"/> instance to read data from.</param>
        /// <param name="destination">The input array to write data to.</param>
        /// <param name="x">The horizontal range of items to copy.</param>
        /// <param name="y">The vertical range of items to copy.</param>
        public static void CopyTo<T>(this Texture2D<T> texture, T[] destination, Range x, Range y)
            where T : unmanaged
        {
            texture.CopyTo(destination.AsSpan(), x, y);
        }

        /// <summary>
        /// Reads the contents of the specified range from the current <see cref="Texture2D{T}"/> instance and writes them into a target array.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the texture.</typeparam>
        /// <param name="texture">The input <see cref="Texture2D{T}"/> instance to read data from.</param>
        /// <param name="destination">The input array to write data to.</param>
        /// <param name="x">The horizontal offset in the source texture.</param>
        /// <param name="y">The vertical offset in the source texture.</param>
        /// <param name="width">The width of the memory area to copy.</param>
        /// <param name="height">The height of the memory area to copy.</param>
        public static void CopyTo<T>(this Texture2D<T> texture, T[] destination, int x, int y, int width, int height)
            where T : unmanaged
        {
            texture.CopyTo(destination.AsSpan(), x, y, width, height);
        }

        /// <summary>
        /// Reads the contents of the specified range from the current <see cref="Texture2D{T}"/> instance and writes them into a target array.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the texture.</typeparam>
        /// <param name="texture">The input <see cref="Texture2D{T}"/> instance to read data from.</param>
        /// <param name="destination">The input array to write data to.</param>
        /// <param name="offset">The starting offset within <paramref name="destination"/> to write data to.</param>
        public static void CopyTo<T>(this Texture2D<T> texture, T[] destination, int offset)
            where T : unmanaged
        {
            texture.CopyTo(destination.AsSpan(offset), 0, 0, texture.Width, texture.Height);
        }

        /// <summary>
        /// Reads the contents of the specified range from the current <see cref="Texture2D{T}"/> instance and writes them into a target array.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the texture.</typeparam>
        /// <param name="texture">The input <see cref="Texture2D{T}"/> instance to read data from.</param>
        /// <param name="destination">The input array to write data to.</param>
        /// <param name="offset">The starting offset within <paramref name="destination"/> to write data to.</param>
        /// <param name="x">The horizontal range of items to copy.</param>
        /// <param name="y">The vertical range of items to copy.</param>
        public static void CopyTo<T>(this Texture2D<T> texture, T[] destination, int offset, Range x, Range y)
            where T : unmanaged
        {
            texture.CopyTo(destination.AsSpan(offset), x, y);
        }

        /// <summary>
        /// Reads the contents of the specified range from the current <see cref="Texture2D{T}"/> instance and writes them into a target array.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the texture.</typeparam>
        /// <param name="texture">The input <see cref="Texture2D{T}"/> instance to read data from.</param>
        /// <param name="destination">The input array to write data to.</param>
        /// <param name="offset">The starting offset within <paramref name="destination"/> to write data to.</param>
        /// <param name="x">The horizontal offset in the source texture.</param>
        /// <param name="y">The vertical offset in the source texture.</param>
        /// <param name="width">The width of the memory area to copy.</param>
        /// <param name="height">The height of the memory area to copy.</param>
        public static void CopyTo<T>(this Texture2D<T> texture, T[] destination, int offset, int x, int y, int width, int height)
            where T : unmanaged
        {
            texture.CopyTo(destination.AsSpan(offset), x, y, width, height);
        }

        /// <summary>
        /// Reads the contents of the current <see cref="Texture2D{T}"/> instance and writes them into a target <see cref="Span{T}"/>.
        /// The input data will be read from the start of the texture.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the texture.</typeparam>
        /// <param name="texture">The input <see cref="Texture2D{T}"/> instance to read data from.</param>
        /// <param name="destination">The input <see cref="Span{T}"/> to write data to.</param>
        public static void CopyTo<T>(this Texture2D<T> texture, Span<T> destination)
            where T : unmanaged
        {
            texture.CopyTo(destination, 0, 0, texture.Width, texture.Height);
        }

        /// <summary>
        /// Reads the contents of the specified range from the current <see cref="Texture2D{T}"/> instance and writes them into a target <see cref="Span{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the texture.</typeparam>
        /// <param name="texture">The input <see cref="Texture2D{T}"/> instance to read data from.</param>
        /// <param name="destination">The input <see cref="Span{T}"/> to write data to.</param>
        /// <param name="x">The horizontal range of items to copy.</param>
        /// <param name="y">The vertical range of items to copy.</param>
        public static void CopyTo<T>(this Texture2D<T> texture, Span<T> destination, Range x, Range y)
            where T : unmanaged
        {
            var (offsetX, width) = x.GetOffsetAndLength(texture.Width);
            var (offsetY, height) = y.GetOffsetAndLength(texture.Height);

            texture.CopyTo(destination, offsetX, offsetY, width, height);
        }

        /// <summary>
        /// Reads the contents of the specified range from the current <see cref="Texture2D{T}"/> instance and writes them into a target <see cref="Span{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the texture.</typeparam>
        /// <param name="texture">The input <see cref="Texture2D{T}"/> instance to read data from.</param>
        /// <param name="destination">The input <see cref="Span{T}"/> to write data to.</param>
        /// <param name="x">The horizontal offset in the source texture.</param>
        /// <param name="y">The vertical offset in the source texture.</param>
        /// <param name="width">The width of the memory area to copy.</param>
        /// <param name="height">The height of the memory area to copy.</param>
        public static void CopyTo<T>(this Texture2D<T> texture, Span<T> destination, int x, int y, int width, int height)
            where T : unmanaged
        {
            texture.CopyTo(ref MemoryMarshal.GetReference(destination), destination.Length, x, y, width, height);
        }

        /// <summary>
        /// Reads the contents of a <see cref="Texture2D{T}"/> instance and writes them into a target <see cref="ReadBackTexture2D{T}"/> instance.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the texture.</typeparam>
        /// <param name="texture">The input <see cref="Texture2D{T}"/> instance to read data from.</param>
        /// <param name="destination">The target <see cref="ReadBackTexture2D{T}"/> instance to write data to.</param>
        public static void CopyTo<T>(this Texture2D<T> texture, ReadBackTexture2D<T> destination)
            where T : unmanaged
        {
            texture.CopyTo(destination, 0, 0, 0, 0, texture.Width, texture.Height);
        }

        /// <summary>
        /// Reads the contents of a <see cref="Texture2D{T}"/> instance and writes them into a target <see cref="ReadBackTexture2D{T}"/> instance.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the texture.</typeparam>
        /// <param name="texture">The input <see cref="Texture2D{T}"/> instance to read data from.</param>
        /// <param name="destination">The target <see cref="ReadBackTexture2D{T}"/> instance to write data to.</param>
        /// <param name="x">The horizontal offset in the source texture.</param>
        /// <param name="y">The vertical offset in the source texture.</param>
        /// <param name="width">The width of the memory area to copy.</param>
        /// <param name="height">The height of the memory area to copy.</param>
        public static void CopyTo<T>(this Texture2D<T> texture, ReadBackTexture2D<T> destination, int x, int y, int width, int height)
            where T : unmanaged
        {
            texture.CopyTo(destination, 0, 0, x, y, width, height);
        }

        /// <summary>
        /// Writes the contents of a given <typeparamref name="T"/> array to the current <see cref="Texture2D{T}"/> instance.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the texture.</typeparam>
        /// <param name="texture">The target <see cref="Texture2D{T}"/> instance to write data to.</param>
        /// <param name="source">The input <typeparamref name="T"/> array to read data from.</param>
        public static unsafe void CopyFrom<T>(this Texture2D<T> texture, T[,] source)
            where T : unmanaged
        {
            Guard.IsEqualTo(source.GetLength(0), texture.Height, nameof(source));
            Guard.IsEqualTo(source.GetLength(1), texture.Width, nameof(source));

            texture.CopyFrom(ref source[0, 0], source.Length, 0, 0, texture.Width, texture.Height);
        }

        /// <summary>
        /// Writes the contents of a given <typeparamref name="T"/> array to the current <see cref="Texture2D{T}"/> instance.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the texture.</typeparam>
        /// <param name="texture">The target <see cref="Texture2D{T}"/> instance to write data to.</param>
        /// <param name="source">The input <typeparamref name="T"/> array to read data from.</param>
        public static void CopyFrom<T>(this Texture2D<T> texture, T[] source)
            where T : unmanaged
        {
            texture.CopyFrom(source.AsSpan(), 0, 0, texture.Width, texture.Height);
        }

        /// <summary>
        /// Writes the contents of a given <typeparamref name="T"/> array to a specified area of the current <see cref="Texture2D{T}"/> instance.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the texture.</typeparam>
        /// <param name="texture">The target <see cref="Texture2D{T}"/> instance to write data to.</param>
        /// <param name="source">The input <typeparamref name="T"/> array to read data from.</param>
        /// <param name="x">The horizontal range of items to write.</param>
        /// <param name="y">The vertical range of items to write.</param>
        public static void CopyFrom<T>(this Texture2D<T> texture, T[] source, Range x, Range y)
            where T : unmanaged
        {
            texture.CopyFrom(source.AsSpan(), x, y);
        }

        /// <summary>
        /// Writes the contents of a given <typeparamref name="T"/> array to a specified area of the current <see cref="Texture2D{T}"/> instance.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the texture.</typeparam>
        /// <param name="texture">The target <see cref="Texture2D{T}"/> instance to write data to.</param>
        /// <param name="source">The input <typeparamref name="T"/> array to read data from.</param>
        /// <param name="x">The horizontal offset in the destination texture.</param>
        /// <param name="y">The vertical offset in the destination texture.</param>
        /// <param name="width">The width of the memory area to write to.</param>
        /// <param name="height">The height of the memory area to write to.</param>
        public static void CopyFrom<T>(this Texture2D<T> texture, T[] source, int x, int y, int width, int height)
            where T : unmanaged
        {
            texture.CopyFrom(source.AsSpan(), x, y, width, height);
        }

        /// <summary>
        /// Writes the contents of a given <typeparamref name="T"/> array to a specified area of the current <see cref="Texture2D{T}"/> instance.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the texture.</typeparam>
        /// <param name="texture">The target <see cref="Texture2D{T}"/> instance to write data to.</param>
        /// <param name="source">The input <typeparamref name="T"/> array to read data from.</param>
        /// <param name="offset">The starting offset within <paramref name="source"/> to read data from.</param>
        /// <param name="x">The horizontal range of items to write.</param>
        /// <param name="y">The vertical range of items to write.</param>
        public static void CopyFrom<T>(this Texture2D<T> texture, T[] source, int offset, Range x, Range y)
            where T : unmanaged
        {
            texture.CopyFrom(source.AsSpan(offset), x, y);
        }

        /// <summary>
        /// Writes the contents of a given <typeparamref name="T"/> array to a specified area of the current <see cref="Texture2D{T}"/> instance.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the texture.</typeparam>
        /// <param name="texture">The target <see cref="Texture2D{T}"/> instance to write data to.</param>
        /// <param name="source">The input <typeparamref name="T"/> array to read data from.</param>
        /// <param name="offset">The starting offset within <paramref name="source"/> to read data from.</param>
        /// <param name="x">The horizontal offset in the destination texture.</param>
        /// <param name="y">The vertical offset in the destination texture.</param>
        /// <param name="width">The width of the memory area to write to.</param>
        /// <param name="height">The height of the memory area to write to.</param>
        public static void CopyFrom<T>(this Texture2D<T> texture, T[] source, int offset, int x, int y, int width, int height)
            where T : unmanaged
        {
            texture.CopyFrom(source.AsSpan(offset), x, y, width, height);
        }

        /// <summary>
        /// Writes the contents of a given <see cref="ReadOnlySpan{T}"/> to the current <see cref="Texture2D{T}"/> instance.
        /// The input data will be written to the start of the texture, and all input items will be copied.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the texture.</typeparam>
        /// <param name="texture">The target <see cref="Texture2D{T}"/> instance to write data to.</param>
        /// <param name="source">The input <see cref="ReadOnlySpan{T}"/> to read data from.</param>
        public static void CopyFrom<T>(this Texture2D<T> texture, ReadOnlySpan<T> source)
            where T : unmanaged
        {
            texture.CopyFrom(source, 0, 0, texture.Width, texture.Height);
        }

        /// <summary>
        /// Writes the contents of a given <see cref="ReadOnlySpan{T}"/> to a specified area of the current <see cref="Texture2D{T}"/> instance.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the texture.</typeparam>
        /// <param name="texture">The target <see cref="Texture2D{T}"/> instance to write data to.</param>
        /// <param name="source">The input <see cref="ReadOnlySpan{T}"/> to read data from.</param>
        /// <param name="x">The horizontal range of items to write.</param>
        /// <param name="y">The vertical range of items to write.</param>
        public static void CopyFrom<T>(this Texture2D<T> texture, ReadOnlySpan<T> source, Range x, Range y)
            where T : unmanaged
        {
            var (offsetX, width) = x.GetOffsetAndLength(texture.Width);
            var (offsetY, height) = y.GetOffsetAndLength(texture.Height);

            texture.CopyFrom(source, offsetX, offsetY, width, height);
        }

        /// <summary>
        /// Writes the contents of a given <see cref="ReadOnlySpan{T}"/> to a specified area of the current <see cref="Texture2D{T}"/> instance.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the texture.</typeparam>
        /// <param name="texture">The target <see cref="Texture2D{T}"/> instance to write data to.</param>
        /// <param name="source">The input <see cref="ReadOnlySpan{T}"/> to read data from.</param>
        /// <param name="x">The horizontal offset in the destination texture.</param>
        /// <param name="y">The vertical offset in the destination texture.</param>
        /// <param name="width">The width of the memory area to write to.</param>
        /// <param name="height">The height of the memory area to write to.</param>
        public static void CopyFrom<T>(this Texture2D<T> texture, ReadOnlySpan<T> source, int x, int y, int width, int height)
            where T : unmanaged
        {
            texture.CopyFrom(ref MemoryMarshal.GetReference(source), source.Length, x, y, width, height);
        }
    }
}
