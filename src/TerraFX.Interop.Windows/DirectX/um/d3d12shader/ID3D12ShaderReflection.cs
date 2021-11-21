// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d3d12shader.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TerraFX.Interop.Windows;

namespace TerraFX.Interop.DirectX
{
    [Guid("5A58797D-A72C-478D-8BA2-EFC6B0EFE88E")]
    [NativeTypeName("struct ID3D12ShaderReflection : IUnknown")]
    [NativeInheritance("IUnknown")]
    internal unsafe partial struct ID3D12ShaderReflection
    {
        public void** lpVtbl;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(0)]
        public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12ShaderReflection*, Guid*, void**, int>)(lpVtbl[0]))((ID3D12ShaderReflection*)Unsafe.AsPointer(ref this), riid, ppvObject);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(1)]
        [return: NativeTypeName("ULONG")]
        public uint AddRef()
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12ShaderReflection*, uint>)(lpVtbl[1]))((ID3D12ShaderReflection*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(2)]
        [return: NativeTypeName("ULONG")]
        public uint Release()
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12ShaderReflection*, uint>)(lpVtbl[2]))((ID3D12ShaderReflection*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(3)]
        public HRESULT GetDesc(D3D12_SHADER_DESC* pDesc)
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12ShaderReflection*, D3D12_SHADER_DESC*, int>)(lpVtbl[3]))((ID3D12ShaderReflection*)Unsafe.AsPointer(ref this), pDesc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(12)]
        public uint GetMovInstructionCount()
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12ShaderReflection*, uint>)(lpVtbl[12]))((ID3D12ShaderReflection*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(13)]
        public uint GetMovcInstructionCount()
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12ShaderReflection*, uint>)(lpVtbl[13]))((ID3D12ShaderReflection*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(14)]
        public uint GetConversionInstructionCount()
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12ShaderReflection*, uint>)(lpVtbl[14]))((ID3D12ShaderReflection*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(15)]
        public uint GetBitwiseInstructionCount()
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12ShaderReflection*, uint>)(lpVtbl[15]))((ID3D12ShaderReflection*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(16)]
        public D3D_PRIMITIVE GetGSInputPrimitive()
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12ShaderReflection*, D3D_PRIMITIVE>)(lpVtbl[16]))((ID3D12ShaderReflection*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(17)]
        public BOOL IsSampleFrequencyShader()
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12ShaderReflection*, int>)(lpVtbl[17]))((ID3D12ShaderReflection*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(18)]
        public uint GetNumInterfaceSlots()
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12ShaderReflection*, uint>)(lpVtbl[18]))((ID3D12ShaderReflection*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(19)]
        public HRESULT GetMinFeatureLevel([NativeTypeName("enum D3D_FEATURE_LEVEL *")] D3D_FEATURE_LEVEL* pLevel)
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12ShaderReflection*, D3D_FEATURE_LEVEL*, int>)(lpVtbl[19]))((ID3D12ShaderReflection*)Unsafe.AsPointer(ref this), pLevel);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(20)]
        public uint GetThreadGroupSize(uint* pSizeX, uint* pSizeY, uint* pSizeZ)
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12ShaderReflection*, uint*, uint*, uint*, uint>)(lpVtbl[20]))((ID3D12ShaderReflection*)Unsafe.AsPointer(ref this), pSizeX, pSizeY, pSizeZ);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(21)]
        [return: NativeTypeName("UINT64")]
        public ulong GetRequiresFlags()
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12ShaderReflection*, ulong>)(lpVtbl[21]))((ID3D12ShaderReflection*)Unsafe.AsPointer(ref this));
        }
    }
}
