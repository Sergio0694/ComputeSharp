// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d2d1effectauthor.h in the Windows SDK for Windows 10.0.22000.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TerraFX.Interop.Windows;

#pragma warning disable CS0649

namespace TerraFX.Interop.DirectX;

[Guid("3D9F916B-27DC-4AD7-B4F1-64945340F563")]
[NativeTypeName("struct ID2D1EffectContext : IUnknown")]
[NativeInheritance("IUnknown")]
internal unsafe partial struct ID2D1EffectContext
{
    public void** lpVtbl;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(0)]
    public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IUnknown*, Guid*, void**, int>)(lpVtbl[0]))((IUnknown*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(1)]
    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IUnknown*, uint>)(lpVtbl[1]))((IUnknown*)Unsafe.AsPointer(ref this));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(2)]
    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IUnknown*, uint>)(lpVtbl[2]))((IUnknown*)Unsafe.AsPointer(ref this));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(4)]
    public HRESULT CreateEffect([NativeTypeName("const IID &")] Guid* effectId, void** effect)
    {
        return ((delegate* unmanaged[Stdcall]<ID2D1EffectContext*, Guid*, void**, int>)(lpVtbl[4]))((ID2D1EffectContext*)Unsafe.AsPointer(ref this), effectId, effect);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(5)]
    public HRESULT GetMaximumSupportedFeatureLevel([NativeTypeName("const D3D_FEATURE_LEVEL *")] D3D_FEATURE_LEVEL* featureLevels, [NativeTypeName("UINT32")] uint featureLevelsCount, D3D_FEATURE_LEVEL* maximumSupportedFeatureLevel)
    {
        return ((delegate* unmanaged[Stdcall]<ID2D1EffectContext*, D3D_FEATURE_LEVEL*, uint, D3D_FEATURE_LEVEL*, int>)(lpVtbl[5]))((ID2D1EffectContext*)Unsafe.AsPointer(ref this), featureLevels, featureLevelsCount, maximumSupportedFeatureLevel);
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(15)]
    public HRESULT CreateResourceTexture([NativeTypeName("const GUID *")] Guid* resourceId, [NativeTypeName("const D2D1_RESOURCE_TEXTURE_PROPERTIES *")] D2D1_RESOURCE_TEXTURE_PROPERTIES* resourceTextureProperties, [NativeTypeName("const BYTE *")] byte* data, [NativeTypeName("const UINT32 *")] uint* strides, [NativeTypeName("UINT32")] uint dataSize, ID2D1ResourceTexture** resourceTexture)
    {
        return ((delegate* unmanaged[Stdcall]<ID2D1EffectContext*, Guid*, D2D1_RESOURCE_TEXTURE_PROPERTIES*, byte*, uint*, uint, ID2D1ResourceTexture**, int>)(lpVtbl[15]))((ID2D1EffectContext*)Unsafe.AsPointer(ref this), resourceId, resourceTextureProperties, data, strides, dataSize, resourceTexture);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(22)]
    public HRESULT CheckFeatureSupport(D2D1_FEATURE feature, void* featureSupportData, [NativeTypeName("UINT32")] uint featureSupportDataSize)
    {
        return ((delegate* unmanaged[Stdcall]<ID2D1EffectContext*, D2D1_FEATURE, void*, uint, int>)(lpVtbl[22]))((ID2D1EffectContext*)Unsafe.AsPointer(ref this), feature, featureSupportData, featureSupportDataSize);
    }
}