// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from shared/winerror.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

namespace ComputeSharp.Win32;

internal static partial class STG
{
    [NativeTypeName("#define STG_E_INVALIDFUNCTION _HRESULT_TYPEDEF_(0x80030001L)")]
    public const int STG_E_INVALIDFUNCTION = unchecked((int)(0x80030001));
}