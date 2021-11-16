// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d3d12.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

namespace TerraFX.Interop.DirectX
{
    public enum D3D12_COMPARISON_FUNC
    {
        D3D12_COMPARISON_FUNC_NEVER = 1,
        D3D12_COMPARISON_FUNC_LESS = 2,
        D3D12_COMPARISON_FUNC_EQUAL = 3,
        D3D12_COMPARISON_FUNC_LESS_EQUAL = 4,
        D3D12_COMPARISON_FUNC_GREATER = 5,
        D3D12_COMPARISON_FUNC_NOT_EQUAL = 6,
        D3D12_COMPARISON_FUNC_GREATER_EQUAL = 7,
        D3D12_COMPARISON_FUNC_ALWAYS = 8,
    }
}
