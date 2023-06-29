using ComputeSharp.Graphics.Extensions;
using ComputeSharp.Interop.Allocation;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using ResourceType = ComputeSharp.Graphics.Resources.Enums.ResourceType;

namespace ComputeSharp;

/// <inheritdoc/>
unsafe partial class GraphicsDevice
{
    /// <summary>
    /// Creates or allocates a resource for a given buffer type.
    /// </summary>
    /// <param name="resourceType">The resource type currently in use.</param>
    /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
    /// <param name="sizeInBytes">The size in bytes of the current buffer.</param>
    /// <param name="allocation">The resulting <see cref="ID3D12Allocation"/> object, if one is used.</param>
    /// <param name="d3D12Resource">The resulting <see cref="ID3D12Resource"/> object for the buffer.</param>
    internal void CreateOrAllocateResource(
        ResourceType resourceType,
        AllocationMode allocationMode,
        ulong sizeInBytes,
        out ComPtr<ID3D12Allocation> allocation,
        out ComPtr<ID3D12Resource> d3D12Resource)
    {
        if (this.allocator.Get() is not null)
        {
            allocation = this.allocator.Get()->AllocateResource(resourceType, allocationMode, sizeInBytes);
            d3D12Resource = allocation.Get()->GetD3D12Resource();
        }
        else
        {
            allocation = null;
            d3D12Resource = this.d3D12Device.Get()->CreateCommittedResource(resourceType, sizeInBytes, IsCacheCoherentUMA);
        }
    }

    /// <summary>
    /// Creates or allocates a resource for a given 1D texture type.
    /// </summary>
    /// <param name="resourceType">The resource type currently in use.</param>
    /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
    /// <param name="dxgiFormat">The <see cref="DXGI_FORMAT"/> value to use.</param>
    /// <param name="width">The width of the texture resource.</param>
    /// <param name="d3D12ResourceStates">The default <see cref="D3D12_RESOURCE_STATES"/> value for the resource.</param>
    /// <param name="allocation">The resulting <see cref="ID3D12Allocation"/> object, if one is used.</param>
    /// <param name="d3D12Resource">The resulting <see cref="ID3D12Resource"/> object for the 1D texture.</param>
    internal void CreateOrAllocateResource(
        ResourceType resourceType,
        AllocationMode allocationMode,
        DXGI_FORMAT dxgiFormat,
        uint width,
        out ComPtr<ID3D12Allocation> allocation,
        out ComPtr<ID3D12Resource> d3D12Resource,
        out D3D12_RESOURCE_STATES d3D12ResourceStates)
    {
        if (this.allocator.Get() is not null)
        {
            allocation = this.allocator.Get()->AllocateResource(
                resourceType,
                allocationMode,
                dxgiFormat,
                width,
                out d3D12ResourceStates);

            d3D12Resource = allocation.Get()->GetD3D12Resource();
        }
        else
        {
            allocation = null;

            d3D12Resource = this.d3D12Device.Get()->CreateCommittedResource(
                resourceType,
                dxgiFormat,
                width,
                IsCacheCoherentUMA,
                out d3D12ResourceStates);
        }
    }

    /// <summary>
    /// Creates or allocates a resource for a given 2D texture type.
    /// </summary>
    /// <param name="resourceType">The resource type currently in use.</param>
    /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
    /// <param name="dxgiFormat">The <see cref="DXGI_FORMAT"/> value to use.</param>
    /// <param name="width">The width of the texture resource.</param>
    /// <param name="height">The height of the texture resource.</param>
    /// <param name="d3D12ResourceStates">The default <see cref="D3D12_RESOURCE_STATES"/> value for the resource.</param>
    /// <param name="allocation">The resulting <see cref="ID3D12Allocation"/> object, if one is used.</param>
    /// <param name="d3D12Resource">The resulting <see cref="ID3D12Resource"/> object for the 2D texture.</param>
    internal void CreateOrAllocateResource(
        ResourceType resourceType,
        AllocationMode allocationMode,
        DXGI_FORMAT dxgiFormat,
        uint width,
        uint height,
        out ComPtr<ID3D12Allocation> allocation,
        out ComPtr<ID3D12Resource> d3D12Resource,
        out D3D12_RESOURCE_STATES d3D12ResourceStates)
    {
        if (this.allocator.Get() is not null)
        {
            allocation = this.allocator.Get()->AllocateResource(
                resourceType,
                allocationMode,
                dxgiFormat,
                width,
                height,
                out d3D12ResourceStates);

            d3D12Resource = allocation.Get()->GetD3D12Resource();
        }
        else
        {
            allocation = null;

            d3D12Resource = this.d3D12Device.Get()->CreateCommittedResource(
                resourceType,
                dxgiFormat,
                width,
                height,
                IsCacheCoherentUMA,
                out d3D12ResourceStates);
        }
    }

    /// <summary>
    /// Creates or allocates a resource for a given 3D texture type.
    /// </summary>
    /// <param name="resourceType">The resource type currently in use.</param>
    /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
    /// <param name="dxgiFormat">The <see cref="DXGI_FORMAT"/> value to use.</param>
    /// <param name="width">The width of the texture resource.</param>
    /// <param name="height">The height of the texture resource.</param>
    /// <param name="depth">The depth of the texture resource.</param>
    /// <param name="d3D12ResourceStates">The default <see cref="D3D12_RESOURCE_STATES"/> value for the resource.</param>
    /// <param name="allocation">The resulting <see cref="ID3D12Allocation"/> object, if one is used.</param>
    /// <param name="d3D12Resource">The resulting <see cref="ID3D12Resource"/> object for the 3D texture.</param>
    internal void CreateOrAllocateResource(
        ResourceType resourceType,
        AllocationMode allocationMode,
        DXGI_FORMAT dxgiFormat,
        uint width,
        uint height,
        ushort depth,
        out ComPtr<ID3D12Allocation> allocation,
        out ComPtr<ID3D12Resource> d3D12Resource,
        out D3D12_RESOURCE_STATES d3D12ResourceStates)
    {
        if (this.allocator.Get() is not null)
        {
            allocation = this.allocator.Get()->AllocateResource(
                resourceType,
                allocationMode,
                dxgiFormat,
                width,
                height,
                depth,
                out d3D12ResourceStates);

            d3D12Resource = allocation.Get()->GetD3D12Resource();
        }
        else
        {
            allocation = null;

            d3D12Resource = this.d3D12Device.Get()->CreateCommittedResource(
                resourceType,
                dxgiFormat,
                width,
                height,
                depth,
                IsCacheCoherentUMA,
                out d3D12ResourceStates);
        }
    }
}