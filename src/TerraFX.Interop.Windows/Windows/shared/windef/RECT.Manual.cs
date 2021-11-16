// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from shared/windef.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;

namespace TerraFX.Interop.Windows
{
    public partial struct RECT : IEquatable<RECT>
    {
        public RECT([NativeTypeName("LONG")] int Left, [NativeTypeName("LONG")] int Top, [NativeTypeName("LONG")] int Right, [NativeTypeName("LONG")] int Bottom)
        {
            left = Left;
            top = Top;
            right = Right;
            bottom = Bottom;
        }

        public static bool operator ==([NativeTypeName("const RECT &")] in RECT l, [NativeTypeName("const RECT &")] in RECT r)
        {
            return (l.left == r.left)
                && (l.top == r.top)
                && (l.right == r.right)
                && (l.bottom == r.bottom);
        }

        public static bool operator !=([NativeTypeName("const RECT &")] in RECT l, [NativeTypeName("const RECT &")] in RECT r)
            => !(l == r);

        public override bool Equals(object? obj) => (obj is RECT other) && Equals(other);

        public bool Equals(RECT other) => this == other;

        public override int GetHashCode() => HashCode.Combine(left, top, right, bottom);
    }
}
