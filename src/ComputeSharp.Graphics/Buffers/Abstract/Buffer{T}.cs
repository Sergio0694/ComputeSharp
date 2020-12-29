using System.Buffers;
using System.Runtime.CompilerServices;
using ComputeSharp.Exceptions;
using ComputeSharp.Graphics.Buffers.Enums;
using ComputeSharp.Graphics.Extensions;
using ComputeSharp.Graphics.Interop;
using Microsoft.Toolkit.Diagnostics;
using TerraFX.Interop;
using FX = TerraFX.Interop.Windows;
#if NET5_0
using MemoryMarshal = System.Runtime.InteropServices.MemoryMarshal;
#else
using MemoryMarshal = ComputeSharp.System.Runtime.InteropServices.MemoryMarshal;
#endif

namespace ComputeSharp.Graphics.Buffers.Abstract
{
    /// <summary>
    /// A <see langword="class"/> representing a typed buffer stored on GPU memory.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the buffer.</typeparam>
    public unsafe abstract class Buffer<T> : NativeObject
        where T : unmanaged
    {
        /// <summary>
        /// The <see cref="ID3D12Resource"/> instance currently mapped.
        /// </summary>
        private ComPtr<ID3D12Resource> d3D12Resource;

        /// <summary>
        /// The <see cref="D3D12_CPU_DESCRIPTOR_HANDLE"/> instance for the current resource.
        /// </summary>
        internal readonly D3D12_CPU_DESCRIPTOR_HANDLE D3D12CpuDescriptorHandle;

        /// <summary>
        /// The <see cref="D3D12_GPU_DESCRIPTOR_HANDLE"/> instance for the current resource.
        /// </summary>
        /// <remarks>This field is dynamically accessed when loading shader dispatch data.</remarks>
        internal readonly D3D12_GPU_DESCRIPTOR_HANDLE D3D12GpuDescriptorHandle;

        /// <summary>
        /// The size in bytes of the current buffer (this value is never negative).
        /// </summary>
        protected readonly nint SizeInBytes;

        /// <summary>
        /// Creates a new <see cref="Buffer{T}"/> instance with the specified parameters.
        /// </summary>
        /// <param name="device">The <see cref="GraphicsDevice"/> associated with the current instance.</param>
        /// <param name="length">The number of items to store in the current buffer.</param>
        /// <param name="elementSizeInBytes">The size in bytes of each buffer item (including padding, if any).</param>
        /// <param name="resourceType">The resource type for the current buffer.</param>
        private protected Buffer(GraphicsDevice device, int length, uint elementSizeInBytes, ResourceType resourceType)
        {
            device.ThrowIfDisposed();

            if (resourceType == ResourceType.Constant)
            {
                Guard.IsBetweenOrEqualTo(length, 1, FX.D3D12_REQ_CONSTANT_BUFFER_ELEMENT_COUNT, nameof(length));
            }
            else
            {
                // The maximum length is set such that the aligned buffer size can't exceed uint.MaxValue
                Guard.IsBetweenOrEqualTo(length, 1, (uint.MaxValue / elementSizeInBytes) & ~255, nameof(length));
            }

            SizeInBytes = checked((nint)(length * elementSizeInBytes));
            GraphicsDevice = device;
            Length = length;

            this.d3D12Resource = device.D3D12Device->CreateCommittedResource(resourceType, (ulong)SizeInBytes, device.IsCacheCoherentUMA);

            device.RentShaderResourceViewDescriptorHandles(out D3D12CpuDescriptorHandle, out D3D12GpuDescriptorHandle);

            switch (resourceType)
            {
                case ResourceType.Constant:
                    device.D3D12Device->CreateConstantBufferView(this.d3D12Resource, SizeInBytes, D3D12CpuDescriptorHandle);
                    break;
                case ResourceType.ReadOnly:
                    device.D3D12Device->CreateShaderResourceView(this.d3D12Resource, (uint)length, elementSizeInBytes, D3D12CpuDescriptorHandle);
                    break;
                case ResourceType.ReadWrite:
                    device.D3D12Device->CreateUnorderedAccessView(this.d3D12Resource, (uint)length, elementSizeInBytes, D3D12CpuDescriptorHandle);
                    break;
            }
        }

        /// <summary>
        /// Gets the <see cref="Graphics.GraphicsDevice"/> associated with the current instance.
        /// </summary>
        public GraphicsDevice GraphicsDevice { get; }

        /// <summary>
        /// Gets the length of the current buffer.
        /// </summary>
        public int Length { get; }

        /// <summary>
        /// Gets whether or not there is some padding between elements in the current buffer.
        /// </summary>
        internal bool IsPaddingPresent
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => SizeInBytes > (nint)Length * sizeof(T);
        }

        /// <summary>
        /// Gets the <see cref="ID3D12Resource"/> instance currently mapped.
        /// </summary>
        internal ID3D12Resource* D3D12Resource => this.d3D12Resource;

        /// <summary>
        /// Reads the contents of the specified range from the current <see cref="Buffer{T}"/> instance and writes them into a target memory area.
        /// </summary>
        /// <param name="destination">The input memory area to write data to.</param>
        /// <param name="size">The size of the memory area to write data to.</param>
        /// <param name="offset">The offset to start reading data from.</param>
        internal abstract void GetData(ref T destination, int size, int offset);

        /// <summary>
        /// Writes the contents of a given memory area to a specified area of the current <see cref="Buffer{T}"/> instance.
        /// </summary>
        /// <param name="source">The input memory area to read data from.</param>
        /// <param name="size">The size of the input memory area to read data from.</param>
        /// <param name="offset">The offset to start writing data to.</param>
        internal abstract void SetData(ref T source, int size, int offset);

        /// <summary>
        /// Writes the contents of a given <see cref="Buffer{T}"/> to the current <see cref="Buffer{T}"/> instance.
        /// </summary>
        /// <param name="source">The input <see cref="Buffer{T}"/> to read data from.</param>
        public abstract void SetData(Buffer<T> source);

        /// <summary>
        /// Writes the contents of a given <see cref="Buffer{T}"/> to the current <see cref="Buffer{T}"/> instance, using a temporary CPU buffer.
        /// </summary>
        /// <param name="source">The input <see cref="Buffer{T}"/> to read data from.</param>
        protected void SetDataWithCpuBuffer(Buffer<T> source)
        {
            T[] array = ArrayPool<T>.Shared.Rent(source.Length);

            try
            {
                ref T r0 = ref MemoryMarshal.GetArrayDataReference(array);

                source.GetData(ref r0, source.Length, 0);

                SetData(ref r0, source.Length, 0);
            }
            finally
            {
                ArrayPool<T>.Shared.Return(array);
            }
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
    }
}
