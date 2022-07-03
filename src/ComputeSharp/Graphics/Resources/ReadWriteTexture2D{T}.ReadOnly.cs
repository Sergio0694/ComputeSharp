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
partial class ReadWriteTexture2D<T>
{
    /// <summary>
    /// The wrapping <see cref="ReadOnly"/> instance, if available.
    /// </summary>
    private ReadOnly? readOnlyWrapper;

    /// <inheritdoc cref="ReadWriteTexture2DExtensions.AsReadOnly(ReadWriteTexture2D{float})"/>
    public IReadOnlyTexture2D<T> AsReadOnly()
    {
        GraphicsDevice.ThrowIfDisposed();
        GraphicsDevice.ThrowIfDeviceLost();

        ThrowIfDisposed();
        ThrowIfIsNotInReadOnlyState();

        ReadOnly? readOnlyWrapper = this.readOnlyWrapper;

        if (readOnlyWrapper is not null)
        {
            return readOnlyWrapper;
        }

        // The wrapper initialization is moved to a non inlined helper to keep the codegen of this method
        // as compact as possible. If the current wrapper is not initialized, control goes directly to the
        // helper that will initialize and return it, which translates to a single jump.
        [MethodImpl(MethodImplOptions.NoInlining)]
        static ReadOnly InitializeWrapper(ReadWriteTexture2D<T> texture)
        {
            return texture.readOnlyWrapper = new(texture);
        }

        return InitializeWrapper(this);
    }

    /// <inheritdoc/>
    protected override void OnDispose()
    {
        base.OnDispose();

        this.readOnlyWrapper?.Dispose();
    }

    /// <summary>
    /// A wrapper for a <see cref="ReadWriteTexture2D{T}"/> resource that has been temporarily transitioned to readonly.
    /// </summary>
    private sealed unsafe class ReadOnly : NativeObject, IReadOnlyTexture2D<T>, GraphicsResourceHelper.IGraphicsResource
    {
        /// <summary>
        /// The owning <see cref="ReadWriteTexture2D{T}"/> instance being wrapped.
        /// </summary>
        private readonly ReadWriteTexture2D<T> owner;

        /// <summary>
        /// The <see cref="ID3D12ResourceDescriptorHandles"/> instance for the current resource.
        /// </summary>
        private readonly ID3D12ResourceDescriptorHandles d3D12ResourceDescriptorHandles;

        /// <summary>
        /// Creates a new <see cref="ReadOnly"/> instance with the specified parameters.
        /// </summary>
        /// <param name="owner">The owning <see cref="ReadWriteTexture2D{T}"/> instance to wrap.</param>
        public ReadOnly(ReadWriteTexture2D<T> owner)
        {
            this.owner = owner;

            owner.GraphicsDevice.RentShaderResourceViewDescriptorHandles(out this.d3D12ResourceDescriptorHandles);

            owner.GraphicsDevice.D3D12Device->CreateShaderResourceView(owner.D3D12Resource, DXGIFormatHelper.GetForType<T>(), D3D12_SRV_DIMENSION_TEXTURE2D, this.d3D12ResourceDescriptorHandles.D3D12CpuDescriptorHandle);
        }

        /// <inheritdoc/>
        public ref readonly T this[int x, int y] => throw new InvalidExecutionContextException($"{typeof(ReadWriteTexture2D<T>.ReadOnly)}[{typeof(int)}, {typeof(int)}]");

        /// <inheritdoc/>
        public ref readonly T this[Int2 xy] => throw new InvalidExecutionContextException($"{typeof(ReadWriteTexture2D<T>.ReadOnly)}[{typeof(Int2)}]");

        /// <inheritdoc/>
        public ref readonly T this[float u, float v] => throw new InvalidExecutionContextException($"{typeof(ReadWriteTexture2D<T>.ReadOnly)}[{typeof(float)}, {typeof(float)}]");

        /// <inheritdoc/>
        public ref readonly T this[Float2 uv] => throw new InvalidExecutionContextException($"{typeof(ReadWriteTexture2D<T>.ReadOnly)}[{typeof(Float2)}]");

        /// <inheritdoc/>
        public int Width => this.owner.Width;

        /// <inheritdoc/>
        public int Height => this.owner.Height;

        /// <inheritdoc/>
        public GraphicsDevice GraphicsDevice => this.owner.GraphicsDevice;

        /// <inheritdoc/>
        D3D12_GPU_DESCRIPTOR_HANDLE GraphicsResourceHelper.IGraphicsResource.ValidateAndGetGpuDescriptorHandle(GraphicsDevice device)
        {
            ThrowIfDisposed();

            this.owner.ThrowIfDisposed();
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
            ThrowIfDisposed();

            this.owner.ThrowIfDisposed();
            this.owner.ThrowIfDeviceMismatch(device);

            return this.owner.D3D12Resource;
        }

        /// <inheritdoc/>
        (D3D12_RESOURCE_STATES, D3D12_RESOURCE_STATES) GraphicsResourceHelper.IGraphicsResource.ValidateAndGetID3D12ResourceAndTransitionStates(GraphicsDevice device, ResourceState resourceState, out ID3D12Resource* d3D12Resource)
        {
            throw new NotSupportedException("This operation cannot be performaned on a readonly wrapper.");
        }

        /// <inheritdoc/>
        protected override void OnDispose()
        {
            if (this.owner.GraphicsDevice is GraphicsDevice device)
            {
                device.ReturnShaderResourceViewDescriptorHandles(in this.d3D12ResourceDescriptorHandles);
            }
        }
    }
}
