// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d3d12.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TerraFX.Interop.Windows;

namespace TerraFX.Interop.DirectX
{
    public partial struct D3D12_BLEND_DESC
    {
        public BOOL AlphaToCoverageEnable;

        public BOOL IndependentBlendEnable;

        [NativeTypeName("D3D12_RENDER_TARGET_BLEND_DESC [8]")]
        public _RenderTarget_e__FixedBuffer RenderTarget;

        public partial struct _RenderTarget_e__FixedBuffer
        {
            public D3D12_RENDER_TARGET_BLEND_DESC e0;
            public D3D12_RENDER_TARGET_BLEND_DESC e1;
            public D3D12_RENDER_TARGET_BLEND_DESC e2;
            public D3D12_RENDER_TARGET_BLEND_DESC e3;
            public D3D12_RENDER_TARGET_BLEND_DESC e4;
            public D3D12_RENDER_TARGET_BLEND_DESC e5;
            public D3D12_RENDER_TARGET_BLEND_DESC e6;
            public D3D12_RENDER_TARGET_BLEND_DESC e7;

            public ref D3D12_RENDER_TARGET_BLEND_DESC this[int index]
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return ref AsSpan()[index];
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Span<D3D12_RENDER_TARGET_BLEND_DESC> AsSpan() => MemoryMarshal.CreateSpan(ref e0, 8);
        }
    }
}
