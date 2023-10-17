// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d2d1_1.h in the Windows SDK for Windows 10.0.22000.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TerraFX.Interop.Windows;

#pragma warning disable CS0649

namespace ComputeSharp.Win32;

[Guid("BB12D362-DAEE-4B9A-AA1D-14BA401CFA1F")]
[NativeTypeName("struct ID2D1Factory1 : ID2D1Factory")]
[NativeInheritance("ID2D1Factory")]
internal unsafe partial struct ID2D1Factory1
{
    public void** lpVtbl;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(23)]
#if NET6_0_OR_GREATER
    public HRESULT RegisterEffectFromString([NativeTypeName("const IID &")] Guid* classId, [NativeTypeName("PCWSTR")] ushort* propertyXml, [NativeTypeName("const D2D1_PROPERTY_BINDING *")] D2D1_PROPERTY_BINDING* bindings, [NativeTypeName("UINT32")] uint bindingsCount, [NativeTypeName("const PD2D1_EFFECT_FACTORY")] delegate* unmanaged[Stdcall]<IUnknown**, HRESULT> effectFactory)
    {
        return ((delegate* unmanaged[Stdcall]<ID2D1Factory1*, Guid*, ushort*, D2D1_PROPERTY_BINDING*, uint, delegate* unmanaged[Stdcall]<IUnknown**, HRESULT>, int>)(lpVtbl[23]))((ID2D1Factory1*)Unsafe.AsPointer(ref this), classId, propertyXml, bindings, bindingsCount, effectFactory);
    }
#else
    public HRESULT RegisterEffectFromString([NativeTypeName("const IID &")] Guid* classId, [NativeTypeName("PCWSTR")] ushort* propertyXml, [NativeTypeName("const D2D1_PROPERTY_BINDING *")] D2D1_PROPERTY_BINDING* bindings, [NativeTypeName("UINT32")] uint bindingsCount, [NativeTypeName("const PD2D1_EFFECT_FACTORY")] void* effectFactory)
    {
        return ((delegate* unmanaged[Stdcall]<ID2D1Factory1*, Guid*, ushort*, D2D1_PROPERTY_BINDING*, uint, void*, int>)(lpVtbl[23]))((ID2D1Factory1*)Unsafe.AsPointer(ref this), classId, propertyXml, bindings, bindingsCount, effectFactory);
    }
#endif
}