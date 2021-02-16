using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using ComputeSharp.Exceptions;
using ComputeSharp.Graphics.Extensions;
using ComputeSharp.Interop;
using Microsoft.Toolkit.Diagnostics;
using TerraFX.Interop;
using ResourceType = ComputeSharp.Graphics.Resources.Enums.ResourceType;

namespace ComputeSharp.Resources
{
    /// <summary>
    /// A <see langword="class"/> representing a typed buffer stored on CPU memory, that can be used to transfer data to/from the GPU.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the buffer.</typeparam>
    public abstract unsafe class TransferBuffer<T> : NativeObject, IMemoryOwner<T>
        where T : unmanaged
    {
        /// <summary>
        /// The <see cref="ID3D12Resource"/> instance currently mapped.
        /// </summary>
        private ComPtr<ID3D12Resource> d3D12Resource;

        /// <summary>
        /// The pointer to the start of the mapped buffer data.
        /// </summary>
        private readonly T* mappedData;

        /// <summary>
        /// The <see cref="Allocation"/> instance used to retrieve <see cref="d3D12Resource"/>, if any.
        /// </summary>
        /// <remarks>This will be <see langword="null"/> if the owning device has <see cref="GraphicsDevice.IsCacheCoherentUMA"/> set.</remarks>
        private UniquePtr<Allocation> allocation;

        /// <summary>
        /// Creates a new <see cref="TransferBuffer{T}"/> instance with the specified parameters.
        /// </summary>
        /// <param name="device">The <see cref="GraphicsDevice"/> associated with the current instance.</param>
        /// <param name="length">The number of items to store in the current buffer.</param>
        /// <param name="resourceType">The resource type for the current buffer.</param>
        /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
        private protected TransferBuffer(GraphicsDevice device, int length, ResourceType resourceType, AllocationMode allocationMode)
        {
            device.ThrowIfDisposed();

            // The maximum length is set such that the aligned buffer size can't exceed uint.MaxValue
            Guard.IsBetweenOrEqualTo(length, 1, (uint.MaxValue / (uint)sizeof(T)) & ~255, nameof(length));

            GraphicsDevice = device;
            Length = length;

            ulong sizeInBytes = (uint)length * (uint)sizeof(T);

            if (device.IsCacheCoherentUMA)
            {
                this.d3D12Resource = device.D3D12Device->CreateCommittedResource(resourceType, allocationMode, sizeInBytes, true);
            }
            else
            {
                this.allocation = device.Allocator->CreateResource(resourceType, allocationMode, sizeInBytes);
                this.d3D12Resource = new ComPtr<ID3D12Resource>(this.allocation.Get()->GetResource());
            }

            this.mappedData = (T*)this.d3D12Resource.Get()->Map().Pointer;

            this.d3D12Resource.Get()->SetName(this);
        }

        /// <summary>
        /// Gets the <see cref="ComputeSharp.GraphicsDevice"/> associated with the current instance.
        /// </summary>
        public GraphicsDevice GraphicsDevice { get; }

        /// <summary>
        /// Gets the length of the current buffer.
        /// </summary>
        public int Length { get; }

        /// <summary>
        /// Gets the <see cref="ID3D12Resource"/> instance currently mapped.
        /// </summary>
        internal ID3D12Resource* D3D12Resource => this.d3D12Resource;

        /// <summary>
        /// Gets the pointer to the start of the mapped buffer data.
        /// </summary>
        internal T* MappedData => this.mappedData;

        /// <inheritdoc/>
        public Memory<T> Memory
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                ThrowIfDisposed();

                return new MemoryManager(this).Memory;
            }
        }

        /// <inheritdoc/>
        public Span<T> Span
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                ThrowIfDisposed();

                return new(this.mappedData, Length);
            }
        }

        /// <inheritdoc/>
        protected override bool OnDispose()
        {
            this.d3D12Resource.Dispose();
            this.allocation.Dispose();

            return true;
        }

        /// <summary>
        /// Throws a <see cref="GraphicsDeviceMismatchException"/> if the target device doesn't match the current one.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void ThrowIfDeviceMismatch(GraphicsDevice device)
        {
            if (GraphicsDevice != device)
            {
                GraphicsDeviceMismatchException.Throw(this, device);
            }
        }

        /// <summary>
        /// A <see cref="MemoryManager{T}"/> implementation wrapping a <see cref="TransferBuffer{T}"/> instance.
        /// </summary>
        private sealed class MemoryManager : MemoryManager<T>
        {
            /// <summary>
            /// The <see cref="TransferBuffer{T}"/> in use.
            /// </summary>
            private readonly TransferBuffer<T> buffer;

            /// <summary>
            /// Creates a new <see cref="MemoryManager"/> instance for a given buffer.
            /// </summary>
            /// <param name="buffer">The <see cref="TransferBuffer{T}"/> in use.</param>
            public MemoryManager(TransferBuffer<T> buffer)
            {
                this.buffer = buffer;
            }

            /// <inheritdoc/>
            public override Memory<T> Memory
            {
                get => CreateMemory(this.buffer.Length);
            }

            /// <inheritdoc/>
            public override Span<T> GetSpan()
            {
                return this.buffer.Span;
            }

            /// <inheritdoc/>
            public override MemoryHandle Pin(int elementIndex = 0)
            {
                Guard.IsEqualTo(elementIndex, 0, nameof(elementIndex));

                this.buffer.ThrowIfDisposed();

                return new(this.buffer.mappedData);
            }

            /// <inheritdoc/>
            public override void Unpin()
            {
            }

            /// <inheritdoc/>
            protected override void Dispose(bool disposing)
            {
            }
        }
    }
}
