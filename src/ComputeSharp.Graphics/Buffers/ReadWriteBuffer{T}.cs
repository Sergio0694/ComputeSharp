using System;
using System.Runtime.CompilerServices;
using ComputeSharp.Graphics.Buffers.Abstract;
using ComputeSharp.Graphics.Helpers;
using SharpDX.Direct3D12;
using CommandList = ComputeSharp.Graphics.Commands.CommandList;

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
        internal ReadWriteBuffer(GraphicsDevice device, int size) : base(device, size, size * Unsafe.SizeOf<T>(), HeapType.Default) { }

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
            using Buffer<T> readbackBaffer = new Buffer<T>(GraphicsDevice, Size, SizeInBytes, HeapType.Readback);
            using CommandList copyCommandList = new CommandList(GraphicsDevice, CommandListType.Copy);

            copyCommandList.CopyBufferRegion(this, 0, readbackBaffer, 0, SizeInBytes);
            copyCommandList.Flush();

            readbackBaffer.Map(0);
            MemoryHelper.Copy(readbackBaffer.MappedResource, span);
            readbackBaffer.Unmap(0);
        }

        /// <inheritdoc/>
        public override void SetData(Span<T> span)
        {
            using Buffer<T> uploadBuffer = new Buffer<T>(GraphicsDevice, Size, SizeInBytes, HeapType.Upload);

            uploadBuffer.Map(0);
            MemoryHelper.Copy(span, uploadBuffer.MappedResource);
            uploadBuffer.Unmap(0);

            using CommandList copyCommandList = new CommandList(GraphicsDevice, CommandListType.Copy);

            copyCommandList.CopyBufferRegion(uploadBuffer, 0, this, 0, SizeInBytes);
            copyCommandList.Flush();
        }

        /// <inheritdoc/>
        public override void SetData(HlslBuffer<T> buffer)
        {
            if (buffer is ConstantBuffer<T> readOnlyBuffer && !readOnlyBuffer.IsPaddingPresent)
            {
                // Directly copy the input buffer
                using CommandList copyCommandList = new CommandList(GraphicsDevice, CommandListType.Copy);

                copyCommandList.CopyBufferRegion(buffer, 0, this, 0, SizeInBytes);
                copyCommandList.Flush();
            }
            else SetDataWithCpuBuffer(buffer);
        }
    }
}
