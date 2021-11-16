// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

using System;

namespace TerraFX.Interop.Windows
{
    public unsafe partial struct HBRUSH : IEquatable<HBRUSH>
    {
        public static explicit operator HBRUSH(HGDIOBJ value) => new HBRUSH(value.Value);

        public static implicit operator HGDIOBJ(HBRUSH value) => new HGDIOBJ(value.Value);
    }
}
