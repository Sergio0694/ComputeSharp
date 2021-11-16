// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d3d12sdklayers.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using TerraFX.Interop.Windows;

namespace TerraFX.Interop.DirectX
{
    public unsafe partial struct D3D12_MESSAGE
    {
        public D3D12_MESSAGE_CATEGORY Category;

        public D3D12_MESSAGE_SEVERITY Severity;

        public D3D12_MESSAGE_ID ID;

        [NativeTypeName("const char *")]
        public sbyte* pDescription;

        [NativeTypeName("SIZE_T")]
        public nuint DescriptionByteLength;
    }
}
