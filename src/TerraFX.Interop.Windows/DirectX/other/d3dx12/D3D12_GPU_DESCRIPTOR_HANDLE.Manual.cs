// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from d3dx12.h in DirectX-Graphics-Samples commit a7a87f1853b5540f10920518021d91ae641033fb
// Original source is Copyright © Microsoft. All rights reserved. Licensed under the MIT License (MIT).

using System;

namespace TerraFX.Interop.DirectX
{
    public unsafe partial struct D3D12_GPU_DESCRIPTOR_HANDLE : IEquatable<D3D12_GPU_DESCRIPTOR_HANDLE>
    {
        public static D3D12_GPU_DESCRIPTOR_HANDLE DEFAULT => default;

        public D3D12_GPU_DESCRIPTOR_HANDLE([NativeTypeName("const D3D12_GPU_DESCRIPTOR_HANDLE &")] in D3D12_GPU_DESCRIPTOR_HANDLE other, int offsetScaledByIncrementSize)
        {
            InitOffsetted(out this, other, offsetScaledByIncrementSize);
        }

        public D3D12_GPU_DESCRIPTOR_HANDLE([NativeTypeName("const D3D12_GPU_DESCRIPTOR_HANDLE &")] in D3D12_GPU_DESCRIPTOR_HANDLE other, int offsetInDescriptors, uint descriptorIncrementSize)
        {
            InitOffsetted(out this, other, offsetInDescriptors, descriptorIncrementSize);
        }

        public D3D12_GPU_DESCRIPTOR_HANDLE Offset(int offsetInDescriptors, uint descriptorIncrementSize)
        {
            ptr = (ulong)((long)ptr + ((long)offsetInDescriptors * (long)descriptorIncrementSize));
            return this;
        }

        public D3D12_GPU_DESCRIPTOR_HANDLE Offset(int offsetScaledByIncrementSize)
        {
            ptr = (ulong)((long)ptr + (long)offsetScaledByIncrementSize);
            return this;
        }

        public static bool operator ==([NativeTypeName("const D3D12_GPU_DESCRIPTOR_HANDLE &")] in D3D12_GPU_DESCRIPTOR_HANDLE l, [NativeTypeName("const D3D12_GPU_DESCRIPTOR_HANDLE &")] in D3D12_GPU_DESCRIPTOR_HANDLE r)
            => (l.ptr == r.ptr);

        public static bool operator !=([NativeTypeName("const D3D12_GPU_DESCRIPTOR_HANDLE &")] in D3D12_GPU_DESCRIPTOR_HANDLE l, [NativeTypeName("const D3D12_GPU_DESCRIPTOR_HANDLE &")] in D3D12_GPU_DESCRIPTOR_HANDLE r)
            => (l.ptr != r.ptr);

        public void InitOffsetted([NativeTypeName("const D3D12_GPU_DESCRIPTOR_HANDLE &")] in D3D12_GPU_DESCRIPTOR_HANDLE @base, int offsetScaledByIncrementSize)
        {
            InitOffsetted(out this, @base, offsetScaledByIncrementSize);
        }

        public void InitOffsetted([NativeTypeName("const D3D12_GPU_DESCRIPTOR_HANDLE &")] in D3D12_GPU_DESCRIPTOR_HANDLE @base, int offsetInDescriptors, uint descriptorIncrementSize)
        {
            InitOffsetted(out this, @base, offsetInDescriptors, descriptorIncrementSize);
        }

        public static void InitOffsetted([NativeTypeName("D3D12_GPU_DESCRIPTOR_HANDLE &")] out D3D12_GPU_DESCRIPTOR_HANDLE handle, [NativeTypeName("const D3D12_GPU_DESCRIPTOR_HANDLE &")] in D3D12_GPU_DESCRIPTOR_HANDLE @base, int offsetScaledByIncrementSize)
        {
            handle.ptr = (ulong)((long)@base.ptr + (long)offsetScaledByIncrementSize);
        }

        public static void InitOffsetted([NativeTypeName("D3D12_GPU_DESCRIPTOR_HANDLE &")] out D3D12_GPU_DESCRIPTOR_HANDLE handle, [NativeTypeName("const D3D12_GPU_DESCRIPTOR_HANDLE &")] in D3D12_GPU_DESCRIPTOR_HANDLE @base, int offsetInDescriptors, uint descriptorIncrementSize)
        {
            handle.ptr = (ulong)((long)@base.ptr + ((long)offsetInDescriptors * (long)descriptorIncrementSize));
        }

        public override bool Equals(object? obj) => (obj is D3D12_GPU_DESCRIPTOR_HANDLE other) && Equals(other);

        public bool Equals(D3D12_GPU_DESCRIPTOR_HANDLE other) => this == other;

        public override int GetHashCode() => ptr.GetHashCode();
    }
}
