using System;
using SharpDX.Direct3D12;
using SharpDX.DXGI;

namespace DirectX12GameEngine.Graphics
{
    public partial class Buffer
    {
        public static class Index
        {
            public static unsafe Buffer New(GraphicsDevice device, int size, GraphicsHeapType heapType = GraphicsHeapType.Default)
            {
                return Buffer.New(device, size, BufferFlags.IndexBuffer, heapType);
            }

            public static unsafe Buffer<T> New<T>(GraphicsDevice device, in T data, GraphicsHeapType heapType = GraphicsHeapType.Default) where T : unmanaged
            {
                return Buffer.New(device, data, BufferFlags.IndexBuffer, heapType);
            }

            public static unsafe Buffer<T> New<T>(GraphicsDevice device, Span<T> data, GraphicsHeapType heapType = GraphicsHeapType.Default) where T : unmanaged
            {
                return Buffer.New(device, data, BufferFlags.IndexBuffer, heapType);
            }
        }
    }
}
