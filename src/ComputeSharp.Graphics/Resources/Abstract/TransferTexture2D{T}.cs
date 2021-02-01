using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using ComputeSharp.Exceptions;
using ComputeSharp.Graphics.Extensions;
using ComputeSharp.Graphics.Helpers;
using ComputeSharp.Graphics.Resources.Enums;
using ComputeSharp.Interop;
using Microsoft.Toolkit.Diagnostics;
using Microsoft.Toolkit.HighPerformance.Memory;
using TerraFX.Interop;
using FX = TerraFX.Interop.Windows;
using static TerraFX.Interop.D3D12_FORMAT_SUPPORT1;

namespace ComputeSharp.Resources
{
    /// <summary>
    /// A <see langword="class"/> representing a typed 2D texture stored on on CPU memory, that can be used to transfer data to/from the GPU.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    public unsafe abstract class TransferTexture2D<T> : NativeObject
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
        /// The pitch size for each row within <see cref="mappedData"/>.
        /// </summary>
        private readonly int rowPitch;

        /// <summary>
        /// Creates a new <see cref="TransferTexture2D{T}"/> instance with the specified parameters.
        /// </summary>
        /// <param name="device">The <see cref="ComputeSharp.GraphicsDevice"/> associated with the current instance.</param>
        /// <param name="height">The height of the texture.</param>
        /// <param name="width">The width of the texture.</param>
        /// <param name="resourceType">The resource type for the current texture.</param>
        /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
        private protected TransferTexture2D(GraphicsDevice device, int width, int height, ResourceType resourceType, AllocationMode allocationMode)
        {
            device.ThrowIfDisposed();

            Guard.IsBetweenOrEqualTo(width, 1, FX.D3D12_REQ_TEXTURE2D_U_OR_V_DIMENSION, nameof(width));
            Guard.IsBetweenOrEqualTo(height, 1, FX.D3D12_REQ_TEXTURE2D_U_OR_V_DIMENSION, nameof(height));

            if (!device.D3D12Device->IsDxgiFormatSupported(DXGIFormatHelper.GetForType<T>(), D3D12_FORMAT_SUPPORT1_TEXTURE2D))
            {
                UnsupportedTextureTypeException.ThrowForTexture2D<T>();
            }

            GraphicsDevice = device;
            Width = width;
            Height = height;

            D3D12_RESOURCE_DESC d3D12ResourceDescription = D3D12_RESOURCE_DESC.Tex2D(DXGIFormatHelper.GetForType<T>(), (ulong)width, (uint)height);

            GraphicsDevice.D3D12Device->GetCopyableFootprint(
                &d3D12ResourceDescription,
                out D3D12_PLACED_SUBRESOURCE_FOOTPRINT d3D12PlacedSubresourceFootprint,
                out ulong rowSizeInBytes,
                out ulong totalSizeInBytes);

            this.d3D12Resource = GraphicsDevice.D3D12Device->CreateCommittedResource(resourceType, allocationMode, totalSizeInBytes, GraphicsDevice.IsCacheCoherentUMA);
            this.mappedData = (T*)this.d3D12Resource.Get()->Map().Pointer;
            this.rowPitch = (int)((d3D12PlacedSubresourceFootprint.Footprint.RowPitch - rowSizeInBytes) / (uint)sizeof(T));
        }

        /// <summary>
        /// Gets the <see cref="ComputeSharp.GraphicsDevice"/> associated with the current instance.
        /// </summary>
        public GraphicsDevice GraphicsDevice { get; }

        /// <summary>
        /// Gets the width of the current texture.
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// Gets the height of the current texture.
        /// </summary>
        public int Height { get; }

        /// <summary>
        /// Gets the <see cref="ID3D12Resource"/> instance currently mapped.
        /// </summary>
        internal ID3D12Resource* D3D12Resource => this.d3D12Resource;

        /// <inheritdoc/>
        public Memory2D<T> Memory
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                ThrowIfDisposed();

                MemoryManager memoryManager = new(this);

                return new(memoryManager, 0, Height, Width, this.rowPitch);
            }
        }

        /// <inheritdoc/>
        public Span2D<T> Span
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                ThrowIfDisposed();

                return new(this.mappedData, Height, Width, this.rowPitch);
            }
        }

        /// <inheritdoc/>
        protected override bool OnDispose()
        {
            this.d3D12Resource.Dispose();

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
        /// A <see cref="MemoryManager{T}"/> implementation wrapping a <see cref="TransferTexture2D{T}"/> instance.
        /// </summary>
        private sealed class MemoryManager : MemoryManager<T>
        {
            /// <summary>
            /// The <see cref="TransferTexture2D{T}"/> in use.
            /// </summary>
            private readonly TransferTexture2D<T> texture;

            /// <summary>
            /// Creates a new <see cref="MemoryManager"/> instance for a given texture.
            /// </summary>
            /// <param name="texture">The <see cref="TransferTexture2D{T}"/> in use.</param>
            public MemoryManager(TransferTexture2D<T> texture)
            {
                this.texture = texture;
            }

            /// <inheritdoc/>
            public override Memory<T> Memory
            {
                get => ThrowHelper.ThrowNotSupportedException<Memory<T>>();
            }

            /// <inheritdoc/>
            public override Span<T> GetSpan()
            {
                return new(this.texture.mappedData, this.texture.Height * this.texture.rowPitch);
            }

            /// <inheritdoc/>
            public override MemoryHandle Pin(int elementIndex = 0)
            {
                Guard.IsEqualTo(elementIndex, 0, nameof(elementIndex));

                this.texture.ThrowIfDisposed();

                return new(this.texture.mappedData);
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
