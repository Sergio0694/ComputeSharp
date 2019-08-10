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

            public static IndexBufferView CreateIndexBufferView(Buffer indexBuffer, PixelFormat format, int size)
            {
                switch (format)
                {
                    case PixelFormat.R16_UInt:
                    case PixelFormat.R32_UInt:
                        break;
                    default:
                        throw new NotSupportedException("Index buffer type must be ushort or uint");
                }

                return new IndexBufferView
                {
                    BufferLocation = indexBuffer.NativeResource.GPUVirtualAddress,
                    Format = (Format)format,
                    SizeInBytes = size
                };
            }
        }
    }
}
