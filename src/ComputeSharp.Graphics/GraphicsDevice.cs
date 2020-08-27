using System;
using ComputeSharp.Graphics.Commands;
using Vortice.Direct3D12;
using Vortice.DXGI;
using CommandList = ComputeSharp.Graphics.Commands.CommandList;
using Direct3DFeatureLevel = Vortice.Direct3D.FeatureLevel;

namespace ComputeSharp.Graphics
{
    /// <summary>
    /// A <see langword="class"/> that represents a DX12.1-compatible GPU device that can be used to run compute shaders
    /// </summary>
    public sealed class GraphicsDevice : IDisposable
    {
        /// <summary>
        /// Gets the <see cref="FeatureLevel"/> value that needs to be supported by GPUs mapped to a <see cref="GraphicsDevice"/> instance
        /// </summary>
        public const Direct3DFeatureLevel FeatureLevel = Direct3DFeatureLevel.Level_12_1;

        /// <summary>
        /// The <see cref="ID3D12CommandQueue"/> instance to use for compute operations
        /// </summary>
        private readonly ID3D12CommandQueue NativeComputeCommandQueue;

        /// <summary>
        /// The <see cref="ID3D12CommandQueue"/> instance to use for copy operations
        /// </summary>
        private readonly ID3D12CommandQueue NativeCopyCommandQueue;

        /// <summary>
        /// The <see cref="ID3D12CommandQueue"/> instance to use for direct operations
        /// </summary>
        private readonly ID3D12CommandQueue NativeDirectCommandQueue;

        /// <summary>
        /// Creates a new <see cref="GraphicsDevice"/> instance for the input <see cref="ID3D12Device"/>
        /// </summary>
        /// <param name="device">The <see cref="ID3D12Device"/> to use for the new <see cref="GraphicsDevice"/> instance</param>
        /// <param name="description">The available info for the new <see cref="GraphicsDevice"/> instance</param>
        public GraphicsDevice(ID3D12Device device, AdapterDescription description)
        {
            NativeDevice = device;
            Name = description.Description;
            MemorySize = description.DedicatedVideoMemory;
            ComputeUnits = device.Options1.TotalLaneCount;
            WavefrontSize = device.Options1.WaveLaneCountMin;

            NativeComputeCommandQueue = NativeDevice.CreateCommandQueue(new CommandQueueDescription(CommandListType.Compute));
            NativeCopyCommandQueue = NativeDevice.CreateCommandQueue(new CommandQueueDescription(CommandListType.Copy));
            NativeDirectCommandQueue = NativeDevice.CreateCommandQueue(new CommandQueueDescription(CommandListType.Direct));

            ComputeAllocatorPool = new CommandAllocatorPool(this, CommandListType.Compute);
            CopyAllocatorPool = new CommandAllocatorPool(this, CommandListType.Copy);
            DirectAllocatorPool = new CommandAllocatorPool(this, CommandListType.Direct);

            NativeComputeFence = NativeDevice.CreateFence(0);
            NativeCopyFence = NativeDevice.CreateFence(0);
            NativeDirectFence = NativeDevice.CreateFence(0);

            ShaderResourceViewAllocator = new DescriptorAllocator(NativeDevice);
        }

        /// <summary>
        /// Gets the name of the current <see cref="GraphicsDevice"/> instance
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the size of the dedicated video memory for the current <see cref="GraphicsDevice"/> instance
        /// </summary>
        public long MemorySize { get; }

        /// <summary>
        /// Gets the number of total lanes on the current device (eg. CUDA cores on an nVidia GPU)
        /// </summary>
        public int ComputeUnits { get; }

        /// <summary>
        /// Gets the number of lanes in a SIMD wave on the current device (also known as "wavefront size" or "warp width")
        /// </summary>
        public int WavefrontSize { get; }

        /// <summary>
        /// Gets the <see cref="ID3D12Device"/> object wrapped by the current instance
        /// </summary>
        internal ID3D12Device NativeDevice { get; }

        /// <summary>
        /// Gets the <see cref="DescriptorAllocator"/> object for the current instance, used when allocating new buffers
        /// </summary>
        internal DescriptorAllocator ShaderResourceViewAllocator { get; set; }

        /// <summary>
        /// Gets the <see cref="CommandAllocatorPool"/> instance for compute operations
        /// </summary>
        internal CommandAllocatorPool ComputeAllocatorPool { get; }

        /// <summary>
        /// Gets the <see cref="CommandAllocatorPool"/> instance for copy operations
        /// </summary>
        internal CommandAllocatorPool CopyAllocatorPool { get; }

        /// <summary>
        /// Gets the <see cref="CommandAllocatorPool"/> instance for direct operations
        /// </summary>
        internal CommandAllocatorPool DirectAllocatorPool { get; }

