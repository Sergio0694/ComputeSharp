using ComputeSharp.D2D1Interop.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.D2D1Interop.Tests;

[TestClass]
[TestCategory("EndToEnd")]
public class EndToEndTests
{
    [TestMethod]
    public unsafe void Test()
    {
    }
}

[D2DInputCount(1)]
[D2DInputSimple(0)]
[D2DEmbeddedBytecode(D2D1ShaderProfile.PixelShader50)]
[AutoConstructor]
public partial struct InvertShader : ID2D1PixelShader
{
    public float number;

    public float4 Execute()
    {
        float4 color = D2D1.GetInput(0);
        float3 rgb = Hlsl.Saturate(this.number - color.RGB);

        return new(rgb, 1);
    }
}