// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d2d1effectauthor.h in the Windows SDK for Windows 10.0.22621.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TerraFX.Interop.Windows;

#pragma warning disable CS0649

namespace TerraFX.Interop.DirectX;

[Guid("5598B14B-9FD7-48B7-9BDB-8F0964EB38BC")]
[NativeTypeName("struct ID2D1ComputeInfo : ID2D1RenderInfo")]
[NativeInheritance("ID2D1RenderInfo")]
internal unsafe partial struct ID2D1ComputeInfo
{
    public void** lpVtbl;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(0)]
    public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<ID2D1ComputeInfo*, Guid*, void**, int>)(lpVtbl[0]))((ID2D1ComputeInfo*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(1)]
    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<ID2D1ComputeInfo*, uint>)(lpVtbl[1]))((ID2D1ComputeInfo*)Unsafe.AsPointer(ref this));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(2)]
    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<ID2D1ComputeInfo*, uint>)(lpVtbl[2]))((ID2D1ComputeInfo*)Unsafe.AsPointer(ref this));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(3)]
    public HRESULT SetInputDescription([NativeTypeName("UINT32")] uint inputIndex, D2D1_INPUT_DESCRIPTION inputDescription)
    {
        return ((delegate* unmanaged[Stdcall]<ID2D1ComputeInfo*, uint, D2D1_INPUT_DESCRIPTION, int>)(lpVtbl[3]))((ID2D1ComputeInfo*)Unsafe.AsPointer(ref this), inputIndex, inputDescription);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(4)]
    public HRESULT SetOutputBuffer(D2D1_BUFFER_PRECISION bufferPrecision, D2D1_CHANNEL_DEPTH channelDepth)
    {
        return ((delegate* unmanaged[Stdcall]<ID2D1ComputeInfo*, D2D1_BUFFER_PRECISION, D2D1_CHANNEL_DEPTH, int>)(lpVtbl[4]))((ID2D1ComputeInfo*)Unsafe.AsPointer(ref this), bufferPrecision, channelDepth);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(7)]
    public HRESULT SetComputeShaderConstantBuffer([NativeTypeName("const BYTE *")] byte* buffer, [NativeTypeName("UINT32")] uint bufferCount)
    {
        return ((delegate* unmanaged[Stdcall]<ID2D1ComputeInfo*, byte*, uint, int>)(lpVtbl[7]))((ID2D1ComputeInfo*)Unsafe.AsPointer(ref this), buffer, bufferCount);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(8)]
    public HRESULT SetComputeShader([NativeTypeName("const GUID &")] Guid* shaderId)
    {
        return ((delegate* unmanaged[Stdcall]<ID2D1ComputeInfo*, Guid*, int>)(lpVtbl[8]))((ID2D1ComputeInfo*)Unsafe.AsPointer(ref this), shaderId);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(9)]
    public HRESULT SetResourceTexture([NativeTypeName("UINT32")] uint textureIndex, ID2D1ResourceTexture* resourceTexture)
    {
        return ((delegate* unmanaged[Stdcall]<ID2D1ComputeInfo*, uint, ID2D1ResourceTexture*, int>)(lpVtbl[9]))((ID2D1ComputeInfo*)Unsafe.AsPointer(ref this), textureIndex, resourceTexture);
    }
}