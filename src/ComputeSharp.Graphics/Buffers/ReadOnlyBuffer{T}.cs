using System.Diagnostics;
using ComputeSharp.Exceptions;
using ComputeSharp.Graphics;
using ComputeSharp.Graphics.Buffers.Abstract;
using ComputeSharp.Graphics.Buffers.Enums;
using ComputeSharp.Graphics.Buffers.Views;

namespace ComputeSharp
{
    /// <summary>
    /// A <see langword="class"/> representing a typed readonly buffer stored on GPU memory.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the buffer.</typeparam>
    [DebuggerTypeProxy(typeof(BufferDebugView<>))]
    [DebuggerDisplay("{ToString(),raw}")]
    public sealed class ReadOnlyBuffer<T> : StructuredBuffer<T>
        where T : unmanaged
    {
        /// <summary>
        /// Creates a new <see cref="ReadOnlyBuffer{T}"/> instance with the specified parameters.
        /// </summary>
        /// <param name="device">The <see cref="GraphicsDevice"/> associated with the current instance.</param>
        /// <param name="length">The number of items to store in the current buffer.</param>
        internal ReadOnlyBuffer(GraphicsDevice device, int length)
            : base(device, length, BufferType.ReadOnly)
        {
        }

        /// <summary>
        /// Gets a single <typeparamref name="T"/> value from the current readonly buffer.
        /// </summary>
        /// <param name="i">The index of the value to get.</param>
        /// <remarks>This API can only be used from a compute shader, and will always throw if used anywhere else.</remarks>
        public T this[int i] => throw new InvalidExecutionContextException($"{nameof(ReadOnlyBuffer<T>)}<T>[int]");

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"ComputeSharp.ReadOnlyBuffer<{typeof(T)}>[{Length}]";
        }
    }
}
