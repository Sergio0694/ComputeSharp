using ComputeSharp.Core.Extensions;
using ComputeSharp.Interop;
using Microsoft.Toolkit.Diagnostics;
using TerraFX.Interop;
using static TerraFX.Interop.D3D12_HEAP_TYPE;
using static TerraFX.Interop.D3D12_RESOURCE_FLAGS;
using static TerraFX.Interop.D3D12_RESOURCE_STATES;
using static TerraFX.Interop.D3D12MA_ALLOCATION_FLAGS;
using ResourceType = ComputeSharp.Graphics.Resources.Enums.ResourceType;

namespace ComputeSharp.Graphics.Extensions
{
    /// <summary>
    /// A <see langword="class"/> with extensions for the <see cref="D3D12MA_Allocator"/> type.
    /// </summary>
    internal static unsafe class AllocatorExtensions
    {
        /// <summary>
        /// Creates a resource for a given buffer type.
        /// </summary>
        /// <param name="allocator">The <see cref="D3D12MA_Allocator"/> instance in use.</param>
        /// <param name="resourceType">The resource type currently in use.</param>
        /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
        /// <param name="sizeInBytes">The size in bytes of the current buffer.</param>
        /// <returns>An <see cref="UniquePtr{T}"/> reference for the current <see cref="D3D12MA_Allocation"/> object.</returns>
        public static UniquePtr<D3D12MA_Allocation> CreateResource(
            this ref D3D12MA_Allocator allocator,
            ResourceType resourceType,
            AllocationMode allocationMode,
            ulong sizeInBytes)
        {
            D3D12MA_ALLOCATION_FLAGS allocationFlags = allocationMode == AllocationMode.Default ? D3D12MA_ALLOCATION_FLAG_NONE : D3D12MA_ALLOCATION_FLAG_COMMITTED;
            (D3D12_HEAP_TYPE d3D12HeapType,
             D3D12_RESOURCE_FLAGS d3D12ResourceFlags,
             D3D12_RESOURCE_STATES d3D12ResourceStates) = resourceType switch
            {
                 ResourceType.Constant => (D3D12_HEAP_TYPE_UPLOAD, D3D12_RESOURCE_FLAG_NONE, D3D12_RESOURCE_STATE_GENERIC_READ),
                 ResourceType.ReadOnly => (D3D12_HEAP_TYPE_DEFAULT, D3D12_RESOURCE_FLAG_NONE, D3D12_RESOURCE_STATE_COMMON),
                 ResourceType.ReadWrite => (D3D12_HEAP_TYPE_DEFAULT, D3D12_RESOURCE_FLAG_ALLOW_UNORDERED_ACCESS, D3D12_RESOURCE_STATE_COMMON),
                 ResourceType.ReadBack => (D3D12_HEAP_TYPE_READBACK, D3D12_RESOURCE_FLAG_NONE, D3D12_RESOURCE_STATE_COPY_DEST),
                 ResourceType.Upload => (D3D12_HEAP_TYPE_UPLOAD, D3D12_RESOURCE_FLAG_NONE, D3D12_RESOURCE_STATE_GENERIC_READ),
                 _ => ThrowHelper.ThrowArgumentException<(D3D12_HEAP_TYPE, D3D12_RESOURCE_FLAGS, D3D12_RESOURCE_STATES)>()
            };

            using UniquePtr<D3D12MA_Allocation> allocation = default;

            D3D12_RESOURCE_DESC d3D12ResourceDescription = D3D12_RESOURCE_DESC.Buffer(sizeInBytes, d3D12ResourceFlags);

            D3D12MA_ALLOCATION_DESC allocationDesc = default;
            allocationDesc.HeapType = d3D12HeapType;
            allocationDesc.Flags = allocationFlags;

            allocator.CreateResource(
                &allocationDesc,
                &d3D12ResourceDescription,
                d3D12ResourceStates,
                null,
                allocation.GetAddressOf(),
                null,
                null).Assert();

            return allocation.Move();
        }

