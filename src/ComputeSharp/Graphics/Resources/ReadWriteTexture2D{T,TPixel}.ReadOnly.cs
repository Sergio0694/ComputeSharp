using System;
using System.Runtime.CompilerServices;
using ComputeSharp.__Internals;
using ComputeSharp.Exceptions;
using ComputeSharp.Graphics.Commands.Interop;
using ComputeSharp.Graphics.Extensions;
using ComputeSharp.Graphics.Helpers;
using ComputeSharp.Interop;
using TerraFX.Interop.DirectX;
using static TerraFX.Interop.DirectX.D3D12_SRV_DIMENSION;

#pragma warning disable CS0618

namespace ComputeSharp;

/// <inheritdoc/>
partial class ReadWriteTexture2D<T, TPixel>
{
    /// <summary>
    /// The wrapping <see cref="ReadOnly"/> instance, if available.
    /// </summary>
    private ReadOnly? readOnlyWrapper;

    /// <inheritdoc cref="ReadWriteTexture2DExtensions.AsReadOnly{T, TPixel}(ReadWriteTexture2D{T, TPixel})"/>
    internal IReadOnlyNormalizedTexture2D<TPixel> AsReadOnly()
    {
        using var _0 = GraphicsDevice.GetReferenceTrackingLease();
        using var _1 = GetReferenceTrackingLease();

        GraphicsDevice.ThrowIfDeviceLost();

        ThrowIfIsNotInReadOnlyState();

        ReadOnly? readOnlyWrapper = this.readOnlyWrapper;

        if (readOnlyWrapper is not null)
        {
            return readOnlyWrapper;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        static ReadOnly InitializeWrapper(ReadWriteTexture2D<T, TPixel> texture)
        {
            return texture.readOnlyWrapper = new(texture);
        }

        return InitializeWrapper(this);
    }

    /// <inheritdoc/>
    private protected override void OnDispose()
    {
        base.OnDispose();

        this.readOnlyWrapper?.Dispose();
    }

    /// <summary>
    /// A wrapper for a <see cref="ReadWriteTexture2D{T, TPixel}"/> resource that has been temporarily transitioned to readonly.
    /// </summary>
    private sealed unsafe class ReadOnly : NativeObject, IReadOnlyNormalizedTexture2D<TPixel>, GraphicsResourceHelper.IGraphicsResource
    {
        /// <summary>
        /// The owning <see cref="ReadWriteTexture2D{T, TPixel}"/> instance being wrapped.
        /// </summary>
        private readonly ReadWriteTexture2D<T, TPixel> owner;

        /// <summary>
        /// The <see cref="ID3D12ResourceDescriptorHandles"/> instance for the current resource.
        /// </summary>
        private readonly ID3D12ResourceDescriptorHandles d3D12ResourceDescriptorHandles;

        /// <summary>
        /// Creates a new <see cref="ReadOnly"/> instance with the specified parameters.
        /// </summary>
        /// <param name="owner">The owning <see cref="ReadWriteTexture2D{T, TPixel}"/> instance to wrap.</param>
        public ReadOnly(ReadWriteTexture2D<T, TPixel> owner)
        {
            this.owner = owner;

            owner.GraphicsDevice.RentShaderResourceViewDescriptorHandles(out this.d3D12ResourceDescriptorHandles);

            owner.GraphicsDevice.D3D12Device->CreateShaderResourceView(owner.D3D12Resource, DXGIFormatHelper.GetForType<T>(), D3D12_SRV_DIMENSION_TEXTURE2D, this.d3D12ResourceDescriptorHandles.D3D12CpuDescriptorHandle);
        }

        /// <inheritdoc/>
        public ref readonly TPixel this[int x, int y] => throw new InvalidExecutionContextException($"{typeof(ReadWriteTexture2D<T, TPixel>.ReadOnly)}[{typeof(int)}, {typeof(int)}]");

        /// <inheritdoc/>
        public ref readonly TPixel this[Int2 xy] => throw new InvalidExecutionContextException($"{typeof(ReadWriteTexture2D<T, TPixel>.ReadOnly)}[{typeof(Int2)}]");

        /// <inheritdoc/>
        public ref readonly TPixel this[float u, float v] => throw new InvalidExecutionContextException($"{typeof(ReadWriteTexture2D<T, TPixel>.ReadOnly)}[{typeof(float)}, {typeof(float)}]");

        /// <inheritdoc/>
        public ref readonly TPixel this[Float2 uv] => throw new InvalidExecutionContextException($"{typeof(ReadWriteTexture2D<T, TPixel>.ReadOnly)}[{typeof(Float2)}]");

        /// <inheritdoc/>
        public int Width => this.owner.Width;

        /// <inheritdoc/>
        public int Height => this.owner.Height;

        /// <inheritdoc/>
        public GraphicsDevice GraphicsDevice => this.owner.GraphicsDevice;

        /// <inheritdoc/>
        D3D12_GPU_DESCRIPTOR_HANDLE GraphicsResourceHelper.IGraphicsResource.ValidateAndGetGpuDescriptorHandle(GraphicsDevice device)
        {
            using var _0 = GetReferenceTrackingLease();
            using var _1 = this.owner.GetReferenceTrackingLease();

            this.owner.ThrowIfDeviceMismatch(device);

            return this.d3D12ResourceDescriptorHandles.D3D12GpuDescriptorHandle;
        }

        /// <inheritdoc/>
        (D3D12_GPU_DESCRIPTOR_HANDLE, D3D12_CPU_DESCRIPTOR_HANDLE) GraphicsResourceHelper.IGraphicsResource.ValidateAndGetGpuAndCpuDescriptorHandlesForClear(GraphicsDevice device, out bool isNormalized)
        {
            throw new NotSupportedException("This operation cannot be performaned on a readonly wrapper.");
        }

        /// <inheritdoc/>
        ID3D12Resource* GraphicsResourceHelper.IGraphicsResource.ValidateAndGetID3D12Resource(GraphicsDevice device)
        {
            using var _0 = GetReferenceTrackingLease();
            using var _1 = this.owner.GetReferenceTrackingLease();

            this.owner.ThrowIfDeviceMismatch(device);

            return this.owner.D3D12Resource;
        }

        /// <inheritdoc/>
        (D3D12_RESOURCE_STATES, D3D12_RESOURCE_STATES) GraphicsResourceHelper.IGraphicsResource.ValidateAndGetID3D12ResourceAndTransitionStates(GraphicsDevice device, ResourceState resourceState, out ID3D12Resource* d3D12Resource)
        {
            throw new NotSupportedException("This operation cannot be performaned on a readonly wrapper.");
        }

        /// <inheritdoc/>
        private protected override void OnDispose()
        {
            if (this.owner.GraphicsDevice is GraphicsDevice device)
            {
                device.ReturnShaderResourceViewDescriptorHandles(in this.d3D12ResourceDescriptorHandles);
            }
        }
    }
}
