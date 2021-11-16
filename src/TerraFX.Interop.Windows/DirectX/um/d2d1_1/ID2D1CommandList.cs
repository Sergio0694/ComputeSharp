// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d2d1_1.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TerraFX.Interop.Windows;

namespace TerraFX.Interop.DirectX
{
    [Guid("B4F34A19-2383-4D76-94F6-EC343657C3DC")]
    [NativeTypeName("struct ID2D1CommandList : ID2D1Image")]
    [NativeInheritance("ID2D1Image")]
    public unsafe partial struct ID2D1CommandList : ID2D1CommandList.Interface
    {
        public void** lpVtbl;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(0)]
        public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
        {
            return ((delegate* unmanaged<ID2D1CommandList*, Guid*, void**, int>)(lpVtbl[0]))((ID2D1CommandList*)Unsafe.AsPointer(ref this), riid, ppvObject);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(1)]
        [return: NativeTypeName("ULONG")]
        public uint AddRef()
        {
            return ((delegate* unmanaged<ID2D1CommandList*, uint>)(lpVtbl[1]))((ID2D1CommandList*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(2)]
        [return: NativeTypeName("ULONG")]
        public uint Release()
        {
            return ((delegate* unmanaged<ID2D1CommandList*, uint>)(lpVtbl[2]))((ID2D1CommandList*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(3)]
        public void GetFactory(ID2D1Factory** factory)
        {
            ((delegate* unmanaged<ID2D1CommandList*, ID2D1Factory**, void>)(lpVtbl[3]))((ID2D1CommandList*)Unsafe.AsPointer(ref this), factory);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(4)]
        public HRESULT Stream(ID2D1CommandSink* sink)
        {
            return ((delegate* unmanaged<ID2D1CommandList*, ID2D1CommandSink*, int>)(lpVtbl[4]))((ID2D1CommandList*)Unsafe.AsPointer(ref this), sink);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(5)]
        public HRESULT Close()
        {
            return ((delegate* unmanaged<ID2D1CommandList*, int>)(lpVtbl[5]))((ID2D1CommandList*)Unsafe.AsPointer(ref this));
        }

        public interface Interface : ID2D1Image.Interface
        {
            [VtblIndex(4)]
            HRESULT Stream(ID2D1CommandSink* sink);

            [VtblIndex(5)]
            HRESULT Close();
        }

        public partial struct Vtbl
        {
            [NativeTypeName("HRESULT (const IID &, void **) __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1CommandList*, Guid*, void**, int> QueryInterface;

            [NativeTypeName("ULONG () __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1CommandList*, uint> AddRef;

            [NativeTypeName("ULONG () __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1CommandList*, uint> Release;

            [NativeTypeName("void (ID2D1Factory **) const __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1CommandList*, ID2D1Factory**, void> GetFactory;

            [NativeTypeName("HRESULT (ID2D1CommandSink *) __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1CommandList*, ID2D1CommandSink*, int> Stream;

            [NativeTypeName("HRESULT () __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1CommandList*, int> Close;
        }
    }
}
