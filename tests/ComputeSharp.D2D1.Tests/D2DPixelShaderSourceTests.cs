using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.D2D1.Tests;

[TestClass]
public partial class D2DPixelShaderSourceTests
{
    [D2DPixelShaderSource("""
        #define D2D_INPUT_COUNT 1
        #define D2D_INPUT0_SIMPLE

        #include "d2d1effecthelpers.hlsli"

        D2D_PS_ENTRY(Execute)
        {
            float4 color = D2DGetInput(0);
            float3 rgb = saturate(1.0 - color.rgb);
            return float4(rgb, 1);
        }
        """)]
    [D2DShaderProfile(D2D1ShaderProfile.PixelShader50)]
    [D2DCompileOptions(D2D1CompileOptions.OptimizationLevel0)]
    private static partial ReadOnlySpan<byte> InvertEffect();

    [TestMethod]
    public void VerifyInvertEffectBytecode()
    {
        ReadOnlySpan<byte> bytecode = InvertEffect();

        Assert.IsTrue(bytecode.Length > 0);
    }
}