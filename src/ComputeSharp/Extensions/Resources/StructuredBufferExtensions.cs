using ComputeSharp.Resources;

namespace ComputeSharp
{
    /// <summary>
    /// A <see langword="class"/> that contains extension methods for the <see cref="StructuredBuffer{T}"/> type.
    /// </summary>
    public static class StructuredBufferExtensions
    {
        /// <summary>
        /// Reads the contents of a <see cref="StructuredBuffer{T}"/> instance and writes them into a target <see cref="ReadBackBuffer{T}"/> instance.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the buffer.</typeparam>
        /// <param name="buffer">The input <see cref="StructuredBuffer{T}"/> instance to read data from.</param>
        /// <param name="destination">The target <see cref="ReadBackBuffer{T}"/> instance to write data to.</param>
        public static void CopyTo<T>(this StructuredBuffer<T> buffer, ReadBackBuffer<T> destination)
            where T : unmanaged
        {
            buffer.CopyTo(destination, 0, buffer.Length, 0);
        }

        /// <summary>
        /// Reads the contents of a <see cref="StructuredBuffer{T}"/> instance and writes them into a target <see cref="ReadBackBuffer{T}"/> instance.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the buffer.</typeparam>
        /// <param name="buffer">The input <see cref="StructuredBuffer{T}"/> instance to read data from.</param>
        /// <param name="destination">The target <see cref="ReadBackBuffer{T}"/> instance to write data to.</param>
        /// <param name="destinationOffset">The starting offset within <paramref name="destination"/> to write data to.</param>
        /// <param name="bufferOffset">The offset to start reading data from.</param>
        /// <param name="count">The number of items to read.</param>
        public static void CopyTo<T>(this StructuredBuffer<T> buffer, ReadBackBuffer<T> destination, int destinationOffset, int bufferOffset, int count)
            where T : unmanaged
        {
            buffer.CopyTo(destination, destinationOffset, count, bufferOffset);
        }
    }
}
