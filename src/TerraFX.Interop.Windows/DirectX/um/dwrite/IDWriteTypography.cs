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
    internal unsafe partial struct IDWriteTypography
    {
        public void** lpVtbl;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(0)]
        public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
        {
            return ((delegate* unmanaged[Stdcall]<IDWriteTypography*, Guid*, void**, int>)(lpVtbl[0]))((IDWriteTypography*)Unsafe.AsPointer(ref this), riid, ppvObject);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(1)]
        [return: NativeTypeName("ULONG")]
        public uint AddRef()
        {
            return ((delegate* unmanaged[Stdcall]<IDWriteTypography*, uint>)(lpVtbl[1]))((IDWriteTypography*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(2)]
        [return: NativeTypeName("ULONG")]
        public uint Release()
        {
            return ((delegate* unmanaged[Stdcall]<IDWriteTypography*, uint>)(lpVtbl[2]))((IDWriteTypography*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(3)]
        public HRESULT AddFontFeature(DWRITE_FONT_FEATURE fontFeature)
        {
            return ((delegate* unmanaged[Stdcall]<IDWriteTypography*, DWRITE_FONT_FEATURE, int>)(lpVtbl[3]))((IDWriteTypography*)Unsafe.AsPointer(ref this), fontFeature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(4)]
        [return: NativeTypeName("UINT32")]
        public uint GetFontFeatureCount()
        {
            return ((delegate* unmanaged[Stdcall]<IDWriteTypography*, uint>)(lpVtbl[4]))((IDWriteTypography*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(5)]
        public HRESULT GetFontFeature([NativeTypeName("UINT32")] uint fontFeatureIndex, DWRITE_FONT_FEATURE* fontFeature)
        {
            return ((delegate* unmanaged[Stdcall]<IDWriteTypography*, uint, DWRITE_FONT_FEATURE*, int>)(lpVtbl[5]))((IDWriteTypography*)Unsafe.AsPointer(ref this), fontFeatureIndex, fontFeature);
        }
    }
}
