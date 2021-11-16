// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from d3dx12.h in DirectX-Graphics-Samples commit a7a87f1853b5540f10920518021d91ae641033fb
// Original source is Copyright © Microsoft. All rights reserved. Licensed under the MIT License (MIT).

using static TerraFX.Interop.DirectX.D3D12;
using static TerraFX.Interop.DirectX.D3D12_RESOURCE_BARRIER_FLAGS;
using static TerraFX.Interop.DirectX.D3D12_RESOURCE_BARRIER_TYPE;

namespace TerraFX.Interop.DirectX
{
    public unsafe partial struct D3D12_RESOURCE_BARRIER
    {
        public static D3D12_RESOURCE_BARRIER InitTransition(ID3D12Resource* pResource, D3D12_RESOURCE_STATES stateBefore, D3D12_RESOURCE_STATES stateAfter, uint subresource = D3D12_RESOURCE_BARRIER_ALL_SUBRESOURCES, D3D12_RESOURCE_BARRIER_FLAGS flags = D3D12_RESOURCE_BARRIER_FLAG_NONE)
        {
            D3D12_RESOURCE_BARRIER result = default;
            result.Type = D3D12_RESOURCE_BARRIER_TYPE_TRANSITION;
            result.Flags = flags;
            result.Anonymous.Transition.pResource = pResource;
            result.Anonymous.Transition.StateBefore = stateBefore;
            result.Anonymous.Transition.StateAfter = stateAfter;
            result.Anonymous.Transition.Subresource = subresource;
            return result;
        }

        public static D3D12_RESOURCE_BARRIER InitAliasing(ID3D12Resource* pResourceBefore, ID3D12Resource* pResourceAfter)
        {
            D3D12_RESOURCE_BARRIER result = default;
            result.Type = D3D12_RESOURCE_BARRIER_TYPE_ALIASING;
            result.Anonymous.Aliasing.pResourceBefore = pResourceBefore;
            result.Anonymous.Aliasing.pResourceAfter = pResourceAfter;
            return result;
        }

        public static D3D12_RESOURCE_BARRIER InitUAV(ID3D12Resource* pResource)
        {
            D3D12_RESOURCE_BARRIER result = default;
            result.Type = D3D12_RESOURCE_BARRIER_TYPE_UAV;
            result.Anonymous.UAV.pResource = pResource;
            return result;
        }
    }
}
