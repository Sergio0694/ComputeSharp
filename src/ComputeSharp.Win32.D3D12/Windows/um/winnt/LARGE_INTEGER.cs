// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/winnt.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System.Runtime.InteropServices;

namespace ComputeSharp.Win32;

[StructLayout(LayoutKind.Explicit)]
internal partial struct LARGE_INTEGER
{
    [FieldOffset(0)]
    [NativeTypeName("_LARGE_INTEGER::(anonymous struct at C:/Program Files (x86)/Windows Kits/10/Include/10.0.20348.0/um/winnt.h:857:5)")]
    public _Anonymous_e__Struct Anonymous;

    [FieldOffset(0)]
    [NativeTypeName("struct (anonymous struct at C:/Program Files (x86)/Windows Kits/10/Include/10.0.20348.0/um/winnt.h:861:5)")]
    public _u_e__Struct u;

    [FieldOffset(0)]
    [NativeTypeName("LONGLONG")]
    public long QuadPart;

    internal partial struct _Anonymous_e__Struct
    {
        [NativeTypeName("DWORD")]
        public uint LowPart;

        [NativeTypeName("LONG")]
        public int HighPart;
    }

    internal partial struct _u_e__Struct
    {
        [NativeTypeName("DWORD")]
        public uint LowPart;

        [NativeTypeName("LONG")]
        public int HighPart;
    }
}