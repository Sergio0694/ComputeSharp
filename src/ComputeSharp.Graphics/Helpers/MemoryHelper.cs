using System;
using System.Runtime.CompilerServices;

namespace ComputeSharp.Graphics.Helpers
{
    /// <summary>
    /// A <see langword="class"/> with some helper methods to copy data between memory areas.
    /// </summary>
    internal static class MemoryHelper
    {
        /// <summary>
        /// Copies the content of a <see cref="ReadOnlySpan{T}"/> to the area pointed by an input pointer.
        /// </summary>
        /// <typeparam name="T">The type of values in the input <see cref="ReadOnlySpan{T}"/>.</typeparam>
        /// <param name="source">The source <see cref="ReadOnlySpan{T}"/> to read.</param>
        /// <param name="destination">The pointer for the destination memory area.</param>
        /// <param name="destinationOffset">The destination offset to start writing data to.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void Copy<T>(ReadOnlySpan<T> source, void* destination, int destinationOffset)
            where T : unmanaged
        {
            source.CopyTo(new Span<T>((T*)destination + destinationOffset, source.Length));
        }

        /// <summary>
        /// Copies a memory area pointed by a pointer to a target <see cref="Span{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of values to read.</typeparam>
        /// <param name="source">The pointer that indicates the memory area to read from.</param>
        /// <param name="sourceOffset">The source offset to start reading data from.</param>
        /// <param name="destination">The destination <see cref="Span{T}"/> to write.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void Copy<T>(void* source, int sourceOffset, Span<T> destination)
            where T : unmanaged
        {
            new Span<T>((T*)source + sourceOffset, destination.Length).CopyTo(destination);
        }

        /// <summary>
        /// Copies the content of a <see cref="ReadOnlySpan{T}"/> to the 2D area pointed by an input pointer.
        /// </summary>
        /// <typeparam name="T">The type of values to read.</typeparam>
        /// <param name="source">The destination <see cref="Span{T}"/> to read from.</param>
        /// <param name="destination">The pointer that indicates the 2D memory area to write to.</param>
        /// <param name="width">The width of the 2D memory area to read.</param>
        /// <param name="height">The height of the 2D memory area to read.</param>
        /// <param name="widthInBytes">The width of the memory area in bytes.</param>
        /// <param name="pitchInBytes">The pitch (padded width) of the memory area in bytes.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void Copy<T>(
            ReadOnlySpan<T> source,
            void* destination,
            int width,
            int height,
            ulong widthInBytes,
            ulong pitchInBytes)
            where T : unmanaged
        {
            if (widthInBytes == pitchInBytes)
            {
                source.CopyTo(new Span<T>(destination, width * height));
            }
            else
            {
                for (int i = 0; i < height; i++)
                {
                    ReadOnlySpan<T> sourceRow = source.Slice(i * width, width);
                    Span<T> destinationRow = new((byte*)destination + (uint)i * pitchInBytes, width);

                    sourceRow.CopyTo(destinationRow);
                }
            }
        }

        /// <summary>
        /// Copies a 2D memory area pointed by a pointer value to a target <see cref="Span{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of values to read.</typeparam>
        /// <param name="source">The pointer that indicates the memory area to read from.</param>
        /// <param name="width">The width of the 2D memory area to read.</param>
        /// <param name="height">The height of the 2D memory area to read.</param>
        /// <param name="widthInBytes">The width of the memory area in bytes.</param>
        /// <param name="pitchInBytes">The pitch (padded width) of the memory area in bytes.</param>
        /// <param name="destination">The destination <see cref="Span{T}"/> to write.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void Copy<T>(
            void* source,
            int width,
            int height,
            ulong widthInBytes,
            ulong pitchInBytes,
            Span<T> destination)
            where T : unmanaged
        {
            if (widthInBytes == pitchInBytes)
            {
                new Span<T>(source, width * height).CopyTo(destination);
            }
            else
            {
                for (int i = 0; i < height; i++)
                {
                    ReadOnlySpan<T> sourceRow = new((byte*)source + (uint)i * pitchInBytes, width);
                    Span<T> destinationRow = destination.Slice(i * width, width);

                    sourceRow.CopyTo(destinationRow);
                }
            }
        }
    }
}
