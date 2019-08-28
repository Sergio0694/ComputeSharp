using System;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;

namespace ComputeSharp.Core
{
    /// <summary>
    /// A <see langword="class"/> that contains extension methods for the <see cref="Span{T}"/>
    /// </summary>
    public static class SpanExtensions
    {
        /// <summary>
        /// Creates a <see cref="Span{T}"/> from a given 2D <typeparamref name="T"/> array
        /// </summary>
        /// <typeparam name="T">The type of items in the input array</typeparam>
        /// <param name="array">The input 2D <typeparamref name="T"/> array</param>
        /// <returns>A <see cref="Span{T}"/> that maps all the items of the input array</returns>
        [Pure]
        public static Span<T> AsSpan<T>(this T[,] array)
        {
            ref T r0 = ref array[0, 0];

            return MemoryMarshal.CreateSpan(ref r0, array.Length);
        }

        /// <summary>
        /// Creates a <see cref="Span{T}"/> from a given 2D <typeparamref name="T"/> array
        /// </summary>
        /// <typeparam name="T">The type of items in the input array</typeparam>
        /// <param name="array">The input 2D <typeparamref name="T"/> array</param>
        /// <param name="startIndex">The index of the initial row to map from the input array</param>
        /// <returns>A <see cref="Span{T}"/> that maps the desired items of the input array</returns>
        [Pure]
        public static Span<T> AsSpan<T>(this T[,] array, Index startIndex)
        {
            int
                rows = array.GetLength(0),
                offset = startIndex.GetOffset(rows),
                interval = rows - offset;
            ref T r0 = ref array[offset, 0];

            return MemoryMarshal.CreateSpan(ref r0, interval * array.GetLength(1));
        }

        /// <summary>
        /// Creates a <see cref="Span{T}"/> from a given 2D <typeparamref name="T"/> array
        /// </summary>
        /// <typeparam name="T">The type of items in the input array</typeparam>
        /// <param name="array">The input 2D <typeparamref name="T"/> array</param>
        /// <param name="start">The index of the initial row to map from the input array</param>
        /// <returns>A <see cref="Span{T}"/> that maps the desired items of the input array</returns>
        [Pure]
        public static Span<T> AsSpan<T>(this T[,] array, int start)
        {
            int interval = array.GetLength(0) - start;
            ref T r0 = ref array[start, 0];

            return MemoryMarshal.CreateSpan(ref r0, interval * array.GetLength(1));
        }

        /// <summary>
        /// Creates a <see cref="Span{T}"/> from a given 2D <typeparamref name="T"/> array
        /// </summary>
        /// <typeparam name="T">The type of items in the input array</typeparam>
        /// <param name="array">The input 2D <typeparamref name="T"/> array</param>
        /// <param name="start">The index of the initial row to map from the input array</param>
        /// <param name="length">The number of rows to map from the input array</param>
        /// <returns>A <see cref="Span{T}"/> that maps the desired items of the input array</returns>
        [Pure]
        public static Span<T> AsSpan<T>(this T[,] array, int start, int length)
        {
            ref T r0 = ref array[start, 0];

            return MemoryMarshal.CreateSpan(ref r0, length * array.GetLength(1));
        }

        /// <summary>
        /// Creates a <see cref="Span{T}"/> from a given 2D <typeparamref name="T"/> array
        /// </summary>
        /// <typeparam name="T">The type of items in the input array</typeparam>
        /// <param name="array">The input 2D <typeparamref name="T"/> array</param>
        /// <param name="range">The range of rows to map from the input array</param>
        /// <returns>A <see cref="Span{T}"/> that maps the desired items of the input array</returns>
        [Pure]
        public static Span<T> AsSpan<T>(this T[,] array, Range range)
        {
            var indices = range.GetOffsetAndLength(array.GetLength(0));
            ref T r0 = ref array[indices.Offset, 0];

            return MemoryMarshal.CreateSpan(ref r0, indices.Length * array.GetLength(1));
        }
    }
}
