// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/oaidl.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace TerraFX.Interop.Windows
{
    [Guid("00020401-0000-0000-C000-000000000046")]
    [NativeTypeName("struct ITypeInfo : IUnknown")]
    [NativeInheritance("IUnknown")]
    internal unsafe partial struct ITypeInfo : ITypeInfo.Interface
    {
        public void** lpVtbl;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(0)]
        public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
        {
            return ((delegate* unmanaged[Stdcall]<ITypeInfo*, Guid*, void**, int>)(lpVtbl[0]))((ITypeInfo*)Unsafe.AsPointer(ref this), riid, ppvObject);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(1)]
        [return: NativeTypeName("ULONG")]
        public uint AddRef()
        {
            return ((delegate* unmanaged[Stdcall]<ITypeInfo*, uint>)(lpVtbl[1]))((ITypeInfo*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(2)]
        [return: NativeTypeName("ULONG")]
        public uint Release()
        {
            return ((delegate* unmanaged[Stdcall]<ITypeInfo*, uint>)(lpVtbl[2]))((ITypeInfo*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(3)]
        public HRESULT GetTypeAttr(TYPEATTR** ppTypeAttr)
        {
            return ((delegate* unmanaged[Stdcall]<ITypeInfo*, TYPEATTR**, int>)(lpVtbl[3]))((ITypeInfo*)Unsafe.AsPointer(ref this), ppTypeAttr);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(4)]
        public HRESULT GetTypeComp(ITypeComp** ppTComp)
        {
            return ((delegate* unmanaged[Stdcall]<ITypeInfo*, ITypeComp**, int>)(lpVtbl[4]))((ITypeInfo*)Unsafe.AsPointer(ref this), ppTComp);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(5)]
        public HRESULT GetFuncDesc(uint index, FUNCDESC** ppFuncDesc)
        {
            return ((delegate* unmanaged[Stdcall]<ITypeInfo*, uint, FUNCDESC**, int>)(lpVtbl[5]))((ITypeInfo*)Unsafe.AsPointer(ref this), index, ppFuncDesc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(6)]
        public HRESULT GetVarDesc(uint index, VARDESC** ppVarDesc)
        {
            return ((delegate* unmanaged[Stdcall]<ITypeInfo*, uint, VARDESC**, int>)(lpVtbl[6]))((ITypeInfo*)Unsafe.AsPointer(ref this), index, ppVarDesc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(7)]
        public HRESULT GetNames([NativeTypeName("MEMBERID")] int memid, [NativeTypeName("BSTR *")] ushort** rgBstrNames, uint cMaxNames, uint* pcNames)
        {
            return ((delegate* unmanaged[Stdcall]<ITypeInfo*, int, ushort**, uint, uint*, int>)(lpVtbl[7]))((ITypeInfo*)Unsafe.AsPointer(ref this), memid, rgBstrNames, cMaxNames, pcNames);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(8)]
        public HRESULT GetRefTypeOfImplType(uint index, [NativeTypeName("HREFTYPE *")] uint* pRefType)
        {
            return ((delegate* unmanaged[Stdcall]<ITypeInfo*, uint, uint*, int>)(lpVtbl[8]))((ITypeInfo*)Unsafe.AsPointer(ref this), index, pRefType);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(9)]
        public HRESULT GetImplTypeFlags(uint index, int* pImplTypeFlags)
        {
            return ((delegate* unmanaged[Stdcall]<ITypeInfo*, uint, int*, int>)(lpVtbl[9]))((ITypeInfo*)Unsafe.AsPointer(ref this), index, pImplTypeFlags);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(10)]
        public HRESULT GetIDsOfNames([NativeTypeName("LPOLESTR *")] ushort** rgszNames, uint cNames, [NativeTypeName("MEMBERID *")] int* pMemId)
        {
            return ((delegate* unmanaged[Stdcall]<ITypeInfo*, ushort**, uint, int*, int>)(lpVtbl[10]))((ITypeInfo*)Unsafe.AsPointer(ref this), rgszNames, cNames, pMemId);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(11)]
        public HRESULT Invoke([NativeTypeName("PVOID")] void* pvInstance, [NativeTypeName("MEMBERID")] int memid, [NativeTypeName("WORD")] ushort wFlags, DISPPARAMS* pDispParams, VARIANT* pVarResult, EXCEPINFO* pExcepInfo, uint* puArgErr)
        {
            return ((delegate* unmanaged[Stdcall]<ITypeInfo*, void*, int, ushort, DISPPARAMS*, VARIANT*, EXCEPINFO*, uint*, int>)(lpVtbl[11]))((ITypeInfo*)Unsafe.AsPointer(ref this), pvInstance, memid, wFlags, pDispParams, pVarResult, pExcepInfo, puArgErr);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(12)]
        public HRESULT GetDocumentation([NativeTypeName("MEMBERID")] int memid, [NativeTypeName("BSTR *")] ushort** pBstrName, [NativeTypeName("BSTR *")] ushort** pBstrDocString, [NativeTypeName("DWORD *")] uint* pdwHelpContext, [NativeTypeName("BSTR *")] ushort** pBstrHelpFile)
        {
            return ((delegate* unmanaged[Stdcall]<ITypeInfo*, int, ushort**, ushort**, uint*, ushort**, int>)(lpVtbl[12]))((ITypeInfo*)Unsafe.AsPointer(ref this), memid, pBstrName, pBstrDocString, pdwHelpContext, pBstrHelpFile);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(13)]
        public HRESULT GetDllEntry([NativeTypeName("MEMBERID")] int memid, INVOKEKIND invKind, [NativeTypeName("BSTR *")] ushort** pBstrDllName, [NativeTypeName("BSTR *")] ushort** pBstrName, [NativeTypeName("WORD *")] ushort* pwOrdinal)
        {
            return ((delegate* unmanaged[Stdcall]<ITypeInfo*, int, INVOKEKIND, ushort**, ushort**, ushort*, int>)(lpVtbl[13]))((ITypeInfo*)Unsafe.AsPointer(ref this), memid, invKind, pBstrDllName, pBstrName, pwOrdinal);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(14)]
        public HRESULT GetRefTypeInfo([NativeTypeName("HREFTYPE")] uint hRefType, ITypeInfo** ppTInfo)
        {
            return ((delegate* unmanaged[Stdcall]<ITypeInfo*, uint, ITypeInfo**, int>)(lpVtbl[14]))((ITypeInfo*)Unsafe.AsPointer(ref this), hRefType, ppTInfo);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(15)]
        public HRESULT AddressOfMember([NativeTypeName("MEMBERID")] int memid, INVOKEKIND invKind, [NativeTypeName("PVOID *")] void** ppv)
        {
            return ((delegate* unmanaged[Stdcall]<ITypeInfo*, int, INVOKEKIND, void**, int>)(lpVtbl[15]))((ITypeInfo*)Unsafe.AsPointer(ref this), memid, invKind, ppv);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(16)]
        public HRESULT CreateInstance(IUnknown* pUnkOuter, [NativeTypeName("const IID &")] Guid* riid, [NativeTypeName("PVOID *")] void** ppvObj)
        {
            return ((delegate* unmanaged[Stdcall]<ITypeInfo*, IUnknown*, Guid*, void**, int>)(lpVtbl[16]))((ITypeInfo*)Unsafe.AsPointer(ref this), pUnkOuter, riid, ppvObj);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(17)]
        public HRESULT GetMops([NativeTypeName("MEMBERID")] int memid, [NativeTypeName("BSTR *")] ushort** pBstrMops)
        {
            return ((delegate* unmanaged[Stdcall]<ITypeInfo*, int, ushort**, int>)(lpVtbl[17]))((ITypeInfo*)Unsafe.AsPointer(ref this), memid, pBstrMops);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(18)]
        public HRESULT GetContainingTypeLib(ITypeLib** ppTLib, uint* pIndex)
        {
            return ((delegate* unmanaged[Stdcall]<ITypeInfo*, ITypeLib**, uint*, int>)(lpVtbl[18]))((ITypeInfo*)Unsafe.AsPointer(ref this), ppTLib, pIndex);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(19)]
        public void ReleaseTypeAttr(TYPEATTR* pTypeAttr)
        {
            ((delegate* unmanaged[Stdcall]<ITypeInfo*, TYPEATTR*, void>)(lpVtbl[19]))((ITypeInfo*)Unsafe.AsPointer(ref this), pTypeAttr);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(20)]
        public void ReleaseFuncDesc(FUNCDESC* pFuncDesc)
        {
            ((delegate* unmanaged[Stdcall]<ITypeInfo*, FUNCDESC*, void>)(lpVtbl[20]))((ITypeInfo*)Unsafe.AsPointer(ref this), pFuncDesc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(21)]
        public void ReleaseVarDesc(VARDESC* pVarDesc)
        {
            ((delegate* unmanaged[Stdcall]<ITypeInfo*, VARDESC*, void>)(lpVtbl[21]))((ITypeInfo*)Unsafe.AsPointer(ref this), pVarDesc);
        }

        public interface Interface : IUnknown.Interface
        {
            [VtblIndex(3)]
            HRESULT GetTypeAttr(TYPEATTR** ppTypeAttr);

            [VtblIndex(4)]
            HRESULT GetTypeComp(ITypeComp** ppTComp);

            [VtblIndex(5)]
            HRESULT GetFuncDesc(uint index, FUNCDESC** ppFuncDesc);

            [VtblIndex(6)]
            HRESULT GetVarDesc(uint index, VARDESC** ppVarDesc);

            [VtblIndex(7)]
            HRESULT GetNames([NativeTypeName("MEMBERID")] int memid, [NativeTypeName("BSTR *")] ushort** rgBstrNames, uint cMaxNames, uint* pcNames);

            [VtblIndex(8)]
            HRESULT GetRefTypeOfImplType(uint index, [NativeTypeName("HREFTYPE *")] uint* pRefType);

            [VtblIndex(9)]
            HRESULT GetImplTypeFlags(uint index, int* pImplTypeFlags);

            [VtblIndex(10)]
            HRESULT GetIDsOfNames([NativeTypeName("LPOLESTR *")] ushort** rgszNames, uint cNames, [NativeTypeName("MEMBERID *")] int* pMemId);

            [VtblIndex(11)]
            HRESULT Invoke([NativeTypeName("PVOID")] void* pvInstance, [NativeTypeName("MEMBERID")] int memid, [NativeTypeName("WORD")] ushort wFlags, DISPPARAMS* pDispParams, VARIANT* pVarResult, EXCEPINFO* pExcepInfo, uint* puArgErr);

            [VtblIndex(12)]
            HRESULT GetDocumentation([NativeTypeName("MEMBERID")] int memid, [NativeTypeName("BSTR *")] ushort** pBstrName, [NativeTypeName("BSTR *")] ushort** pBstrDocString, [NativeTypeName("DWORD *")] uint* pdwHelpContext, [NativeTypeName("BSTR *")] ushort** pBstrHelpFile);

            [VtblIndex(13)]
            HRESULT GetDllEntry([NativeTypeName("MEMBERID")] int memid, INVOKEKIND invKind, [NativeTypeName("BSTR *")] ushort** pBstrDllName, [NativeTypeName("BSTR *")] ushort** pBstrName, [NativeTypeName("WORD *")] ushort* pwOrdinal);

            [VtblIndex(14)]
            HRESULT GetRefTypeInfo([NativeTypeName("HREFTYPE")] uint hRefType, ITypeInfo** ppTInfo);

            [VtblIndex(15)]
            HRESULT AddressOfMember([NativeTypeName("MEMBERID")] int memid, INVOKEKIND invKind, [NativeTypeName("PVOID *")] void** ppv);

            [VtblIndex(16)]
            HRESULT CreateInstance(IUnknown* pUnkOuter, [NativeTypeName("const IID &")] Guid* riid, [NativeTypeName("PVOID *")] void** ppvObj);

            [VtblIndex(17)]
            HRESULT GetMops([NativeTypeName("MEMBERID")] int memid, [NativeTypeName("BSTR *")] ushort** pBstrMops);

            [VtblIndex(18)]
            HRESULT GetContainingTypeLib(ITypeLib** ppTLib, uint* pIndex);

            [VtblIndex(19)]
            void ReleaseTypeAttr(TYPEATTR* pTypeAttr);

            [VtblIndex(20)]
            void ReleaseFuncDesc(FUNCDESC* pFuncDesc);

            [VtblIndex(21)]
            void ReleaseVarDesc(VARDESC* pVarDesc);
        }
    }
}
