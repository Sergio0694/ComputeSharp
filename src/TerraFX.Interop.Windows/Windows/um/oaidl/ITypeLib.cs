// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/oaidl.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace TerraFX.Interop.Windows
{
    [Guid("00020402-0000-0000-C000-000000000046")]
    [NativeTypeName("struct ITypeLib : IUnknown")]
    [NativeInheritance("IUnknown")]
    internal unsafe partial struct ITypeLib : ITypeLib.Interface
    {
        public void** lpVtbl;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(0)]
        public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
        {
            return ((delegate* unmanaged[Stdcall]<ITypeLib*, Guid*, void**, int>)(lpVtbl[0]))((ITypeLib*)Unsafe.AsPointer(ref this), riid, ppvObject);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(1)]
        [return: NativeTypeName("ULONG")]
        public uint AddRef()
        {
            return ((delegate* unmanaged[Stdcall]<ITypeLib*, uint>)(lpVtbl[1]))((ITypeLib*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(2)]
        [return: NativeTypeName("ULONG")]
        public uint Release()
        {
            return ((delegate* unmanaged[Stdcall]<ITypeLib*, uint>)(lpVtbl[2]))((ITypeLib*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(3)]
        public uint GetTypeInfoCount()
        {
            return ((delegate* unmanaged[Stdcall]<ITypeLib*, uint>)(lpVtbl[3]))((ITypeLib*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(4)]
        public HRESULT GetTypeInfo(uint index, ITypeInfo** ppTInfo)
        {
            return ((delegate* unmanaged[Stdcall]<ITypeLib*, uint, ITypeInfo**, int>)(lpVtbl[4]))((ITypeLib*)Unsafe.AsPointer(ref this), index, ppTInfo);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(5)]
        public HRESULT GetTypeInfoType(uint index, TYPEKIND* pTKind)
        {
            return ((delegate* unmanaged[Stdcall]<ITypeLib*, uint, TYPEKIND*, int>)(lpVtbl[5]))((ITypeLib*)Unsafe.AsPointer(ref this), index, pTKind);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(6)]
        public HRESULT GetTypeInfoOfGuid([NativeTypeName("const GUID &")] Guid* guid, ITypeInfo** ppTinfo)
        {
            return ((delegate* unmanaged[Stdcall]<ITypeLib*, Guid*, ITypeInfo**, int>)(lpVtbl[6]))((ITypeLib*)Unsafe.AsPointer(ref this), guid, ppTinfo);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(7)]
        public HRESULT GetLibAttr(TLIBATTR** ppTLibAttr)
        {
            return ((delegate* unmanaged[Stdcall]<ITypeLib*, TLIBATTR**, int>)(lpVtbl[7]))((ITypeLib*)Unsafe.AsPointer(ref this), ppTLibAttr);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(8)]
        public HRESULT GetTypeComp(ITypeComp** ppTComp)
        {
            return ((delegate* unmanaged[Stdcall]<ITypeLib*, ITypeComp**, int>)(lpVtbl[8]))((ITypeLib*)Unsafe.AsPointer(ref this), ppTComp);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(9)]
        public HRESULT GetDocumentation(int index, [NativeTypeName("BSTR *")] ushort** pBstrName, [NativeTypeName("BSTR *")] ushort** pBstrDocString, [NativeTypeName("DWORD *")] uint* pdwHelpContext, [NativeTypeName("BSTR *")] ushort** pBstrHelpFile)
        {
            return ((delegate* unmanaged[Stdcall]<ITypeLib*, int, ushort**, ushort**, uint*, ushort**, int>)(lpVtbl[9]))((ITypeLib*)Unsafe.AsPointer(ref this), index, pBstrName, pBstrDocString, pdwHelpContext, pBstrHelpFile);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(10)]
        public HRESULT IsName([NativeTypeName("LPOLESTR")] ushort* szNameBuf, [NativeTypeName("ULONG")] uint lHashVal, BOOL* pfName)
        {
            return ((delegate* unmanaged[Stdcall]<ITypeLib*, ushort*, uint, BOOL*, int>)(lpVtbl[10]))((ITypeLib*)Unsafe.AsPointer(ref this), szNameBuf, lHashVal, pfName);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(11)]
        public HRESULT FindName([NativeTypeName("LPOLESTR")] ushort* szNameBuf, [NativeTypeName("ULONG")] uint lHashVal, ITypeInfo** ppTInfo, [NativeTypeName("MEMBERID *")] int* rgMemId, ushort* pcFound)
        {
            return ((delegate* unmanaged[Stdcall]<ITypeLib*, ushort*, uint, ITypeInfo**, int*, ushort*, int>)(lpVtbl[11]))((ITypeLib*)Unsafe.AsPointer(ref this), szNameBuf, lHashVal, ppTInfo, rgMemId, pcFound);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(12)]
        public void ReleaseTLibAttr(TLIBATTR* pTLibAttr)
        {
            ((delegate* unmanaged[Stdcall]<ITypeLib*, TLIBATTR*, void>)(lpVtbl[12]))((ITypeLib*)Unsafe.AsPointer(ref this), pTLibAttr);
        }

        public interface Interface : IUnknown.Interface
        {
            [VtblIndex(3)]
            uint GetTypeInfoCount();

            [VtblIndex(4)]
            HRESULT GetTypeInfo(uint index, ITypeInfo** ppTInfo);

            [VtblIndex(5)]
            HRESULT GetTypeInfoType(uint index, TYPEKIND* pTKind);

            [VtblIndex(6)]
            HRESULT GetTypeInfoOfGuid([NativeTypeName("const GUID &")] Guid* guid, ITypeInfo** ppTinfo);

            [VtblIndex(7)]
            HRESULT GetLibAttr(TLIBATTR** ppTLibAttr);

            [VtblIndex(8)]
            HRESULT GetTypeComp(ITypeComp** ppTComp);

            [VtblIndex(9)]
            HRESULT GetDocumentation(int index, [NativeTypeName("BSTR *")] ushort** pBstrName, [NativeTypeName("BSTR *")] ushort** pBstrDocString, [NativeTypeName("DWORD *")] uint* pdwHelpContext, [NativeTypeName("BSTR *")] ushort** pBstrHelpFile);

            [VtblIndex(10)]
            HRESULT IsName([NativeTypeName("LPOLESTR")] ushort* szNameBuf, [NativeTypeName("ULONG")] uint lHashVal, BOOL* pfName);

            [VtblIndex(11)]
            HRESULT FindName([NativeTypeName("LPOLESTR")] ushort* szNameBuf, [NativeTypeName("ULONG")] uint lHashVal, ITypeInfo** ppTInfo, [NativeTypeName("MEMBERID *")] int* rgMemId, ushort* pcFound);

            [VtblIndex(12)]
            void ReleaseTLibAttr(TLIBATTR* pTLibAttr);
        }
    }
}
