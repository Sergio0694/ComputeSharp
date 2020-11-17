using System.Runtime.CompilerServices;
using ComputeSharp.Graphics.Buffers.Abstract;
using ComputeSharp.Graphics.Buffers.Enums;
using ComputeSharp.Graphics.Extensions;
using TerraFX.Interop;
using static TerraFX.Interop.D3D12_HEAP_TYPE;
using static TerraFX.Interop.D3D12_RESOURCE_FLAGS;
using static TerraFX.Interop.D3D12_RESOURCE_STATES;
using static TerraFX.Interop.D3D12_SRV_DIMENSION;
using static TerraFX.Interop.D3D12_UAV_DIMENSION;
using FX = TerraFX.Interop.Windows;

namespace ComputeSharp.Graphics.Buffers
{
    /// <summary>
    /// A <see langword="class"/> representing a typed buffer stored on GPU memory.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the buffer.</typeparam>
    public unsafe class Buffer<T> : GraphicsResource
        where T : unmanaged
    {
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
            : base(device, CreateCommittedResource(device.D3D12Device, bufferType, sizeInBytes))
        {
            Size = size;
            SizeInBytes = sizeInBytes;
            ElementSizeInBytes = Unsafe.SizeOf<T>();
            BufferType = bufferType;
            PaddedElementSizeInBytes = sizeInBytes / size;

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
        /// Gets the size of the current buffer, as in the number of <typeparamref name="T"/> values it contains
        /// </summary>
        public int Size { get; }

        /// <summary>
        /// Gets whether or not there is some padding between elements in the current buffer
        /// </summary>
        internal bool IsPaddingPresent => PaddedElementSizeInBytes > ElementSizeInBytes;

        /// <summary>
        /// Creates a committed resource for a given buffer type.
        /// </summary>
        /// <param name="d3D12Device">The <see cref="ID3D12Device"/> instance in use.</param>
        /// <param name="bufferType">The buffer type currently in use.</param>
        /// <param name="sizeInBytes">The size in bytes of the current buffer.</param>
        /// <returns>An <see cref="ID3D12Resource"/> reference for the current buffer.</returns>
        private static ID3D12Resource* CreateCommittedResource(ID3D12Device* d3D12Device, BufferType bufferType, int sizeInBytes)
        {
            (D3D12_HEAP_TYPE d3D12HeapType,
             D3D12_RESOURCE_FLAGS d3D12ResourceFlags,
             D3D12_RESOURCE_STATES d3D12ResourceStates) = bufferType switch
            {
                BufferType.Constant => (D3D12_HEAP_TYPE_UPLOAD, D3D12_RESOURCE_FLAG_NONE, D3D12_RESOURCE_STATE_GENERIC_READ),
                BufferType.ReadOnly => (D3D12_HEAP_TYPE_DEFAULT, D3D12_RESOURCE_FLAG_NONE, D3D12_RESOURCE_STATE_COMMON),
                BufferType.ReadWrite => (D3D12_HEAP_TYPE_DEFAULT, D3D12_RESOURCE_FLAG_ALLOW_UNORDERED_ACCESS, D3D12_RESOURCE_STATE_COMMON),
                BufferType.ReadBack => (D3D12_HEAP_TYPE_READBACK, D3D12_RESOURCE_FLAG_NONE, D3D12_RESOURCE_STATE_COPY_DEST),
                BufferType.Transfer => (D3D12_HEAP_TYPE_UPLOAD, D3D12_RESOURCE_FLAG_NONE, D3D12_RESOURCE_STATE_GENERIC_READ),
                _ => throw null!
            };

            return d3D12Device->CreateCommittedResource(d3D12HeapType, (uint)sizeInBytes, d3D12ResourceFlags, d3D12ResourceStates);
        }

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
    }
}
