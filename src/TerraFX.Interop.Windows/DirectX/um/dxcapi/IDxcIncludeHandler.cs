// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/dxcapi.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved. Licensed under the University of Illinois Open Source License.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TerraFX.Interop.Windows;

namespace TerraFX.Interop.DirectX
{
    [Guid("7F61FC7D-950D-467F-B3E3-3C02FB49187C")]
    [NativeTypeName("struct IDxcIncludeHandler : IUnknown")]
    [NativeInheritance("IUnknown")]
    internal unsafe partial struct IDxcIncludeHandler
    {
        public void** lpVtbl;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(0)]
        public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
        {
            return ((delegate* unmanaged[Stdcall]<IDxcIncludeHandler*, Guid*, void**, int>)(lpVtbl[0]))((IDxcIncludeHandler*)Unsafe.AsPointer(ref this), riid, ppvObject);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(1)]
        [return: NativeTypeName("ULONG")]
        public uint AddRef()
        {
            return ((delegate* unmanaged[Stdcall]<IDxcIncludeHandler*, uint>)(lpVtbl[1]))((IDxcIncludeHandler*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(2)]
        [return: NativeTypeName("ULONG")]
        public uint Release()
        {
            return ((delegate* unmanaged[Stdcall]<IDxcIncludeHandler*, uint>)(lpVtbl[2]))((IDxcIncludeHandler*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(3)]
        public HRESULT LoadSource([NativeTypeName("LPCWSTR")] ushort* pFilename, IDxcBlob** ppIncludeSource)
        {
            return ((delegate* unmanaged[Stdcall]<IDxcIncludeHandler*, ushort*, IDxcBlob**, int>)(lpVtbl[3]))((IDxcIncludeHandler*)Unsafe.AsPointer(ref this), pFilename, ppIncludeSource);
        }
    }
}
