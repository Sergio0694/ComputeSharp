// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d3d12.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;

namespace TerraFX.Interop.DirectX
{
    [Flags]
    public enum D3D12_RESOURCE_FLAGS
    {
        D3D12_RESOURCE_FLAG_NONE = 0,
        D3D12_RESOURCE_FLAG_ALLOW_RENDER_TARGET = 0x1,
        D3D12_RESOURCE_FLAG_ALLOW_DEPTH_STENCIL = 0x2,
        D3D12_RESOURCE_FLAG_ALLOW_UNORDERED_ACCESS = 0x4,
        D3D12_RESOURCE_FLAG_DENY_SHADER_RESOURCE = 0x8,
        D3D12_RESOURCE_FLAG_ALLOW_CROSS_ADAPTER = 0x10,
        D3D12_RESOURCE_FLAG_ALLOW_SIMULTANEOUS_ACCESS = 0x20,
        D3D12_RESOURCE_FLAG_VIDEO_DECODE_REFERENCE_ONLY = 0x40,
        D3D12_RESOURCE_FLAG_VIDEO_ENCODE_REFERENCE_ONLY = 0x80,
    }
}
