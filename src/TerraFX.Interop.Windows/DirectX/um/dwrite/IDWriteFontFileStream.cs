// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/dwrite.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TerraFX.Interop.Windows;

namespace TerraFX.Interop.DirectX
{
    [Guid("6D4865FE-0AB8-4D91-8F62-5DD6BE34A3E0")]
    [NativeTypeName("struct IDWriteFontFileStream : IUnknown")]
    [NativeInheritance("IUnknown")]
    public unsafe partial struct IDWriteFontFileStream : IDWriteFontFileStream.Interface
    {
        public void** lpVtbl;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(0)]
        public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
        {
            return ((delegate* unmanaged<IDWriteFontFileStream*, Guid*, void**, int>)(lpVtbl[0]))((IDWriteFontFileStream*)Unsafe.AsPointer(ref this), riid, ppvObject);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(1)]
        [return: NativeTypeName("ULONG")]
        public uint AddRef()
        {
            return ((delegate* unmanaged<IDWriteFontFileStream*, uint>)(lpVtbl[1]))((IDWriteFontFileStream*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(2)]
        [return: NativeTypeName("ULONG")]
        public uint Release()
        {
            return ((delegate* unmanaged<IDWriteFontFileStream*, uint>)(lpVtbl[2]))((IDWriteFontFileStream*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(3)]
        public HRESULT ReadFileFragment([NativeTypeName("const void **")] void** fragmentStart, [NativeTypeName("UINT64")] ulong fileOffset, [NativeTypeName("UINT64")] ulong fragmentSize, void** fragmentContext)
        {
            return ((delegate* unmanaged<IDWriteFontFileStream*, void**, ulong, ulong, void**, int>)(lpVtbl[3]))((IDWriteFontFileStream*)Unsafe.AsPointer(ref this), fragmentStart, fileOffset, fragmentSize, fragmentContext);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(4)]
        public void ReleaseFileFragment(void* fragmentContext)
        {
            ((delegate* unmanaged<IDWriteFontFileStream*, void*, void>)(lpVtbl[4]))((IDWriteFontFileStream*)Unsafe.AsPointer(ref this), fragmentContext);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(5)]
        public HRESULT GetFileSize([NativeTypeName("UINT64 *")] ulong* fileSize)
        {
            return ((delegate* unmanaged<IDWriteFontFileStream*, ulong*, int>)(lpVtbl[5]))((IDWriteFontFileStream*)Unsafe.AsPointer(ref this), fileSize);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(6)]
        public HRESULT GetLastWriteTime([NativeTypeName("UINT64 *")] ulong* lastWriteTime)
        {
            return ((delegate* unmanaged<IDWriteFontFileStream*, ulong*, int>)(lpVtbl[6]))((IDWriteFontFileStream*)Unsafe.AsPointer(ref this), lastWriteTime);
        }

        public interface Interface : IUnknown.Interface
        {
            [VtblIndex(3)]
            HRESULT ReadFileFragment([NativeTypeName("const void **")] void** fragmentStart, [NativeTypeName("UINT64")] ulong fileOffset, [NativeTypeName("UINT64")] ulong fragmentSize, void** fragmentContext);

            [VtblIndex(4)]
            void ReleaseFileFragment(void* fragmentContext);

            [VtblIndex(5)]
            HRESULT GetFileSize([NativeTypeName("UINT64 *")] ulong* fileSize);

            [VtblIndex(6)]
            HRESULT GetLastWriteTime([NativeTypeName("UINT64 *")] ulong* lastWriteTime);
        }

        public partial struct Vtbl
        {
            [NativeTypeName("HRESULT (const IID &, void **) __attribute__((stdcall))")]
            public delegate* unmanaged<IDWriteFontFileStream*, Guid*, void**, int> QueryInterface;

            [NativeTypeName("ULONG () __attribute__((stdcall))")]
            public delegate* unmanaged<IDWriteFontFileStream*, uint> AddRef;

            [NativeTypeName("ULONG () __attribute__((stdcall))")]
            public delegate* unmanaged<IDWriteFontFileStream*, uint> Release;

            [NativeTypeName("HRESULT (const void **, UINT64, UINT64, void **) __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<IDWriteFontFileStream*, void**, ulong, ulong, void**, int> ReadFileFragment;

            [NativeTypeName("void (void *) __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<IDWriteFontFileStream*, void*, void> ReleaseFileFragment;

            [NativeTypeName("HRESULT (UINT64 *) __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<IDWriteFontFileStream*, ulong*, int> GetFileSize;

            [NativeTypeName("HRESULT (UINT64 *) __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<IDWriteFontFileStream*, ulong*, int> GetLastWriteTime;
        }
    }
}
