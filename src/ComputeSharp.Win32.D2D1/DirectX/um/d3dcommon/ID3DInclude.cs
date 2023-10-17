// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from d3dcommon.h in Microsoft.Direct3D.D3D12 v1.600.10
// Original source is Copyright © Microsoft. Licensed under the MIT license

using System.Runtime.CompilerServices;
using TerraFX.Interop.Windows;

#pragma warning disable CS0649

namespace ComputeSharp.Win32;

internal unsafe partial struct ID3DInclude
{
    public void** lpVtbl;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(0)]
    public HRESULT Open(D3D_INCLUDE_TYPE IncludeType, [NativeTypeName("LPCSTR")] sbyte* pFileName, [NativeTypeName("LPCVOID")] void* pParentData, [NativeTypeName("LPCVOID *")] void** ppData, uint* pBytes)
    {
        return ((delegate* unmanaged[Stdcall]<ID3DInclude*, D3D_INCLUDE_TYPE, sbyte*, void*, void**, uint*, int>)(lpVtbl[0]))((ID3DInclude*)Unsafe.AsPointer(ref this), IncludeType, pFileName, pParentData, ppData, pBytes);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(1)]
    public HRESULT Close([NativeTypeName("LPCVOID")] void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<ID3DInclude*, void*, int>)(lpVtbl[1]))((ID3DInclude*)Unsafe.AsPointer(ref this), pData);
    }
}