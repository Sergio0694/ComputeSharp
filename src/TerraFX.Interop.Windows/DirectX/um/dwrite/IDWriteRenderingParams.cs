// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/dwrite.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TerraFX.Interop.Windows;

namespace TerraFX.Interop.DirectX
{
    [Guid("2F0DA53A-2ADD-47CD-82EE-D9EC34688E75")]
    [NativeTypeName("struct IDWriteRenderingParams : IUnknown")]
    [NativeInheritance("IUnknown")]
    internal unsafe partial struct IDWriteRenderingParams : IDWriteRenderingParams.Interface
    {
        public void** lpVtbl;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(0)]
        public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
        {
            return ((delegate* unmanaged[Stdcall]<IDWriteRenderingParams*, Guid*, void**, int>)(lpVtbl[0]))((IDWriteRenderingParams*)Unsafe.AsPointer(ref this), riid, ppvObject);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(1)]
        [return: NativeTypeName("ULONG")]
        public uint AddRef()
        {
            return ((delegate* unmanaged[Stdcall]<IDWriteRenderingParams*, uint>)(lpVtbl[1]))((IDWriteRenderingParams*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(2)]
        [return: NativeTypeName("ULONG")]
        public uint Release()
        {
            return ((delegate* unmanaged[Stdcall]<IDWriteRenderingParams*, uint>)(lpVtbl[2]))((IDWriteRenderingParams*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(3)]
        public float GetGamma()
        {
            return ((delegate* unmanaged[Stdcall]<IDWriteRenderingParams*, float>)(lpVtbl[3]))((IDWriteRenderingParams*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(4)]
        public float GetEnhancedContrast()
        {
            return ((delegate* unmanaged[Stdcall]<IDWriteRenderingParams*, float>)(lpVtbl[4]))((IDWriteRenderingParams*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(5)]
        public float GetClearTypeLevel()
        {
            return ((delegate* unmanaged[Stdcall]<IDWriteRenderingParams*, float>)(lpVtbl[5]))((IDWriteRenderingParams*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(6)]
        public DWRITE_PIXEL_GEOMETRY GetPixelGeometry()
        {
            return ((delegate* unmanaged[Stdcall]<IDWriteRenderingParams*, DWRITE_PIXEL_GEOMETRY>)(lpVtbl[6]))((IDWriteRenderingParams*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(7)]
        public DWRITE_RENDERING_MODE GetRenderingMode()
        {
            return ((delegate* unmanaged[Stdcall]<IDWriteRenderingParams*, DWRITE_RENDERING_MODE>)(lpVtbl[7]))((IDWriteRenderingParams*)Unsafe.AsPointer(ref this));
        }

        public interface Interface : IUnknown.Interface
        {
            [VtblIndex(3)]
            float GetGamma();

            [VtblIndex(4)]
            float GetEnhancedContrast();

            [VtblIndex(5)]
            float GetClearTypeLevel();

            [VtblIndex(6)]
            DWRITE_PIXEL_GEOMETRY GetPixelGeometry();

            [VtblIndex(7)]
            DWRITE_RENDERING_MODE GetRenderingMode();
        }
    }
}
