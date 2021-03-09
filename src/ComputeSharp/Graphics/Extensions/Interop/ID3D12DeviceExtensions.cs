using System;
using System.Diagnostics.Contracts;
using ComputeSharp.Core.Extensions;
using ComputeSharp.Graphics.Helpers;
using Microsoft.Toolkit.Diagnostics;
using TerraFX.Interop;
using static TerraFX.Interop.D3D12_COMMAND_QUEUE_FLAGS;
using static TerraFX.Interop.D3D12_COMMAND_QUEUE_PRIORITY;
using static TerraFX.Interop.D3D12_CPU_PAGE_PROPERTY;
using static TerraFX.Interop.D3D12_DESCRIPTOR_HEAP_FLAGS;
using static TerraFX.Interop.D3D12_DESCRIPTOR_HEAP_TYPE;
using static TerraFX.Interop.D3D12_FEATURE;
using static TerraFX.Interop.D3D12_FENCE_FLAGS;
using static TerraFX.Interop.D3D12_MEMORY_POOL;
using static TerraFX.Interop.D3D12_MESSAGE_ID;
using static TerraFX.Interop.D3D12_HEAP_TYPE;
using static TerraFX.Interop.D3D12_RESOURCE_FLAGS;
using static TerraFX.Interop.D3D12_RESOURCE_STATES;
using static TerraFX.Interop.D3D12_SRV_DIMENSION;
using static TerraFX.Interop.D3D12_UAV_DIMENSION;
using FX = TerraFX.Interop.Windows;
using ResourceType = ComputeSharp.Graphics.Resources.Enums.ResourceType;

namespace ComputeSharp.Graphics.Extensions
{
    /// <summary>
    /// A <see langword="class"/> with extensions for the <see cref="ID3D12Device"/> type.
    /// </summary>
    internal static unsafe class ID3D12DeviceExtensions
    {
        /// <summary>
        /// Creates a new <see cref="ID3D12InfoQueue"/> for a given device.
        /// </summary>
        /// <param name="d3D12Device">The target <see cref="ID3D12Device"/> to use to create the info queue.</param>
        /// <returns>A pointer to the newly created <see cref="ID3D12InfoQueue"/> instance.</returns>
        /// <exception cref="Exception">Thrown when the creation of the info queue fails.</exception>
        public static ComPtr<ID3D12InfoQueue> CreateInfoQueue(this ref ID3D12Device d3D12Device)
        {
            ComPtr<ID3D12InfoQueue> d3D12InfoQueue = default;

            d3D12Device.QueryInterface(FX.__uuidof<ID3D12InfoQueue>(), d3D12InfoQueue.GetVoidAddressOf()).Assert();

            D3D12_MESSAGE_ID* d3D12MessageIds = stackalloc D3D12_MESSAGE_ID[3]
            {
                D3D12_MESSAGE_ID_CREATEDEVICE_DEBUG_LAYER_STARTUP_OPTIONS,
                D3D12_MESSAGE_ID_MAP_INVALID_NULLRANGE,
                D3D12_MESSAGE_ID_UNMAP_INVALID_NULLRANGE
            };

            D3D12_INFO_QUEUE_FILTER d3D12InfoQueueFilter = default;
            d3D12InfoQueueFilter.DenyList.NumIDs = 3;
            d3D12InfoQueueFilter.DenyList.pIDList = d3D12MessageIds;

            d3D12InfoQueue.Get()->PushRetrievalFilter(&d3D12InfoQueueFilter).Assert();

            return d3D12InfoQueue.Move();
        }

