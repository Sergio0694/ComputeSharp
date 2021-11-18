// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from d3dx12.h in the microsoft/DirectX-Graphics-Samples tag v10.0.19041.0
// Original source is Copyright © Microsoft. All rights reserved. Licensed under the MIT License (MIT).

using TerraFX.Interop.Windows;
using static TerraFX.Interop.DirectX.D3D12_FEATURE;

namespace TerraFX.Interop.DirectX
{
    internal static unsafe partial class DirectX
    {
        public static uint D3D12CalcSubresource(uint MipSlice, uint ArraySlice, uint PlaneSlice, uint MipLevels, uint ArraySize)
        {
            return MipSlice + ArraySlice * MipLevels + PlaneSlice * MipLevels * ArraySize;
        }

        [return: NativeTypeName("UINT8")]
        public static byte D3D12GetFormatPlaneCount(ID3D12Device* pDevice, DXGI_FORMAT Format)
        {
            D3D12_FEATURE_DATA_FORMAT_INFO formatInfo = new D3D12_FEATURE_DATA_FORMAT_INFO
            {
                Format = Format,
                PlaneCount = 0,
            };

            if ((((HRESULT)(pDevice->CheckFeatureSupport(D3D12_FEATURE_FORMAT_INFO, &formatInfo, (uint)(sizeof(D3D12_FEATURE_DATA_FORMAT_INFO))))) < 0))
            {
                return 0;
            }

            return formatInfo.PlaneCount;
        }
    }
}
