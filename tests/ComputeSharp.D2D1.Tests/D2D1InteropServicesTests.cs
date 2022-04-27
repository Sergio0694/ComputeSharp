using ComputeSharp.D2D1.Interop;
using ComputeSharp.D2D1.Tests.Effects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.D2D1.Tests;

[TestClass]
[TestCategory("D2D1InteropServices")]
public class D2D1InteropServicesTests
{
    [TestMethod]
    public unsafe void GetInputCount()
    {
        Assert.AreEqual(D2D1InteropServices.GetPixelShaderInputCount<InvertEffect>(), 1u);
        Assert.AreEqual(D2D1InteropServices.GetPixelShaderInputCount<PixelateEffect.Shader>(), 1u);
        Assert.AreEqual(D2D1InteropServices.GetPixelShaderInputCount<ZonePlateEffect>(), 0u);
    }
}