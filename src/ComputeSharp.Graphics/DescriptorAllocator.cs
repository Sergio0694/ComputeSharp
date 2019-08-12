using System;
using SharpDX.Direct3D12;

namespace DirectX12GameEngine.Graphics
{
    internal sealed class DescriptorAllocator : IDisposable
    {
        private const int DescriptorsPerHeap = 4096;

        private readonly object allocatorLock = new object();
        private readonly int descriptorSize;

        private CpuDescriptorHandle currentCpuHandle;
        private GpuDescriptorHandle currentGpuHandle;
        private int remainingHandles;

        public DescriptorAllocator(GraphicsDevice device, DescriptorHeapType descriptorHeapType, DescriptorHeapFlags descriptorHeapFlags = DescriptorHeapFlags.None, int descriptorCount = DescriptorsPerHeap)
        {
            if (descriptorCount < 1 || descriptorCount > DescriptorsPerHeap)
            {
                throw new ArgumentOutOfRangeException(nameof(descriptorCount), $"Descriptor count must be between 1 and {DescriptorsPerHeap}.");
            }

            descriptorSize = device.NativeDevice.GetDescriptorHandleIncrementSize(descriptorHeapType);

            DescriptorHeapDescription rtvHeapDescription = new DescriptorHeapDescription
            {
                DescriptorCount = descriptorCount,
                Flags = descriptorHeapFlags,
                Type = descriptorHeapType
            };

            DescriptorHeap = device.NativeDevice.CreateDescriptorHeap(rtvHeapDescription);

            remainingHandles = descriptorCount;
            currentCpuHandle = DescriptorHeap.CPUDescriptorHandleForHeapStart;
            currentGpuHandle = DescriptorHeap.GPUDescriptorHandleForHeapStart;
        }

        public DescriptorHeap DescriptorHeap { get; }

        public (CpuDescriptorHandle, GpuDescriptorHandle) Allocate(int count)
        {
            if (count < 1 || (count > remainingHandles && remainingHandles != 0))
            {
                throw new ArgumentOutOfRangeException(nameof(count), "Count must be between 1 and the remaining handles if the remaining handles are not 0.");
            }

            lock (allocatorLock)
            {
                if (remainingHandles == 0)
                {
                    remainingHandles = DescriptorHeap.Description.DescriptorCount;
                    currentCpuHandle = DescriptorHeap.CPUDescriptorHandleForHeapStart;
                    currentGpuHandle = DescriptorHeap.GPUDescriptorHandleForHeapStart;
                }

                CpuDescriptorHandle cpuDescriptorHandle = currentCpuHandle;
                GpuDescriptorHandle gpuDescriptorHandle = currentGpuHandle;

                currentCpuHandle += descriptorSize * count;
                currentGpuHandle += descriptorSize * count;
                remainingHandles -= count;

                return (cpuDescriptorHandle, gpuDescriptorHandle);
            }
        }

        public (CpuDescriptorHandle, GpuDescriptorHandle) AllocateSlot(int slot)
        {
            if (slot < 0 || slot > DescriptorHeap.Description.DescriptorCount - 1)
            {
                throw new ArgumentOutOfRangeException(nameof(slot), "Slot must be between 0 and the descript count - 1.");
            }

            lock (allocatorLock)
            {
                CpuDescriptorHandle cpuDescriptorHandle = DescriptorHeap.CPUDescriptorHandleForHeapStart + descriptorSize * slot;
                GpuDescriptorHandle gpuDescriptorHandle = DescriptorHeap.GPUDescriptorHandleForHeapStart + descriptorSize * slot;

                return (cpuDescriptorHandle, gpuDescriptorHandle);
            }
        }

        public void Dispose()
        {
            DescriptorHeap.Dispose();
        }
    }
}
