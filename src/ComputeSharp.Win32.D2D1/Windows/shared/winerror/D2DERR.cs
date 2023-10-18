// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from shared/winerror.h in the Windows SDK for Windows 10.0.22000.0
// Original source is Copyright © Microsoft. All rights reserved.

namespace ComputeSharp.Win32;

internal static partial class D2DERR
{
    [NativeTypeName("#define D2DERR_WRONG_FACTORY _HRESULT_TYPEDEF_(0x88990012L)")]
    public const int D2DERR_WRONG_FACTORY = unchecked((int)(0x88990012));

    [NativeTypeName("#define D2DERR_CYCLIC_GRAPH _HRESULT_TYPEDEF_(0x88990020L)")]
    public const int D2DERR_CYCLIC_GRAPH = unchecked((int)(0x88990020));

    [NativeTypeName("#define D2DERR_INSUFFICIENT_DEVICE_CAPABILITIES _HRESULT_TYPEDEF_(0x88990026L)")]
    public const int D2DERR_INSUFFICIENT_DEVICE_CAPABILITIES = unchecked((int)(0x88990026));

    [NativeTypeName("#define D2DERR_EFFECT_IS_NOT_REGISTERED _HRESULT_TYPEDEF_(0x88990028L)")]
    public const int D2DERR_EFFECT_IS_NOT_REGISTERED = unchecked((int)(0x88990028));
}