using System.Runtime.CompilerServices;
using ComputeSharp.Core.Interop;
using ComputeSharp.Graphics.Buffers.Enums;
using ComputeSharp.Graphics.Extensions;
using TerraFX.Interop;
using static TerraFX.Interop.D3D12_SRV_DIMENSION;
using static TerraFX.Interop.D3D12_UAV_DIMENSION;
using FX = TerraFX.Interop.Windows;

namespace ComputeSharp.Graphics.Buffers
{
    /// <summary>
    /// A <see langword="class"/> representing a typed buffer stored on GPU memory.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the buffer.</typeparam>
    public unsafe class Buffer<T> : NativeObject
        where T : unmanaged
    {
        /// <summary>
        /// The <see cref="ID3D12Resource"/> instance currently mapped.
        /// </summary>
        private ComPtr<ID3D12Resource> d3D12Resource;

        /// <summary>
        /// The <see cref="D3D12_CPU_DESCRIPTOR_HANDLE"/> instance for the current resource.
        /// </summary>
        protected internal readonly D3D12_CPU_DESCRIPTOR_HANDLE D3D12CpuDescriptorHandle;

        /// <summary>
        /// The <see cref="D3D12_GPU_DESCRIPTOR_HANDLE"/> instance for the current resource.
        /// </summary>
        protected internal readonly D3D12_GPU_DESCRIPTOR_HANDLE D3D12GpuDescriptorHandle;

        /// <summary>
        /// The size in bytes of the current buffer.
        /// </summary>
        protected readonly int SizeInBytes;

        /// <summary>
        /// The size in bytes of each <typeparamref name="T"/> value contained in the buffer.
        /// </summary>
        protected readonly int ElementSizeInBytes;

        /// <summary>
        /// The size in bytes of the current buffer.
        /// </summary>
        internal readonly int PaddedElementSizeInBytes;

        /// <summary>
        /// The buffer type for the current <see cref="Buffer{T}"/> instance.
        /// </summary>
        internal readonly BufferType BufferType;

        /// <summary>
        /// Creates a new <see cref="Buffer{T}"/> instance with the specified parameters
        /// </summary>
        /// <param name="device">The <see cref="GraphicsDevice"/> associated with the current instance</param>
        /// <param name="size">The number of items to store in the current buffer</param>
        /// <param name="sizeInBytes">The size in bytes for the current buffer</param>
        /// <param name="bufferType">The buffer type for the current buffer</param>
        internal Buffer(GraphicsDevice device, int size, int sizeInBytes, BufferType bufferType)
        {
            this.d3D12Resource = device.D3D12Device->CreateCommittedResource(bufferType, sizeInBytes);

            Size = size;
            SizeInBytes = sizeInBytes;
            ElementSizeInBytes = Unsafe.SizeOf<T>();
            BufferType = bufferType;
            PaddedElementSizeInBytes = sizeInBytes / size;

            GraphicsDevice = device;

            if (bufferType is BufferType.Constant or BufferType.ReadOnly or BufferType.ReadWrite)
            {
                GraphicsDevice.AllocateShaderResourceViewDescriptorHandles(out D3D12CpuDescriptorHandle, out D3D12GpuDescriptorHandle);

                switch (bufferType)
                {
                    case BufferType.Constant: CreateConstantBufferView(); break;
                    case BufferType.ReadOnly: CreateShaderResourceView(); break;
                    case BufferType.ReadWrite: CreateUnorderedAccessView(); break;
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="GraphicsDevice"/> associated with the current instance.
        /// </summary>
        public GraphicsDevice GraphicsDevice { get; }

        /// <summary>
        /// Gets the size of the current buffer, as in the number of <typeparamref name="T"/> values it contains
        /// </summary>
        public int Size { get; }

        /// <summary>
        /// Gets whether or not there is some padding between elements in the current buffer
        /// </summary>
        internal bool IsPaddingPresent => PaddedElementSizeInBytes > ElementSizeInBytes;

        /// <summary>
        /// Gets the <see cref="ID3D12Resource"/> instance currently mapped.
        /// </summary>
        internal ID3D12Resource* D3D12Resource => this.d3D12Resource;

        /// <summary>
        /// Creates a view for a constant buffer.
        /// </summary>
        private void CreateConstantBufferView()
        {
            uint constantBufferSize = (uint)((SizeInBytes + 255) & ~255);

            D3D12_CONSTANT_BUFFER_VIEW_DESC d3D12ConstantBufferViewDescription;
            d3D12ConstantBufferViewDescription.BufferLocation = D3D12Resource->GetGPUVirtualAddress();
            d3D12ConstantBufferViewDescription.SizeInBytes = constantBufferSize;

            GraphicsDevice.D3D12Device->CreateConstantBufferView(&d3D12ConstantBufferViewDescription, D3D12CpuDescriptorHandle);
        }
        
        /// <summary>
        /// Creates a view for a readonly buffer.
        /// </summary>
        private void CreateShaderResourceView()
        {
            D3D12_SHADER_RESOURCE_VIEW_DESC d3D12ShaderResourceViewDescription = default;
            d3D12ShaderResourceViewDescription.ViewDimension = D3D12_SRV_DIMENSION_BUFFER;
            d3D12ShaderResourceViewDescription.Shader4ComponentMapping = FX.D3D12_DEFAULT_SHADER_4_COMPONENT_MAPPING;
            d3D12ShaderResourceViewDescription.Buffer.NumElements = (uint)Size;
            d3D12ShaderResourceViewDescription.Buffer.StructureByteStride = (uint)ElementSizeInBytes;

            GraphicsDevice.D3D12Device->CreateShaderResourceView(D3D12Resource, &d3D12ShaderResourceViewDescription, D3D12CpuDescriptorHandle);
        }

        /// <summary>
        /// Creates a view for a buffer than be both read and written to.
        /// </summary>
        private void CreateUnorderedAccessView()
        {
            D3D12_UNORDERED_ACCESS_VIEW_DESC d3D12UnorderedAccessViewDescription = default;
            d3D12UnorderedAccessViewDescription.ViewDimension = D3D12_UAV_DIMENSION_BUFFER;
            d3D12UnorderedAccessViewDescription.Buffer.NumElements = (uint)Size;
            d3D12UnorderedAccessViewDescription.Buffer.StructureByteStride = (uint)ElementSizeInBytes;

            GraphicsDevice.D3D12Device->CreateUnorderedAccessView(D3D12Resource, null, &d3D12UnorderedAccessViewDescription, D3D12CpuDescriptorHandle);
        }

        /// <inheritdoc/>
        protected override void OnDispose()
        {
            this.d3D12Resource.Dispose();
        }
    }
}
