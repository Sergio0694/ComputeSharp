using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.Graphics.Buffers.Abstract;
using ComputeSharp.Graphics.Buffers.Enums;
using SharpGen.Runtime;
using Vortice.Direct3D12;

namespace ComputeSharp.Graphics.Buffers
{
    /// <summary>
    /// A <see langword="class"/> representing a typed buffer stored on GPU memory
    /// </summary>
    /// <typeparam name="T">The type of items stored on the buffer</typeparam>
    public class Buffer<T> : GraphicsResource where T : unmanaged
    {
        /// <summary>
        /// Creates a new <see cref="Buffer{T}"/> instance with the specified parameters
        /// </summary>
        /// <param name="device">The <see cref="GraphicsDevice"/> associated with the current instance</param>
        /// <param name="size">The number of items to store in the current buffer</param>
        /// <param name="sizeInBytes">The size in bytes for the current buffer</param>
        /// <param name="bufferType">The buffer type for the current buffer</param>
        internal Buffer(GraphicsDevice device, int size, int sizeInBytes, BufferType bufferType) : base(device)
        {
            Size = size;
            SizeInBytes = sizeInBytes; // Not necessarily a multiple of the element size, as there could be padding
            ElementSizeInBytes = Unsafe.SizeOf<T>();
            BufferType = bufferType;
            PaddedElementSizeInBytes = sizeInBytes / size;

            // Determine the right heap type and flags
            (HeapType heapType, ResourceFlags flags, ResourceStates states) = bufferType switch
            {
                BufferType.Constant => (HeapType.Upload, ResourceFlags.None, ResourceStates.GenericRead),
                BufferType.ReadOnly => (HeapType.Default, ResourceFlags.None, ResourceStates.Common),
                BufferType.ReadWrite => (HeapType.Default, ResourceFlags.AllowUnorderedAccess, ResourceStates.Common),
                BufferType.ReadBack => (HeapType.Readback, ResourceFlags.None, ResourceStates.CopyDestination),
                BufferType.Transfer => (HeapType.Upload, ResourceFlags.None, ResourceStates.GenericRead),
                _ => throw new ArgumentException($"Invalid buffer type {bufferType}", nameof(bufferType))
            };

            // Create the native resource
            ResourceDescription description = ResourceDescription.Buffer(sizeInBytes, flags);
            Result result = GraphicsDevice.NativeDevice.CreateCommittedResource(new HeapProperties(heapType), HeapFlags.None, description, states, out ID3D12Resource? resource);

            if (result.Failure)
            {
                throw new COMException("Failed to create the committed resource", result.Code);
            }

            NativeResource = resource!;

            // Create the resource handles, if needed
            (NativeCpuDescriptorHandle, NativeGpuDescriptorHandle) = bufferType switch
            {
                BufferType.Constant => CreateConstantBufferView(),
                BufferType.ReadOnly => CreateShaderResourceView(),
                BufferType.ReadWrite => CreateUnorderedAccessView(),
                _ => default
            };
        }

        /// <summary>
        /// Gets the size of the current buffer, as in the number of <typeparamref name="T"/> values it contains
        /// </summary>
        public int Size { get; }

        /// <summary>
        /// Gets the size in bytes of the current buffer
        /// </summary>
        protected int SizeInBytes { get; }

        /// <summary>
        /// Gets the size in bytes of each <typeparamref name="T"/> value contained in the buffer
        /// </summary>
        protected int ElementSizeInBytes { get; }

        /// <summary>
        /// Gets the size in bytes of the current buffer
        /// </summary>
        internal int PaddedElementSizeInBytes { get; }

        /// <summary>
        /// Gets whether or not there is some padding between elements in the current buffer
        /// </summary>
        internal bool IsPaddingPresent => PaddedElementSizeInBytes > ElementSizeInBytes;

        /// <summary>
        /// Gets the buffer type for the current <see cref="Buffer{T}"/> instance
        /// </summary>
        internal BufferType BufferType { get; }

        /// <summary>
        /// Creates the descriptors for a constant buffer
        /// </summary>
        /// <returns>The CPU and GPU handles for a constant buffer</returns>
        [Pure]
        private (CpuDescriptorHandle, GpuDescriptorHandle) CreateConstantBufferView()
        {
            (CpuDescriptorHandle cpuHandle, GpuDescriptorHandle gpuHandle) = GraphicsDevice.ShaderResourceViewAllocator.Allocate();

            int constantBufferSize = (SizeInBytes + 255) & ~255;

            ConstantBufferViewDescription description = new ConstantBufferViewDescription
            {
                BufferLocation = NativeResource!.GPUVirtualAddress,
                SizeInBytes = constantBufferSize
            };

            GraphicsDevice.NativeDevice.CreateConstantBufferView(description, cpuHandle);

            return (cpuHandle, gpuHandle);
        }

        private (CpuDescriptorHandle, GpuDescriptorHandle) CreateShaderResourceView()
        {
            (CpuDescriptorHandle cpuHandle, GpuDescriptorHandle gpuHandle) = GraphicsDevice.ShaderResourceViewAllocator.Allocate();

            ShaderResourceViewDescription description = new ShaderResourceViewDescription
            {
                Shader4ComponentMapping = 5768,
                ViewDimension = ShaderResourceViewDimension.Buffer,
                Buffer = { NumElements = Size, StructureByteStride = ElementSizeInBytes }
            };

            GraphicsDevice.NativeDevice.CreateShaderResourceView(NativeResource, description, cpuHandle);

            return (cpuHandle, gpuHandle);
        }

        /// <summary>
        /// Creates the descriptors for a read write buffer
        /// </summary>
        /// <returns>The CPU and GPU handles for a read write buffer</returns>
        [Pure]
        private (CpuDescriptorHandle, GpuDescriptorHandle) CreateUnorderedAccessView()
        {
            (CpuDescriptorHandle cpuHandle, GpuDescriptorHandle gpuHandle) = GraphicsDevice.ShaderResourceViewAllocator.Allocate();

            UnorderedAccessViewDescription description = new UnorderedAccessViewDescription
            {
                ViewDimension = UnorderedAccessViewDimension.Buffer,
                Buffer = { NumElements = Size, StructureByteStride = ElementSizeInBytes }
            };

            GraphicsDevice.NativeDevice.CreateUnorderedAccessView(NativeResource, null, description, cpuHandle);

            return (cpuHandle, gpuHandle);
        }
    }
}
