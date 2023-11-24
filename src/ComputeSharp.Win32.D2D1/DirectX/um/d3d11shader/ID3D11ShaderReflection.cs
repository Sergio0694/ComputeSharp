// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d3d11shader.h in the Windows SDK for Windows 10.0.22000.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ComputeSharp.Win32;

[NativeTypeName("struct ID3D11ShaderReflection : IUnknown")]
[NativeInheritance("IUnknown")]
internal unsafe partial struct ID3D11ShaderReflection : IComObject
{
    /// <inheritdoc/>
    static Guid* IComObject.IID
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            ReadOnlySpan<byte> data =
            [
                0xA1, 0x6C, 0x53, 0x8D,
                0xCA, 0x0C,
                0x56, 0x49,
                0xA8,
                0x37,
                0x78,
                0x69,
                0x63,
                0x75,
                0x55,
                0x84
            ];

            return (Guid*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(data));
        }
    }

    public void** lpVtbl;

    /// <inheritdoc cref="IUnknown.QueryInterface" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(0)]
    public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[MemberFunction]<ID3D11ShaderReflection*, Guid*, void**, int>)(lpVtbl[0]))((ID3D11ShaderReflection*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    /// <inheritdoc cref="IUnknown.AddRef" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(1)]
    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[MemberFunction]<ID3D11ShaderReflection*, uint>)(lpVtbl[1]))((ID3D11ShaderReflection*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IUnknown.Release" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(2)]
    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[MemberFunction]<ID3D11ShaderReflection*, uint>)(lpVtbl[2]))((ID3D11ShaderReflection*)Unsafe.AsPointer(ref this));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(3)]
    public HRESULT GetDesc(D3D11_SHADER_DESC* pDesc)
    {
        return ((delegate* unmanaged[MemberFunction]<ID3D11ShaderReflection*, D3D11_SHADER_DESC*, int>)(lpVtbl[3]))((ID3D11ShaderReflection*)Unsafe.AsPointer(ref this), pDesc);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(12)]
    public uint GetMovInstructionCount()
    {
        return ((delegate* unmanaged[MemberFunction]<ID3D11ShaderReflection*, uint>)(lpVtbl[12]))((ID3D11ShaderReflection*)Unsafe.AsPointer(ref this));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(13)]
    public uint GetMovcInstructionCount()
    {
        return ((delegate* unmanaged[MemberFunction]<ID3D11ShaderReflection*, uint>)(lpVtbl[13]))((ID3D11ShaderReflection*)Unsafe.AsPointer(ref this));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(14)]
    public uint GetConversionInstructionCount()
    {
        return ((delegate* unmanaged[MemberFunction]<ID3D11ShaderReflection*, uint>)(lpVtbl[14]))((ID3D11ShaderReflection*)Unsafe.AsPointer(ref this));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(15)]
    public uint GetBitwiseInstructionCount()
    {
        return ((delegate* unmanaged[MemberFunction]<ID3D11ShaderReflection*, uint>)(lpVtbl[15]))((ID3D11ShaderReflection*)Unsafe.AsPointer(ref this));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(16)]
    public D3D_PRIMITIVE GetGSInputPrimitive()
    {
        return ((delegate* unmanaged[MemberFunction]<ID3D11ShaderReflection*, D3D_PRIMITIVE>)(lpVtbl[16]))((ID3D11ShaderReflection*)Unsafe.AsPointer(ref this));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(18)]
    public uint GetNumInterfaceSlots()
    {
        return ((delegate* unmanaged[MemberFunction]<ID3D11ShaderReflection*, uint>)(lpVtbl[18]))((ID3D11ShaderReflection*)Unsafe.AsPointer(ref this));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(19)]
    public HRESULT GetMinFeatureLevel([NativeTypeName("enum D3D_FEATURE_LEVEL *")] D3D_FEATURE_LEVEL* pLevel)
    {
        return ((delegate* unmanaged[MemberFunction]<ID3D11ShaderReflection*, D3D_FEATURE_LEVEL*, int>)(lpVtbl[19]))((ID3D11ShaderReflection*)Unsafe.AsPointer(ref this), pLevel);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(20)]
    public uint GetThreadGroupSize(uint* pSizeX, uint* pSizeY, uint* pSizeZ)
    {
        return ((delegate* unmanaged[MemberFunction]<ID3D11ShaderReflection*, uint*, uint*, uint*, uint>)(lpVtbl[20]))((ID3D11ShaderReflection*)Unsafe.AsPointer(ref this), pSizeX, pSizeY, pSizeZ);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(21)]
    [return: NativeTypeName("UINT64")]
    public ulong GetRequiresFlags()
    {
        return ((delegate* unmanaged[MemberFunction]<ID3D11ShaderReflection*, ulong>)(lpVtbl[21]))((ID3D11ShaderReflection*)Unsafe.AsPointer(ref this));
    }
}