        /// <summary>
        /// Creates a new <see cref="ID3D12CommandQueue"/> of the specified type, for a given device.
        /// </summary>
        /// <param name="d3D12Device">The target <see cref="ID3D12Device"/> to use to create the command queue.</param>
        /// <param name="type">The type of command queue to create.</param>
        /// <returns>A pointer to the newly allocated <see cref="ID3D12CommandQueue"/> instance.</returns>
        /// <exception cref="Exception">Thrown when the creation of the command queue fails.</exception>
        public static ComPtr<ID3D12CommandQueue> CreateCommandQueue(this ref ID3D12Device d3D12Device, D3D12_COMMAND_LIST_TYPE type)
        {
            using ComPtr<ID3D12CommandQueue> d3D12CommandQueue = default;

            D3D12_COMMAND_QUEUE_DESC d3D12CommandQueueDesc;
            d3D12CommandQueueDesc.Type = type;
            d3D12CommandQueueDesc.Priority = (int)D3D12_COMMAND_QUEUE_PRIORITY_NORMAL;
            d3D12CommandQueueDesc.Flags = D3D12_COMMAND_QUEUE_FLAG_NONE;
            d3D12CommandQueueDesc.NodeMask = 0;

            d3D12Device.CreateCommandQueue(
                &d3D12CommandQueueDesc,
                FX.__uuidof<ID3D12CommandQueue>(),
                d3D12CommandQueue.GetVoidAddressOf()).Assert();

            return d3D12CommandQueue.Move();
        }

        /// <summary>
        /// Creates a new <see cref="ID3D12Fence"/> for a given device.
        /// </summary>
        /// <param name="d3D12Device">The target <see cref="ID3D12Device"/> to use to create the fence.</param>
        /// <returns>A pointer to the newly allocated <see cref="ID3D12Fence"/> instance.</returns>
        /// <exception cref="Exception">Thrown when the creation of the command queue fails.</exception>
        public static ComPtr<ID3D12Fence> CreateFence(this ref ID3D12Device d3D12Device)
        {
            using ComPtr<ID3D12Fence> d3D12Fence = default;

            d3D12Device.CreateFence(
                0,
                D3D12_FENCE_FLAG_NONE,
                FX.__uuidof<ID3D12Fence>(),
                d3D12Fence.GetVoidAddressOf()).Assert();

            return d3D12Fence.Move();
        }

        /// <summary>
        /// Creates a new <see cref="ID3D12DescriptorHeap"/> for a given device.
        /// </summary>
        /// <param name="d3D12Device">The target <see cref="ID3D12Device"/> to use to create the descriptor heap.</param>
        /// <param name="descriptorsCount">The number of descriptors to allocate.</param>
        /// <returns>A pointer to the newly allocated <see cref="ID3D12DescriptorHeap"/> instance.</returns>
        /// <exception cref="Exception">Thrown when the creation of the command queue fails.</exception>
        public static ComPtr<ID3D12DescriptorHeap> CreateDescriptorHeap(this ref ID3D12Device d3D12Device, uint descriptorsCount)
        {
            using ComPtr<ID3D12DescriptorHeap> d3D12DescriptorHeap = default;

            D3D12_DESCRIPTOR_HEAP_DESC d3D12DescriptorHeapDesc;
            d3D12DescriptorHeapDesc.Type = D3D12_DESCRIPTOR_HEAP_TYPE_CBV_SRV_UAV;
            d3D12DescriptorHeapDesc.NumDescriptors = descriptorsCount;
            d3D12DescriptorHeapDesc.Flags = D3D12_DESCRIPTOR_HEAP_FLAG_SHADER_VISIBLE;
            d3D12DescriptorHeapDesc.NodeMask = 0;

            d3D12Device.CreateDescriptorHeap(
                &d3D12DescriptorHeapDesc,
                FX.__uuidof<ID3D12DescriptorHeap>(),
                d3D12DescriptorHeap.GetVoidAddressOf()).Assert();

            return d3D12DescriptorHeap.Move();
        }

        /// <summary>
        /// Creates a committed resource for a given buffer type.
        /// </summary>
        /// <param name="d3D12Device">The <see cref="ID3D12Device"/> instance in use.</param>
        /// <param name="resourceType">The resource type currently in use.</param>
        /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
        /// <param name="sizeInBytes">The size in bytes of the current buffer.</param>
        /// <param name="isCacheCoherentUMA">Indicates whether or not the current device has a cache coherent UMA architecture.</param>
        /// <returns>An <see cref="ID3D12Resource"/> reference for the current buffer.</returns>
        public static ComPtr<ID3D12Resource> CreateCommittedResource(
            this ref ID3D12Device d3D12Device,
            ResourceType resourceType,
            AllocationMode allocationMode,
            ulong sizeInBytes,
            bool isCacheCoherentUMA)
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
                 _ => ThrowHelper.ThrowArgumentException<(D3D12_HEAP_TYPE, D3D12_RESOURCE_FLAGS, D3D12_RESOURCE_STATES)>()
            };

