// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d2d1_1.h in the Windows SDK for Windows 10.0.22000.0
// Original source is Copyright © Microsoft. All rights reserved.

using System.Runtime.CompilerServices;

namespace ComputeSharp.Win32;

[NativeTypeName("struct ID2D1Effect : ID2D1Properties")]
[NativeInheritance("ID2D1Properties")]
internal unsafe partial struct ID2D1Effect
{
    public void** lpVtbl;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(9)]
    public HRESULT SetValue([NativeTypeName("UINT32")] uint index, D2D1_PROPERTY_TYPE type, [NativeTypeName("const BYTE *")] byte* data, [NativeTypeName("UINT32")] uint dataSize)
    {
        return ((delegate* unmanaged[Stdcall]<ID2D1Effect*, uint, D2D1_PROPERTY_TYPE, byte*, uint, int>)(lpVtbl[9]))((ID2D1Effect*)Unsafe.AsPointer(ref this), index, type, data, dataSize);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(11)]
    public HRESULT GetValue([NativeTypeName("UINT32")] uint index, D2D1_PROPERTY_TYPE type, byte* data, [NativeTypeName("UINT32")] uint dataSize)
    {
        return ((delegate* unmanaged[Stdcall]<ID2D1Effect*, uint, D2D1_PROPERTY_TYPE, byte*, uint, int>)(lpVtbl[11]))((ID2D1Effect*)Unsafe.AsPointer(ref this), index, type, data, dataSize);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(14)]
    public void SetInput([NativeTypeName("UINT32")] uint index, ID2D1Image* input, [NativeTypeName("BOOL")] int invalidate = 1)
    {
        ((delegate* unmanaged[Stdcall]<ID2D1Effect*, uint, ID2D1Image*, int, void>)(lpVtbl[14]))((ID2D1Effect*)Unsafe.AsPointer(ref this), index, input, invalidate);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(16)]
    public void GetInput([NativeTypeName("UINT32")] uint index, ID2D1Image** input)
    {
        ((delegate* unmanaged[Stdcall]<ID2D1Effect*, uint, ID2D1Image**, void>)(lpVtbl[16]))((ID2D1Effect*)Unsafe.AsPointer(ref this), index, input);
    }
}