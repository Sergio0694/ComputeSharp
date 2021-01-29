using System;
using ComputeSharp.Graphics.Extensions;
using Microsoft.Toolkit.Diagnostics;
using TerraFX.Interop;
using static TerraFX.Interop.D3D12_DESCRIPTOR_HEAP_TYPE;

namespace ComputeSharp.Graphics.Commands.Interop
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
        /// The array of <see cref="D3D12DescriptorHandlePair"/> items with the available descriptor handles.
        /// </summary>
        private readonly D3D12DescriptorHandlePair[] d3D12DescriptorHandlePairs;

        /// <summary>
        /// The head index for the queue.
        /// </summary>
        private int head;

        /// <summary>
        /// The tail index for the queue.
        /// </summary>
        private int tail;

        /// <summary>
        /// The current size of the queue (ie. the number of remaining pairs in <see cref="d3D12DescriptorHandlePairs"/>).
        /// </summary>
        private int size;

        /// <summary>
        /// Creates a new <see cref="ID3D12DescriptorHandleAllocator"/> instance with the specified parameters.
        /// </summary>
        /// <param name="device">The <see cref="ID3D12Device"/> instance to use.</param>
        public ID3D12DescriptorHandleAllocator(ID3D12Device* device)
        {
            this.d3D12DescriptorHeap = device->CreateDescriptorHeap(DescriptorsPerHeap);
            this.d3D12DescriptorHandlePairs = GC.AllocateUninitializedArray<D3D12DescriptorHandlePair>((int)DescriptorsPerHeap);
            this.head = 0;
            this.tail = 0;
            this.size = (int)DescriptorsPerHeap;

            D3D12_CPU_DESCRIPTOR_HANDLE d3D12CpuDescriptorHandle = this.d3D12DescriptorHeap.Get()->GetCPUDescriptorHandleForHeapStart();
            D3D12_GPU_DESCRIPTOR_HANDLE d3D12GpuDescriptorHandleStart = this.d3D12DescriptorHeap.Get()->GetGPUDescriptorHandleForHeapStart();
            uint descriptorIncrementSize = device->GetDescriptorHandleIncrementSize(D3D12_DESCRIPTOR_HEAP_TYPE_CBV_SRV_UAV);

            for (int i = 0; i < this.d3D12DescriptorHandlePairs.Length; i++)
            {
                this.d3D12DescriptorHandlePairs[i] = new D3D12DescriptorHandlePair(
                    in d3D12CpuDescriptorHandle,
                    in d3D12GpuDescriptorHandleStart,
                    i,
                    descriptorIncrementSize);
            }
        }

        /// <summary>
        /// Gets the <see cref="ID3D12DescriptorHeap"/> in use for the current <see cref="ID3D12DescriptorHandleAllocator"/> instance.
        /// </summary>
        public ID3D12DescriptorHeap* D3D12DescriptorHeap => this.d3D12DescriptorHeap;

        /// <summary>
        /// Rents a new CPU and GPU handle pair to use in a memory buffer.
        /// </summary>
        /// <param name="d3D12CpuDescriptorHandle">The resulting <see cref="D3D12_CPU_DESCRIPTOR_HANDLE"/> value.</param>
        /// <param name="d3D12GpuDescriptorHandle">The resulting <see cref="D3D12_GPU_DESCRIPTOR_HANDLE"/> value.</param>
        public void Rent(
            out D3D12_CPU_DESCRIPTOR_HANDLE d3D12CpuDescriptorHandle,
            out D3D12_GPU_DESCRIPTOR_HANDLE d3D12GpuDescriptorHandle)
        {
            lock (this.d3D12DescriptorHandlePairs)
            {
                Guard.IsGreaterThan(this.size, 0, nameof(size));

                ref readonly D3D12DescriptorHandlePair d3D12DescriptorHandlePair = ref this.d3D12DescriptorHandlePairs[this.head++];

                d3D12CpuDescriptorHandle = d3D12DescriptorHandlePair.D3D12CpuDescriptorHandle;
                d3D12GpuDescriptorHandle = d3D12DescriptorHandlePair.D3D12GpuDescriptorhandle;

                if (this.head == DescriptorsPerHeap)
                {
                    this.head = 0;
                }

                this.size--;
            }
        }

        /// <summary>
        /// Returns a CPU and GPU handle pair for later use.
        /// </summary>
        /// <param name="d3D12CpuDescriptorHandle">The returned <see cref="D3D12_CPU_DESCRIPTOR_HANDLE"/> value.</param>
        /// <param name="d3D12GpuDescriptorHandle">The returned <see cref="D3D12_GPU_DESCRIPTOR_HANDLE"/> value.</param>
        public void Return(
            D3D12_CPU_DESCRIPTOR_HANDLE d3D12CpuDescriptorHandle,
            D3D12_GPU_DESCRIPTOR_HANDLE d3D12GpuDescriptorHandle)
        {
            lock (this.d3D12DescriptorHandlePairs)
            {
                Guard.IsLessThan(this.size, DescriptorsPerHeap, nameof(size));

                this.d3D12DescriptorHandlePairs[this.tail++] = new D3D12DescriptorHandlePair(d3D12CpuDescriptorHandle, d3D12GpuDescriptorHandle);

                if (this.tail == DescriptorsPerHeap)
                {
                    this.tail = 0;
                }

                this.size++;
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            this.d3D12DescriptorHeap.Dispose();
        }

        /// <summary>
        /// A type representing a pair of reusable descriptor handles.
        /// </summary>
        private readonly struct D3D12DescriptorHandlePair
        {
            /// <summary>
            /// The <see cref="D3D12_GPU_DESCRIPTOR_HANDLE"/> value for the current entry.
            /// </summary>
            public readonly D3D12_CPU_DESCRIPTOR_HANDLE D3D12CpuDescriptorHandle;

            /// <summary>
            /// The <see cref="D3D12_GPU_DESCRIPTOR_HANDLE"/> value for the current entry.
            /// </summary>
            public readonly D3D12_GPU_DESCRIPTOR_HANDLE D3D12GpuDescriptorhandle;

            /// <summary>
            /// Creates a new <see cref="D3D12DescriptorHandlePair"/> instance with the given parameters.
            /// </summary>
            /// <param name="d3D12CpuDescriptorHandle">The <see cref="D3D12_GPU_DESCRIPTOR_HANDLE"/> value to wrap.</param>
            /// <param name="d3D12GpuDescriptorHandle">The <see cref="D3D12_GPU_DESCRIPTOR_HANDLE"/> value to wrap.</param>
            public D3D12DescriptorHandlePair(
                D3D12_CPU_DESCRIPTOR_HANDLE d3D12CpuDescriptorHandle,
                D3D12_GPU_DESCRIPTOR_HANDLE d3D12GpuDescriptorHandle)
            {
                D3D12CpuDescriptorHandle = d3D12CpuDescriptorHandle;
                D3D12GpuDescriptorhandle = d3D12GpuDescriptorHandle;
            }

            /// <summary>
            /// Creates a new <see cref="D3D12DescriptorHandlePair"/> instance with the given parameters.
            /// </summary>
            /// <param name="d3D12CpuDescriptorHandleStart">The base <see cref="D3D12_GPU_DESCRIPTOR_HANDLE"/> value.</param>
            /// <param name="d3D12GpuDescriptorHandleStart">The base <see cref="D3D12_GPU_DESCRIPTOR_HANDLE"/> value.</param>
            /// <param name="offset">The offset for the new pair of handles to compute.</param>
            /// <param name="descriptorIncrementSize">The increment size for each consecutive handles pair.</param>
            public D3D12DescriptorHandlePair(
                in D3D12_CPU_DESCRIPTOR_HANDLE d3D12CpuDescriptorHandleStart,
                in D3D12_GPU_DESCRIPTOR_HANDLE d3D12GpuDescriptorHandleStart,
                int offset,
                uint descriptorIncrementSize)
            {
                D3D12_CPU_DESCRIPTOR_HANDLE.InitOffsetted(out D3D12CpuDescriptorHandle, in d3D12CpuDescriptorHandleStart, offset, descriptorIncrementSize);
                D3D12_GPU_DESCRIPTOR_HANDLE.InitOffsetted(out D3D12GpuDescriptorhandle, in d3D12GpuDescriptorHandleStart, offset, descriptorIncrementSize);
            }
        }
    }
}
