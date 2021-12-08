// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/coml2api.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

#if NET6_0_OR_GREATER

namespace TerraFX.Interop.Windows;

// TODO: remove this once TerraFX.Interop.Windows is updated
internal static partial class STGM
{
    public const int STGM_READ = 0x00000000;

    public const int STGM_WRITE = 0x00000001;

    public const int STGM_READWRITE = 0x00000002;
}

#endif