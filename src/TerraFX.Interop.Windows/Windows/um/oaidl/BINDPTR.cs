// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/oaidl.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System.Runtime.InteropServices;

namespace TerraFX.Interop.Windows
{
    [StructLayout(LayoutKind.Explicit)]
    public unsafe partial struct BINDPTR
    {
        [FieldOffset(0)]
        public FUNCDESC* lpfuncdesc;

        [FieldOffset(0)]
        public VARDESC* lpvardesc;

        [FieldOffset(0)]
        public ITypeComp* lptcomp;
    }
}
