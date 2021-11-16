// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/dwrite.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TerraFX.Interop.Windows;

namespace TerraFX.Interop.DirectX
{
    [Guid("739D886A-CEF5-47DC-8769-1A8B41BEBBB0")]
    [NativeTypeName("struct IDWriteFontFile : IUnknown")]
    [NativeInheritance("IUnknown")]
    public unsafe partial struct IDWriteFontFile : IDWriteFontFile.Interface
    {
        public void** lpVtbl;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(0)]
        public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
        {
            return ((delegate* unmanaged<IDWriteFontFile*, Guid*, void**, int>)(lpVtbl[0]))((IDWriteFontFile*)Unsafe.AsPointer(ref this), riid, ppvObject);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(1)]
        [return: NativeTypeName("ULONG")]
        public uint AddRef()
        {
            return ((delegate* unmanaged<IDWriteFontFile*, uint>)(lpVtbl[1]))((IDWriteFontFile*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(2)]
        [return: NativeTypeName("ULONG")]
        public uint Release()
        {
            return ((delegate* unmanaged<IDWriteFontFile*, uint>)(lpVtbl[2]))((IDWriteFontFile*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(3)]
        public HRESULT GetReferenceKey([NativeTypeName("const void **")] void** fontFileReferenceKey, [NativeTypeName("UINT32 *")] uint* fontFileReferenceKeySize)
        {
            return ((delegate* unmanaged<IDWriteFontFile*, void**, uint*, int>)(lpVtbl[3]))((IDWriteFontFile*)Unsafe.AsPointer(ref this), fontFileReferenceKey, fontFileReferenceKeySize);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(4)]
        public HRESULT GetLoader(IDWriteFontFileLoader** fontFileLoader)
        {
            return ((delegate* unmanaged<IDWriteFontFile*, IDWriteFontFileLoader**, int>)(lpVtbl[4]))((IDWriteFontFile*)Unsafe.AsPointer(ref this), fontFileLoader);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(5)]
        public HRESULT Analyze(BOOL* isSupportedFontType, DWRITE_FONT_FILE_TYPE* fontFileType, DWRITE_FONT_FACE_TYPE* fontFaceType, [NativeTypeName("UINT32 *")] uint* numberOfFaces)
        {
            return ((delegate* unmanaged<IDWriteFontFile*, BOOL*, DWRITE_FONT_FILE_TYPE*, DWRITE_FONT_FACE_TYPE*, uint*, int>)(lpVtbl[5]))((IDWriteFontFile*)Unsafe.AsPointer(ref this), isSupportedFontType, fontFileType, fontFaceType, numberOfFaces);
        }

        public interface Interface : IUnknown.Interface
        {
            [VtblIndex(3)]
            HRESULT GetReferenceKey([NativeTypeName("const void **")] void** fontFileReferenceKey, [NativeTypeName("UINT32 *")] uint* fontFileReferenceKeySize);

            [VtblIndex(4)]
            HRESULT GetLoader(IDWriteFontFileLoader** fontFileLoader);

            [VtblIndex(5)]
            HRESULT Analyze(BOOL* isSupportedFontType, DWRITE_FONT_FILE_TYPE* fontFileType, DWRITE_FONT_FACE_TYPE* fontFaceType, [NativeTypeName("UINT32 *")] uint* numberOfFaces);
        }

        public partial struct Vtbl
        {
            [NativeTypeName("HRESULT (const IID &, void **) __attribute__((stdcall))")]
            public delegate* unmanaged<IDWriteFontFile*, Guid*, void**, int> QueryInterface;

            [NativeTypeName("ULONG () __attribute__((stdcall))")]
            public delegate* unmanaged<IDWriteFontFile*, uint> AddRef;

            [NativeTypeName("ULONG () __attribute__((stdcall))")]
            public delegate* unmanaged<IDWriteFontFile*, uint> Release;

            [NativeTypeName("HRESULT (const void **, UINT32 *) __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<IDWriteFontFile*, void**, uint*, int> GetReferenceKey;

            [NativeTypeName("HRESULT (IDWriteFontFileLoader **) __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<IDWriteFontFile*, IDWriteFontFileLoader**, int> GetLoader;

            [NativeTypeName("HRESULT (BOOL *, DWRITE_FONT_FILE_TYPE *, DWRITE_FONT_FACE_TYPE *, UINT32 *) __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<IDWriteFontFile*, BOOL*, DWRITE_FONT_FILE_TYPE*, DWRITE_FONT_FACE_TYPE*, uint*, int> Analyze;
        }
    }
}
