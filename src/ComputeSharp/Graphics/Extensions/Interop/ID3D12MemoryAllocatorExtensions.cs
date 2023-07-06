using System;
using ComputeSharp.Core.Extensions;
using ComputeSharp.Interop.Allocation;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using static TerraFX.Interop.DirectX.D3D12_HEAP_TYPE;
using static TerraFX.Interop.DirectX.D3D12_RESOURCE_FLAGS;
using static TerraFX.Interop.DirectX.D3D12_RESOURCE_STATES;
using ResourceType = ComputeSharp.Graphics.Resources.Enums.ResourceType;

namespace ComputeSharp.Graphics.Extensions;

/// <summary>
/// A <see langword="class"/> with extensions for the <see cref="ID3D12MemoryAllocator"/> type.
/// </summary>
internal static unsafe class ID3D12MemoryAllocatorExtensions
{
    /// <summary>
    /// Allocates a resource for a given buffer type.
    /// </summary>
    /// <param name="allocator">The <see cref="ID3D12MemoryAllocator"/> instance in use.</param>
    /// <param name="resourceType">The resource type currently in use.</param>
    /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
    /// <param name="sizeInBytes">The size in bytes of the current buffer.</param>
    /// <returns>An <see cref="ComPtr{T}"/> reference for the current <see cref="ID3D12Allocation"/> object.</returns>
    public static ComPtr<ID3D12Allocation> AllocateResource(
        this ref ID3D12MemoryAllocator allocator,
        ResourceType resourceType,
        AllocationMode allocationMode,
        ulong sizeInBytes)
    {
        (D3D12_HEAP_TYPE d3D12HeapType,
         D3D12_RESOURCE_FLAGS d3D12ResourceFlags,
         D3D12_RESOURCE_STATES d3D12ResourceStates) = resourceType switch
         {
             ResourceType.Constant => (D3D12_HEAP_TYPE_UPLOAD, D3D12_RESOURCE_FLAG_NONE, D3D12_RESOURCE_STATE_GENERIC_READ),
             ResourceType.ReadOnly => (D3D12_HEAP_TYPE_DEFAULT, D3D12_RESOURCE_FLAG_NONE, D3D12_RESOURCE_STATE_COMMON),
             ResourceType.ReadWrite => (D3D12_HEAP_TYPE_DEFAULT, D3D12_RESOURCE_FLAG_ALLOW_UNORDERED_ACCESS, D3D12_RESOURCE_STATE_COMMON),
             ResourceType.ReadBack => (D3D12_HEAP_TYPE_READBACK, D3D12_RESOURCE_FLAG_NONE, D3D12_RESOURCE_STATE_COPY_DEST),
             ResourceType.Upload => (D3D12_HEAP_TYPE_UPLOAD, D3D12_RESOURCE_FLAG_NONE, D3D12_RESOURCE_STATE_GENERIC_READ),
             _ => default(ArgumentException).Throw<(D3D12_HEAP_TYPE, D3D12_RESOURCE_FLAGS, D3D12_RESOURCE_STATES)>(nameof(resourceType))
         };

        using ComPtr<ID3D12Allocation> allocation = default;

        D3D12_RESOURCE_DESC d3D12ResourceDescription = D3D12_RESOURCE_DESC.Buffer(sizeInBytes, d3D12ResourceFlags);

        allocator.AllocateResource(
            resourceDescription: &d3D12ResourceDescription,
            heapType: d3D12HeapType,
            resourceStates: d3D12ResourceStates,
            clearAllocation: allocationMode == AllocationMode.Clear,
            allocation: allocation.GetAddressOf()).Assert();

        return allocation.Move();
    }

    /// <summary>
    /// Allocates a resource for a given 1D texture type.
    /// </summary>
    /// <param name="allocator">The <see cref="ID3D12MemoryAllocator"/> instance in use.</param>
    /// <param name="resourceType">The resource type currently in use.</param>
    /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
    /// <param name="dxgiFormat">The <see cref="DXGI_FORMAT"/> value to use.</param>
    /// <param name="width">The width of the texture resource.</param>
    /// <param name="d3D12ResourceStates">The default <see cref="D3D12_RESOURCE_STATES"/> value for the resource.</param>
    /// <returns>An <see cref="ComPtr{T}"/> reference for the current <see cref="ID3D12Allocation"/> object.</returns>
    public static ComPtr<ID3D12Allocation> AllocateResource(
        this ref ID3D12MemoryAllocator allocator,
        ResourceType resourceType,
        AllocationMode allocationMode,
        DXGI_FORMAT dxgiFormat,
        uint width,
        out D3D12_RESOURCE_STATES d3D12ResourceStates)
    {
        (D3D12_RESOURCE_FLAGS d3D12ResourceFlags, d3D12ResourceStates) = resourceType switch
        {
            ResourceType.ReadOnly => (D3D12_RESOURCE_FLAG_NONE, D3D12_RESOURCE_STATE_COMMON),
            ResourceType.ReadWrite => (D3D12_RESOURCE_FLAG_ALLOW_UNORDERED_ACCESS, D3D12_RESOURCE_STATE_UNORDERED_ACCESS),
            _ => default(ArgumentException).Throw<(D3D12_RESOURCE_FLAGS, D3D12_RESOURCE_STATES)>(nameof(resourceType))
        };

        using ComPtr<ID3D12Allocation> allocation = default;

        D3D12_RESOURCE_DESC d3D12ResourceDescription = D3D12_RESOURCE_DESC.Tex1D(dxgiFormat, width, mipLevels: 1, flags: d3D12ResourceFlags);

        allocator.AllocateResource(
            resourceDescription: &d3D12ResourceDescription,
            heapType: D3D12_HEAP_TYPE_DEFAULT,
            resourceStates: d3D12ResourceStates,
            clearAllocation: allocationMode == AllocationMode.Clear,
            allocation: allocation.GetAddressOf()).Assert();

        return allocation.Move();
    }

