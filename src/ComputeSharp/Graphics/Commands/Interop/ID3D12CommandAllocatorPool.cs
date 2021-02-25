using System;
using System.Collections.Generic;
using ComputeSharp.Core.Extensions;
using ComputeSharp.Graphics.Extensions;
using TerraFX.Interop;

namespace ComputeSharp.Graphics.Commands.Interop
{
    /// <summary>
    /// A type that acts as a pool to get new <see cref="ID3D12CommandAllocator"/> instances wheen needed.
    /// </summary>
    internal readonly unsafe struct ID3D12CommandAllocatorPool
    {
        /// <summary>
        /// The command list type being used by the current instance.
        /// </summary>
        private readonly D3D12_COMMAND_LIST_TYPE d3D12CommandListType;

        /// <summary>
        /// The queue of currently pooled <see cref="ID3D12CommandAllocator"/> instances.
        /// </summary>
        private readonly Queue<ComPtr<ID3D12CommandAllocator>> d3D12CommandAllocatorQueue;

        /// <summary>
        /// Creates a new <see cref="ID3D12CommandAllocatorPool"/> instance with the specified values.
        /// </summary>
        /// <param name="d3D12CommandListType">The command list type to use.</param>
        public ID3D12CommandAllocatorPool(D3D12_COMMAND_LIST_TYPE d3D12CommandListType)
        {
            this.d3D12CommandListType = d3D12CommandListType;
            this.d3D12CommandAllocatorQueue = new();
        }

        /// <summary>
        /// Adds a new <see cref="ID3D12CommandAllocator"/> item to the internal pool queue.
        /// </summary>
        /// <param name="d3D12CommandAllocator">The input <see cref="ID3D12CommandAllocator"/> to enqueue.</param>
        public void Enqueue(ComPtr<ID3D12CommandAllocator> d3D12CommandAllocator)
        {
            lock (this.d3D12CommandAllocatorQueue)
            {
                this.d3D12CommandAllocatorQueue.Enqueue(d3D12CommandAllocator);
            }
        }

        /// <summary>
        /// Gets a new or reused <see cref="ID3D12CommandAllocator"/> item to use to issue GPU commands.
        /// </summary>
        /// <returns>The rented <see cref="ID3D12CommandAllocator"/> object to use.</returns>
        public ComPtr<ID3D12CommandAllocator> GetCommandAllocator(ID3D12Device* d3D12Device, ID3D12Fence* d3D12Fence)
        {
            using ComPtr<ID3D12CommandAllocator> d3D12CommandAllocator = default;

            lock (this.d3D12CommandAllocatorQueue)
            {
                if (this.d3D12CommandAllocatorQueue.TryDequeue(out *&d3D12CommandAllocator))
                {
                    d3D12CommandAllocator.Get()->Reset().Assert();

                    return d3D12CommandAllocator.Move();
                }

                return d3D12Device->CreateCommandAllocator(this.d3D12CommandListType);
            }
        }

        /// <inheritdoc cref="IDisposable.Dispose"/>
        public void Dispose()
        {
            lock (this.d3D12CommandAllocatorQueue)
            {
                foreach (ComPtr<ID3D12CommandAllocator> d3D12CommandAllocator in this.d3D12CommandAllocatorQueue)
                {
                    d3D12CommandAllocator.Dispose();
                }

                this.d3D12CommandAllocatorQueue.Clear();
            }
        }
    }
}