            using ComPtr<ID3D12Resource> d3D12Resource = default;

            D3D12_HEAP_PROPERTIES d3D12HeapProperties;
            d3D12HeapProperties.CreationNodeMask = 1;
            d3D12HeapProperties.VisibleNodeMask = 1;

            if (isCacheCoherentUMA)
            {
                d3D12HeapProperties.Type = D3D12_HEAP_TYPE_CUSTOM;
                d3D12HeapProperties.CPUPageProperty = D3D12_CPU_PAGE_PROPERTY_WRITE_BACK;
                d3D12HeapProperties.MemoryPoolPreference = D3D12_MEMORY_POOL_L0;
            }
            else
            {
                d3D12HeapProperties.Type = d3D12HeapType;
                d3D12HeapProperties.CPUPageProperty = D3D12_CPU_PAGE_PROPERTY_UNKNOWN;
                d3D12HeapProperties.MemoryPoolPreference = D3D12_MEMORY_POOL_UNKNOWN;
            }

            D3D12_RESOURCE_DESC d3D12ResourceDescription = D3D12_RESOURCE_DESC.Buffer(sizeInBytes, d3D12ResourceFlags);

            d3D12Device.CreateCommittedResource(
                &d3D12HeapProperties,
                D3D12FeatureHelper.GetD3D12HeapFlags(allocationMode),
                &d3D12ResourceDescription,
                d3D12ResourceStates,
                null,
                FX.__uuidof<ID3D12Resource>(),
                d3D12Resource.GetVoidAddressOf()).Assert();

