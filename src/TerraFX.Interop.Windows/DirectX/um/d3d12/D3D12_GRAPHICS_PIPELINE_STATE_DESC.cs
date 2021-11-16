// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d3d12.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TerraFX.Interop.Windows;

namespace TerraFX.Interop.DirectX
{
    public unsafe partial struct D3D12_GRAPHICS_PIPELINE_STATE_DESC
    {
        public ID3D12RootSignature* pRootSignature;

        public D3D12_SHADER_BYTECODE VS;

        public D3D12_SHADER_BYTECODE PS;

        public D3D12_SHADER_BYTECODE DS;

        public D3D12_SHADER_BYTECODE HS;

        public D3D12_SHADER_BYTECODE GS;

        public D3D12_STREAM_OUTPUT_DESC StreamOutput;

        public D3D12_BLEND_DESC BlendState;

        public uint SampleMask;

        public D3D12_RASTERIZER_DESC RasterizerState;

        public D3D12_DEPTH_STENCIL_DESC DepthStencilState;

        public D3D12_INPUT_LAYOUT_DESC InputLayout;

        public D3D12_INDEX_BUFFER_STRIP_CUT_VALUE IBStripCutValue;

        public D3D12_PRIMITIVE_TOPOLOGY_TYPE PrimitiveTopologyType;

        public uint NumRenderTargets;

        [NativeTypeName("DXGI_FORMAT [8]")]
        public _RTVFormats_e__FixedBuffer RTVFormats;

        public DXGI_FORMAT DSVFormat;

        public DXGI_SAMPLE_DESC SampleDesc;

        public uint NodeMask;

        public D3D12_CACHED_PIPELINE_STATE CachedPSO;

        public D3D12_PIPELINE_STATE_FLAGS Flags;

        public partial struct _RTVFormats_e__FixedBuffer
        {
            public DXGI_FORMAT e0;
            public DXGI_FORMAT e1;
            public DXGI_FORMAT e2;
            public DXGI_FORMAT e3;
            public DXGI_FORMAT e4;
            public DXGI_FORMAT e5;
            public DXGI_FORMAT e6;
            public DXGI_FORMAT e7;

            public ref DXGI_FORMAT this[int index]
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return ref AsSpan()[index];
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Span<DXGI_FORMAT> AsSpan() => MemoryMarshal.CreateSpan(ref e0, 8);
        }
    }
}
