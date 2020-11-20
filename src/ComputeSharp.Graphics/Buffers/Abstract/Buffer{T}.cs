using System;
using System.Buffers;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using ComputeSharp.Core.Interop;
using ComputeSharp.Graphics.Buffers.Enums;
using ComputeSharp.Graphics.Extensions;
using TerraFX.Interop;
using static TerraFX.Interop.D3D12_SRV_DIMENSION;
using static TerraFX.Interop.D3D12_UAV_DIMENSION;
using FX = TerraFX.Interop.Windows;

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
        /// The <see cref="D3D12_GPU_DESCRIPTOR_HANDLE"/> instance for the current resource.
        /// </summary>
        internal readonly D3D12_GPU_DESCRIPTOR_HANDLE D3D12GpuDescriptorHandle;

        /// <summary>
        /// The size in bytes of the current buffer.
        /// </summary>
        protected readonly int SizeInBytes;

        /// <summary>
        /// Creates a new <see cref="Buffer{T}"/> instance with the specified parameters.
        /// </summary>
        /// <param name="device">The <see cref="GraphicsDevice"/> associated with the current instance.</param>
        /// <param name="size">The number of items to store in the current buffer.</param>
        /// <param name="sizeInBytes">The size in bytes for the current buffer.</param>
        /// <param name="bufferType">The buffer type for the current buffer.</param>
        internal Buffer(GraphicsDevice device, int size, int sizeInBytes, BufferType bufferType)
        {
            this.d3D12Resource = device.D3D12Device->CreateCommittedResource(bufferType, sizeInBytes);

            SizeInBytes = sizeInBytes;
            GraphicsDevice = device;
            Size = size;

            GraphicsDevice.AllocateShaderResourceViewDescriptorHandles(out D3D12_CPU_DESCRIPTOR_HANDLE d3D12CpuDescriptorHandle, out D3D12GpuDescriptorHandle);

            switch (bufferType)
            {
                case BufferType.Constant: CreateConstantBufferView(d3D12CpuDescriptorHandle); break;
                case BufferType.ReadOnly: CreateShaderResourceView(d3D12CpuDescriptorHandle); break;
                case BufferType.ReadWrite: CreateUnorderedAccessView(d3D12CpuDescriptorHandle); break;
            }
        }

        /// <summary>
        /// Gets the <see cref="GraphicsDevice"/> associated with the current instance.
        /// </summary>
        public GraphicsDevice GraphicsDevice { get; }

        /// <summary>
        /// Gets the size of the current buffer, as in the number of <typeparamref name="T"/> values it contains.
        /// </summary>
        public int Size { get; }

        /// <summary>
        /// Gets the size in bytes of each <typeparamref name="T"/> value contained in the buffer.
        /// </summary>
        protected int ElementSizeInBytes
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Unsafe.SizeOf<T>();
        }

        /// <summary>
        /// Gets whether or not there is some padding between elements in the current buffer.
        /// </summary>
        internal bool IsPaddingPresent
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => SizeInBytes > Size * Unsafe.SizeOf<T>();
        }

        /// <summary>
        /// Gets the <see cref="ID3D12Resource"/> instance currently mapped.
        /// </summary>
        internal ID3D12Resource* D3D12Resource => this.d3D12Resource;

        /// <summary>
        /// Reads the contents of the current <see cref="Buffer{T}"/> instance and returns an array.
        /// </summary>
        /// <returns>A <typeparamref name="T"/> array with the contents of the current buffer.</returns>
        [Pure]
        public T[] GetData() => GetData(0, Size);

        /// <summary>
        /// Reads the contents of the current <see cref="Buffer{T}"/> instance in a given range and returns an array.
        /// </summary>
        /// <param name="offset">The offset to start reading data from.</param>
        /// <param name="count">The number of items to read.</param>
        /// <returns>A <typeparamref name="T"/> array with the contents of the specified range from the current buffer.</returns>
        [Pure]
        public T[] GetData(int offset, int count)
        {
            T[] data = new T[count];

            GetData(data, offset, count);

            return data;
        }

        /// <summary>
        /// Reads the contents of the current <see cref="Buffer{T}"/> instance and writes them into a target <see cref="Span{T}"/>.
        /// </summary>
        /// <param name="span">The input <see cref="Span{T}"/> to write data to.</param>
        public void GetData(Span<T> span) => GetData(span, 0, Size);

        /// <summary>
        /// Reads the contents of the specified range from the current <see cref="Buffer{T}"/> instance and writes them into a target <see cref="Span{T}"/>.
        /// </summary>
        /// <param name="span">The input <see cref="Span{T}"/> to write data to.</param>
        /// <param name="offset">The offset to start reading data from.</param>
        /// <param name="count">The number of items to read.</param>
        public abstract void GetData(Span<T> span, int offset, int count);

        /// <summary>
        /// Writes the contents of a given <typeparamref name="T"/> array to the current <see cref="Buffer{T}"/> instance.
        /// </summary>
        /// <param name="array">The input <typeparamref name="T"/> array to read data from.</param>
        public void SetData(T[] array) => SetData(array.AsSpan());

        /// <summary>
        /// Writes the contents of a given <typeparamref name="T"/> array to a specified area of the current <see cref="Buffer{T}"/> instance.
        /// </summary>
        /// <param name="array">The input <typeparamref name="T"/> array to read data from.</param>
        /// <param name="offset">The offset to start writing data to.</param>
        /// <param name="count">The number of items to write.</param>
        public void SetData(T[] array, int offset, int count) => SetData(array.AsSpan(), offset, count);

        /// <summary>
        /// Writes the contents of a given <see cref="ReadOnlySpan{T}"/> to the current <see cref="Buffer{T}"/> instance.
        /// </summary>
        /// <param name="span">The input <see cref="ReadOnlySpan{T}"/> to read data from.</param>
        public void SetData(ReadOnlySpan<T> span) => SetData(span, 0, Size);

        /// <summary>
        /// Writes the contents of a given <see cref="ReadOnlySpan{T}"/> to a specified area of the current <see cref="Buffer{T}"/> instance.
        /// </summary>
        /// <param name="span">The input <see cref="ReadOnlySpan{T}"/> to read data from.</param>
        /// <param name="offset">The offset to start writing data to.</param>
        /// <param name="count">The number of items to write.</param>
        public abstract void SetData(ReadOnlySpan<T> span, int offset, int count);

        /// <summary>
        /// Writes the contents of a given <see cref="Buffer{T}"/> to the current <see cref="Buffer{T}"/> instance.
        /// </summary>
        /// <param name="buffer">The input <see cref="Buffer{T}"/> to read data from.</param>
        public abstract void SetData(Buffer<T> buffer);

        /// <summary>
        /// Writes the contents of a given <see cref="Buffer{T}"/> to the current <see cref="Buffer{T}"/> instance, using a temporary CPU buffer.
        /// </summary>
        /// <param name="buffer">The input <see cref="Buffer{T}"/> to read data from.</param>
        protected void SetDataWithCpuBuffer(Buffer<T> buffer)
        {
            T[] array = ArrayPool<T>.Shared.Rent(buffer.Size);

            try
            {
                Span<T> span = array.AsSpan(0, buffer.Size);

                buffer.GetData(span);

                SetData(span);
            }
            finally
            {
                ArrayPool<T>.Shared.Return(array);
            }
        }

        /// <summary>
        /// Creates a view for a constant buffer.
        /// </summary>
        /// <param name="d3D12CpuDescriptorHandle">The <see cref="D3D12_CPU_DESCRIPTOR_HANDLE"/> instance for the current resource.</param>
        private void CreateConstantBufferView(D3D12_CPU_DESCRIPTOR_HANDLE d3D12CpuDescriptorHandle)
        {
            uint constantBufferSize = (uint)((SizeInBytes + 255) & ~255);

            D3D12_CONSTANT_BUFFER_VIEW_DESC d3D12ConstantBufferViewDescription;
            d3D12ConstantBufferViewDescription.BufferLocation = D3D12Resource->GetGPUVirtualAddress();
            d3D12ConstantBufferViewDescription.SizeInBytes = constantBufferSize;

            GraphicsDevice.D3D12Device->CreateConstantBufferView(&d3D12ConstantBufferViewDescription, d3D12CpuDescriptorHandle);
        }

        /// <summary>
        /// Creates a view for a readonly buffer.
        /// </summary>
        /// <param name="d3D12CpuDescriptorHandle">The <see cref="D3D12_CPU_DESCRIPTOR_HANDLE"/> instance for the current resource.</param>
        private void CreateShaderResourceView(D3D12_CPU_DESCRIPTOR_HANDLE d3D12CpuDescriptorHandle)
        {
            D3D12_SHADER_RESOURCE_VIEW_DESC d3D12ShaderResourceViewDescription = default;
            d3D12ShaderResourceViewDescription.ViewDimension = D3D12_SRV_DIMENSION_BUFFER;
            d3D12ShaderResourceViewDescription.Shader4ComponentMapping = FX.D3D12_DEFAULT_SHADER_4_COMPONENT_MAPPING;
            d3D12ShaderResourceViewDescription.Buffer.NumElements = (uint)Size;
            d3D12ShaderResourceViewDescription.Buffer.StructureByteStride = (uint)ElementSizeInBytes;

            GraphicsDevice.D3D12Device->CreateShaderResourceView(D3D12Resource, &d3D12ShaderResourceViewDescription, d3D12CpuDescriptorHandle);
        }

        /// <summary>
        /// Creates a view for a buffer than be both read and written to.
        /// </summary>
        /// <param name="d3D12CpuDescriptorHandle">The <see cref="D3D12_CPU_DESCRIPTOR_HANDLE"/> instance for the current resource.</param>
        private void CreateUnorderedAccessView(D3D12_CPU_DESCRIPTOR_HANDLE d3D12CpuDescriptorHandle)
        {
            D3D12_UNORDERED_ACCESS_VIEW_DESC d3D12UnorderedAccessViewDescription = default;
            d3D12UnorderedAccessViewDescription.ViewDimension = D3D12_UAV_DIMENSION_BUFFER;
            d3D12UnorderedAccessViewDescription.Buffer.NumElements = (uint)Size;
            d3D12UnorderedAccessViewDescription.Buffer.StructureByteStride = (uint)ElementSizeInBytes;

            GraphicsDevice.D3D12Device->CreateUnorderedAccessView(D3D12Resource, null, &d3D12UnorderedAccessViewDescription, d3D12CpuDescriptorHandle);
        }

        /// <inheritdoc/>
        protected override void OnDispose()
        {
            this.d3D12Resource.Dispose();
        }
    }
}
