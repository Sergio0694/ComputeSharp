// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d2d1.h in the Windows SDK for Windows 10.0.22000.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.InteropServices;

namespace ComputeSharp.Win32;

[Guid("06152247-6F50-465A-9245-118BFD3B6007")]
[NativeTypeName("struct ID2D1Factory : IUnknown")]
[NativeInheritance("IUnknown")]
internal unsafe partial struct ID2D1Factory
{
    public void** lpVtbl;
}