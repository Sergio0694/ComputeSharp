// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from d3dx12.h in DirectX-Graphics-Samples commit a7a87f1853b5540f10920518021d91ae641033fb
// Original source is Copyright © Microsoft. All rights reserved. Licensed under the MIT License (MIT).

using System;

namespace TerraFX.Interop.DirectX
{
    public partial struct D3D12_BOX : IEquatable<D3D12_BOX>
    {
        public D3D12_BOX([NativeTypeName("LONG")] int Left, [NativeTypeName("LONG")] int Right)
        {
            left = (uint)Left;
            top = 0;
            front = 0;
            right = (uint)Right;
            bottom = 1;
            back = 1;
        }

        public D3D12_BOX([NativeTypeName("LONG")] int Left, [NativeTypeName("LONG")] int Top, [NativeTypeName("LONG")] int Right, [NativeTypeName("LONG")] int Bottom)
        {
            left = (uint)Left;
            top = (uint)Top;
            front = 0;
            right = (uint)Right;
            bottom = (uint)Bottom;
            back = 1;
        }

        public D3D12_BOX([NativeTypeName("LONG")] int Left, [NativeTypeName("LONG")] int Top, [NativeTypeName("LONG")] int Front, [NativeTypeName("LONG")] int Right, [NativeTypeName("LONG")] int Bottom, [NativeTypeName("LONG")] int Back)
        {
            left = (uint)Left;
            top = (uint)Top;
            front = (uint)Front;
            right = (uint)Right;
            bottom = (uint)Bottom;
            back = (uint)Back;
        }

        public static bool operator ==([NativeTypeName("const D3D12_BOX &")] in D3D12_BOX l, [NativeTypeName("const D3D12_BOX &")] in D3D12_BOX r)
        {
            return l.left == r.left && l.top == r.top && l.front == r.front && l.right == r.right && l.bottom == r.bottom && l.back == r.back;
        }

        public static bool operator !=([NativeTypeName("const D3D12_BOX &")] in D3D12_BOX l, [NativeTypeName("const D3D12_BOX &")] in D3D12_BOX r)
        {
            return !(l == r);
        }

        public override bool Equals(object? obj) => (obj is D3D12_BOX other) && Equals(other);

        public bool Equals(D3D12_BOX other) => this == other;

        public override int GetHashCode() => HashCode.Combine(left, top, front, right, bottom, back);
    }
}
