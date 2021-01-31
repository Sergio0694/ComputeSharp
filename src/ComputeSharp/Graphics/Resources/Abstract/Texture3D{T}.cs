using System.Runtime.CompilerServices;
using ComputeSharp.__Internals;
using ComputeSharp.Exceptions;
using ComputeSharp.Graphics;
using ComputeSharp.Graphics.Commands;
using ComputeSharp.Graphics.Extensions;
using ComputeSharp.Graphics.Helpers;
using ComputeSharp.Graphics.Resources.Interop;
using ComputeSharp.Interop;
using Microsoft.Toolkit.Diagnostics;
using TerraFX.Interop;
using static TerraFX.Interop.D3D12_COMMAND_LIST_TYPE;
using static TerraFX.Interop.D3D12_RESOURCE_STATES;
using static TerraFX.Interop.D3D12_SRV_DIMENSION;
using static TerraFX.Interop.D3D12_UAV_DIMENSION;
using FX = TerraFX.Interop.Windows;
using ResourceType = ComputeSharp.Graphics.Resources.Enums.ResourceType;

#pragma warning disable CS0618

namespace ComputeSharp.Resources
{
    /// <summary>
    /// A <see langword="class"/> representing a typed 3D texture stored on GPU memory.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    public unsafe abstract class Texture3D<T> : NativeObject, GraphicsResourceHelper.IGraphicsResource
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
        /// The <see cref="D3D12_PLACED_SUBRESOURCE_FOOTPRINT"/> description for the current resource.
        /// </summary>
        private readonly D3D12_PLACED_SUBRESOURCE_FOOTPRINT d3D12PlacedSubresourceFootprint;

        /// The <see cref="Allocation"/> instance used to retrieve <see cref="d3D12Resource"/>, if any.
        /// </summary>
        /// <remarks>This will be <see langword="null"/> if the owning device has <see cref="GraphicsDevice.IsCacheCoherentUMA"/> set.</remarks>
        private UniquePtr<Allocation> allocation;

        /// <summary>
        /// Creates a new <see cref="Texture3D{T}"/> instance with the specified parameters.
        /// </summary>
        /// <param name="device">The <see cref="ComputeSharp.GraphicsDevice"/> associated with the current instance.</param>
        /// <param name="height">The height of the texture.</param>
        /// <param name="width">The width of the texture.</param>
        /// <param name="depth">The depth of the texture.</param>
        /// <param name="resourceType">The resource type for the current texture.</param>
        /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
        /// <param name="d3D12FormatSupport">The format support for the current texture type.</param>
        private protected Texture3D(GraphicsDevice device, int width, int height, int depth, ResourceType resourceType, AllocationMode allocationMode, D3D12_FORMAT_SUPPORT1 d3D12FormatSupport)
        {
            device.ThrowIfDisposed();

            Guard.IsBetweenOrEqualTo(width, 1, FX.D3D12_REQ_TEXTURE3D_U_V_OR_W_DIMENSION, nameof(width));
            Guard.IsBetweenOrEqualTo(height, 1, FX.D3D12_REQ_TEXTURE3D_U_V_OR_W_DIMENSION, nameof(height));
            Guard.IsBetweenOrEqualTo(depth, 1, FX.D3D12_REQ_TEXTURE3D_U_V_OR_W_DIMENSION, nameof(depth));

            if (!device.D3D12Device->IsDxgiFormatSupported(DXGIFormatHelper.GetForType<T>(), d3D12FormatSupport))
            {
                UnsupportedTextureTypeException.ThrowForTexture2D<T>();
            }

            GraphicsDevice = device;

            if (device.IsCacheCoherentUMA)
            {
                this.d3D12Resource = device.D3D12Device->CreateCommittedResource(
                    resourceType,
                    allocationMode,
                    DXGIFormatHelper.GetForType<T>(),
                    (uint)width,
                    (uint)height,
                    (ushort)depth,
                    true,
                    out this.d3D12ResourceState);
            }
            else
            {
                this.allocation = device.Allocator->CreateResource(
                    resourceType,
                    allocationMode,
                    DXGIFormatHelper.GetForType<T>(),
                    (uint)width,
                    (uint)height,
                    (ushort)depth,
                    out this.d3D12ResourceState);
                this.d3D12Resource = new ComPtr<ID3D12Resource>(this.allocation.Get()->GetResource());
            }

            this.d3D12CommandListType = this.d3D12ResourceState == D3D12_RESOURCE_STATE_COMMON
                ? D3D12_COMMAND_LIST_TYPE_COPY
                : D3D12_COMMAND_LIST_TYPE_COMPUTE;

            GraphicsDevice.D3D12Device->GetCopyableFootprint(
                DXGIFormatHelper.GetForType<T>(),
                (uint)width,
                (uint)height,
                (ushort)depth,
                out this.d3D12PlacedSubresourceFootprint,
                out _,
                out _);

            device.RentShaderResourceViewDescriptorHandles(out D3D12CpuDescriptorHandle, out D3D12GpuDescriptorHandle);

            switch (resourceType)
            {
                case ResourceType.ReadOnly:
                    device.D3D12Device->CreateShaderResourceView(this.d3D12Resource.Get(), DXGIFormatHelper.GetForType<T>(), D3D12_SRV_DIMENSION_TEXTURE3D, D3D12CpuDescriptorHandle);
                    break;
                case ResourceType.ReadWrite:
                    device.D3D12Device->CreateUnorderedAccessView(this.d3D12Resource.Get(), DXGIFormatHelper.GetForType<T>(), D3D12_UAV_DIMENSION_TEXTURE3D, D3D12CpuDescriptorHandle);
                    break;
            }

            this.d3D12Resource.Get()->SetName(this);
        }

