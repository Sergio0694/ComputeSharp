// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/dxcapi.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved. Licensed under the University of Illinois Open Source License.

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace TerraFX.Interop.Windows
{
    internal static partial class CLSID
    {
        [NativeTypeName("const CLSID")]
        public static ref readonly Guid CLSID_DxcCompiler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                ReadOnlySpan<byte> data = new byte[] {
                    0x93, 0x2D, 0xE2, 0x73,
                    0xCE, 0xE6,
                    0xF3, 0x47,
                    0xB5,
                    0xBF,
                    0xF0,
                    0x66,
                    0x4F,
                    0x39,
                    0xC1,
                    0xB0
                };

                Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
                return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
            }
        }

        [NativeTypeName("const GUID")]
        public static ref readonly Guid CLSID_DxcLibrary
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                ReadOnlySpan<byte> data = new byte[] {
                    0xAF, 0xD6, 0x45, 0x62,
                    0xE0, 0x66,
                    0xFD, 0x48,
                    0x80,
                    0xB4,
                    0x4D,
                    0x27,
                    0x17,
                    0x96,
                    0x74,
                    0x8C
                };

                Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
                return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
            }
        }
    }
}