using ComputeSharp.D2D1.Interop;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#pragma warning disable CA1822

namespace ComputeSharp.D2D1.Tests;

[TestClass]
public partial class ShaderRewriterTests
{
    // See https://github.com/Sergio0694/ComputeSharp/issues/725
    [TestMethod]
    public void ShaderWithStructAndInstanceMethodUsingIt_IsRewrittenCorrectly()
    {
        D2D1ShaderInfo shaderInfo = D2D1ReflectionServices.GetShaderInfo<ClassWithShader.ShaderWithStructAndInstanceMethodUsingIt>();

        Assert.AreEqual("""
            #define D2D_INPUT_COUNT 0

            #include "d2d1effecthelpers.hlsli"

            struct ComputeSharp_D2D1_Tests_ShaderRewriterTests_ClassWithShader_ShaderWithStructAndInstanceMethodUsingIt_Data
            {
                int value;
            };

            void UseData(inout ComputeSharp_D2D1_Tests_ShaderRewriterTests_ClassWithShader_ShaderWithStructAndInstanceMethodUsingIt_Data data);

            void UseData(inout ComputeSharp_D2D1_Tests_ShaderRewriterTests_ClassWithShader_ShaderWithStructAndInstanceMethodUsingIt_Data data)
            {
                ++data.value;
            }

            D2D_PS_ENTRY(Execute)
            {
                ComputeSharp_D2D1_Tests_ShaderRewriterTests_ClassWithShader_ShaderWithStructAndInstanceMethodUsingIt_Data data = (ComputeSharp_D2D1_Tests_ShaderRewriterTests_ClassWithShader_ShaderWithStructAndInstanceMethodUsingIt_Data)0;
                UseData(data);
                return float4(data.value, data.value, data.value, data.value);
            }
            """, shaderInfo.HlslSource);
    }

    internal sealed partial class ClassWithShader
    {
        [D2DInputCount(0)]
        [D2DShaderProfile(D2D1ShaderProfile.PixelShader50)]
        [D2DGeneratedPixelShaderDescriptor]
        internal readonly partial struct ShaderWithStructAndInstanceMethodUsingIt : ID2D1PixelShader
        {
            private struct Data
            {
                public int value;
            }

            public float4 Execute()
            {
                Data data = default;

                UseData(ref data);

                return new float4(data.value, data.value, data.value, data.value);
            }

            private void UseData(ref Data data)
            {
                ++data.value;
            }
        }
    }

    // See https://github.com/Sergio0694/ComputeSharp/issues/735
    [TestMethod]
    public void KnownNamedIntrinsic_ConditionalSelect()
    {
        D2D1ShaderInfo shaderInfo = D2D1ReflectionServices.GetShaderInfo<KnownNamedIntrinsic_ConditionalSelectShader>();

        Assert.AreEqual("""
            #define D2D_INPUT_COUNT 0

            #include "d2d1effecthelpers.hlsli"

            D2D_PS_ENTRY(Execute)
            {
                bool4 mask4 = bool4(true, false, true, true);
                float4 float4_1 = float4(1, 2, 3.14, 4);
                float4 float4_2 = float4(5, 6, 7, 8);
                float4 float4_r = (mask4 ? float4_1 : float4_2);
                bool1x3 mask1x3 = bool1x3((bool)true, (bool)true, (bool)false);
                int1x3 int1x3_1 = int1x3((int)1, (int)2, (int)3);
                int1x3 int1x3_2 = int1x3((int)4, (int)5, (int)6);
                int1x3 int1x3_r = (mask1x3 ? int1x3_1 : int1x3_2);
                bool2x4 mask2x4 = bool2x4((bool)true, (bool)true, (bool)false, (bool)true, (bool)false, (bool)false, (bool)false, (bool)true);
                uint2x4 uint2x4_1 = uint2x4((uint)1, (uint)2, (uint)3, (uint)4, (uint)5, (uint)6, (uint)7, (uint)8);
                uint2x4 uint2x4_2 = uint2x4((uint)111, (uint)222, (uint)333, (uint)444, (uint)555, (uint)666, (uint)777, (uint)888);
                uint2x4 uint2x4_r = (mask2x4 ? uint2x4_1 : uint2x4_2);
                float2x2 f2x2_r = mul((float2x4)uint2x4_r, float4x2((float2)float4_r.xy, (float2)float4_r.zw, (float2)float2(int1x3_r._m00, int1x3_r._m01), (float2)float2(int1x3_r._m02, 1.0)));
                return float4(f2x2_r._m00, f2x2_r._m01, f2x2_r._m10, f2x2_r._m11);
            }
            """, shaderInfo.HlslSource);
    }

    [D2DInputCount(0)]
    [D2DShaderProfile(D2D1ShaderProfile.PixelShader50)]
    [D2DGeneratedPixelShaderDescriptor]
    internal readonly partial struct KnownNamedIntrinsic_ConditionalSelectShader : ID2D1PixelShader
    {
        public float4 Execute()
        {
            bool4 mask4 = new(true, false, true, true);
            float4 float4_1 = new(1, 2, 3.14f, 4);
            float4 float4_2 = new(5, 6, 7, 8);

            float4 float4_r = Hlsl.ConditionalSelect(mask4, float4_1, float4_2);

            bool1x3 mask1x3 = new(true, true, false);
            int1x3 int1x3_1 = new(1, 2, 3);
            int1x3 int1x3_2 = new(4, 5, 6);

            int1x3 int1x3_r = Hlsl.ConditionalSelect(mask1x3, int1x3_1, int1x3_2);

            bool2x4 mask2x4 = new(true, true, false, true, false, false, false, true);
            uint2x4 uint2x4_1 = new(1, 2, 3, 4, 5, 6, 7, 8);
            uint2x4 uint2x4_2 = new(111, 222, 333, 444, 555, 666, 777, 888);

            uint2x4 uint2x4_r = Hlsl.ConditionalSelect(mask2x4, uint2x4_1, uint2x4_2);

            float2x2 f2x2_r = (float2x4)uint2x4_r * new float4x2(float4_r.XY, float4_r.ZW, new float2(int1x3_r.M11, int1x3_r.M12), new float2(int1x3_r.M13, 1.0f));

            return new(f2x2_r.M11, f2x2_r.M12, f2x2_r.M21, f2x2_r.M22);
        }
    }
}