// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d2d1effectauthor.h in the Windows SDK for Windows 10.0.22000.0
// Original source is Copyright © Microsoft. All rights reserved.

#pragma warning disable CS0649

#if NET6_0_OR_GREATER
using TerraFX.Interop.Windows;
#endif

namespace ComputeSharp.Win32;

internal unsafe partial struct D2D1_PROPERTY_BINDING
{
    [NativeTypeName("PCWSTR")]
    public ushort* propertyName;

    [NativeTypeName("PD2D1_PROPERTY_SET_FUNCTION")]
#if NET6_0_OR_GREATER
    public delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, int> setFunction;
#else
    public void* setFunction;
#endif

    [NativeTypeName("PD2D1_PROPERTY_GET_FUNCTION")]
#if NET6_0_OR_GREATER
    public delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, uint*, int> getFunction;
#else
    public void* getFunction;
#endif
}