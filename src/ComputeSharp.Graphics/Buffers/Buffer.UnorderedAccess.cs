using System;

namespace ComputeSharp.Graphics.Buffers
{
    public partial class Buffer
    {
        public static class UnorderedAccess
        {
            public static unsafe Buffers.Buffer New(GraphicsDevice device, int size, GraphicsHeapType heapType = GraphicsHeapType.Default)
            {
                return Buffers.Buffer.New(device, size, BufferFlags.UnorderedAccess, heapType);
            }

            public static unsafe Buffer<T> New<T>(GraphicsDevice device, in T data, GraphicsHeapType heapType = GraphicsHeapType.Default) where T : unmanaged
            {
                return Buffers.Buffer.New(device, data, BufferFlags.UnorderedAccess, heapType);
            }

            public static unsafe Buffer<T> New<T>(GraphicsDevice device, Span<T> data, GraphicsHeapType heapType = GraphicsHeapType.Default) where T : unmanaged
            {
                return Buffers.Buffer.New(device, data, BufferFlags.UnorderedAccess, heapType);
            }
        }
    }
}