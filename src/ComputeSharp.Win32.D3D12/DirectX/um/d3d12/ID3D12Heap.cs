// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d3d12.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

namespace ComputeSharp.Win32;

[NativeTypeName("struct ID3D12Heap : ID3D12Pageable")]
[NativeInheritance("ID3D12Pageable")]
internal unsafe partial struct ID3D12Heap
{
    public void** lpVtbl;
}