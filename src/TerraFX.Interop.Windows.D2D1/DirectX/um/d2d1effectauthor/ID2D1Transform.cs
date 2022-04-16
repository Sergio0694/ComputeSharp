// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d2d1effectauthor.h in the Windows SDK for Windows 10.0.22000.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.InteropServices;

#pragma warning disable CS0649

namespace TerraFX.Interop.DirectX
{
    [Guid("EF1A287D-342A-4F76-8FDB-DA0D6EA9F92B")]
    [NativeTypeName("struct ID2D1Transform : ID2D1TransformNode")]
    [NativeInheritance("ID2D1TransformNode")]
    internal unsafe partial struct ID2D1Transform
    {
        public void** lpVtbl;
    }
}