using System;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using ComputeSharp.Resources;
using Microsoft.Toolkit.Diagnostics;
#if NET5_0
using GC = System.GC;
#else
using GC = Polyfills.GC;
#endif

namespace ComputeSharp
{
    /// <summary>
    /// A <see langword="class"/> that contains extension methods for the <see cref="Buffer{T}"/> type.
    /// </summary>
    public static class BufferExtensions
    {
        /// <summary>
        /// Reads the contents of a <see cref="Buffer{T}"/> instance and returns an array.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the buffer.</typeparam>
        /// <param name="buffer">The input <see cref="Buffer{T}"/> instance to read data from.</param>
        /// <returns>A <typeparamref name="T"/> array with the contents of the input buffer.</returns>
        [Pure]
        public static T[] GetData<T>(this Buffer<T> buffer)
            where T : unmanaged
        {
            return buffer.GetData(0, buffer.Length);
        }

        /// <summary>
        /// Reads the contents of a <see cref="Buffer{T}"/> instance in a given range and returns an array.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the buffer.</typeparam>
        /// <param name="buffer">The input <see cref="Buffer{T}"/> instance to read data from.</param>
        /// <param name="offset">The offset to start reading data from.</param>
        /// <param name="count">The number of items to read.</param>
        /// <returns>A <typeparamref name="T"/> array with the contents of the specified range from the current buffer.</returns>
        [Pure]
        public static T[] GetData<T>(this Buffer<T> buffer, int offset, int count)
            where T : unmanaged
        {
            Guard.IsGreaterThanOrEqualTo(count, 0, nameof(count));

            T[] data = GC.AllocateUninitializedArray<T>(count);

            buffer.GetData(data.AsSpan(), offset);

            return data;
        }

        /// <summary>
        /// Reads the contents of s <see cref="Buffer{T}"/> instance and writes them into a target array.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the buffer.</typeparam>
        /// <param name="buffer">The input <see cref="Buffer{T}"/> instance to read data from.</param>
        /// <param name="destination">The input array to write data to.</param>
        public static void GetData<T>(this Buffer<T> buffer, T[] destination)
            where T : unmanaged
        {
            buffer.GetData(destination.AsSpan(), 0);
        }

        /// <summary>
        /// Reads the contents of the specified range from s <see cref="Buffer{T}"/> instance and writes them into a target array.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the buffer.</typeparam>
        /// <param name="buffer">The input <see cref="Buffer{T}"/> instance to read data from.</param>
        /// <param name="destination">The input array to write data to.</param>
        /// <param name="destinationOffset">The starting offset within <paramref name="source"/> to write data to.</param>
        /// <param name="bufferOffset">The offset to start reading data from.</param>
        /// <param name="count">The number of items to read.</param>
        public static void GetData<T>(this Buffer<T> buffer, T[] destination, int destinationOffset, int bufferOffset, int count)
            where T : unmanaged
        {
            Span<T> span = destination.AsSpan(destinationOffset, count);

            buffer.GetData(span, bufferOffset);
        }

        /// <summary>
        /// Reads the contents of a <see cref="Buffer{T}"/> instance and writes them into a target <see cref="Span{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the buffer.</typeparam>
        /// <param name="buffer">The input <see cref="Buffer{T}"/> instance to read data from.</param>
        /// <param name="destination">The input <see cref="Span{T}"/> to write data to.</param>
        public static void GetData<T>(this Buffer<T> buffer, Span<T> destination)
            where T : unmanaged
        {
            buffer.GetData(destination, 0);
        }

        /// <summary>
        /// Reads the contents of the specified range from a <see cref="Buffer{T}"/> instance and writes them into a target <see cref="Span{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the buffer.</typeparam>
        /// <param name="buffer">The input <see cref="Buffer{T}"/> instance to read data from.</param>
        /// <param name="destination">The input <see cref="Span{T}"/> to write data to.</param>
        /// <param name="offset">The offset to start reading data from.</param>
        public static void GetData<T>(this Buffer<T> buffer, Span<T> destination, int offset)
            where T : unmanaged
        {
            buffer.GetData(ref MemoryMarshal.GetReference(destination), destination.Length, offset);
        }

        /// <summary>
        /// Writes the contents of a given <typeparamref name="T"/> array to a <see cref="Buffer{T}"/> instance.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the buffer.</typeparam>
        /// <param name="buffer">The target <see cref="Buffer{T}"/> instance to write data to.</param>
        /// <param name="source">The input <typeparamref name="T"/> array to read data from.</param>
        public static void SetData<T>(this Buffer<T> buffer, T[] source)
            where T : unmanaged
        {
            buffer.SetData(source.AsSpan(), 0);
        }

        /// <summary>
        /// Writes the contents of a given <typeparamref name="T"/> array to a specified area of a <see cref="Buffer{T}"/> instance.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the buffer.</typeparam>
        /// <param name="buffer">The target <see cref="Buffer{T}"/> instance to write data to.</param>
        /// <param name="source">The input <typeparamref name="T"/> array to read data from.</param>
        /// <param name="sourceOffset">The starting offset within <paramref name="source"/> to read data from.</param>
        /// <param name="bufferOffset">The offset to start writing data to.</param>
        /// <param name="count">The number of items to write.</param>
        public static void SetData<T>(this Buffer<T> buffer, T[] source, int sourceOffset, int bufferOffset, int count)
            where T : unmanaged
        {
            ReadOnlySpan<T> span = source.AsSpan(sourceOffset, count);

            buffer.SetData(span, bufferOffset);
        }

        /// <summary>
        /// Writes the contents of a given <see cref="ReadOnlySpan{T}"/> to a <see cref="Buffer{T}"/> instance.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the buffer.</typeparam>
        /// <param name="buffer">The target <see cref="Buffer{T}"/> instance to write data to.</param>
        /// <param name="source">The input <see cref="ReadOnlySpan{T}"/> to read data from.</param>
        public static void SetData<T>(this Buffer<T> buffer, ReadOnlySpan<T> source)
            where T : unmanaged
        {
            buffer.SetData(source, 0);
        }

        /// <summary>
        /// Writes the contents of a given <see cref="ReadOnlySpan{T}"/> to a specified area of a <see cref="Buffer{T}"/> instance.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the buffer.</typeparam>
        /// <param name="buffer">The target <see cref="Buffer{T}"/> instance to write data to.</param>
        /// <param name="source">The input <see cref="ReadOnlySpan{T}"/> to read data from.</param>
        /// <param name="offset">The offset to start writing data to.</param>
        public static void SetData<T>(this Buffer<T> buffer, ReadOnlySpan<T> source, int offset)
            where T : unmanaged
        {
            buffer.SetData(ref MemoryMarshal.GetReference(source), source.Length, offset);
        }
    }
}
