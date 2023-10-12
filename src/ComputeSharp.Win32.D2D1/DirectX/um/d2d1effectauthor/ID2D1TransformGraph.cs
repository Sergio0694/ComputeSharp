// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d2d1effectauthor.h in the Windows SDK for Windows 10.0.22000.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TerraFX.Interop.Windows;

#pragma warning disable CS0649

namespace TerraFX.Interop.DirectX;

[Guid("13D29038-C3E6-4034-9081-13B53A417992")]
[NativeTypeName("struct ID2D1TransformGraph : IUnknown")]
[NativeInheritance("IUnknown")]
internal unsafe partial struct ID2D1TransformGraph
{
    public void** lpVtbl;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(4)]
    public HRESULT SetSingleTransformNode(ID2D1TransformNode* node)
    {
        return ((delegate* unmanaged[Stdcall]<ID2D1TransformGraph*, ID2D1TransformNode*, int>)(lpVtbl[4]))((ID2D1TransformGraph*)Unsafe.AsPointer(ref this), node);
    }
}