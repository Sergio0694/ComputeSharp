// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/oaidl.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace TerraFX.Interop.Windows
{
    [Guid("0000002F-0000-0000-C000-000000000046")]
    [NativeTypeName("struct IRecordInfo : IUnknown")]
    [NativeInheritance("IUnknown")]
    internal unsafe partial struct IRecordInfo
    {
        public void** lpVtbl;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(0)]
        public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
        {
            return ((delegate* unmanaged[Stdcall]<IRecordInfo*, Guid*, void**, int>)(lpVtbl[0]))((IRecordInfo*)Unsafe.AsPointer(ref this), riid, ppvObject);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(1)]
        [return: NativeTypeName("ULONG")]
        public uint AddRef()
        {
            return ((delegate* unmanaged[Stdcall]<IRecordInfo*, uint>)(lpVtbl[1]))((IRecordInfo*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(2)]
        [return: NativeTypeName("ULONG")]
        public uint Release()
        {
            return ((delegate* unmanaged[Stdcall]<IRecordInfo*, uint>)(lpVtbl[2]))((IRecordInfo*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(3)]
        public HRESULT RecordInit([NativeTypeName("PVOID")] void* pvNew)
        {
            return ((delegate* unmanaged[Stdcall]<IRecordInfo*, void*, int>)(lpVtbl[3]))((IRecordInfo*)Unsafe.AsPointer(ref this), pvNew);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(4)]
        public HRESULT RecordClear([NativeTypeName("PVOID")] void* pvExisting)
        {
            return ((delegate* unmanaged[Stdcall]<IRecordInfo*, void*, int>)(lpVtbl[4]))((IRecordInfo*)Unsafe.AsPointer(ref this), pvExisting);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(5)]
        public HRESULT RecordCopy([NativeTypeName("PVOID")] void* pvExisting, [NativeTypeName("PVOID")] void* pvNew)
        {
            return ((delegate* unmanaged[Stdcall]<IRecordInfo*, void*, void*, int>)(lpVtbl[5]))((IRecordInfo*)Unsafe.AsPointer(ref this), pvExisting, pvNew);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(6)]
        public HRESULT GetGuid(Guid* pguid)
        {
            return ((delegate* unmanaged[Stdcall]<IRecordInfo*, Guid*, int>)(lpVtbl[6]))((IRecordInfo*)Unsafe.AsPointer(ref this), pguid);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(7)]
        public HRESULT GetName([NativeTypeName("BSTR *")] ushort** pbstrName)
        {
            return ((delegate* unmanaged[Stdcall]<IRecordInfo*, ushort**, int>)(lpVtbl[7]))((IRecordInfo*)Unsafe.AsPointer(ref this), pbstrName);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(8)]
        public HRESULT GetSize([NativeTypeName("ULONG *")] uint* pcbSize)
        {
            return ((delegate* unmanaged[Stdcall]<IRecordInfo*, uint*, int>)(lpVtbl[8]))((IRecordInfo*)Unsafe.AsPointer(ref this), pcbSize);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(9)]
        public HRESULT GetTypeInfo(ITypeInfo** ppTypeInfo)
        {
            return ((delegate* unmanaged[Stdcall]<IRecordInfo*, ITypeInfo**, int>)(lpVtbl[9]))((IRecordInfo*)Unsafe.AsPointer(ref this), ppTypeInfo);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(10)]
        public HRESULT GetField([NativeTypeName("PVOID")] void* pvData, [NativeTypeName("LPCOLESTR")] ushort* szFieldName, VARIANT* pvarField)
        {
            return ((delegate* unmanaged[Stdcall]<IRecordInfo*, void*, ushort*, VARIANT*, int>)(lpVtbl[10]))((IRecordInfo*)Unsafe.AsPointer(ref this), pvData, szFieldName, pvarField);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(11)]
        public HRESULT GetFieldNoCopy([NativeTypeName("PVOID")] void* pvData, [NativeTypeName("LPCOLESTR")] ushort* szFieldName, VARIANT* pvarField, [NativeTypeName("PVOID *")] void** ppvDataCArray)
        {
            return ((delegate* unmanaged[Stdcall]<IRecordInfo*, void*, ushort*, VARIANT*, void**, int>)(lpVtbl[11]))((IRecordInfo*)Unsafe.AsPointer(ref this), pvData, szFieldName, pvarField, ppvDataCArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(12)]
        public HRESULT PutField([NativeTypeName("ULONG")] uint wFlags, [NativeTypeName("PVOID")] void* pvData, [NativeTypeName("LPCOLESTR")] ushort* szFieldName, VARIANT* pvarField)
        {
            return ((delegate* unmanaged[Stdcall]<IRecordInfo*, uint, void*, ushort*, VARIANT*, int>)(lpVtbl[12]))((IRecordInfo*)Unsafe.AsPointer(ref this), wFlags, pvData, szFieldName, pvarField);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(13)]
        public HRESULT PutFieldNoCopy([NativeTypeName("ULONG")] uint wFlags, [NativeTypeName("PVOID")] void* pvData, [NativeTypeName("LPCOLESTR")] ushort* szFieldName, VARIANT* pvarField)
        {
            return ((delegate* unmanaged[Stdcall]<IRecordInfo*, uint, void*, ushort*, VARIANT*, int>)(lpVtbl[13]))((IRecordInfo*)Unsafe.AsPointer(ref this), wFlags, pvData, szFieldName, pvarField);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(14)]
        public HRESULT GetFieldNames([NativeTypeName("ULONG *")] uint* pcNames, [NativeTypeName("BSTR *")] ushort** rgBstrNames)
        {
            return ((delegate* unmanaged[Stdcall]<IRecordInfo*, uint*, ushort**, int>)(lpVtbl[14]))((IRecordInfo*)Unsafe.AsPointer(ref this), pcNames, rgBstrNames);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(15)]
        public BOOL IsMatchingType(IRecordInfo* pRecordInfo)
        {
            return ((delegate* unmanaged[Stdcall]<IRecordInfo*, IRecordInfo*, int>)(lpVtbl[15]))((IRecordInfo*)Unsafe.AsPointer(ref this), pRecordInfo);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(16)]
        [return: NativeTypeName("PVOID")]
        public void* RecordCreate()
        {
            return ((delegate* unmanaged[Stdcall]<IRecordInfo*, void*>)(lpVtbl[16]))((IRecordInfo*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(17)]
        public HRESULT RecordCreateCopy([NativeTypeName("PVOID")] void* pvSource, [NativeTypeName("PVOID *")] void** ppvDest)
        {
            return ((delegate* unmanaged[Stdcall]<IRecordInfo*, void*, void**, int>)(lpVtbl[17]))((IRecordInfo*)Unsafe.AsPointer(ref this), pvSource, ppvDest);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(18)]
        public HRESULT RecordDestroy([NativeTypeName("PVOID")] void* pvRecord)
        {
            return ((delegate* unmanaged[Stdcall]<IRecordInfo*, void*, int>)(lpVtbl[18]))((IRecordInfo*)Unsafe.AsPointer(ref this), pvRecord);
        }
    }
}
