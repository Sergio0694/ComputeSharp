// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/oaidl.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace TerraFX.Interop.Windows
{
    [Guid("00020403-0000-0000-C000-000000000046")]
    [NativeTypeName("struct ITypeComp : IUnknown")]
    [NativeInheritance("IUnknown")]
    public unsafe partial struct ITypeComp : ITypeComp.Interface
    {
        public void** lpVtbl;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(0)]
        public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
        {
            return ((delegate* unmanaged<ITypeComp*, Guid*, void**, int>)(lpVtbl[0]))((ITypeComp*)Unsafe.AsPointer(ref this), riid, ppvObject);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(1)]
        [return: NativeTypeName("ULONG")]
        public uint AddRef()
        {
            return ((delegate* unmanaged<ITypeComp*, uint>)(lpVtbl[1]))((ITypeComp*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(2)]
        [return: NativeTypeName("ULONG")]
        public uint Release()
        {
            return ((delegate* unmanaged<ITypeComp*, uint>)(lpVtbl[2]))((ITypeComp*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(3)]
        public HRESULT Bind([NativeTypeName("LPOLESTR")] ushort* szName, [NativeTypeName("ULONG")] uint lHashVal, [NativeTypeName("WORD")] ushort wFlags, ITypeInfo** ppTInfo, DESCKIND* pDescKind, BINDPTR* pBindPtr)
        {
            return ((delegate* unmanaged<ITypeComp*, ushort*, uint, ushort, ITypeInfo**, DESCKIND*, BINDPTR*, int>)(lpVtbl[3]))((ITypeComp*)Unsafe.AsPointer(ref this), szName, lHashVal, wFlags, ppTInfo, pDescKind, pBindPtr);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(4)]
        public HRESULT BindType([NativeTypeName("LPOLESTR")] ushort* szName, [NativeTypeName("ULONG")] uint lHashVal, ITypeInfo** ppTInfo, ITypeComp** ppTComp)
        {
            return ((delegate* unmanaged<ITypeComp*, ushort*, uint, ITypeInfo**, ITypeComp**, int>)(lpVtbl[4]))((ITypeComp*)Unsafe.AsPointer(ref this), szName, lHashVal, ppTInfo, ppTComp);
        }

        public interface Interface : IUnknown.Interface
        {
            [VtblIndex(3)]
            HRESULT Bind([NativeTypeName("LPOLESTR")] ushort* szName, [NativeTypeName("ULONG")] uint lHashVal, [NativeTypeName("WORD")] ushort wFlags, ITypeInfo** ppTInfo, DESCKIND* pDescKind, BINDPTR* pBindPtr);

            [VtblIndex(4)]
            HRESULT BindType([NativeTypeName("LPOLESTR")] ushort* szName, [NativeTypeName("ULONG")] uint lHashVal, ITypeInfo** ppTInfo, ITypeComp** ppTComp);
        }

        public partial struct Vtbl
        {
            [NativeTypeName("HRESULT (const IID &, void **) __attribute__((stdcall))")]
            public delegate* unmanaged<ITypeComp*, Guid*, void**, int> QueryInterface;

            [NativeTypeName("ULONG () __attribute__((stdcall))")]
            public delegate* unmanaged<ITypeComp*, uint> AddRef;

            [NativeTypeName("ULONG () __attribute__((stdcall))")]
            public delegate* unmanaged<ITypeComp*, uint> Release;

            [NativeTypeName("HRESULT (LPOLESTR, ULONG, WORD, ITypeInfo **, DESCKIND *, BINDPTR *) __attribute__((stdcall))")]
            public delegate* unmanaged<ITypeComp*, ushort*, uint, ushort, ITypeInfo**, DESCKIND*, BINDPTR*, int> Bind;

            [NativeTypeName("HRESULT (LPOLESTR, ULONG, ITypeInfo **, ITypeComp **) __attribute__((stdcall))")]
            public delegate* unmanaged<ITypeComp*, ushort*, uint, ITypeInfo**, ITypeComp**, int> BindType;
        }
    }
}
