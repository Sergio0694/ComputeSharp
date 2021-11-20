// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/dxcapi.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved. Licensed under the University of Illinois Open Source License.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TerraFX.Interop.Windows;

namespace TerraFX.Interop.DirectX
{
    [Guid("73EFFE2A-70DC-45F8-9690-EFF64C02429D")]
    [NativeTypeName("struct IDxcCompilerArgs : IUnknown")]
    [NativeInheritance("IUnknown")]
    internal unsafe partial struct IDxcCompilerArgs : IDxcCompilerArgs.Interface
    {
        public void** lpVtbl;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(0)]
        public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
        {
            return ((delegate* unmanaged[Stdcall]<IDxcCompilerArgs*, Guid*, void**, int>)(lpVtbl[0]))((IDxcCompilerArgs*)Unsafe.AsPointer(ref this), riid, ppvObject);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(1)]
        [return: NativeTypeName("ULONG")]
        public uint AddRef()
        {
            return ((delegate* unmanaged[Stdcall]<IDxcCompilerArgs*, uint>)(lpVtbl[1]))((IDxcCompilerArgs*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(2)]
        [return: NativeTypeName("ULONG")]
        public uint Release()
        {
            return ((delegate* unmanaged[Stdcall]<IDxcCompilerArgs*, uint>)(lpVtbl[2]))((IDxcCompilerArgs*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(3)]
        [return: NativeTypeName("LPCWSTR *")]
        public ushort** GetArguments()
        {
            return ((delegate* unmanaged[Stdcall]<IDxcCompilerArgs*, ushort**>)(lpVtbl[3]))((IDxcCompilerArgs*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(4)]
        [return: NativeTypeName("UINT32")]
        public uint GetCount()
        {
            return ((delegate* unmanaged[Stdcall]<IDxcCompilerArgs*, uint>)(lpVtbl[4]))((IDxcCompilerArgs*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(5)]
        public HRESULT AddArguments([NativeTypeName("LPCWSTR *")] ushort** pArguments, [NativeTypeName("UINT32")] uint argCount)
        {
            return ((delegate* unmanaged[Stdcall]<IDxcCompilerArgs*, ushort**, uint, int>)(lpVtbl[5]))((IDxcCompilerArgs*)Unsafe.AsPointer(ref this), pArguments, argCount);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(6)]
        public HRESULT AddArgumentsUTF8([NativeTypeName("LPCSTR *")] sbyte** pArguments, [NativeTypeName("UINT32")] uint argCount)
        {
            return ((delegate* unmanaged[Stdcall]<IDxcCompilerArgs*, sbyte**, uint, int>)(lpVtbl[6]))((IDxcCompilerArgs*)Unsafe.AsPointer(ref this), pArguments, argCount);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(7)]
        public HRESULT AddDefines([NativeTypeName("const DxcDefine *")] DxcDefine* pDefines, [NativeTypeName("UINT32")] uint defineCount)
        {
            return ((delegate* unmanaged[Stdcall]<IDxcCompilerArgs*, DxcDefine*, uint, int>)(lpVtbl[7]))((IDxcCompilerArgs*)Unsafe.AsPointer(ref this), pDefines, defineCount);
        }

        public interface Interface : IUnknown.Interface
        {
            [VtblIndex(3)]
            [return: NativeTypeName("LPCWSTR *")]
            ushort** GetArguments();

            [VtblIndex(4)]
            [return: NativeTypeName("UINT32")]
            uint GetCount();

            [VtblIndex(5)]
            HRESULT AddArguments([NativeTypeName("LPCWSTR *")] ushort** pArguments, [NativeTypeName("UINT32")] uint argCount);

            [VtblIndex(6)]
            HRESULT AddArgumentsUTF8([NativeTypeName("LPCSTR *")] sbyte** pArguments, [NativeTypeName("UINT32")] uint argCount);

            [VtblIndex(7)]
            HRESULT AddDefines([NativeTypeName("const DxcDefine *")] DxcDefine* pDefines, [NativeTypeName("UINT32")] uint defineCount);
        }
    }
}
