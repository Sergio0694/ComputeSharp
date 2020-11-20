using System;
using System.Runtime.CompilerServices;
using ComputeSharp.Graphics.Buffers.Enums;
using ComputeSharp.Graphics.Buffers.Interop;
using ComputeSharp.Graphics.Commands;
using ComputeSharp.Graphics.Extensions;
using ComputeSharp.Graphics.Helpers;
using TerraFX.Interop;
using static TerraFX.Interop.D3D12_COMMAND_LIST_TYPE;

namespace ComputeSharp.Graphics.Buffers.Abstract
{
    /// <summary>
    /// A <see langword="class"/> representing a typed structured buffer stored on GPU memory
    /// </summary>
    /// <typeparam name="T">The type of items stored on the buffer</typeparam>
    public abstract class HlslStructuredBuffer<T> : HlslBuffer<T>
        where T : unmanaged
    {
        /// <summary>
        /// Creates a new <see cref="HlslStructuredBuffer{T}"/> instance with the specified parameters
        /// </summary>
        /// <param name="device">The <see cref="GraphicsDevice"/> associated with the current instance</param>
        /// <param name="size">The number of items to store in the current buffer</param>
        /// <param name="bufferType">The buffer type for the current buffer</param>
        internal HlslStructuredBuffer(GraphicsDevice device, int size, BufferType bufferType)
            : base(device, size, size * Unsafe.SizeOf<T>(), bufferType)
        {
        }

        /// <inheritdoc/>
        public override unsafe void GetData(Span<T> span, int offset, int count)
        {
            int
                byteOffset = offset * ElementSizeInBytes,
                byteSize = count * ElementSizeInBytes;

            using ComPtr<ID3D12Resource> d3D12Resource = GraphicsDevice.D3D12Device->CreateCommittedResource(BufferType.ReadBack, byteSize);

            using (CommandList copyCommandList = new CommandList(GraphicsDevice, D3D12_COMMAND_LIST_TYPE_COPY))
            {
                copyCommandList.CopyBufferRegion(D3D12Resource, byteOffset, d3D12Resource.Get(), 0, byteSize);
                copyCommandList.ExecuteAndWaitForCompletion();
            }

            using ID3D12ResourceMap resource = d3D12Resource.Get()->Map();

            MemoryHelper.Copy(resource.Pointer, 0, span, count);
        }

        /// <inheritdoc/>
        public override unsafe void SetData(ReadOnlySpan<T> span, int offset, int count)
        {
            int
                byteOffset = offset * ElementSizeInBytes,
                byteSize = count * ElementSizeInBytes;

            using ComPtr<ID3D12Resource> d3D12Resource = GraphicsDevice.D3D12Device->CreateCommittedResource(BufferType.Upload, byteSize);

            using (ID3D12ResourceMap resource = d3D12Resource.Get()->Map())
            {
                MemoryHelper.Copy(span, resource.Pointer, 0, count);
            }

            using CommandList copyCommandList = new CommandList(GraphicsDevice, D3D12_COMMAND_LIST_TYPE_COPY);

            copyCommandList.CopyBufferRegion(d3D12Resource.Get(), 0, D3D12Resource, byteOffset, byteSize);
            copyCommandList.ExecuteAndWaitForCompletion();
        }

        /// <inheritdoc/>
        public override unsafe void SetData(HlslBuffer<T> buffer)
        {
            if (!buffer.IsPaddingPresent)
            {
                // Directly copy the input buffer
                using CommandList copyCommandList = new CommandList(GraphicsDevice, D3D12_COMMAND_LIST_TYPE_COPY);

                copyCommandList.CopyBufferRegion(buffer.D3D12Resource, 0, D3D12Resource, 0, SizeInBytes);
                copyCommandList.ExecuteAndWaitForCompletion();
            }
            else SetDataWithCpuBuffer(buffer);
        }
    }
}
