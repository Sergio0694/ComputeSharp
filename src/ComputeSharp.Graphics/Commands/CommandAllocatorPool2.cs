using ComputeSharp.Graphics.Extensions;
using ComputeSharp.Graphics.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using TerraFX.Interop;

namespace ComputeSharp.Graphics.Commands
{
    /// <summary>
    /// A type that acts as a pool to get new <see cref="ID3D12CommandAllocator"/> instances wheen needed.
    /// </summary>
    internal readonly unsafe struct CommandAllocatorPool2
    {
        /// <summary>
        /// The command list type being used by the current instance.
        /// </summary>
        private readonly D3D12_COMMAND_LIST_TYPE d3d12CommandListType;

        /// <summary>
        /// The queue of currently pooled <see cref="ID3D12CommandAllocator"/> instances.
        /// </summary>
        private readonly Queue<ID3D12CommandAllocatorEntry> d3d12CommandAllocatorQueue;

        /// <summary>
        /// Creates a new <see cref="CommandAllocatorPool2"/> instance with the specified values.
        /// </summary>
        /// <param name="d3d12CommandListType">The command list type to use.</param>
        public CommandAllocatorPool2(D3D12_COMMAND_LIST_TYPE d3d12CommandListType)
        {
            this.d3d12CommandListType = d3d12CommandListType;
            this.d3d12CommandAllocatorQueue = new();
        }

        /// <summary>
        /// Adds a new <see cref="ID3D12CommandAllocator"/> item to the internal pool queue.
        /// </summary>
        /// <param name="commandAllocator">The input <see cref="ID3D12CommandAllocator"/> to enqueue.</param>
        /// <param name="fenceValue">The fence value for the input <see cref="ID3D12CommandAllocator"/>.</param>
        public void Enqueue(ComPtr<ID3D12CommandAllocator> commandAllocator, ulong fenceValue)
        {
            lock (this.d3d12CommandAllocatorQueue)
            {
                this.d3d12CommandAllocatorQueue.Enqueue(new(commandAllocator, fenceValue));
            }
        }

        /// <summary>
        /// Gets a new or reused <see cref="ID3D12CommandAllocator"/> item to use to issue GPU commands.
        /// </summary>
        /// <returns>The rented <see cref="ID3D12CommandAllocator"/> object to use.</returns>
        public ComPtr<ID3D12CommandAllocator> GetCommandAllocator(ID3D12Device* d3D12Device, ID3D12Fence* d3D12Fence)
        {
            using ComPtr<ID3D12CommandAllocator> d3D12CommandAllocator = default;

            lock (this.d3d12CommandAllocatorQueue)
            {
                // See if there is a reusable command allocator
                if (this.d3d12CommandAllocatorQueue.Count > 0)
                {
                    ID3D12CommandAllocatorEntry d3D12CommandAllocatorEntry = this.d3d12CommandAllocatorQueue.Peek();

                    d3D12CommandAllocatorEntry.D3D12CommandAllocator.Swap(ref *&d3D12CommandAllocator);

                    // We found one that is no longer in use, reset it and return it
                    if (d3D12CommandAllocatorEntry.FenceValue <= d3D12Fence->GetCompletedValue())
                    {
                        _ = this.d3d12CommandAllocatorQueue.Dequeue();

                        int result = d3D12CommandAllocator.Get()->Reset();

                        ThrowHelper.ThrowIfFailed(result);

                        return d3D12CommandAllocator.Move();
                    }
                }

                return d3D12Device->CreateCommandAllocator(this.d3d12CommandListType);
            }
        }

        /// <inheritdoc cref="IDisposable.Dispose"/>
        public void Dispose()
        {
            lock (this.d3d12CommandAllocatorQueue)
            {
                foreach (var entry in this.d3d12CommandAllocatorQueue)
                {
                    entry.D3D12CommandAllocator.Dispose();
                }

                this.d3d12CommandAllocatorQueue.Clear();
            }
        }

        /// <summary>
        /// A type that stores info on pooled <see cref="ID3D12CommandAllocator"/> references.
        /// </summary>
        private unsafe struct ID3D12CommandAllocatorEntry
        {
            /// <summary>
            /// The current <see cref="ID3D12CommandAllocator"/> reference.
            /// </summary>
            public ComPtr<ID3D12CommandAllocator> D3D12CommandAllocator;

            /// <summary>
            /// The fence value for the current <see cref="ID3D12CommandAllocator"/> reference.
            /// </summary>
            public readonly ulong FenceValue;

            /// <summary>
            /// Creates a new <see cref="ID3D12CommandAllocatorEntry"/> instance with the specified parameters.
            /// </summary>
            /// <param name="d3D12CommandAllocator">The input <see cref="ID3D12CommandAllocator"/> reference.</param>
            /// <param name="fenceValue">The fence value for the input <see cref="ID3D12CommandAllocator"/> reference.</param>
            public ID3D12CommandAllocatorEntry(ComPtr<ID3D12CommandAllocator> d3D12CommandAllocator, ulong fenceValue)
            {
                D3D12CommandAllocator = d3D12CommandAllocator;
                FenceValue = fenceValue;
            }
        }
    }
}
