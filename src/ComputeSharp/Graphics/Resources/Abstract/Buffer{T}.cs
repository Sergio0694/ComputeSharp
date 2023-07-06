using System;
using System.Buffers;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using ComputeSharp.Core.Helpers;
using ComputeSharp.Exceptions;
using ComputeSharp.Graphics.Commands.Interop;
using ComputeSharp.Graphics.Extensions;
using ComputeSharp.Graphics.Helpers;
using ComputeSharp.Interop;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using static TerraFX.Interop.DirectX.D3D12_FEATURE;
using static TerraFX.Interop.DirectX.DXGI_FORMAT;
using ResourceType = ComputeSharp.Graphics.Resources.Enums.ResourceType;
using ComputeSharp.Interop.Allocation;
#if NET6_0_OR_GREATER
using MemoryMarshal = System.Runtime.InteropServices.MemoryMarshal;
#else
using MemoryMarshal = ComputeSharp.NetStandard.MemoryMarshal;
#endif

namespace ComputeSharp.Resources;

/// <summary>
/// A <see langword="class"/> representing a typed buffer stored on GPU memory.
/// </summary>
/// <typeparam name="T">The type of items stored on the buffer.</typeparam>
public abstract unsafe partial class Buffer<T> : IReferenceTrackedObject, IGraphicsResource
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
    /// The <see cref="ID3D12ResourceDescriptorHandles"/> instance for the current resource, when a typed UAV is needed.
    /// </summary>
    private readonly ID3D12ResourceDescriptorHandles d3D12ResourceDescriptorHandlesForTypedUnorderedAccessView;

    /// <summary>
    /// The size in bytes of the current buffer (this value is never negative).
    /// </summary>
    protected readonly nint SizeInBytes;

    /// <summary>
    /// Creates a new <see cref="Buffer{T}"/> instance with the specified parameters.
    /// </summary>
    /// <param name="device">The <see cref="GraphicsDevice"/> associated with the current instance.</param>
    /// <param name="length">The number of items to store in the current buffer.</param>
    /// <param name="elementSizeInBytes">The size in bytes of each buffer item (including padding, if any).</param>
    /// <param name="resourceType">The resource type for the current buffer.</param>
    /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
    [RequiresUnreferencedCode("This method reads type info of all fields of the resource element type (recursively).")]
    private protected Buffer(GraphicsDevice device, int length, uint elementSizeInBytes, ResourceType resourceType, AllocationMode allocationMode)
    {
        using ReferenceTracker.Lease _0 = ReferenceTracker.Create(this, out this.referenceTracker);

        if (resourceType == ResourceType.Constant)
        {
            default(ArgumentOutOfRangeException).ThrowIfNotBetweenOrEqual(length, 1, D3D12.D3D12_REQ_CONSTANT_BUFFER_ELEMENT_COUNT);
        }
        else
        {
            // The maximum length is set such that the aligned buffer size can't exceed uint.MaxValue
            default(ArgumentOutOfRangeException).ThrowIfNotBetweenOrEqual(length, 1, (uint.MaxValue / elementSizeInBytes) & ~255);
        }

        using ReferenceTracker.Lease _1 = device.GetReferenceTracker().GetLease();

        device.ThrowIfDeviceLost();

        if (TypeInfo<T>.IsDoubleOrContainsDoubles &&
            device.D3D12Device->CheckFeatureSupport<D3D12_FEATURE_DATA_D3D12_OPTIONS>(D3D12_FEATURE_D3D12_OPTIONS).DoublePrecisionFloatShaderOps == 0)
        {
            UnsupportedDoubleOperationException.Throw<T>();
        }

        nint usableSizeInBytes = checked((nint)(length * elementSizeInBytes));
        nint effectiveSizeInBytes = resourceType == ResourceType.Constant ? AlignmentHelper.Pad(usableSizeInBytes, D3D12.D3D12_CONSTANT_BUFFER_DATA_PLACEMENT_ALIGNMENT) : usableSizeInBytes;

        this.SizeInBytes = usableSizeInBytes;
        GraphicsDevice = device;
        Length = length;

        device.CreateOrAllocateResource(
            resourceType,
            allocationMode,
            (ulong)effectiveSizeInBytes,
            out this.allocation,
            out this.d3D12Resource);

        device.RentShaderResourceViewDescriptorHandles(out this.d3D12ResourceDescriptorHandles);
        device.RentShaderResourceViewDescriptorHandles(out this.d3D12ResourceDescriptorHandlesForTypedUnorderedAccessView);

        switch (resourceType)
        {
            case ResourceType.Constant:
                device.D3D12Device->CreateConstantBufferView(this.d3D12Resource.Get(), effectiveSizeInBytes, this.d3D12ResourceDescriptorHandles.D3D12CpuDescriptorHandle);
                break;
            case ResourceType.ReadOnly:
                device.D3D12Device->CreateShaderResourceView(this.d3D12Resource.Get(), (uint)length, elementSizeInBytes, this.d3D12ResourceDescriptorHandles.D3D12CpuDescriptorHandle);
                break;
            case ResourceType.ReadWrite:
                device.D3D12Device->CreateUnorderedAccessView(this.d3D12Resource.Get(), (uint)length, elementSizeInBytes, this.d3D12ResourceDescriptorHandles.D3D12CpuDescriptorHandle);
                device.D3D12Device->CreateUnorderedAccessViewForClear(
                    this.d3D12Resource.Get(),
                    DXGI_FORMAT_R32_UINT,
                    (uint)(usableSizeInBytes / sizeof(uint)),
                    this.d3D12ResourceDescriptorHandlesForTypedUnorderedAccessView.D3D12CpuDescriptorHandle,
                    this.d3D12ResourceDescriptorHandlesForTypedUnorderedAccessView.D3D12CpuDescriptorHandleNonShaderVisible);
                break;
        }

        this.d3D12Resource.Get()->SetName(this);
    }

    /// <inheritdoc/>
    public GraphicsDevice GraphicsDevice { get; }

    /// <summary>
    /// Gets the length of the current buffer.
    /// </summary>
    public int Length { get; }

    /// <summary>
    /// Gets whether or not there is some padding between elements in the current buffer.
    /// </summary>
    internal bool IsPaddingPresent
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => this.SizeInBytes > (nint)Length * sizeof(T);
    }

    /// <summary>
    /// Gets the <see cref="ID3D12Resource"/> instance currently mapped.
    /// </summary>
    internal ID3D12Resource* D3D12Resource => this.d3D12Resource;

    /// <summary>
    /// Gets the <see cref="D3D12_GPU_DESCRIPTOR_HANDLE"/> instance for the current resource.
    /// </summary>
    internal D3D12_GPU_DESCRIPTOR_HANDLE D3D12GpuDescriptorHandle => this.d3D12ResourceDescriptorHandles.D3D12GpuDescriptorHandle;

    /// <summary>
    /// Reads the contents of the specified range from the current <see cref="Buffer{T}"/> instance and writes them into a target memory area.
    /// </summary>
    /// <param name="destination">The input memory area to write data to.</param>
    /// <param name="sourceOffset">The offset to start reading data from.</param>
    /// <param name="count">The length of the memory area to write data to.</param>
    internal abstract void CopyTo(ref T destination, int sourceOffset, int count);

    /// <summary>
    /// Writes the contents of a given range from the current <see cref="Buffer{T}"/> instance and writes them into a target <see cref="Buffer{T}"/> instance.
    /// </summary>
    /// <param name="destination">The target <see cref="Buffer{T}"/> instance to write data to.</param>
    /// <param name="sourceOffset">The offset to start reading data from.</param>
    /// <param name="destinationOffset">The starting offset within <paramref name="destination"/> to write data to.</param>
    /// <param name="count">The number of items to read.</param>
    internal abstract void CopyTo(Buffer<T> destination, int sourceOffset, int destinationOffset, int count);

    /// <summary>
    /// Writes the contents of a given range from the current <see cref="Buffer{T}"/> instance and writes them into a target <see cref="Buffer{T}"/> instance, using a temporary CPU buffer.
    /// </summary>
    /// <param name="destination">The target <see cref="Buffer{T}"/> instance to write data to.</param>
    /// <param name="sourceOffset">The offset to start reading data from.</param>
    /// <param name="destinationOffset">The starting offset within <paramref name="destination"/> to write data to.</param>
    /// <param name="count">The number of items to read.</param>
    protected void CopyToWithCpuBuffer(Buffer<T> destination, int sourceOffset, int destinationOffset, int count)
    {
        T[] array = ArrayPool<T>.Shared.Rent(count);

        try
        {
            ref T r0 = ref MemoryMarshal.GetArrayDataReference(array);

            CopyTo(ref r0, sourceOffset, count);

            destination.CopyFrom(ref r0, destinationOffset, count);
        }
        finally
        {
            ArrayPool<T>.Shared.Return(array);
        }
    }

    /// <summary>
    /// Writes the contents of a given memory area to a specified area of the current <see cref="Buffer{T}"/> instance.
    /// </summary>
    /// <param name="source">The input memory area to read data from.</param>
    /// <param name="destinationOffset">The offset to start writing data to.</param>
    /// <param name="count">The length of the input memory area to read data from.</param>
    internal abstract void CopyFrom(ref T source, int destinationOffset, int count);

    /// <inheritdoc/>
    void IReferenceTrackedObject.DangerousOnDispose()
    {
        this.d3D12Resource.Dispose();
        this.allocation.Dispose();

        if (GraphicsDevice is GraphicsDevice device)
        {
            device.ReturnShaderResourceViewDescriptorHandles(in this.d3D12ResourceDescriptorHandles);
            device.ReturnShaderResourceViewDescriptorHandles(in this.d3D12ResourceDescriptorHandlesForTypedUnorderedAccessView);
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

    /// <inheritdoc cref="__Internals.GraphicsResourceHelper.IGraphicsResource.ValidateAndGetGpuAndCpuDescriptorHandlesForClear(GraphicsDevice, out bool)"/>
    internal (D3D12_GPU_DESCRIPTOR_HANDLE Gpu, D3D12_CPU_DESCRIPTOR_HANDLE Cpu) ValidateAndGetGpuAndCpuDescriptorHandlesForClear(GraphicsDevice device)
    {
        using ReferenceTracker.Lease _0 = GetReferenceTracker().GetLease();

        ThrowIfDeviceMismatch(device);

        return (
            this.d3D12ResourceDescriptorHandlesForTypedUnorderedAccessView.D3D12GpuDescriptorHandle,
            this.d3D12ResourceDescriptorHandlesForTypedUnorderedAccessView.D3D12CpuDescriptorHandleNonShaderVisible);
    }

    /// <inheritdoc cref="__Internals.GraphicsResourceHelper.IGraphicsResource.ValidateAndGetID3D12Resource(GraphicsDevice, out ReferenceTracker.Lease)"/>
    internal unsafe ID3D12Resource* ValidateAndGetID3D12Resource(GraphicsDevice device, out ReferenceTracker.Lease lease)
    {
        lease = GetReferenceTracker().GetLease();

        ThrowIfDeviceMismatch(device);

        return D3D12Resource;
    }
}