        /// <summary>
        /// Gets the <see cref="ComputeSharp.GraphicsDevice"/> associated with the current instance.
        /// </summary>
        public GraphicsDevice GraphicsDevice { get; }

        /// <summary>
        /// Gets the width of the current texture.
        /// </summary>
        public int Width => (int)this.d3D12PlacedSubresourceFootprint.Footprint.Width;

        /// <summary>
        /// Gets the height of the current texture.
        /// </summary>
        public int Height => (int)this.d3D12PlacedSubresourceFootprint.Footprint.Height;

        /// <summary>
        /// Gets the depth of the current texture.
        /// </summary>
        public int Depth => (int)this.d3D12PlacedSubresourceFootprint.Footprint.Depth;

        /// <summary>
        /// Gets the <see cref="ID3D12Resource"/> instance currently mapped.
        /// </summary>
        internal ID3D12Resource* D3D12Resource => this.d3D12Resource;

        /// <summary>
        /// Reads the contents of the specified range from the current <see cref="Texture3D{T}"/> instance and writes them into a target memory area.
        /// </summary>
        /// <param name="destination">The target memory area to write data to.</param>
        /// <param name="size">The size of the memory area to write data to.</param>
        /// <param name="x">The horizontal offset in the source texture.</param>
        /// <param name="y">The vertical offset in the source texture.</param>
        /// <param name="z">The depthwise offset in the source texture.</param>
        /// <param name="width">The width of the memory area to copy.</param>
        /// <param name="height">The height of the memory area to copy.</param>
        /// <param name="depth">The depth of the memory area to copy.</param>
        internal void CopyTo(ref T destination, int size, int x, int y, int z, int width, int height, int depth)
        {
            GraphicsDevice.ThrowIfDisposed();

            ThrowIfDisposed();

            Guard.IsInRange(x, 0, Width, nameof(x));
            Guard.IsInRange(y, 0, Height, nameof(y));
            Guard.IsInRange(z, 0, Depth, nameof(z));
            Guard.IsBetweenOrEqualTo(width, 1, Width, nameof(width));
            Guard.IsBetweenOrEqualTo(height, 1, Height, nameof(height));
            Guard.IsBetweenOrEqualTo(depth, 1, Depth, nameof(depth));
            Guard.IsLessThanOrEqualTo(x + width, Width, nameof(x));
            Guard.IsLessThanOrEqualTo(y + height, Height, nameof(y));
            Guard.IsLessThanOrEqualTo(z + depth, Depth, nameof(z));
            Guard.IsGreaterThanOrEqualTo(size, (nint)width * height * depth, nameof(size));

            GraphicsDevice.D3D12Device->GetCopyableFootprint(
                DXGIFormatHelper.GetForType<T>(),
                (uint)width,
                (uint)height,
                (ushort)depth,
                out D3D12_PLACED_SUBRESOURCE_FOOTPRINT d3D12PlacedSubresourceFootprintDestination,
                out ulong rowSizeInBytes,
                out ulong totalSizeInBytes);

            using UniquePtr<Allocation> allocation = default;
            using ComPtr<ID3D12Resource> d3D12Resource = default;

            if (GraphicsDevice.IsCacheCoherentUMA)
            {
                *&d3D12Resource = GraphicsDevice.D3D12Device->CreateCommittedResource(ResourceType.ReadBack, AllocationMode.Default, totalSizeInBytes, true);
            }
            else
            {
                *&allocation = GraphicsDevice.Allocator->CreateResource(ResourceType.ReadBack, AllocationMode.Default, totalSizeInBytes);
                *&d3D12Resource = new ComPtr<ID3D12Resource>(allocation.Get()->GetResource());
            }

            using (CommandList copyCommandList = new(GraphicsDevice, this.d3D12CommandListType))
            {
                if (copyCommandList.D3D12CommandListType == D3D12_COMMAND_LIST_TYPE_COMPUTE)
                {
                    copyCommandList.D3D12GraphicsCommandList->ResourceBarrier(D3D12Resource, this.d3D12ResourceState, D3D12_RESOURCE_STATE_COPY_SOURCE);
                }

                copyCommandList.D3D12GraphicsCommandList->CopyTextureRegion(
                    d3D12ResourceDestination: d3D12Resource.Get(),
                    &d3D12PlacedSubresourceFootprintDestination,
                    destinationX: 0,
                    destinationY: 0,
                    destinationZ: 0,
                    d3D12ResourceSource: D3D12Resource,
                    sourceX: (uint)x,
                    sourceY: (uint)y,
                    sourceZ: (ushort)z,
                    (uint)width,
                    (uint)height,
                    (ushort)depth);

                if (copyCommandList.D3D12CommandListType == D3D12_COMMAND_LIST_TYPE_COMPUTE)
                {
                    copyCommandList.D3D12GraphicsCommandList->ResourceBarrier(D3D12Resource, D3D12_RESOURCE_STATE_COPY_SOURCE, this.d3D12ResourceState);
                }

                copyCommandList.ExecuteAndWaitForCompletion();
            }

            using ID3D12ResourceMap resource = d3D12Resource.Get()->Map();

            fixed (void* destinationPointer = &destination)
            {
                MemoryHelper.Copy(
                    resource.Pointer,
                    (uint)height,
                    (uint)depth,
                    rowSizeInBytes,
                    d3D12PlacedSubresourceFootprintDestination.Footprint.RowPitch,
                    d3D12PlacedSubresourceFootprintDestination.Footprint.RowPitch * (uint)height,
                    destinationPointer);
            }
        }

