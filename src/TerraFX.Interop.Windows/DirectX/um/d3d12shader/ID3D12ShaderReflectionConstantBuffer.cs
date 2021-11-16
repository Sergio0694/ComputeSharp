// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d3d12shader.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TerraFX.Interop.Windows;

namespace TerraFX.Interop.DirectX
{
    [Guid("C59598B4-48B3-4869-B9B1-B1618B14A8B7")]
    public unsafe partial struct ID3D12ShaderReflectionConstantBuffer : ID3D12ShaderReflectionConstantBuffer.Interface
    {
        public void** lpVtbl;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(0)]
        public HRESULT GetDesc(D3D12_SHADER_BUFFER_DESC* pDesc)
        {
            return ((delegate* unmanaged<ID3D12ShaderReflectionConstantBuffer*, D3D12_SHADER_BUFFER_DESC*, int>)(lpVtbl[0]))((ID3D12ShaderReflectionConstantBuffer*)Unsafe.AsPointer(ref this), pDesc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(1)]
        public ID3D12ShaderReflectionVariable* GetVariableByIndex(uint Index)
        {
            return ((delegate* unmanaged<ID3D12ShaderReflectionConstantBuffer*, uint, ID3D12ShaderReflectionVariable*>)(lpVtbl[1]))((ID3D12ShaderReflectionConstantBuffer*)Unsafe.AsPointer(ref this), Index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(2)]
        public ID3D12ShaderReflectionVariable* GetVariableByName([NativeTypeName("LPCSTR")] sbyte* Name)
        {
            return ((delegate* unmanaged<ID3D12ShaderReflectionConstantBuffer*, sbyte*, ID3D12ShaderReflectionVariable*>)(lpVtbl[2]))((ID3D12ShaderReflectionConstantBuffer*)Unsafe.AsPointer(ref this), Name);
        }

        public interface Interface
        {
            [VtblIndex(0)]
            HRESULT GetDesc(D3D12_SHADER_BUFFER_DESC* pDesc);

            [VtblIndex(1)]
            ID3D12ShaderReflectionVariable* GetVariableByIndex(uint Index);

            [VtblIndex(2)]
            ID3D12ShaderReflectionVariable* GetVariableByName([NativeTypeName("LPCSTR")] sbyte* Name);
        }

        public partial struct Vtbl
        {
            [NativeTypeName("HRESULT (D3D12_SHADER_BUFFER_DESC *) __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<ID3D12ShaderReflectionConstantBuffer*, D3D12_SHADER_BUFFER_DESC*, int> GetDesc;

            [NativeTypeName("ID3D12ShaderReflectionVariable *(UINT) __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<ID3D12ShaderReflectionConstantBuffer*, uint, ID3D12ShaderReflectionVariable*> GetVariableByIndex;

            [NativeTypeName("ID3D12ShaderReflectionVariable *(LPCSTR) __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<ID3D12ShaderReflectionConstantBuffer*, sbyte*, ID3D12ShaderReflectionVariable*> GetVariableByName;
        }
    }
}
