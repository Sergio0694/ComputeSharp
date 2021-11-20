// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/wincodec.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace TerraFX.Interop.Windows
{
    [Guid("00000103-A8F2-4877-BA0A-FD2B6645FB94")]
    [NativeTypeName("struct IWICBitmapEncoder : IUnknown")]
    [NativeInheritance("IUnknown")]
    internal unsafe partial struct IWICBitmapEncoder : IWICBitmapEncoder.Interface
    {
        public void** lpVtbl;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(0)]
        public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
        {
            return ((delegate* unmanaged[Stdcall]<IWICBitmapEncoder*, Guid*, void**, int>)(lpVtbl[0]))((IWICBitmapEncoder*)Unsafe.AsPointer(ref this), riid, ppvObject);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(1)]
        [return: NativeTypeName("ULONG")]
        public uint AddRef()
        {
            return ((delegate* unmanaged[Stdcall]<IWICBitmapEncoder*, uint>)(lpVtbl[1]))((IWICBitmapEncoder*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(2)]
        [return: NativeTypeName("ULONG")]
        public uint Release()
        {
            return ((delegate* unmanaged[Stdcall]<IWICBitmapEncoder*, uint>)(lpVtbl[2]))((IWICBitmapEncoder*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(3)]
        public HRESULT Initialize(IStream* pIStream, WICBitmapEncoderCacheOption cacheOption)
        {
            return ((delegate* unmanaged[Stdcall]<IWICBitmapEncoder*, IStream*, WICBitmapEncoderCacheOption, int>)(lpVtbl[3]))((IWICBitmapEncoder*)Unsafe.AsPointer(ref this), pIStream, cacheOption);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(4)]
        public HRESULT GetContainerFormat(Guid* pguidContainerFormat)
        {
            return ((delegate* unmanaged[Stdcall]<IWICBitmapEncoder*, Guid*, int>)(lpVtbl[4]))((IWICBitmapEncoder*)Unsafe.AsPointer(ref this), pguidContainerFormat);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(5)]
        public HRESULT GetEncoderInfo(IWICBitmapEncoderInfo** ppIEncoderInfo)
        {
            return ((delegate* unmanaged[Stdcall]<IWICBitmapEncoder*, IWICBitmapEncoderInfo**, int>)(lpVtbl[5]))((IWICBitmapEncoder*)Unsafe.AsPointer(ref this), ppIEncoderInfo);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(6)]
        public HRESULT SetColorContexts(uint cCount, IWICColorContext** ppIColorContext)
        {
            return ((delegate* unmanaged[Stdcall]<IWICBitmapEncoder*, uint, IWICColorContext**, int>)(lpVtbl[6]))((IWICBitmapEncoder*)Unsafe.AsPointer(ref this), cCount, ppIColorContext);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(7)]
        public HRESULT SetPalette(IWICPalette* pIPalette)
        {
            return ((delegate* unmanaged[Stdcall]<IWICBitmapEncoder*, IWICPalette*, int>)(lpVtbl[7]))((IWICBitmapEncoder*)Unsafe.AsPointer(ref this), pIPalette);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(8)]
        public HRESULT SetThumbnail(IWICBitmapSource* pIThumbnail)
        {
            return ((delegate* unmanaged[Stdcall]<IWICBitmapEncoder*, IWICBitmapSource*, int>)(lpVtbl[8]))((IWICBitmapEncoder*)Unsafe.AsPointer(ref this), pIThumbnail);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(9)]
        public HRESULT SetPreview(IWICBitmapSource* pIPreview)
        {
            return ((delegate* unmanaged[Stdcall]<IWICBitmapEncoder*, IWICBitmapSource*, int>)(lpVtbl[9]))((IWICBitmapEncoder*)Unsafe.AsPointer(ref this), pIPreview);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(10)]
        public HRESULT CreateNewFrame(IWICBitmapFrameEncode** ppIFrameEncode, IPropertyBag2** ppIEncoderOptions)
        {
            return ((delegate* unmanaged[Stdcall]<IWICBitmapEncoder*, IWICBitmapFrameEncode**, IPropertyBag2**, int>)(lpVtbl[10]))((IWICBitmapEncoder*)Unsafe.AsPointer(ref this), ppIFrameEncode, ppIEncoderOptions);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(11)]
        public HRESULT Commit()
        {
            return ((delegate* unmanaged[Stdcall]<IWICBitmapEncoder*, int>)(lpVtbl[11]))((IWICBitmapEncoder*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(12)]
        public HRESULT GetMetadataQueryWriter(IWICMetadataQueryWriter** ppIMetadataQueryWriter)
        {
            return ((delegate* unmanaged[Stdcall]<IWICBitmapEncoder*, IWICMetadataQueryWriter**, int>)(lpVtbl[12]))((IWICBitmapEncoder*)Unsafe.AsPointer(ref this), ppIMetadataQueryWriter);
        }

        public interface Interface : IUnknown.Interface
        {
            [VtblIndex(3)]
            HRESULT Initialize(IStream* pIStream, WICBitmapEncoderCacheOption cacheOption);

            [VtblIndex(4)]
            HRESULT GetContainerFormat(Guid* pguidContainerFormat);

            [VtblIndex(5)]
            HRESULT GetEncoderInfo(IWICBitmapEncoderInfo** ppIEncoderInfo);

            [VtblIndex(6)]
            HRESULT SetColorContexts(uint cCount, IWICColorContext** ppIColorContext);

            [VtblIndex(7)]
            HRESULT SetPalette(IWICPalette* pIPalette);

            [VtblIndex(8)]
            HRESULT SetThumbnail(IWICBitmapSource* pIThumbnail);

            [VtblIndex(9)]
            HRESULT SetPreview(IWICBitmapSource* pIPreview);

            [VtblIndex(10)]
            HRESULT CreateNewFrame(IWICBitmapFrameEncode** ppIFrameEncode, IPropertyBag2** ppIEncoderOptions);

            [VtblIndex(11)]
            HRESULT Commit();

            [VtblIndex(12)]
            HRESULT GetMetadataQueryWriter(IWICMetadataQueryWriter** ppIMetadataQueryWriter);
        }
    }
}