        /// <summary>
        /// Reads the contents of the specified range from the current <see cref="Texture3D{T}"/> instance and writes them into a <see cref="ReadBackTexture3D{T}"/> instance.
        /// </summary>
        /// <param name="destination">The target <see cref="ReadBackTexture3D{T}"/> instance to write data to.</param>
        /// <param name="destinationX">The horizontal offset within <paramref name="destination"/>.</param>
        /// <param name="destinationY">The vertical offset within <paramref name="destination"/>.</param>
        /// <param name="destinationZ">The depthwise offset within <paramref name="destination"/>.</param>
        /// <param name="sourceX">The horizontal offset in the source texture.</param>
        /// <param name="sourceY">The vertical offset in the source texture.</param>
        /// <param name="sourceZ">The depthwise offset in the source texture.</param>
        /// <param name="width">The width of the memory area to copy.</param>
        /// <param name="height">The height of the memory area to copy.</param>
        /// <param name="depth">The depth of the memory area to copy.</param>
        internal void CopyTo(ReadBackTexture3D<T> destination, int destinationX, int destinationY, int destinationZ, int sourceX, int sourceY, int sourceZ, int width, int height, int depth)
        {
            GraphicsDevice.ThrowIfDisposed();

            ThrowIfDisposed();

            destination.ThrowIfDeviceMismatch(GraphicsDevice);
            destination.ThrowIfDisposed();

            Guard.IsInRange(destinationX, 0, destination.Width, nameof(destinationX));
            Guard.IsInRange(destinationY, 0, destination.Height, nameof(destinationY));
            Guard.IsInRange(destinationZ, 0, destination.Depth, nameof(destinationZ));
            Guard.IsInRange(sourceX, 0, Width, nameof(sourceX));
            Guard.IsInRange(sourceY, 0, Height, nameof(sourceY));
            Guard.IsInRange(sourceZ, 0, Depth, nameof(sourceZ));
            Guard.IsBetweenOrEqualTo(width, 1, Width, nameof(width));
            Guard.IsBetweenOrEqualTo(height, 1, Height, nameof(height));
            Guard.IsBetweenOrEqualTo(depth, 1, Depth, nameof(depth));
            Guard.IsBetweenOrEqualTo(width, 1, destination.Width, nameof(width));
            Guard.IsBetweenOrEqualTo(height, 1, destination.Height, nameof(height));
            Guard.IsBetweenOrEqualTo(depth, 1, destination.Depth, nameof(depth));
            Guard.IsBetweenOrEqualTo(destinationX + width, 1, destination.Width, nameof(destinationX));
            Guard.IsBetweenOrEqualTo(destinationY + height, 1, destination.Height, nameof(destinationY));
            Guard.IsBetweenOrEqualTo(destinationZ + depth, 1, destination.Depth, nameof(destinationZ));
            Guard.IsLessThanOrEqualTo(sourceX + width, Width, nameof(sourceX));
            Guard.IsLessThanOrEqualTo(sourceY + height, Height, nameof(sourceY));
            Guard.IsLessThanOrEqualTo(sourceZ + depth, Depth, nameof(sourceZ));

            using CommandList copyCommandList = new(GraphicsDevice, this.d3D12CommandListType);

            if (copyCommandList.D3D12CommandListType == D3D12_COMMAND_LIST_TYPE_COMPUTE)
            {
                copyCommandList.D3D12GraphicsCommandList->ResourceBarrier(D3D12Resource, this.d3D12ResourceState, D3D12_RESOURCE_STATE_COPY_SOURCE);
            }

            fixed (D3D12_PLACED_SUBRESOURCE_FOOTPRINT* d3D12PlacedSubresourceFootprintDestination = &destination.D3D12PlacedSubresourceFootprint)
            {
                copyCommandList.D3D12GraphicsCommandList->CopyTextureRegion(
                    d3D12ResourceDestination: destination.D3D12Resource,
                    d3D12PlacedSubresourceFootprintDestination,
                    (uint)destinationX,
                    (uint)destinationY,
                    (ushort)destinationZ,
                    d3D12ResourceSource: D3D12Resource,
                    (uint)sourceX,
                    (uint)sourceY,
                    (ushort)sourceZ,
                    (uint)width,
                    (uint)height,
                    (ushort)depth);
            }

            if (copyCommandList.D3D12CommandListType == D3D12_COMMAND_LIST_TYPE_COMPUTE)
            {
                copyCommandList.D3D12GraphicsCommandList->ResourceBarrier(D3D12Resource, D3D12_RESOURCE_STATE_COPY_SOURCE, this.d3D12ResourceState);
            }

            copyCommandList.ExecuteAndWaitForCompletion();
        }

