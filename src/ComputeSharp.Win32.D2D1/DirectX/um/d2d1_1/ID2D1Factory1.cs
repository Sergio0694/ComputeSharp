// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d2d1_1.h in the Windows SDK for Windows 10.0.22000.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#pragma warning disable IDE0055

namespace ComputeSharp.Win32;

[NativeTypeName("struct ID2D1Factory1 : ID2D1Factory")]
[NativeInheritance("ID2D1Factory")]
internal unsafe partial struct ID2D1Factory1 : IComObject
{
    /// <inheritdoc/>
    static Guid* IComObject.IID
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            ReadOnlySpan<byte> data =
            [
                0x62, 0xD3, 0x12, 0xBB,
                0xEE, 0xDA,
                0x9A, 0x4B,
                0xAA,
                0x1D,
                0x14,
                0xBA,
                0x40,
                0x1C,
                0xFA,
                0x1F
            ];

            return (Guid*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(data));
        }
    }

    public void** lpVtbl;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(23)]
    public HRESULT RegisterEffectFromString([NativeTypeName("const IID &")] Guid* classId, [NativeTypeName("PCWSTR")] ushort* propertyXml, [NativeTypeName("const D2D1_PROPERTY_BINDING *")] D2D1_PROPERTY_BINDING* bindings, [NativeTypeName("UINT32")] uint bindingsCount, [NativeTypeName("const PD2D1_EFFECT_FACTORY")] delegate* unmanaged<IUnknown**, HRESULT> effectFactory)
    {
        return ((delegate* unmanaged<ID2D1Factory1*, Guid*, ushort*, D2D1_PROPERTY_BINDING*, uint, delegate* unmanaged<IUnknown**, HRESULT>, int>)(lpVtbl[23]))((ID2D1Factory1*)Unsafe.AsPointer(ref this), classId, propertyXml, bindings, bindingsCount, effectFactory);
    }
}