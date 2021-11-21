// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/synchapi.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System.Runtime.InteropServices;

namespace TerraFX.Interop.Windows
{
    internal static unsafe partial class Windows
    {
        [DllImport("kernel32", ExactSpelling = true)]
        [return: NativeTypeName("DWORD")]
        public static extern uint WaitForSingleObjectEx(HANDLE hHandle, [NativeTypeName("DWORD")] uint dwMilliseconds, BOOL bAlertable);
    }
}
