// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/dwrite.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TerraFX.Interop.Windows;

namespace TerraFX.Interop.DirectX
{
    [Guid("08256209-099A-4B34-B86D-C22B110E7771")]
    [NativeTypeName("struct IDWriteLocalizedStrings : IUnknown")]
    [NativeInheritance("IUnknown")]
    public unsafe partial struct IDWriteLocalizedStrings : IDWriteLocalizedStrings.Interface
    {
        public void** lpVtbl;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(0)]
        public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
        {
            return ((delegate* unmanaged<IDWriteLocalizedStrings*, Guid*, void**, int>)(lpVtbl[0]))((IDWriteLocalizedStrings*)Unsafe.AsPointer(ref this), riid, ppvObject);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(1)]
        [return: NativeTypeName("ULONG")]
        public uint AddRef()
        {
            return ((delegate* unmanaged<IDWriteLocalizedStrings*, uint>)(lpVtbl[1]))((IDWriteLocalizedStrings*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(2)]
        [return: NativeTypeName("ULONG")]
        public uint Release()
        {
            return ((delegate* unmanaged<IDWriteLocalizedStrings*, uint>)(lpVtbl[2]))((IDWriteLocalizedStrings*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(3)]
        [return: NativeTypeName("UINT32")]
        public uint GetCount()
        {
            return ((delegate* unmanaged<IDWriteLocalizedStrings*, uint>)(lpVtbl[3]))((IDWriteLocalizedStrings*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(4)]
        public HRESULT FindLocaleName([NativeTypeName("const WCHAR *")] ushort* localeName, [NativeTypeName("UINT32 *")] uint* index, BOOL* exists)
        {
            return ((delegate* unmanaged<IDWriteLocalizedStrings*, ushort*, uint*, BOOL*, int>)(lpVtbl[4]))((IDWriteLocalizedStrings*)Unsafe.AsPointer(ref this), localeName, index, exists);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(5)]
        public HRESULT GetLocaleNameLength([NativeTypeName("UINT32")] uint index, [NativeTypeName("UINT32 *")] uint* length)
        {
            return ((delegate* unmanaged<IDWriteLocalizedStrings*, uint, uint*, int>)(lpVtbl[5]))((IDWriteLocalizedStrings*)Unsafe.AsPointer(ref this), index, length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(6)]
        public HRESULT GetLocaleName([NativeTypeName("UINT32")] uint index, [NativeTypeName("WCHAR *")] ushort* localeName, [NativeTypeName("UINT32")] uint size)
        {
            return ((delegate* unmanaged<IDWriteLocalizedStrings*, uint, ushort*, uint, int>)(lpVtbl[6]))((IDWriteLocalizedStrings*)Unsafe.AsPointer(ref this), index, localeName, size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(7)]
        public HRESULT GetStringLength([NativeTypeName("UINT32")] uint index, [NativeTypeName("UINT32 *")] uint* length)
        {
            return ((delegate* unmanaged<IDWriteLocalizedStrings*, uint, uint*, int>)(lpVtbl[7]))((IDWriteLocalizedStrings*)Unsafe.AsPointer(ref this), index, length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(8)]
        public HRESULT GetString([NativeTypeName("UINT32")] uint index, [NativeTypeName("WCHAR *")] ushort* stringBuffer, [NativeTypeName("UINT32")] uint size)
        {
            return ((delegate* unmanaged<IDWriteLocalizedStrings*, uint, ushort*, uint, int>)(lpVtbl[8]))((IDWriteLocalizedStrings*)Unsafe.AsPointer(ref this), index, stringBuffer, size);
        }

        public interface Interface : IUnknown.Interface
        {
            [VtblIndex(3)]
            [return: NativeTypeName("UINT32")]
            uint GetCount();

            [VtblIndex(4)]
            HRESULT FindLocaleName([NativeTypeName("const WCHAR *")] ushort* localeName, [NativeTypeName("UINT32 *")] uint* index, BOOL* exists);

            [VtblIndex(5)]
            HRESULT GetLocaleNameLength([NativeTypeName("UINT32")] uint index, [NativeTypeName("UINT32 *")] uint* length);

            [VtblIndex(6)]
            HRESULT GetLocaleName([NativeTypeName("UINT32")] uint index, [NativeTypeName("WCHAR *")] ushort* localeName, [NativeTypeName("UINT32")] uint size);

            [VtblIndex(7)]
            HRESULT GetStringLength([NativeTypeName("UINT32")] uint index, [NativeTypeName("UINT32 *")] uint* length);

            [VtblIndex(8)]
            HRESULT GetString([NativeTypeName("UINT32")] uint index, [NativeTypeName("WCHAR *")] ushort* stringBuffer, [NativeTypeName("UINT32")] uint size);
        }

        public partial struct Vtbl
        {
            [NativeTypeName("HRESULT (const IID &, void **) __attribute__((stdcall))")]
            public delegate* unmanaged<IDWriteLocalizedStrings*, Guid*, void**, int> QueryInterface;

            [NativeTypeName("ULONG () __attribute__((stdcall))")]
            public delegate* unmanaged<IDWriteLocalizedStrings*, uint> AddRef;

            [NativeTypeName("ULONG () __attribute__((stdcall))")]
            public delegate* unmanaged<IDWriteLocalizedStrings*, uint> Release;

            [NativeTypeName("UINT32 () __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<IDWriteLocalizedStrings*, uint> GetCount;

            [NativeTypeName("HRESULT (const WCHAR *, UINT32 *, BOOL *) __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<IDWriteLocalizedStrings*, ushort*, uint*, BOOL*, int> FindLocaleName;

            [NativeTypeName("HRESULT (UINT32, UINT32 *) __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<IDWriteLocalizedStrings*, uint, uint*, int> GetLocaleNameLength;

            [NativeTypeName("HRESULT (UINT32, WCHAR *, UINT32) __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<IDWriteLocalizedStrings*, uint, ushort*, uint, int> GetLocaleName;

            [NativeTypeName("HRESULT (UINT32, UINT32 *) __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<IDWriteLocalizedStrings*, uint, uint*, int> GetStringLength;

            [NativeTypeName("HRESULT (UINT32, WCHAR *, UINT32) __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<IDWriteLocalizedStrings*, uint, ushort*, uint, int> GetString;
        }
    }
}
