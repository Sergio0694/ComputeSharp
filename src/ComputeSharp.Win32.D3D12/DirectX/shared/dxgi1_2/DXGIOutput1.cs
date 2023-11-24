// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from shared/dxgi1_2.h in the Windows SDK for Windows 10.0.22000.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ComputeSharp.Win32;

[NativeTypeName("struct IDXGIOutput1 : IDXGIOutput")]
[NativeInheritance("IDXGIOutput")]
internal unsafe partial struct IDXGIOutput1 : IComObject
{
    /// <inheritdoc/>
    static Guid* IComObject.IID
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            ReadOnlySpan<byte> data =
            [
                0xA8, 0xDE, 0xCD, 0x00,
                0x9B, 0x93,
                0x83, 0x4B,
                0xA3,
                0x40,
                0xA6,
                0x85,
                0x22,
                0x66,
                0x66,
                0xCC
            ];

            return (Guid*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(data));
        }
    }

    public void** lpVtbl;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(20)]
    public HRESULT FindClosestMatchingMode1([NativeTypeName("const DXGI_MODE_DESC1 *")] DXGI_MODE_DESC1* pModeToMatch, DXGI_MODE_DESC1* pClosestMatch, IUnknown* pConcernedDevice)
    {
        return ((delegate* unmanaged[MemberFunction]<IDXGIOutput1*, DXGI_MODE_DESC1*, DXGI_MODE_DESC1*, IUnknown*, int>)(lpVtbl[20]))((IDXGIOutput1*)Unsafe.AsPointer(ref this), pModeToMatch, pClosestMatch, pConcernedDevice);
    }
}