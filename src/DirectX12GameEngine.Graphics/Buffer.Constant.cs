using System;

namespace DirectX12GameEngine.Graphics
{
    public partial class Buffer
    {
        public static class Constant
        {
            public static unsafe Buffer New(GraphicsDevice device, int size, GraphicsHeapType heapType = GraphicsHeapType.Upload)
            {
                return Buffer.New(device, size, BufferFlags.ConstantBuffer, heapType);
            }

            public static unsafe Buffer<T> New<T>(GraphicsDevice device, in T data, GraphicsHeapType heapType = GraphicsHeapType.Upload) where T : unmanaged
            {
                return Buffer.New(device, data, BufferFlags.ConstantBuffer, heapType);
            }

            public static unsafe Buffer<T> New<T>(GraphicsDevice device, Span<T> data, GraphicsHeapType heapType = GraphicsHeapType.Upload) where T : unmanaged
            {
                return Buffer.New(device, data, BufferFlags.ConstantBuffer, heapType);
            }
        }
    }
}
