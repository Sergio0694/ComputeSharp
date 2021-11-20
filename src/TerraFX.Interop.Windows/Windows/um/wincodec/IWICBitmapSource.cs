// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/wincodec.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace TerraFX.Interop.Windows
{
    [Guid("00000120-A8F2-4877-BA0A-FD2B6645FB94")]
    [NativeTypeName("struct IWICBitmapSource : IUnknown")]
    [NativeInheritance("IUnknown")]
    internal unsafe partial struct IWICBitmapSource : IWICBitmapSource.Interface
    {
        public void** lpVtbl;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(0)]
        public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
        {
            return ((delegate* unmanaged[Stdcall]<IWICBitmapSource*, Guid*, void**, int>)(lpVtbl[0]))((IWICBitmapSource*)Unsafe.AsPointer(ref this), riid, ppvObject);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(1)]
        [return: NativeTypeName("ULONG")]
        public uint AddRef()
        {
            return ((delegate* unmanaged[Stdcall]<IWICBitmapSource*, uint>)(lpVtbl[1]))((IWICBitmapSource*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(2)]
        [return: NativeTypeName("ULONG")]
        public uint Release()
        {
            return ((delegate* unmanaged[Stdcall]<IWICBitmapSource*, uint>)(lpVtbl[2]))((IWICBitmapSource*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(3)]
        public HRESULT GetSize(uint* puiWidth, uint* puiHeight)
        {
            return ((delegate* unmanaged[Stdcall]<IWICBitmapSource*, uint*, uint*, int>)(lpVtbl[3]))((IWICBitmapSource*)Unsafe.AsPointer(ref this), puiWidth, puiHeight);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(4)]
        public HRESULT GetPixelFormat([NativeTypeName("WICPixelFormatGUID *")] Guid* pPixelFormat)
        {
            return ((delegate* unmanaged[Stdcall]<IWICBitmapSource*, Guid*, int>)(lpVtbl[4]))((IWICBitmapSource*)Unsafe.AsPointer(ref this), pPixelFormat);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(5)]
        public HRESULT GetResolution(double* pDpiX, double* pDpiY)
        {
            return ((delegate* unmanaged[Stdcall]<IWICBitmapSource*, double*, double*, int>)(lpVtbl[5]))((IWICBitmapSource*)Unsafe.AsPointer(ref this), pDpiX, pDpiY);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(6)]
        public HRESULT CopyPalette(IWICPalette* pIPalette)
        {
            return ((delegate* unmanaged[Stdcall]<IWICBitmapSource*, IWICPalette*, int>)(lpVtbl[6]))((IWICBitmapSource*)Unsafe.AsPointer(ref this), pIPalette);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(7)]
        public HRESULT CopyPixels([NativeTypeName("const WICRect *")] WICRect* prc, uint cbStride, uint cbBufferSize, byte* pbBuffer)
        {
            return ((delegate* unmanaged[Stdcall]<IWICBitmapSource*, WICRect*, uint, uint, byte*, int>)(lpVtbl[7]))((IWICBitmapSource*)Unsafe.AsPointer(ref this), prc, cbStride, cbBufferSize, pbBuffer);
        }

        public interface Interface : IUnknown.Interface
        {
            [VtblIndex(3)]
            HRESULT GetSize(uint* puiWidth, uint* puiHeight);

            [VtblIndex(4)]
            HRESULT GetPixelFormat([NativeTypeName("WICPixelFormatGUID *")] Guid* pPixelFormat);

            [VtblIndex(5)]
            HRESULT GetResolution(double* pDpiX, double* pDpiY);

            [VtblIndex(6)]
            HRESULT CopyPalette(IWICPalette* pIPalette);

            [VtblIndex(7)]
            HRESULT CopyPixels([NativeTypeName("const WICRect *")] WICRect* prc, uint cbStride, uint cbBufferSize, byte* pbBuffer);
        }
    }
}
