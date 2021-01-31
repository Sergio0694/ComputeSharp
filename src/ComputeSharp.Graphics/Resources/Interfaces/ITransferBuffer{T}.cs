using System;

namespace ComputeSharp
{
    /// <summary>
    /// An interface representing a typed readonly buffer used to transfer data to the GPU.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the buffer.</typeparam>
    internal interface ITransferBuffer<T>
        where T : unmanaged
    {
        /// <summary>
        /// Gets a <see cref="Span{T}"/> with the data of the current buffer.
        /// </summary>
        Span<T> Span { get; }
    }
}
