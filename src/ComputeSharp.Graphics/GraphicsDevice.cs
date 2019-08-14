using System;
using System.Threading;
using ComputeSharp.Graphics.Commands;
using SharpDX.Direct3D;
using SharpDX.Direct3D12;
using SharpDX.DXGI;
using CommandList = ComputeSharp.Graphics.Commands.CommandList;
using Device = SharpDX.Direct3D12.Device;

namespace ComputeSharp.Graphics
{
    /// <summary>
    /// A <see langword="class"/> that represents a DX12.1-compatible GPU device that can be used to run compute shaders
    /// </summary>
    public sealed class GraphicsDevice : IDisposable
    {
        /// <summary>
        /// Gets the <see cref="SharpDX.Direct3D.FeatureLevel"/> value that needs to be supported by GPUs mapped to a <see cref="GraphicsDevice"/> instance
        /// </summary>
        public const FeatureLevel FeatureLevel = SharpDX.Direct3D.FeatureLevel.Level_12_1;

        /// <summary>
        /// The <see cref="System.Threading.AutoResetEvent"/> instance used to wait for completion when executing commands
        /// </summary>
        private readonly AutoResetEvent AutoResetEvent = new AutoResetEvent(false);

        /// <summary>
        /// The <see cref="CommandQueue"/> instance to use for compute operations
        /// </summary>
        private readonly CommandQueue NativeComputeCommandQueue;

        /// <summary>
        /// The <see cref="CommandQueue"/> instance to use for copy operations
        /// </summary>
        private readonly CommandQueue NativeCopyCommandQueue;

        /// <summary>
        /// The <see cref="CommandQueue"/> instance to use for direct operations
        /// </summary>
        private readonly CommandQueue NativeDirectCommandQueue;

        /// <summary>
        /// Creates a new <see cref="GraphicsDevice"/> instance for the input <see cref="Device"/>
        /// </summary>
        /// <param name="device">The <see cref="Device"/> to use for the new <see cref="GraphicsDevice"/> instance</param>
        /// <param name="description">The available info for the new <see cref="GraphicsDevice"/> instance</param>
        public GraphicsDevice(Device device, AdapterDescription description)
        {
            NativeDevice = device;
            Description = description;
            WavefrontSize = NativeDevice.D3D12Options1.WaveLaneCountMin;

            NativeComputeCommandQueue = NativeDevice.CreateCommandQueue(new CommandQueueDescription(CommandListType.Compute));
            NativeCopyCommandQueue = NativeDevice.CreateCommandQueue(new CommandQueueDescription(CommandListType.Copy));
            NativeDirectCommandQueue = NativeDevice.CreateCommandQueue(new CommandQueueDescription(CommandListType.Direct));

            ComputeAllocatorPool = new CommandAllocatorPool(this, CommandListType.Compute);
            CopyAllocatorPool = new CommandAllocatorPool(this, CommandListType.Copy);
            DirectAllocatorPool = new CommandAllocatorPool(this, CommandListType.Direct);

            NativeComputeFence = NativeDevice.CreateFence(0, FenceFlags.None);
            NativeCopyFence = NativeDevice.CreateFence(0, FenceFlags.None);
            NativeDirectFence = NativeDevice.CreateFence(0, FenceFlags.None);

            ShaderResourceViewAllocator = new DescriptorAllocator(this);
        }

        /// <summary>
        /// Gets the available info for the current <see cref="GraphicsDevice"/> instance
        /// </summary>
        public AdapterDescription Description { get; }

        /// <summary>
        /// Gets the number of lanes in a SIMD wave on the current device (also known as "wavefront size" or "warp width")
        /// </summary>
        public int WavefrontSize { get; }

        /// <summary>
        /// Gets the <see cref="Device"/> object wrapped by the current instance
        /// </summary>
        internal Device NativeDevice { get; }

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
        /// Gets the <see cref="Fence"/> instance used for compute operations
        /// </summary>
        internal Fence NativeComputeFence { get; }

        /// <summary>
        /// Gets the <see cref="Fence"/> instance used for copy operations
        /// </summary>
        internal Fence NativeCopyFence { get; }

        /// <summary>
        /// Gets the <see cref="Fence"/> instance used for direct operations
        /// </summary>
        internal Fence NativeDirectFence { get; }

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
        /// Creates a <see cref="RootSignature"/> instance from a description
        /// </summary>
        /// <param name="rootSignatureDescription">A <see cref="RootSignatureDescription"/> instance with the info for the new <see cref="RootSignature"/> object to create</param>
        /// <returns>A new <see cref="RootSignature"/> instance to use in a compute shader</returns>
        internal RootSignature CreateRootSignature(RootSignatureDescription rootSignatureDescription)
        {
            return NativeDevice.CreateRootSignature(rootSignatureDescription.Serialize());
        }

        /// <summary>
        /// Executes a <see cref="CommandList"/> and waits for the GPU to finish processing it
        /// </summary>
        /// <param name="commandList">The input <see cref="CommandList"/> to execute</param>
        internal void ExecuteCommandList(CommandList commandList)
        {
            Fence fence = commandList.CommandListType switch
            {
                CommandListType.Direct => NativeDirectFence,
                CommandListType.Compute => NativeComputeFence,
                CommandListType.Copy => NativeCopyFence,
                _ => throw new NotSupportedException("This command list type is not supported.")
            };

            long fenceValue = GetFenceValueForCommandList(commandList);

            if (fenceValue <= fence.CompletedValue) return;

            lock (fence)
            {
                fence.SetEventOnCompletion(fenceValue, AutoResetEvent.SafeWaitHandle.DangerousGetHandle());
                AutoResetEvent.WaitOne();
            }
        }

        /// <summary>
        /// Executes a <see cref="CommandList"/> and, signals and retrieves the following fence value
        /// </summary>
        /// <param name="commandList">The input <see cref="CommandList"/> to execute</param>
        /// <returns>The value of the new fence to wait for</returns>
        private long GetFenceValueForCommandList(CommandList commandList)
        {
            CommandAllocatorPool commandAllocatorPool;
            CommandQueue commandQueue;
            Fence fence;
            long fenceValue;

            switch (commandList.CommandListType)
            {
                case CommandListType.Compute:
                    commandAllocatorPool = ComputeAllocatorPool;
                    commandQueue = NativeComputeCommandQueue;
                    fence = NativeComputeFence;
                    fenceValue = NextComputeFenceValue;

                    NextComputeFenceValue++;
                    break;
                case CommandListType.Copy:
                    commandAllocatorPool = CopyAllocatorPool;
                    commandQueue = NativeCopyCommandQueue;
                    fence = NativeCopyFence;
                    fenceValue = NextCopyFenceValue;

                    NextCopyFenceValue++;
                    break;
                case CommandListType.Direct:
                    commandAllocatorPool = DirectAllocatorPool;
                    commandQueue = NativeDirectCommandQueue;
                    fence = NativeDirectFence;
                    fenceValue = NextDirectFenceValue;

                    NextDirectFenceValue++;
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

            AutoResetEvent.Dispose();
        }
    }
}
