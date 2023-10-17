// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d2d1_1.h in the Windows SDK for Windows 10.0.22000.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TerraFX.Interop.Windows;

#pragma warning disable CS0649

namespace ComputeSharp.Win32;

[Guid("E8F7FE7A-191C-466D-AD95-975678BDA998")]
[NativeTypeName("struct ID2D1DeviceContext : ID2D1RenderTarget")]
[NativeInheritance("ID2D1RenderTarget")]
internal unsafe partial struct ID2D1DeviceContext
{
    public void** lpVtbl;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(3)]
    public void GetFactory(ID2D1Factory** factory)
    {
        ((delegate* unmanaged[Stdcall]<ID2D1DeviceContext*, ID2D1Factory**, void>)(lpVtbl[3]))((ID2D1DeviceContext*)Unsafe.AsPointer(ref this), factory);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(52)]
    public void GetDpi(float* dpiX, float* dpiY)
    {
        ((delegate* unmanaged[Stdcall]<ID2D1DeviceContext*, float*, float*, void>)(lpVtbl[52]))((ID2D1DeviceContext*)Unsafe.AsPointer(ref this), dpiX, dpiY);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(63)]
    public HRESULT CreateEffect([NativeTypeName("const IID &")] Guid* effectId, ID2D1Effect** effect)
    {
        return ((delegate* unmanaged[Stdcall]<ID2D1DeviceContext*, Guid*, ID2D1Effect**, int>)(lpVtbl[63]))((ID2D1DeviceContext*)Unsafe.AsPointer(ref this), effectId, effect);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(75)]
    public void GetTarget(ID2D1Image** image)
    {
        ((delegate* unmanaged[Stdcall]<ID2D1DeviceContext*, ID2D1Image**, void>)(lpVtbl[75]))((ID2D1DeviceContext*)Unsafe.AsPointer(ref this), image);
    }
}