        /// <summary>
        /// Gets the <see cref="ID3D12Fence"/> instance used for compute operations
        /// </summary>
        internal ID3D12Fence NativeComputeFence { get; }

        /// <summary>
        /// Gets the <see cref="ID3D12Fence"/> instance used for copy operations
        /// </summary>
        internal ID3D12Fence NativeCopyFence { get; }

        /// <summary>
        /// Gets the <see cref="ID3D12Fence"/> instance used for direct operations
        /// </summary>
        internal ID3D12Fence NativeDirectFence { get; }

        /// <summary>
        /// Gets the next fence value for compute operations
        /// </summary>
        internal long NextComputeFenceValue { get; private set; } = 1;

        /// <summary>
        /// Gets the next fence value for copy operations
        /// </summary>
        internal long NextCopyFenceValue { get; private set; } = 1;

        /// <summary>
        /// Gets the next fence value for direct operations
        /// </summary>
        internal long NextDirectFenceValue { get; private set; } = 1;

        /// <summary>
        /// Creates a <see cref="ID3D12RootSignature"/> instance from a description
        /// </summary>
        /// <param name="rootSignatureDescription">A <see cref="VersionedRootSignatureDescription"/> instance with the info for the new <see cref="ID3D12RootSignature"/> object to create</param>
        /// <returns>A new <see cref="ID3D12RootSignature"/> instance to use in a compute shader</returns>
        internal ID3D12RootSignature CreateRootSignature(VersionedRootSignatureDescription rootSignatureDescription)
        {
            return NativeDevice.CreateRootSignature(0, rootSignatureDescription);
        }

        /// <summary>
        /// Executes a <see cref="CommandList"/> and waits for the GPU to finish processing it
        /// </summary>
        /// <param name="commandList">The input <see cref="CommandList"/> to execute</param>
        internal void ExecuteCommandList(CommandList commandList)
        {
            ID3D12Fence fence = commandList.CommandListType switch
            {
                CommandListType.Direct => NativeDirectFence,
                CommandListType.Compute => NativeComputeFence,
                CommandListType.Copy => NativeCopyFence,
                _ => throw new NotSupportedException("This command list type is not supported.")
            };

            long fenceValue = GetFenceValueForCommandList(commandList);

            if (fenceValue <= fence.CompletedValue) return;

            fence.SetEventOnCompletion(fenceValue, default(IntPtr));
        }

        /// <summary>
        /// Executes a <see cref="CommandList"/> and, signals and retrieves the following fence value
        /// </summary>
        /// <param name="commandList">The input <see cref="CommandList"/> to execute</param>
        /// <returns>The value of the new fence to wait for</returns>
        private long GetFenceValueForCommandList(CommandList commandList)
        {
            CommandAllocatorPool commandAllocatorPool;
            ID3D12CommandQueue commandQueue;
            ID3D12Fence fence;
            long fenceValue;

            switch (commandList.CommandListType)
            {
                case CommandListType.Compute:
                    commandAllocatorPool = ComputeAllocatorPool;
                    commandQueue = NativeComputeCommandQueue;
                    fence = NativeComputeFence;
                    fenceValue = NextComputeFenceValue++;
                    break;
                case CommandListType.Copy:
                    commandAllocatorPool = CopyAllocatorPool;
                    commandQueue = NativeCopyCommandQueue;
                    fence = NativeCopyFence;
                    fenceValue = NextCopyFenceValue++;
                    break;
                case CommandListType.Direct:
                    commandAllocatorPool = DirectAllocatorPool;
                    commandQueue = NativeDirectCommandQueue;
                    fence = NativeDirectFence;
                    fenceValue = NextDirectFenceValue++;
                    break;
                default: throw new NotSupportedException($"Unsupported command list of type {commandList.CommandListType}");
            }

            commandAllocatorPool.Enqueue(commandList.CommandAllocator, fenceValue);
            commandQueue.ExecuteCommandLists(commandList.NativeCommandList);
            commandQueue.Signal(fence, fenceValue);

            return fenceValue;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            NativeDirectCommandQueue.Signal(NativeDirectFence, NextDirectFenceValue);
            NativeDirectCommandQueue.Wait(NativeDirectFence, NextDirectFenceValue);

            ShaderResourceViewAllocator.Dispose();

            ComputeAllocatorPool.Dispose();
            CopyAllocatorPool.Dispose();
            DirectAllocatorPool.Dispose();

            NativeComputeCommandQueue.Dispose();
            NativeCopyCommandQueue.Dispose();
            NativeDirectCommandQueue.Dispose();

            NativeComputeFence.Dispose();
            NativeDirectFence.Dispose();
            NativeDirectFence.Dispose();

            NativeDevice.Dispose();
        }
    }
}
