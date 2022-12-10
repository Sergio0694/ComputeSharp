// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

namespace TerraFX.Interop.WinRT;

internal readonly unsafe partial struct HSTRING
{
    public readonly void* Value;

    public HSTRING(void* value)
    {
        Value = value;
    }
}