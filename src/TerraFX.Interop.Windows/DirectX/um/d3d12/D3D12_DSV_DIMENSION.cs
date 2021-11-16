// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d3d12.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

namespace TerraFX.Interop.DirectX
{
    public enum D3D12_DSV_DIMENSION
    {
        D3D12_DSV_DIMENSION_UNKNOWN = 0,
        D3D12_DSV_DIMENSION_TEXTURE1D = 1,
        D3D12_DSV_DIMENSION_TEXTURE1DARRAY = 2,
        D3D12_DSV_DIMENSION_TEXTURE2D = 3,
        D3D12_DSV_DIMENSION_TEXTURE2DARRAY = 4,
        D3D12_DSV_DIMENSION_TEXTURE2DMS = 5,
        D3D12_DSV_DIMENSION_TEXTURE2DMSARRAY = 6,
    }
}
