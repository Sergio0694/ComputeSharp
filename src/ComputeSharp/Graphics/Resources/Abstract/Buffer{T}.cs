using System.Buffers;
using System.Runtime.CompilerServices;
using ComputeSharp.Core.Helpers;
using ComputeSharp.Exceptions;
using ComputeSharp.Graphics.Extensions;
using ComputeSharp.Graphics.Helpers;
using ComputeSharp.Interop;
using Microsoft.Toolkit.Diagnostics;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using static TerraFX.Interop.DirectX.D3D12_FEATURE;
using ResourceType = ComputeSharp.Graphics.Resources.Enums.ResourceType;
#if NET6_0_OR_GREATER
using MemoryMarshal = System.Runtime.InteropServices.MemoryMarshal;
#else
using MemoryMarshal = ComputeSharp.NetStandard.System.Runtime.InteropServices.MemoryMarshal;
#endif

namespace ComputeSharp.Resources;

/// <summary>
/// A <see langword="class"/> representing a typed buffer stored on GPU memory.
/// </summary>
/// <typeparam name="T">The type of items stored on the buffer.</typeparam>
public unsafe abstract class Buffer<T> : NativeObject
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
    /// The <see cref="D3D12_CPU_DESCRIPTOR_HANDLE"/> instance for the current resource.
    /// </summary>
    private readonly D3D12_CPU_DESCRIPTOR_HANDLE D3D12CpuDescriptorHandle;

    /// <summary>
    /// The <see cref="D3D12_GPU_DESCRIPTOR_HANDLE"/> instance for the current resource.
    /// </summary>
    internal readonly D3D12_GPU_DESCRIPTOR_HANDLE D3D12GpuDescriptorHandle;

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
    private protected Buffer(GraphicsDevice device, int length, uint elementSizeInBytes, ResourceType resourceType, AllocationMode allocationMode)
    {
        device.ThrowIfDisposed();

        if (resourceType == ResourceType.Constant)
        {
            Guard.IsBetweenOrEqualTo(length, 1, D3D12.D3D12_REQ_CONSTANT_BUFFER_ELEMENT_COUNT, nameof(length));
        }
        else
        {
            // The maximum length is set such that the aligned buffer size can't exceed uint.MaxValue
            Guard.IsBetweenOrEqualTo(length, 1, (uint.MaxValue / elementSizeInBytes) & ~255, nameof(length));
        }

        if (TypeInfo<T>.IsDoubleOrContainsDoubles &&
            device.D3D12Device->CheckFeatureSupport<D3D12_FEATURE_DATA_D3D12_OPTIONS>(D3D12_FEATURE_D3D12_OPTIONS).DoublePrecisionFloatShaderOps == 0)
        {
            UnsupportedDoubleOperationsException.Throw<T>();
        }

        nint usableSizeInBytes = checked((nint)(length * elementSizeInBytes));
        nint effectiveSizeInBytes = resourceType == ResourceType.Constant ? AlignmentHelper.Pad(usableSizeInBytes, D3D12.D3D12_CONSTANT_BUFFER_DATA_PLACEMENT_ALIGNMENT) : usableSizeInBytes;

        SizeInBytes = usableSizeInBytes;
        GraphicsDevice = device;
        Length = length;

#if NET6_0_OR_GREATER
        this.allocation = device.Allocator->CreateResource(device.Pool, resourceType, allocationMode, (ulong)effectiveSizeInBytes);
        this.d3D12Resource = new ComPtr<ID3D12Resource>(this.allocation.Get()->GetResource());
#else
        this.d3D12Resource = device.D3D12Device->CreateCommittedResource(resourceType, allocationMode, (ulong)effectiveSizeInBytes, device.IsCacheCoherentUMA);
#endif

        device.RegisterAllocatedResource();
        device.RentShaderResourceViewDescriptorHandles(out D3D12CpuDescriptorHandle, out D3D12GpuDescriptorHandle);

        switch (resourceType)
        {
            case ResourceType.Constant:
                device.D3D12Device->CreateConstantBufferView(this.d3D12Resource.Get(), effectiveSizeInBytes, D3D12CpuDescriptorHandle);
                break;
            case ResourceType.ReadOnly:
                device.D3D12Device->CreateShaderResourceView(this.d3D12Resource.Get(), (uint)length, elementSizeInBytes, D3D12CpuDescriptorHandle);
                break;
            case ResourceType.ReadWrite:
                device.D3D12Device->CreateUnorderedAccessView(this.d3D12Resource.Get(), (uint)length, elementSizeInBytes, D3D12CpuDescriptorHandle);
                break;
        }

        this.d3D12Resource.Get()->SetName(this);
    }

    /// <summary>
    /// Gets the <see cref="ComputeSharp.GraphicsDevice"/> associated with the current instance.
    /// </summary>
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
        get => SizeInBytes > (nint)Length * sizeof(T);
    }

    /// <summary>
    /// Gets the <see cref="ID3D12Resource"/> instance currently mapped.
    /// </summary>
    internal ID3D12Resource* D3D12Resource => this.d3D12Resource;

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
    protected override void OnDispose()
    {
        this.d3D12Resource.Dispose();
#if NET6_0_OR_GREATER
        this.allocation.Dispose();
#endif

        if (GraphicsDevice is GraphicsDevice device)
        {
            device.UnregisterAllocatedResource();
            device.ReturnShaderResourceViewDescriptorHandles(D3D12CpuDescriptorHandle, D3D12GpuDescriptorHandle);
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
