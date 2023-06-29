using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using ComputeSharp.Exceptions;
using ComputeSharp.Graphics.Extensions;
using ComputeSharp.Graphics.Helpers;
using ComputeSharp.Interop;
using ComputeSharp.Interop.Allocation;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using static TerraFX.Interop.DirectX.D3D12_FORMAT_SUPPORT1;
using ResourceType = ComputeSharp.Graphics.Resources.Enums.ResourceType;

#pragma warning disable CA1063

namespace ComputeSharp.Resources;

/// <summary>
/// A <see langword="class"/> representing a typed 1D texture stored on on CPU memory, that can be used to transfer data to/from the GPU.
/// </summary>
/// <typeparam name="T">The type of items stored on the texture.</typeparam>
public abstract unsafe partial class TransferTexture1D<T> : IReferenceTrackedObject, IGraphicsResource, IMemoryOwner<T>
    where T : unmanaged
{
    /// <summary>
    /// The <see cref="ReferenceTracker"/> value for the current instance.
    /// </summary>
    private ReferenceTracker referenceTracker;

    /// <summary>
    /// The <see cref="ID3D12Allocation"/> instance used to retrieve <see cref="d3D12Resource"/>, if available.
    /// </summary>
    private ComPtr<ID3D12Allocation> allocation;

    /// <summary>
    /// The <see cref="ID3D12Resource"/> instance currently mapped.
    /// </summary>
    private ComPtr<ID3D12Resource> d3D12Resource;

    /// <summary>
    /// The pointer to the start of the mapped buffer data.
    /// </summary>
    private readonly T* mappedData;

    /// <summary>
    /// The <see cref="D3D12_PLACED_SUBRESOURCE_FOOTPRINT"/> description for the current resource.
    /// </summary>
    private readonly D3D12_PLACED_SUBRESOURCE_FOOTPRINT d3D12PlacedSubresourceFootprint;

    /// <summary>
    /// Creates a new <see cref="TransferTexture1D{T}"/> instance with the specified parameters.
    /// </summary>
    /// <param name="device">The <see cref="ComputeSharp.GraphicsDevice"/> associated with the current instance.</param>
    /// <param name="width">The width of the texture.</param>
    /// <param name="resourceType">The resource type for the current texture.</param>
    /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
    private protected TransferTexture1D(GraphicsDevice device, int width, ResourceType resourceType, AllocationMode allocationMode)
    {
        using ReferenceTracker.Lease _0 = ReferenceTracker.Create(this, out this.referenceTracker);

        default(ArgumentOutOfRangeException).ThrowIfNotBetweenOrEqual(width, 1, D3D12.D3D12_REQ_TEXTURE1D_U_DIMENSION);

        using ReferenceTracker.Lease _1 = device.GetReferenceTracker().GetLease();

        device.ThrowIfDeviceLost();

        if (!device.D3D12Device->IsDxgiFormatSupported(DXGIFormatHelper.GetForType<T>(), D3D12_FORMAT_SUPPORT1_TEXTURE1D))
        {
            UnsupportedTextureTypeException.ThrowForTexture1D<T>();
        }

        GraphicsDevice = device;

        device.D3D12Device->GetCopyableFootprint(
            DXGIFormatHelper.GetForType<T>(),
            (uint)width,
            out this.d3D12PlacedSubresourceFootprint,
            out _,
            out ulong totalSizeInBytes);

        device.CreateOrAllocateResource(
            resourceType,
            allocationMode,
            totalSizeInBytes,
            out this.allocation,
            out this.d3D12Resource);

        this.mappedData = (T*)this.d3D12Resource.Get()->Map().Pointer;

        this.d3D12Resource.Get()->SetName(this);
    }

    /// <inheritdoc/>
    public GraphicsDevice GraphicsDevice { get; }

    /// <summary>
    /// Gets the width of the current texture.
    /// </summary>
    public int Width => (int)this.d3D12PlacedSubresourceFootprint.Footprint.Width;

    /// <summary>
    /// Gets the <see cref="ID3D12Resource"/> instance currently mapped.
    /// </summary>
    internal ID3D12Resource* D3D12Resource => this.d3D12Resource;

    /// <summary>
    /// Gets the <see cref="D3D12_PLACED_SUBRESOURCE_FOOTPRINT"/> value for the current resource.
    /// </summary>
    internal ref readonly D3D12_PLACED_SUBRESOURCE_FOOTPRINT D3D12PlacedSubresourceFootprint => ref this.d3D12PlacedSubresourceFootprint;

    /// <inheritdoc/>
    public Memory<T> Memory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            using ReferenceTracker.Lease _0 = GetReferenceTracker().GetLease();

            return new MemoryManager(this).Memory;
        }
    }

    /// <inheritdoc/>
    public Span<T> Span
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            using ReferenceTracker.Lease _0 = GetReferenceTracker().GetLease();

            return new(this.mappedData, Width);
        }
    }

    /// <inheritdoc/>
    void IReferenceTrackedObject.DangerousOnDispose()
    {
        this.d3D12Resource.Dispose();
        this.allocation.Dispose();
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
    /// A <see cref="MemoryManager{T}"/> implementation wrapping a <see cref="TransferTexture1D{T}"/> instance.
    /// </summary>
    private sealed class MemoryManager : MemoryManager<T>
    {
        /// <summary>
        /// The <see cref="TransferTexture1D{T}"/> in use.
        /// </summary>
        private readonly TransferTexture1D<T> buffer;

        /// <summary>
        /// Creates a new <see cref="MemoryManager"/> instance for a given buffer.
        /// </summary>
        /// <param name="buffer">The <see cref="TransferTexture1D{T}"/> in use.</param>
        public MemoryManager(TransferTexture1D<T> buffer)
        {
            this.buffer = buffer;
        }

        /// <inheritdoc/>
        public override Memory<T> Memory
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => CreateMemory(this.buffer.Width);
        }

        /// <inheritdoc/>
        public override Span<T> GetSpan()
        {
            return this.buffer.Span;
        }

        /// <inheritdoc/>
        public override MemoryHandle Pin(int elementIndex = 0)
        {
            default(ArgumentOutOfRangeException).ThrowIfNotZero(elementIndex);

            using ReferenceTracker.Lease _0 = this.buffer.GetReferenceTracker().GetLease();

            return new(this.buffer.mappedData);
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