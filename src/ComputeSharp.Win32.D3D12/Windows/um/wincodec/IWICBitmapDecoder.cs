// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/wincodec.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;

namespace ComputeSharp.Win32;

[NativeTypeName("struct IWICBitmapDecoder : IUnknown")]
[NativeInheritance("IUnknown")]
internal unsafe partial struct IWICBitmapDecoder
{
    public void** lpVtbl;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(0)]
    public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[MemberFunction]<IWICBitmapDecoder*, Guid*, void**, int>)(lpVtbl[0]))((IWICBitmapDecoder*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(1)]
    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[MemberFunction]<IWICBitmapDecoder*, uint>)(lpVtbl[1]))((IWICBitmapDecoder*)Unsafe.AsPointer(ref this));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(2)]
    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[MemberFunction]<IWICBitmapDecoder*, uint>)(lpVtbl[2]))((IWICBitmapDecoder*)Unsafe.AsPointer(ref this));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(3)]
    public HRESULT QueryCapability(IStream* pIStream, [NativeTypeName("DWORD *")] uint* pdwCapability)
    {
        return ((delegate* unmanaged[MemberFunction]<IWICBitmapDecoder*, IStream*, uint*, int>)(lpVtbl[3]))((IWICBitmapDecoder*)Unsafe.AsPointer(ref this), pIStream, pdwCapability);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(4)]
    public HRESULT Initialize(IStream* pIStream, WICDecodeOptions cacheOptions)
    {
        return ((delegate* unmanaged[MemberFunction]<IWICBitmapDecoder*, IStream*, WICDecodeOptions, int>)(lpVtbl[4]))((IWICBitmapDecoder*)Unsafe.AsPointer(ref this), pIStream, cacheOptions);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(5)]
    public HRESULT GetContainerFormat(Guid* pguidContainerFormat)
    {
        return ((delegate* unmanaged[MemberFunction]<IWICBitmapDecoder*, Guid*, int>)(lpVtbl[5]))((IWICBitmapDecoder*)Unsafe.AsPointer(ref this), pguidContainerFormat);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(7)]
    public HRESULT CopyPalette(IWICPalette* pIPalette)
    {
        return ((delegate* unmanaged[MemberFunction]<IWICBitmapDecoder*, IWICPalette*, int>)(lpVtbl[7]))((IWICBitmapDecoder*)Unsafe.AsPointer(ref this), pIPalette);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(9)]
    public HRESULT GetPreview(IWICBitmapSource** ppIBitmapSource)
    {
        return ((delegate* unmanaged[MemberFunction]<IWICBitmapDecoder*, IWICBitmapSource**, int>)(lpVtbl[9]))((IWICBitmapDecoder*)Unsafe.AsPointer(ref this), ppIBitmapSource);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(11)]
    public HRESULT GetThumbnail(IWICBitmapSource** ppIThumbnail)
    {
        return ((delegate* unmanaged[MemberFunction]<IWICBitmapDecoder*, IWICBitmapSource**, int>)(lpVtbl[11]))((IWICBitmapDecoder*)Unsafe.AsPointer(ref this), ppIThumbnail);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(12)]
    public HRESULT GetFrameCount(uint* pCount)
    {
        return ((delegate* unmanaged[MemberFunction]<IWICBitmapDecoder*, uint*, int>)(lpVtbl[12]))((IWICBitmapDecoder*)Unsafe.AsPointer(ref this), pCount);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(13)]
    public HRESULT GetFrame(uint index, IWICBitmapFrameDecode** ppIBitmapFrame)
    {
        return ((delegate* unmanaged[MemberFunction]<IWICBitmapDecoder*, uint, IWICBitmapFrameDecode**, int>)(lpVtbl[13]))((IWICBitmapDecoder*)Unsafe.AsPointer(ref this), index, ppIBitmapFrame);
    }
}