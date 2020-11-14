using System;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using TerraFX.Interop;
using FX = TerraFX.Interop.Windows;

namespace ComputeSharp.Graphics.Helpers
{
    /// <summary>
    /// A <see langword="class"/> with helper methods to perform some D3D12 operations.
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
        public static ID3D12CommandQueue* CreateCommandQueue(ID3D12Device* d3d12device, D3D12_COMMAND_LIST_TYPE type)
        {
            D3D12_COMMAND_QUEUE_DESC d3d12CommandQueueDesc;
            d3d12CommandQueueDesc.Type = D3D12_COMMAND_LIST_TYPE.D3D12_COMMAND_LIST_TYPE_COMPUTE;
            d3d12CommandQueueDesc.Priority = 0;
            d3d12CommandQueueDesc.Flags = D3D12_COMMAND_QUEUE_FLAGS.D3D12_COMMAND_QUEUE_FLAG_NONE;
            d3d12CommandQueueDesc.NodeMask = 0;
            Guid d3d12CommandQueueDescGuid = FX.IID_ID3D12CommandQueue;
            ID3D12CommandQueue* commandQueue;

            int result = d3d12device->CreateCommandQueue(
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
        public static ID3D12Fence* CreateFence(ID3D12Device* d3d12device)
        {
            Guid d3D12FenceGuid = FX.IID_ID3D12Fence;
            ID3D12Fence* d3d12Fence;

            int result = d3d12device->CreateFence(
                0,
                D3D12_FENCE_FLAGS.D3D12_FENCE_FLAG_NONE,
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
        public static ID3D12DescriptorHeap* CreateDescriptorHeap(ID3D12Device* d3d12device, uint descriptorsCount)
        {
            D3D12_DESCRIPTOR_HEAP_DESC d3d12DescriptorHeapDesc;
            d3d12DescriptorHeapDesc.Type = D3D12_DESCRIPTOR_HEAP_TYPE.D3D12_DESCRIPTOR_HEAP_TYPE_CBV_SRV_UAV;
            d3d12DescriptorHeapDesc.NumDescriptors = descriptorsCount;
            d3d12DescriptorHeapDesc.Flags = D3D12_DESCRIPTOR_HEAP_FLAGS.D3D12_DESCRIPTOR_HEAP_FLAG_SHADER_VISIBLE;
            d3d12DescriptorHeapDesc.NodeMask = 0;
            Guid d3d12DescriptorHeapDescGuid = FX.IID_ID3D12DescriptorHeap;
            ID3D12DescriptorHeap* d3d12DescriptorHeap;

            int result = d3d12device->CreateDescriptorHeap(
                &d3d12DescriptorHeapDesc,
                &d3d12DescriptorHeapDescGuid,
                (void**)&d3d12DescriptorHeap);

            if (FX.FAILED(result)) Marshal.ThrowExceptionForHR(result);

            return d3d12DescriptorHeap;
        }

        [Pure]
        public static unsafe TFeature CheckFeatureSupport<TFeature>(ID3D12Device* d3d12device, D3D12_FEATURE d3d12feature)
            where TFeature : unmanaged
        {
            TFeature feature;

            int result = d3d12device->CheckFeatureSupport(d3d12feature, &feature, (uint)sizeof(TFeature));

            if (FX.FAILED(result)) Marshal.ThrowExceptionForHR(result);

            return feature;
        }
    }
}
