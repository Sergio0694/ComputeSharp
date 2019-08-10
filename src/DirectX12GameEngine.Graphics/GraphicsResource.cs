using System;
using SharpDX.Direct3D12;

namespace DirectX12GameEngine.Graphics
{
    public abstract class GraphicsResource : IDisposable
    {
        protected GraphicsResource()
        {
        }

        protected GraphicsResource(GraphicsDevice device)
        {
            AttachToGraphicsDevice(device);
        }

#nullable disable
        public GraphicsDevice GraphicsDevice { get; private set; }
#nullable restore

        public IntPtr MappedResource { get; private set; }

#nullable disable
        protected internal Resource NativeResource { get; set; }
#nullable restore

        protected internal CpuDescriptorHandle NativeCpuDescriptorHandle { get; protected set; }

        protected internal GpuDescriptorHandle NativeGpuDescriptorHandle { get; protected set; }

        public void AttachToGraphicsDevice(GraphicsDevice device)
        {
            GraphicsDevice = device;
        }

        public virtual void Dispose()
        {
            NativeResource.Dispose();
        }

        public IntPtr Map(int subresource)
        {
            IntPtr mappedResource = NativeResource.Map(subresource);
            MappedResource = mappedResource;
            return mappedResource;
        }

        public void Unmap(int subresource)
        {
            NativeResource.Unmap(subresource);
            MappedResource = IntPtr.Zero;
        }
    }
}
