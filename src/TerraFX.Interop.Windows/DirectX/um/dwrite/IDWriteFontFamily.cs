// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/dwrite.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TerraFX.Interop.Windows;

namespace TerraFX.Interop.DirectX
{
    [Guid("DA20D8EF-812A-4C43-9802-62EC4ABD7ADD")]
    [NativeTypeName("struct IDWriteFontFamily : IDWriteFontList")]
    [NativeInheritance("IDWriteFontList")]
    internal unsafe partial struct IDWriteFontFamily : IDWriteFontFamily.Interface
    {
        public void** lpVtbl;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(0)]
        public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
        {
            return ((delegate* unmanaged[Stdcall]<IDWriteFontFamily*, Guid*, void**, int>)(lpVtbl[0]))((IDWriteFontFamily*)Unsafe.AsPointer(ref this), riid, ppvObject);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(1)]
        [return: NativeTypeName("ULONG")]
        public uint AddRef()
        {
            return ((delegate* unmanaged[Stdcall]<IDWriteFontFamily*, uint>)(lpVtbl[1]))((IDWriteFontFamily*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(2)]
        [return: NativeTypeName("ULONG")]
        public uint Release()
        {
            return ((delegate* unmanaged[Stdcall]<IDWriteFontFamily*, uint>)(lpVtbl[2]))((IDWriteFontFamily*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(3)]
        public HRESULT GetFontCollection(IDWriteFontCollection** fontCollection)
        {
            return ((delegate* unmanaged[Stdcall]<IDWriteFontFamily*, IDWriteFontCollection**, int>)(lpVtbl[3]))((IDWriteFontFamily*)Unsafe.AsPointer(ref this), fontCollection);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(4)]
        [return: NativeTypeName("UINT32")]
        public uint GetFontCount()
        {
            return ((delegate* unmanaged[Stdcall]<IDWriteFontFamily*, uint>)(lpVtbl[4]))((IDWriteFontFamily*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(5)]
        public HRESULT GetFont([NativeTypeName("UINT32")] uint index, IDWriteFont** font)
        {
            return ((delegate* unmanaged[Stdcall]<IDWriteFontFamily*, uint, IDWriteFont**, int>)(lpVtbl[5]))((IDWriteFontFamily*)Unsafe.AsPointer(ref this), index, font);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(6)]
        public HRESULT GetFamilyNames(IDWriteLocalizedStrings** names)
        {
            return ((delegate* unmanaged[Stdcall]<IDWriteFontFamily*, IDWriteLocalizedStrings**, int>)(lpVtbl[6]))((IDWriteFontFamily*)Unsafe.AsPointer(ref this), names);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(7)]
        public HRESULT GetFirstMatchingFont(DWRITE_FONT_WEIGHT weight, DWRITE_FONT_STRETCH stretch, DWRITE_FONT_STYLE style, IDWriteFont** matchingFont)
        {
            return ((delegate* unmanaged[Stdcall]<IDWriteFontFamily*, DWRITE_FONT_WEIGHT, DWRITE_FONT_STRETCH, DWRITE_FONT_STYLE, IDWriteFont**, int>)(lpVtbl[7]))((IDWriteFontFamily*)Unsafe.AsPointer(ref this), weight, stretch, style, matchingFont);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(8)]
        public HRESULT GetMatchingFonts(DWRITE_FONT_WEIGHT weight, DWRITE_FONT_STRETCH stretch, DWRITE_FONT_STYLE style, IDWriteFontList** matchingFonts)
        {
            return ((delegate* unmanaged[Stdcall]<IDWriteFontFamily*, DWRITE_FONT_WEIGHT, DWRITE_FONT_STRETCH, DWRITE_FONT_STYLE, IDWriteFontList**, int>)(lpVtbl[8]))((IDWriteFontFamily*)Unsafe.AsPointer(ref this), weight, stretch, style, matchingFonts);
        }

        public interface Interface : IDWriteFontList.Interface
        {
            [VtblIndex(6)]
            HRESULT GetFamilyNames(IDWriteLocalizedStrings** names);

            [VtblIndex(7)]
            HRESULT GetFirstMatchingFont(DWRITE_FONT_WEIGHT weight, DWRITE_FONT_STRETCH stretch, DWRITE_FONT_STYLE style, IDWriteFont** matchingFont);

            [VtblIndex(8)]
            HRESULT GetMatchingFonts(DWRITE_FONT_WEIGHT weight, DWRITE_FONT_STRETCH stretch, DWRITE_FONT_STYLE style, IDWriteFontList** matchingFonts);
        }
    }
}
