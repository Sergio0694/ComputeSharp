using ComputeSharp.Core.Interop;
using TerraFX.Interop;

namespace ComputeSharp.Graphics.Buffers.Abstract
{
    /// <summary>
    /// An abstract <see langword="class"/> that represents a graphics resource object.
    /// </summary>
    public abstract unsafe class GraphicsResource : NativeObject
    {
        /// <summary>
        /// The <see cref="ID3D12Resource"/> instance currently mapped.
        /// </summary>
        private ComPtr<ID3D12Resource> d3D12Resource;

        /// <summary>
        /// Creates a new <see cref="GraphicsResource"/> instance with the specified parameters.
        /// </summary>
        /// <param name="device">The <see cref="GraphicsDevice"/> associated with the current instance.</param>
        protected GraphicsResource(GraphicsDevice device, ComPtr<ID3D12Resource> d3d12resource)
        {
            this.d3D12Resource = d3d12resource;

            GraphicsDevice = device;
        }

        /// <summary>
        /// Gets the <see cref="GraphicsDevice"/> associated with the current instance.
        /// </summary>
        public GraphicsDevice GraphicsDevice { get; }

        /// <summary>
        /// Gets the <see cref="ID3D12Resource"/> instance currently mapped.
        /// </summary>
        internal ID3D12Resource* D3D12Resource => this.d3D12Resource;

        /// <inheritdoc/>
        protected override void OnDispose()
        {
            d3D12Resource.Dispose();
        }
    }
}
