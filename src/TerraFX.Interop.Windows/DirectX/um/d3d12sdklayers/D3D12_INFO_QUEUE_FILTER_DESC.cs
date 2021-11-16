// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d3d12sdklayers.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

namespace TerraFX.Interop.DirectX
{
    public unsafe partial struct D3D12_INFO_QUEUE_FILTER_DESC
    {
        public uint NumCategories;

        public D3D12_MESSAGE_CATEGORY* pCategoryList;

        public uint NumSeverities;

        public D3D12_MESSAGE_SEVERITY* pSeverityList;

        public uint NumIDs;

        public D3D12_MESSAGE_ID* pIDList;
    }
}
