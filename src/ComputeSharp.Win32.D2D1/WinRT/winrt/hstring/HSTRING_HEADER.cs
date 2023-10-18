// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from winrt/hstring.h in the Windows SDK for Windows 10.0.22621.0
// Original source is Copyright © Microsoft. All rights reserved.

namespace ComputeSharp.Win32;

internal partial struct HSTRING_HEADER
{
    [NativeTypeName("union (anonymous union at C:/Program Files (x86)/Windows Kits/10/Include/10.0.22621.0/winrt/hstring.h:80:5)")]
    public _Reserved_e__Union Reserved;

    public unsafe partial struct _Reserved_e__Union
    {
        internal fixed byte Reserved1[24];
    }
}