        /// <summary>
        /// Writes the contents of a given memory area to a specified area of the current <see cref="Texture3D{T}"/> instance.
        /// </summary>
        /// <param name="source">The input memory area to read data from.</param>
        /// <param name="size">The size of the memory area to read data from.</param>
        /// <param name="x">The horizontal offset in the destination texture.</param>
        /// <param name="y">The vertical offset in the destination texture.</param>
        /// <param name="z">The depthwise offset in the destination texture.</param>
        /// <param name="width">The width of the memory area to write to.</param>
        /// <param name="height">The height of the memory area to write to.</param>
        /// <param name="depth">The depth of the memory area to write to.</param>
        internal void CopyFrom(ref T source, int size, int x, int y, int z, int width, int height, int depth)
        {
            GraphicsDevice.ThrowIfDisposed();

            ThrowIfDisposed();

            Guard.IsInRange(x, 0, Width, nameof(x));
            Guard.IsInRange(y, 0, Height, nameof(y));
            Guard.IsInRange(z, 0, Depth, nameof(z));
            Guard.IsBetweenOrEqualTo(width, 1, Width, nameof(width));
            Guard.IsBetweenOrEqualTo(height, 1, Height, nameof(height));
            Guard.IsBetweenOrEqualTo(depth, 1, Depth, nameof(depth));
            Guard.IsLessThanOrEqualTo(x + width, Width, nameof(x));
            Guard.IsLessThanOrEqualTo(y + height, Height, nameof(y));
            Guard.IsLessThanOrEqualTo(z + depth, Depth, nameof(z));
            Guard.IsGreaterThanOrEqualTo(size, (nint)width * height * depth, nameof(size));

            GraphicsDevice.D3D12Device->GetCopyableFootprint(
                DXGIFormatHelper.GetForType<T>(),
                (uint)width,
                (uint)height,
                (ushort)depth,
                out D3D12_PLACED_SUBRESOURCE_FOOTPRINT d3D12PlacedSubresourceFootprintSource,
                out ulong rowSizeInBytes,
                out ulong totalSizeInBytes);

            using UniquePtr<Allocation> allocation = default;
            using ComPtr<ID3D12Resource> d3D12Resource = default;

            if (GraphicsDevice.IsCacheCoherentUMA)
            {
                *&d3D12Resource = GraphicsDevice.D3D12Device->CreateCommittedResource(ResourceType.Upload, AllocationMode.Default, totalSizeInBytes, true);
            }
            else
            {
                *&allocation = GraphicsDevice.Allocator->CreateResource(ResourceType.Upload, AllocationMode.Default, totalSizeInBytes);
                *&d3D12Resource = new ComPtr<ID3D12Resource>(allocation.Get()->GetResource());
            }

            using (ID3D12ResourceMap resource = d3D12Resource.Get()->Map())
            fixed (void* sourcePointer = &source)
            {
                MemoryHelper.Copy(
                    sourcePointer,
                    resource.Pointer,
                    (uint)height,
                    (uint)depth,
                    rowSizeInBytes,
                    d3D12PlacedSubresourceFootprintSource.Footprint.RowPitch,
                    d3D12PlacedSubresourceFootprintSource.Footprint.RowPitch * (uint)height);
            }

            using CommandList copyCommandList = new(GraphicsDevice, this.d3D12CommandListType);

            if (copyCommandList.D3D12CommandListType == D3D12_COMMAND_LIST_TYPE_COMPUTE)
            {
                copyCommandList.D3D12GraphicsCommandList->ResourceBarrier(D3D12Resource, this.d3D12ResourceState, D3D12_RESOURCE_STATE_COPY_DEST);
            }

            copyCommandList.D3D12GraphicsCommandList->CopyTextureRegion(
                d3D12ResourceDestination: D3D12Resource,
                destinationX: (uint)x,
                destinationY: (uint)y,
                destinationZ: (ushort)z,
                d3D12ResourceSource: d3D12Resource.Get(),
                &d3D12PlacedSubresourceFootprintSource,
                sourceX: 0,
                sourceY: 0,
                sourceZ: 0,
                (uint)width,
                (uint)height,
                (ushort)depth);

            if (copyCommandList.D3D12CommandListType == D3D12_COMMAND_LIST_TYPE_COMPUTE)
            {
                copyCommandList.D3D12GraphicsCommandList->ResourceBarrier(D3D12Resource, D3D12_RESOURCE_STATE_COPY_DEST, this.d3D12ResourceState);
            }

            copyCommandList.ExecuteAndWaitForCompletion();
        }

