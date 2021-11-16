// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d3d12.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace TerraFX.Interop.DirectX
{
    public partial struct D3D12_DEPTH_STENCIL_VIEW_DESC
    {
        public DXGI_FORMAT Format;

        public D3D12_DSV_DIMENSION ViewDimension;

        public D3D12_DSV_FLAGS Flags;

        [NativeTypeName("D3D12_DEPTH_STENCIL_VIEW_DESC::(anonymous union at C:/Program Files (x86)/Windows Kits/10/Include/10.0.20348.0/um/d3d12.h:3462:5)")]
        public _Anonymous_e__Union Anonymous;

        public ref D3D12_TEX1D_DSV Texture1D
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Texture1D, 1));
            }
        }

        public ref D3D12_TEX1D_ARRAY_DSV Texture1DArray
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Texture1DArray, 1));
            }
        }

        public ref D3D12_TEX2D_DSV Texture2D
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Texture2D, 1));
            }
        }

        public ref D3D12_TEX2D_ARRAY_DSV Texture2DArray
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Texture2DArray, 1));
            }
        }

        public ref D3D12_TEX2DMS_DSV Texture2DMS
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Texture2DMS, 1));
            }
        }

        public ref D3D12_TEX2DMS_ARRAY_DSV Texture2DMSArray
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Texture2DMSArray, 1));
            }
        }

        [StructLayout(LayoutKind.Explicit)]
        public partial struct _Anonymous_e__Union
        {
            [FieldOffset(0)]
            public D3D12_TEX1D_DSV Texture1D;

            [FieldOffset(0)]
            public D3D12_TEX1D_ARRAY_DSV Texture1DArray;

            [FieldOffset(0)]
            public D3D12_TEX2D_DSV Texture2D;

            [FieldOffset(0)]
            public D3D12_TEX2D_ARRAY_DSV Texture2DArray;

            [FieldOffset(0)]
            public D3D12_TEX2DMS_DSV Texture2DMS;

            [FieldOffset(0)]
            public D3D12_TEX2DMS_ARRAY_DSV Texture2DMSArray;
        }
    }
}
