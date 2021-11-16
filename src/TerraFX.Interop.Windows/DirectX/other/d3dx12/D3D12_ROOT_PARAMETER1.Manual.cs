// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from d3dx12.h in DirectX-Graphics-Samples commit a7a87f1853b5540f10920518021d91ae641033fb
// Original source is Copyright © Microsoft. All rights reserved. Licensed under the MIT License (MIT).

using static TerraFX.Interop.DirectX.D3D12_ROOT_PARAMETER_TYPE;
using static TerraFX.Interop.DirectX.D3D12_SHADER_VISIBILITY;

namespace TerraFX.Interop.DirectX
{
    public unsafe partial struct D3D12_ROOT_PARAMETER1
    {
        public static void InitAsDescriptorTable([NativeTypeName("D3D12_ROOT_PARAMETER1 &")] out D3D12_ROOT_PARAMETER1 rootParam, uint numDescriptorRanges, [NativeTypeName("const D3D12_DESCRIPTOR_RANGE1 *")] D3D12_DESCRIPTOR_RANGE1* pDescriptorRanges, D3D12_SHADER_VISIBILITY visibility = D3D12_SHADER_VISIBILITY_ALL)
        {
            rootParam = default;

            rootParam.ParameterType = D3D12_ROOT_PARAMETER_TYPE_DESCRIPTOR_TABLE;
            rootParam.ShaderVisibility = visibility;
            D3D12_ROOT_DESCRIPTOR_TABLE1.Init(out rootParam.Anonymous.DescriptorTable, numDescriptorRanges, pDescriptorRanges);
        }

        public static void InitAsConstants([NativeTypeName("D3D12_ROOT_PARAMETER1 &")] out D3D12_ROOT_PARAMETER1 rootParam, uint num32BitValues, uint shaderRegister, uint registerSpace = 0, D3D12_SHADER_VISIBILITY visibility = D3D12_SHADER_VISIBILITY_ALL)
        {
            rootParam = default;

            rootParam.ParameterType = D3D12_ROOT_PARAMETER_TYPE_32BIT_CONSTANTS;
            rootParam.ShaderVisibility = visibility;
            D3D12_ROOT_CONSTANTS.Init(out rootParam.Anonymous.Constants, num32BitValues, shaderRegister, registerSpace);
        }

        public static void InitAsConstantBufferView([NativeTypeName("D3D12_ROOT_PARAMETER1 &")] out D3D12_ROOT_PARAMETER1 rootParam, uint shaderRegister, uint registerSpace = 0, D3D12_SHADER_VISIBILITY visibility = D3D12_SHADER_VISIBILITY_ALL)
        {
            rootParam = default;

            rootParam.ParameterType = D3D12_ROOT_PARAMETER_TYPE_CBV;
            rootParam.ShaderVisibility = visibility;
            D3D12_ROOT_DESCRIPTOR1.Init(out rootParam.Anonymous.Descriptor, shaderRegister, registerSpace);
        }

        public static void InitAsShaderResourceView([NativeTypeName("D3D12_ROOT_PARAMETER1 &")] out D3D12_ROOT_PARAMETER1 rootParam, uint shaderRegister, uint registerSpace = 0, D3D12_SHADER_VISIBILITY visibility = D3D12_SHADER_VISIBILITY_ALL)
        {
            rootParam = default;

            rootParam.ParameterType = D3D12_ROOT_PARAMETER_TYPE_SRV;
            rootParam.ShaderVisibility = visibility;
            D3D12_ROOT_DESCRIPTOR1.Init(out rootParam.Anonymous.Descriptor, shaderRegister, registerSpace);
        }

        public static void InitAsUnorderedAccessView([NativeTypeName("D3D12_ROOT_PARAMETER1 &")] out D3D12_ROOT_PARAMETER1 rootParam, uint shaderRegister, uint registerSpace = 0, D3D12_SHADER_VISIBILITY visibility = D3D12_SHADER_VISIBILITY_ALL)
        {
            rootParam = default;

            rootParam.ParameterType = D3D12_ROOT_PARAMETER_TYPE_UAV;
            rootParam.ShaderVisibility = visibility;
            D3D12_ROOT_DESCRIPTOR1.Init(out rootParam.Anonymous.Descriptor, shaderRegister, registerSpace);
        }

        public void InitAsDescriptorTable(uint numDescriptorRanges, [NativeTypeName("const D3D12_DESCRIPTOR_RANGE1 *")] D3D12_DESCRIPTOR_RANGE1* pDescriptorRanges, D3D12_SHADER_VISIBILITY visibility = D3D12_SHADER_VISIBILITY_ALL)
        {
            InitAsDescriptorTable(out this, numDescriptorRanges, pDescriptorRanges, visibility);
        }

        public void InitAsConstants(uint num32BitValues, uint shaderRegister, uint registerSpace = 0, D3D12_SHADER_VISIBILITY visibility = D3D12_SHADER_VISIBILITY_ALL)
        {
            InitAsConstants(out this, num32BitValues, shaderRegister, registerSpace, visibility);
        }

        public void InitAsConstantBufferView(uint shaderRegister, uint registerSpace = 0, D3D12_SHADER_VISIBILITY visibility = D3D12_SHADER_VISIBILITY_ALL)
        {
            InitAsConstantBufferView(out this, shaderRegister, registerSpace, visibility);
        }

        public void InitAsShaderResourceView(uint shaderRegister, uint registerSpace = 0, D3D12_SHADER_VISIBILITY visibility = D3D12_SHADER_VISIBILITY_ALL)
        {
            InitAsShaderResourceView(out this, shaderRegister, registerSpace, visibility);
        }

        public void InitAsUnorderedAccessView(uint shaderRegister, uint registerSpace = 0, D3D12_SHADER_VISIBILITY visibility = D3D12_SHADER_VISIBILITY_ALL)
        {
            InitAsUnorderedAccessView(out this, shaderRegister, registerSpace, visibility);
        }
    }
}
