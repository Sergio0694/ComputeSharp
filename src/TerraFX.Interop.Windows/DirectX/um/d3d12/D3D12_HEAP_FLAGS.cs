// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d3d12.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;

namespace TerraFX.Interop.DirectX
{
    [Flags]
    public enum D3D12_HEAP_FLAGS
    {
        D3D12_HEAP_FLAG_NONE = 0,
        D3D12_HEAP_FLAG_SHARED = 0x1,
        D3D12_HEAP_FLAG_DENY_BUFFERS = 0x4,
        D3D12_HEAP_FLAG_ALLOW_DISPLAY = 0x8,
        D3D12_HEAP_FLAG_SHARED_CROSS_ADAPTER = 0x20,
        D3D12_HEAP_FLAG_DENY_RT_DS_TEXTURES = 0x40,
        D3D12_HEAP_FLAG_DENY_NON_RT_DS_TEXTURES = 0x80,
        D3D12_HEAP_FLAG_HARDWARE_PROTECTED = 0x100,
        D3D12_HEAP_FLAG_ALLOW_WRITE_WATCH = 0x200,
        D3D12_HEAP_FLAG_ALLOW_SHADER_ATOMICS = 0x400,
        D3D12_HEAP_FLAG_CREATE_NOT_RESIDENT = 0x800,
        D3D12_HEAP_FLAG_CREATE_NOT_ZEROED = 0x1000,
        D3D12_HEAP_FLAG_ALLOW_ALL_BUFFERS_AND_TEXTURES = 0,
        D3D12_HEAP_FLAG_ALLOW_ONLY_BUFFERS = 0xc0,
        D3D12_HEAP_FLAG_ALLOW_ONLY_NON_RT_DS_TEXTURES = 0x44,
        D3D12_HEAP_FLAG_ALLOW_ONLY_RT_DS_TEXTURES = 0x84,
    }
}
