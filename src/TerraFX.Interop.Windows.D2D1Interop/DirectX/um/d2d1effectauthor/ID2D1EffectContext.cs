// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d2d1effectauthor.h in the Windows SDK for Windows 10.0.22000.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TerraFX.Interop.Windows;

#pragma warning disable CS0649

namespace TerraFX.Interop.DirectX
{
    [Guid("3D9F916B-27DC-4AD7-B4F1-64945340F563")]
    [NativeTypeName("struct ID2D1EffectContext : IUnknown")]
    [NativeInheritance("IUnknown")]
    internal unsafe partial struct ID2D1EffectContext
    {
        public void** lpVtbl;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(4)]
        public HRESULT CreateEffect([NativeTypeName("const IID &")] Guid* effectId, void** effect)
        {
            return ((delegate* unmanaged[Stdcall]<ID2D1EffectContext*, Guid*, void**, int>)(lpVtbl[4]))((ID2D1EffectContext*)Unsafe.AsPointer(ref this), effectId, effect);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(11)]
        public HRESULT LoadPixelShader([NativeTypeName("const GUID &")] Guid* shaderId, [NativeTypeName("const BYTE *")] byte* shaderBuffer, [NativeTypeName("UINT32")] uint shaderBufferCount)
        {
            return ((delegate* unmanaged[Stdcall]<ID2D1EffectContext*, Guid*, byte*, uint, int>)(lpVtbl[11]))((ID2D1EffectContext*)Unsafe.AsPointer(ref this), shaderId, shaderBuffer, shaderBufferCount);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(14)]
        public int IsShaderLoaded([NativeTypeName("const GUID &")] Guid* shaderId)
        {
            return ((delegate* unmanaged[Stdcall]<ID2D1EffectContext*, Guid*, int>)(lpVtbl[14]))((ID2D1EffectContext*)Unsafe.AsPointer(ref this), shaderId);
        }
    }
}