// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/dwrite.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TerraFX.Interop.Windows;

namespace TerraFX.Interop.DirectX
{
    [Guid("55F1112B-1DC2-4B3C-9541-F46894ED85B6")]
    [NativeTypeName("struct IDWriteTypography : IUnknown")]
    [NativeInheritance("IUnknown")]
    public unsafe partial struct IDWriteTypography : IDWriteTypography.Interface
    {
        public void** lpVtbl;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(0)]
        public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
        {
            return ((delegate* unmanaged<IDWriteTypography*, Guid*, void**, int>)(lpVtbl[0]))((IDWriteTypography*)Unsafe.AsPointer(ref this), riid, ppvObject);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(1)]
        [return: NativeTypeName("ULONG")]
        public uint AddRef()
        {
            return ((delegate* unmanaged<IDWriteTypography*, uint>)(lpVtbl[1]))((IDWriteTypography*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(2)]
        [return: NativeTypeName("ULONG")]
        public uint Release()
        {
            return ((delegate* unmanaged<IDWriteTypography*, uint>)(lpVtbl[2]))((IDWriteTypography*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(3)]
        public HRESULT AddFontFeature(DWRITE_FONT_FEATURE fontFeature)
        {
            return ((delegate* unmanaged<IDWriteTypography*, DWRITE_FONT_FEATURE, int>)(lpVtbl[3]))((IDWriteTypography*)Unsafe.AsPointer(ref this), fontFeature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(4)]
        [return: NativeTypeName("UINT32")]
        public uint GetFontFeatureCount()
        {
            return ((delegate* unmanaged<IDWriteTypography*, uint>)(lpVtbl[4]))((IDWriteTypography*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(5)]
        public HRESULT GetFontFeature([NativeTypeName("UINT32")] uint fontFeatureIndex, DWRITE_FONT_FEATURE* fontFeature)
        {
            return ((delegate* unmanaged<IDWriteTypography*, uint, DWRITE_FONT_FEATURE*, int>)(lpVtbl[5]))((IDWriteTypography*)Unsafe.AsPointer(ref this), fontFeatureIndex, fontFeature);
        }

        public interface Interface : IUnknown.Interface
        {
            [VtblIndex(3)]
            HRESULT AddFontFeature(DWRITE_FONT_FEATURE fontFeature);

            [VtblIndex(4)]
            [return: NativeTypeName("UINT32")]
            uint GetFontFeatureCount();

            [VtblIndex(5)]
            HRESULT GetFontFeature([NativeTypeName("UINT32")] uint fontFeatureIndex, DWRITE_FONT_FEATURE* fontFeature);
        }

        public partial struct Vtbl
        {
            [NativeTypeName("HRESULT (const IID &, void **) __attribute__((stdcall))")]
            public delegate* unmanaged<IDWriteTypography*, Guid*, void**, int> QueryInterface;

            [NativeTypeName("ULONG () __attribute__((stdcall))")]
            public delegate* unmanaged<IDWriteTypography*, uint> AddRef;

            [NativeTypeName("ULONG () __attribute__((stdcall))")]
            public delegate* unmanaged<IDWriteTypography*, uint> Release;

            [NativeTypeName("HRESULT (DWRITE_FONT_FEATURE) __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<IDWriteTypography*, DWRITE_FONT_FEATURE, int> AddFontFeature;

            [NativeTypeName("UINT32 () __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<IDWriteTypography*, uint> GetFontFeatureCount;

            [NativeTypeName("HRESULT (UINT32, DWRITE_FONT_FEATURE *) __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<IDWriteTypography*, uint, DWRITE_FONT_FEATURE*, int> GetFontFeature;
        }
    }
}
