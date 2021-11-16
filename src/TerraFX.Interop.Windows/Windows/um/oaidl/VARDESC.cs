// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/oaidl.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace TerraFX.Interop.Windows
{
    public unsafe partial struct VARDESC
    {
        [NativeTypeName("MEMBERID")]
        public int memid;

        [NativeTypeName("LPOLESTR")]
        public ushort* lpstrSchema;

        [NativeTypeName("tagVARDESC::(anonymous union at C:/Program Files (x86)/Windows Kits/10/Include/10.0.20348.0/um/oaidl.h:880:36)")]
        public _Anonymous_e__Union Anonymous;

        public ELEMDESC elemdescVar;

        [NativeTypeName("WORD")]
        public ushort wVarFlags;

        public VARKIND varkind;

        public ref uint oInst
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.oInst, 1));
            }
        }

        public ref VARIANT* lpvarValue
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1)).Anonymous.lpvarValue;
            }
        }

        [StructLayout(LayoutKind.Explicit)]
        public unsafe partial struct _Anonymous_e__Union
        {
            [FieldOffset(0)]
            [NativeTypeName("ULONG")]
            public uint oInst;

            [FieldOffset(0)]
            public VARIANT* lpvarValue;
        }
    }
}
