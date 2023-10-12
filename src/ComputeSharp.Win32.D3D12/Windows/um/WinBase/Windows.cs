// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/synchapi.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System.Runtime.InteropServices;

namespace ComputeSharp.Win32;

internal static unsafe partial class Windows
{
    [DllImport("kernel32", ExactSpelling = true)]
    public static extern BOOL RegisterWaitForSingleObject([NativeTypeName("PHANDLE")] HANDLE* phNewWaitObject, HANDLE hObject, [NativeTypeName("WAITORTIMERCALLBACK")] delegate* unmanaged<void*, byte, void> Callback, [NativeTypeName("PVOID")] void* Context, [NativeTypeName("ULONG")] uint dwMilliseconds, [NativeTypeName("ULONG")] uint dwFlags);

    [DllImport("kernel32", ExactSpelling = true)]
    public static extern BOOL UnregisterWait(HANDLE WaitHandle);

    [NativeTypeName("#define INFINITE 0xFFFFFFFF")]
    public const uint INFINITE = 0xFFFFFFFF;
}