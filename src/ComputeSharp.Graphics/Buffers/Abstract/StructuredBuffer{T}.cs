using System;
using System.Runtime.CompilerServices;
using ComputeSharp.Graphics.Buffers.Enums;
using ComputeSharp.Graphics.Buffers.Interop;
using ComputeSharp.Graphics.Commands;
using ComputeSharp.Graphics.Extensions;
using ComputeSharp.Graphics.Helpers;
using Microsoft.Toolkit.Diagnostics;
using TerraFX.Interop;
using static TerraFX.Interop.D3D12_COMMAND_LIST_TYPE;

namespace ComputeSharp.Graphics.Buffers.Abstract
{
    /// <summary>
    /// A <see langword="class"/> representing a typed structured buffer stored on GPU memory.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the buffer.</typeparam>
    public abstract class StructuredBuffer<T> : Buffer<T>
        where T : unmanaged
    {
        /// <summary>
        /// Creates a new <see cref="StructuredBuffer{T}"/> instance with the specified parameters.
        /// </summary>
        /// <param name="device">The <see cref="GraphicsDevice"/> associated with the current instance.</param>
        /// <param name="length">The number of items to store in the current buffer.</param>
        /// <param name="resourceType">The buffer type for the current buffer.</param>
        private protected StructuredBuffer(GraphicsDevice device, int length, ResourceType resourceType)
            : base(device, length, (uint)Unsafe.SizeOf<T>(), resourceType)
        {
        }

        /// <inheritdoc/>
        public override unsafe void GetData(Span<T> destination, int offset)
        {
            GraphicsDevice.ThrowIfDisposed();

            ThrowIfDisposed();

            Guard.IsInRange(offset, 0, Length, nameof(offset));
            Guard.IsLessThanOrEqualTo((uint)offset + destination.Length, (uint)Length, nameof(destination));

            nint
                byteOffset = (nint)offset * ElementSizeInBytes,
                byteSize = (nint)destination.Length * ElementSizeInBytes;

            using ComPtr<ID3D12Resource> d3D12Resource = GraphicsDevice.D3D12Device->CreateCommittedResource(ResourceType.ReadBack, (ulong)byteSize);

            using (CommandList copyCommandList = new(GraphicsDevice, D3D12_COMMAND_LIST_TYPE_COPY))
            {
                copyCommandList.CopyBufferRegion(d3D12Resource.Get(), 0, D3D12Resource, (ulong)byteOffset,(ulong)byteSize);
                copyCommandList.ExecuteAndWaitForCompletion();
            }

            using ID3D12ResourceMap resource = d3D12Resource.Get()->Map();

            MemoryHelper.Copy(resource.Pointer, 0, destination);
        }

        /// <inheritdoc/>
        public override unsafe void SetData(ReadOnlySpan<T> source, int offset)
        {
            GraphicsDevice.ThrowIfDisposed();

            ThrowIfDisposed();

            Guard.IsInRange(offset, 0, Length, nameof(offset));
            Guard.IsLessThanOrEqualTo((uint)offset + source.Length, (uint)Length, nameof(source));

            nint
                byteOffset = (nint)offset * ElementSizeInBytes,
                byteSize = (nint)source.Length * ElementSizeInBytes;

            using ComPtr<ID3D12Resource> d3D12Resource = GraphicsDevice.D3D12Device->CreateCommittedResource(ResourceType.Upload, (ulong)byteSize);

            using (ID3D12ResourceMap resource = d3D12Resource.Get()->Map())
            {
                MemoryHelper.Copy(source, resource.Pointer, 0);
            }

            using CommandList copyCommandList = new(GraphicsDevice, D3D12_COMMAND_LIST_TYPE_COPY);

            copyCommandList.CopyBufferRegion(D3D12Resource, (ulong)byteOffset, d3D12Resource.Get(), 0,(ulong)byteSize);
            copyCommandList.ExecuteAndWaitForCompletion();
        }

        /// <inheritdoc/>
        public override unsafe void SetData(Buffer<T> source)
        {
            GraphicsDevice.ThrowIfDisposed();
            source.GraphicsDevice.ThrowIfDisposed();

            ThrowIfDisposed();
            source.ThrowIfDisposed();

            if (!source.IsPaddingPresent &&
                source.GraphicsDevice == GraphicsDevice)
            {
                // Directly copy the input buffer
                using CommandList copyCommandList = new(GraphicsDevice, D3D12_COMMAND_LIST_TYPE_COPY);

                copyCommandList.CopyBufferRegion(D3D12Resource, 0, source.D3D12Resource, 0,(ulong)SizeInBytes);
                copyCommandList.ExecuteAndWaitForCompletion();
            }
            else SetDataWithCpuBuffer(source);
        }
    }
}
