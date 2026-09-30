// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/synchapi.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System.Runtime.InteropServices;

namespace ComputeSharp.Win32;

internal static unsafe partial class Windows
{
    // Runtime marshalling is disabled for this assembly. LibraryImport generates
    // the last-error capture needed by Marshal.GetLastPInvokeError; adding
    // SetLastError to DllImport would rely on disabled runtime support.
    [LibraryImport("kernel32.dll", SetLastError = true)]
    public static partial BOOL RegisterWaitForSingleObject([NativeTypeName("PHANDLE")] HANDLE* phNewWaitObject, HANDLE hObject, [NativeTypeName("WAITORTIMERCALLBACK")] delegate* unmanaged<void*, byte, void> Callback, [NativeTypeName("PVOID")] void* Context, [NativeTypeName("ULONG")] uint dwMilliseconds, [NativeTypeName("ULONG")] uint dwFlags);

    // The completion argument selects blocking or nonblocking cancellation.
    // Capture last error to distinguish ERROR_IO_PENDING from cancellation failure.
    [LibraryImport("kernel32.dll", SetLastError = true)]
    public static partial BOOL UnregisterWaitEx(HANDLE WaitHandle, HANDLE CompletionEvent);

    [DllImport("kernel32", ExactSpelling = true)]
    public static extern BOOL UnregisterWait(HANDLE WaitHandle);

    [NativeTypeName("#define INFINITE 0xFFFFFFFF")]
    public const uint INFINITE = 0xFFFFFFFF;
}
