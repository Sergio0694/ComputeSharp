// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/oaidl.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace TerraFX.Interop.Windows
{
    [Guid("3127CA40-446E-11CE-8135-00AA004BB851")]
    [NativeTypeName("struct IErrorLog : IUnknown")]
    [NativeInheritance("IUnknown")]
    internal unsafe partial struct IErrorLog : IErrorLog.Interface
    {
        public void** lpVtbl;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(0)]
        public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
        {
            return ((delegate* unmanaged[Stdcall]<IErrorLog*, Guid*, void**, int>)(lpVtbl[0]))((IErrorLog*)Unsafe.AsPointer(ref this), riid, ppvObject);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(1)]
        [return: NativeTypeName("ULONG")]
        public uint AddRef()
        {
            return ((delegate* unmanaged[Stdcall]<IErrorLog*, uint>)(lpVtbl[1]))((IErrorLog*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(2)]
        [return: NativeTypeName("ULONG")]
        public uint Release()
        {
            return ((delegate* unmanaged[Stdcall]<IErrorLog*, uint>)(lpVtbl[2]))((IErrorLog*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(3)]
        public HRESULT AddError([NativeTypeName("LPCOLESTR")] ushort* pszPropName, EXCEPINFO* pExcepInfo)
        {
            return ((delegate* unmanaged[Stdcall]<IErrorLog*, ushort*, EXCEPINFO*, int>)(lpVtbl[3]))((IErrorLog*)Unsafe.AsPointer(ref this), pszPropName, pExcepInfo);
        }

        public interface Interface : IUnknown.Interface
        {
            [VtblIndex(3)]
            HRESULT AddError([NativeTypeName("LPCOLESTR")] ushort* pszPropName, EXCEPINFO* pExcepInfo);
        }
    }
}
