// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/dxcapi.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved. Licensed under the University of Illinois Open Source License.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TerraFX.Interop.Windows;

namespace TerraFX.Interop.DirectX
{
    [Guid("E5204DC7-D18C-4C3C-BDFB-851673980FE7")]
    [NativeTypeName("struct IDxcLibrary : IUnknown")]
    [NativeInheritance("IUnknown")]
    internal unsafe partial struct IDxcLibrary
    {
        public void** lpVtbl;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(0)]
        public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
        {
            return ((delegate* unmanaged[Stdcall]<IDxcLibrary*, Guid*, void**, int>)(lpVtbl[0]))((IDxcLibrary*)Unsafe.AsPointer(ref this), riid, ppvObject);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(1)]
        [return: NativeTypeName("ULONG")]
        public uint AddRef()
        {
            return ((delegate* unmanaged[Stdcall]<IDxcLibrary*, uint>)(lpVtbl[1]))((IDxcLibrary*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(2)]
        [return: NativeTypeName("ULONG")]
        public uint Release()
        {
            return ((delegate* unmanaged[Stdcall]<IDxcLibrary*, uint>)(lpVtbl[2]))((IDxcLibrary*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(4)]
        public HRESULT CreateBlobFromBlob(IDxcBlob* pBlob, [NativeTypeName("UINT32")] uint offset, [NativeTypeName("UINT32")] uint length, IDxcBlob** ppResult)
        {
            return ((delegate* unmanaged[Stdcall]<IDxcLibrary*, IDxcBlob*, uint, uint, IDxcBlob**, int>)(lpVtbl[4]))((IDxcLibrary*)Unsafe.AsPointer(ref this), pBlob, offset, length, ppResult);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(5)]
        public HRESULT CreateBlobFromFile([NativeTypeName("LPCWSTR")] ushort* pFileName, [NativeTypeName("UINT32 *")] uint* codePage, IDxcBlobEncoding** pBlobEncoding)
        {
            return ((delegate* unmanaged[Stdcall]<IDxcLibrary*, ushort*, uint*, IDxcBlobEncoding**, int>)(lpVtbl[5]))((IDxcLibrary*)Unsafe.AsPointer(ref this), pFileName, codePage, pBlobEncoding);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(6)]
        public HRESULT CreateBlobWithEncodingFromPinned([NativeTypeName("LPCVOID")] void* pText, [NativeTypeName("UINT32")] uint size, [NativeTypeName("UINT32")] uint codePage, IDxcBlobEncoding** pBlobEncoding)
        {
            return ((delegate* unmanaged[Stdcall]<IDxcLibrary*, void*, uint, uint, IDxcBlobEncoding**, int>)(lpVtbl[6]))((IDxcLibrary*)Unsafe.AsPointer(ref this), pText, size, codePage, pBlobEncoding);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(7)]
        public HRESULT CreateBlobWithEncodingOnHeapCopy([NativeTypeName("LPCVOID")] void* pText, [NativeTypeName("UINT32")] uint size, [NativeTypeName("UINT32")] uint codePage, IDxcBlobEncoding** pBlobEncoding)
        {
            return ((delegate* unmanaged[Stdcall]<IDxcLibrary*, void*, uint, uint, IDxcBlobEncoding**, int>)(lpVtbl[7]))((IDxcLibrary*)Unsafe.AsPointer(ref this), pText, size, codePage, pBlobEncoding);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(9)]
        public HRESULT CreateIncludeHandler(IDxcIncludeHandler** ppResult)
        {
            return ((delegate* unmanaged[Stdcall]<IDxcLibrary*, IDxcIncludeHandler**, int>)(lpVtbl[9]))((IDxcLibrary*)Unsafe.AsPointer(ref this), ppResult);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(10)]
        public HRESULT CreateStreamFromBlobReadOnly(IDxcBlob* pBlob, IStream** ppStream)
        {
            return ((delegate* unmanaged[Stdcall]<IDxcLibrary*, IDxcBlob*, IStream**, int>)(lpVtbl[10]))((IDxcLibrary*)Unsafe.AsPointer(ref this), pBlob, ppStream);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(11)]
        public HRESULT GetBlobAsUtf8(IDxcBlob* pBlob, IDxcBlobEncoding** pBlobEncoding)
        {
            return ((delegate* unmanaged[Stdcall]<IDxcLibrary*, IDxcBlob*, IDxcBlobEncoding**, int>)(lpVtbl[11]))((IDxcLibrary*)Unsafe.AsPointer(ref this), pBlob, pBlobEncoding);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(12)]
        public HRESULT GetBlobAsUtf16(IDxcBlob* pBlob, IDxcBlobEncoding** pBlobEncoding)
        {
            return ((delegate* unmanaged[Stdcall]<IDxcLibrary*, IDxcBlob*, IDxcBlobEncoding**, int>)(lpVtbl[12]))((IDxcLibrary*)Unsafe.AsPointer(ref this), pBlob, pBlobEncoding);
        }
    }
}
