// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from shared/winerror.h in the Windows SDK for Windows 10.0.22621.0
// Original source is Copyright © Microsoft. All rights reserved.

namespace ComputeSharp.Win32;

internal static partial class RO
{
    [NativeTypeName("#define RO_E_CLOSED _HRESULT_TYPEDEF_(0x80000013L)")]
    public const int RO_E_CLOSED = unchecked((int)(0x80000013));
}