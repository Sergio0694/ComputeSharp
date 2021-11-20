// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/winnt.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

namespace TerraFX.Interop.Windows
{
    internal static partial class Windows
    {
        [NativeTypeName("#define GENERIC_READ (0x80000000L)")]
        public const uint GENERIC_READ = (0x80000000);

        [NativeTypeName("#define GENERIC_WRITE (0x40000000L)")]
        public const int GENERIC_WRITE = (0x40000000);
    }
}
