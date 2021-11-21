// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/wincodec.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace TerraFX.Interop.Windows
{
    internal static partial class GUID
    {
        [NativeTypeName("const GUID")]
        public static ref readonly Guid GUID_ContainerFormatBmp
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                ReadOnlySpan<byte> data = new byte[] {
                    0x7E, 0xD8, 0xF1, 0x0A,
                    0xFE, 0xFC,
                    0x88, 0x41,
                    0xBD,
                    0xEB,
                    0xA7,
                    0x90,
                    0x64,
                    0x71,
                    0xCB,
                    0xE3
                };

                Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
                return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
            }
        }

        [NativeTypeName("const GUID")]
        public static ref readonly Guid GUID_ContainerFormatPng
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                ReadOnlySpan<byte> data = new byte[] {
                    0xF4, 0xFA, 0x7C, 0x1B,
                    0x3F, 0x71,
                    0x3C, 0x47,
                    0xBB,
                    0xCD,
                    0x61,
                    0x37,
                    0x42,
                    0x5F,
                    0xAE,
                    0xAF
                };

                Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
                return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
            }
        }

        [NativeTypeName("const GUID")]
        public static ref readonly Guid GUID_ContainerFormatJpeg
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                ReadOnlySpan<byte> data = new byte[] {
                    0xAA, 0xA5, 0xE4, 0x19,
                    0x62, 0x56,
                    0xC5, 0x4F,
                    0xA0,
                    0xC0,
                    0x17,
                    0x58,
                    0x02,
                    0x8E,
                    0x10,
                    0x57
                };

                Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
                return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
            }
        }

        [NativeTypeName("const GUID")]
        public static ref readonly Guid GUID_ContainerFormatTiff
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                ReadOnlySpan<byte> data = new byte[] {
                    0x30, 0xCC, 0x3B, 0x16,
                    0xE9, 0xE2,
                    0x0B, 0x4F,
                    0x96,
                    0x1D,
                    0xA3,
                    0xE9,
                    0xFD,
                    0xB7,
                    0x88,
                    0xA3
                };

                Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
                return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
            }
        }

        [NativeTypeName("const GUID")]
        public static ref readonly Guid GUID_ContainerFormatWmp
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                ReadOnlySpan<byte> data = new byte[] {
                    0xAA, 0x7C, 0xA3, 0x57,
                    0x7A, 0x36,
                    0x40, 0x45,
                    0x91,
                    0x6B,
                    0xF1,
                    0x83,
                    0xC5,
                    0x09,
                    0x3A,
                    0x4B
                };

                Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
                return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
            }
        }

        [NativeTypeName("const GUID")]
        public static ref readonly Guid GUID_ContainerFormatDds
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                ReadOnlySpan<byte> data = new byte[] {
                    0x95, 0xCB, 0x67, 0x99,
                    0x85, 0x2E,
                    0xC8, 0x4A,
                    0x8C,
                    0xA2,
                    0x83,
                    0xD7,
                    0xCC,
                    0xD4,
                    0x25,
                    0xC9
                };

                Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
                return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
            }
        }

        [NativeTypeName("const GUID")]
        public static ref readonly Guid GUID_WICPixelFormat8bppGray
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                ReadOnlySpan<byte> data = new byte[] {
                    0x24, 0xC3, 0xDD, 0x6F,
                    0x03, 0x4E,
                    0xFE, 0x4B,
                    0xB1,
                    0x85,
                    0x3D,
                    0x77,
                    0x76,
                    0x8D,
                    0xC9,
                    0x08
                };

                Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
                return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
            }
        }

        [NativeTypeName("const GUID")]
        public static ref readonly Guid GUID_WICPixelFormat16bppGray
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                ReadOnlySpan<byte> data = new byte[] {
                    0x24, 0xC3, 0xDD, 0x6F,
                    0x03, 0x4E,
                    0xFE, 0x4B,
                    0xB1,
                    0x85,
                    0x3D,
                    0x77,
                    0x76,
                    0x8D,
                    0xC9,
                    0x0B
                };

                Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
                return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
            }
        }

        [NativeTypeName("const GUID")]
        public static ref readonly Guid GUID_WICPixelFormat24bppBGR
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                ReadOnlySpan<byte> data = new byte[] {
                    0x24, 0xC3, 0xDD, 0x6F,
                    0x03, 0x4E,
                    0xFE, 0x4B,
                    0xB1,
                    0x85,
                    0x3D,
                    0x77,
                    0x76,
                    0x8D,
                    0xC9,
                    0x0C
                };

                Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
                return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
            }
        }

        [NativeTypeName("const GUID")]
        public static ref readonly Guid GUID_WICPixelFormat32bppBGRA
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                ReadOnlySpan<byte> data = new byte[] {
                    0x24, 0xC3, 0xDD, 0x6F,
                    0x03, 0x4E,
                    0xFE, 0x4B,
                    0xB1,
                    0x85,
                    0x3D,
                    0x77,
                    0x76,
                    0x8D,
                    0xC9,
                    0x0F
                };

                Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
                return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
            }
        }

        [NativeTypeName("const GUID")]
        public static ref readonly Guid GUID_WICPixelFormat32bppRGBA
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                ReadOnlySpan<byte> data = new byte[] {
                    0x2D, 0xAD, 0xC7, 0xF5,
                    0x8D, 0x6A,
                    0xDD, 0x43,
                    0xA7,
                    0xA8,
                    0xA2,
                    0x99,
                    0x35,
                    0x26,
                    0x1A,
                    0xE9
                };

                Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
                return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
            }
        }

        [NativeTypeName("const GUID")]
        public static ref readonly Guid GUID_WICPixelFormat64bppRGBA
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                ReadOnlySpan<byte> data = new byte[] {
                    0x24, 0xC3, 0xDD, 0x6F,
                    0x03, 0x4E,
                    0xFE, 0x4B,
                    0xB1,
                    0x85,
                    0x3D,
                    0x77,
                    0x76,
                    0x8D,
                    0xC9,
                    0x16
                };

                Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
                return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
            }
        }
    }
}
