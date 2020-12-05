using System;
using System.Runtime.CompilerServices;
using ComputeSharp.Core.Interop;
using ComputeSharp.Exceptions;
using ComputeSharp.Graphics.Buffers.Enums;
using ComputeSharp.Graphics.Buffers.Interop;
using ComputeSharp.Graphics.Commands;
using ComputeSharp.Graphics.Extensions;
using ComputeSharp.Graphics.Helpers;
using Microsoft.Toolkit.Diagnostics;
using TerraFX.Interop;
using static TerraFX.Interop.D3D12_COMMAND_LIST_TYPE;
using static TerraFX.Interop.D3D12_RESOURCE_STATES;
using static TerraFX.Interop.D3D12_SRV_DIMENSION;
using static TerraFX.Interop.D3D12_UAV_DIMENSION;

namespace ComputeSharp.Graphics.Buffers.Abstract
{
    /// <summary>
    /// A <see langword="class"/> representing a typed 3D texture stored on GPU memory.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    public unsafe abstract class Texture3D<T> : NativeObject
        where T : unmanaged
    {
        /// <summary>
        /// The <see cref="ID3D12Resource"/> instance currently mapped.
        /// </summary>
        private ComPtr<ID3D12Resource> d3D12Resource;

        /// <summary>
        /// The <see cref="D3D12_GPU_DESCRIPTOR_HANDLE"/> instance for the current resource.
        /// </summary>
        /// <remarks>This field is dynamically accessed when loading shader dispatch data.</remarks>
        internal readonly D3D12_GPU_DESCRIPTOR_HANDLE D3D12GpuDescriptorHandle;

        /// <summary>
        /// The default <see cref="D3D12_RESOURCE_STATES"/> value for the current resource.
        /// </summary>
        private readonly D3D12_RESOURCE_STATES D3D12ResourceState;

        /// <summary>
        /// Creates a new <see cref="Texture3D{T}"/> instance with the specified parameters.
        /// </summary>
        /// <param name="device">The <see cref="Graphics.GraphicsDevice"/> associated with the current instance.</param>
        /// <param name="height">The height of the texture.</param>
        /// <param name="width">The width of the texture.</param>
        /// <param name="depth">The depth of the texture.</param>
        /// <param name="resourceType">The resource type for the current texture.</param>
        private protected Texture3D(GraphicsDevice device, int width, int height, int depth, ResourceType resourceType)
        {
            device.ThrowIfDisposed();

            Guard.IsGreaterThanOrEqualTo(width, 0, nameof(width));
            Guard.IsGreaterThanOrEqualTo(height, 0, nameof(height));
            Guard.IsBetweenOrEqualTo(depth, 0, ushort.MaxValue, nameof(depth));

            GraphicsDevice = device;
            Width = width;
            Height = height;
            Depth = depth;

            this.d3D12Resource = device.D3D12Device->CreateCommittedResource(
                resourceType,
                DXGIFormatHelper.GetForType<T>(),
                (uint)width,
                (uint)height,
                (ushort)depth,
                out D3D12ResourceState);

            device.AllocateShaderResourceViewDescriptorHandles(out D3D12_CPU_DESCRIPTOR_HANDLE d3D12CpuDescriptorHandle, out D3D12GpuDescriptorHandle);

            switch (resourceType)
            {
                case ResourceType.ReadOnly:
                    device.D3D12Device->CreateShaderResourceView(this.d3D12Resource, DXGIFormatHelper.GetForType<T>(), D3D12_SRV_DIMENSION_TEXTURE3D, d3D12CpuDescriptorHandle);
                    break;
                case ResourceType.ReadWrite:
                    device.D3D12Device->CreateUnorderedAccessView(this.d3D12Resource, DXGIFormatHelper.GetForType<T>(), D3D12_UAV_DIMENSION_TEXTURE3D, d3D12CpuDescriptorHandle);
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
        /// Gets the depth of the current texture.
        /// </summary>
        public int Depth { get; }

        /// <summary>
        /// Gets the <see cref="ID3D12Resource"/> instance currently mapped.
        /// </summary>
        internal ID3D12Resource* D3D12Resource => this.d3D12Resource;

        /// <summary>
        /// Reads the contents of the specified range from the current <see cref="Texture3D{T}"/> instance and writes them into a target <see cref="Span{T}"/>.
        /// </summary>
        /// <param name="destination">The input <see cref="Span{T}"/> to write data to.</param>
        /// <param name="x">The horizontal offset in the source texture.</param>
        /// <param name="y">The vertical offset in the source texture.</param>
        /// <param name="z">The depthwise offset in the source texture.</param>
        /// <param name="width">The width of the memory area to copy.</param>
        /// <param name="height">The height of the memory area to copy.</param>
        /// <param name="depth">The depth of the memory area to copy.</param>
        public void GetData(Span<T> destination, int x, int y, int z, int width, int height, int depth)
        {
            GraphicsDevice.ThrowIfDisposed();

            ThrowIfDisposed();

            Guard.IsInRange(x, 0, Width, nameof(x));
            Guard.IsInRange(y, 0, Height, nameof(y));
            Guard.IsInRange(z, 0, Depth, nameof(z));
            Guard.IsBetweenOrEqualTo(width, 0, Width, nameof(width));
            Guard.IsBetweenOrEqualTo(height, 0, Height, nameof(height));
            Guard.IsBetweenOrEqualTo(depth, 0, Depth, nameof(depth));
            Guard.IsLessThanOrEqualTo(x + width, Width, nameof(x));
            Guard.IsLessThanOrEqualTo(y + height, Height, nameof(y));
            Guard.IsLessThanOrEqualTo(z + depth, Depth, nameof(z));
            Guard.HasSizeGreaterThanOrEqualTo(destination, width * height * depth, nameof(destination));

            D3D12_RESOURCE_DESC d3D12ResourceDescription = D3D12_RESOURCE_DESC.Tex3D(DXGIFormatHelper.GetForType<T>(), (ulong)width, (uint)height, (ushort)depth);

            GraphicsDevice.D3D12Device->GetCopyableFootprint(
                &d3D12ResourceDescription,
                out D3D12_PLACED_SUBRESOURCE_FOOTPRINT d3D12PlacedSubresourceFootprint,
                out _,
                out ulong rowSizeInBytes,
                out ulong totalSizeInBytes);

            using ComPtr<ID3D12Resource> d3D12Resource = GraphicsDevice.D3D12Device->CreateCommittedResource(ResourceType.ReadBack, totalSizeInBytes);

            using (CommandList copyCommandList = new(GraphicsDevice, D3D12_COMMAND_LIST_TYPE_COMPUTE))
            {
                copyCommandList.ResourceBarrier(D3D12Resource, D3D12ResourceState, D3D12_RESOURCE_STATE_COPY_SOURCE);

                copyCommandList.CopyTextureRegion(
                    d3D12Resource.Get(),
                    (uint)Unsafe.SizeOf<T>(),
                    D3D12Resource,
                    DXGIFormatHelper.GetForType<T>(),
                    (uint)x,
                    (uint)y,
                    (ushort)z,
                    (uint)width,
                    (uint)height,
                    (ushort)depth);

                copyCommandList.ResourceBarrier(D3D12Resource, D3D12_RESOURCE_STATE_COPY_SOURCE, D3D12ResourceState);
                copyCommandList.ExecuteAndWaitForCompletion();
            }

            using ID3D12ResourceMap resource = d3D12Resource.Get()->Map();

            MemoryHelper.Copy(
                resource.Pointer,
                width,
                height,
                depth,
                rowSizeInBytes,
                d3D12PlacedSubresourceFootprint.Footprint.RowPitch,
                d3D12PlacedSubresourceFootprint.Footprint.RowPitch * (uint)height,
                destination);
        }

        /// <summary>
        /// Writes the contents of a given <see cref="ReadOnlySpan{T}"/> to a specified area of the current <see cref="Texture2D{T}"/> instance.
        /// </summary>
        /// <param name="source">The input <see cref="ReadOnlySpan{T}"/> to read data from.</param>
        /// <param name="x">The horizontal offset in the destination texture.</param>
        /// <param name="y">The vertical offset in the destination texture.</param>
        /// <param name="z">The depthwise offseet in the destination texture.</param>
        /// <param name="width">The width of the memory area to write to.</param>
        /// <param name="height">The height of the memory area to write to.</param>
        /// <param name="depth">The depth of the memory area to write to.</param>
        public void SetData(ReadOnlySpan<T> source, int x, int y, int z, int width, int height, int depth)
        {
            GraphicsDevice.ThrowIfDisposed();

            ThrowIfDisposed();

            Guard.IsInRange(x, 0, Width, nameof(x));
            Guard.IsInRange(y, 0, Height, nameof(y));
            Guard.IsInRange(z, 0, Depth, nameof(z));
            Guard.IsBetweenOrEqualTo(width, 0, Width, nameof(width));
            Guard.IsBetweenOrEqualTo(height, 0, Height, nameof(height));
            Guard.IsBetweenOrEqualTo(depth, 0, Depth, nameof(depth));
            Guard.IsLessThanOrEqualTo(x + width, Width, nameof(x));
            Guard.IsLessThanOrEqualTo(y + height, Height, nameof(y));
            Guard.IsLessThanOrEqualTo(z + depth, Depth, nameof(z));
            Guard.HasSizeGreaterThanOrEqualTo(source, width * height * depth, nameof(source));

            D3D12_RESOURCE_DESC d3D12ResourceDescription = D3D12_RESOURCE_DESC.Tex3D(DXGIFormatHelper.GetForType<T>(), (ulong)width, (uint)height, (ushort)depth);

            GraphicsDevice.D3D12Device->GetCopyableFootprint(
                &d3D12ResourceDescription,
                out D3D12_PLACED_SUBRESOURCE_FOOTPRINT d3D12PlacedSubresourceFootprint,
                out _,
                out ulong rowSizeInBytes,
                out ulong totalSizeInBytes);

            using ComPtr<ID3D12Resource> d3D12Resource = GraphicsDevice.D3D12Device->CreateCommittedResource(ResourceType.Upload, totalSizeInBytes);

            using (ID3D12ResourceMap resource = d3D12Resource.Get()->Map())
            {
                MemoryHelper.Copy(
                    source,
                    resource.Pointer,
                    width,
                    height,
                    depth,
                    rowSizeInBytes, 
                    d3D12PlacedSubresourceFootprint.Footprint.RowPitch,
                    d3D12PlacedSubresourceFootprint.Footprint.RowPitch * (uint)height);
            }

            using CommandList copyCommandList = new(GraphicsDevice, D3D12_COMMAND_LIST_TYPE_COMPUTE);

            copyCommandList.ResourceBarrier(D3D12Resource, D3D12ResourceState, D3D12_RESOURCE_STATE_COPY_DEST);

            copyCommandList.CopyTextureRegion(
                D3D12Resource,
                DXGIFormatHelper.GetForType<T>(),
                (uint)x,
                (uint)y,
                (ushort)z,
                (uint)width,
                (uint)height,
                (ushort)depth,
                d3D12Resource.Get(),
                (uint)Unsafe.SizeOf<T>());

            copyCommandList.ResourceBarrier(D3D12Resource, D3D12_RESOURCE_STATE_COPY_DEST, D3D12ResourceState);
            copyCommandList.ExecuteAndWaitForCompletion();
        }

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
