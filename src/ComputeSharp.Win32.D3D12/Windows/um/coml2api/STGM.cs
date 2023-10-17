// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/coml2api.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

namespace ComputeSharp.Win32;

internal static partial class STGM
{
    [NativeTypeName("#define STGM_READ 0x00000000L")]
    public const int STGM_READ = 0x00000000;

    [NativeTypeName("#define STGM_WRITE 0x00000001L")]
    public const int STGM_WRITE = 0x00000001;

    [NativeTypeName("#define STGM_READWRITE 0x00000002L")]
    public const int STGM_READWRITE = 0x00000002;
}