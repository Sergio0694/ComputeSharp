// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from shared/dxgi.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using TerraFX.Interop.Windows;

namespace TerraFX.Interop.DirectX
{
    public unsafe partial struct DXGI_ADAPTER_DESC
    {
        [NativeTypeName("WCHAR [128]")]
        public fixed ushort Description[128];

        public uint VendorId;

        public uint DeviceId;

        public uint SubSysId;

        public uint Revision;

        [NativeTypeName("SIZE_T")]
        public nuint DedicatedVideoMemory;

        [NativeTypeName("SIZE_T")]
        public nuint DedicatedSystemMemory;

        [NativeTypeName("SIZE_T")]
        public nuint SharedSystemMemory;

        public LUID AdapterLuid;
    }
}
