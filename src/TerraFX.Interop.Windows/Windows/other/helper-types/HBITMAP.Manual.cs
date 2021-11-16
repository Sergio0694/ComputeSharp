// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

using System;

namespace TerraFX.Interop.Windows
{
    public unsafe partial struct HBITMAP : IEquatable<HBITMAP>
    {
        public static explicit operator HBITMAP(HGDIOBJ value) => new HBITMAP(value.Value);

        public static implicit operator HGDIOBJ(HBITMAP value) => new HGDIOBJ(value.Value);
    }
}
