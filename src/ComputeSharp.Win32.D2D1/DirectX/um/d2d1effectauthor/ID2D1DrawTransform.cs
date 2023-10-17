// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d2d1effectauthor.h in the Windows SDK for Windows 10.0.22000.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TerraFX.Interop.Windows;

#pragma warning disable CS0649

namespace ComputeSharp.Win32;

[Guid("36BFDCB6-9739-435D-A30D-A653BEFF6A6F")]
[NativeTypeName("struct ID2D1DrawTransform : ID2D1Transform")]
[NativeInheritance("ID2D1Transform")]
internal unsafe partial struct ID2D1DrawTransform
{
    public void** lpVtbl;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(0)]
    public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<ID2D1DrawTransform*, Guid*, void**, int>)(lpVtbl[0]))((ID2D1DrawTransform*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(1)]
    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<ID2D1DrawTransform*, uint>)(lpVtbl[1]))((ID2D1DrawTransform*)Unsafe.AsPointer(ref this));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(2)]
    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<ID2D1DrawTransform*, uint>)(lpVtbl[2]))((ID2D1DrawTransform*)Unsafe.AsPointer(ref this));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(3)]
    [return: NativeTypeName("UINT32")]
    public uint GetInputCount()
    {
        return ((delegate* unmanaged[Stdcall]<ID2D1DrawTransform*, uint>)(lpVtbl[3]))((ID2D1DrawTransform*)Unsafe.AsPointer(ref this));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(4)]
    public HRESULT MapOutputRectToInputRects([NativeTypeName("const D2D1_RECT_L *")] RECT* outputRect, [NativeTypeName("D2D1_RECT_L *")] RECT* inputRects, [NativeTypeName("UINT32")] uint inputRectsCount)
    {
        return ((delegate* unmanaged[Stdcall]<ID2D1DrawTransform*, RECT*, RECT*, uint, int>)(lpVtbl[4]))((ID2D1DrawTransform*)Unsafe.AsPointer(ref this), outputRect, inputRects, inputRectsCount);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(5)]
    public HRESULT MapInputRectsToOutputRect([NativeTypeName("const D2D1_RECT_L *")] RECT* inputRects, [NativeTypeName("const D2D1_RECT_L *")] RECT* inputOpaqueSubRects, [NativeTypeName("UINT32")] uint inputRectCount, [NativeTypeName("D2D1_RECT_L *")] RECT* outputRect, [NativeTypeName("D2D1_RECT_L *")] RECT* outputOpaqueSubRect)
    {
        return ((delegate* unmanaged[Stdcall]<ID2D1DrawTransform*, RECT*, RECT*, uint, RECT*, RECT*, int>)(lpVtbl[5]))((ID2D1DrawTransform*)Unsafe.AsPointer(ref this), inputRects, inputOpaqueSubRects, inputRectCount, outputRect, outputOpaqueSubRect);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(6)]
    public HRESULT MapInvalidRect([NativeTypeName("UINT32")] uint inputIndex, [NativeTypeName("D2D1_RECT_L")] RECT invalidInputRect, [NativeTypeName("D2D1_RECT_L *")] RECT* invalidOutputRect)
    {
        return ((delegate* unmanaged[Stdcall]<ID2D1DrawTransform*, uint, RECT, RECT*, int>)(lpVtbl[6]))((ID2D1DrawTransform*)Unsafe.AsPointer(ref this), inputIndex, invalidInputRect, invalidOutputRect);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(7)]
    public HRESULT SetDrawInfo(ID2D1DrawInfo* drawInfo)
    {
        return ((delegate* unmanaged[Stdcall]<ID2D1DrawTransform*, ID2D1DrawInfo*, int>)(lpVtbl[7]))((ID2D1DrawTransform*)Unsafe.AsPointer(ref this), drawInfo);
    }
}