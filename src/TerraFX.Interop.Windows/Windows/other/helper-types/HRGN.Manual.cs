// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

using System;

namespace TerraFX.Interop.Windows
{
    public unsafe partial struct HRGN : IEquatable<HRGN>
    {
        public static explicit operator HRGN(HGDIOBJ value) => new HRGN(value.Value);

        public static implicit operator HGDIOBJ(HRGN value) => new HGDIOBJ(value.Value);
    }
}
