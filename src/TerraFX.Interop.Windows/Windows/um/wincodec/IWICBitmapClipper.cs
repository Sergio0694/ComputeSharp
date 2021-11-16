// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/wincodec.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace TerraFX.Interop.Windows
{
    [Guid("E4FBCF03-223D-4E81-9333-D635556DD1B5")]
    [NativeTypeName("struct IWICBitmapClipper : IWICBitmapSource")]
    [NativeInheritance("IWICBitmapSource")]
    public unsafe partial struct IWICBitmapClipper : IWICBitmapClipper.Interface
    {
        public void** lpVtbl;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(0)]
        public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
        {
            return ((delegate* unmanaged<IWICBitmapClipper*, Guid*, void**, int>)(lpVtbl[0]))((IWICBitmapClipper*)Unsafe.AsPointer(ref this), riid, ppvObject);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(1)]
        [return: NativeTypeName("ULONG")]
        public uint AddRef()
        {
            return ((delegate* unmanaged<IWICBitmapClipper*, uint>)(lpVtbl[1]))((IWICBitmapClipper*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(2)]
        [return: NativeTypeName("ULONG")]
        public uint Release()
        {
            return ((delegate* unmanaged<IWICBitmapClipper*, uint>)(lpVtbl[2]))((IWICBitmapClipper*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(3)]
        public HRESULT GetSize(uint* puiWidth, uint* puiHeight)
        {
            return ((delegate* unmanaged<IWICBitmapClipper*, uint*, uint*, int>)(lpVtbl[3]))((IWICBitmapClipper*)Unsafe.AsPointer(ref this), puiWidth, puiHeight);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(4)]
        public HRESULT GetPixelFormat([NativeTypeName("WICPixelFormatGUID *")] Guid* pPixelFormat)
        {
            return ((delegate* unmanaged<IWICBitmapClipper*, Guid*, int>)(lpVtbl[4]))((IWICBitmapClipper*)Unsafe.AsPointer(ref this), pPixelFormat);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(5)]
        public HRESULT GetResolution(double* pDpiX, double* pDpiY)
        {
            return ((delegate* unmanaged<IWICBitmapClipper*, double*, double*, int>)(lpVtbl[5]))((IWICBitmapClipper*)Unsafe.AsPointer(ref this), pDpiX, pDpiY);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(6)]
        public HRESULT CopyPalette(IWICPalette* pIPalette)
        {
            return ((delegate* unmanaged<IWICBitmapClipper*, IWICPalette*, int>)(lpVtbl[6]))((IWICBitmapClipper*)Unsafe.AsPointer(ref this), pIPalette);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(7)]
        public HRESULT CopyPixels([NativeTypeName("const WICRect *")] WICRect* prc, uint cbStride, uint cbBufferSize, byte* pbBuffer)
        {
            return ((delegate* unmanaged<IWICBitmapClipper*, WICRect*, uint, uint, byte*, int>)(lpVtbl[7]))((IWICBitmapClipper*)Unsafe.AsPointer(ref this), prc, cbStride, cbBufferSize, pbBuffer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(8)]
        public HRESULT Initialize(IWICBitmapSource* pISource, [NativeTypeName("const WICRect *")] WICRect* prc)
        {
            return ((delegate* unmanaged<IWICBitmapClipper*, IWICBitmapSource*, WICRect*, int>)(lpVtbl[8]))((IWICBitmapClipper*)Unsafe.AsPointer(ref this), pISource, prc);
        }

        public interface Interface : IWICBitmapSource.Interface
        {
            [VtblIndex(8)]
            HRESULT Initialize(IWICBitmapSource* pISource, [NativeTypeName("const WICRect *")] WICRect* prc);
        }

        public partial struct Vtbl
        {
            [NativeTypeName("HRESULT (const IID &, void **) __attribute__((stdcall))")]
            public delegate* unmanaged<IWICBitmapClipper*, Guid*, void**, int> QueryInterface;

            [NativeTypeName("ULONG () __attribute__((stdcall))")]
            public delegate* unmanaged<IWICBitmapClipper*, uint> AddRef;

            [NativeTypeName("ULONG () __attribute__((stdcall))")]
            public delegate* unmanaged<IWICBitmapClipper*, uint> Release;

            [NativeTypeName("HRESULT (UINT *, UINT *) __attribute__((stdcall))")]
            public delegate* unmanaged<IWICBitmapClipper*, uint*, uint*, int> GetSize;

            [NativeTypeName("HRESULT (WICPixelFormatGUID *) __attribute__((stdcall))")]
            public delegate* unmanaged<IWICBitmapClipper*, Guid*, int> GetPixelFormat;

            [NativeTypeName("HRESULT (double *, double *) __attribute__((stdcall))")]
            public delegate* unmanaged<IWICBitmapClipper*, double*, double*, int> GetResolution;

            [NativeTypeName("HRESULT (IWICPalette *) __attribute__((stdcall))")]
            public delegate* unmanaged<IWICBitmapClipper*, IWICPalette*, int> CopyPalette;

            [NativeTypeName("HRESULT (const WICRect *, UINT, UINT, BYTE *) __attribute__((stdcall))")]
            public delegate* unmanaged<IWICBitmapClipper*, WICRect*, uint, uint, byte*, int> CopyPixels;

            [NativeTypeName("HRESULT (IWICBitmapSource *, const WICRect *) __attribute__((stdcall))")]
            public delegate* unmanaged<IWICBitmapClipper*, IWICBitmapSource*, WICRect*, int> Initialize;
        }
    }
}