    /// <summary>
    /// Allocates a resource for a given 2D texture type.
    /// </summary>
    /// <param name="allocator">The <see cref="ID3D12MemoryAllocator"/> instance in use.</param>
    /// <param name="resourceType">The resource type currently in use.</param>
    /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
    /// <param name="dxgiFormat">The <see cref="DXGI_FORMAT"/> value to use.</param>
    /// <param name="width">The width of the texture resource.</param>
    /// <param name="height">The height of the texture resource.</param>
    /// <param name="d3D12ResourceStates">The default <see cref="D3D12_RESOURCE_STATES"/> value for the resource.</param>
    /// <returns>An <see cref="ComPtr{T}"/> reference for the current <see cref="ID3D12Allocation"/> object.</returns>
    public static ComPtr<ID3D12Allocation> AllocateResource(
        this ref ID3D12MemoryAllocator allocator,
        ResourceType resourceType,
        AllocationMode allocationMode,
        DXGI_FORMAT dxgiFormat,
        uint width,
        uint height,
        out D3D12_RESOURCE_STATES d3D12ResourceStates)
    {
        (D3D12_RESOURCE_FLAGS d3D12ResourceFlags, d3D12ResourceStates) = resourceType switch
        {
            ResourceType.ReadOnly => (D3D12_RESOURCE_FLAG_NONE, D3D12_RESOURCE_STATE_COMMON),
            ResourceType.ReadWrite => (D3D12_RESOURCE_FLAG_ALLOW_UNORDERED_ACCESS, D3D12_RESOURCE_STATE_UNORDERED_ACCESS),
            _ => default(ArgumentException).Throw<(D3D12_RESOURCE_FLAGS, D3D12_RESOURCE_STATES)>(nameof(resourceType))
        };

        using ComPtr<ID3D12Allocation> allocation = default;

        D3D12_RESOURCE_DESC d3D12ResourceDescription = D3D12_RESOURCE_DESC.Tex2D(dxgiFormat, width, height, mipLevels: 1, flags: d3D12ResourceFlags);

        allocator.AllocateResource(
            resourceDescription: &d3D12ResourceDescription,
            heapType: D3D12_HEAP_TYPE_DEFAULT,
            resourceStates: d3D12ResourceStates,
            clearAllocation: allocationMode == AllocationMode.Clear,
            allocation: allocation.GetAddressOf()).Assert();

        return allocation.Move();
    }

    /// <summary>
    /// Allocates a resource for a given 3D texture type.
    /// </summary>
    /// <param name="allocator">The <see cref="ID3D12MemoryAllocator"/> instance in use.</param>
    /// <param name="resourceType">The resource type currently in use.</param>
    /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
    /// <param name="dxgiFormat">The <see cref="DXGI_FORMAT"/> value to use.</param>
    /// <param name="width">The width of the texture resource.</param>
    /// <param name="height">The height of the texture resource.</param>
    /// <param name="depth">The depth of the texture resource.</param>
    /// <param name="d3D12ResourceStates">The default <see cref="D3D12_RESOURCE_STATES"/> value for the resource.</param>
    /// <returns>An <see cref="ComPtr{T}"/> reference for the current <see cref="ID3D12Allocation"/> object.</returns>
    public static ComPtr<ID3D12Allocation> AllocateResource(
        this ref ID3D12MemoryAllocator allocator,
        ResourceType resourceType,
        AllocationMode allocationMode,
        DXGI_FORMAT dxgiFormat,
        uint width,
        uint height,
        ushort depth,
        out D3D12_RESOURCE_STATES d3D12ResourceStates)
    {
        (D3D12_RESOURCE_FLAGS d3D12ResourceFlags, d3D12ResourceStates) = resourceType switch
        {
            ResourceType.ReadOnly => (D3D12_RESOURCE_FLAG_NONE, D3D12_RESOURCE_STATE_COMMON),
            ResourceType.ReadWrite => (D3D12_RESOURCE_FLAG_ALLOW_UNORDERED_ACCESS, D3D12_RESOURCE_STATE_UNORDERED_ACCESS),
            _ => default(ArgumentException).Throw<(D3D12_RESOURCE_FLAGS, D3D12_RESOURCE_STATES)>(nameof(resourceType))
        };

        using ComPtr<ID3D12Allocation> allocation = default;

        D3D12_RESOURCE_DESC d3D12ResourceDescription = D3D12_RESOURCE_DESC.Tex3D(dxgiFormat, width, height, depth, mipLevels: 1, flags: d3D12ResourceFlags);

        allocator.AllocateResource(
            resourceDescription: &d3D12ResourceDescription,
            heapType: D3D12_HEAP_TYPE_DEFAULT,
            resourceStates: d3D12ResourceStates,
            clearAllocation: allocationMode == AllocationMode.Clear,
            allocation: allocation.GetAddressOf()).Assert();

        return allocation.Move();
    }
}