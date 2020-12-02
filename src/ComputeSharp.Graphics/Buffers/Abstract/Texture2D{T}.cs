using System.Runtime.CompilerServices;
using ComputeSharp.Core.Interop;
using ComputeSharp.Exceptions;
using ComputeSharp.Graphics.Buffers.Enums;
using ComputeSharp.Graphics.Extensions;
using ComputeSharp.Graphics.Helpers;
using Microsoft.Toolkit.Diagnostics;
using TerraFX.Interop;

namespace ComputeSharp.Graphics.Buffers.Abstract
{
    /// <summary>
    /// A <see langword="class"/> representing a typed 2D texture stored on GPU memory.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    public unsafe abstract class Texture2D<T> : NativeObject
        where T : unmanaged
    {
        /// <summary>
        /// The <see cref="ID3D12Resource"/> instance currently mapped.
        /// </summary>
        private ComPtr<ID3D12Resource> d3D12Resource;

        /// <summary>
        /// The <see cref="D3D12_GPU_DESCRIPTOR_HANDLE"/> instance for the current resource.
        /// </summary>
        internal readonly D3D12_GPU_DESCRIPTOR_HANDLE D3D12GpuDescriptorHandle;

        /// <summary>
        /// Creates a new <see cref="Texture2D{T}"/> instance with the specified parameters.
        /// </summary>
        /// <param name="device">The <see cref="Graphics.GraphicsDevice"/> associated with the current instance.</param>
        /// <param name="height">The height of the texture.</param>
        /// <param name="width">The width of the texture.</param>
        /// <param name="textureType">The texture type for the current texture.</param>
        private protected Texture2D(GraphicsDevice device, int width, int height, TextureType textureType)
        {
            device.ThrowIfDisposed();

            Guard.IsGreaterThanOrEqualTo(width, 0, nameof(width));
            Guard.IsGreaterThanOrEqualTo(height, 0, nameof(height));

            GraphicsDevice = device;
            Width = width;
            Height = height;

            this.d3D12Resource = device.D3D12Device->CreateCommittedResource(textureType, DXGIFormatHelper.GetForType<T>(), (uint)width, (uint)height);

            device.AllocateShaderResourceViewDescriptorHandles(out D3D12_CPU_DESCRIPTOR_HANDLE d3D12CpuDescriptorHandle, out D3D12GpuDescriptorHandle);

            switch (textureType)
            {
                case TextureType.ReadOnly:
                    device.D3D12Device->CreateShaderResourceView(this.d3D12Resource, DXGIFormatHelper.GetForType<T>(), d3D12CpuDescriptorHandle);
                    break;
                case TextureType.ReadWrite:
                    device.D3D12Device->CreateUnorderedAccessView(this.d3D12Resource, DXGIFormatHelper.GetForType<T>(), d3D12CpuDescriptorHandle);
                    break;
            }
        }

        /// <summary>
        /// Gets the <see cref="Graphics.GraphicsDevice"/> associated with the current instance.
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
        protected override void OnDispose()
        {
            this.d3D12Resource.Dispose();
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
    }
}
