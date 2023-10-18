// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d2d1effectauthor.h in the Windows SDK for Windows 10.0.22000.0
// Original source is Copyright © Microsoft. All rights reserved.

namespace ComputeSharp.Win32;

internal partial struct D2D1_INPUT_DESCRIPTION
{
    public D2D1_FILTER filter;

    [NativeTypeName("UINT32")]
    public uint levelOfDetailCount;
}