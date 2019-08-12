using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using ComputeSharp.Graphics.Buffers.Abstract;
using ComputeSharp.Graphics.Helpers;
using SharpDX.Direct3D12;

namespace ComputeSharp.Graphics.Buffers
{
    /// <summary>
    /// A <see langword="class"/> representing a typed buffer stored on GPU memory
    /// </summary>
    /// <typeparam name="T">The type of items stored on the buffer</typeparam>
    public sealed class Buffer2<T> : GraphicsResource where T : unmanaged
    {
        /// <summary>
        /// Creates a new <see cref="Buffer2{T}"/> instance with the specified parameters
        /// </summary>
        /// <param name="device">The <see cref="GraphicsDevice"/> associated with the current instance</param>
        /// <param name="size">The number of items to store in the current buffer</param>
        /// <param name="heapType">The heap type for the current buffer</param>
        internal Buffer2(GraphicsDevice device, int size, HeapType heapType) : base(device)
        {
            Size = size;
            ElementSizeInBytes = Unsafe.SizeOf<T>();
            SizeInBytes = Size * ElementSizeInBytes;
            HeapType = heapType;

            ResourceFlags flags = heapType == HeapType.Default ? ResourceFlags.AllowUnorderedAccess : ResourceFlags.None;
            ResourceDescription description = ResourceDescription.Buffer(ElementSizeInBytes, flags);
            ResourceStates resourceStates = heapType switch
            {
                HeapType.Upload => ResourceStates.GenericRead,
                HeapType.Readback => ResourceStates.CopyDestination,
                _ => ResourceStates.CopyDestination
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
        /// Creates the descriptors for a constant buffer
        /// </summary>
        /// <returns>The CPU and GPU handles for a constant buffer</returns>
        [Pure]
        private (CpuDescriptorHandle, GpuDescriptorHandle) CreateConstantBufferView()
        {
            (CpuDescriptorHandle cpuHandle, GpuDescriptorHandle gpuHandle) = GraphicsDevice.ShaderResourceViewAllocator.Allocate(1);

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
            (CpuDescriptorHandle cpuHandle, GpuDescriptorHandle gpuHandle) = GraphicsDevice.ShaderResourceViewAllocator.Allocate(1);

            UnorderedAccessViewDescription description = new UnorderedAccessViewDescription
            {
                Format = SharpDX.DXGI.Format.R32_Float,
                Dimension = UnorderedAccessViewDimension.Buffer,
                Buffer = { ElementCount = Size }
            };

            GraphicsDevice.NativeDevice.CreateUnorderedAccessView(NativeResource, null, description, cpuHandle);

            return (cpuHandle, gpuHandle);
        }

        /// <summary>
        /// Gets the size of the current buffer, as in the number of <typeparamref name="T"/> values it contains
        /// </summary>
        public int Size { get; }

        /// <summary>
        /// Gets the size in bytes of the current buffer
        /// </summary>
        public int SizeInBytes { get; }

        /// <summary>
        /// Gets the size in bytes of each <typeparamref name="T"/> value contained in the buffer
        /// </summary>
        public int ElementSizeInBytes { get; }

        /// <summary>
        /// Gets the heap type being targeted by the current buffer
        /// </summary>
        public HeapType HeapType { get; }

        /// <summary>
        /// Gets or sets a single value from the current buffer
        /// </summary>
        /// <param name="i">The index of the value to get or set</param>
        /// <remarks>This API can only be used from a compute shader, and will always throw if used anywhere else</remarks>
        public T this[uint i]
        {
            get => throw new InvalidOperationException("The indexer APIs can only be used from a compute shader");
            set => throw new InvalidOperationException("The indexer APIs can only be used from a compute shader");
        }

        /// <summary>
        /// Reads the contents of the current <see cref="Buffer2{T}"/> instance and returns an array
        /// </summary>
        /// <returns>A <typeparamref name="T"/> array with the contents of the current buffer</returns>
        [Pure]
        public T[] GetData()
        {
            T[] data = new T[Size];
            GetData(data);

            return data;
        }

        /// <summary>
        /// Reads the contents of the current <see cref="Buffer2{T}"/> instance and writes them into a target <see cref="Span{T}"/>
        /// </summary>
        /// <param name="span">The input <see cref="Span{T}"/> to write data to</param>
        public void GetData(Span<T> span)
        {
            // Create a temporary readback buffer if the current buffer type is read write (can't be read from directly)
            if (HeapType == HeapType.Default)
            {
                using Buffer2<T> readbackBaffer = new Buffer2<T>(GraphicsDevice, Size, HeapType.Readback);
                using CommandList copyCommandList = new CommandList(GraphicsDevice, CommandListType.Copy);

                copyCommandList.CopyBufferRegion(this, 0, readbackBaffer, 0, SizeInBytes);
                copyCommandList.Flush(true);

                readbackBaffer.GetData(span);
            }
            else
            {
                // Map and read the memory back directly
                Map(0);
                MemoryHelper.Copy(MappedResource, span);
                Unmap(0);
            }
        }

        /// <summary>
        /// Writes the contents of a given <see cref="Span{T}"/> to the current <see cref="Buffer2{T}"/> instance
        /// </summary>
        /// <param name="span">The input <see cref="Span{T}"/> to read data from</param>
        public void SetData(Span<T> span)
        {
            // Create a temporary upload buffer if the current buffer type is read write (can't be written to directly)
            if (HeapType == HeapType.Default)
            {
                using Buffer2<T> uploadBuffer = new Buffer2<T>(GraphicsDevice, Size, HeapType.Upload);
                using CommandList copyCommandList = new CommandList(GraphicsDevice, CommandListType.Copy);

                copyCommandList.CopyBufferRegion(uploadBuffer, 0, this, 0, SizeInBytes);
                copyCommandList.Flush(true);
            }
            else
            {
                // Map and copy the memory directly
                Map(0);
                MemoryHelper.Copy(span, MappedResource);
                Unmap(0);
            }
        }
    }
}
