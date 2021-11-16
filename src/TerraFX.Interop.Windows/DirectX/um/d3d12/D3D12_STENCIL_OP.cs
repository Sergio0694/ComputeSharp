// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d3d12.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

namespace TerraFX.Interop.DirectX
{
    public enum D3D12_STENCIL_OP
    {
        D3D12_STENCIL_OP_KEEP = 1,
        D3D12_STENCIL_OP_ZERO = 2,
        D3D12_STENCIL_OP_REPLACE = 3,
        D3D12_STENCIL_OP_INCR_SAT = 4,
        D3D12_STENCIL_OP_DECR_SAT = 5,
        D3D12_STENCIL_OP_INVERT = 6,
        D3D12_STENCIL_OP_INCR = 7,
        D3D12_STENCIL_OP_DECR = 8,
    }
}
