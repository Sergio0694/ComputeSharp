// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d2d1effectauthor.h in the Windows SDK for Windows 10.0.22000.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.InteropServices;

#pragma warning disable CS0649

namespace TerraFX.Interop.DirectX;

[Guid("B2EFE1E7-729F-4102-949F-505FA21BF666")]
[NativeTypeName("struct ID2D1TransformNode : IUnknown")]
[NativeInheritance("IUnknown")]
internal unsafe partial struct ID2D1TransformNode
{
    public void** lpVtbl;
}