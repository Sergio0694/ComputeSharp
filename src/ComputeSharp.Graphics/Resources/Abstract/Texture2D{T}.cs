using System.Runtime.CompilerServices;
using ComputeSharp.__Internals;
using ComputeSharp.Exceptions;
using ComputeSharp.Graphics.Commands;
using ComputeSharp.Graphics.Extensions;
using ComputeSharp.Graphics.Helpers;
using ComputeSharp.Graphics.Resources.Enums;
using ComputeSharp.Graphics.Resources.Interop;
using ComputeSharp.Interop;
using Microsoft.Toolkit.Diagnostics;
using TerraFX.Interop;
using static TerraFX.Interop.D3D12_COMMAND_LIST_TYPE;
using static TerraFX.Interop.D3D12_RESOURCE_STATES;
using static TerraFX.Interop.D3D12_SRV_DIMENSION;
using static TerraFX.Interop.D3D12_UAV_DIMENSION;
using FX = TerraFX.Interop.Windows;

#pragma warning disable CS0618

namespace ComputeSharp.Resources
{
    /// <summary>
    /// A <see langword="class"/> representing a typed 2D texture stored on GPU memory.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    public unsafe abstract class Texture2D<T> : NativeObject, GraphicsResourceHelper.IGraphicsResource
        where T : unmanaged
    {
        /// <summary>
        /// The <see cref="ID3D12Resource"/> instance currently mapped.
        /// </summary>
        private ComPtr<ID3D12Resource> d3D12Resource;

        /// <summary>
        /// The <see cref="D3D12_CPU_DESCRIPTOR_HANDLE"/> instance for the current resource.
        /// </summary>
        private readonly D3D12_CPU_DESCRIPTOR_HANDLE D3D12CpuDescriptorHandle;

        /// <summary>
        /// The <see cref="D3D12_GPU_DESCRIPTOR_HANDLE"/> instance for the current resource.
        /// </summary>
        internal readonly D3D12_GPU_DESCRIPTOR_HANDLE D3D12GpuDescriptorHandle;

        /// <summary>
        /// The default <see cref="D3D12_RESOURCE_STATES"/> value for the current resource.
        /// </summary>
        private readonly D3D12_RESOURCE_STATES d3D12ResourceState;

        /// <summary>
        /// The <see cref="D3D12_COMMAND_LIST_TYPE"/> value to use for copy operations.
        /// </summary>
        private readonly D3D12_COMMAND_LIST_TYPE d3D12CommandListType;

        /// <summary>
        /// Creates a new <see cref="Texture2D{T}"/> instance with the specified parameters.
        /// </summary>
        /// <param name="device">The <see cref="ComputeSharp.GraphicsDevice"/> associated with the current instance.</param>
        /// <param name="height">The height of the texture.</param>
        /// <param name="width">The width of the texture.</param>
        /// <param name="resourceType">The resource type for the current texture.</param>
        /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
        /// <param name="d3D12FormatSupport">The format support for the current texture type.</param>
        private protected Texture2D(GraphicsDevice device, int width, int height, ResourceType resourceType, AllocationMode allocationMode, D3D12_FORMAT_SUPPORT1 d3D12FormatSupport)
        {
            device.ThrowIfDisposed();

            Guard.IsBetweenOrEqualTo(width, 1, FX.D3D12_REQ_TEXTURE2D_U_OR_V_DIMENSION, nameof(width));
            Guard.IsBetweenOrEqualTo(height, 1, FX.D3D12_REQ_TEXTURE2D_U_OR_V_DIMENSION, nameof(height));

            if (!device.D3D12Device->IsDxgiFormatSupported(DXGIFormatHelper.GetForType<T>(), d3D12FormatSupport))
            {
                UnsupportedTextureTypeException.ThrowForTexture2D<T>();
            }

            GraphicsDevice = device;
            Width = width;
            Height = height;

            this.d3D12Resource = device.D3D12Device->CreateCommittedResource(
                resourceType,
                allocationMode,
                DXGIFormatHelper.GetForType<T>(),
                (uint)width,
                (uint)height,
                device.IsCacheCoherentUMA,
                out this.d3D12ResourceState);

            this.d3D12CommandListType = this.d3D12ResourceState == D3D12_RESOURCE_STATE_COMMON
                ? D3D12_COMMAND_LIST_TYPE_COPY
                : D3D12_COMMAND_LIST_TYPE_COMPUTE;

            device.RentShaderResourceViewDescriptorHandles(out D3D12CpuDescriptorHandle, out D3D12GpuDescriptorHandle);

            switch (resourceType)
            {
                case ResourceType.ReadOnly:
                    device.D3D12Device->CreateShaderResourceView(this.d3D12Resource, DXGIFormatHelper.GetForType<T>(), D3D12_SRV_DIMENSION_TEXTURE2D, D3D12CpuDescriptorHandle);
                    break;
                case ResourceType.ReadWrite:
                    device.D3D12Device->CreateUnorderedAccessView(this.d3D12Resource, DXGIFormatHelper.GetForType<T>(), D3D12_UAV_DIMENSION_TEXTURE2D, D3D12CpuDescriptorHandle);
                    break;
            }
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

        /// <summary>
        /// Reads the contents of the specified range from the current <see cref="Texture2D{T}"/> instance and writes them into a target memory area.
        /// </summary>
        /// <param name="destination">The target memory area to write data to.</param>
        /// <param name="size">The size of the target memory area to write data to.</param>
        /// <param name="x">The horizontal offset in the source texture.</param>
        /// <param name="y">The vertical offset in the source texture.</param>
        /// <param name="width">The width of the memory area to copy.</param>
        /// <param name="height">The height of the memory area to copy.</param>
        internal void CopyTo(ref T destination, int size, int x, int y, int width, int height)
        {
            GraphicsDevice.ThrowIfDisposed();

            ThrowIfDisposed();

            Guard.IsInRange(x, 0, Width, nameof(x));
            Guard.IsInRange(y, 0, Height, nameof(y));
            Guard.IsBetweenOrEqualTo(width, 1, Width, nameof(width));
            Guard.IsBetweenOrEqualTo(height, 1, Height, nameof(height));
            Guard.IsLessThanOrEqualTo(x + width, Width, nameof(x));
            Guard.IsLessThanOrEqualTo(y + height, Height, nameof(y));
            Guard.IsGreaterThanOrEqualTo(size, (nint)width * height, nameof(size));

            D3D12_RESOURCE_DESC d3D12ResourceDescription = D3D12_RESOURCE_DESC.Tex2D(DXGIFormatHelper.GetForType<T>(), (ulong)width, (uint)height);

            GraphicsDevice.D3D12Device->GetCopyableFootprint(
                &d3D12ResourceDescription,
                out D3D12_PLACED_SUBRESOURCE_FOOTPRINT d3D12PlacedSubresourceFootprint,
                out ulong rowSizeInBytes,
                out ulong totalSizeInBytes);

            using ComPtr<ID3D12Resource> d3D12Resource = GraphicsDevice.D3D12Device->CreateCommittedResource(ResourceType.ReadBack, AllocationMode.Default, totalSizeInBytes, GraphicsDevice.IsCacheCoherentUMA);

            using (CommandList copyCommandList = new(GraphicsDevice, this.d3D12CommandListType))
            {
                if (copyCommandList.D3D12CommandListType == D3D12_COMMAND_LIST_TYPE_COMPUTE)
                {
                    copyCommandList.ResourceBarrier(D3D12Resource, this.d3D12ResourceState, D3D12_RESOURCE_STATE_COPY_SOURCE);
                }

                copyCommandList.CopyTextureRegion(
                    d3D12Resource.Get(),
                    &d3D12PlacedSubresourceFootprint,
                    D3D12Resource,
                    (uint)x,
                    (uint)y,
                    0,
                    (uint)width,
                    (uint)height,
                    1);

                if (copyCommandList.D3D12CommandListType == D3D12_COMMAND_LIST_TYPE_COMPUTE)
                {
                    copyCommandList.ResourceBarrier(D3D12Resource, D3D12_RESOURCE_STATE_COPY_SOURCE, this.d3D12ResourceState);
                }

                copyCommandList.ExecuteAndWaitForCompletion();
            }

            using ID3D12ResourceMap resource = d3D12Resource.Get()->Map();

            fixed (void* destinationPointer = &destination)
            {
                MemoryHelper.Copy(
                    resource.Pointer,
                    (uint)height,
                    rowSizeInBytes,
                    d3D12PlacedSubresourceFootprint.Footprint.RowPitch,
                    destinationPointer);
            }
        }

        /// <summary>
        /// Reads the contents of the specified range from the current <see cref="Texture2D{T}"/> instance and writes them into a target <see cref="ReadBackTexture2D{T}"/> instance.
        /// </summary>
        /// <param name="destination">The target <see cref="ReadBackTexture2D{T}"/> instance to write data to.</param>
        /// <param name="x">The horizontal offset in the source texture.</param>
        /// <param name="y">The vertical offset in the source texture.</param>
        /// <param name="width">The width of the memory area to copy.</param>
        /// <param name="height">The height of the memory area to copy.</param>
        internal void CopyTo(ReadBackTexture2D<T> destination, int x, int y, int width, int height)
        {
            GraphicsDevice.ThrowIfDisposed();

            ThrowIfDisposed();

            destination.ThrowIfDeviceMismatch(GraphicsDevice);
            destination.ThrowIfDisposed();

            Guard.IsInRange(x, 0, Width, nameof(x));
            Guard.IsInRange(y, 0, Height, nameof(y));
            Guard.IsBetweenOrEqualTo(width, 1, Width, nameof(width));
            Guard.IsBetweenOrEqualTo(height, 1, Height, nameof(height));
            Guard.IsBetweenOrEqualTo(width, 1, destination.Width, nameof(width));
            Guard.IsBetweenOrEqualTo(height, 1, destination.Height, nameof(height));
            Guard.IsLessThanOrEqualTo(x + width, Width, nameof(x));
            Guard.IsLessThanOrEqualTo(y + height, Height, nameof(y));

            D3D12_RESOURCE_DESC d3D12ResourceDescription = D3D12_RESOURCE_DESC.Tex2D(DXGIFormatHelper.GetForType<T>(), (ulong)destination.Width, (uint)destination.Height);

            GraphicsDevice.D3D12Device->GetCopyableFootprint(
                &d3D12ResourceDescription,
                out D3D12_PLACED_SUBRESOURCE_FOOTPRINT d3D12PlacedSubresourceFootprint,
                out ulong rowSizeInBytes,
                out ulong totalSizeInBytes);

            using CommandList copyCommandList = new(GraphicsDevice, this.d3D12CommandListType);

            if (copyCommandList.D3D12CommandListType == D3D12_COMMAND_LIST_TYPE_COMPUTE)
            {
                copyCommandList.ResourceBarrier(D3D12Resource, this.d3D12ResourceState, D3D12_RESOURCE_STATE_COPY_SOURCE);
            }

            copyCommandList.CopyTextureRegion(
                destination.D3D12Resource,
                &d3D12PlacedSubresourceFootprint,
                D3D12Resource,
                (uint)x,
                (uint)y,
                0,
                (uint)width,
                (uint)height,
                1);

            if (copyCommandList.D3D12CommandListType == D3D12_COMMAND_LIST_TYPE_COMPUTE)
            {
                copyCommandList.ResourceBarrier(D3D12Resource, D3D12_RESOURCE_STATE_COPY_SOURCE, this.d3D12ResourceState);
            }

            copyCommandList.ExecuteAndWaitForCompletion();
        }

        /// <summary>
        /// Writes the contents of a given memory area to a specified area of the current <see cref="Texture2D{T}"/> instance.
        /// </summary>
        /// <param name="source">The input memory area to read data from.</param>
        /// <param name="size">The size of the memory area to read data from.</param>
        /// <param name="x">The horizontal offset in the destination texture.</param>
        /// <param name="y">The vertical offset in the destination texture.</param>
        /// <param name="width">The width of the memory area to write to.</param>
        /// <param name="height">The height of the memory area to write to.</param>
        internal void CopyFrom(ref T source, int size, int x, int y, int width, int height)
        {
            GraphicsDevice.ThrowIfDisposed();

            ThrowIfDisposed();

            Guard.IsInRange(x, 0, Width, nameof(x));
            Guard.IsInRange(y, 0, Height, nameof(y));
            Guard.IsBetweenOrEqualTo(width, 1, Width, nameof(width));
            Guard.IsBetweenOrEqualTo(height, 1, Height, nameof(height));
            Guard.IsLessThanOrEqualTo(x + width, Width, nameof(x));
            Guard.IsLessThanOrEqualTo(y + height, Height, nameof(y));
            Guard.IsGreaterThanOrEqualTo(size, (nint)width * height, nameof(size));

            D3D12_RESOURCE_DESC d3D12ResourceDescription = D3D12_RESOURCE_DESC.Tex2D(DXGIFormatHelper.GetForType<T>(), (ulong)width, (uint)height);

            GraphicsDevice.D3D12Device->GetCopyableFootprint(
                &d3D12ResourceDescription,
                out D3D12_PLACED_SUBRESOURCE_FOOTPRINT d3D12PlacedSubresourceFootprint,
                out ulong rowSizeInBytes,
                out ulong totalSizeInBytes);

            using ComPtr<ID3D12Resource> d3D12Resource = GraphicsDevice.D3D12Device->CreateCommittedResource(ResourceType.Upload, AllocationMode.Default, totalSizeInBytes, GraphicsDevice.IsCacheCoherentUMA);

            using (ID3D12ResourceMap resource = d3D12Resource.Get()->Map())
            fixed (void* sourcePointer = &source)
            {
                MemoryHelper.Copy(
                    sourcePointer,
                    resource.Pointer,
                    (uint)height,
                    rowSizeInBytes,
                    d3D12PlacedSubresourceFootprint.Footprint.RowPitch);
            }

            using CommandList copyCommandList = new(GraphicsDevice, this.d3D12CommandListType);

            if (copyCommandList.D3D12CommandListType == D3D12_COMMAND_LIST_TYPE_COMPUTE)
            {
                copyCommandList.ResourceBarrier(D3D12Resource, this.d3D12ResourceState, D3D12_RESOURCE_STATE_COPY_DEST);
            }

            copyCommandList.CopyTextureRegion(
                D3D12Resource,
                (uint)x,
                (uint)y,
                0,
                d3D12Resource.Get(),
                &d3D12PlacedSubresourceFootprint);

            if (copyCommandList.D3D12CommandListType == D3D12_COMMAND_LIST_TYPE_COMPUTE)
            {
                copyCommandList.ResourceBarrier(D3D12Resource, D3D12_RESOURCE_STATE_COPY_DEST, this.d3D12ResourceState);
            }

            copyCommandList.ExecuteAndWaitForCompletion();
        }

        /// <summary>
        /// Writes the contents of a given <see cref="UploadTexture2D{T}"/> instance to a specified area of the current <see cref="Texture2D{T}"/> instance.
        /// </summary>
        /// <param name="source">The input <see cref="UploadTexture2D{T}"/> instance to read data from.</param>
        /// <param name="x">The horizontal offset in the destination texture.</param>
        /// <param name="y">The vertical offset in the destination texture.</param>
        /// <param name="width">The width of the memory area to write to.</param>
        /// <param name="height">The height of the memory area to write to.</param>
        internal void CopyFrom(UploadTexture2D<T> source, int x, int y, int width, int height)
        {
            GraphicsDevice.ThrowIfDisposed();

            ThrowIfDisposed();

            source.ThrowIfDeviceMismatch(GraphicsDevice);
            source.ThrowIfDisposed();

            Guard.IsInRange(x, 0, Width, nameof(x));
            Guard.IsInRange(y, 0, Height, nameof(y));
            Guard.IsBetweenOrEqualTo(width, 1, Width, nameof(width));
            Guard.IsBetweenOrEqualTo(height, 1, Height, nameof(height));
            Guard.IsBetweenOrEqualTo(width, 1, source.Width, nameof(width));
            Guard.IsBetweenOrEqualTo(height, 1, source.Height, nameof(height));
            Guard.IsLessThanOrEqualTo(x + width, Width, nameof(x));
            Guard.IsLessThanOrEqualTo(y + height, Height, nameof(y));

            D3D12_RESOURCE_DESC d3D12ResourceDescription = D3D12_RESOURCE_DESC.Tex2D(DXGIFormatHelper.GetForType<T>(), (ulong)source.Width, (uint)source.Height);

            GraphicsDevice.D3D12Device->GetCopyableFootprint(
                &d3D12ResourceDescription,
                out D3D12_PLACED_SUBRESOURCE_FOOTPRINT d3D12PlacedSubresourceFootprint,
                out ulong rowSizeInBytes,
                out ulong totalSizeInBytes);

            using CommandList copyCommandList = new(GraphicsDevice, this.d3D12CommandListType);

            if (copyCommandList.D3D12CommandListType == D3D12_COMMAND_LIST_TYPE_COMPUTE)
            {
                copyCommandList.ResourceBarrier(D3D12Resource, this.d3D12ResourceState, D3D12_RESOURCE_STATE_COPY_DEST);
            }

            copyCommandList.CopyTextureRegion(
                D3D12Resource,
                (uint)x,
                (uint)y,
                0,
                source.D3D12Resource,
                &d3D12PlacedSubresourceFootprint);

            if (copyCommandList.D3D12CommandListType == D3D12_COMMAND_LIST_TYPE_COMPUTE)
            {
                copyCommandList.ResourceBarrier(D3D12Resource, D3D12_RESOURCE_STATE_COPY_DEST, this.d3D12ResourceState);
            }

            copyCommandList.ExecuteAndWaitForCompletion();
        }

        /// <inheritdoc/>
        protected override bool OnDispose()
        {
            this.d3D12Resource.Dispose();

            if (GraphicsDevice?.IsDisposed == false)
            {
                GraphicsDevice.ReturnShaderResourceViewDescriptorHandles(D3D12CpuDescriptorHandle, D3D12GpuDescriptorHandle);
            }

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

        /// <inheritdoc/>
        D3D12_GPU_DESCRIPTOR_HANDLE GraphicsResourceHelper.IGraphicsResource.ValidateAndGetGpuDescriptorHandle(GraphicsDevice device)
        {
            ThrowIfDisposed();
            ThrowIfDeviceMismatch(device);

            return D3D12GpuDescriptorHandle;
        }
    }
}
