using System;
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
        /// Gets the <see cref="IntPtr"/> associated to the currently mapped resource
        /// </summary>
        internal IntPtr MappedResource { get; private set; }

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
        /// <returns>An <see cref="IntPtr"/> for the newly mapped resource</returns>
        internal IntPtr Map()
        {
            IntPtr mappedResource = NativeResource?.Map(0) ?? throw new InvalidOperationException("Missing resource");
            MappedResource = mappedResource;
            return mappedResource;
        }

        /// <summary>
        /// Unmaps a subresource specified by a given index
        /// </summary>
        internal void Unmap()
        {
            NativeResource?.Unmap(0);
            MappedResource = IntPtr.Zero;
        }

        /// <inheritdoc/>
        public virtual void Dispose() => NativeResource?.Dispose();
    }
}
