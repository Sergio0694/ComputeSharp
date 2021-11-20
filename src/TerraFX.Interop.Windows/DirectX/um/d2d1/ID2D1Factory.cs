// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d2d1.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TerraFX.Interop.Windows;

namespace TerraFX.Interop.DirectX
{
    [Guid("06152247-6F50-465A-9245-118BFD3B6007")]
    [NativeTypeName("struct ID2D1Factory : IUnknown")]
    [NativeInheritance("IUnknown")]
    internal unsafe partial struct ID2D1Factory : ID2D1Factory.Interface
    {
        public void** lpVtbl;

        public HRESULT CreateDrawingStateBlock([NativeTypeName("const D2D1_DRAWING_STATE_DESCRIPTION &")] D2D1_DRAWING_STATE_DESCRIPTION* drawingStateDescription, ID2D1DrawingStateBlock** drawingStateBlock)
        {
            return CreateDrawingStateBlock(drawingStateDescription, null, drawingStateBlock);
        }

        public HRESULT CreateDrawingStateBlock(ID2D1DrawingStateBlock** drawingStateBlock)
        {
            return CreateDrawingStateBlock(null, null, drawingStateBlock);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(0)]
        public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
        {
            return ((delegate* unmanaged[Stdcall]<ID2D1Factory*, Guid*, void**, int>)(lpVtbl[0]))((ID2D1Factory*)Unsafe.AsPointer(ref this), riid, ppvObject);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(1)]
        [return: NativeTypeName("ULONG")]
        public uint AddRef()
        {
            return ((delegate* unmanaged[Stdcall]<ID2D1Factory*, uint>)(lpVtbl[1]))((ID2D1Factory*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(2)]
        [return: NativeTypeName("ULONG")]
        public uint Release()
        {
            return ((delegate* unmanaged[Stdcall]<ID2D1Factory*, uint>)(lpVtbl[2]))((ID2D1Factory*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(3)]
        public HRESULT ReloadSystemMetrics()
        {
            return ((delegate* unmanaged[Stdcall]<ID2D1Factory*, int>)(lpVtbl[3]))((ID2D1Factory*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(4)]
        public void GetDesktopDpi(float* dpiX, float* dpiY)
        {
            ((delegate* unmanaged[Stdcall]<ID2D1Factory*, float*, float*, void>)(lpVtbl[4]))((ID2D1Factory*)Unsafe.AsPointer(ref this), dpiX, dpiY);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(5)]
        public HRESULT CreateRectangleGeometry([NativeTypeName("const D2D1_RECT_F *")] D2D_RECT_F* rectangle, ID2D1RectangleGeometry** rectangleGeometry)
        {
            return ((delegate* unmanaged[Stdcall]<ID2D1Factory*, D2D_RECT_F*, ID2D1RectangleGeometry**, int>)(lpVtbl[5]))((ID2D1Factory*)Unsafe.AsPointer(ref this), rectangle, rectangleGeometry);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(6)]
        public HRESULT CreateRoundedRectangleGeometry([NativeTypeName("const D2D1_ROUNDED_RECT *")] D2D1_ROUNDED_RECT* roundedRectangle, ID2D1RoundedRectangleGeometry** roundedRectangleGeometry)
        {
            return ((delegate* unmanaged[Stdcall]<ID2D1Factory*, D2D1_ROUNDED_RECT*, ID2D1RoundedRectangleGeometry**, int>)(lpVtbl[6]))((ID2D1Factory*)Unsafe.AsPointer(ref this), roundedRectangle, roundedRectangleGeometry);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(7)]
        public HRESULT CreateEllipseGeometry([NativeTypeName("const D2D1_ELLIPSE *")] D2D1_ELLIPSE* ellipse, ID2D1EllipseGeometry** ellipseGeometry)
        {
            return ((delegate* unmanaged[Stdcall]<ID2D1Factory*, D2D1_ELLIPSE*, ID2D1EllipseGeometry**, int>)(lpVtbl[7]))((ID2D1Factory*)Unsafe.AsPointer(ref this), ellipse, ellipseGeometry);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(8)]
        public HRESULT CreateGeometryGroup(D2D1_FILL_MODE fillMode, ID2D1Geometry** geometries, [NativeTypeName("UINT32")] uint geometriesCount, ID2D1GeometryGroup** geometryGroup)
        {
            return ((delegate* unmanaged[Stdcall]<ID2D1Factory*, D2D1_FILL_MODE, ID2D1Geometry**, uint, ID2D1GeometryGroup**, int>)(lpVtbl[8]))((ID2D1Factory*)Unsafe.AsPointer(ref this), fillMode, geometries, geometriesCount, geometryGroup);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(9)]
        public HRESULT CreateTransformedGeometry(ID2D1Geometry* sourceGeometry, [NativeTypeName("const D2D1_MATRIX_3X2_F *")] D2D_MATRIX_3X2_F* transform, ID2D1TransformedGeometry** transformedGeometry)
        {
            return ((delegate* unmanaged[Stdcall]<ID2D1Factory*, ID2D1Geometry*, D2D_MATRIX_3X2_F*, ID2D1TransformedGeometry**, int>)(lpVtbl[9]))((ID2D1Factory*)Unsafe.AsPointer(ref this), sourceGeometry, transform, transformedGeometry);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(10)]
        public HRESULT CreatePathGeometry(ID2D1PathGeometry** pathGeometry)
        {
            return ((delegate* unmanaged[Stdcall]<ID2D1Factory*, ID2D1PathGeometry**, int>)(lpVtbl[10]))((ID2D1Factory*)Unsafe.AsPointer(ref this), pathGeometry);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(11)]
        public HRESULT CreateStrokeStyle([NativeTypeName("const D2D1_STROKE_STYLE_PROPERTIES *")] D2D1_STROKE_STYLE_PROPERTIES* strokeStyleProperties, [NativeTypeName("const FLOAT *")] float* dashes, [NativeTypeName("UINT32")] uint dashesCount, ID2D1StrokeStyle** strokeStyle)
        {
            return ((delegate* unmanaged[Stdcall]<ID2D1Factory*, D2D1_STROKE_STYLE_PROPERTIES*, float*, uint, ID2D1StrokeStyle**, int>)(lpVtbl[11]))((ID2D1Factory*)Unsafe.AsPointer(ref this), strokeStyleProperties, dashes, dashesCount, strokeStyle);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(12)]
        public HRESULT CreateDrawingStateBlock([NativeTypeName("const D2D1_DRAWING_STATE_DESCRIPTION *")] D2D1_DRAWING_STATE_DESCRIPTION* drawingStateDescription, IDWriteRenderingParams* textRenderingParams, ID2D1DrawingStateBlock** drawingStateBlock)
        {
            return ((delegate* unmanaged[Stdcall]<ID2D1Factory*, D2D1_DRAWING_STATE_DESCRIPTION*, IDWriteRenderingParams*, ID2D1DrawingStateBlock**, int>)(lpVtbl[12]))((ID2D1Factory*)Unsafe.AsPointer(ref this), drawingStateDescription, textRenderingParams, drawingStateBlock);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(13)]
        public HRESULT CreateWicBitmapRenderTarget(IWICBitmap* target, [NativeTypeName("const D2D1_RENDER_TARGET_PROPERTIES *")] D2D1_RENDER_TARGET_PROPERTIES* renderTargetProperties, ID2D1RenderTarget** renderTarget)
        {
            return ((delegate* unmanaged[Stdcall]<ID2D1Factory*, IWICBitmap*, D2D1_RENDER_TARGET_PROPERTIES*, ID2D1RenderTarget**, int>)(lpVtbl[13]))((ID2D1Factory*)Unsafe.AsPointer(ref this), target, renderTargetProperties, renderTarget);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(14)]
        public HRESULT CreateHwndRenderTarget([NativeTypeName("const D2D1_RENDER_TARGET_PROPERTIES *")] D2D1_RENDER_TARGET_PROPERTIES* renderTargetProperties, [NativeTypeName("const D2D1_HWND_RENDER_TARGET_PROPERTIES *")] D2D1_HWND_RENDER_TARGET_PROPERTIES* hwndRenderTargetProperties, ID2D1HwndRenderTarget** hwndRenderTarget)
        {
            return ((delegate* unmanaged[Stdcall]<ID2D1Factory*, D2D1_RENDER_TARGET_PROPERTIES*, D2D1_HWND_RENDER_TARGET_PROPERTIES*, ID2D1HwndRenderTarget**, int>)(lpVtbl[14]))((ID2D1Factory*)Unsafe.AsPointer(ref this), renderTargetProperties, hwndRenderTargetProperties, hwndRenderTarget);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(15)]
        public HRESULT CreateDxgiSurfaceRenderTarget(IDXGISurface* dxgiSurface, [NativeTypeName("const D2D1_RENDER_TARGET_PROPERTIES *")] D2D1_RENDER_TARGET_PROPERTIES* renderTargetProperties, ID2D1RenderTarget** renderTarget)
        {
            return ((delegate* unmanaged[Stdcall]<ID2D1Factory*, IDXGISurface*, D2D1_RENDER_TARGET_PROPERTIES*, ID2D1RenderTarget**, int>)(lpVtbl[15]))((ID2D1Factory*)Unsafe.AsPointer(ref this), dxgiSurface, renderTargetProperties, renderTarget);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(16)]
        public HRESULT CreateDCRenderTarget([NativeTypeName("const D2D1_RENDER_TARGET_PROPERTIES *")] D2D1_RENDER_TARGET_PROPERTIES* renderTargetProperties, ID2D1DCRenderTarget** dcRenderTarget)
        {
            return ((delegate* unmanaged[Stdcall]<ID2D1Factory*, D2D1_RENDER_TARGET_PROPERTIES*, ID2D1DCRenderTarget**, int>)(lpVtbl[16]))((ID2D1Factory*)Unsafe.AsPointer(ref this), renderTargetProperties, dcRenderTarget);
        }

        public interface Interface : IUnknown.Interface
        {
            [VtblIndex(3)]
            HRESULT ReloadSystemMetrics();

            [VtblIndex(4)]
            void GetDesktopDpi(float* dpiX, float* dpiY);

            [VtblIndex(5)]
            HRESULT CreateRectangleGeometry([NativeTypeName("const D2D1_RECT_F *")] D2D_RECT_F* rectangle, ID2D1RectangleGeometry** rectangleGeometry);

            [VtblIndex(6)]
            HRESULT CreateRoundedRectangleGeometry([NativeTypeName("const D2D1_ROUNDED_RECT *")] D2D1_ROUNDED_RECT* roundedRectangle, ID2D1RoundedRectangleGeometry** roundedRectangleGeometry);

            [VtblIndex(7)]
            HRESULT CreateEllipseGeometry([NativeTypeName("const D2D1_ELLIPSE *")] D2D1_ELLIPSE* ellipse, ID2D1EllipseGeometry** ellipseGeometry);

            [VtblIndex(8)]
            HRESULT CreateGeometryGroup(D2D1_FILL_MODE fillMode, ID2D1Geometry** geometries, [NativeTypeName("UINT32")] uint geometriesCount, ID2D1GeometryGroup** geometryGroup);

            [VtblIndex(9)]
            HRESULT CreateTransformedGeometry(ID2D1Geometry* sourceGeometry, [NativeTypeName("const D2D1_MATRIX_3X2_F *")] D2D_MATRIX_3X2_F* transform, ID2D1TransformedGeometry** transformedGeometry);

            [VtblIndex(10)]
            HRESULT CreatePathGeometry(ID2D1PathGeometry** pathGeometry);

            [VtblIndex(11)]
            HRESULT CreateStrokeStyle([NativeTypeName("const D2D1_STROKE_STYLE_PROPERTIES *")] D2D1_STROKE_STYLE_PROPERTIES* strokeStyleProperties, [NativeTypeName("const FLOAT *")] float* dashes, [NativeTypeName("UINT32")] uint dashesCount, ID2D1StrokeStyle** strokeStyle);

            [VtblIndex(12)]
            HRESULT CreateDrawingStateBlock([NativeTypeName("const D2D1_DRAWING_STATE_DESCRIPTION *")] D2D1_DRAWING_STATE_DESCRIPTION* drawingStateDescription, IDWriteRenderingParams* textRenderingParams, ID2D1DrawingStateBlock** drawingStateBlock);

            [VtblIndex(13)]
            HRESULT CreateWicBitmapRenderTarget(IWICBitmap* target, [NativeTypeName("const D2D1_RENDER_TARGET_PROPERTIES *")] D2D1_RENDER_TARGET_PROPERTIES* renderTargetProperties, ID2D1RenderTarget** renderTarget);

            [VtblIndex(14)]
            HRESULT CreateHwndRenderTarget([NativeTypeName("const D2D1_RENDER_TARGET_PROPERTIES *")] D2D1_RENDER_TARGET_PROPERTIES* renderTargetProperties, [NativeTypeName("const D2D1_HWND_RENDER_TARGET_PROPERTIES *")] D2D1_HWND_RENDER_TARGET_PROPERTIES* hwndRenderTargetProperties, ID2D1HwndRenderTarget** hwndRenderTarget);

            [VtblIndex(15)]
            HRESULT CreateDxgiSurfaceRenderTarget(IDXGISurface* dxgiSurface, [NativeTypeName("const D2D1_RENDER_TARGET_PROPERTIES *")] D2D1_RENDER_TARGET_PROPERTIES* renderTargetProperties, ID2D1RenderTarget** renderTarget);

            [VtblIndex(16)]
            HRESULT CreateDCRenderTarget([NativeTypeName("const D2D1_RENDER_TARGET_PROPERTIES *")] D2D1_RENDER_TARGET_PROPERTIES* renderTargetProperties, ID2D1DCRenderTarget** dcRenderTarget);
        }
    }
}
