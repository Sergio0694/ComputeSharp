using System;
using System.Runtime.CompilerServices;
using SharpDX.Direct3D12;

namespace DirectX12GameEngine.Graphics
{
    public partial class Buffer
    {
        public static class Vertex
        {
            public static unsafe Buffer New(GraphicsDevice device, int size, GraphicsHeapType heapType = GraphicsHeapType.Default)
            {
                return Buffer.New(device, size, BufferFlags.VertexBuffer, heapType);
            }

            public static unsafe Buffer<T> New<T>(GraphicsDevice device, in T data, GraphicsHeapType heapType = GraphicsHeapType.Default) where T : unmanaged
            {
                return Buffer.New(device, data, BufferFlags.VertexBuffer, heapType);
            }

            public static unsafe Buffer<T> New<T>(GraphicsDevice device, Span<T> data, GraphicsHeapType heapType = GraphicsHeapType.Default) where T : unmanaged
            {
                return Buffer.New(device, data, BufferFlags.VertexBuffer, heapType);
            }

            public static VertexBufferView CreateVertexBufferView(Buffer vertexBuffer, int size, int stride)
            {
                return new VertexBufferView
                {
                    BufferLocation = vertexBuffer.NativeResource.GPUVirtualAddress,
                    StrideInBytes = stride,
                    SizeInBytes = size
                };
            }

            public static unsafe VertexBufferView CreateVertexBufferView<T>(Buffer vertexBuffer, int size) where T : unmanaged
            {
                return CreateVertexBufferView(vertexBuffer, size, Unsafe.SizeOf<T>());
            }
        }
    }
}
