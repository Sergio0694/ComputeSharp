// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d3d12.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System.Runtime.InteropServices;

namespace ComputeSharp.Win32;

internal partial struct D3D12_ROOT_PARAMETER
{
    public D3D12_ROOT_PARAMETER_TYPE ParameterType;

    [NativeTypeName("D3D12_ROOT_PARAMETER::(anonymous union at C:/Program Files (x86)/Windows Kits/10/Include/10.0.20348.0/um/d3d12.h:3580:5)")]
    public _Anonymous_e__Union Anonymous;

    public D3D12_SHADER_VISIBILITY ShaderVisibility;

    [StructLayout(LayoutKind.Explicit)]
    internal partial struct _Anonymous_e__Union
    {
        [FieldOffset(0)]
        public D3D12_ROOT_DESCRIPTOR_TABLE DescriptorTable;

        [FieldOffset(0)]
        public D3D12_ROOT_CONSTANTS Constants;

        [FieldOffset(0)]
        public D3D12_ROOT_DESCRIPTOR Descriptor;
    }
}