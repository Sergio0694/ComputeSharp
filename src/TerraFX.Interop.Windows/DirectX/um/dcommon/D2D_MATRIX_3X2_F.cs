// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/dcommon.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace TerraFX.Interop.DirectX
{
    public unsafe partial struct D2D_MATRIX_3X2_F
    {
        [NativeTypeName("D2D_MATRIX_3X2_F::(anonymous union at C:/Program Files (x86)/Windows Kits/10/Include/10.0.20348.0/um/dcommon.h:277:5)")]
        public _Anonymous_e__Union Anonymous;

        public ref float m11
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous1.m11, 1));
            }
        }

        public ref float m12
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous1.m12, 1));
            }
        }

        public ref float m21
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous1.m21, 1));
            }
        }

        public ref float m22
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous1.m22, 1));
            }
        }

        public ref float dx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous1.dx, 1));
            }
        }

        public ref float dy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous1.dy, 1));
            }
        }

        public ref float _11
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous2._11, 1));
            }
        }

        public ref float _12
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous2._12, 1));
            }
        }

        public ref float _21
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous2._21, 1));
            }
        }

        public ref float _22
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous2._22, 1));
            }
        }

        public ref float _31
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous2._31, 1));
            }
        }

        public ref float _32
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous2._32, 1));
            }
        }

        public Span<float> m
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return MemoryMarshal.CreateSpan(ref Anonymous.m[0], 3);
            }
        }

        [StructLayout(LayoutKind.Explicit)]
        public unsafe partial struct _Anonymous_e__Union
        {
            [FieldOffset(0)]
            [NativeTypeName("D2D_MATRIX_3X2_F::(anonymous struct at C:/Program Files (x86)/Windows Kits/10/Include/10.0.20348.0/um/dcommon.h:279:9)")]
            public _Anonymous1_e__Struct Anonymous1;

            [FieldOffset(0)]
            [NativeTypeName("D2D_MATRIX_3X2_F::(anonymous struct at C:/Program Files (x86)/Windows Kits/10/Include/10.0.20348.0/um/dcommon.h:312:9)")]
            public _Anonymous2_e__Struct Anonymous2;

            [FieldOffset(0)]
            [NativeTypeName("FLOAT [3][2]")]
            public fixed float m[3 * 2];

            public partial struct _Anonymous1_e__Struct
            {
                public float m11;

                public float m12;

                public float m21;

                public float m22;

                public float dx;

                public float dy;
            }

            public partial struct _Anonymous2_e__Struct
            {
                public float _11;

                public float _12;

                public float _21;

                public float _22;

                public float _31;

                public float _32;
            }
        }
    }
}
