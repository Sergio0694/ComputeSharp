using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using ComputeSharp.Graphics.Commands.Abstract;
using SharpGen.Runtime;
using Vortice.Direct3D12;

namespace ComputeSharp.Graphics.Commands
{
    /// <summary>
    /// A <see langword="class"/> that acts as a pool to get new <see cref="ID3D12CommandAllocator"/> instances wheen needed
    /// </summary>
    internal sealed class CommandAllocatorPool : CommandController
    {
        /// <summary>
        /// The queue of currently pooled <see cref="ID3D12CommandAllocator"/> instances
        /// </summary>
        private readonly Queue<(ID3D12CommandAllocator CommandAllocator, long FenceValue)> CommandAllocatorQueue = new Queue<(ID3D12CommandAllocator, long)>();

        /// <inheritdoc/>
        public CommandAllocatorPool(GraphicsDevice device, CommandListType commandListType) : base(device, commandListType) { }

        /// <summary>
        /// Adds a new <see cref="ID3D12CommandAllocator"/> item to the internal pool queue
        /// </summary>
        /// <param name="commandAllocator">The input <see cref="ID3D12CommandAllocator"/> to enqueue</param>
        /// <param name="fenceValue">The fence value for the input <see cref="ID3D12CommandAllocator"/></param>
        public void Enqueue(ID3D12CommandAllocator commandAllocator, long fenceValue)
        {
            lock (CommandAllocatorQueue)
            {
                CommandAllocatorQueue.Enqueue((commandAllocator, fenceValue));
            }
        }

        /// <summary>
        /// Gets a new or reused <see cref="ID3D12CommandAllocator"/> item to use to issue GPU commands
        /// </summary>
        [Pure]
        public ID3D12CommandAllocator GetCommandAllocator()
        {
            lock (CommandAllocatorQueue)
            {
                // Reuse an existing command allocator, if one is present in the queue
                if (CommandAllocatorQueue.Count > 0)
                {
                    (ID3D12CommandAllocator commandAllocator, long fenceValue) = CommandAllocatorQueue.Peek();

                    long completedValue = CommandListType switch
                    {
                        CommandListType.Compute => GraphicsDevice.NativeComputeFence.CompletedValue,
                        CommandListType.Copy => GraphicsDevice.NativeCopyFence.CompletedValue,
                        CommandListType.Direct => GraphicsDevice.NativeDirectFence.CompletedValue,
                        _ => throw new NotSupportedException($"Unsupported command list type with value {CommandListType}")
                    };

                    if (fenceValue <= completedValue)
                    {
                        CommandAllocatorQueue.Dequeue();
                        commandAllocator.Reset();

                        return commandAllocator;
                    }
                }

                // Create a new command allocator
                Result result = GraphicsDevice.NativeDevice.CreateCommandAllocator(CommandListType, out ID3D12CommandAllocator? nativeCommandAllocator);

                if (result.Failure)
                {
                    throw new COMException("Failed to create the command allocator", result.Code);
                }

                return nativeCommandAllocator!;
            }
        }

        /// <inheritdoc/>
        public override void Dispose()
        {
            lock (CommandAllocatorQueue)
            {
                foreach ((ID3D12CommandAllocator commandAllocator, long _) in CommandAllocatorQueue)
                {
                    commandAllocator.Dispose();
                }

                CommandAllocatorQueue.Clear();
            }
        }
    }
}
