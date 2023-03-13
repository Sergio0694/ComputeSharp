// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from winrt/inspectable.h in the Windows SDK for Windows 10.0.22621.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TerraFX.Interop.Windows;

#pragma warning disable CS0649, IDE1006

namespace TerraFX.Interop.WinRT;

/// <summary>
/// The native WinRT interface for <see href="https://learn.microsoft.com/windows/win32/api/inspectable/nn-inspectable-iinspectable"><c>IInspectable</c></see> objects.
/// </summary>
[Guid("AF86E2E0-B12D-4C6A-9C5A-D7AA65101E90")]
[NativeTypeName("struct IInspectable : IUnknown")]
[NativeInheritance("IUnknown")]
internal unsafe struct IInspectable
{
    public void** lpVtbl;

    /// <inheritdoc cref="IUnknown.QueryInterface" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(0)]
    public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IInspectable*, Guid*, void**, int>)this.lpVtbl[0])((IInspectable*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    /// <inheritdoc cref="IUnknown.AddRef" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(1)]
    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IInspectable*, uint>)this.lpVtbl[1])((IInspectable*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IUnknown.Release" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(2)]
    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IInspectable*, uint>)this.lpVtbl[2])((IInspectable*)Unsafe.AsPointer(ref this));
    }
}