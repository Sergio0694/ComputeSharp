using System;

namespace DirectX12GameEngine.Graphics
{
    [Flags]
    public enum BufferFlags
    {
        None = 0,
        ConstantBuffer = 1,
        IndexBuffer = 2,
        VertexBuffer = 4,
        RenderTarget = 8,
        ShaderResource = 16,
        UnorderedAccess = 32,
        StructuredBuffer = 64,
        RawBuffer = 512,
        ArgumentBuffer = 1024
    }
}
