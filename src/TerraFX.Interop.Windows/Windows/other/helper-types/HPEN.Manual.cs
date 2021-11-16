// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

using System;

namespace TerraFX.Interop.Windows
{
    public unsafe partial struct HPEN : IEquatable<HPEN>
    {
        public static explicit operator HPEN(HGDIOBJ value) => new HPEN(value.Value);

        public static implicit operator HGDIOBJ(HPEN value) => new HGDIOBJ(value.Value);
    }
}
