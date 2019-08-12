using System;
using ComputeSharp.Graphics.Buffers;

namespace ComputeSharp.Shaders
{
    public abstract class ShaderResource
    {
    }

    public sealed class RWBufferResource<T> : ShaderResource where T : unmanaged
    {
        internal RWBufferResource(Buffer<T> buffer) => Buffer = buffer;

        public Buffer<T> Buffer { get; }

        public T this[uint index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

    public static class RWBufferResourceExtensions
    {
        public static RWBufferResource<T> GetGpuResource<T>(this Buffer<T> buffer) where T : unmanaged
        {
            return new RWBufferResource<T>(buffer);
        }
    }
}
