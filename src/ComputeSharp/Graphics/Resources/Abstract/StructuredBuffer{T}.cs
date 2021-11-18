using ComputeSharp.Graphics.Commands;
using ComputeSharp.Graphics.Extensions;
using ComputeSharp.Graphics.Helpers;
using ComputeSharp.Graphics.Resources.Interop;
using Microsoft.Toolkit.Diagnostics;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using static TerraFX.Interop.DirectX.D3D12_COMMAND_LIST_TYPE;
using ResourceType = ComputeSharp.Graphics.Resources.Enums.ResourceType;

namespace ComputeSharp.Resources;

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
    internal override unsafe void CopyTo(ref T destination, int sourceOffset, int count)
    {
        GraphicsDevice.ThrowIfDisposed();

        ThrowIfDisposed();

        Guard.IsBetweenOrEqualTo(count, 0, Length, nameof(count));
        Guard.IsInRange(sourceOffset, 0, Length, nameof(sourceOffset));
        Guard.IsLessThanOrEqualTo(sourceOffset + count, Length, nameof(sourceOffset));

        if (GraphicsDevice.IsCacheCoherentUMA)
        {
            using ID3D12ResourceMap resource = D3D12Resource->Map();

            fixed (void* destinationPointer = &destination)
            {
                MemoryHelper.Copy<T>(
                    source: resource.Pointer,
                    destination: destinationPointer,
                    sourceElementOffset: (uint)sourceOffset,
                    destinationElementOffset: 0,
                    sourceElementPitchInBytes: (uint)sizeof(T),
                    destinationElementPitchInBytes: (uint)sizeof(T),
                    count: (uint)count);
            }
        }
        else
        {
            nint byteOffset = (nint)sourceOffset * sizeof(T);
            nint byteLength = count * sizeof(T);

#if NET6_0_OR_GREATER
            using ComPtr<D3D12MA_Allocation> allocation = GraphicsDevice.Allocator->CreateResource(null, ResourceType.ReadBack, AllocationMode.Default, (ulong)byteLength);
#else
            using ComPtr<ID3D12Resource> d3D12Resource = GraphicsDevice.D3D12Device->CreateCommittedResource(
                ResourceType.ReadBack,
                AllocationMode.Default,
                (ulong)byteLength,
                GraphicsDevice.IsCacheCoherentUMA);
#endif

            using (CommandList copyCommandList = new(GraphicsDevice, D3D12_COMMAND_LIST_TYPE_COPY))
            {
#if NET6_0_OR_GREATER
                copyCommandList.D3D12GraphicsCommandList->CopyBufferRegion(allocation.Get()->GetResource(), 0, D3D12Resource, (ulong)byteOffset, (ulong)byteLength);
#else
                copyCommandList.D3D12GraphicsCommandList->CopyBufferRegion(d3D12Resource.Get(), 0, D3D12Resource, (ulong)byteOffset, (ulong)byteLength);
#endif
                copyCommandList.ExecuteAndWaitForCompletion();
            }

#if NET6_0_OR_GREATER
            using ID3D12ResourceMap resource = allocation.Get()->GetResource()->Map();
#else
            using ID3D12ResourceMap resource = d3D12Resource.Get()->Map();
#endif

            fixed (void* destinationPointer = &destination)
            {
                MemoryHelper.Copy<T>(
                    source: resource.Pointer,
                    destination: destinationPointer,
                    sourceElementOffset: 0,
                    destinationElementOffset: 0,
                    sourceElementPitchInBytes: (uint)sizeof(T),
                    destinationElementPitchInBytes: (uint)sizeof(T),
                    count: (uint)count);
            }
        }
    }

    /// <inheritdoc/>
    internal override unsafe void CopyTo(Buffer<T> destination, int sourceOffset, int destinationOffset, int count)
    {
        GraphicsDevice.ThrowIfDisposed();

        ThrowIfDisposed();

        destination.ThrowIfDeviceMismatch(GraphicsDevice);
        destination.ThrowIfDisposed();

        Guard.IsBetweenOrEqualTo(count, 0, Length, nameof(count));
        Guard.IsBetweenOrEqualTo(count, 0, destination.Length, nameof(count));
        Guard.IsInRange(sourceOffset, 0, Length, nameof(sourceOffset));
        Guard.IsLessThanOrEqualTo(sourceOffset + count, Length, nameof(sourceOffset));
        Guard.IsInRange(destinationOffset, 0, destination.Length, nameof(destinationOffset));
        Guard.IsLessThanOrEqualTo(destinationOffset + count, destination.Length, nameof(destinationOffset));

        if (!destination.IsPaddingPresent)
        {
            nint sourceByteOffset = (nint)sourceOffset * sizeof(T);
            nint destinationByteOffset = (nint)destinationOffset * sizeof(T);
            nint byteLength = count * sizeof(T);

            // Directly copy the input buffer
            using CommandList copyCommandList = new(GraphicsDevice, D3D12_COMMAND_LIST_TYPE_COPY);

            copyCommandList.D3D12GraphicsCommandList->CopyBufferRegion(
                destination.D3D12Resource,
                (ulong)destinationByteOffset,
                D3D12Resource,
                (ulong)sourceByteOffset,
                (ulong)byteLength);

            copyCommandList.ExecuteAndWaitForCompletion();
        }
        else CopyToWithCpuBuffer(destination, sourceOffset, destinationOffset, count);
    }

    /// <summary>
    /// Reads the contents of the specified range from the current <see cref="StructuredBuffer{T}"/> instance and writes them into a target <see cref="ReadBackBuffer{T}"/> instance.
    /// </summary>
    /// <param name="destination">The target <see cref="ReadBackBuffer{T}"/> instance to write data to.</param>
    /// <param name="sourceOffset">The offset to start reading data from.</param>
    /// <param name="destinationOffset">The starting offset within <paramref name="destination"/> to write data to.</param>
    /// <param name="count">The number of items to read.</param>
    internal unsafe void CopyTo(ReadBackBuffer<T> destination, int sourceOffset, int destinationOffset, int count)
    {
        GraphicsDevice.ThrowIfDisposed();

        ThrowIfDisposed();

        destination.ThrowIfDeviceMismatch(GraphicsDevice);
        destination.ThrowIfDisposed();

        Guard.IsBetweenOrEqualTo(count, 0, Length, nameof(count));
        Guard.IsBetweenOrEqualTo(count, 0, destination.Length, nameof(count));
        Guard.IsInRange(sourceOffset, 0, Length, nameof(sourceOffset));
        Guard.IsLessThanOrEqualTo(sourceOffset + count, Length, nameof(sourceOffset));
        Guard.IsInRange(destinationOffset, 0, destination.Length, nameof(destinationOffset));
        Guard.IsLessThanOrEqualTo(destinationOffset + count, destination.Length, nameof(destinationOffset));

        if (GraphicsDevice.IsCacheCoherentUMA)
        {
            using ID3D12ResourceMap resource = D3D12Resource->Map();

            MemoryHelper.Copy<T>(
                source: resource.Pointer,
                destination: destination.MappedData,
                sourceElementOffset: (uint)sourceOffset,
                destinationElementOffset: (uint)destinationOffset,
                sourceElementPitchInBytes: (uint)sizeof(T),
                destinationElementPitchInBytes: (uint)sizeof(T),
                count: (uint)count);
        }
        else
        {
            ulong byteDestinationOffset = (uint)destinationOffset * (uint)sizeof(T);
            ulong byteOffset = (uint)sourceOffset * (uint)sizeof(T);
            ulong byteLength = (uint)count * (uint)sizeof(T);

            using CommandList copyCommandList = new(GraphicsDevice, D3D12_COMMAND_LIST_TYPE_COPY);

            copyCommandList.D3D12GraphicsCommandList->CopyBufferRegion(destination.D3D12Resource, byteDestinationOffset, D3D12Resource, byteOffset, byteLength);
            copyCommandList.ExecuteAndWaitForCompletion();
        }
    }

    /// <inheritdoc/>
    internal override unsafe void CopyFrom(ref T source, int offset, int length)
    {
        GraphicsDevice.ThrowIfDisposed();

        ThrowIfDisposed();

        Guard.IsBetweenOrEqualTo(length, 0, Length, nameof(length));
        Guard.IsInRange(offset, 0, Length, nameof(offset));
        Guard.IsLessThanOrEqualTo(offset + length, Length, nameof(offset));

        if (GraphicsDevice.IsCacheCoherentUMA)
        {
            using ID3D12ResourceMap resource = D3D12Resource->Map();

            fixed (void* sourcePointer = &source)
            {
                MemoryHelper.Copy<T>(
                    source: sourcePointer,
                    destination: resource.Pointer,
                    sourceElementOffset: 0,
                    destinationElementOffset: (uint)offset,
                    sourceElementPitchInBytes: (uint)sizeof(T),
                    destinationElementPitchInBytes: (uint)sizeof(T),
                    count: (uint)length);
            }
        }
        else
        {
            nint byteOffset = (nint)offset * sizeof(T);
            nint byteLength = length * sizeof(T);

#if NET6_0_OR_GREATER
            using ComPtr<D3D12MA_Allocation> allocation = GraphicsDevice.Allocator->CreateResource(null, ResourceType.Upload, AllocationMode.Default, (ulong)byteLength);
#else
            using ComPtr<ID3D12Resource> d3D12Resource = GraphicsDevice.D3D12Device->CreateCommittedResource(
                ResourceType.Upload,
                AllocationMode.Default,
                (ulong)byteLength,
                GraphicsDevice.IsCacheCoherentUMA);
#endif

#if NET6_0_OR_GREATER
            using (ID3D12ResourceMap resource = allocation.Get()->GetResource()->Map())
#else
            using (ID3D12ResourceMap resource = d3D12Resource.Get()->Map())
#endif
            fixed (void* sourcePointer = &source)
            {
                MemoryHelper.Copy<T>(
                    source: sourcePointer,
                    destination: resource.Pointer,
                    sourceElementOffset: 0,
                    destinationElementOffset: 0,
                    sourceElementPitchInBytes: (uint)sizeof(T),
                    destinationElementPitchInBytes: (uint)sizeof(T),
                    count: (uint)length);
            }

            using CommandList copyCommandList = new(GraphicsDevice, D3D12_COMMAND_LIST_TYPE_COPY);

#if NET6_0_OR_GREATER
            copyCommandList.D3D12GraphicsCommandList->CopyBufferRegion(D3D12Resource, (ulong)byteOffset, allocation.Get()->GetResource(), 0, (ulong)byteLength);
#else
            copyCommandList.D3D12GraphicsCommandList->CopyBufferRegion(D3D12Resource, (ulong)byteOffset, d3D12Resource.Get(), 0, (ulong)byteLength);
#endif
            copyCommandList.ExecuteAndWaitForCompletion();
        }
    }

    /// <summary>
    /// Reads the contents of the specified range from an input <see cref="ReadBackBuffer{T}"/> instance and writes them to the current the current <see cref="StructuredBuffer{T}"/> instance.
    /// </summary>
    /// <param name="source">The input <see cref="UploadBuffer{T}"/> instance to read data from.</param>
    /// <param name="sourceOffset">The starting offset within <paramref name="source"/> to read data from.</param>
    /// <param name="destinationOffset">The offset to start reading writing data to.</param>
    /// <param name="count">The number of items to read.</param>
    internal unsafe void CopyFrom(UploadBuffer<T> source, int sourceOffset, int destinationOffset, int count)
    {
        GraphicsDevice.ThrowIfDisposed();

        ThrowIfDisposed();

        source.ThrowIfDeviceMismatch(GraphicsDevice);
        source.ThrowIfDisposed();

        Guard.IsBetweenOrEqualTo(count, 0, Length, nameof(count));
        Guard.IsBetweenOrEqualTo(count, 0, source.Length, nameof(count));
        Guard.IsInRange(sourceOffset, 0, source.Length, nameof(sourceOffset));
        Guard.IsLessThanOrEqualTo(sourceOffset + count, source.Length, nameof(sourceOffset));
        Guard.IsInRange(destinationOffset, 0, Length, nameof(destinationOffset));
        Guard.IsLessThanOrEqualTo(destinationOffset + count, Length, nameof(destinationOffset));

        if (GraphicsDevice.IsCacheCoherentUMA)
        {
            using ID3D12ResourceMap resource = D3D12Resource->Map();

            MemoryHelper.Copy<T>(
                source: source.MappedData,
                destination: resource.Pointer,
                sourceElementOffset: (uint)sourceOffset,
                destinationElementOffset: (uint)destinationOffset,
                sourceElementPitchInBytes: (uint)sizeof(T),
                destinationElementPitchInBytes: (uint)sizeof(T),
                count: (uint)count);
        }
        else
        {
            ulong byteSourceOffset = (uint)sourceOffset * (uint)sizeof(T);
            ulong byteOffset = (uint)destinationOffset * (uint)sizeof(T);
            ulong byteLength = (uint)count * (uint)sizeof(T);

            using CommandList copyCommandList = new(GraphicsDevice, D3D12_COMMAND_LIST_TYPE_COPY);

            copyCommandList.D3D12GraphicsCommandList->CopyBufferRegion(D3D12Resource, byteOffset, source.D3D12Resource, byteSourceOffset, byteLength);
            copyCommandList.ExecuteAndWaitForCompletion();
        }
    }
}
