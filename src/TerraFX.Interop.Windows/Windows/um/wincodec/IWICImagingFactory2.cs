// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/wincodec.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using TerraFX.Interop.DirectX;

namespace TerraFX.Interop.Windows
{
    [SupportedOSPlatform("windows8.0")]
    [Guid("7B816B45-1996-4476-B132-DE9E247C8AF0")]
    [NativeTypeName("struct IWICImagingFactory2 : IWICImagingFactory")]
    [NativeInheritance("IWICImagingFactory")]
    internal unsafe partial struct IWICImagingFactory2
    {
        public void** lpVtbl;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(0)]
        public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
        {
            return ((delegate* unmanaged[Stdcall]<IWICImagingFactory2*, Guid*, void**, int>)(lpVtbl[0]))((IWICImagingFactory2*)Unsafe.AsPointer(ref this), riid, ppvObject);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(1)]
        [return: NativeTypeName("ULONG")]
        public uint AddRef()
        {
            return ((delegate* unmanaged[Stdcall]<IWICImagingFactory2*, uint>)(lpVtbl[1]))((IWICImagingFactory2*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(2)]
        [return: NativeTypeName("ULONG")]
        public uint Release()
        {
            return ((delegate* unmanaged[Stdcall]<IWICImagingFactory2*, uint>)(lpVtbl[2]))((IWICImagingFactory2*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(3)]
        public HRESULT CreateDecoderFromFilename([NativeTypeName("LPCWSTR")] ushort* wzFilename, [NativeTypeName("const GUID *")] Guid* pguidVendor, [NativeTypeName("DWORD")] uint dwDesiredAccess, WICDecodeOptions metadataOptions, IWICBitmapDecoder** ppIDecoder)
        {
            return ((delegate* unmanaged[Stdcall]<IWICImagingFactory2*, ushort*, Guid*, uint, WICDecodeOptions, IWICBitmapDecoder**, int>)(lpVtbl[3]))((IWICImagingFactory2*)Unsafe.AsPointer(ref this), wzFilename, pguidVendor, dwDesiredAccess, metadataOptions, ppIDecoder);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(4)]
        public HRESULT CreateDecoderFromStream(IStream* pIStream, [NativeTypeName("const GUID *")] Guid* pguidVendor, WICDecodeOptions metadataOptions, IWICBitmapDecoder** ppIDecoder)
        {
            return ((delegate* unmanaged[Stdcall]<IWICImagingFactory2*, IStream*, Guid*, WICDecodeOptions, IWICBitmapDecoder**, int>)(lpVtbl[4]))((IWICImagingFactory2*)Unsafe.AsPointer(ref this), pIStream, pguidVendor, metadataOptions, ppIDecoder);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(5)]
        public HRESULT CreateDecoderFromFileHandle([NativeTypeName("ULONG_PTR")] nuint hFile, [NativeTypeName("const GUID *")] Guid* pguidVendor, WICDecodeOptions metadataOptions, IWICBitmapDecoder** ppIDecoder)
        {
            return ((delegate* unmanaged[Stdcall]<IWICImagingFactory2*, nuint, Guid*, WICDecodeOptions, IWICBitmapDecoder**, int>)(lpVtbl[5]))((IWICImagingFactory2*)Unsafe.AsPointer(ref this), hFile, pguidVendor, metadataOptions, ppIDecoder);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(7)]
        public HRESULT CreateDecoder([NativeTypeName("const GUID &")] Guid* guidContainerFormat, [NativeTypeName("const GUID *")] Guid* pguidVendor, IWICBitmapDecoder** ppIDecoder)
        {
            return ((delegate* unmanaged[Stdcall]<IWICImagingFactory2*, Guid*, Guid*, IWICBitmapDecoder**, int>)(lpVtbl[7]))((IWICImagingFactory2*)Unsafe.AsPointer(ref this), guidContainerFormat, pguidVendor, ppIDecoder);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(8)]
        public HRESULT CreateEncoder([NativeTypeName("const GUID &")] Guid* guidContainerFormat, [NativeTypeName("const GUID *")] Guid* pguidVendor, IWICBitmapEncoder** ppIEncoder)
        {
            return ((delegate* unmanaged[Stdcall]<IWICImagingFactory2*, Guid*, Guid*, IWICBitmapEncoder**, int>)(lpVtbl[8]))((IWICImagingFactory2*)Unsafe.AsPointer(ref this), guidContainerFormat, pguidVendor, ppIEncoder);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(9)]
        public HRESULT CreatePalette(IWICPalette** ppIPalette)
        {
            return ((delegate* unmanaged[Stdcall]<IWICImagingFactory2*, IWICPalette**, int>)(lpVtbl[9]))((IWICImagingFactory2*)Unsafe.AsPointer(ref this), ppIPalette);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(10)]
        public HRESULT CreateFormatConverter(IWICFormatConverter** ppIFormatConverter)
        {
            return ((delegate* unmanaged[Stdcall]<IWICImagingFactory2*, IWICFormatConverter**, int>)(lpVtbl[10]))((IWICImagingFactory2*)Unsafe.AsPointer(ref this), ppIFormatConverter);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(14)]
        public HRESULT CreateStream(IWICStream** ppIWICStream)
        {
            return ((delegate* unmanaged[Stdcall]<IWICImagingFactory2*, IWICStream**, int>)(lpVtbl[14]))((IWICImagingFactory2*)Unsafe.AsPointer(ref this), ppIWICStream);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(17)]
        public HRESULT CreateBitmap(uint uiWidth, uint uiHeight, [NativeTypeName("REFWICPixelFormatGUID")] Guid* pixelFormat, WICBitmapCreateCacheOption option, IWICBitmap** ppIBitmap)
        {
            return ((delegate* unmanaged[Stdcall]<IWICImagingFactory2*, uint, uint, Guid*, WICBitmapCreateCacheOption, IWICBitmap**, int>)(lpVtbl[17]))((IWICImagingFactory2*)Unsafe.AsPointer(ref this), uiWidth, uiHeight, pixelFormat, option, ppIBitmap);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(18)]
        public HRESULT CreateBitmapFromSource(IWICBitmapSource* pIBitmapSource, WICBitmapCreateCacheOption option, IWICBitmap** ppIBitmap)
        {
            return ((delegate* unmanaged[Stdcall]<IWICImagingFactory2*, IWICBitmapSource*, WICBitmapCreateCacheOption, IWICBitmap**, int>)(lpVtbl[18]))((IWICImagingFactory2*)Unsafe.AsPointer(ref this), pIBitmapSource, option, ppIBitmap);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(19)]
        public HRESULT CreateBitmapFromSourceRect(IWICBitmapSource* pIBitmapSource, uint x, uint y, uint width, uint height, IWICBitmap** ppIBitmap)
        {
            return ((delegate* unmanaged[Stdcall]<IWICImagingFactory2*, IWICBitmapSource*, uint, uint, uint, uint, IWICBitmap**, int>)(lpVtbl[19]))((IWICImagingFactory2*)Unsafe.AsPointer(ref this), pIBitmapSource, x, y, width, height, ppIBitmap);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(20)]
        public HRESULT CreateBitmapFromMemory(uint uiWidth, uint uiHeight, [NativeTypeName("REFWICPixelFormatGUID")] Guid* pixelFormat, uint cbStride, uint cbBufferSize, byte* pbBuffer, IWICBitmap** ppIBitmap)
        {
            return ((delegate* unmanaged[Stdcall]<IWICImagingFactory2*, uint, uint, Guid*, uint, uint, byte*, IWICBitmap**, int>)(lpVtbl[20]))((IWICImagingFactory2*)Unsafe.AsPointer(ref this), uiWidth, uiHeight, pixelFormat, cbStride, cbBufferSize, pbBuffer, ppIBitmap);
        }
    }
}
