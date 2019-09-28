using System;
using Vortice.Direct3D12;

namespace ComputeSharp.Graphics
{
    /// <summary>
    /// A <see langword="class"/> that provides logic to create resource descriptors for a <see cref="GraphicsDevice"/> instance
    /// </summary>
    internal sealed class DescriptorAllocator : IDisposable
    {
        /// <summary>
        /// The default number of available descriptors per heap
        /// </summary>
        private const int DescriptorsPerHeap = 4096;

        /// <summary>
        /// The dummy object used to handle concurrent allocation requests
        /// </summary>
        private readonly object Lock = new object();

        /// <summary>
        /// The size of each new descriptor being allocated
        /// </summary>
        private readonly int DescriptorSize;

        /// <summary>
        /// The current <see cref="CpuDescriptorHandle"/> for the <see cref="DescriptorAllocator"/> instance in use
        /// </summary>
        private CpuDescriptorHandle _CurrentCpuHandle;

        /// <summary>
        /// The current <see cref="GpuDescriptorHandle"/> for the <see cref="DescriptorAllocator"/> instance in use
        /// </summary>
        private GpuDescriptorHandle _CurrentGpuHandle;

        /// <summary>
        /// The number of remaining handles to allocate on the current heap
        /// </summary>
        private int _RemainingHandles;

        /// <summary>
        /// Creates a new <see cref="DescriptorAllocator"/> instance with the specified parameters
        /// </summary>
        /// <param name="device">The <see cref="ID3D12Device"/> instance to use</param>
        public DescriptorAllocator(ID3D12Device device)
        {
            DescriptorSize = device.GetDescriptorHandleIncrementSize(DescriptorHeapType.ConstantBufferViewShaderResourceViewUnorderedAccessView);

            DescriptorHeapDescription descriptorHeapDescription = new DescriptorHeapDescription
            {
                DescriptorCount = DescriptorsPerHeap,
                Flags = DescriptorHeapFlags.ShaderVisible,
                Type = DescriptorHeapType.ConstantBufferViewShaderResourceViewUnorderedAccessView
            };

            DescriptorHeap = device.CreateDescriptorHeap(descriptorHeapDescription);

            _RemainingHandles = DescriptorsPerHeap;
            _CurrentCpuHandle = DescriptorHeap.GetCPUDescriptorHandleForHeapStart();
            _CurrentGpuHandle = DescriptorHeap.GetGPUDescriptorHandleForHeapStart();
        }

        /// <summary>
        /// Gets the <see cref="ID3D12DescriptorHeap"/> object in use for the current <see cref="DescriptorAllocator"/> instance
        /// </summary>
        public ID3D12DescriptorHeap DescriptorHeap { get; }

        /// <summary>
        /// Allocates a new CPU and GPU handle pair to use in a memory buffer
        /// </summary>
        /// <returns>A pair of <see cref="CpuDescriptorHandle"/> and <see cref="GpuDescriptorHandle"/> to use</returns>
        public (CpuDescriptorHandle, GpuDescriptorHandle) Allocate()
        {
            lock (Lock)
            {
                if (_RemainingHandles == 0)
                {
                    _RemainingHandles = DescriptorHeap.Description.DescriptorCount;
                    _CurrentCpuHandle = DescriptorHeap.GetCPUDescriptorHandleForHeapStart();
                    _CurrentGpuHandle = DescriptorHeap.GetGPUDescriptorHandleForHeapStart();
                }

                CpuDescriptorHandle cpuDescriptorHandle = _CurrentCpuHandle;
                GpuDescriptorHandle gpuDescriptorHandle = _CurrentGpuHandle;

                _CurrentCpuHandle += DescriptorSize;
                _CurrentGpuHandle += DescriptorSize;
                _RemainingHandles -= 1;

                return (cpuDescriptorHandle, gpuDescriptorHandle);
            }
        }

        /// <inheritdoc/>
        public void Dispose() => DescriptorHeap.Dispose();
    }
}
