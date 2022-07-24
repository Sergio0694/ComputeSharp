﻿using System.Runtime.CompilerServices;
using CommunityToolkit.Diagnostics;
using ComputeSharp.Exceptions;
using ComputeSharp.Graphics.Extensions;
using ComputeSharp.Graphics.Helpers;
using ComputeSharp.Interop;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using static TerraFX.Interop.DirectX.D3D12_FORMAT_SUPPORT1;
using ResourceType = ComputeSharp.Graphics.Resources.Enums.ResourceType;

namespace ComputeSharp.Resources;

/// <summary>
/// A <see langword="class"/> representing a typed 2D texture stored on on CPU memory, that can be used to transfer data to/from the GPU.
/// </summary>
/// <typeparam name="T">The type of items stored on the texture.</typeparam>
public unsafe abstract class TransferTexture2D<T> : NativeObject, IGraphicsResource
    where T : unmanaged
{
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
    /// The <see cref="D3D12_PLACED_SUBRESOURCE_FOOTPRINT"/> description for the current resource.
    /// </summary>
    private readonly D3D12_PLACED_SUBRESOURCE_FOOTPRINT d3D12PlacedSubresourceFootprint;

    /// <summary>
    /// Creates a new <see cref="TransferTexture2D{T}"/> instance with the specified parameters.
    /// </summary>
    /// <param name="device">The <see cref="ComputeSharp.GraphicsDevice"/> associated with the current instance.</param>
    /// <param name="height">The height of the texture.</param>
    /// <param name="width">The width of the texture.</param>
    /// <param name="resourceType">The resource type for the current texture.</param>
    /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
    private protected TransferTexture2D(GraphicsDevice device, int width, int height, ResourceType resourceType, AllocationMode allocationMode)
    {
        Guard.IsBetweenOrEqualTo(width, 1, D3D12.D3D12_REQ_TEXTURE2D_U_OR_V_DIMENSION);
        Guard.IsBetweenOrEqualTo(height, 1, D3D12.D3D12_REQ_TEXTURE2D_U_OR_V_DIMENSION);

        using (device.GetReferenceTracker().GetLease())
        {
            device.ThrowIfDeviceLost();

            if (!device.D3D12Device->IsDxgiFormatSupported(DXGIFormatHelper.GetForType<T>(), D3D12_FORMAT_SUPPORT1_TEXTURE2D))
            {
                UnsupportedTextureTypeException.ThrowForTexture2D<T>();
            }

            GraphicsDevice = device;

            device.D3D12Device->GetCopyableFootprint(
                DXGIFormatHelper.GetForType<T>(),
                (uint)width,
                (uint)height,
                out this.d3D12PlacedSubresourceFootprint,
                out _,
                out ulong totalSizeInBytes);

#if NET6_0_OR_GREATER
            this.allocation = device.Allocator->CreateResource(device.Pool, resourceType, allocationMode, totalSizeInBytes);
            this.d3D12Resource = new ComPtr<ID3D12Resource>(this.allocation.Get()->GetResource());
#else
            this.d3D12Resource = device.D3D12Device->CreateCommittedResource(resourceType, totalSizeInBytes, device.IsCacheCoherentUMA);
#endif

            device.RegisterAllocatedResource();

            this.mappedData = (T*)this.d3D12Resource.Get()->Map().Pointer;

            this.d3D12Resource.Get()->SetName(this);
        }
    }

    /// <inheritdoc/>
    public GraphicsDevice GraphicsDevice { get; }

    /// <summary>
    /// Gets the width of the current texture.
    /// </summary>
    public int Width => (int)this.d3D12PlacedSubresourceFootprint.Footprint.Width;

    /// <summary>
    /// Gets the height of the current texture.
    /// </summary>
    public int Height => (int)this.d3D12PlacedSubresourceFootprint.Footprint.Height;

    /// <summary>
    /// Gets the <see cref="ID3D12Resource"/> instance currently mapped.
    /// </summary>
    internal ID3D12Resource* D3D12Resource => this.d3D12Resource;

    /// <summary>
    /// Gets the <see cref="D3D12_PLACED_SUBRESOURCE_FOOTPRINT"/> value for the current resource.
    /// </summary>
    internal ref readonly D3D12_PLACED_SUBRESOURCE_FOOTPRINT D3D12PlacedSubresourceFootprint => ref this.d3D12PlacedSubresourceFootprint;

    /// <inheritdoc/>
    public TextureView2D<T> View
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            using (this.GetReferenceTracker().GetLease())
            {
                return new(this.mappedData, Width, Height, (int)this.d3D12PlacedSubresourceFootprint.Footprint.RowPitch);
            }
        }
    }

    /// <inheritdoc/>
    private protected sealed override void OnDispose()
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
}
