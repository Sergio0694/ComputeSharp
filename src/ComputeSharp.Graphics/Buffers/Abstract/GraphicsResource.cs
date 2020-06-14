using System;
using System.Runtime.CompilerServices;
using Vortice.Direct3D12;

namespace ComputeSharp.Graphics.Buffers.Abstract
{
    /// <summary>
    /// An abstract <see langword="class"/> that represents a graphics resource object
    /// </summary>
    public abstract class GraphicsResource : IDisposable
    {
        /// <summary>
        /// Creates a new <see cref="GraphicsResource"/> instance with the specified parameters
        /// </summary>
        /// <param name="device">The <see cref="GraphicsDevice"/> associated with the current instance</param>
        protected GraphicsResource(GraphicsDevice device)
        {
            GraphicsDevice = device;
        }

        /// <summary>
        /// Gets the <see cref="GraphicsDevice"/> associated with the current instance
        /// </summary>
        public GraphicsDevice GraphicsDevice { get; }

        /// <summary>
        /// Gets the <see cref="ID3D12Resource"/> object currently mapped
        /// </summary>
        protected internal ID3D12Resource? NativeResource { get; set; }

        /// <summary>
        /// Gets the <see cref="CpuDescriptorHandle"/> instance for the current resource
        /// </summary>
        protected internal CpuDescriptorHandle? NativeCpuDescriptorHandle { get; protected set; }

        /// <summary>
        /// Gets the <see cref="GpuDescriptorHandle"/> instance for the current resource
        /// </summary>
        protected internal GpuDescriptorHandle? NativeGpuDescriptorHandle { get; protected set; }

        /// <summary>
        /// Maps the current resource to a specified subresource
        /// </summary>
        /// <returns>A <see cref="MappedResource"/> instance representing the mapped resource</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal MappedResource MapResource()
        {
            var resource = NativeResource ?? throw new InvalidOperationException("Missing resource");

            return new MappedResource(resource);
        }

        /// <inheritdoc/>
        public virtual void Dispose() => NativeResource?.Dispose();

        /// <summary>
        /// A type representing a mapped memory resource
        /// </summary>
        internal readonly ref struct MappedResource
        {
            /// <summary>
            /// The target <see cref="ID3D12Resource"/> to map
            /// </summary>
            private readonly ID3D12Resource Resource;

            /// <summary>
            /// The pointer to the mapped resource
            /// </summary>
            public readonly IntPtr Pointer;

            /// <summary>
            /// Creates a new <see cref="MappedResource"/> instance for a given <see cref="ID3D12Resource"/> value
            /// </summary>
            /// <param name="resource">The input <see cref="ID3D12Resource"/> instance to map</param>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public MappedResource(ID3D12Resource resource)
            {
                Resource = resource;
                Pointer = resource.Map(0);
            }

            /// <inheritdoc cref="IDisposable.Dispose"/>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void Dispose()
            {
                Resource.Unmap(0);
            }
        }
    }
}
