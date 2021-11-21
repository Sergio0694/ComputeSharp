// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/wincodec.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace TerraFX.Interop.Windows
{
    [Guid("00000301-A8F2-4877-BA0A-FD2B6645FB94")]
    [NativeTypeName("struct IWICFormatConverter : IWICBitmapSource")]
    [NativeInheritance("IWICBitmapSource")]
    internal unsafe partial struct IWICFormatConverter
    {
        public void** lpVtbl;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(0)]
        public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
        {
            return ((delegate* unmanaged[Stdcall]<IWICFormatConverter*, Guid*, void**, int>)(lpVtbl[0]))((IWICFormatConverter*)Unsafe.AsPointer(ref this), riid, ppvObject);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(1)]
        [return: NativeTypeName("ULONG")]
        public uint AddRef()
        {
            return ((delegate* unmanaged[Stdcall]<IWICFormatConverter*, uint>)(lpVtbl[1]))((IWICFormatConverter*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(2)]
        [return: NativeTypeName("ULONG")]
        public uint Release()
        {
            return ((delegate* unmanaged[Stdcall]<IWICFormatConverter*, uint>)(lpVtbl[2]))((IWICFormatConverter*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(3)]
        public HRESULT GetSize(uint* puiWidth, uint* puiHeight)
        {
            return ((delegate* unmanaged[Stdcall]<IWICFormatConverter*, uint*, uint*, int>)(lpVtbl[3]))((IWICFormatConverter*)Unsafe.AsPointer(ref this), puiWidth, puiHeight);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(4)]
        public HRESULT GetPixelFormat([NativeTypeName("WICPixelFormatGUID *")] Guid* pPixelFormat)
        {
            return ((delegate* unmanaged[Stdcall]<IWICFormatConverter*, Guid*, int>)(lpVtbl[4]))((IWICFormatConverter*)Unsafe.AsPointer(ref this), pPixelFormat);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(5)]
        public HRESULT GetResolution(double* pDpiX, double* pDpiY)
        {
            return ((delegate* unmanaged[Stdcall]<IWICFormatConverter*, double*, double*, int>)(lpVtbl[5]))((IWICFormatConverter*)Unsafe.AsPointer(ref this), pDpiX, pDpiY);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(6)]
        public HRESULT CopyPalette(IWICPalette* pIPalette)
        {
            return ((delegate* unmanaged[Stdcall]<IWICFormatConverter*, IWICPalette*, int>)(lpVtbl[6]))((IWICFormatConverter*)Unsafe.AsPointer(ref this), pIPalette);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(7)]
        public HRESULT CopyPixels([NativeTypeName("const WICRect *")] WICRect* prc, uint cbStride, uint cbBufferSize, byte* pbBuffer)
        {
            return ((delegate* unmanaged[Stdcall]<IWICFormatConverter*, WICRect*, uint, uint, byte*, int>)(lpVtbl[7]))((IWICFormatConverter*)Unsafe.AsPointer(ref this), prc, cbStride, cbBufferSize, pbBuffer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(8)]
        public HRESULT Initialize(IWICBitmapSource* pISource, [NativeTypeName("REFWICPixelFormatGUID")] Guid* dstFormat, WICBitmapDitherType dither, IWICPalette* pIPalette, double alphaThresholdPercent, WICBitmapPaletteType paletteTranslate)
        {
            return ((delegate* unmanaged[Stdcall]<IWICFormatConverter*, IWICBitmapSource*, Guid*, WICBitmapDitherType, IWICPalette*, double, WICBitmapPaletteType, int>)(lpVtbl[8]))((IWICFormatConverter*)Unsafe.AsPointer(ref this), pISource, dstFormat, dither, pIPalette, alphaThresholdPercent, paletteTranslate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(9)]
        public HRESULT CanConvert([NativeTypeName("REFWICPixelFormatGUID")] Guid* srcPixelFormat, [NativeTypeName("REFWICPixelFormatGUID")] Guid* dstPixelFormat, BOOL* pfCanConvert)
        {
            return ((delegate* unmanaged[Stdcall]<IWICFormatConverter*, Guid*, Guid*, BOOL*, int>)(lpVtbl[9]))((IWICFormatConverter*)Unsafe.AsPointer(ref this), srcPixelFormat, dstPixelFormat, pfCanConvert);
        }
    }
}
