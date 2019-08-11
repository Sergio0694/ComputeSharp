using System;
using System.Numerics;
using DirectX12GameEngine.Graphics.Buffers;
using Buffer = System.Buffer;

namespace DirectX12GameEngine.Shaders
{
    public abstract class ShaderResource
    {
    }

    [SamplerResource]
    public class SamplerResource : ShaderResource
    {
    }

    [SamplerResource]
    public class SamplerComparisonResource : ShaderResource
    {
    }

    [TextureResource]
    public class Texture2DResource : ShaderResource
    {
        public Vector4 Sample(SamplerResource sampler, Vector2 texCoord) => throw new NotImplementedException();
    }

    [TextureResource]
    public class Texture2DResource<T> : ShaderResource where T : unmanaged
    {
        public T Sample(SamplerResource sampler, Vector2 texCoord) => throw new NotImplementedException();
    }

    [TextureResource]
    public class Texture2DArrayResource : ShaderResource
    {
    }

    [TextureResource]
    public class TextureCubeResource : ShaderResource
    {
    }

    [UnorderedAccessViewResource]
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

    [UnorderedAccessViewResource]
    public class RWTexture2DResource<T> : ShaderResource where T : unmanaged
    {
        public T this[Numerics.UInt2 index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
