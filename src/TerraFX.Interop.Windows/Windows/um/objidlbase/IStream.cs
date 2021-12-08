// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/objidlbase.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace TerraFX.Interop.Windows
{
    [Guid("0000000C-0000-0000-C000-000000000046")]
    [NativeTypeName("struct IStream : ISequentialStream")]
    [NativeInheritance("ISequentialStream")]
    internal unsafe partial struct IStream
    {
        public void** lpVtbl;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(0)]
        public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
        {
            return ((delegate* unmanaged[Stdcall]<IStream*, Guid*, void**, int>)(lpVtbl[0]))((IStream*)Unsafe.AsPointer(ref this), riid, ppvObject);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(1)]
        [return: NativeTypeName("ULONG")]
        public uint AddRef()
        {
            return ((delegate* unmanaged[Stdcall]<IStream*, uint>)(lpVtbl[1]))((IStream*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(2)]
        [return: NativeTypeName("ULONG")]
        public uint Release()
        {
            return ((delegate* unmanaged[Stdcall]<IStream*, uint>)(lpVtbl[2]))((IStream*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(3)]
        public HRESULT Read(void* pv, [NativeTypeName("ULONG")] uint cb, [NativeTypeName("ULONG *")] uint* pcbRead)
        {
            return ((delegate* unmanaged[Stdcall]<IStream*, void*, uint, uint*, int>)(lpVtbl[3]))((IStream*)Unsafe.AsPointer(ref this), pv, cb, pcbRead);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(4)]
        public HRESULT Write([NativeTypeName("const void *")] void* pv, [NativeTypeName("ULONG")] uint cb, [NativeTypeName("ULONG *")] uint* pcbWritten)
        {
            return ((delegate* unmanaged[Stdcall]<IStream*, void*, uint, uint*, int>)(lpVtbl[4]))((IStream*)Unsafe.AsPointer(ref this), pv, cb, pcbWritten);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(5)]
        public HRESULT Seek(LARGE_INTEGER dlibMove, [NativeTypeName("DWORD")] uint dwOrigin, ULARGE_INTEGER* plibNewPosition)
        {
            return ((delegate* unmanaged[Stdcall]<IStream*, LARGE_INTEGER, uint, ULARGE_INTEGER*, int>)(lpVtbl[5]))((IStream*)Unsafe.AsPointer(ref this), dlibMove, dwOrigin, plibNewPosition);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(6)]
        public HRESULT SetSize(ULARGE_INTEGER libNewSize)
        {
            return ((delegate* unmanaged[Stdcall]<IStream*, ULARGE_INTEGER, int>)(lpVtbl[6]))((IStream*)Unsafe.AsPointer(ref this), libNewSize);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(7)]
        public HRESULT CopyTo(IStream* pstm, ULARGE_INTEGER cb, ULARGE_INTEGER* pcbRead, ULARGE_INTEGER* pcbWritten)
        {
            return ((delegate* unmanaged[Stdcall]<IStream*, IStream*, ULARGE_INTEGER, ULARGE_INTEGER*, ULARGE_INTEGER*, int>)(lpVtbl[7]))((IStream*)Unsafe.AsPointer(ref this), pstm, cb, pcbRead, pcbWritten);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(8)]
        public HRESULT Commit([NativeTypeName("DWORD")] uint grfCommitFlags)
        {
            return ((delegate* unmanaged[Stdcall]<IStream*, uint, int>)(lpVtbl[8]))((IStream*)Unsafe.AsPointer(ref this), grfCommitFlags);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(9)]
        public HRESULT Revert()
        {
            return ((delegate* unmanaged[Stdcall]<IStream*, int>)(lpVtbl[9]))((IStream*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(10)]
        public HRESULT LockRegion(ULARGE_INTEGER libOffset, ULARGE_INTEGER cb, [NativeTypeName("DWORD")] uint dwLockType)
        {
            return ((delegate* unmanaged[Stdcall]<IStream*, ULARGE_INTEGER, ULARGE_INTEGER, uint, int>)(lpVtbl[10]))((IStream*)Unsafe.AsPointer(ref this), libOffset, cb, dwLockType);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(11)]
        public HRESULT UnlockRegion(ULARGE_INTEGER libOffset, ULARGE_INTEGER cb, [NativeTypeName("DWORD")] uint dwLockType)
        {
            return ((delegate* unmanaged[Stdcall]<IStream*, ULARGE_INTEGER, ULARGE_INTEGER, uint, int>)(lpVtbl[11]))((IStream*)Unsafe.AsPointer(ref this), libOffset, cb, dwLockType);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(12)]
        public HRESULT Stat(STATSTG* pstatstg, [NativeTypeName("DWORD")] uint grfStatFlag)
        {
            return ((delegate* unmanaged[Stdcall]<IStream*, STATSTG*, uint, int>)(lpVtbl[12]))((IStream*)Unsafe.AsPointer(ref this), pstatstg, grfStatFlag);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(13)]
        public HRESULT Clone(IStream** ppstm)
        {
            return ((delegate* unmanaged[Stdcall]<IStream*, IStream**, int>)(lpVtbl[13]))((IStream*)Unsafe.AsPointer(ref this), ppstm);
        }
    }
}
