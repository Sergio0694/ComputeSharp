using ComputeSharp.D2D1.Interop;
using ComputeSharp.D2D1.Tests.Effects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.D2D1.Tests;

[TestClass]
[TestCategory("D2D1InteropServices")]
public partial class D2D1InteropServicesTests
{
    [TestMethod]
    public unsafe void GetInputCount()
    {
        Assert.AreEqual(D2D1InteropServices.GetPixelShaderInputCount<InvertEffect>(), 1u);
        Assert.AreEqual(D2D1InteropServices.GetPixelShaderInputCount<PixelateEffect.Shader>(), 1u);
        Assert.AreEqual(D2D1InteropServices.GetPixelShaderInputCount<ZonePlateEffect>(), 0u);
        Assert.AreEqual(D2D1InteropServices.GetPixelShaderInputCount<ShaderWithMultipleInputs>(), 7u);
    }

    [TestMethod]
    public unsafe void GetInputType()
    {
        Assert.AreEqual(D2D1InteropServices.GetPixelShaderInputType<ShaderWithMultipleInputs>(0), D2D1PixelShaderInputType.Simple);
        Assert.AreEqual(D2D1InteropServices.GetPixelShaderInputType<ShaderWithMultipleInputs>(1), D2D1PixelShaderInputType.Complex);
        Assert.AreEqual(D2D1InteropServices.GetPixelShaderInputType<ShaderWithMultipleInputs>(2), D2D1PixelShaderInputType.Simple);
        Assert.AreEqual(D2D1InteropServices.GetPixelShaderInputType<ShaderWithMultipleInputs>(3), D2D1PixelShaderInputType.Complex);
        Assert.AreEqual(D2D1InteropServices.GetPixelShaderInputType<ShaderWithMultipleInputs>(4), D2D1PixelShaderInputType.Complex);
        Assert.AreEqual(D2D1InteropServices.GetPixelShaderInputType<ShaderWithMultipleInputs>(5), D2D1PixelShaderInputType.Complex);
    }

    [D2DInputCount(7)]
    [D2DInputSimple(0)]
    [D2DInputSimple(2)]
    [D2DInputComplex(1)]
    [D2DInputComplex(3)]
    [D2DInputComplex(5)]
    [D2DInputSimple(6)]
    partial struct ShaderWithMultipleInputs : ID2D1PixelShader
    {
        public Float4 Execute()
        {
            return 0;
        }
    }
}