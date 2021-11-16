// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d3d12.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace TerraFX.Interop.DirectX
{
    public partial struct D3D12_SHADER_RESOURCE_VIEW_DESC
    {
        public DXGI_FORMAT Format;

        public D3D12_SRV_DIMENSION ViewDimension;

        public uint Shader4ComponentMapping;

        [NativeTypeName("D3D12_SHADER_RESOURCE_VIEW_DESC::(anonymous union at C:/Program Files (x86)/Windows Kits/10/Include/10.0.20348.0/um/d3d12.h:3094:5)")]
        public _Anonymous_e__Union Anonymous;

        public ref D3D12_BUFFER_SRV Buffer
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Buffer, 1));
            }
        }

        public ref D3D12_TEX1D_SRV Texture1D
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Texture1D, 1));
            }
        }

        public ref D3D12_TEX1D_ARRAY_SRV Texture1DArray
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Texture1DArray, 1));
            }
        }

        public ref D3D12_TEX2D_SRV Texture2D
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Texture2D, 1));
            }
        }

        public ref D3D12_TEX2D_ARRAY_SRV Texture2DArray
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Texture2DArray, 1));
            }
        }

        public ref D3D12_TEX2DMS_SRV Texture2DMS
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Texture2DMS, 1));
            }
        }

        public ref D3D12_TEX2DMS_ARRAY_SRV Texture2DMSArray
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Texture2DMSArray, 1));
            }
        }

        public ref D3D12_TEX3D_SRV Texture3D
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Texture3D, 1));
            }
        }

        public ref D3D12_TEXCUBE_SRV TextureCube
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.TextureCube, 1));
            }
        }

        public ref D3D12_TEXCUBE_ARRAY_SRV TextureCubeArray
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.TextureCubeArray, 1));
            }
        }

        public ref D3D12_RAYTRACING_ACCELERATION_STRUCTURE_SRV RaytracingAccelerationStructure
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.RaytracingAccelerationStructure, 1));
            }
        }

        [StructLayout(LayoutKind.Explicit)]
        public partial struct _Anonymous_e__Union
        {
            [FieldOffset(0)]
            public D3D12_BUFFER_SRV Buffer;

            [FieldOffset(0)]
            public D3D12_TEX1D_SRV Texture1D;

            [FieldOffset(0)]
            public D3D12_TEX1D_ARRAY_SRV Texture1DArray;

            [FieldOffset(0)]
            public D3D12_TEX2D_SRV Texture2D;

            [FieldOffset(0)]
            public D3D12_TEX2D_ARRAY_SRV Texture2DArray;

            [FieldOffset(0)]
            public D3D12_TEX2DMS_SRV Texture2DMS;

            [FieldOffset(0)]
            public D3D12_TEX2DMS_ARRAY_SRV Texture2DMSArray;

            [FieldOffset(0)]
            public D3D12_TEX3D_SRV Texture3D;

            [FieldOffset(0)]
            public D3D12_TEXCUBE_SRV TextureCube;

            [FieldOffset(0)]
            public D3D12_TEXCUBE_ARRAY_SRV TextureCubeArray;

            [FieldOffset(0)]
            public D3D12_RAYTRACING_ACCELERATION_STRUCTURE_SRV RaytracingAccelerationStructure;
        }
    }
}
