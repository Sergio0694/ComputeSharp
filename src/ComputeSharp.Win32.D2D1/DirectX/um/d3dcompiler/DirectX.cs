// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d3dcompiler.h in the Windows SDK for Windows 10.0.22000.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.InteropServices;

namespace ComputeSharp.Win32;

internal static unsafe partial class DirectX
{
    [DllImport("d3dcompiler_47", ExactSpelling = true)]
    public static extern HRESULT D3DCompile([NativeTypeName("LPCVOID")] void* pSrcData, [NativeTypeName("SIZE_T")] nuint SrcDataSize, [NativeTypeName("LPCSTR")] sbyte* pSourceName, [NativeTypeName("const D3D_SHADER_MACRO *")] D3D_SHADER_MACRO* pDefines, [NativeTypeName("const ID3DInclude *")] void* pInclude, [NativeTypeName("LPCSTR")] sbyte* pEntrypoint, [NativeTypeName("LPCSTR")] sbyte* pTarget, uint Flags1, uint Flags2, ID3DBlob** ppCode, ID3DBlob** ppErrorMsgs);

    [DllImport("d3dcompiler_47", ExactSpelling = true)]
    public static extern HRESULT D3DReflect([NativeTypeName("LPCVOID")] void* pSrcData, [NativeTypeName("SIZE_T")] nuint SrcDataSize, [NativeTypeName("const IID &")] Guid* pInterface, void** ppReflector);

    [DllImport("d3dcompiler_47", ExactSpelling = true)]
    public static extern HRESULT D3DSetBlobPart([NativeTypeName("LPCVOID")] void* pSrcData, [NativeTypeName("SIZE_T")] nuint SrcDataSize, D3D_BLOB_PART Part, uint Flags, [NativeTypeName("LPCVOID")] void* pPart, [NativeTypeName("SIZE_T")] nuint PartSize, ID3DBlob** ppNewShader);

    [DllImport("d3dcompiler_47", ExactSpelling = true)]
    public static extern HRESULT D3DStripShader([NativeTypeName("LPCVOID")] void* pShaderBytecode, [NativeTypeName("SIZE_T")] nuint BytecodeLength, uint uStripFlags, ID3DBlob** ppStrippedBlob);
}