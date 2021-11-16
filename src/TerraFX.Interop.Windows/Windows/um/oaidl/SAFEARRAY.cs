// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/oaidl.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using MemoryMarshal = ComputeSharp.NetStandard.System.Runtime.InteropServices.MemoryMarshal;

namespace TerraFX.Interop.Windows
{
    internal unsafe partial struct SAFEARRAY
    {
        public ushort cDims;

        public ushort fFeatures;

        [NativeTypeName("ULONG")]
        public uint cbElements;

        [NativeTypeName("ULONG")]
        public uint cLocks;

        [NativeTypeName("PVOID")]
        public void* pvData;

        [NativeTypeName("SAFEARRAYBOUND [1]")]
        public _rgsabound_e__FixedBuffer rgsabound;

        internal partial struct _rgsabound_e__FixedBuffer
        {
            public SAFEARRAYBOUND e0;

            public ref SAFEARRAYBOUND this[int index]
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return ref AsSpan(int.MaxValue)[index];
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Span<SAFEARRAYBOUND> AsSpan(int length) => MemoryMarshal.CreateSpan(ref e0, length);
        }
    }
}
