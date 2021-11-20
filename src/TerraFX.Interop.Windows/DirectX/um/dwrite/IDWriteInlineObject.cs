// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/dwrite.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TerraFX.Interop.Windows;

namespace TerraFX.Interop.DirectX
{
    [Guid("8339FDE3-106F-47AB-8373-1C6295EB10B3")]
    [NativeTypeName("struct IDWriteInlineObject : IUnknown")]
    [NativeInheritance("IUnknown")]
    internal unsafe partial struct IDWriteInlineObject : IDWriteInlineObject.Interface
    {
        public void** lpVtbl;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(0)]
        public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
        {
            return ((delegate* unmanaged[Stdcall]<IDWriteInlineObject*, Guid*, void**, int>)(lpVtbl[0]))((IDWriteInlineObject*)Unsafe.AsPointer(ref this), riid, ppvObject);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(1)]
        [return: NativeTypeName("ULONG")]
        public uint AddRef()
        {
            return ((delegate* unmanaged[Stdcall]<IDWriteInlineObject*, uint>)(lpVtbl[1]))((IDWriteInlineObject*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(2)]
        [return: NativeTypeName("ULONG")]
        public uint Release()
        {
            return ((delegate* unmanaged[Stdcall]<IDWriteInlineObject*, uint>)(lpVtbl[2]))((IDWriteInlineObject*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(3)]
        public HRESULT Draw(void* clientDrawingContext, IDWriteTextRenderer* renderer, float originX, float originY, BOOL isSideways, BOOL isRightToLeft, IUnknown* clientDrawingEffect)
        {
            return ((delegate* unmanaged[Stdcall]<IDWriteInlineObject*, void*, IDWriteTextRenderer*, float, float, BOOL, BOOL, IUnknown*, int>)(lpVtbl[3]))((IDWriteInlineObject*)Unsafe.AsPointer(ref this), clientDrawingContext, renderer, originX, originY, isSideways, isRightToLeft, clientDrawingEffect);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(4)]
        public HRESULT GetMetrics(DWRITE_INLINE_OBJECT_METRICS* metrics)
        {
            return ((delegate* unmanaged[Stdcall]<IDWriteInlineObject*, DWRITE_INLINE_OBJECT_METRICS*, int>)(lpVtbl[4]))((IDWriteInlineObject*)Unsafe.AsPointer(ref this), metrics);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(5)]
        public HRESULT GetOverhangMetrics(DWRITE_OVERHANG_METRICS* overhangs)
        {
            return ((delegate* unmanaged[Stdcall]<IDWriteInlineObject*, DWRITE_OVERHANG_METRICS*, int>)(lpVtbl[5]))((IDWriteInlineObject*)Unsafe.AsPointer(ref this), overhangs);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(6)]
        public HRESULT GetBreakConditions(DWRITE_BREAK_CONDITION* breakConditionBefore, DWRITE_BREAK_CONDITION* breakConditionAfter)
        {
            return ((delegate* unmanaged[Stdcall]<IDWriteInlineObject*, DWRITE_BREAK_CONDITION*, DWRITE_BREAK_CONDITION*, int>)(lpVtbl[6]))((IDWriteInlineObject*)Unsafe.AsPointer(ref this), breakConditionBefore, breakConditionAfter);
        }

        public interface Interface : IUnknown.Interface
        {
            [VtblIndex(3)]
            HRESULT Draw(void* clientDrawingContext, IDWriteTextRenderer* renderer, float originX, float originY, BOOL isSideways, BOOL isRightToLeft, IUnknown* clientDrawingEffect);

            [VtblIndex(4)]
            HRESULT GetMetrics(DWRITE_INLINE_OBJECT_METRICS* metrics);

            [VtblIndex(5)]
            HRESULT GetOverhangMetrics(DWRITE_OVERHANG_METRICS* overhangs);

            [VtblIndex(6)]
            HRESULT GetBreakConditions(DWRITE_BREAK_CONDITION* breakConditionBefore, DWRITE_BREAK_CONDITION* breakConditionAfter);
        }
    }
}
