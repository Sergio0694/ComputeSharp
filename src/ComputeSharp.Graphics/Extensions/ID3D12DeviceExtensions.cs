using System;
using System.Diagnostics.Contracts;
using ComputeSharp.Core.Extensions;
using ComputeSharp.Graphics.Buffers.Enums;
using Microsoft.Toolkit.Diagnostics;
using TerraFX.Interop;
using static TerraFX.Interop.D3D12_COMMAND_QUEUE_FLAGS;
using static TerraFX.Interop.D3D12_COMMAND_QUEUE_PRIORITY;
using static TerraFX.Interop.D3D12_CPU_PAGE_PROPERTY;
using static TerraFX.Interop.D3D12_DESCRIPTOR_HEAP_FLAGS;
using static TerraFX.Interop.D3D12_DESCRIPTOR_HEAP_TYPE;
using static TerraFX.Interop.D3D12_FENCE_FLAGS;
using static TerraFX.Interop.D3D12_MEMORY_POOL;
using static TerraFX.Interop.D3D12_HEAP_FLAGS;
using static TerraFX.Interop.D3D12_HEAP_TYPE;
using static TerraFX.Interop.D3D12_RESOURCE_FLAGS;
using static TerraFX.Interop.D3D12_RESOURCE_STATES;
using static TerraFX.Interop.D3D12_SRV_DIMENSION;
using static TerraFX.Interop.D3D12_UAV_DIMENSION;
using FX = TerraFX.Interop.Windows;

namespace ComputeSharp.Graphics.Extensions
{
    /// <summary>
    /// A <see langword="class"/> with extensions for the <see cref="ID3D12Device"/> type.
    /// </summary>
    internal static unsafe class ID3D12DeviceExtensions
    {
        /// <summary>
        /// Creates a new <see cref="ID3D12CommandQueue"/> of the specified type, for a given device.
        /// </summary>
        /// <param name="d3d12device">The target <see cref="ID3D12Device"/> to use to create the command queue.</param>
        /// <param name="type">The type of command queue to create.</param>
        /// <returns>A pointer to the newly allocated <see cref="ID3D12CommandQueue"/> instance.</returns>
        /// <exception cref="Exception">Thrown when the creation of the command queue fails.</exception>
        public static ComPtr<ID3D12CommandQueue> CreateCommandQueue(this ref ID3D12Device d3d12device, D3D12_COMMAND_LIST_TYPE type)
        {
            using ComPtr<ID3D12CommandQueue> d3D12CommandQueue = default;

            D3D12_COMMAND_QUEUE_DESC d3d12CommandQueueDesc;
            d3d12CommandQueueDesc.Type = type;
            d3d12CommandQueueDesc.Priority = (int)D3D12_COMMAND_QUEUE_PRIORITY_NORMAL;
            d3d12CommandQueueDesc.Flags = D3D12_COMMAND_QUEUE_FLAG_NONE;
            d3d12CommandQueueDesc.NodeMask = 0;

            d3d12device.CreateCommandQueue(
                &d3d12CommandQueueDesc,
                FX.__uuidof<ID3D12CommandQueue>(),
                d3D12CommandQueue.GetVoidAddressOf()).Assert();

            return d3D12CommandQueue.Move();
        }

        /// <summary>
        /// Creates a new <see cref="ID3D12Fence"/> for a given device.
        /// </summary>
        /// <param name="d3d12device">The target <see cref="ID3D12Device"/> to use to create the fence.</param>
        /// <returns>A pointer to the newly allocated <see cref="ID3D12Fence"/> instance.</returns>
        /// <exception cref="Exception">Thrown when the creation of the command queue fails.</exception>
        public static ComPtr<ID3D12Fence> CreateFence(this ref ID3D12Device d3d12device)
        {
            using ComPtr<ID3D12Fence> d3d12Fence = default;

            d3d12device.CreateFence(
                0,
                D3D12_FENCE_FLAG_NONE,
                FX.__uuidof<ID3D12Fence>(),
                d3d12Fence.GetVoidAddressOf()).Assert();

            return d3d12Fence.Move();
        }

