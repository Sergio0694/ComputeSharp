// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from shared/winerror.h in the Windows SDK for Windows 10.0.22000.0
// Original source is Copyright © Microsoft. All rights reserved.

namespace ComputeSharp.Win32;

internal static class E
{
    [NativeTypeName("#define E_NOTFOUND HRESULT_FROM_WIN32(ERROR_NOT_FOUND)")]
    public const int E_NOTFOUND = -2147023728;

    [NativeTypeName("#define E_NOT_VALID_STATE HRESULT_FROM_WIN32(ERROR_INVALID_STATE)")]
    public const int E_NOT_VALID_STATE = -2147019873;

    [NativeTypeName("#define E_NOT_SUFFICIENT_BUFFER HRESULT_FROM_WIN32(ERROR_INSUFFICIENT_BUFFER)")]
    public const int E_NOT_SUFFICIENT_BUFFER = -2147024774;

    [NativeTypeName("#define E_NOTIMPL _HRESULT_TYPEDEF_(0x80004001L)")]
    public const int E_NOTIMPL = unchecked((int)(0x80004001));

    [NativeTypeName("#define E_OUTOFMEMORY _HRESULT_TYPEDEF_(0x8007000EL)")]
    public const int E_OUTOFMEMORY = unchecked((int)(0x8007000E));

    [NativeTypeName("#define E_INVALIDARG _HRESULT_TYPEDEF_(0x80070057L)")]
    public const int E_INVALIDARG = unchecked((int)(0x80070057));

    [NativeTypeName("#define E_NOINTERFACE _HRESULT_TYPEDEF_(0x80004002L)")]
    public const int E_NOINTERFACE = unchecked((int)(0x80004002));

    [NativeTypeName("#define E_POINTER _HRESULT_TYPEDEF_(0x80004003L)")]
    public const int E_POINTER = unchecked((int)(0x80004003));

    [NativeTypeName("#define E_FAIL _HRESULT_TYPEDEF_(0x80004005L)")]
    public const int E_FAIL = unchecked((int)(0x80004005));
}