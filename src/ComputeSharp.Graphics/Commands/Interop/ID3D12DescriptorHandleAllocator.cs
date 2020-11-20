using ComputeSharp.Graphics.Extensions;
using System;
using TerraFX.Interop;
using static TerraFX.Interop.D3D12_DESCRIPTOR_HEAP_TYPE;

namespace ComputeSharp.Graphics
{
    /// <summary>
    /// A type that provides logic to create resource descriptors for a <see cref="GraphicsDevice"/> instance.
    /// </summary>
    internal unsafe struct ID3D12DescriptorHandleAllocator : IDisposable
    {
        /// <summary>
        /// The default number of available descriptors per heap.
        /// </summary>
        private const uint DescriptorsPerHeap = 4096;

        /// <summary>
        /// The <see cref="ID3D12DescriptorHeap"/> in use for the current <see cref="ID3D12DescriptorHandleAllocator"/> instance.
        /// </summary>
        private ComPtr<ID3D12DescriptorHeap> d3D12DescriptorHeap;

        /// <summary>
        /// The dummy object used to handle concurrent allocation requests.
        /// </summary>
        private readonly object allocationLock;

        /// <summary>
        /// The size of each new descriptor being allocated.
        /// </summary>
        private readonly uint descriptorSize;

        /// <summary>
        /// The current <see cref="D3D12_CPU_DESCRIPTOR_HANDLE"/> for the <see cref="ID3D12DescriptorHandleAllocator"/> instance in use.
        /// </summary>
        private D3D12_CPU_DESCRIPTOR_HANDLE d3d12CpuDescriptorHandle;

        /// <summary>
        /// The current <see cref="D3D12_GPU_DESCRIPTOR_HANDLE"/> for the <see cref="ID3D12DescriptorHandleAllocator"/> instance in use.
        /// </summary>
        private D3D12_GPU_DESCRIPTOR_HANDLE d3d12GpuDescriptorHandle;

        /// <summary>
        /// The number of remaining handles to allocate on the current heap.
        /// </summary>
        private uint remainingHandles;

        /// <summary>
        /// Creates a new <see cref="ID3D12DescriptorHandleAllocator"/> instance with the specified parameters.
        /// </summary>
        /// <param name="device">The <see cref="ID3D12Device"/> instance to use</param>
        public ID3D12DescriptorHandleAllocator(ID3D12Device* device)
        {
            this.d3D12DescriptorHeap = device->CreateDescriptorHeap(DescriptorsPerHeap);
            this.allocationLock = new object();
            this.descriptorSize = device->GetDescriptorHandleIncrementSize(D3D12_DESCRIPTOR_HEAP_TYPE_CBV_SRV_UAV);
            this.d3d12CpuDescriptorHandle = this.d3D12DescriptorHeap.Get()->GetCPUDescriptorHandleForHeapStart();
            this.d3d12GpuDescriptorHandle = this.d3D12DescriptorHeap.Get()->GetGPUDescriptorHandleForHeapStart();
            this.remainingHandles = this.d3D12DescriptorHeap.Get()->GetDesc().NumDescriptors;
        }

        /// <summary>
        /// Gets the <see cref="ID3D12DescriptorHeap"/> in use for the current <see cref="ID3D12DescriptorHandleAllocator"/> instance.
        /// </summary>
        public ID3D12DescriptorHeap* D3D12DescriptorHeap => this.d3D12DescriptorHeap;

        /// <summary>
        /// Allocates a new CPU and GPU handle pair to use in a memory buffer
        /// </summary>
        /// <param name="d3d12CpuDescriptorHandle">The resulting <see cref="D3D12_CPU_DESCRIPTOR_HANDLE"/> value.</param>
        /// <param name="d3d12CpuDescriptorHandle">The resulting <see cref="D3D12_GPU_DESCRIPTOR_HANDLE"/> value.</param>
        public void Allocate(
            out D3D12_CPU_DESCRIPTOR_HANDLE d3d12CpuDescriptorHandle,
            out D3D12_GPU_DESCRIPTOR_HANDLE d3d12GpuDescriptorHandle)
        {
            lock (this.allocationLock)
            {
                if (this.remainingHandles == 0)
                {
                    this.d3d12CpuDescriptorHandle = this.d3D12DescriptorHeap.Get()->GetCPUDescriptorHandleForHeapStart();
                    this.d3d12GpuDescriptorHandle = this.d3D12DescriptorHeap.Get()->GetGPUDescriptorHandleForHeapStart();
                    this.remainingHandles = this.d3D12DescriptorHeap.Get()->GetDesc().NumDescriptors;
                }

                d3d12CpuDescriptorHandle = this.d3d12CpuDescriptorHandle;
                d3d12GpuDescriptorHandle = this.d3d12GpuDescriptorHandle;

                this.d3d12CpuDescriptorHandle.ptr += this.descriptorSize;
                this.d3d12GpuDescriptorHandle.ptr += this.descriptorSize;
                this.remainingHandles--;
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            this.d3D12DescriptorHeap.Dispose();
        }
    }
}
