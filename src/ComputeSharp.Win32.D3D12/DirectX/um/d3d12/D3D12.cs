// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d3d12.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

namespace ComputeSharp.Win32;

internal static partial class D3D12
{
    [NativeTypeName("#define D3D12_COMMONSHADER_CONSTANT_BUFFER_PARTIAL_UPDATE_EXTENTS_BYTE_ALIGNMENT ( 16 )")]
    public const int D3D12_COMMONSHADER_CONSTANT_BUFFER_PARTIAL_UPDATE_EXTENTS_BYTE_ALIGNMENT = (16);

    [NativeTypeName("#define D3D12_CONSTANT_BUFFER_DATA_PLACEMENT_ALIGNMENT ( 256 )")]
    public const int D3D12_CONSTANT_BUFFER_DATA_PLACEMENT_ALIGNMENT = (256);

    [NativeTypeName("#define D3D12_DESCRIPTOR_RANGE_OFFSET_APPEND ( 0xffffffff )")]
    public const uint D3D12_DESCRIPTOR_RANGE_OFFSET_APPEND = (0xffffffff);

    [NativeTypeName("#define D3D12_FLOAT32_MAX ( 3.402823466e+38f )")]
    public const float D3D12_FLOAT32_MAX = (3.402823466e+38f);

    [NativeTypeName("#define D3D12_REQ_CONSTANT_BUFFER_ELEMENT_COUNT ( 4096 )")]
    public const int D3D12_REQ_CONSTANT_BUFFER_ELEMENT_COUNT = (4096);

    [NativeTypeName("#define D3D12_REQ_TEXTURE1D_U_DIMENSION ( 16384 )")]
    public const int D3D12_REQ_TEXTURE1D_U_DIMENSION = (16384);

    [NativeTypeName("#define D3D12_REQ_TEXTURE2D_U_OR_V_DIMENSION ( 16384 )")]
    public const int D3D12_REQ_TEXTURE2D_U_OR_V_DIMENSION = (16384);

    [NativeTypeName("#define D3D12_REQ_TEXTURE3D_U_V_OR_W_DIMENSION ( 2048 )")]
    public const int D3D12_REQ_TEXTURE3D_U_V_OR_W_DIMENSION = (2048);

    [NativeTypeName("#define D3D12_RESOURCE_BARRIER_ALL_SUBRESOURCES ( 0xffffffff )")]
    public const uint D3D12_RESOURCE_BARRIER_ALL_SUBRESOURCES = (0xffffffff);

    [NativeTypeName("#define D3D12_DEFAULT_SHADER_4_COMPONENT_MAPPING D3D12_ENCODE_SHADER_4_COMPONENT_MAPPING(0,1,2,3)")]
    public const int D3D12_DEFAULT_SHADER_4_COMPONENT_MAPPING = ((((0) & 0x7) | (((1) & 0x7) << 3) | (((2) & 0x7) << (3 * 2)) | (((3) & 0x7) << (3 * 3)) | (1 << (3 * 4))));
}