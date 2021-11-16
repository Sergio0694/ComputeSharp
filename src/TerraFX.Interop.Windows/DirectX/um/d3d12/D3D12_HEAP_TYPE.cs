// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d3d12.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

namespace TerraFX.Interop.DirectX
{
    public enum D3D12_HEAP_TYPE
    {
        D3D12_HEAP_TYPE_DEFAULT = 1,
        D3D12_HEAP_TYPE_UPLOAD = 2,
        D3D12_HEAP_TYPE_READBACK = 3,
        D3D12_HEAP_TYPE_CUSTOM = 4,
    }
}
