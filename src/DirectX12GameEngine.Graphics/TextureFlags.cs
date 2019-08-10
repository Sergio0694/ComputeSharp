using System;

namespace DirectX12GameEngine.Graphics
{
    [Flags]
    public enum TextureFlags
    {
        None = 0,
        RenderTarget = 1,
        DepthStencil = 2,
        UnorderedAccess = 4,
        ShaderResource = 8
    }
}
