// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d2d1_1.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TerraFX.Interop.Windows;

namespace TerraFX.Interop.DirectX
{
    [Guid("41343A53-E41A-49A2-91CD-21793BBB62E5")]
    [NativeTypeName("struct ID2D1BitmapBrush1 : ID2D1BitmapBrush")]
    [NativeInheritance("ID2D1BitmapBrush")]
    public unsafe partial struct ID2D1BitmapBrush1 : ID2D1BitmapBrush1.Interface
    {
        public void** lpVtbl;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(0)]
        public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
        {
            return ((delegate* unmanaged<ID2D1BitmapBrush1*, Guid*, void**, int>)(lpVtbl[0]))((ID2D1BitmapBrush1*)Unsafe.AsPointer(ref this), riid, ppvObject);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(1)]
        [return: NativeTypeName("ULONG")]
        public uint AddRef()
        {
            return ((delegate* unmanaged<ID2D1BitmapBrush1*, uint>)(lpVtbl[1]))((ID2D1BitmapBrush1*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(2)]
        [return: NativeTypeName("ULONG")]
        public uint Release()
        {
            return ((delegate* unmanaged<ID2D1BitmapBrush1*, uint>)(lpVtbl[2]))((ID2D1BitmapBrush1*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(3)]
        public void GetFactory(ID2D1Factory** factory)
        {
            ((delegate* unmanaged<ID2D1BitmapBrush1*, ID2D1Factory**, void>)(lpVtbl[3]))((ID2D1BitmapBrush1*)Unsafe.AsPointer(ref this), factory);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(4)]
        public void SetOpacity(float opacity)
        {
            ((delegate* unmanaged<ID2D1BitmapBrush1*, float, void>)(lpVtbl[4]))((ID2D1BitmapBrush1*)Unsafe.AsPointer(ref this), opacity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(5)]
        public void SetTransform([NativeTypeName("const D2D1_MATRIX_3X2_F *")] D2D_MATRIX_3X2_F* transform)
        {
            ((delegate* unmanaged<ID2D1BitmapBrush1*, D2D_MATRIX_3X2_F*, void>)(lpVtbl[5]))((ID2D1BitmapBrush1*)Unsafe.AsPointer(ref this), transform);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(6)]
        public float GetOpacity()
        {
            return ((delegate* unmanaged<ID2D1BitmapBrush1*, float>)(lpVtbl[6]))((ID2D1BitmapBrush1*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(7)]
        public void GetTransform([NativeTypeName("D2D1_MATRIX_3X2_F *")] D2D_MATRIX_3X2_F* transform)
        {
            ((delegate* unmanaged<ID2D1BitmapBrush1*, D2D_MATRIX_3X2_F*, void>)(lpVtbl[7]))((ID2D1BitmapBrush1*)Unsafe.AsPointer(ref this), transform);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(8)]
        public void SetExtendModeX(D2D1_EXTEND_MODE extendModeX)
        {
            ((delegate* unmanaged<ID2D1BitmapBrush1*, D2D1_EXTEND_MODE, void>)(lpVtbl[8]))((ID2D1BitmapBrush1*)Unsafe.AsPointer(ref this), extendModeX);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(9)]
        public void SetExtendModeY(D2D1_EXTEND_MODE extendModeY)
        {
            ((delegate* unmanaged<ID2D1BitmapBrush1*, D2D1_EXTEND_MODE, void>)(lpVtbl[9]))((ID2D1BitmapBrush1*)Unsafe.AsPointer(ref this), extendModeY);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(10)]
        public void SetInterpolationMode(D2D1_BITMAP_INTERPOLATION_MODE interpolationMode)
        {
            ((delegate* unmanaged<ID2D1BitmapBrush1*, D2D1_BITMAP_INTERPOLATION_MODE, void>)(lpVtbl[10]))((ID2D1BitmapBrush1*)Unsafe.AsPointer(ref this), interpolationMode);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(11)]
        public void SetBitmap(ID2D1Bitmap* bitmap)
        {
            ((delegate* unmanaged<ID2D1BitmapBrush1*, ID2D1Bitmap*, void>)(lpVtbl[11]))((ID2D1BitmapBrush1*)Unsafe.AsPointer(ref this), bitmap);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(12)]
        public D2D1_EXTEND_MODE GetExtendModeX()
        {
            return ((delegate* unmanaged<ID2D1BitmapBrush1*, D2D1_EXTEND_MODE>)(lpVtbl[12]))((ID2D1BitmapBrush1*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(13)]
        public D2D1_EXTEND_MODE GetExtendModeY()
        {
            return ((delegate* unmanaged<ID2D1BitmapBrush1*, D2D1_EXTEND_MODE>)(lpVtbl[13]))((ID2D1BitmapBrush1*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(14)]
        public D2D1_BITMAP_INTERPOLATION_MODE GetInterpolationMode()
        {
            return ((delegate* unmanaged<ID2D1BitmapBrush1*, D2D1_BITMAP_INTERPOLATION_MODE>)(lpVtbl[14]))((ID2D1BitmapBrush1*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(15)]
        public void GetBitmap(ID2D1Bitmap** bitmap)
        {
            ((delegate* unmanaged<ID2D1BitmapBrush1*, ID2D1Bitmap**, void>)(lpVtbl[15]))((ID2D1BitmapBrush1*)Unsafe.AsPointer(ref this), bitmap);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(16)]
        public void SetInterpolationMode1(D2D1_INTERPOLATION_MODE interpolationMode)
        {
            ((delegate* unmanaged<ID2D1BitmapBrush1*, D2D1_INTERPOLATION_MODE, void>)(lpVtbl[16]))((ID2D1BitmapBrush1*)Unsafe.AsPointer(ref this), interpolationMode);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(17)]
        public D2D1_INTERPOLATION_MODE GetInterpolationMode1()
        {
            return ((delegate* unmanaged<ID2D1BitmapBrush1*, D2D1_INTERPOLATION_MODE>)(lpVtbl[17]))((ID2D1BitmapBrush1*)Unsafe.AsPointer(ref this));
        }

        public interface Interface : ID2D1BitmapBrush.Interface
        {
            [VtblIndex(16)]
            void SetInterpolationMode1(D2D1_INTERPOLATION_MODE interpolationMode);

            [VtblIndex(17)]
            D2D1_INTERPOLATION_MODE GetInterpolationMode1();
        }

        public partial struct Vtbl
        {
            [NativeTypeName("HRESULT (const IID &, void **) __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1BitmapBrush1*, Guid*, void**, int> QueryInterface;

            [NativeTypeName("ULONG () __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1BitmapBrush1*, uint> AddRef;

            [NativeTypeName("ULONG () __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1BitmapBrush1*, uint> Release;

            [NativeTypeName("void (ID2D1Factory **) const __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1BitmapBrush1*, ID2D1Factory**, void> GetFactory;

            [NativeTypeName("void (FLOAT) __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1BitmapBrush1*, float, void> SetOpacity;

            [NativeTypeName("void (const D2D1_MATRIX_3X2_F *) __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1BitmapBrush1*, D2D_MATRIX_3X2_F*, void> SetTransform;

            [NativeTypeName("FLOAT () const __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1BitmapBrush1*, float> GetOpacity;

            [NativeTypeName("void (D2D1_MATRIX_3X2_F *) const __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1BitmapBrush1*, D2D_MATRIX_3X2_F*, void> GetTransform;

            [NativeTypeName("void (D2D1_EXTEND_MODE) __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1BitmapBrush1*, D2D1_EXTEND_MODE, void> SetExtendModeX;

            [NativeTypeName("void (D2D1_EXTEND_MODE) __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1BitmapBrush1*, D2D1_EXTEND_MODE, void> SetExtendModeY;

            [NativeTypeName("void (D2D1_BITMAP_INTERPOLATION_MODE) __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1BitmapBrush1*, D2D1_BITMAP_INTERPOLATION_MODE, void> SetInterpolationMode;

            [NativeTypeName("void (ID2D1Bitmap *) __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1BitmapBrush1*, ID2D1Bitmap*, void> SetBitmap;

            [NativeTypeName("D2D1_EXTEND_MODE () const __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1BitmapBrush1*, D2D1_EXTEND_MODE> GetExtendModeX;

            [NativeTypeName("D2D1_EXTEND_MODE () const __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1BitmapBrush1*, D2D1_EXTEND_MODE> GetExtendModeY;

            [NativeTypeName("D2D1_BITMAP_INTERPOLATION_MODE () const __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1BitmapBrush1*, D2D1_BITMAP_INTERPOLATION_MODE> GetInterpolationMode;

            [NativeTypeName("void (ID2D1Bitmap **) const __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1BitmapBrush1*, ID2D1Bitmap**, void> GetBitmap;

            [NativeTypeName("void (D2D1_INTERPOLATION_MODE) __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1BitmapBrush1*, D2D1_INTERPOLATION_MODE, void> SetInterpolationMode1;

            [NativeTypeName("D2D1_INTERPOLATION_MODE () const __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1BitmapBrush1*, D2D1_INTERPOLATION_MODE> GetInterpolationMode1;
        }
    }
}
