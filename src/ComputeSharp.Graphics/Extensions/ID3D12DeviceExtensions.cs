using System;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using TerraFX.Interop;
using static TerraFX.Interop.D3D12_COMMAND_LIST_TYPE;
using static TerraFX.Interop.D3D12_COMMAND_QUEUE_FLAGS;
using static TerraFX.Interop.D3D12_CPU_PAGE_PROPERTY;
using static TerraFX.Interop.D3D12_DESCRIPTOR_HEAP_FLAGS;
using static TerraFX.Interop.D3D12_DESCRIPTOR_HEAP_TYPE;
using static TerraFX.Interop.D3D12_FENCE_FLAGS;
using static TerraFX.Interop.D3D12_MEMORY_POOL;
using FX = TerraFX.Interop.Windows;

namespace ComputeSharp.Graphics.Extensions
{
    /// <summary>
    /// A <see langword="class"/> with extensions for the <see cref="ID3D12Device"/> type.
    /// </summary>
    internal static unsafe class D3D12Helper
    {
        /// <summary>
        /// Creates a new <see cref="ID3D12CommandQueue"/> of the specified type, for a given device.
        /// </summary>
        /// <param name="d3d12device">The target <see cref="ID3D12Device"/> to use to create the command queue.</param>
        /// <param name="type">The type of command queue to create.</param>
        /// <returns>A pointer to the newly allocated <see cref="ID3D12CommandQueue"/> instance.</returns>
        /// <exception cref="Exception">Thrown when the creation of the command queue fails.</exception>
        public static ID3D12CommandQueue* CreateCommandQueue(this ref ID3D12Device d3d12device, D3D12_COMMAND_LIST_TYPE type)
        {
            D3D12_COMMAND_QUEUE_DESC d3d12CommandQueueDesc;
            d3d12CommandQueueDesc.Type = D3D12_COMMAND_LIST_TYPE_COMPUTE;
            d3d12CommandQueueDesc.Priority = 0;
            d3d12CommandQueueDesc.Flags = D3D12_COMMAND_QUEUE_FLAG_NONE;
            d3d12CommandQueueDesc.NodeMask = 0;
            Guid d3d12CommandQueueDescGuid = FX.IID_ID3D12CommandQueue;
            ID3D12CommandQueue* commandQueue;

            int result = d3d12device.CreateCommandQueue(
                &d3d12CommandQueueDesc,
                &d3d12CommandQueueDescGuid,
                (void**)&commandQueue);

            if (FX.FAILED(result)) Marshal.ThrowExceptionForHR(result);

            return commandQueue;
        }

        /// <summary>
        /// Creates a new <see cref="ID3D12Fence"/> for a given device.
        /// </summary>
        /// <param name="d3d12device">The target <see cref="ID3D12Device"/> to use to create the fence.</param>
        /// <returns>A pointer to the newly allocated <see cref="ID3D12Fence"/> instance.</returns>
        /// <exception cref="Exception">Thrown when the creation of the command queue fails.</exception>
        public static ID3D12Fence* CreateFence(this ref ID3D12Device d3d12device)
        {
            Guid d3D12FenceGuid = FX.IID_ID3D12Fence;
            ID3D12Fence* d3d12Fence;

            int result = d3d12device.CreateFence(
                0,
                D3D12_FENCE_FLAG_NONE,
                &d3D12FenceGuid,
                (void**)&d3d12Fence);

            if (FX.FAILED(result)) Marshal.ThrowExceptionForHR(result);

            return d3d12Fence;
        }

        /// <summary>
        /// Creates a new <see cref="ID3D12DescriptorHeap"/> for a given device.
        /// </summary>
        /// <param name="d3d12device">The target <see cref="ID3D12Device"/> to use to create the descriptor heap.</param>
        /// <param name="descriptorsCount">The number of descriptors to allocate.</param>
        /// <returns>A pointer to the newly allocated <see cref="ID3D12DescriptorHeap"/> instance.</returns>
        /// <exception cref="Exception">Thrown when the creation of the command queue fails.</exception>
        public static ID3D12DescriptorHeap* CreateDescriptorHeap(this ref ID3D12Device d3d12device, uint descriptorsCount)
        {
            D3D12_DESCRIPTOR_HEAP_DESC d3d12DescriptorHeapDesc;
            d3d12DescriptorHeapDesc.Type = D3D12_DESCRIPTOR_HEAP_TYPE_CBV_SRV_UAV;
            d3d12DescriptorHeapDesc.NumDescriptors = descriptorsCount;
            d3d12DescriptorHeapDesc.Flags = D3D12_DESCRIPTOR_HEAP_FLAG_SHADER_VISIBLE;
            d3d12DescriptorHeapDesc.NodeMask = 0;
            Guid d3d12DescriptorHeapDescGuid = FX.IID_ID3D12DescriptorHeap;
            ID3D12DescriptorHeap* d3d12DescriptorHeap;

            int result = d3d12device.CreateDescriptorHeap(
                &d3d12DescriptorHeapDesc,
                &d3d12DescriptorHeapDescGuid,
                (void**)&d3d12DescriptorHeap);

            if (FX.FAILED(result)) Marshal.ThrowExceptionForHR(result);

            return d3d12DescriptorHeap;
        }

        /// <summary>
        /// Creates a new <see cref="ID3D12DescriptorHeap"/> for a given device.
        /// </summary>
        /// <param name="d3d12device">The target <see cref="ID3D12Device"/> to use to create the descriptor heap.</param>
        /// <param name="descriptorsCount">The number of descriptors to allocate.</param>
        /// <returns>A pointer to the newly allocated <see cref="ID3D12DescriptorHeap"/> instance.</returns>
        /// <exception cref="Exception">Thrown when the creation of the command queue fails.</exception>
        public static ID3D12Resource* CreateCommittedResource(
            this ref ID3D12Device d3d12device,
            D3D12_HEAP_TYPE d3D12HeapType,
            ulong width,
            D3D12_RESOURCE_FLAGS d3D12ResourceFlags,
            D3D12_RESOURCE_STATES d3D12ResourceStates)
        {
            D3D12_HEAP_PROPERTIES d3D12HeapProperties;
            d3D12HeapProperties.Type = d3D12HeapType;
            d3D12HeapProperties.CPUPageProperty = D3D12_CPU_PAGE_PROPERTY_UNKNOWN;
            d3D12HeapProperties.MemoryPoolPreference = D3D12_MEMORY_POOL_UNKNOWN;
            d3D12HeapProperties.CreationNodeMask = 1;
            d3D12HeapProperties.VisibleNodeMask = 1;
            D3D12_RESOURCE_DESC d3D12ResourceDescription = D3D12_RESOURCE_DESC.Buffer(width, d3D12ResourceFlags);
            Guid d3D12ResourceGuid = FX.IID_ID3D12Resource;
            ID3D12Resource* d3D12Resource;

            int result = d3d12device.CreateCommittedResource(
                &d3D12HeapProperties,
                D3D12_HEAP_FLAGS.D3D12_HEAP_FLAG_NONE,
                &d3D12ResourceDescription,
                d3D12ResourceStates,
                null,
                &d3D12ResourceGuid,
                (void**)&d3D12Resource);

            if (FX.FAILED(result)) Marshal.ThrowExceptionForHR(result);

            return d3D12Resource;
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

            int result = d3d12device.CheckFeatureSupport(d3d12feature, &feature, (uint)sizeof(TFeature));

            if (FX.FAILED(result)) Marshal.ThrowExceptionForHR(result);

            return feature;
        }
    }
}
