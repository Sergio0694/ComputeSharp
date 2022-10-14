// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d3d12.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

#if !NET6_0_OR_GREATER

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace TerraFX.Interop.DirectX
{
    [Guid("8B4F173B-2FEA-4B80-8F58-4307191AB95D")]
    public unsafe partial struct ID3D12Device5
    {
        public void** lpVtbl;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint Release()
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12Device5*, uint>)(lpVtbl[2]))((ID3D12Device5*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RemoveDevice()
        {
            ((delegate* unmanaged[Stdcall]<ID3D12Device5*, void>)(lpVtbl[58]))((ID3D12Device5*)Unsafe.AsPointer(ref this));
        }
    }
}

#endif