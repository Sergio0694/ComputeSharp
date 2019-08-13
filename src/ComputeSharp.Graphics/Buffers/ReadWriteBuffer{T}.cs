using System;
using System.Runtime.CompilerServices;
using ComputeSharp.Graphics.Buffers.Abstract;
using SharpDX.Direct3D12;

namespace ComputeSharp.Graphics.Buffers
{
    /// <summary>
    /// A <see langword="class"/> representing a typed read write buffer stored on GPU memory
    /// </summary>
    /// <typeparam name="T">The type of items stored on the buffer</typeparam>
    public sealed class ReadWriteBuffer<T> : HlslBuffer<T> where T : unmanaged
    {
        /// <summary>
        /// Creates a new <see cref="ReadWriteBuffer{T}"/> instance with the specified parameters
        /// </summary>
        /// <param name="device">The <see cref="GraphicsDevice"/> associated with the current instance</param>
        /// <param name="size">The number of items to store in the current buffer</param>
        internal ReadWriteBuffer(GraphicsDevice device, int size) : base(device, size, size * Unsafe.SizeOf<T>(), HeapType.Default)
        {

        }

        /// <summary>
        /// Gets or sets a single <typeparamref name="T"/> value from the current read write buffer
        /// </summary>
        /// <param name="i">The index of the value to get or set</param>
        /// <remarks>This API can only be used from a compute shader, and will always throw if used anywhere else</remarks>
        public T this[uint i]
        {
            get => throw new InvalidOperationException("The indexer APIs can only be used from a compute shader");
            set => throw new InvalidOperationException("The indexer APIs can only be used from a compute shader");
        }

        /// <inheritdoc/>
        public override void GetData(Span<T> span)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override void SetData(Span<T> span)
        {
            throw new NotImplementedException();
        }
    }
}
