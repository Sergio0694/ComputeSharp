// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d3d12.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

namespace TerraFX.Interop.DirectX
{
    public enum D3D12_QUERY_HEAP_TYPE
    {
        D3D12_QUERY_HEAP_TYPE_OCCLUSION = 0,
        D3D12_QUERY_HEAP_TYPE_TIMESTAMP = 1,
        D3D12_QUERY_HEAP_TYPE_PIPELINE_STATISTICS = 2,
        D3D12_QUERY_HEAP_TYPE_SO_STATISTICS = 3,
        D3D12_QUERY_HEAP_TYPE_VIDEO_DECODE_STATISTICS = 4,
        D3D12_QUERY_HEAP_TYPE_COPY_QUEUE_TIMESTAMP = 5,
        D3D12_QUERY_HEAP_TYPE_PIPELINE_STATISTICS1 = 7,
    }
}
