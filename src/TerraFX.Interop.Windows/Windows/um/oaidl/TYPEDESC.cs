// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/oaidl.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace TerraFX.Interop.Windows
{
    public unsafe partial struct TYPEDESC
    {
        [NativeTypeName("tagTYPEDESC::(anonymous union at C:/Program Files (x86)/Windows Kits/10/Include/10.0.20348.0/um/oaidl.h:653:36)")]
        public _Anonymous_e__Union Anonymous;

        [NativeTypeName("VARTYPE")]
        public ushort vt;

        public ref TYPEDESC* lptdesc
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1)).Anonymous.lptdesc;
            }
        }

        public ref ARRAYDESC* lpadesc
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1)).Anonymous.lpadesc;
            }
        }

        public ref uint hreftype
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.hreftype, 1));
            }
        }

        [StructLayout(LayoutKind.Explicit)]
        public unsafe partial struct _Anonymous_e__Union
        {
            [FieldOffset(0)]
            [NativeTypeName("struct tagTYPEDESC *")]
            public TYPEDESC* lptdesc;

            [FieldOffset(0)]
            [NativeTypeName("struct tagARRAYDESC *")]
            public ARRAYDESC* lpadesc;

            [FieldOffset(0)]
            [NativeTypeName("HREFTYPE")]
            public uint hreftype;
        }
    }
}
