using System;
using System.Runtime.CompilerServices;
using ComputeSharp.Graphics.Buffers.Enums;
using ComputeSharp.Graphics.Helpers;
using Vortice.DirectX.Direct3D12;
using CommandList = ComputeSharp.Graphics.Commands.CommandList;

namespace ComputeSharp.Graphics.Buffers.Abstract
{
    /// <summary>
    /// A <see langword="class"/> representing a typed structured buffer stored on GPU memory
    /// </summary>
    /// <typeparam name="T">The type of items stored on the buffer</typeparam>
    public abstract class HlslStructuredBuffer<T> : HlslBuffer<T> where T : unmanaged
    {
        /// <summary>
        /// Creates a new <see cref="HlslStructuredBuffer{T}"/> instance with the specified parameters
        /// </summary>
        /// <param name="device">The <see cref="GraphicsDevice"/> associated with the current instance</param>
        /// <param name="size">The number of items to store in the current buffer</param>
        /// <param name="bufferType">The buffer type for the current buffer</param>
        internal HlslStructuredBuffer(GraphicsDevice device, int size, BufferType bufferType) : base(device, size, size * Unsafe.SizeOf<T>(), bufferType) { }

        /// <inheritdoc/>
        public override void GetData(Span<T> span, int offset, int count)
        {
            using Buffer<T> transferBuffer = new Buffer<T>(GraphicsDevice, count, count * ElementSizeInBytes, BufferType.ReadBack);
            using CommandList copyCommandList = new CommandList(GraphicsDevice, CommandListType.Copy);

            copyCommandList.CopyBufferRegion(this, offset * ElementSizeInBytes, transferBuffer, 0, count * ElementSizeInBytes);
            copyCommandList.Flush();

            transferBuffer.Map(0);
            MemoryHelper.Copy(transferBuffer.MappedResource, 0, span, 0, count);
            transferBuffer.Unmap(0);
        }

        /// <inheritdoc/>
        public override void SetData(Span<T> span, int offset, int count)
        {
            using Buffer<T> transferBuffer = new Buffer<T>(GraphicsDevice, count, count * ElementSizeInBytes, BufferType.Transfer);

            transferBuffer.Map(0);
            MemoryHelper.Copy(span, 0, transferBuffer.MappedResource, 0, count);
            transferBuffer.Unmap(0);

            using CommandList copyCommandList = new CommandList(GraphicsDevice, CommandListType.Copy);

            copyCommandList.CopyBufferRegion(transferBuffer, 0, this, offset * ElementSizeInBytes, count * ElementSizeInBytes);
            copyCommandList.Flush();
        }

        /// <inheritdoc/>
        public override void SetData(HlslBuffer<T> buffer)
        {
            if (!buffer.IsPaddingPresent)
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
