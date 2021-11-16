// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from d3dx12.h in DirectX-Graphics-Samples commit a7a87f1853b5540f10920518021d91ae641033fb
// Original source is Copyright © Microsoft. All rights reserved. Licensed under the MIT License (MIT).

namespace TerraFX.Interop.DirectX
{
    public unsafe partial struct D3D12_ROOT_DESCRIPTOR_TABLE1
    {
        public D3D12_ROOT_DESCRIPTOR_TABLE1(uint numDescriptorRanges, [NativeTypeName("const D3D12_DESCRIPTOR_RANGE1 *")] D3D12_DESCRIPTOR_RANGE1* _pDescriptorRanges)
        {
            Init(out this, numDescriptorRanges, _pDescriptorRanges);
        }

        public void Init(uint numDescriptorRanges, [NativeTypeName("const D3D12_DESCRIPTOR_RANGE1 *")] D3D12_DESCRIPTOR_RANGE1* _pDescriptorRanges)
        {
            Init(out this, numDescriptorRanges, _pDescriptorRanges);
        }

        public static void Init([NativeTypeName("D3D12_ROOT_DESCRIPTOR_TABLE &")] out D3D12_ROOT_DESCRIPTOR_TABLE1 rootDescriptorTable, uint numDescriptorRanges, [NativeTypeName("const D3D12_DESCRIPTOR_RANGE1 *")] D3D12_DESCRIPTOR_RANGE1* _pDescriptorRanges)
        {
            rootDescriptorTable.NumDescriptorRanges = numDescriptorRanges;
            rootDescriptorTable.pDescriptorRanges = _pDescriptorRanges;
        }
    }
}
