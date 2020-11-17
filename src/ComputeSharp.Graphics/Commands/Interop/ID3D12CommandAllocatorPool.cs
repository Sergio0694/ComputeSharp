using ComputeSharp.Graphics.Extensions;
using ComputeSharp.Graphics.Helpers;
using System;
using System.Collections.Generic;
using TerraFX.Interop;

namespace ComputeSharp.Graphics.Commands
{
    /// <summary>
    /// A type that acts as a pool to get new <see cref="ID3D12CommandAllocator"/> instances wheen needed.
    /// </summary>
    internal readonly unsafe struct ID3D12CommandAllocatorPool
    {
        /// <summary>
        /// The command list type being used by the current instance.
        /// </summary>
        private readonly D3D12_COMMAND_LIST_TYPE d3d12CommandListType;

        /// <summary>
        /// The queue of currently pooled <see cref="ID3D12CommandAllocator"/> instances.
        /// </summary>
        private readonly Queue<ComPtr<ID3D12CommandAllocator>> d3d12CommandAllocatorQueue;

        /// <summary>
        /// Creates a new <see cref="ID3D12CommandAllocatorPool"/> instance with the specified values.
        /// </summary>
        /// <param name="d3d12CommandListType">The command list type to use.</param>
        public ID3D12CommandAllocatorPool(D3D12_COMMAND_LIST_TYPE d3d12CommandListType)
        {
            this.d3d12CommandListType = d3d12CommandListType;
            this.d3d12CommandAllocatorQueue = new();
        }

        /// <summary>
        /// Adds a new <see cref="ID3D12CommandAllocator"/> item to the internal pool queue.
        /// </summary>
        /// <param name="d3D12CommandAllocator">The input <see cref="ID3D12CommandAllocator"/> to enqueue.</param>
        public void Enqueue(ComPtr<ID3D12CommandAllocator> d3D12CommandAllocator)
        {
            lock (this.d3d12CommandAllocatorQueue)
            {
                this.d3d12CommandAllocatorQueue.Enqueue(d3D12CommandAllocator);
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
                if (this.d3d12CommandAllocatorQueue.TryDequeue(out *&d3D12CommandAllocator))
                {
                    int result = d3D12CommandAllocator.Get()->Reset();

                    ThrowHelper.ThrowIfFailed(result);

                    return d3D12CommandAllocator.Move();
                }

                return d3D12Device->CreateCommandAllocator(this.d3d12CommandListType);
            }
        }

        /// <inheritdoc cref="IDisposable.Dispose"/>
        public void Dispose()
        {
            lock (this.d3d12CommandAllocatorQueue)
            {
                foreach (ComPtr<ID3D12CommandAllocator> d3D12CommandAllocator in this.d3d12CommandAllocatorQueue)
                {
                    d3D12CommandAllocator.Dispose();
                }

                this.d3d12CommandAllocatorQueue.Clear();
            }
        }
    }
}
