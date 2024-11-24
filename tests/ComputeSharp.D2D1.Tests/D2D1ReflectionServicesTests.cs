using ComputeSharp.D2D1.Interop;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.D2D1.Tests;

[TestClass]
public partial class D2D1ReflectionServicesTests
{
    [TestMethod]
    public void GetShaderInfo()
    {
        D2D1ShaderInfo shaderInfo = D2D1ReflectionServices.GetShaderInfo<ReflectedShader>();

        Assert.IsNotNull(shaderInfo);
        Assert.AreEqual(10u, shaderInfo.BoundResourceCount);
        Assert.AreEqual(5u, shaderInfo.DeclarationCount);
        Assert.IsFalse(shaderInfo.RequiresDoublePrecisionSupport);
        Assert.AreEqual(D3DFeatureLevel.FeatureLevel10_1, shaderInfo.MinimumFeatureLevel);
        Assert.IsNotNull(shaderInfo.CompilerVersion);

        Assert.AreEqual("""
            #define D2D_INPUT_COUNT 3
            #define D2D_INPUT0_SIMPLE
            #define D2D_INPUT1_SIMPLE
            #define D2D_INPUT2_COMPLEX
            #define D2D_REQUIRES_SCENE_POSITION

            #include "d2d1effecthelpers.hlsli"

            float2 offset;

            Texture1D<float> __reserved__buffer : register(t3);
            SamplerState __sampler____reserved__buffer : register(s3);

            Texture2D<float4> __reserved__texture : register(t4);
            SamplerState __sampler____reserved__texture : register(s4);

            D2D_PS_ENTRY(Execute)
            {
                int2 xy = (int2)D2DGetScenePosition().xy;
                float4 input0 = D2DGetInput(0);
                float4 input1 = D2DGetInput(1);
                float4 input2 = D2DSampleInputAtPosition(2, xy + offset);
                float value3 = __reserved__buffer[0];
                float4 value4 = __reserved__texture.Sample(__sampler____reserved__texture, float2(0, asfloat(1056964608U)));
                return input0 + input1 + input2 + value3 + value4;
            }
            """, shaderInfo.HlslSource);

        CollectionAssert.AreEqual(D2D1PixelShader.LoadBytecode<ReflectedShader>().ToArray(), shaderInfo.HlslBytecode.ToArray());
    }

    [D2DInputCount(3)]
    [D2DInputSimple(0)]
    [D2DInputSimple(1)]
    [D2DInputComplex(2)]
    [D2DRequiresScenePosition]
    [D2DShaderProfile(D2D1ShaderProfile.PixelShader41)]
    [D2DGeneratedPixelShaderDescriptor]
    [AutoConstructor]
    internal readonly partial struct ReflectedShader : ID2D1PixelShader
    {
        private readonly float2 offset;

        [D2DResourceTextureIndex(3)]
        private readonly D2D1ResourceTexture1D<float> buffer;

        [D2DResourceTextureIndex(4)]
        private readonly D2D1ResourceTexture2D<float4> texture;

        public float4 Execute()
        {
            int2 xy = (int2)D2D.GetScenePosition().XY;

            float4 input0 = D2D.GetInput(0);
            float4 input1 = D2D.GetInput(1);
            float4 input2 = D2D.SampleInputAtPosition(2, xy + this.offset);

            float value3 = this.buffer[0];
            float4 value4 = this.texture.Sample(0, 0.5f);

            return input0 + input1 + input2 + value3 + value4;
        }
    }

    [TestMethod]
    public void GetShaderInfoWithDoublePrecisionFeature()
    {
        D2D1ShaderInfo shaderInfo = D2D1ReflectionServices.GetShaderInfo<ReflectedShaderWithDoubleOperations>();

        Assert.IsNotNull(shaderInfo);
        Assert.AreEqual(3u, shaderInfo.BoundResourceCount);
        Assert.AreEqual(1u, shaderInfo.ConstantBufferCount);
        Assert.AreEqual(2u, shaderInfo.DeclarationCount);
        Assert.IsTrue(shaderInfo.RequiresDoublePrecisionSupport);
        Assert.AreEqual(D3DFeatureLevel.FeatureLevel11_0, shaderInfo.MinimumFeatureLevel);
        Assert.IsNotNull(shaderInfo.CompilerVersion);

        Assert.AreEqual("""
            #define D2D_INPUT_COUNT 1
            #define D2D_INPUT0_SIMPLE

            #include "d2d1effecthelpers.hlsli"

            double amount;

            D2D_PS_ENTRY(Execute)
            {
                return (float4)(D2DGetInput(0) + (double4)amount);
            }
            """, shaderInfo.HlslSource);

        CollectionAssert.AreEqual(D2D1PixelShader.LoadBytecode<ReflectedShaderWithDoubleOperations>().ToArray(), shaderInfo.HlslBytecode.ToArray());
    }

    [D2DInputCount(1)]
    [D2DInputSimple(0)]
    [D2DRequiresDoublePrecisionSupport]
    [D2DShaderProfile(D2D1ShaderProfile.PixelShader50)]
    [D2DGeneratedPixelShaderDescriptor]
    [AutoConstructor]
    internal readonly partial struct ReflectedShaderWithDoubleOperations : ID2D1PixelShader
    {
        private readonly double amount;

        public float4 Execute()
        {
            return (float4)(D2D.GetInput(0) + (double4)this.amount);
        }
    }
}