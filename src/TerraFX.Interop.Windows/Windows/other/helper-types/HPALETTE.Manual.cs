// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

using System;

namespace TerraFX.Interop.Windows
{
    public unsafe partial struct HPALETTE : IEquatable<HPALETTE>
    {
        public static explicit operator HPALETTE(HGDIOBJ value) => new HPALETTE(value.Value);

        public static implicit operator HGDIOBJ(HPALETTE value) => new HGDIOBJ(value.Value);
    }
}
