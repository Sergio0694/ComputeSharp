﻿// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from shared/winerror.h in the Windows SDK for Windows 10.0.22000.0
// Original source is Copyright © Microsoft. All rights reserved.

namespace TerraFX.Interop.Windows
{
    internal static class E
    {
        [NativeTypeName("#define E_NOTIMPL _HRESULT_TYPEDEF_(0x80004001L)")]
        public const int E_NOTIMPL = unchecked((int)(0x80004001));

        [NativeTypeName("#define E_INVALIDARG _HRESULT_TYPEDEF_(0x80070057L)")]
        public const int E_INVALIDARG = unchecked((int)(0x80070057));

        [NativeTypeName("#define E_NOINTERFACE _HRESULT_TYPEDEF_(0x80004002L)")]
        public const int E_NOINTERFACE = unchecked((int)(0x80004002));

        [NativeTypeName("#define E_POINTER _HRESULT_TYPEDEF_(0x80004003L)")]
        public const int E_POINTER = unchecked((int)(0x80004003));
    }
}