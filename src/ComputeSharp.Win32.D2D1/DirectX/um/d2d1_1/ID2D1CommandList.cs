// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d2d1_1.h in the Windows SDK for Windows 10.0.22621.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.InteropServices;

namespace TerraFX.Interop.DirectX;

[Guid("B4F34A19-2383-4D76-94F6-EC343657C3DC")]
[NativeTypeName("struct ID2D1CommandList : ID2D1Image")]
[NativeInheritance("ID2D1Image")]
internal unsafe partial struct ID2D1CommandList
{
    public void** lpVtbl;
}