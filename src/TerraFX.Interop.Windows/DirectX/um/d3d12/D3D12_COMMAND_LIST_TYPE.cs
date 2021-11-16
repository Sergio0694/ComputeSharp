// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d3d12.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

namespace TerraFX.Interop.DirectX
{
    public enum D3D12_COMMAND_LIST_TYPE
    {
        D3D12_COMMAND_LIST_TYPE_DIRECT = 0,
        D3D12_COMMAND_LIST_TYPE_BUNDLE = 1,
        D3D12_COMMAND_LIST_TYPE_COMPUTE = 2,
        D3D12_COMMAND_LIST_TYPE_COPY = 3,
        D3D12_COMMAND_LIST_TYPE_VIDEO_DECODE = 4,
        D3D12_COMMAND_LIST_TYPE_VIDEO_PROCESS = 5,
        D3D12_COMMAND_LIST_TYPE_VIDEO_ENCODE = 6,
    }
}
