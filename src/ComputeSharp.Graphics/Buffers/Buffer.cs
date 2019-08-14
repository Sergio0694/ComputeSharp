using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using ComputeSharp.Graphics.Buffers.Abstract;
using SharpDX.Direct3D12;

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
        /// <param name="heapType">The heap type for the current buffer</param>
        protected internal Buffer(GraphicsDevice device, int size, int sizeInBytes, HeapType heapType) : base(device)
        {
            Size = size;
            SizeInBytes = sizeInBytes; // Not necessarily a multiple of the element size, as there could be padding
            ElementSizeInBytes = Unsafe.SizeOf<T>();
            HeapType = heapType;

            ResourceFlags flags = heapType == HeapType.Default ? ResourceFlags.AllowUnorderedAccess : ResourceFlags.None;
            ResourceDescription description = ResourceDescription.Buffer(SizeInBytes, flags);
            ResourceStates resourceStates = heapType switch
            {
                HeapType.Upload => ResourceStates.GenericRead,
                HeapType.Readback => ResourceStates.CopyDestination,
                _ => ResourceStates.Common
            };

            NativeResource = GraphicsDevice.NativeDevice.CreateCommittedResource(new HeapProperties(heapType), HeapFlags.None, description, resourceStates);

            (NativeCpuDescriptorHandle, NativeGpuDescriptorHandle) = heapType switch
            {
                HeapType.Default => CreateUnorderedAccessView(),
                HeapType.Upload => CreateConstantBufferView(),
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
        /// Gets the heap type being targeted by the current buffer
        /// </summary>
        internal HeapType HeapType { get; }

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
                Format = SharpDX.DXGI.Format.R32_Float,
                Dimension = UnorderedAccessViewDimension.Buffer,
                Buffer = { ElementCount = Size }
            };

            GraphicsDevice.NativeDevice.CreateUnorderedAccessView(NativeResource, null, description, cpuHandle);

            return (cpuHandle, gpuHandle);
        }
    }
}
