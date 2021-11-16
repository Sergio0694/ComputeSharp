// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/wincodec.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace TerraFX.Interop.Windows
{
    [Guid("00000123-A8F2-4877-BA0A-FD2B6645FB94")]
    [NativeTypeName("struct IWICBitmapLock : IUnknown")]
    [NativeInheritance("IUnknown")]
    public unsafe partial struct IWICBitmapLock : IWICBitmapLock.Interface
    {
        public void** lpVtbl;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(0)]
        public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
        {
            return ((delegate* unmanaged<IWICBitmapLock*, Guid*, void**, int>)(lpVtbl[0]))((IWICBitmapLock*)Unsafe.AsPointer(ref this), riid, ppvObject);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(1)]
        [return: NativeTypeName("ULONG")]
        public uint AddRef()
        {
            return ((delegate* unmanaged<IWICBitmapLock*, uint>)(lpVtbl[1]))((IWICBitmapLock*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(2)]
        [return: NativeTypeName("ULONG")]
        public uint Release()
        {
            return ((delegate* unmanaged<IWICBitmapLock*, uint>)(lpVtbl[2]))((IWICBitmapLock*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(3)]
        public HRESULT GetSize(uint* puiWidth, uint* puiHeight)
        {
            return ((delegate* unmanaged<IWICBitmapLock*, uint*, uint*, int>)(lpVtbl[3]))((IWICBitmapLock*)Unsafe.AsPointer(ref this), puiWidth, puiHeight);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(4)]
        public HRESULT GetStride(uint* pcbStride)
        {
            return ((delegate* unmanaged<IWICBitmapLock*, uint*, int>)(lpVtbl[4]))((IWICBitmapLock*)Unsafe.AsPointer(ref this), pcbStride);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(5)]
        public HRESULT GetDataPointer(uint* pcbBufferSize, [NativeTypeName("WICInProcPointer *")] byte** ppbData)
        {
            return ((delegate* unmanaged<IWICBitmapLock*, uint*, byte**, int>)(lpVtbl[5]))((IWICBitmapLock*)Unsafe.AsPointer(ref this), pcbBufferSize, ppbData);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(6)]
        public HRESULT GetPixelFormat([NativeTypeName("WICPixelFormatGUID *")] Guid* pPixelFormat)
        {
            return ((delegate* unmanaged<IWICBitmapLock*, Guid*, int>)(lpVtbl[6]))((IWICBitmapLock*)Unsafe.AsPointer(ref this), pPixelFormat);
        }

        public interface Interface : IUnknown.Interface
        {
            [VtblIndex(3)]
            HRESULT GetSize(uint* puiWidth, uint* puiHeight);

            [VtblIndex(4)]
            HRESULT GetStride(uint* pcbStride);

            [VtblIndex(5)]
            HRESULT GetDataPointer(uint* pcbBufferSize, [NativeTypeName("WICInProcPointer *")] byte** ppbData);

            [VtblIndex(6)]
            HRESULT GetPixelFormat([NativeTypeName("WICPixelFormatGUID *")] Guid* pPixelFormat);
        }

        public partial struct Vtbl
        {
            [NativeTypeName("HRESULT (const IID &, void **) __attribute__((stdcall))")]
            public delegate* unmanaged<IWICBitmapLock*, Guid*, void**, int> QueryInterface;

            [NativeTypeName("ULONG () __attribute__((stdcall))")]
            public delegate* unmanaged<IWICBitmapLock*, uint> AddRef;

            [NativeTypeName("ULONG () __attribute__((stdcall))")]
            public delegate* unmanaged<IWICBitmapLock*, uint> Release;

            [NativeTypeName("HRESULT (UINT *, UINT *) __attribute__((stdcall))")]
            public delegate* unmanaged<IWICBitmapLock*, uint*, uint*, int> GetSize;

            [NativeTypeName("HRESULT (UINT *) __attribute__((stdcall))")]
            public delegate* unmanaged<IWICBitmapLock*, uint*, int> GetStride;

            [NativeTypeName("HRESULT (UINT *, WICInProcPointer *) __attribute__((stdcall))")]
            public delegate* unmanaged<IWICBitmapLock*, uint*, byte**, int> GetDataPointer;

            [NativeTypeName("HRESULT (WICPixelFormatGUID *) __attribute__((stdcall))")]
            public delegate* unmanaged<IWICBitmapLock*, Guid*, int> GetPixelFormat;
        }
    }
}