            return d3D12Resource.Move();
        }

        /// <summary>
        /// Creates a committed resource for a given 2D texture type.
        /// </summary>
        /// <param name="d3D12Device">The <see cref="ID3D12Device"/> instance in use.</param>
        /// <param name="resourceType">The resource type currently in use.</param>
        /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
        /// <param name="dxgiFormat">The <see cref="DXGI_FORMAT"/> value to use.</param>
        /// <param name="width">The width of the texture resource.</param>
        /// <param name="height">The height of the texture resource.</param>
        /// <param name="isCacheCoherentUMA">Indicates whether or not the current device has a cache coherent UMA architecture.</param>
        /// <param name="d3D12ResourceStates">The default <see cref="D3D12_RESOURCE_STATES"/> value for the resource.</param>
        /// <returns>An <see cref="ID3D12Resource"/> reference for the current texture.</returns>
        public static ComPtr<ID3D12Resource> CreateCommittedResource(
            this ref ID3D12Device d3D12Device,
            ResourceType resourceType,
            AllocationMode allocationMode,
            DXGI_FORMAT dxgiFormat,
            uint width,
            uint height,
            bool isCacheCoherentUMA,
            out D3D12_RESOURCE_STATES d3D12ResourceStates)
        {
            D3D12_RESOURCE_FLAGS d3D12ResourceFlags;

            (d3D12ResourceFlags, d3D12ResourceStates) = resourceType switch
            {
                ResourceType.ReadOnly => (D3D12_RESOURCE_FLAG_NONE, D3D12_RESOURCE_STATE_COMMON),
                ResourceType.ReadWrite => (D3D12_RESOURCE_FLAG_ALLOW_UNORDERED_ACCESS, D3D12_RESOURCE_STATE_UNORDERED_ACCESS),
                _ => ThrowHelper.ThrowArgumentException<(D3D12_RESOURCE_FLAGS, D3D12_RESOURCE_STATES)>()
            };

            using ComPtr<ID3D12Resource> d3D12Resource = default;

            D3D12_HEAP_PROPERTIES d3D12HeapProperties;
            d3D12HeapProperties.CreationNodeMask = 1;
            d3D12HeapProperties.VisibleNodeMask = 1;

            if (isCacheCoherentUMA)
            {
                d3D12HeapProperties.Type = D3D12_HEAP_TYPE_CUSTOM;
                d3D12HeapProperties.CPUPageProperty = D3D12_CPU_PAGE_PROPERTY_WRITE_BACK;
                d3D12HeapProperties.MemoryPoolPreference = D3D12_MEMORY_POOL_L0;
            }
            else
            {
                d3D12HeapProperties.Type = D3D12_HEAP_TYPE_DEFAULT;
                d3D12HeapProperties.CPUPageProperty = D3D12_CPU_PAGE_PROPERTY_UNKNOWN;
                d3D12HeapProperties.MemoryPoolPreference = D3D12_MEMORY_POOL_UNKNOWN;
            }

            D3D12_RESOURCE_DESC d3D12ResourceDescription = D3D12_RESOURCE_DESC.Tex2D(dxgiFormat, width, height, mipLevels: 1, flags: d3D12ResourceFlags);

            d3D12Device.CreateCommittedResource(
                &d3D12HeapProperties,
                D3D12FeatureHelper.GetD3D12HeapFlags(allocationMode),
                &d3D12ResourceDescription,
                d3D12ResourceStates,
                null,
                FX.__uuidof<ID3D12Resource>(),
                d3D12Resource.GetVoidAddressOf()).Assert();

            return d3D12Resource.Move();
        }

        /// <summary>
        /// Creates a committed resource for a given 3D texture type.
        /// </summary>
        /// <param name="d3D12Device">The <see cref="ID3D12Device"/> instance in use.</param>
        /// <param name="resourceType">The resource type currently in use.</param>
        /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
        /// <param name="dxgiFormat">The <see cref="DXGI_FORMAT"/> value to use.</param>
        /// <param name="width">The width of the texture resource.</param>
        /// <param name="height">The height of the texture resource.</param>
        /// <param name="depth">The depth of the texture resource.</param>
        /// <param name="isCacheCoherentUMA">Indicates whether or not the current device has a cache coherent UMA architecture.</param>
        /// <param name="d3D12ResourceStates">The default <see cref="D3D12_RESOURCE_STATES"/> value for the resource.</param>
        /// <returns>An <see cref="ID3D12Resource"/> reference for the current texture.</returns>
        public static ComPtr<ID3D12Resource> CreateCommittedResource(
            this ref ID3D12Device d3D12Device,
            ResourceType resourceType,
            AllocationMode allocationMode,
            DXGI_FORMAT dxgiFormat,
            uint width,
            uint height,
            ushort depth,
            bool isCacheCoherentUMA,
            out D3D12_RESOURCE_STATES d3D12ResourceStates)
        {
            D3D12_RESOURCE_FLAGS d3D12ResourceFlags;

            (d3D12ResourceFlags, d3D12ResourceStates) = resourceType switch
            {
                ResourceType.ReadOnly => (D3D12_RESOURCE_FLAG_NONE, D3D12_RESOURCE_STATE_COMMON),
                ResourceType.ReadWrite => (D3D12_RESOURCE_FLAG_ALLOW_UNORDERED_ACCESS, D3D12_RESOURCE_STATE_UNORDERED_ACCESS),
                _ => ThrowHelper.ThrowArgumentException<(D3D12_RESOURCE_FLAGS, D3D12_RESOURCE_STATES)>()
            };

            using ComPtr<ID3D12Resource> d3D12Resource = default;

            D3D12_HEAP_PROPERTIES d3D12HeapProperties;
            d3D12HeapProperties.CreationNodeMask = 1;
            d3D12HeapProperties.VisibleNodeMask = 1;

            if (isCacheCoherentUMA)
            {
                d3D12HeapProperties.Type = D3D12_HEAP_TYPE_CUSTOM;
                d3D12HeapProperties.CPUPageProperty = D3D12_CPU_PAGE_PROPERTY_WRITE_BACK;
                d3D12HeapProperties.MemoryPoolPreference = D3D12_MEMORY_POOL_L0;
            }
            else
            {
                d3D12HeapProperties.Type = D3D12_HEAP_TYPE_DEFAULT;
                d3D12HeapProperties.CPUPageProperty = D3D12_CPU_PAGE_PROPERTY_UNKNOWN;
                d3D12HeapProperties.MemoryPoolPreference = D3D12_MEMORY_POOL_UNKNOWN;
            }

            D3D12_RESOURCE_DESC d3D12ResourceDescription = D3D12_RESOURCE_DESC.Tex3D(dxgiFormat, width, height, depth, mipLevels: 1, flags: d3D12ResourceFlags);

            d3D12Device.CreateCommittedResource(
                &d3D12HeapProperties,
                D3D12FeatureHelper.GetD3D12HeapFlags(allocationMode),
                &d3D12ResourceDescription,
                d3D12ResourceStates,
                null,
                FX.__uuidof<ID3D12Resource>(),
                d3D12Resource.GetVoidAddressOf()).Assert();

            return d3D12Resource.Move();
        }

        /// <summary>
        /// Creates a view for a constant buffer.
        /// </summary>
        /// <param name="d3D12Device">The target <see cref="ID3D12Device"/> instance in use.</param>
        /// <param name="d3D12Resource">The <see cref="ID3D12Resource"/> to create a view for.</param>
        /// <param name="bufferSize">The size of the target resource.</param>
        /// <param name="d3D12CpuDescriptorHandle">The <see cref="D3D12_CPU_DESCRIPTOR_HANDLE"/> instance for the current resource.</param>
        public static void CreateConstantBufferView(
            this ref ID3D12Device d3D12Device,
            ID3D12Resource* d3D12Resource,
            nint bufferSize,
            D3D12_CPU_DESCRIPTOR_HANDLE d3D12CpuDescriptorHandle)
        {
            uint constantBufferSize = checked((uint)((bufferSize + 255) & ~255));

            D3D12_CONSTANT_BUFFER_VIEW_DESC d3D12ConstantBufferViewDescription;
            d3D12ConstantBufferViewDescription.BufferLocation = d3D12Resource->GetGPUVirtualAddress();
            d3D12ConstantBufferViewDescription.SizeInBytes = constantBufferSize;

            d3D12Device.CreateConstantBufferView(&d3D12ConstantBufferViewDescription, d3D12CpuDescriptorHandle);
        }

        /// <summary>
        /// Creates a view for a readonly buffer.
        /// </summary>
        /// <param name="d3D12Device">The target <see cref="ID3D12Device"/> instance in use.</param>
        /// <param name="d3D12Resource">The <see cref="ID3D12Resource"/> to create a view for.</param>
        /// <param name="bufferSize">The size of the target resource.</param>
        /// <param name="elementSize">The size in byte of each item in the resource.</param>
        /// <param name="d3D12CpuDescriptorHandle">The <see cref="D3D12_CPU_DESCRIPTOR_HANDLE"/> instance for the current resource.</param>
        public static void CreateShaderResourceView(
            this ref ID3D12Device d3D12Device,
            ID3D12Resource* d3D12Resource,
            uint bufferSize,
            uint elementSize,
            D3D12_CPU_DESCRIPTOR_HANDLE d3D12CpuDescriptorHandle)
        {
            D3D12_SHADER_RESOURCE_VIEW_DESC d3D12ShaderResourceViewDescription = default;
            d3D12ShaderResourceViewDescription.ViewDimension = D3D12_SRV_DIMENSION_BUFFER;
            d3D12ShaderResourceViewDescription.Shader4ComponentMapping = FX.D3D12_DEFAULT_SHADER_4_COMPONENT_MAPPING;
            d3D12ShaderResourceViewDescription.Buffer.NumElements = bufferSize;
            d3D12ShaderResourceViewDescription.Buffer.StructureByteStride = elementSize;

            d3D12Device.CreateShaderResourceView(d3D12Resource, &d3D12ShaderResourceViewDescription, d3D12CpuDescriptorHandle);
        }

        /// <summary>
        /// Creates a view for a readonly texture.
        /// </summary>
        /// <param name="d3D12Device">The target <see cref="ID3D12Device"/> instance in use.</param>
        /// <param name="d3D12Resource">The <see cref="ID3D12Resource"/> to create a view for.</param>
        /// <param name="dxgiFormat">The <see cref="DXGI_FORMAT"/> value to use.</param>
        /// <param name="d3D12SrvDimension">The <see cref="D3D12_SRV_DIMENSION"/> value for the view to create.</param>
        /// <param name="d3D12CpuDescriptorHandle">The <see cref="D3D12_CPU_DESCRIPTOR_HANDLE"/> instance for the current resource.</param>
        public static void CreateShaderResourceView(
            this ref ID3D12Device d3D12Device,
            ID3D12Resource* d3D12Resource,
            DXGI_FORMAT dxgiFormat,
            D3D12_SRV_DIMENSION d3D12SrvDimension,
            D3D12_CPU_DESCRIPTOR_HANDLE d3D12CpuDescriptorHandle)
        {
            D3D12_SHADER_RESOURCE_VIEW_DESC d3D12ShaderResourceViewDescription = default;
            d3D12ShaderResourceViewDescription.ViewDimension = d3D12SrvDimension;
            d3D12ShaderResourceViewDescription.Format = dxgiFormat;
            d3D12ShaderResourceViewDescription.Shader4ComponentMapping = FX.D3D12_DEFAULT_SHADER_4_COMPONENT_MAPPING;
            d3D12ShaderResourceViewDescription.Texture2D.MipLevels = uint.MaxValue;

            d3D12Device.CreateShaderResourceView(d3D12Resource, &d3D12ShaderResourceViewDescription, d3D12CpuDescriptorHandle);
        }

        /// <summary>
        /// Creates a view for a buffer that can be both read and written to.
        /// </summary>
        /// <param name="d3D12Device">The target <see cref="ID3D12Device"/> instance in use.</param>
        /// <param name="d3D12Resource">The <see cref="ID3D12Resource"/> to create a view for.</param>
        /// <param name="bufferSize">The size of the target resource.</param>
        /// <param name="elementSize">The size in byte of each item in the resource.</param>
        /// <param name="d3D12CpuDescriptorHandle">The <see cref="D3D12_CPU_DESCRIPTOR_HANDLE"/> instance for the current resource.</param>
        public static void CreateUnorderedAccessView(
            this ref ID3D12Device d3D12Device,
            ID3D12Resource* d3D12Resource,
            uint bufferSize,
            uint elementSize,
            D3D12_CPU_DESCRIPTOR_HANDLE d3D12CpuDescriptorHandle)
        {
            D3D12_UNORDERED_ACCESS_VIEW_DESC d3D12UnorderedAccessViewDescription = default;
            d3D12UnorderedAccessViewDescription.ViewDimension = D3D12_UAV_DIMENSION_BUFFER;
            d3D12UnorderedAccessViewDescription.Buffer.NumElements = bufferSize;
            d3D12UnorderedAccessViewDescription.Buffer.StructureByteStride = elementSize;

            d3D12Device.CreateUnorderedAccessView(d3D12Resource, null, &d3D12UnorderedAccessViewDescription, d3D12CpuDescriptorHandle);
        }

        /// <summary>
        /// Creates a view for a texture that can be both read and written to.
        /// </summary>
        /// <param name="d3D12Device">The target <see cref="ID3D12Device"/> instance in use.</param>
        /// <param name="d3D12Resource">The <see cref="ID3D12Resource"/> to create a view for.</param>
        /// <param name="dxgiFormat">The <see cref="DXGI_FORMAT"/> value to use.</param>
        /// <param name="d3D12UavDimension">The <see cref="D3D12_UAV_DIMENSION"/> value for the view to create.</param>
        /// <param name="d3D12CpuDescriptorHandle">The <see cref="D3D12_CPU_DESCRIPTOR_HANDLE"/> instance for the current resource.</param>
        public static void CreateUnorderedAccessView(
            this ref ID3D12Device d3D12Device,
            ID3D12Resource* d3D12Resource,
            DXGI_FORMAT dxgiFormat,
            D3D12_UAV_DIMENSION d3D12UavDimension,
            D3D12_CPU_DESCRIPTOR_HANDLE d3D12CpuDescriptorHandle)
        {
            D3D12_UNORDERED_ACCESS_VIEW_DESC d3D12UnorderedAccessViewDescription = default;
            d3D12UnorderedAccessViewDescription.ViewDimension = d3D12UavDimension;
            d3D12UnorderedAccessViewDescription.Format = dxgiFormat;
            d3D12UnorderedAccessViewDescription.Texture3D.WSize = uint.MaxValue;

            d3D12Device.CreateUnorderedAccessView(d3D12Resource, null, &d3D12UnorderedAccessViewDescription, d3D12CpuDescriptorHandle);
        }

        /// <summary>
        /// Creates a new <see cref="ID3D12CommandAllocator"/> for a given device.
        /// </summary>
        /// <param name="d3D12Device">The target <see cref="ID3D12Device"/> to use to create the command allocator.</param>
        /// <param name="d3D12CommandListType">The type of command list allocator to create.</param>
        /// <returns>A pointer to the newly allocated <see cref="ID3D12CommandAllocator"/> instance.</returns>
        /// <exception cref="Exception">Thrown when the creation of the command allocator fails.</exception>
        public static ComPtr<ID3D12CommandAllocator> CreateCommandAllocator(
            this ref ID3D12Device d3D12Device,
            D3D12_COMMAND_LIST_TYPE d3D12CommandListType)
        {
            using ComPtr<ID3D12CommandAllocator> d3D12CommandAllocator = default;

            d3D12Device.CreateCommandAllocator(
                d3D12CommandListType,
                FX.__uuidof<ID3D12CommandAllocator>(),
                d3D12CommandAllocator.GetVoidAddressOf()).Assert();

            return d3D12CommandAllocator.Move();
        }

        /// <summary>
        /// Creates a new <see cref="ID3D12GraphicsCommandList"/> for a given device.
        /// </summary>
        /// <param name="d3D12Device">The target <see cref="ID3D12Device"/> to use to create the command list.</param>
        /// <param name="d3D12CommandListType">The type of command list to create.</param>
        /// <param name="d3D12CommandAllocator">The command allocator to use to create the command list.</param>
        /// <returns>A pointer to the newly allocated <see cref="ID3D12GraphicsCommandList"/> instance.</returns>
        /// <exception cref="Exception">Thrown when the creation of the command list fails.</exception>
        public static ComPtr<ID3D12GraphicsCommandList> CreateCommandList(
            this ref ID3D12Device d3D12Device,
            D3D12_COMMAND_LIST_TYPE d3D12CommandListType,
            ID3D12CommandAllocator* d3D12CommandAllocator)
        {
            using ComPtr<ID3D12GraphicsCommandList> d3D12GraphicsCommandList = default;

            d3D12Device.CreateCommandList(
                0,
                d3D12CommandListType,
                d3D12CommandAllocator,
                null,
                FX.__uuidof<ID3D12GraphicsCommandList>(),
                d3D12GraphicsCommandList.GetVoidAddressOf()).Assert();

            return d3D12GraphicsCommandList.Move();
        }

        /// <summary>
        /// Gets the layout data for a target resource.
        /// </summary>
        /// <param name="d3D12Device">The target <see cref="ID3D12Device"/> to use to get the layout info.</param>
        /// <param name="dxgiFormat">The <see cref="DXGI_FORMAT"/> value to use.</param>
        /// <param name="width">The width of the texture resource.</param>
        /// <param name="height">The height of the texture resource.</param>
        /// <param name="d3D12PlacedSubresourceFootprint">The resulting layout info for the resource.</param>
        /// <param name="rowSizeInBytes">The size in bytes of each row in the resource.</param>
        /// <param name="totalSizeInBytes">The total number of bytes for the resource.</param>
        public static void GetCopyableFootprint(
            this ref ID3D12Device d3D12Device,
            DXGI_FORMAT dxgiFormat,
            uint width,
            uint height,
            out D3D12_PLACED_SUBRESOURCE_FOOTPRINT d3D12PlacedSubresourceFootprint,
            out ulong rowSizeInBytes,
            out ulong totalSizeInBytes)
        {
            D3D12_RESOURCE_DESC d3D12ResourceDescription = D3D12_RESOURCE_DESC.Tex2D(dxgiFormat, width, height);
            ulong a, b;

            fixed (D3D12_PLACED_SUBRESOURCE_FOOTPRINT* p = &d3D12PlacedSubresourceFootprint)
            {
                uint _;

                d3D12Device.GetCopyableFootprints(&d3D12ResourceDescription, 0, 1, 0, p, &_, &a, &b);
            }

            rowSizeInBytes = a;
            totalSizeInBytes = b;
        }

        /// <summary>
        /// Gets the layout data for a target resource.
        /// </summary>
        /// <param name="d3D12Device">The target <see cref="ID3D12Device"/> to use to get the layout info.</param>
        /// <param name="dxgiFormat">The <see cref="DXGI_FORMAT"/> value to use.</param>
        /// <param name="width">The width of the texture resource.</param>
        /// <param name="height">The height of the texture resource.</param>
        /// <param name="depth">The depth of the texture resource.</param>
        /// <param name="d3D12PlacedSubresourceFootprint">The resulting layout info for the resource.</param>
        /// <param name="rowSizeInBytes">The size in bytes of each row in the resource.</param>
        /// <param name="totalSizeInBytes">The total number of bytes for the resource.</param>
        public static void GetCopyableFootprint(
            this ref ID3D12Device d3D12Device,
            DXGI_FORMAT dxgiFormat,
            uint width,
            uint height,
            ushort depth,
            out D3D12_PLACED_SUBRESOURCE_FOOTPRINT d3D12PlacedSubresourceFootprint,
            out ulong rowSizeInBytes,
            out ulong totalSizeInBytes)
        {
            D3D12_RESOURCE_DESC d3D12ResourceDescription = D3D12_RESOURCE_DESC.Tex3D(dxgiFormat, width, height, depth);
            ulong a, b;

            fixed (D3D12_PLACED_SUBRESOURCE_FOOTPRINT* p = &d3D12PlacedSubresourceFootprint)
            {
                uint _;

                d3D12Device.GetCopyableFootprints(&d3D12ResourceDescription, 0, 1, 0, p, &_, &a, &b);
            }

            rowSizeInBytes = a;
            totalSizeInBytes = b;
        }

        /// <summary>
        /// Checks the feature support of a given type for a given device.
        /// </summary>
        /// <typeparam name="TFeature">The type of feature support data to retrieve.</typeparam>
        /// <param name="d3D12Device">The target <see cref="ID3D12Device"/> to use to check features for.</param>
        /// <param name="d3D12Feature">The type of features to check.</param>
        /// <returns>A <see typeparamref="TFeature"/> value with the features data.</returns>
        [Pure]
        public static unsafe TFeature CheckFeatureSupport<TFeature>(this ref ID3D12Device d3D12Device, D3D12_FEATURE d3D12Feature)
            where TFeature : unmanaged
        {
            TFeature feature = default;

            d3D12Device.CheckFeatureSupport(d3D12Feature, &feature, (uint)sizeof(TFeature)).Assert();

            return feature;
        }

        /// <summary>
        /// Checks whether or not a given DXGI format is supported for the specified resource type.
        /// </summary>
        /// <param name="d3D12Device">The target <see cref="ID3D12Device"/> to use to check features for.</param>
        /// <param name="dxgiFormat">The <see cref="DXGI_FORMAT"/> type to check support for.</param>
        /// <param name="d3D12FormatSupport1">The resource type to check support for.</param>
        /// <returns>Whether or not the input device supports the requested format for the specified resource type.</returns>
        [Pure]
        public static unsafe bool IsDxgiFormatSupported(this ref ID3D12Device d3D12Device, DXGI_FORMAT dxgiFormat, D3D12_FORMAT_SUPPORT1 d3D12FormatSupport1)
        {
            D3D12_FEATURE_DATA_FORMAT_SUPPORT d3D12FeatureDataFormatSupport = default;
            d3D12FeatureDataFormatSupport.Format = dxgiFormat;

            d3D12Device.CheckFeatureSupport(D3D12_FEATURE_FORMAT_SUPPORT, &d3D12FeatureDataFormatSupport, (uint)sizeof(D3D12_FEATURE_DATA_FORMAT_SUPPORT)).Assert();

            return (d3D12FeatureDataFormatSupport.Support1 & d3D12FormatSupport1) == d3D12FormatSupport1;
        }

        /// <summary>
        /// Checks whether or not a given shader model is supported.
        /// </summary>
        /// <param name="d3D12Device">The target <see cref="ID3D12Device"/> to use to check features for.</param>
        /// <param name="d3DShaderModel">The <see cref="D3D_SHADER_MODEL"/> value to check support for.</param>
        /// <returns>Whether or not the input device supports the requested shader model.</returns>
        [Pure]
        public static unsafe bool IsShaderModelSupported(this ref ID3D12Device d3D12Device, D3D_SHADER_MODEL d3DShaderModel)
        {
            D3D12_FEATURE_DATA_SHADER_MODEL d3D12ShaderModel = default;
            d3D12ShaderModel.HighestShaderModel = d3DShaderModel;

            d3D12Device.CheckFeatureSupport(D3D12_FEATURE_SHADER_MODEL, &d3D12ShaderModel, (uint)sizeof(D3D12_FEATURE_DATA_SHADER_MODEL)).Assert();

            return d3D12ShaderModel.HighestShaderModel == d3DShaderModel;
        }
    }
}
