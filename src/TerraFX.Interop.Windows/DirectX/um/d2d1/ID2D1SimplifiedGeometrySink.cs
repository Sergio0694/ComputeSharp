// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d2d1.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TerraFX.Interop.Windows;

namespace TerraFX.Interop.DirectX
{
    [Guid("2CD9069E-12E2-11DC-9FED-001143A055F9")]
    [NativeTypeName("struct ID2D1SimplifiedGeometrySink : IUnknown")]
    [NativeInheritance("IUnknown")]
    internal unsafe partial struct ID2D1SimplifiedGeometrySink : ID2D1SimplifiedGeometrySink.Interface
    {
        public void** lpVtbl;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(0)]
        public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
        {
            return ((delegate* unmanaged[Stdcall]<ID2D1SimplifiedGeometrySink*, Guid*, void**, int>)(lpVtbl[0]))((ID2D1SimplifiedGeometrySink*)Unsafe.AsPointer(ref this), riid, ppvObject);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(1)]
        [return: NativeTypeName("ULONG")]
        public uint AddRef()
        {
            return ((delegate* unmanaged[Stdcall]<ID2D1SimplifiedGeometrySink*, uint>)(lpVtbl[1]))((ID2D1SimplifiedGeometrySink*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(2)]
        [return: NativeTypeName("ULONG")]
        public uint Release()
        {
            return ((delegate* unmanaged[Stdcall]<ID2D1SimplifiedGeometrySink*, uint>)(lpVtbl[2]))((ID2D1SimplifiedGeometrySink*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(3)]
        public void SetFillMode(D2D1_FILL_MODE fillMode)
        {
            ((delegate* unmanaged[Stdcall]<ID2D1SimplifiedGeometrySink*, D2D1_FILL_MODE, void>)(lpVtbl[3]))((ID2D1SimplifiedGeometrySink*)Unsafe.AsPointer(ref this), fillMode);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(4)]
        public void SetSegmentFlags(D2D1_PATH_SEGMENT vertexFlags)
        {
            ((delegate* unmanaged[Stdcall]<ID2D1SimplifiedGeometrySink*, D2D1_PATH_SEGMENT, void>)(lpVtbl[4]))((ID2D1SimplifiedGeometrySink*)Unsafe.AsPointer(ref this), vertexFlags);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(5)]
        public void BeginFigure([NativeTypeName("D2D1_POINT_2F")] D2D_POINT_2F startPoint, D2D1_FIGURE_BEGIN figureBegin)
        {
            ((delegate* unmanaged[Stdcall]<ID2D1SimplifiedGeometrySink*, D2D_POINT_2F, D2D1_FIGURE_BEGIN, void>)(lpVtbl[5]))((ID2D1SimplifiedGeometrySink*)Unsafe.AsPointer(ref this), startPoint, figureBegin);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(6)]
        public void AddLines([NativeTypeName("const D2D1_POINT_2F *")] D2D_POINT_2F* points, [NativeTypeName("UINT32")] uint pointsCount)
        {
            ((delegate* unmanaged[Stdcall]<ID2D1SimplifiedGeometrySink*, D2D_POINT_2F*, uint, void>)(lpVtbl[6]))((ID2D1SimplifiedGeometrySink*)Unsafe.AsPointer(ref this), points, pointsCount);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(7)]
        public void AddBeziers([NativeTypeName("const D2D1_BEZIER_SEGMENT *")] D2D1_BEZIER_SEGMENT* beziers, [NativeTypeName("UINT32")] uint beziersCount)
        {
            ((delegate* unmanaged[Stdcall]<ID2D1SimplifiedGeometrySink*, D2D1_BEZIER_SEGMENT*, uint, void>)(lpVtbl[7]))((ID2D1SimplifiedGeometrySink*)Unsafe.AsPointer(ref this), beziers, beziersCount);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(8)]
        public void EndFigure(D2D1_FIGURE_END figureEnd)
        {
            ((delegate* unmanaged[Stdcall]<ID2D1SimplifiedGeometrySink*, D2D1_FIGURE_END, void>)(lpVtbl[8]))((ID2D1SimplifiedGeometrySink*)Unsafe.AsPointer(ref this), figureEnd);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(9)]
        public HRESULT Close()
        {
            return ((delegate* unmanaged[Stdcall]<ID2D1SimplifiedGeometrySink*, int>)(lpVtbl[9]))((ID2D1SimplifiedGeometrySink*)Unsafe.AsPointer(ref this));
        }

        public interface Interface : IUnknown.Interface
        {
            [VtblIndex(3)]
            void SetFillMode(D2D1_FILL_MODE fillMode);

            [VtblIndex(4)]
            void SetSegmentFlags(D2D1_PATH_SEGMENT vertexFlags);

            [VtblIndex(5)]
            void BeginFigure([NativeTypeName("D2D1_POINT_2F")] D2D_POINT_2F startPoint, D2D1_FIGURE_BEGIN figureBegin);

            [VtblIndex(6)]
            void AddLines([NativeTypeName("const D2D1_POINT_2F *")] D2D_POINT_2F* points, [NativeTypeName("UINT32")] uint pointsCount);

            [VtblIndex(7)]
            void AddBeziers([NativeTypeName("const D2D1_BEZIER_SEGMENT *")] D2D1_BEZIER_SEGMENT* beziers, [NativeTypeName("UINT32")] uint beziersCount);

            [VtblIndex(8)]
            void EndFigure(D2D1_FIGURE_END figureEnd);

            [VtblIndex(9)]
            HRESULT Close();
        }
    }
}
