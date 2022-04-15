// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d2d1effectauthor.h in the Windows SDK for Windows 10.0.22000.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TerraFX.Interop.Windows;
using static TerraFX.Interop.DirectX.D2D1_PIXEL_OPTIONS;

#pragma warning disable CS0649

namespace TerraFX.Interop.DirectX
{
    [Guid("693CE632-7F2F-45DE-93FE-18D88B37AA21")]
    [NativeTypeName("struct ID2D1DrawInfo : ID2D1RenderInfo")]
    [NativeInheritance("ID2D1RenderInfo")]
    internal unsafe partial struct ID2D1DrawInfo
    {
        public void** lpVtbl;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(7)]
        public HRESULT SetPixelShaderConstantBuffer([NativeTypeName("const BYTE *")] byte* buffer, [NativeTypeName("UINT32")] uint bufferCount)
        {
            return ((delegate* unmanaged[Stdcall]<ID2D1DrawInfo*, byte*, uint, int>)(lpVtbl[7]))((ID2D1DrawInfo*)Unsafe.AsPointer(ref this), buffer, bufferCount);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(10)]
        public HRESULT SetPixelShader([NativeTypeName("const GUID &")] Guid* shaderId, D2D1_PIXEL_OPTIONS pixelOptions = D2D1_PIXEL_OPTIONS_NONE)
        {
            return ((delegate* unmanaged[Stdcall]<ID2D1DrawInfo*, Guid*, D2D1_PIXEL_OPTIONS, int>)(lpVtbl[10]))((ID2D1DrawInfo*)Unsafe.AsPointer(ref this), shaderId, pixelOptions);
        }
    }
}