        /// <summary>
        /// Creates a new <see cref="ID3D12DescriptorHeap"/> for a given device.
        /// </summary>
        /// <param name="d3d12device">The target <see cref="ID3D12Device"/> to use to create the descriptor heap.</param>
        /// <param name="descriptorsCount">The number of descriptors to allocate.</param>
        /// <returns>A pointer to the newly allocated <see cref="ID3D12DescriptorHeap"/> instance.</returns>
        /// <exception cref="Exception">Thrown when the creation of the command queue fails.</exception>
        public static ComPtr<ID3D12DescriptorHeap> CreateDescriptorHeap(this ref ID3D12Device d3d12device, uint descriptorsCount)
        {
            using ComPtr<ID3D12DescriptorHeap> d3d12DescriptorHeap = default;

            D3D12_DESCRIPTOR_HEAP_DESC d3d12DescriptorHeapDesc;
            d3d12DescriptorHeapDesc.Type = D3D12_DESCRIPTOR_HEAP_TYPE_CBV_SRV_UAV;
            d3d12DescriptorHeapDesc.NumDescriptors = descriptorsCount;
            d3d12DescriptorHeapDesc.Flags = D3D12_DESCRIPTOR_HEAP_FLAG_SHADER_VISIBLE;
            d3d12DescriptorHeapDesc.NodeMask = 0;

            d3d12device.CreateDescriptorHeap(
                &d3d12DescriptorHeapDesc,
                FX.__uuidof<ID3D12DescriptorHeap>(),
                d3d12DescriptorHeap.GetVoidAddressOf()).Assert();

            return d3d12DescriptorHeap.Move();
        }

