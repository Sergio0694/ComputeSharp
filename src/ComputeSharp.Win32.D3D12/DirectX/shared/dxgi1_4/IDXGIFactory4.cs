// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from shared/dxgi1_4.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ComputeSharp.Win32;

[Guid("1BC6EA02-EF36-464F-BF0C-21CA39E5168A")]
[NativeTypeName("struct IDXGIFactory4 : IDXGIFactory3")]
[NativeInheritance("IDXGIFactory3")]
internal unsafe partial struct IDXGIFactory4
{
    public void** lpVtbl;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(0)]
    public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory4*, Guid*, void**, int>)(lpVtbl[0]))((IDXGIFactory4*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(1)]
    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory4*, uint>)(lpVtbl[1]))((IDXGIFactory4*)Unsafe.AsPointer(ref this));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(2)]
    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory4*, uint>)(lpVtbl[2]))((IDXGIFactory4*)Unsafe.AsPointer(ref this));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(7)]
    public HRESULT EnumAdapters(uint Adapter, IDXGIAdapter** ppAdapter)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory4*, uint, IDXGIAdapter**, int>)(lpVtbl[7]))((IDXGIFactory4*)Unsafe.AsPointer(ref this), Adapter, ppAdapter);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(12)]
    public HRESULT EnumAdapters1(uint Adapter, IDXGIAdapter1** ppAdapter)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory4*, uint, IDXGIAdapter1**, int>)(lpVtbl[12]))((IDXGIFactory4*)Unsafe.AsPointer(ref this), Adapter, ppAdapter);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(27)]
    public HRESULT EnumWarpAdapter([NativeTypeName("const IID &")] Guid* riid, void** ppvAdapter)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory4*, Guid*, void**, int>)(lpVtbl[27]))((IDXGIFactory4*)Unsafe.AsPointer(ref this), riid, ppvAdapter);
    }
}