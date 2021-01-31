using ComputeSharp.Graphics.Commands;
using ComputeSharp.Graphics.Extensions;
using ComputeSharp.Graphics.Helpers;
using ComputeSharp.Graphics.Resources.Enums;
using ComputeSharp.Graphics.Resources.Interop;
using Microsoft.Toolkit.Diagnostics;
using TerraFX.Interop;
using static TerraFX.Interop.D3D12_COMMAND_LIST_TYPE;

namespace ComputeSharp.Resources
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
        /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
        private protected unsafe StructuredBuffer(GraphicsDevice device, int length, ResourceType resourceType, AllocationMode allocationMode)
            : base(device, length, (uint)sizeof(T), resourceType, allocationMode)
        {
        }

        /// <inheritdoc/>
        internal override unsafe void GetData(ref T destination, int length, int offset)
        {
            GraphicsDevice.ThrowIfDisposed();

            ThrowIfDisposed();

            Guard.IsInRange(offset, 0, Length, nameof(offset));
            Guard.IsLessThanOrEqualTo(offset + length, Length, nameof(length));

            if (GraphicsDevice.IsCacheCoherentUMA)
            {
                using ID3D12ResourceMap resource = D3D12Resource->Map();

                fixed (void* destinationPointer = &destination)
                {
                    MemoryHelper.Copy(
                        resource.Pointer,
                        (uint)offset,
                        (uint)length,
                        (uint)sizeof(T),
                        destinationPointer);
                }
            }
            else
            {
                nint
                    byteOffset = (nint)offset * sizeof(T),
                    byteLength = length * sizeof(T);

                using ComPtr<ID3D12Resource> d3D12Resource = GraphicsDevice.D3D12Device->CreateCommittedResource(ResourceType.ReadBack, AllocationMode.Default, (ulong)byteLength, false);

                using (CommandList copyCommandList = new(GraphicsDevice, D3D12_COMMAND_LIST_TYPE_COPY))
                {
                    copyCommandList.CopyBufferRegion(d3D12Resource.Get(), 0, D3D12Resource, (ulong)byteOffset, (ulong)byteLength);
                    copyCommandList.ExecuteAndWaitForCompletion();
                }

                using ID3D12ResourceMap resource = d3D12Resource.Get()->Map();

                fixed (void* destinationPointer = &destination)
                {
                    MemoryHelper.Copy(
                        resource.Pointer,
                        0u,
                        (uint)length,
                        (uint)sizeof(T),
                        destinationPointer);
                }
            }
        }

        /// <inheritdoc/>
        public unsafe void GetData(ReadBackBuffer<T> destination, int destinationOffset, int length, int offset)
        {
            GraphicsDevice.ThrowIfDisposed();

            ThrowIfDisposed();

            destination.ThrowIfDeviceMismatch(GraphicsDevice);
            destination.ThrowIfDisposed();

            Guard.IsInRange(offset, 0, Length, nameof(offset));
            Guard.IsLessThanOrEqualTo(offset + length, Length, nameof(length));
            Guard.IsInRange(destinationOffset, 0, destination.Length, nameof(destinationOffset));
            Guard.IsLessThanOrEqualTo(destinationOffset + length, destination.Length, nameof(length));

            ulong
                byteDestinationOffset = (uint)destinationOffset * (uint)sizeof(T),
                byteOffset = (uint)offset * (uint)sizeof(T),
                byteLength = (uint)length * (uint)sizeof(T);

            using CommandList copyCommandList = new(GraphicsDevice, D3D12_COMMAND_LIST_TYPE_COPY);

            copyCommandList.CopyBufferRegion(destination.D3D12Resource, byteDestinationOffset, D3D12Resource, byteOffset, byteLength);
            copyCommandList.ExecuteAndWaitForCompletion();
        }

        /// <inheritdoc/>
        internal override unsafe void SetData(ref T source, int length, int offset)
        {
            GraphicsDevice.ThrowIfDisposed();

            ThrowIfDisposed();

            Guard.IsInRange(offset, 0, Length, nameof(offset));
            Guard.IsLessThanOrEqualTo(offset + length, Length, nameof(length));

            if (GraphicsDevice.IsCacheCoherentUMA)
            {
                using ID3D12ResourceMap resource = D3D12Resource->Map();

                fixed (void* sourcePointer = &source)
                {
                    MemoryHelper.Copy(
                        sourcePointer,
                        (uint)offset,
                        (uint)length,
                        (uint)sizeof(T),
                        resource.Pointer);
                }
            }
            else
            {
                nint
                    byteOffset = (nint)offset * sizeof(T),
                    byteLength = length * sizeof(T);

                using ComPtr<ID3D12Resource> d3D12Resource = GraphicsDevice.D3D12Device->CreateCommittedResource(ResourceType.Upload, AllocationMode.Default, (ulong)byteLength, false);

                using (ID3D12ResourceMap resource = d3D12Resource.Get()->Map())
                fixed (void* sourcePointer = &source)
                {
                    MemoryHelper.Copy(
                        sourcePointer,
                        0u,
                        (uint)length,
                        (uint)sizeof(T),
                        resource.Pointer);
                }

                using CommandList copyCommandList = new(GraphicsDevice, D3D12_COMMAND_LIST_TYPE_COPY);

                copyCommandList.CopyBufferRegion(D3D12Resource, (ulong)byteOffset, d3D12Resource.Get(), 0, (ulong)byteLength);
                copyCommandList.ExecuteAndWaitForCompletion();
            }
        }

        /// <inheritdoc/>
        public unsafe void SetData(UploadBuffer<T> source, int sourceOffset, int length, int offset)
        {
            GraphicsDevice.ThrowIfDisposed();

            ThrowIfDisposed();

            source.ThrowIfDeviceMismatch(GraphicsDevice);
            source.ThrowIfDisposed();

            Guard.IsInRange(offset, 0, Length, nameof(offset));
            Guard.IsLessThanOrEqualTo(offset + length, Length, nameof(length));
            Guard.IsInRange(sourceOffset, 0, source.Length, nameof(sourceOffset));
            Guard.IsLessThanOrEqualTo(sourceOffset + length, source.Length, nameof(length));

            ulong
                byteSourceOffset = (uint)sourceOffset * (uint)sizeof(T),
                byteOffset = (uint)offset * (uint)sizeof(T),
                byteLength = (uint)length * (uint)sizeof(T);

            using CommandList copyCommandList = new(GraphicsDevice, D3D12_COMMAND_LIST_TYPE_COPY);

            copyCommandList.CopyBufferRegion(D3D12Resource, byteOffset, source.D3D12Resource, byteSourceOffset, byteLength);
            copyCommandList.ExecuteAndWaitForCompletion();
        }

        /// <inheritdoc/>
        public override unsafe void SetData(Buffer<T> source)
        {
            GraphicsDevice.ThrowIfDisposed();

            ThrowIfDeviceMismatch(source.GraphicsDevice);
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
