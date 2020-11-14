using System;
using System.Runtime.InteropServices;
using TerraFX.Interop;
using FX = TerraFX.Interop.Windows;

namespace ComputeSharp.Graphics
{
    /// <summary>
    /// A type that provides logic to create resource descriptors for a <see cref="GraphicsDevice"/> instance.
    /// </summary>
    internal unsafe struct DescriptorAllocator2
    {
        /// <summary>
        /// The default number of available descriptors per heap.
        /// </summary>
        private const uint DescriptorsPerHeap = 4096;

        /// <summary>
        /// The <see cref="ID3D12DescriptorHeap"/> in use for the current <see cref="DescriptorAllocator2"/> instance.
        /// </summary>
        public readonly ID3D12DescriptorHeap* D3D12DescriptorHeap;

        /// <summary>
        /// The dummy object used to handle concurrent allocation requests.
        /// </summary>
        private readonly object allocationLock;

        /// <summary>
        /// The size of each new descriptor being allocated.
        /// </summary>
        private readonly uint descriptorSize;

        /// <summary>
        /// The current <see cref="D3D12_CPU_DESCRIPTOR_HANDLE"/> for the <see cref="DescriptorAllocator2"/> instance in use.
        /// </summary>
        private D3D12_CPU_DESCRIPTOR_HANDLE d3d12CpuDescriptorHandle;

        /// <summary>
        /// The current <see cref="D3D12_GPU_DESCRIPTOR_HANDLE"/> for the <see cref="DescriptorAllocator2"/> instance in use.
        /// </summary>
        private D3D12_GPU_DESCRIPTOR_HANDLE d3d12GpuDescriptorHandle;

        /// <summary>
        /// The number of remaining handles to allocate on the current heap.
        /// </summary>
        private uint remainingHandles;

        /// <summary>
        /// Creates a new <see cref="DescriptorAllocator2"/> instance with the specified parameters.
        /// </summary>
        /// <param name="device">The <see cref="ID3D12Device"/> instance to use</param>
        public DescriptorAllocator2(ID3D12Device* device)
        {
            D3D12_DESCRIPTOR_HEAP_DESC d3d12DescriptorHeapDesc;
            d3d12DescriptorHeapDesc.Type = D3D12_DESCRIPTOR_HEAP_TYPE.D3D12_DESCRIPTOR_HEAP_TYPE_CBV_SRV_UAV;
            d3d12DescriptorHeapDesc.NumDescriptors = DescriptorsPerHeap;
            d3d12DescriptorHeapDesc.Flags = D3D12_DESCRIPTOR_HEAP_FLAGS.D3D12_DESCRIPTOR_HEAP_FLAG_SHADER_VISIBLE;
            d3d12DescriptorHeapDesc.NodeMask = 0;
            Guid d3d12DescriptorHeapDescGuid = FX.IID_ID3D12DescriptorHeap;
            ID3D12DescriptorHeap* d3d12DescriptorHeap;

            int result = device->CreateDescriptorHeap(
                &d3d12DescriptorHeapDesc,
                &d3d12DescriptorHeapDescGuid,
                (void**)&d3d12DescriptorHeap);

            if (FX.FAILED(result)) Marshal.ThrowExceptionForHR(result);

            D3D12DescriptorHeap = d3d12DescriptorHeap;

            this.allocationLock = new object();
            this.descriptorSize = device->GetDescriptorHandleIncrementSize(D3D12_DESCRIPTOR_HEAP_TYPE.D3D12_DESCRIPTOR_HEAP_TYPE_CBV_SRV_UAV);
            this.d3d12CpuDescriptorHandle = d3d12DescriptorHeap->GetCPUDescriptorHandleForHeapStart();
            this.d3d12GpuDescriptorHandle = d3d12DescriptorHeap->GetGPUDescriptorHandleForHeapStart();
            this.remainingHandles = DescriptorsPerHeap;
        }

        /// <summary>
        /// Allocates a new CPU and GPU handle pair to use in a memory buffer
        /// </summary>
        public void Allocate(
            out D3D12_CPU_DESCRIPTOR_HANDLE d3d12CpuDescriptorHandle,
            out D3D12_GPU_DESCRIPTOR_HANDLE d3d12GpuDescriptorHandle)
        {
            lock (this.allocationLock)
            {
                if (this.remainingHandles == 0)
                {
                    this.remainingHandles = D3D12DescriptorHeap->GetDesc().NumDescriptors;
                    this.d3d12CpuDescriptorHandle = D3D12DescriptorHeap->GetCPUDescriptorHandleForHeapStart();
                    this.d3d12GpuDescriptorHandle = D3D12DescriptorHeap->GetGPUDescriptorHandleForHeapStart();
                }

                d3d12CpuDescriptorHandle = this.d3d12CpuDescriptorHandle;
                d3d12GpuDescriptorHandle = this.d3d12GpuDescriptorHandle;

                this.d3d12CpuDescriptorHandle.ptr += this.descriptorSize;
                this.d3d12GpuDescriptorHandle.ptr += this.descriptorSize;
                this.remainingHandles--;
            }
        }

        /// <inheritdoc cref="IDisposable.Dispose"/>
        public void Dispose()
        {
            D3D12DescriptorHeap->Release();
        }
    }
}