        /// <summary>
        /// Writes the contents of a given <see cref="UploadTexture3D{T}"/> instance to a specified area of the current <see cref="Texture3D{T}"/> instance.
        /// </summary>
        /// <param name="source">The input <see cref="UploadTexture3D{T}"/> instance to read data from.</param>
        /// <param name="sourceX">The horizontal offset within <paramref name="source"/>.</param>
        /// <param name="sourceY">The vertical offset within <paramref name="source"/>.</param>
        /// <param name="sourceZ">The depthwise offset within <paramref name="source"/>.</param>
        /// <param name="destinationX">The horizontal offset in the destination texture.</param>
        /// <param name="destinationY">The vertical offset in the destination texture.</param>
        /// <param name="destinationZ">The depthwise offset in the destination texture.</param>
        /// <param name="width">The width of the memory area to write to.</param>
        /// <param name="height">The height of the memory area to write to.</param>
        /// <param name="depth">The depth of the memory area to write to.</param>
        internal void CopyFrom(UploadTexture3D<T> source, int sourceX, int sourceY, int sourceZ, int destinationX, int destinationY, int destinationZ, int width, int height, int depth)
        {
            GraphicsDevice.ThrowIfDisposed();

            ThrowIfDisposed();

            source.ThrowIfDeviceMismatch(GraphicsDevice);
            source.ThrowIfDisposed();

            Guard.IsInRange(sourceX, 0, source.Width, nameof(sourceX));
            Guard.IsInRange(sourceY, 0, source.Height, nameof(sourceY));
            Guard.IsInRange(sourceZ, 0, source.Depth, nameof(sourceZ));
            Guard.IsInRange(destinationX, 0, Width, nameof(destinationX));
            Guard.IsInRange(destinationY, 0, Height, nameof(destinationY));
            Guard.IsInRange(destinationZ, 0, Depth, nameof(destinationZ));
            Guard.IsBetweenOrEqualTo(width, 1, Width, nameof(width));
            Guard.IsBetweenOrEqualTo(height, 1, Height, nameof(height));
            Guard.IsBetweenOrEqualTo(depth, 1, Depth, nameof(depth));
            Guard.IsBetweenOrEqualTo(width, 1, source.Width, nameof(width));
            Guard.IsBetweenOrEqualTo(height, 1, source.Height, nameof(height));
            Guard.IsBetweenOrEqualTo(depth, 1, source.Depth, nameof(depth));
            Guard.IsLessThanOrEqualTo(sourceX + width, source.Width, nameof(sourceX));
            Guard.IsLessThanOrEqualTo(sourceY + height, source.Height, nameof(sourceY));
            Guard.IsLessThanOrEqualTo(sourceZ + depth, source.Depth, nameof(sourceZ));
            Guard.IsLessThanOrEqualTo(destinationX + width, Width, nameof(destinationX));
            Guard.IsLessThanOrEqualTo(destinationY + height, Height, nameof(destinationY));
            Guard.IsLessThanOrEqualTo(destinationZ + depth, Depth, nameof(destinationZ));

            using CommandList copyCommandList = new(GraphicsDevice, this.d3D12CommandListType);

            if (copyCommandList.D3D12CommandListType == D3D12_COMMAND_LIST_TYPE_COMPUTE)
            {
                copyCommandList.D3D12GraphicsCommandList->ResourceBarrier(D3D12Resource, this.d3D12ResourceState, D3D12_RESOURCE_STATE_COPY_DEST);
            }

            fixed (D3D12_PLACED_SUBRESOURCE_FOOTPRINT* d3D12PlacedSubresourceFootprintSource = &source.D3D12PlacedSubresourceFootprint)
            {
                copyCommandList.D3D12GraphicsCommandList->CopyTextureRegion(
                    d3D12ResourceDestination: D3D12Resource,
                    (uint)destinationX,
                    (uint)destinationY,
                    (ushort)destinationZ,
                    d3D12ResourceSource: source.D3D12Resource,
                    d3D12PlacedSubresourceFootprintSource,
                    (uint)sourceX,
                    (uint)sourceY,
                    (ushort)sourceZ,
                    (uint)width,
                    (uint)height,
                    (ushort)depth);
            }

            if (copyCommandList.D3D12CommandListType == D3D12_COMMAND_LIST_TYPE_COMPUTE)
            {
                copyCommandList.D3D12GraphicsCommandList->ResourceBarrier(D3D12Resource, D3D12_RESOURCE_STATE_COPY_DEST, this.d3D12ResourceState);
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
