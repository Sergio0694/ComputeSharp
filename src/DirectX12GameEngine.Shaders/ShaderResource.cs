using System;
using System.Numerics;

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
    public class RWBufferResource<T> : ShaderResource
    {
        public T this[uint index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

    [UnorderedAccessViewResource]
    public class RWTexture2DResource<T> : ShaderResource where T : unmanaged
    {
        public T this[Numerics.UInt2 index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
