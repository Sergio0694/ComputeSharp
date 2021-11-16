// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

namespace TerraFX.Interop.Windows
{
    public unsafe partial struct HMODULE
    {
        public static explicit operator HMODULE(HINSTANCE value) => new HMODULE(value.Value);

        public static implicit operator HINSTANCE(HMODULE value) => new HINSTANCE(value.Value);
    }
}