        /// <summary>
        /// Creates a committed resource for a given buffer type.
        /// </summary>
        /// <param name="d3D12Device">The <see cref="ID3D12Device"/> instance in use.</param>
        /// <param name="bufferType">The buffer type currently in use.</param>
        /// <param name="sizeInBytes">The size in bytes of the current buffer.</param>
        /// <returns>An <see cref="ID3D12Resource"/> reference for the current buffer.</returns>
        public static ComPtr<ID3D12Resource> CreateCommittedResource(
            this ref ID3D12Device d3D12Device,
            BufferType bufferType,
            ulong sizeInBytes)
        {
            (D3D12_HEAP_TYPE d3D12HeapType,
             D3D12_RESOURCE_FLAGS d3D12ResourceFlags,
             D3D12_RESOURCE_STATES d3D12ResourceStates) = bufferType switch
             {
                 BufferType.Constant => (D3D12_HEAP_TYPE_UPLOAD, D3D12_RESOURCE_FLAG_NONE, D3D12_RESOURCE_STATE_GENERIC_READ),
                 BufferType.ReadOnly => (D3D12_HEAP_TYPE_DEFAULT, D3D12_RESOURCE_FLAG_NONE, D3D12_RESOURCE_STATE_COMMON),
                 BufferType.ReadWrite => (D3D12_HEAP_TYPE_DEFAULT, D3D12_RESOURCE_FLAG_ALLOW_UNORDERED_ACCESS, D3D12_RESOURCE_STATE_COMMON),
                 BufferType.ReadBack => (D3D12_HEAP_TYPE_READBACK, D3D12_RESOURCE_FLAG_NONE, D3D12_RESOURCE_STATE_COPY_DEST),
                 BufferType.Upload => (D3D12_HEAP_TYPE_UPLOAD, D3D12_RESOURCE_FLAG_NONE, D3D12_RESOURCE_STATE_GENERIC_READ),
                 _ => ThrowHelper.ThrowArgumentException<(D3D12_HEAP_TYPE, D3D12_RESOURCE_FLAGS, D3D12_RESOURCE_STATES)>()
             };

            using ComPtr<ID3D12Resource> d3D12Resource = default;

            D3D12_HEAP_PROPERTIES d3D12HeapProperties;
            d3D12HeapProperties.Type = d3D12HeapType;
            d3D12HeapProperties.CPUPageProperty = D3D12_CPU_PAGE_PROPERTY_UNKNOWN;
            d3D12HeapProperties.MemoryPoolPreference = D3D12_MEMORY_POOL_UNKNOWN;
            d3D12HeapProperties.CreationNodeMask = 1;
            d3D12HeapProperties.VisibleNodeMask = 1;
            D3D12_RESOURCE_DESC d3D12ResourceDescription = D3D12_RESOURCE_DESC.Buffer(sizeInBytes, d3D12ResourceFlags);

            d3D12Device.CreateCommittedResource(
                &d3D12HeapProperties,
                D3D12_HEAP_FLAG_NONE,
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
        /// <param name="textureType">The texture type currently in use.</param>
        /// <param name="dxgiFormat">The <see cref="DXGI_FORMAT"/> value to use.</param>
        /// <param name="width">The width of the texture resource.</param>
        /// <param name="height">The height of the texture resource.</param>
        /// <returns>An <see cref="ID3D12Resource"/> reference for the current texture.</returns>
        public static ComPtr<ID3D12Resource> CreateCommittedResource(
            this ref ID3D12Device d3D12Device,
            TextureType textureType,
            DXGI_FORMAT dxgiFormat,
            uint width,
            uint height)
        {
            D3D12_RESOURCE_FLAGS d3D12ResourceFlags = textureType switch
            {
                TextureType.ReadOnly => D3D12_RESOURCE_FLAG_NONE,
                TextureType.ReadWrite => D3D12_RESOURCE_FLAG_ALLOW_UNORDERED_ACCESS,
                _ => ThrowHelper.ThrowArgumentException<D3D12_RESOURCE_FLAGS>()
            };

            using ComPtr<ID3D12Resource> d3D12Resource = default;

            D3D12_HEAP_PROPERTIES d3D12HeapProperties;
            d3D12HeapProperties.Type = D3D12_HEAP_TYPE_DEFAULT;
            d3D12HeapProperties.CPUPageProperty = D3D12_CPU_PAGE_PROPERTY_UNKNOWN;
            d3D12HeapProperties.MemoryPoolPreference = D3D12_MEMORY_POOL_UNKNOWN;
            d3D12HeapProperties.CreationNodeMask = 1;
            d3D12HeapProperties.VisibleNodeMask = 1;
            D3D12_RESOURCE_DESC d3D12ResourceDescription = D3D12_RESOURCE_DESC.Tex2D(dxgiFormat, width, height, flags: d3D12ResourceFlags);

            d3D12Device.CreateCommittedResource(
                &d3D12HeapProperties,
                D3D12_HEAP_FLAG_NONE,
                &d3D12ResourceDescription,
                D3D12_RESOURCE_STATE_COMMON,
                null,
                FX.__uuidof<ID3D12Resource>(),
                d3D12Resource.GetVoidAddressOf()).Assert();

            return d3D12Resource.Move();
        }

        /// <summary>
        /// Creates a view for a constant buffer.
        /// </summary>
        /// <param name="d3d12Device">The target <see cref="ID3D12Device"/> instance in use.</param>
        /// <param name="d3d12resource">The <see cref="ID3D12Resource"/> to create a view for.</param>
        /// <param name="bufferSize">The size of the target resource.</param>
        /// <param name="d3D12CpuDescriptorHandle">The <see cref="D3D12_CPU_DESCRIPTOR_HANDLE"/> instance for the current resource.</param>
        public static void CreateConstantBufferView(
            this ref ID3D12Device d3d12Device,
            ID3D12Resource* d3d12resource,
            nint bufferSize,
            D3D12_CPU_DESCRIPTOR_HANDLE d3D12CpuDescriptorHandle)
        {
            uint constantBufferSize = checked((uint)((bufferSize + 255) & ~255));

            D3D12_CONSTANT_BUFFER_VIEW_DESC d3D12ConstantBufferViewDescription;
            d3D12ConstantBufferViewDescription.BufferLocation = d3d12resource->GetGPUVirtualAddress();
            d3D12ConstantBufferViewDescription.SizeInBytes = constantBufferSize;

            d3d12Device.CreateConstantBufferView(&d3D12ConstantBufferViewDescription, d3D12CpuDescriptorHandle);
        }

        /// <summary>
        /// Creates a view for a readonly buffer.
        /// </summary>
        /// <param name="d3d12Device">The target <see cref="ID3D12Device"/> instance in use.</param>
        /// <param name="d3d12resource">The <see cref="ID3D12Resource"/> to create a view for.</param>
        /// <param name="bufferSize">The size of the target resource.</param>
        /// <param name="elementSize">The size in byte of each item in the resource.</param>
        /// <param name="d3D12CpuDescriptorHandle">The <see cref="D3D12_CPU_DESCRIPTOR_HANDLE"/> instance for the current resource.</param>
        public static void CreateShaderResourceView(
            this ref ID3D12Device d3d12Device,
            ID3D12Resource* d3d12resource,
            uint bufferSize,
            uint elementSize,
            D3D12_CPU_DESCRIPTOR_HANDLE d3D12CpuDescriptorHandle)
        {
            D3D12_SHADER_RESOURCE_VIEW_DESC d3D12ShaderResourceViewDescription = default;
            d3D12ShaderResourceViewDescription.ViewDimension = D3D12_SRV_DIMENSION_BUFFER;
            d3D12ShaderResourceViewDescription.Shader4ComponentMapping = FX.D3D12_DEFAULT_SHADER_4_COMPONENT_MAPPING;
            d3D12ShaderResourceViewDescription.Buffer.NumElements = bufferSize;
            d3D12ShaderResourceViewDescription.Buffer.StructureByteStride = elementSize;

            d3d12Device.CreateShaderResourceView(d3d12resource, &d3D12ShaderResourceViewDescription, d3D12CpuDescriptorHandle);
        }

        /// <summary>
        /// Creates a view for a readonly texture.
        /// </summary>
        /// <param name="d3d12Device">The target <see cref="ID3D12Device"/> instance in use.</param>
        /// <param name="d3d12resource">The <see cref="ID3D12Resource"/> to create a view for.</param>
        /// <param name="dxgiFormat">The <see cref="DXGI_FORMAT"/> value to use.</param>
        /// <param name="d3D12CpuDescriptorHandle">The <see cref="D3D12_CPU_DESCRIPTOR_HANDLE"/> instance for the current resource.</param>
        public static void CreateShaderResourceView(
            this ref ID3D12Device d3d12Device,
            ID3D12Resource* d3d12resource,
            DXGI_FORMAT dxgiFormat,
            D3D12_CPU_DESCRIPTOR_HANDLE d3D12CpuDescriptorHandle)
        {
            D3D12_SHADER_RESOURCE_VIEW_DESC d3D12ShaderResourceViewDescription = default;
            d3D12ShaderResourceViewDescription.ViewDimension = D3D12_SRV_DIMENSION_TEXTURE2D;
            d3D12ShaderResourceViewDescription.Format = dxgiFormat;
            d3D12ShaderResourceViewDescription.Shader4ComponentMapping = FX.D3D12_DEFAULT_SHADER_4_COMPONENT_MAPPING;

            d3d12Device.CreateShaderResourceView(d3d12resource, &d3D12ShaderResourceViewDescription, d3D12CpuDescriptorHandle);
        }

        /// <summary>
        /// Creates a view for a buffer that can be both read and written to.
        /// </summary>
        /// <param name="d3d12Device">The target <see cref="ID3D12Device"/> instance in use.</param>
        /// <param name="d3d12resource">The <see cref="ID3D12Resource"/> to create a view for.</param>
        /// <param name="bufferSize">The size of the target resource.</param>
        /// <param name="elementSize">The size in byte of each item in the resource.</param>
        /// <param name="d3D12CpuDescriptorHandle">The <see cref="D3D12_CPU_DESCRIPTOR_HANDLE"/> instance for the current resource.</param>
        public static void CreateUnorderedAccessView(
            this ref ID3D12Device d3d12Device,
            ID3D12Resource* d3d12resource,
            uint bufferSize,
            uint elementSize,
            D3D12_CPU_DESCRIPTOR_HANDLE d3D12CpuDescriptorHandle)
        {
            D3D12_UNORDERED_ACCESS_VIEW_DESC d3D12UnorderedAccessViewDescription = default;
            d3D12UnorderedAccessViewDescription.ViewDimension = D3D12_UAV_DIMENSION_BUFFER;
            d3D12UnorderedAccessViewDescription.Buffer.NumElements = bufferSize;
            d3D12UnorderedAccessViewDescription.Buffer.StructureByteStride = elementSize;

            d3d12Device.CreateUnorderedAccessView(d3d12resource, null, &d3D12UnorderedAccessViewDescription, d3D12CpuDescriptorHandle);
        }

        /// <summary>
        /// Creates a view for a texture that can be both read and written to.
        /// </summary>
        /// <param name="d3d12Device">The target <see cref="ID3D12Device"/> instance in use.</param>
        /// <param name="d3d12resource">The <see cref="ID3D12Resource"/> to create a view for.</param>
        /// <param name="dxgiFormat">The <see cref="DXGI_FORMAT"/> value to use.</param>
        /// <param name="d3D12CpuDescriptorHandle">The <see cref="D3D12_CPU_DESCRIPTOR_HANDLE"/> instance for the current resource.</param>
        public static void CreateUnorderedAccessView(
            this ref ID3D12Device d3d12Device,
            ID3D12Resource* d3d12resource,
            DXGI_FORMAT dxgiFormat,
            D3D12_CPU_DESCRIPTOR_HANDLE d3D12CpuDescriptorHandle)
        {
            D3D12_UNORDERED_ACCESS_VIEW_DESC d3D12UnorderedAccessViewDescription = default;
            d3D12UnorderedAccessViewDescription.ViewDimension = D3D12_UAV_DIMENSION_TEXTURE2D;
            d3D12UnorderedAccessViewDescription.Format = dxgiFormat;

            d3d12Device.CreateUnorderedAccessView(d3d12resource, null, &d3D12UnorderedAccessViewDescription, d3D12CpuDescriptorHandle);
        }

        /// <summary>
        /// Creates a new <see cref="ID3D12CommandAllocator"/> for a given device.
        /// </summary>
        /// <param name="d3d12device">The target <see cref="ID3D12Device"/> to use to create the command allocator.</param>
        /// <param name="d3d12CommandListType">The type of command list allocator to create.</param>
        /// <returns>A pointer to the newly allocated <see cref="ID3D12CommandAllocator"/> instance.</returns>
        /// <exception cref="Exception">Thrown when the creation of the command allocator fails.</exception>
        public static ComPtr<ID3D12CommandAllocator> CreateCommandAllocator(
            this ref ID3D12Device d3d12device,
            D3D12_COMMAND_LIST_TYPE d3d12CommandListType)
        {
            using ComPtr<ID3D12CommandAllocator> d3D12CommandAllocator = default;

            d3d12device.CreateCommandAllocator(
                d3d12CommandListType,
                FX.__uuidof<ID3D12CommandAllocator>(),
                d3D12CommandAllocator.GetVoidAddressOf()).Assert();

            return d3D12CommandAllocator.Move();
        }

        /// <summary>
        /// Creates a new <see cref="ID3D12GraphicsCommandList"/> for a given device.
        /// </summary>
        /// <param name="d3d12device">The target <see cref="ID3D12Device"/> to use to create the command list.</param>
        /// <param name="d3d12CommandListType">The type of command list to create.</param>
        /// <returns>A pointer to the newly allocated <see cref="ID3D12GraphicsCommandList"/> instance.</returns>
        /// <exception cref="Exception">Thrown when the creation of the command list fails.</exception>
        public static ComPtr<ID3D12GraphicsCommandList> CreateCommandList(
            this ref ID3D12Device d3d12device,
            D3D12_COMMAND_LIST_TYPE d3d12CommandListType,
            ID3D12CommandAllocator* d3D12CommandAllocator)
        {
            using ComPtr<ID3D12GraphicsCommandList> d3d12GraphicsCommandList = default;

            d3d12device.CreateCommandList(
                0,
                d3d12CommandListType,
                d3D12CommandAllocator,
                null,
                FX.__uuidof<ID3D12GraphicsCommandList>(),
                d3d12GraphicsCommandList.GetVoidAddressOf()).Assert();

            return d3d12GraphicsCommandList.Move();
        }

        /// <summary>
        /// Checks the feature support of a given type for a given device.
        /// </summary>
        /// <typeparam name="TFeature">The type of feature support data to retrieve.</typeparam>
        /// <param name="d3d12device">The target <see cref="ID3D12Device"/> to use to check features for.</param>
        /// <param name="d3d12feature">The type of features to check.</param>
        /// <returns>A <see typeparamref="TFeature"/> value with the features data.</returns>
        [Pure]
        public static unsafe TFeature CheckFeatureSupport<TFeature>(this ref ID3D12Device d3d12device, D3D12_FEATURE d3d12feature)
            where TFeature : unmanaged
        {
            TFeature feature;

            d3d12device.CheckFeatureSupport(d3d12feature, &feature, (uint)sizeof(TFeature)).Assert();

            return feature;
        }
    }
}