        /// <summary>
        /// Creates a resource for a given 2D texture type.
        /// </summary>
        /// <param name="allocator">The <see cref="D3D12MA_Allocator"/> instance in use.</param>
        /// <param name="resourceType">The resource type currently in use.</param>
        /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
        /// <param name="dxgiFormat">The <see cref="DXGI_FORMAT"/> value to use.</param>
        /// <param name="width">The width of the texture resource.</param>
        /// <param name="height">The height of the texture resource.</param>
        /// <param name="d3D12ResourceStates">The default <see cref="D3D12_RESOURCE_STATES"/> value for the resource.</param>
        /// <returns>An <see cref="UniquePtr{T}"/> reference for the current <see cref="D3D12MA_Allocation"/> object.</returns>
        public static UniquePtr<D3D12MA_Allocation> CreateResource(
            this ref D3D12MA_Allocator allocator,
            ResourceType resourceType,
            AllocationMode allocationMode,
            DXGI_FORMAT dxgiFormat,
            uint width,
            uint height,
            out D3D12_RESOURCE_STATES d3D12ResourceStates)
        {
            D3D12MA_ALLOCATION_FLAGS allocationFlags = allocationMode == AllocationMode.Default ? D3D12MA_ALLOCATION_FLAG_NONE : D3D12MA_ALLOCATION_FLAG_COMMITTED;
            D3D12_RESOURCE_FLAGS d3D12ResourceFlags;

            (d3D12ResourceFlags, d3D12ResourceStates) = resourceType switch
            {
                ResourceType.ReadOnly => (D3D12_RESOURCE_FLAG_NONE, D3D12_RESOURCE_STATE_COMMON),
                ResourceType.ReadWrite => (D3D12_RESOURCE_FLAG_ALLOW_UNORDERED_ACCESS, D3D12_RESOURCE_STATE_UNORDERED_ACCESS),
                _ => ThrowHelper.ThrowArgumentException<(D3D12_RESOURCE_FLAGS, D3D12_RESOURCE_STATES)>()
            };

            using UniquePtr<D3D12MA_Allocation> allocation = default;

            D3D12_RESOURCE_DESC d3D12ResourceDescription = D3D12_RESOURCE_DESC.Tex2D(dxgiFormat, width, height, mipLevels: 1, flags: d3D12ResourceFlags);

            D3D12MA_ALLOCATION_DESC allocationDesc = default;
            allocationDesc.HeapType = D3D12_HEAP_TYPE_DEFAULT;
            allocationDesc.Flags = allocationFlags;

            allocator.CreateResource(
                &allocationDesc,
                &d3D12ResourceDescription,
                d3D12ResourceStates,
                null,
                allocation.GetAddressOf(),
                null,
                null);

            return allocation.Move();
        }

        /// <summary>
        /// Creates a resource for a given 3D texture type.
        /// </summary>
        /// <param name="allocator">The <see cref="D3D12MA_Allocator"/> instance in use.</param>
        /// <param name="resourceType">The resource type currently in use.</param>
        /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
        /// <param name="dxgiFormat">The <see cref="DXGI_FORMAT"/> value to use.</param>
        /// <param name="width">The width of the texture resource.</param>
        /// <param name="height">The height of the texture resource.</param>
        /// <param name="depth">The depth of the texture resource.</param>
        /// <param name="d3D12ResourceStates">The default <see cref="D3D12_RESOURCE_STATES"/> value for the resource.</param>
        /// <returns>An <see cref="UniquePtr{T}"/> reference for the current <see cref="D3D12MA_Allocation"/> object.</returns>
        public static UniquePtr<D3D12MA_Allocation> CreateResource(
            this ref D3D12MA_Allocator allocator,
            ResourceType resourceType,
            AllocationMode allocationMode,
            DXGI_FORMAT dxgiFormat,
            uint width,
            uint height,
            ushort depth,
            out D3D12_RESOURCE_STATES d3D12ResourceStates)
        {
            D3D12MA_ALLOCATION_FLAGS allocationFlags = allocationMode == AllocationMode.Default ? D3D12MA_ALLOCATION_FLAG_NONE : D3D12MA_ALLOCATION_FLAG_COMMITTED;
            D3D12_RESOURCE_FLAGS d3D12ResourceFlags;

            (d3D12ResourceFlags, d3D12ResourceStates) = resourceType switch
            {
                ResourceType.ReadOnly => (D3D12_RESOURCE_FLAG_NONE, D3D12_RESOURCE_STATE_COMMON),
                ResourceType.ReadWrite => (D3D12_RESOURCE_FLAG_ALLOW_UNORDERED_ACCESS, D3D12_RESOURCE_STATE_UNORDERED_ACCESS),
                _ => ThrowHelper.ThrowArgumentException<(D3D12_RESOURCE_FLAGS, D3D12_RESOURCE_STATES)>()
            };

            using UniquePtr<D3D12MA_Allocation> allocation = default;

            D3D12_RESOURCE_DESC d3D12ResourceDescription = D3D12_RESOURCE_DESC.Tex3D(dxgiFormat, width, height, depth, mipLevels: 1, flags: d3D12ResourceFlags);

            D3D12MA_ALLOCATION_DESC allocationDesc = default;
            allocationDesc.HeapType = D3D12_HEAP_TYPE_DEFAULT;
            allocationDesc.Flags = allocationFlags;

            allocator.CreateResource(
                &allocationDesc,
                &d3D12ResourceDescription,
                d3D12ResourceStates,
                null,
                allocation.GetAddressOf(),
                null,
                null);

            return allocation.Move();
        }
    }
}
