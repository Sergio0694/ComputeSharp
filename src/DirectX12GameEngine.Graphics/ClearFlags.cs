using System;

namespace DirectX12GameEngine.Graphics
{
    [Flags]
    public enum ClearFlags
    {
        None = 0,
        FlagsDepth = 1,
        FlagsStencil = 2
    }
}
