// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d2d1.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TerraFX.Interop.Windows;

namespace TerraFX.Interop.DirectX
{
    [Guid("2CD906A7-12E2-11DC-9FED-001143A055F9")]
    [NativeTypeName("struct ID2D1GradientStopCollection : ID2D1Resource")]
    [NativeInheritance("ID2D1Resource")]
    public unsafe partial struct ID2D1GradientStopCollection : ID2D1GradientStopCollection.Interface
    {
        public void** lpVtbl;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(0)]
        public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
        {
            return ((delegate* unmanaged<ID2D1GradientStopCollection*, Guid*, void**, int>)(lpVtbl[0]))((ID2D1GradientStopCollection*)Unsafe.AsPointer(ref this), riid, ppvObject);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(1)]
        [return: NativeTypeName("ULONG")]
        public uint AddRef()
        {
            return ((delegate* unmanaged<ID2D1GradientStopCollection*, uint>)(lpVtbl[1]))((ID2D1GradientStopCollection*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(2)]
        [return: NativeTypeName("ULONG")]
        public uint Release()
        {
            return ((delegate* unmanaged<ID2D1GradientStopCollection*, uint>)(lpVtbl[2]))((ID2D1GradientStopCollection*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(3)]
        public void GetFactory(ID2D1Factory** factory)
        {
            ((delegate* unmanaged<ID2D1GradientStopCollection*, ID2D1Factory**, void>)(lpVtbl[3]))((ID2D1GradientStopCollection*)Unsafe.AsPointer(ref this), factory);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(4)]
        [return: NativeTypeName("UINT32")]
        public uint GetGradientStopCount()
        {
            return ((delegate* unmanaged<ID2D1GradientStopCollection*, uint>)(lpVtbl[4]))((ID2D1GradientStopCollection*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(5)]
        public void GetGradientStops(D2D1_GRADIENT_STOP* gradientStops, [NativeTypeName("UINT32")] uint gradientStopsCount)
        {
            ((delegate* unmanaged<ID2D1GradientStopCollection*, D2D1_GRADIENT_STOP*, uint, void>)(lpVtbl[5]))((ID2D1GradientStopCollection*)Unsafe.AsPointer(ref this), gradientStops, gradientStopsCount);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(6)]
        public D2D1_GAMMA GetColorInterpolationGamma()
        {
            return ((delegate* unmanaged<ID2D1GradientStopCollection*, D2D1_GAMMA>)(lpVtbl[6]))((ID2D1GradientStopCollection*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(7)]
        public D2D1_EXTEND_MODE GetExtendMode()
        {
            return ((delegate* unmanaged<ID2D1GradientStopCollection*, D2D1_EXTEND_MODE>)(lpVtbl[7]))((ID2D1GradientStopCollection*)Unsafe.AsPointer(ref this));
        }

        public interface Interface : ID2D1Resource.Interface
        {
            [VtblIndex(4)]
            [return: NativeTypeName("UINT32")]
            uint GetGradientStopCount();

            [VtblIndex(5)]
            void GetGradientStops(D2D1_GRADIENT_STOP* gradientStops, [NativeTypeName("UINT32")] uint gradientStopsCount);

            [VtblIndex(6)]
            D2D1_GAMMA GetColorInterpolationGamma();

            [VtblIndex(7)]
            D2D1_EXTEND_MODE GetExtendMode();
        }

        public partial struct Vtbl
        {
            [NativeTypeName("HRESULT (const IID &, void **) __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1GradientStopCollection*, Guid*, void**, int> QueryInterface;

            [NativeTypeName("ULONG () __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1GradientStopCollection*, uint> AddRef;

            [NativeTypeName("ULONG () __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1GradientStopCollection*, uint> Release;

            [NativeTypeName("void (ID2D1Factory **) const __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1GradientStopCollection*, ID2D1Factory**, void> GetFactory;

            [NativeTypeName("UINT32 () const __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1GradientStopCollection*, uint> GetGradientStopCount;

            [NativeTypeName("void (D2D1_GRADIENT_STOP *, UINT32) const __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1GradientStopCollection*, D2D1_GRADIENT_STOP*, uint, void> GetGradientStops;

            [NativeTypeName("D2D1_GAMMA () const __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1GradientStopCollection*, D2D1_GAMMA> GetColorInterpolationGamma;

            [NativeTypeName("D2D1_EXTEND_MODE () const __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1GradientStopCollection*, D2D1_EXTEND_MODE> GetExtendMode;
        }
    }
}
