using System;
using System.Runtime.CompilerServices;
using ComputeSharp.__Internals;
using ComputeSharp.Exceptions;
using ComputeSharp.Graphics.Commands;
using ComputeSharp.Graphics.Commands.Interop;
using ComputeSharp.Graphics.Extensions;
using ComputeSharp.Graphics.Helpers;
using ComputeSharp.Graphics.Resources.Helpers;
using ComputeSharp.Graphics.Resources.Interop;
using ComputeSharp.Interop;
using ComputeSharp.Interop.Allocation;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using static TerraFX.Interop.DirectX.D3D12_COMMAND_LIST_TYPE;
using static TerraFX.Interop.DirectX.D3D12_RESOURCE_STATES;
using static TerraFX.Interop.DirectX.D3D12_SRV_DIMENSION;
using static TerraFX.Interop.DirectX.D3D12_UAV_DIMENSION;
using ResourceType = ComputeSharp.Graphics.Resources.Enums.ResourceType;

#pragma warning disable CS0618, CA1063

namespace ComputeSharp.Resources;

/// <summary>
/// A <see langword="class"/> representing a typed 1D texture stored on GPU memory.
/// </summary>
/// <typeparam name="T">The type of items stored on the texture.</typeparam>
public abstract unsafe partial class Texture1D<T> : IReferenceTrackedObject, IGraphicsResource, GraphicsResourceHelper.IGraphicsResource
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
    /// The <see cref="ID3D12ResourceDescriptorHandles"/> instance for the current resource.
    /// </summary>
    private readonly ID3D12ResourceDescriptorHandles d3D12ResourceDescriptorHandles;

    /// <summary>
    /// The <see cref="D3D12_COMMAND_LIST_TYPE"/> value to use for copy operations.
    /// </summary>
    private readonly D3D12_COMMAND_LIST_TYPE d3D12CommandListType;

    /// <summary>
    /// The <see cref="D3D12_PLACED_SUBRESOURCE_FOOTPRINT"/> description for the current resource.
    /// </summary>
    private readonly D3D12_PLACED_SUBRESOURCE_FOOTPRINT d3D12PlacedSubresourceFootprint;

    /// <summary>
    /// The current <see cref="D3D12_RESOURCE_STATES"/> value for the current resource.
    /// </summary>
    private D3D12_RESOURCE_STATES d3D12ResourceState;

    /// <summary>
    /// Creates a new <see cref="Texture1D{T}"/> instance with the specified parameters.
    /// </summary>
    /// <param name="device">The <see cref="ComputeSharp.GraphicsDevice"/> associated with the current instance.</param>
    /// <param name="width">The width of the texture.</param>
    /// <param name="resourceType">The resource type for the current texture.</param>
    /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
    /// <param name="d3D12FormatSupport">The format support for the current texture type.</param>
    private protected Texture1D(GraphicsDevice device, int width, ResourceType resourceType, AllocationMode allocationMode, D3D12_FORMAT_SUPPORT1 d3D12FormatSupport)
    {
        using ReferenceTracker.Lease _0 = ReferenceTracker.Create(this, out this.referenceTracker);

        default(ArgumentOutOfRangeException).ThrowIfNotBetweenOrEqual(width, 1, D3D12.D3D12_REQ_TEXTURE1D_U_DIMENSION);

        using ReferenceTracker.Lease _1 = device.GetReferenceTracker().GetLease();

        device.ThrowIfDeviceLost();

        if (!device.D3D12Device->IsDxgiFormatSupported(DXGIFormatHelper.GetForType<T>(), d3D12FormatSupport))
        {
            UnsupportedTextureTypeException.ThrowForTexture1D<T>();
        }

        GraphicsDevice = device;

        device.CreateOrAllocateResource(
            resourceType,
            allocationMode,
            DXGIFormatHelper.GetForType<T>(),
            (uint)width,
            out this.allocation,
            out this.d3D12Resource,
            out this.d3D12ResourceState);

        this.d3D12CommandListType = this.d3D12ResourceState == D3D12_RESOURCE_STATE_COMMON
            ? D3D12_COMMAND_LIST_TYPE_COPY
            : D3D12_COMMAND_LIST_TYPE_COMPUTE;

        device.D3D12Device->GetCopyableFootprint(
            DXGIFormatHelper.GetForType<T>(),
            (uint)width,
            out this.d3D12PlacedSubresourceFootprint,
            out _,
            out _);

        device.RentShaderResourceViewDescriptorHandles(out this.d3D12ResourceDescriptorHandles);

        switch (resourceType)
        {
            case ResourceType.ReadOnly:
                device.D3D12Device->CreateShaderResourceView(this.d3D12Resource.Get(), DXGIFormatHelper.GetForType<T>(), D3D12_SRV_DIMENSION_TEXTURE1D, this.d3D12ResourceDescriptorHandles.D3D12CpuDescriptorHandle);
                break;
            case ResourceType.ReadWrite:
                device.D3D12Device->CreateUnorderedAccessView(this.d3D12Resource.Get(), DXGIFormatHelper.GetForType<T>(), D3D12_UAV_DIMENSION_TEXTURE1D, this.d3D12ResourceDescriptorHandles.D3D12CpuDescriptorHandle);
                device.D3D12Device->CreateUnorderedAccessView(this.d3D12Resource.Get(), DXGIFormatHelper.GetForType<T>(), D3D12_UAV_DIMENSION_TEXTURE1D, this.d3D12ResourceDescriptorHandles.D3D12CpuDescriptorHandleNonShaderVisible);
                break;
        }

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
    /// Gets the <see cref="D3D12_GPU_DESCRIPTOR_HANDLE"/> instance for the current resource.
    /// </summary>
    internal D3D12_GPU_DESCRIPTOR_HANDLE D3D12GpuDescriptorHandle => this.d3D12ResourceDescriptorHandles.D3D12GpuDescriptorHandle;

    /// <summary>
    /// Reads the contents of the specified range from the current <see cref="Texture1D{T}"/> instance and writes them into a target memory area.
    /// </summary>
    /// <param name="destination">The target memory area to write data to.</param>
    /// <param name="size">The size of the target memory area to write data to.</param>
    /// <param name="sourceOffsetX">The horizontal offset in the source texture.</param>
    /// <param name="width">The width of the memory area to copy.</param>
    internal void CopyTo(ref T destination, int size, int sourceOffsetX, int width)
    {
        default(ArgumentOutOfRangeException).ThrowIfNotInRange(sourceOffsetX, 0, Width);
        default(ArgumentOutOfRangeException).ThrowIfNotBetweenOrEqual(width, 1, Width);
        default(ArgumentOutOfRangeException).ThrowIfGreaterThan(sourceOffsetX + width, Width, nameof(sourceOffsetX));
        default(ArgumentOutOfRangeException).ThrowIfLessThan(size, width);

        using ReferenceTracker.Lease _0 = GraphicsDevice.GetReferenceTracker().GetLease();
        using ReferenceTracker.Lease _1 = GetReferenceTracker().GetLease();

        GraphicsDevice.ThrowIfDeviceLost();

        GraphicsDevice.D3D12Device->GetCopyableFootprint(
            DXGIFormatHelper.GetForType<T>(),
            (uint)width,
            out D3D12_PLACED_SUBRESOURCE_FOOTPRINT d3D12PlacedSubresourceFootprintDestination,
            out ulong rowSizeInBytes,
            out ulong totalSizeInBytes);

        using ComPtr<ID3D12Allocation> allocation = default;
        using ComPtr<ID3D12Resource> d3D12Resource = default;

        GraphicsDevice.CreateOrAllocateResource(
            ResourceType.ReadBack,
            AllocationMode.Default,
            totalSizeInBytes,
            out *&allocation,
            out *&d3D12Resource);

        using (CommandList copyCommandList = new(GraphicsDevice, this.d3D12CommandListType))
        {
            if (copyCommandList.D3D12CommandListType == D3D12_COMMAND_LIST_TYPE_COMPUTE)
            {
                copyCommandList.D3D12GraphicsCommandList->TransitionBarrier(D3D12Resource, this.d3D12ResourceState, D3D12_RESOURCE_STATE_COPY_SOURCE);
            }

            copyCommandList.D3D12GraphicsCommandList->CopyTextureRegion(
                d3D12ResourceDestination: d3D12Resource.Get(),
                &d3D12PlacedSubresourceFootprintDestination,
                destinationX: 0,
                destinationY: 0,
                destinationZ: 0,
                d3D12ResourceSource: D3D12Resource,
                sourceX: (uint)sourceOffsetX,
                sourceY: 0,
                sourceZ: 0,
                (uint)width,
                height: 1,
                depth: 1);

            if (copyCommandList.D3D12CommandListType == D3D12_COMMAND_LIST_TYPE_COMPUTE)
            {
                copyCommandList.D3D12GraphicsCommandList->TransitionBarrier(D3D12Resource, D3D12_RESOURCE_STATE_COPY_SOURCE, this.d3D12ResourceState);
            }

            copyCommandList.ExecuteAndWaitForCompletion();
        }

        using ID3D12ResourceMap resource = d3D12Resource.Get()->Map();

        fixed (void* destinationPointer = &destination)
        {
            Buffer.MemoryCopy(
                source: resource.Pointer,
                destination: destinationPointer,
                destinationSizeInBytes: rowSizeInBytes,
                sourceBytesToCopy: rowSizeInBytes);
        }
    }

    /// <summary>
    /// Reads the contents of the specified range from the current <see cref="Texture1D{T}"/> instance and writes them into a target <see cref="Texture1D{T}"/> instance.
    /// </summary>
    /// <param name="destination">The target <see cref="Texture1D{T}"/> instance to write data to.</param>
    /// <param name="destinationOffsetX">The horizontal offset within <paramref name="destination"/>.</param>
    /// <param name="sourceOffsetX">The horizontal offset in the source texture.</param>
    /// <param name="width">The width of the memory area to copy.</param>
    internal void CopyTo(Texture1D<T> destination, int sourceOffsetX, int destinationOffsetX, int width)
    {
        default(ArgumentOutOfRangeException).ThrowIfNotInRange(sourceOffsetX, 0, Width);
        default(ArgumentOutOfRangeException).ThrowIfNotInRange(destinationOffsetX, 0, destination.Width);
        default(ArgumentOutOfRangeException).ThrowIfNotBetweenOrEqual(width, 1, Width);
        default(ArgumentOutOfRangeException).ThrowIfNotBetweenOrEqual(width, 1, destination.Width);
        default(ArgumentOutOfRangeException).ThrowIfNotBetweenOrEqual(destinationOffsetX + width, 1, destination.Width, nameof(destinationOffsetX));
        default(ArgumentOutOfRangeException).ThrowIfGreaterThan(sourceOffsetX + width, Width, nameof(sourceOffsetX));

        using ReferenceTracker.Lease _0 = GraphicsDevice.GetReferenceTracker().GetLease();
        using ReferenceTracker.Lease _1 = GetReferenceTracker().GetLease();
        using ReferenceTracker.Lease _2 = destination.GetReferenceTracker().GetLease();

        GraphicsDevice.ThrowIfDeviceLost();

        destination.ThrowIfDeviceMismatch(GraphicsDevice);

        D3D12_COMMAND_LIST_TYPE d3D12CommandListType =
            this.d3D12CommandListType == D3D12_COMMAND_LIST_TYPE_COMPUTE ||
            destination.d3D12CommandListType == D3D12_COMMAND_LIST_TYPE_COMPUTE
            ? D3D12_COMMAND_LIST_TYPE_COMPUTE
            : D3D12_COMMAND_LIST_TYPE_COPY;

        using CommandList copyCommandList = new(GraphicsDevice, d3D12CommandListType);

        if (copyCommandList.D3D12CommandListType == D3D12_COMMAND_LIST_TYPE_COMPUTE)
        {
            copyCommandList.D3D12GraphicsCommandList->TransitionBarrier(D3D12Resource, this.d3D12ResourceState, D3D12_RESOURCE_STATE_COPY_SOURCE);
            copyCommandList.D3D12GraphicsCommandList->TransitionBarrier(destination.D3D12Resource, destination.d3D12ResourceState, D3D12_RESOURCE_STATE_COPY_DEST);
        }

        copyCommandList.D3D12GraphicsCommandList->CopyTextureRegion(
            d3D12ResourceDestination: destination.D3D12Resource,
            (uint)destinationOffsetX,
            destinationY: 0,
            destinationZ: 0,
            d3D12ResourceSource: D3D12Resource,
            (uint)sourceOffsetX,
            sourceY: 0,
            sourceZ: 0,
            (uint)width,
            height: 1,
            depth: 1);

        if (copyCommandList.D3D12CommandListType == D3D12_COMMAND_LIST_TYPE_COMPUTE)
        {
            copyCommandList.D3D12GraphicsCommandList->TransitionBarrier(D3D12Resource, D3D12_RESOURCE_STATE_COPY_SOURCE, this.d3D12ResourceState);
            copyCommandList.D3D12GraphicsCommandList->TransitionBarrier(destination.D3D12Resource, D3D12_RESOURCE_STATE_COPY_DEST, destination.d3D12ResourceState);
        }

        copyCommandList.ExecuteAndWaitForCompletion();
    }

    /// <summary>
    /// Reads the contents of the specified range from the current <see cref="Texture1D{T}"/> instance and writes them into a target <see cref="ReadBackTexture1D{T}"/> instance.
    /// </summary>
    /// <param name="destination">The target <see cref="ReadBackTexture1D{T}"/> instance to write data to.</param>
    /// <param name="sourceOffsetX">The horizontal offset in the source texture.</param>
    /// <param name="destinationOffsetX">The horizontal offset within <paramref name="destination"/>.</param>
    /// <param name="width">The width of the memory area to copy.</param>
    internal void CopyTo(ReadBackTexture1D<T> destination, int sourceOffsetX, int destinationOffsetX, int width)
    {
        default(ArgumentOutOfRangeException).ThrowIfNotInRange(sourceOffsetX, 0, Width);
        default(ArgumentOutOfRangeException).ThrowIfNotInRange(destinationOffsetX, 0, destination.Width);
        default(ArgumentOutOfRangeException).ThrowIfNotBetweenOrEqual(width, 1, Width);
        default(ArgumentOutOfRangeException).ThrowIfNotBetweenOrEqual(width, 1, destination.Width);
        default(ArgumentOutOfRangeException).ThrowIfNotBetweenOrEqual(destinationOffsetX + width, 1, destination.Width, nameof(destinationOffsetX));
        default(ArgumentOutOfRangeException).ThrowIfGreaterThan(sourceOffsetX + width, Width, nameof(sourceOffsetX));

        using ReferenceTracker.Lease _0 = GraphicsDevice.GetReferenceTracker().GetLease();
        using ReferenceTracker.Lease _1 = GetReferenceTracker().GetLease();
        using ReferenceTracker.Lease _2 = destination.GetReferenceTracker().GetLease();

        GraphicsDevice.ThrowIfDeviceLost();

        destination.ThrowIfDeviceMismatch(GraphicsDevice);

        using CommandList copyCommandList = new(GraphicsDevice, this.d3D12CommandListType);

        if (copyCommandList.D3D12CommandListType == D3D12_COMMAND_LIST_TYPE_COMPUTE)
        {
            copyCommandList.D3D12GraphicsCommandList->TransitionBarrier(D3D12Resource, this.d3D12ResourceState, D3D12_RESOURCE_STATE_COPY_SOURCE);
        }

        fixed (D3D12_PLACED_SUBRESOURCE_FOOTPRINT* d3D12PlacedSubresourceFootprintDestination = &destination.D3D12PlacedSubresourceFootprint)
        {
            copyCommandList.D3D12GraphicsCommandList->CopyTextureRegion(
                d3D12ResourceDestination: destination.D3D12Resource,
                d3D12PlacedSubresourceFootprintDestination,
                (uint)destinationOffsetX,
                destinationY: 0,
                destinationZ: 0,
                d3D12ResourceSource: D3D12Resource,
                (uint)sourceOffsetX,
                sourceY: 0,
                sourceZ: 0,
                (uint)width,
                height: 1,
                depth: 1);
        }

        if (copyCommandList.D3D12CommandListType == D3D12_COMMAND_LIST_TYPE_COMPUTE)
        {
            copyCommandList.D3D12GraphicsCommandList->TransitionBarrier(D3D12Resource, D3D12_RESOURCE_STATE_COPY_SOURCE, this.d3D12ResourceState);
        }

        copyCommandList.ExecuteAndWaitForCompletion();
    }

    /// <summary>
    /// Writes the contents of a given memory area to a specified area of the current <see cref="Texture1D{T}"/> instance.
    /// </summary>
    /// <param name="source">The input memory area to read data from.</param>
    /// <param name="size">The size of the memory area to read data from.</param>
    /// <param name="destinationOffsetX">The horizontal offset in the destination texture.</param>
    /// <param name="width">The width of the memory area to write to.</param>
    internal void CopyFrom(ref T source, int size, int destinationOffsetX, int width)
    {
        default(ArgumentOutOfRangeException).ThrowIfNotInRange(destinationOffsetX, 0, Width);
        default(ArgumentOutOfRangeException).ThrowIfNotBetweenOrEqual(width, 1, Width);
        default(ArgumentOutOfRangeException).ThrowIfGreaterThan(destinationOffsetX + width, Width, nameof(destinationOffsetX));
        default(ArgumentOutOfRangeException).ThrowIfLessThan(size, width);

        using ReferenceTracker.Lease _0 = GraphicsDevice.GetReferenceTracker().GetLease();
        using ReferenceTracker.Lease _1 = GetReferenceTracker().GetLease();

        GraphicsDevice.ThrowIfDeviceLost();

        GraphicsDevice.D3D12Device->GetCopyableFootprint(
            DXGIFormatHelper.GetForType<T>(),
            (uint)width,
            out D3D12_PLACED_SUBRESOURCE_FOOTPRINT d3D12PlacedSubresourceFootprintSource,
            out ulong rowSizeInBytes,
            out ulong totalSizeInBytes);

        using ComPtr<ID3D12Allocation> allocation = default;
        using ComPtr<ID3D12Resource> d3D12Resource = default;

        GraphicsDevice.CreateOrAllocateResource(
            ResourceType.Upload,
            AllocationMode.Default,
            totalSizeInBytes,
            out *&allocation,
            out *&d3D12Resource);

        using (ID3D12ResourceMap resource = d3D12Resource.Get()->Map())
        {
            fixed (void* sourcePointer = &source)
            {
                Buffer.MemoryCopy(
                    source: sourcePointer,
                    destination: resource.Pointer,
                    destinationSizeInBytes: rowSizeInBytes,
                    sourceBytesToCopy: rowSizeInBytes);
            }
        }

        using CommandList copyCommandList = new(GraphicsDevice, this.d3D12CommandListType);

        if (copyCommandList.D3D12CommandListType == D3D12_COMMAND_LIST_TYPE_COMPUTE)
        {
            copyCommandList.D3D12GraphicsCommandList->TransitionBarrier(D3D12Resource, this.d3D12ResourceState, D3D12_RESOURCE_STATE_COPY_DEST);
        }

        copyCommandList.D3D12GraphicsCommandList->CopyTextureRegion(
            d3D12ResourceDestination: D3D12Resource,
            destinationX: (uint)destinationOffsetX,
            destinationY: 0,
            destinationZ: 0,
            d3D12ResourceSource: d3D12Resource.Get(),
            &d3D12PlacedSubresourceFootprintSource,
            sourceX: 0,
            sourceY: 0,
            sourceZ: 0,
            (uint)width,
            height: 1,
            depth: 1);

        if (copyCommandList.D3D12CommandListType == D3D12_COMMAND_LIST_TYPE_COMPUTE)
        {
            copyCommandList.D3D12GraphicsCommandList->TransitionBarrier(D3D12Resource, D3D12_RESOURCE_STATE_COPY_DEST, this.d3D12ResourceState);
        }

        copyCommandList.ExecuteAndWaitForCompletion();
    }

    /// <summary>
    /// Writes the contents of a given <see cref="UploadTexture1D{T}"/> instance to a specified area of the current <see cref="Texture1D{T}"/> instance.
    /// </summary>
    /// <param name="source">The input <see cref="UploadTexture1D{T}"/> instance to read data from.</param>
    /// <param name="sourceOffsetX">The horizontal offset within <paramref name="source"/>.</param>
    /// <param name="destinationOffsetX">The horizontal offset in the destination texture.</param>
    /// <param name="width">The width of the memory area to write to.</param>
    internal void CopyFrom(UploadTexture1D<T> source, int sourceOffsetX, int destinationOffsetX, int width)
    {
        default(ArgumentOutOfRangeException).ThrowIfNotInRange(sourceOffsetX, 0, source.Width);
        default(ArgumentOutOfRangeException).ThrowIfNotInRange(destinationOffsetX, 0, Width);
        default(ArgumentOutOfRangeException).ThrowIfNotBetweenOrEqual(width, 1, Width);
        default(ArgumentOutOfRangeException).ThrowIfNotBetweenOrEqual(width, 1, source.Width);
        default(ArgumentOutOfRangeException).ThrowIfGreaterThan(sourceOffsetX + width, source.Width, nameof(sourceOffsetX));
        default(ArgumentOutOfRangeException).ThrowIfGreaterThan(destinationOffsetX + width, Width, nameof(destinationOffsetX));

        using ReferenceTracker.Lease _0 = GraphicsDevice.GetReferenceTracker().GetLease();
        using ReferenceTracker.Lease _1 = GetReferenceTracker().GetLease();
        using ReferenceTracker.Lease _2 = source.GetReferenceTracker().GetLease();

        GraphicsDevice.ThrowIfDeviceLost();

        source.ThrowIfDeviceMismatch(GraphicsDevice);

        using CommandList copyCommandList = new(GraphicsDevice, this.d3D12CommandListType);

        if (copyCommandList.D3D12CommandListType == D3D12_COMMAND_LIST_TYPE_COMPUTE)
        {
            copyCommandList.D3D12GraphicsCommandList->TransitionBarrier(D3D12Resource, this.d3D12ResourceState, D3D12_RESOURCE_STATE_COPY_DEST);
        }

        fixed (D3D12_PLACED_SUBRESOURCE_FOOTPRINT* d3D12PlacedSubresourceFootprintSource = &source.D3D12PlacedSubresourceFootprint)
        {
            copyCommandList.D3D12GraphicsCommandList->CopyTextureRegion(
                d3D12ResourceDestination: D3D12Resource,
                (uint)destinationOffsetX,
                destinationY: 0,
                destinationZ: 0,
                d3D12ResourceSource: source.D3D12Resource,
                d3D12PlacedSubresourceFootprintSource,
                (uint)sourceOffsetX,
                sourceY: 0,
                sourceZ: 0,
                (uint)width,
                height: 1,
                depth: 1);
        }

        if (copyCommandList.D3D12CommandListType == D3D12_COMMAND_LIST_TYPE_COMPUTE)
        {
            copyCommandList.D3D12GraphicsCommandList->TransitionBarrier(D3D12Resource, D3D12_RESOURCE_STATE_COPY_DEST, this.d3D12ResourceState);
        }

        copyCommandList.ExecuteAndWaitForCompletion();
    }

    /// <inheritdoc/>
    protected virtual partial void DangerousOnDispose()
    {
        this.d3D12Resource.Dispose();
        this.allocation.Dispose();

        if (GraphicsDevice is GraphicsDevice device)
        {
            device.ReturnShaderResourceViewDescriptorHandles(in this.d3D12ResourceDescriptorHandles);
        }
    }

    /// <summary>
    /// Throws an <see cref="InvalidOperationException"/> if the current resource is not in a readonly state.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void ThrowIfIsNotInReadOnlyState()
    {
        if (this.d3D12ResourceState == D3D12_RESOURCE_STATE_UNORDERED_ACCESS)
        {
            static void Throw()
            {
                throw new InvalidOperationException(
                    "The texture is not currently in readonly mode. This API can only be used when creating a compute graph with " +
                    "the ComputeContext type, and after having used ComputeContext.Transition() to change the state of the texture.");
            }

            Throw();
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

    /// <inheritdoc cref="GraphicsResourceHelper.IGraphicsResource.ValidateAndGetGpuAndCpuDescriptorHandlesForClear(GraphicsDevice, out bool)"/>
    internal (D3D12_GPU_DESCRIPTOR_HANDLE Gpu, D3D12_CPU_DESCRIPTOR_HANDLE Cpu) ValidateAndGetGpuAndCpuDescriptorHandlesForClear(GraphicsDevice device, out bool isNormalized)
    {
        using ReferenceTracker.Lease _0 = GetReferenceTracker().GetLease();

        ThrowIfDeviceMismatch(device);

        isNormalized = DXGIFormatHelper.IsNormalizedType<T>();

        return (this.d3D12ResourceDescriptorHandles.D3D12GpuDescriptorHandle, this.d3D12ResourceDescriptorHandles.D3D12CpuDescriptorHandleNonShaderVisible);
    }

    /// <inheritdoc cref="GraphicsResourceHelper.IGraphicsResource.ValidateAndGetID3D12Resource(GraphicsDevice, out ReferenceTracker.Lease)"/>
    internal ID3D12Resource* ValidateAndGetID3D12Resource(GraphicsDevice device, out ReferenceTracker.Lease lease)
    {
        lease = GetReferenceTracker().GetLease();

        ThrowIfDeviceMismatch(device);

        return D3D12Resource;
    }

    /// <inheritdoc cref="GraphicsResourceHelper.IGraphicsResource.ValidateAndGetID3D12ResourceAndTransitionStates(GraphicsDevice, ResourceState, out ID3D12Resource*, out ReferenceTracker.Lease)"/>
    internal (D3D12_RESOURCE_STATES Before, D3D12_RESOURCE_STATES After) ValidateAndGetID3D12ResourceAndTransitionStates(GraphicsDevice device, ResourceState resourceState, out ID3D12Resource* d3D12Resource, out ReferenceTracker.Lease lease)
    {
        lease = GetReferenceTracker().GetLease();

        ThrowIfDeviceMismatch(device);

        D3D12_RESOURCE_STATES d3D12ResourceStatesBefore = this.d3D12ResourceState;
        D3D12_RESOURCE_STATES d3D12ResourceStatesAfter = ResourceStateHelper.GetD3D12ResourceStates(resourceState);

        this.d3D12ResourceState = d3D12ResourceStatesAfter;

        d3D12Resource = D3D12Resource;

        return (d3D12ResourceStatesBefore, d3D12ResourceStatesAfter);
    }

    /// <inheritdoc/>
    D3D12_GPU_DESCRIPTOR_HANDLE GraphicsResourceHelper.IGraphicsResource.ValidateAndGetGpuDescriptorHandle(GraphicsDevice device)
    {
        using ReferenceTracker.Lease _0 = GetReferenceTracker().GetLease();

        ThrowIfDeviceMismatch(device);

        return D3D12GpuDescriptorHandle;
    }

    /// <inheritdoc/>
    (D3D12_GPU_DESCRIPTOR_HANDLE, D3D12_CPU_DESCRIPTOR_HANDLE) GraphicsResourceHelper.IGraphicsResource.ValidateAndGetGpuAndCpuDescriptorHandlesForClear(GraphicsDevice device, out bool isNormalized)
    {
        return ValidateAndGetGpuAndCpuDescriptorHandlesForClear(device, out isNormalized);
    }

    /// <inheritdoc/>
    ID3D12Resource* GraphicsResourceHelper.IGraphicsResource.ValidateAndGetID3D12Resource(GraphicsDevice device, out ReferenceTracker.Lease lease)
    {
        return ValidateAndGetID3D12Resource(device, out lease);
    }

    /// <inheritdoc/>
    (D3D12_RESOURCE_STATES, D3D12_RESOURCE_STATES) GraphicsResourceHelper.IGraphicsResource.ValidateAndGetID3D12ResourceAndTransitionStates(GraphicsDevice device, ResourceState resourceState, out ID3D12Resource* d3D12Resource, out ReferenceTracker.Lease lease)
    {
        return ValidateAndGetID3D12ResourceAndTransitionStates(device, resourceState, out d3D12Resource, out lease);
    }
}