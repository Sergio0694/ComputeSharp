using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using ComputeSharp.Graphics.Commands.Abstract;
using SharpDX.Direct3D12;

namespace ComputeSharp.Graphics.Commands
{
    /// <summary>
    /// A <see langword="class"/> that acts as a pool to get new <see cref="CommandAllocator"/> instances wheen needed
    /// </summary>
    internal sealed class CommandAllocatorPool : CommandController
    {
        /// <summary>
        /// The queue of currently pooled <see cref="CommandAllocator"/> instances
        /// </summary>
        private readonly Queue<(CommandAllocator CommandAllocator, long FenceValue)> CommandAllocatorQueue = new Queue<(CommandAllocator, long)>();

        /// <inheritdoc/>
        public CommandAllocatorPool(GraphicsDevice device, CommandListType commandListType) : base(device, commandListType) { }

        /// <summary>
        /// Adds a new <see cref="CommandAllocator"/> item to the internal pool queue
        /// </summary>
        /// <param name="commandAllocator">The input <see cref="CommandAllocator"/> to enqueue</param>
        /// <param name="fenceValue">The fence value for the input <see cref="CommandAllocator"/></param>
        public void Enqueue(CommandAllocator commandAllocator, long fenceValue)
        {
            lock (CommandAllocatorQueue)
            {
                CommandAllocatorQueue.Enqueue((commandAllocator, fenceValue));
            }
        }

        /// <summary>
        /// Gets a new or reused <see cref="CommandAllocator"/> item to use to issue GPU commands
        /// </summary>
        [Pure]
        public CommandAllocator GetCommandAllocator()
        {
            lock (CommandAllocatorQueue)
            {
                if (CommandAllocatorQueue.Count > 0)
                {
                    (CommandAllocator commandAllocator, long fenceValue) = CommandAllocatorQueue.Peek();

                    long completedValue = CommandListType switch
                    {
                        CommandListType.Bundle => GraphicsDevice.NativeDirectFence.CompletedValue,
                        CommandListType.Compute => GraphicsDevice.NativeComputeFence.CompletedValue,
                        CommandListType.Copy => GraphicsDevice.NativeCopyFence.CompletedValue,
                        CommandListType.Direct => GraphicsDevice.NativeDirectFence.CompletedValue,
                        _ => throw new NotSupportedException("This command list type is not supported.")
                    };

                    if (fenceValue <= completedValue)
                    {
                        CommandAllocatorQueue.Dequeue();
                        commandAllocator.Reset();

                        return commandAllocator;
                    }
                }

                return GraphicsDevice.NativeDevice.CreateCommandAllocator(CommandListType);
            }
        }

        /// <inheritdoc/>
        public override void Dispose()
        {
            lock (CommandAllocatorQueue)
            {
                foreach ((CommandAllocator commandAllocator, long _) in CommandAllocatorQueue)
                {
                    commandAllocator.Dispose();
                }

                CommandAllocatorQueue.Clear();
            }
        }
    }
}
