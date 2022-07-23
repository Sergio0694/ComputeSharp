﻿using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using CommunityToolkit.Diagnostics;
using ComputeSharp.Exceptions;
using ComputeSharp.Graphics.Extensions;
using ComputeSharp.Interop;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using ResourceType = ComputeSharp.Graphics.Resources.Enums.ResourceType;

namespace ComputeSharp.Resources;

/// <summary>
/// A <see langword="class"/> representing a typed buffer stored on CPU memory, that can be used to transfer data to/from the GPU.
/// </summary>
/// <typeparam name="T">The type of items stored on the buffer.</typeparam>
public abstract unsafe class TransferBuffer<T> : ReferenceTracker.ITrackedObject, IGraphicsResource, IMemoryOwner<T>
    where T : unmanaged
{
    /// <summary>
    /// The owning <see cref="ReferenceTracker"/> object for the current instance.
    /// </summary>
    private ReferenceTracker referenceTracker;

#if NET6_0_OR_GREATER
    /// <summary>
    /// The <see cref="D3D12MA_Allocation"/> instance used to retrieve <see cref="d3D12Resource"/>.
    /// </summary>
    private ComPtr<D3D12MA_Allocation> allocation;
#endif

    /// <summary>
    /// The <see cref="ID3D12Resource"/> instance currently mapped.
    /// </summary>
    private ComPtr<ID3D12Resource> d3D12Resource;

    /// <summary>
    /// The pointer to the start of the mapped buffer data.
    /// </summary>
    private readonly T* mappedData;

    /// <summary>
    /// Creates a new <see cref="TransferBuffer{T}"/> instance with the specified parameters.
    /// </summary>
    /// <param name="device">The <see cref="GraphicsDevice"/> associated with the current instance.</param>
    /// <param name="length">The number of items to store in the current buffer.</param>
    /// <param name="resourceType">The resource type for the current buffer.</param>
    /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
    private protected TransferBuffer(GraphicsDevice device, int length, ResourceType resourceType, AllocationMode allocationMode)
    {
        this.referenceTracker = new ReferenceTracker(this);

        using (device.GetReferenceTracker().GetLease())
        {
            device.ThrowIfDeviceLost();

            // The maximum length is set such that the aligned buffer size can't exceed uint.MaxValue
            Guard.IsBetweenOrEqualTo(length, 1, (uint.MaxValue / (uint)sizeof(T)) & ~255);

            GraphicsDevice = device;
            Length = length;

            ulong sizeInBytes = (uint)length * (uint)sizeof(T);

#if NET6_0_OR_GREATER
            this.allocation = device.Allocator->CreateResource(device.Pool, resourceType, allocationMode, sizeInBytes);
            this.d3D12Resource = new ComPtr<ID3D12Resource>(this.allocation.Get()->GetResource());
#else
            this.d3D12Resource = device.D3D12Device->CreateCommittedResource(resourceType, sizeInBytes, device.IsCacheCoherentUMA);
#endif

            device.RegisterAllocatedResource();

            this.mappedData = (T*)this.d3D12Resource.Get()->Map().Pointer;

            this.d3D12Resource.Get()->SetName(this);
        }
    }

    /// <summary>
    /// Releases unmanaged resources for the current <see cref="TransferBuffer{T}"/> instance.
    /// </summary>
    ~TransferBuffer()
    {
        this.referenceTracker.Dispose();
    }

    /// <inheritdoc/>
    public GraphicsDevice GraphicsDevice { get; }

    /// <summary>
    /// Gets the length of the current buffer.
    /// </summary>
    public int Length { get; }

    /// <summary>
    /// Gets the <see cref="ID3D12Resource"/> instance currently mapped.
    /// </summary>
    internal ID3D12Resource* D3D12Resource => this.d3D12Resource;

    /// <summary>
    /// Gets the pointer to the start of the mapped buffer data.
    /// </summary>
    internal T* MappedData => this.mappedData;

    /// <inheritdoc/>
    public Memory<T> Memory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            using (this.referenceTracker.GetLease())
            {
                return new MemoryManager(this).Memory;
            }
        }
    }

    /// <inheritdoc/>
    public Span<T> Span
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            using (this.referenceTracker.GetLease())
            {
                return new(this.mappedData, Length);
            }
        }
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        this.referenceTracker.Dispose();

        GC.SuppressFinalize(this);
    }

    /// <inheritdoc cref="ReferenceTracker.ITrackedObject.GetReferenceTracker"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal ref ReferenceTracker GetReferenceTracker()
    {
        return ref this.referenceTracker;
    }

    /// <inheritdoc/>
    ref ReferenceTracker ReferenceTracker.ITrackedObject.GetReferenceTracker()
    {
        return ref this.referenceTracker;
    }

    /// <inheritdoc/>
    void ReferenceTracker.ITrackedObject.DangerousRelease()
    {
        this.d3D12Resource.Dispose();
#if NET6_0_OR_GREATER
        this.allocation.Dispose();
#endif

        if (GraphicsDevice is GraphicsDevice device)
        {
            device.UnregisterAllocatedResource();
        }
    }

    /// <summary>
    /// Throws a <see cref="GraphicsDeviceMismatchException"/> if the target device doesn't match the current one.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void ThrowIfDeviceMismatch(GraphicsDevice device)
    {
        if (GraphicsDevice != device)
        {
            GraphicsDeviceMismatchException.Throw(this, device);
        }
    }

    /// <summary>
    /// A <see cref="MemoryManager{T}"/> implementation wrapping a <see cref="TransferBuffer{T}"/> instance.
    /// </summary>
    private sealed class MemoryManager : MemoryManager<T>
    {
        /// <summary>
        /// The <see cref="TransferBuffer{T}"/> in use.
        /// </summary>
        private readonly TransferBuffer<T> buffer;

        /// <summary>
        /// Creates a new <see cref="MemoryManager"/> instance for a given buffer.
        /// </summary>
        /// <param name="buffer">The <see cref="TransferBuffer{T}"/> in use.</param>
        public MemoryManager(TransferBuffer<T> buffer)
        {
            this.buffer = buffer;
        }

        /// <inheritdoc/>
        public override Memory<T> Memory
        {
            get => CreateMemory(this.buffer.Length);
        }

        /// <inheritdoc/>
        public override Span<T> GetSpan()
        {
            return this.buffer.Span;
        }

        /// <inheritdoc/>
        public override MemoryHandle Pin(int elementIndex = 0)
        {
            using (this.buffer.referenceTracker.GetLease())
            {
                Guard.IsEqualTo(elementIndex, 0);

                return new(this.buffer.mappedData);
            }
        }

        /// <inheritdoc/>
        public override void Unpin()
        {
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
        }
    }
}
