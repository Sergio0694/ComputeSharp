using ComputeSharp.Graphics;
using ComputeSharp.Graphics.Commands;
using ComputeSharp.Graphics.Extensions;
using ComputeSharp.Graphics.Helpers;
using ComputeSharp.Graphics.Resources.Interop;
using Microsoft.Toolkit.Diagnostics;
using TerraFX.Interop;
using static TerraFX.Interop.D3D12_COMMAND_LIST_TYPE;
using ResourceType = ComputeSharp.Graphics.Resources.Enums.ResourceType;

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
        internal override unsafe void CopyTo(ref T destination, int length, int offset)
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

                using UniquePtr<Allocation> allocation = GraphicsDevice.Allocator->CreateResource(ResourceType.ReadBack, AllocationMode.Default, (ulong)byteLength);

                using (CommandList copyCommandList = new(GraphicsDevice, D3D12_COMMAND_LIST_TYPE_COPY))
                {
                    copyCommandList.D3D12GraphicsCommandList->CopyBufferRegion(allocation.Get()->GetResource(), 0, D3D12Resource, (ulong)byteOffset, (ulong)byteLength);
                    copyCommandList.ExecuteAndWaitForCompletion();
                }

                using ID3D12ResourceMap resource = allocation.Get()->GetResource()->Map();

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

        /// <summary>
        /// Reads the contents of the specified range from the current <see cref="StructuredBuffer{T}"/> instance and writes them into a target <see cref="ReadBackBuffer{T}"/> instance.
        /// </summary>
        /// <param name="destination">The target <see cref="ReadBackBuffer{T}"/> instance to write data to.</param>
        /// <param name="destinationOffset">The starting offset within <paramref name="destination"/> to write data to.</param>
        /// <param name="length">The number of items to read.</param>
        /// <param name="offset">The offset to start reading data from.</param>
        internal unsafe void CopyTo(ReadBackBuffer<T> destination, int destinationOffset, int length, int offset)
        {
            GraphicsDevice.ThrowIfDisposed();

            ThrowIfDisposed();

            destination.ThrowIfDeviceMismatch(GraphicsDevice);
            destination.ThrowIfDisposed();

            Guard.IsInRange(offset, 0, Length, nameof(offset));
            Guard.IsLessThanOrEqualTo(offset + length, Length, nameof(length));
            Guard.IsInRange(destinationOffset, 0, destination.Length, nameof(destinationOffset));
            Guard.IsLessThanOrEqualTo(destinationOffset + length, destination.Length, nameof(length));

            if (GraphicsDevice.IsCacheCoherentUMA)
            {
                using ID3D12ResourceMap resource = D3D12Resource->Map();

                MemoryHelper.Copy(
                    resource.Pointer,
                    (uint)offset,
                    (uint)length,
                    (uint)sizeof(T),
                    destination.MappedData + destinationOffset);
            }
            else
            {
                ulong
                    byteDestinationOffset = (uint)destinationOffset * (uint)sizeof(T),
                    byteOffset = (uint)offset * (uint)sizeof(T),
                    byteLength = (uint)length * (uint)sizeof(T);

                using CommandList copyCommandList = new(GraphicsDevice, D3D12_COMMAND_LIST_TYPE_COPY);

                copyCommandList.D3D12GraphicsCommandList->CopyBufferRegion(destination.D3D12Resource, byteDestinationOffset, D3D12Resource, byteOffset, byteLength);
                copyCommandList.ExecuteAndWaitForCompletion();
            }
        }

        /// <inheritdoc/>
        internal override unsafe void CopyFrom(ref T source, int length, int offset)
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

                using UniquePtr<Allocation> allocation = GraphicsDevice.Allocator->CreateResource(ResourceType.Upload, AllocationMode.Default, (ulong)byteLength);

                using (ID3D12ResourceMap resource = allocation.Get()->GetResource()->Map())
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

                copyCommandList.D3D12GraphicsCommandList->CopyBufferRegion(D3D12Resource, (ulong)byteOffset, allocation.Get()->GetResource(), 0, (ulong)byteLength);
                copyCommandList.ExecuteAndWaitForCompletion();
            }
        }

        /// <summary>
        /// Reads the contents of the specified range from an input <see cref="ReadBackBuffer{T}"/> instance and writes them to the current the current <see cref="StructuredBuffer{T}"/> instance.
        /// </summary>
        /// <param name="source">The input <see cref="UploadBuffer{T}"/> instance to read data from.</param>
        /// <param name="sourceOffset">The starting offset within <paramref name="source"/> to read data from.</param>
        /// <param name="length">The number of items to read.</param>
        /// <param name="offset">The offset to start reading writing data to.</param>
        internal unsafe void CopyFrom(UploadBuffer<T> source, int sourceOffset, int length, int offset)
        {
            GraphicsDevice.ThrowIfDisposed();

            ThrowIfDisposed();

            source.ThrowIfDeviceMismatch(GraphicsDevice);
            source.ThrowIfDisposed();

            Guard.IsInRange(offset, 0, Length, nameof(offset));
            Guard.IsLessThanOrEqualTo(offset + length, Length, nameof(length));
            Guard.IsInRange(sourceOffset, 0, source.Length, nameof(sourceOffset));
            Guard.IsLessThanOrEqualTo(sourceOffset + length, source.Length, nameof(length));

            if (GraphicsDevice.IsCacheCoherentUMA)
            {
                using ID3D12ResourceMap resource = D3D12Resource->Map();

                MemoryHelper.Copy(
                    source.MappedData,
                    (uint)sourceOffset,
                    (uint)length,
                    (uint)sizeof(T),
                    (T*)resource.Pointer + offset);
            }
            else
            {
                ulong
                    byteSourceOffset = (uint)sourceOffset * (uint)sizeof(T),
                    byteOffset = (uint)offset * (uint)sizeof(T),
                    byteLength = (uint)length * (uint)sizeof(T);

                using CommandList copyCommandList = new(GraphicsDevice, D3D12_COMMAND_LIST_TYPE_COPY);

                copyCommandList.D3D12GraphicsCommandList->CopyBufferRegion(D3D12Resource, byteOffset, source.D3D12Resource, byteSourceOffset, byteLength);
                copyCommandList.ExecuteAndWaitForCompletion();
            }
        }

        /// <inheritdoc/>
        public override unsafe void CopyFrom(Buffer<T> source)
        {
            GraphicsDevice.ThrowIfDisposed();

            ThrowIfDisposed();

            source.ThrowIfDeviceMismatch(GraphicsDevice);
            source.ThrowIfDisposed();

            Guard.IsLessThanOrEqualTo(source.Length, Length, nameof(Length));

            if (!source.IsPaddingPresent)
            {
                // Directly copy the input buffer
                using CommandList copyCommandList = new(GraphicsDevice, D3D12_COMMAND_LIST_TYPE_COPY);

                copyCommandList.D3D12GraphicsCommandList->CopyBufferRegion(D3D12Resource, 0, source.D3D12Resource, 0,(ulong)SizeInBytes);
                copyCommandList.ExecuteAndWaitForCompletion();
            }
            else CopyFromWithCpuBuffer(source);
        }
    }
}
