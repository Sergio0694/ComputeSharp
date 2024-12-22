using System.Runtime.CompilerServices;
using ComputeSharp.Graphics.Commands.Interop;
using ComputeSharp.Graphics.Extensions;
using ComputeSharp.Graphics.Helpers;
using ComputeSharp.Interop;
using ComputeSharp.Resources.Interop;
using ComputeSharp.Win32;
using static ComputeSharp.Win32.D3D12_SRV_DIMENSION;

#pragma warning disable IDE0022

namespace ComputeSharp;

/// <inheritdoc/>
partial class ReadWriteTexture1D<T>
{
    /// <summary>
    /// The wrapping <see cref="ReadOnly"/> instance, if available.
    /// </summary>
    private ReadOnly? readOnlyWrapper;

    /// <inheritdoc cref="ReadWriteTexture1DExtensions.AsReadOnly(ReadWriteTexture1D{float})"/>
    public IReadOnlyTexture1D<T> AsReadOnly()
    {
        using ReferenceTracker.Lease _0 = GraphicsDevice.GetReferenceTracker().GetLease();
        using ReferenceTracker.Lease _1 = GetReferenceTracker().GetLease();

        GraphicsDevice.ThrowIfDeviceLost();

        ThrowIfIsNotInReadOnlyState();

        ReadOnly? readOnlyWrapper = this.readOnlyWrapper;

        if (readOnlyWrapper is not null)
        {
            return readOnlyWrapper;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        static ReadOnly InitializeWrapper(ReadWriteTexture1D<T> texture)
        {
            return texture.readOnlyWrapper = new(texture);
        }

        return InitializeWrapper(this);
    }

    /// <inheritdoc/>
    protected override void DangerousOnDispose()
    {
        base.DangerousOnDispose();

        this.readOnlyWrapper?.Dispose();
    }

    /// <summary>
    /// A wrapper for a <see cref="ReadWriteTexture1D{T}"/> resource that has been temporarily transitioned to readonly.
    /// </summary>
    private sealed unsafe class ReadOnly : ReferenceTrackedObject, IReadOnlyTexture1D<T>, ID3D12ReadOnlyResource
    {
        /// <summary>
        /// The owning <see cref="ReadWriteTexture1D{T}"/> instance being wrapped.
        /// </summary>
        private readonly ReadWriteTexture1D<T> owner;

        /// <summary>
        /// The <see cref="ID3D12ResourceDescriptorHandles"/> instance for the current resource.
        /// </summary>
        private readonly ID3D12ResourceDescriptorHandles d3D12ResourceDescriptorHandles;

        /// <summary>
        /// Creates a new <see cref="ReadOnly"/> instance with the specified parameters.
        /// </summary>
        /// <param name="owner">The owning <see cref="ReadWriteTexture1D{T}"/> instance to wrap.</param>
        public ReadOnly(ReadWriteTexture1D<T> owner)
        {
            using ReferenceTracker.Lease _0 = GetReferenceTracker().GetLease();

            owner.GetReferenceTracker().DangerousAddRef();

            this.owner = owner;

            owner.GraphicsDevice.RentShaderResourceViewDescriptorHandles(out this.d3D12ResourceDescriptorHandles);

            owner.GraphicsDevice.D3D12Device->CreateShaderResourceView(owner.D3D12Resource, DXGIFormatHelper.GetForType<T>(), D3D12_SRV_DIMENSION_TEXTURE1D, this.d3D12ResourceDescriptorHandles.D3D12CpuDescriptorHandle);
        }

        /// <inheritdoc/>
        public ref readonly T this[int x] => throw new InvalidExecutionContextException($"{typeof(ReadOnly)}[{typeof(int)}]");

        /// <inheritdoc/>
        public ref readonly T Sample(float u) => throw new InvalidExecutionContextException($"{typeof(ReadOnly)}.{nameof(Sample)}({typeof(float)})");

        /// <inheritdoc/>
        public int Width => this.owner.Width;

        /// <inheritdoc/>
        public GraphicsDevice GraphicsDevice => this.owner.GraphicsDevice;

        /// <inheritdoc/>
        D3D12_GPU_DESCRIPTOR_HANDLE ID3D12ReadOnlyResource.ValidateAndGetGpuDescriptorHandle(GraphicsDevice device)
        {
            using ReferenceTracker.Lease _0 = GetReferenceTracker().GetLease();
            using ReferenceTracker.Lease _1 = this.owner.GetReferenceTracker().GetLease();

            this.owner.ThrowIfDeviceMismatch(device);

            return this.d3D12ResourceDescriptorHandles.D3D12GpuDescriptorHandle;
        }

        /// <inheritdoc/>
        ID3D12Resource* ID3D12ReadOnlyResource.ValidateAndGetID3D12Resource(GraphicsDevice device, out ReferenceTracker.Lease lease)
        {
            lease = GetReferenceTracker().GetLease();

            using ReferenceTracker.Lease _1 = this.owner.GetReferenceTracker().GetLease();

            this.owner.ThrowIfDeviceMismatch(device);

            return this.owner.D3D12Resource;
        }

        /// <inheritdoc/>
        protected override void DangerousOnDispose()
        {
            this.owner.GetReferenceTracker().DangerousRelease();

            if (this.owner.GraphicsDevice is GraphicsDevice device)
            {
                device.ReturnShaderResourceViewDescriptorHandles(in this.d3D12ResourceDescriptorHandles);
            }
        }
    }
}