// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d3d12.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using TerraFX.Interop.Windows;

namespace TerraFX.Interop.DirectX
{
    public partial struct D3D12_FEATURE_DATA_D3D12_OPTIONS1
    {
        public BOOL WaveOps;

        public uint WaveLaneCountMin;

        public uint WaveLaneCountMax;

        public uint TotalLaneCount;

        public BOOL ExpandedComputeResourceStates;

        public BOOL Int64ShaderOps;
    }
}
