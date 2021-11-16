// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/dwrite.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TerraFX.Interop.Windows;

namespace TerraFX.Interop.DirectX
{
    [Guid("EAF3A2DA-ECF4-4D24-B644-B34F6842024B")]
    [NativeTypeName("struct IDWritePixelSnapping : IUnknown")]
    [NativeInheritance("IUnknown")]
    public unsafe partial struct IDWritePixelSnapping : IDWritePixelSnapping.Interface
    {
        public void** lpVtbl;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(0)]
        public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
        {
            return ((delegate* unmanaged<IDWritePixelSnapping*, Guid*, void**, int>)(lpVtbl[0]))((IDWritePixelSnapping*)Unsafe.AsPointer(ref this), riid, ppvObject);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(1)]
        [return: NativeTypeName("ULONG")]
        public uint AddRef()
        {
            return ((delegate* unmanaged<IDWritePixelSnapping*, uint>)(lpVtbl[1]))((IDWritePixelSnapping*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(2)]
        [return: NativeTypeName("ULONG")]
        public uint Release()
        {
            return ((delegate* unmanaged<IDWritePixelSnapping*, uint>)(lpVtbl[2]))((IDWritePixelSnapping*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(3)]
        public HRESULT IsPixelSnappingDisabled(void* clientDrawingContext, BOOL* isDisabled)
        {
            return ((delegate* unmanaged<IDWritePixelSnapping*, void*, BOOL*, int>)(lpVtbl[3]))((IDWritePixelSnapping*)Unsafe.AsPointer(ref this), clientDrawingContext, isDisabled);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(4)]
        public HRESULT GetCurrentTransform(void* clientDrawingContext, DWRITE_MATRIX* transform)
        {
            return ((delegate* unmanaged<IDWritePixelSnapping*, void*, DWRITE_MATRIX*, int>)(lpVtbl[4]))((IDWritePixelSnapping*)Unsafe.AsPointer(ref this), clientDrawingContext, transform);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(5)]
        public HRESULT GetPixelsPerDip(void* clientDrawingContext, float* pixelsPerDip)
        {
            return ((delegate* unmanaged<IDWritePixelSnapping*, void*, float*, int>)(lpVtbl[5]))((IDWritePixelSnapping*)Unsafe.AsPointer(ref this), clientDrawingContext, pixelsPerDip);
        }

        public interface Interface : IUnknown.Interface
        {
            [VtblIndex(3)]
            HRESULT IsPixelSnappingDisabled(void* clientDrawingContext, BOOL* isDisabled);

            [VtblIndex(4)]
            HRESULT GetCurrentTransform(void* clientDrawingContext, DWRITE_MATRIX* transform);

            [VtblIndex(5)]
            HRESULT GetPixelsPerDip(void* clientDrawingContext, float* pixelsPerDip);
        }

        public partial struct Vtbl
        {
            [NativeTypeName("HRESULT (const IID &, void **) __attribute__((stdcall))")]
            public delegate* unmanaged<IDWritePixelSnapping*, Guid*, void**, int> QueryInterface;

            [NativeTypeName("ULONG () __attribute__((stdcall))")]
            public delegate* unmanaged<IDWritePixelSnapping*, uint> AddRef;

            [NativeTypeName("ULONG () __attribute__((stdcall))")]
            public delegate* unmanaged<IDWritePixelSnapping*, uint> Release;

            [NativeTypeName("HRESULT (void *, BOOL *) __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<IDWritePixelSnapping*, void*, BOOL*, int> IsPixelSnappingDisabled;

            [NativeTypeName("HRESULT (void *, DWRITE_MATRIX *) __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<IDWritePixelSnapping*, void*, DWRITE_MATRIX*, int> GetCurrentTransform;

            [NativeTypeName("HRESULT (void *, FLOAT *) __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<IDWritePixelSnapping*, void*, float*, int> GetPixelsPerDip;
        }
    }
}
