using System;
using ComputeSharp.D2D1.Exceptions;
using ComputeSharp.D2D1.Interop;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.D2D1.Tests;

[TestClass]
[TestCategory("D2D1ShaderCompiler")]
public partial class D2D1ShaderCompilerTests
{
    [TestMethod]
    public unsafe void CompileInvertEffectWithDefaultOptions()
    {
        const string source = @"
            #define D2D_INPUT_COUNT 1
            #define D2D_INPUT0_SIMPLE

            #include ""d2d1effecthelpers.hlsli""


            D2D_PS_ENTRY(PSMain)
            {
                float4 color = D2DGetInput(0);
                float3 rgb = saturate(1.0 - color.rgb);
                return float4(rgb, 1);
            }";

        ReadOnlyMemory<byte> bytecode = D2D1ShaderCompiler.Compile(
            source,
            "PSMain",
            D2D1ShaderProfile.PixelShader40Level93,
            D2D1CompileOptions.Default);

        Assert.IsTrue(bytecode.Length > 0);
    }

    [TestMethod]
    public unsafe void CompileInvertEffectWithDefaultOptionsAndLinking()
    {
        const string source = @"
            #define D2D_INPUT_COUNT 1
            #define D2D_INPUT0_SIMPLE

            #include ""d2d1effecthelpers.hlsli""


            D2D_PS_ENTRY(PSMain)
            {
                float4 color = D2DGetInput(0);
                float3 rgb = saturate(1.0 - color.rgb);
                return float4(rgb, 1);
            }";

        ReadOnlyMemory<byte> bytecode = D2D1ShaderCompiler.Compile(
            source,
            "PSMain",
            D2D1ShaderProfile.PixelShader40Level93,
            D2D1CompileOptions.Default | D2D1CompileOptions.EnableLinking);

        Assert.IsTrue(bytecode.Length > 0);
    }

    [TestMethod]
    [ExpectedException(typeof(FxcCompilationException))]
    public unsafe void CompileInvertEffectWithInvalidEntryPoint()
    {
        const string source = @"
            #define D2D_INPUT_COUNT 1
            #define D2D_INPUT0_SIMPLE

            #include ""d2d1effecthelpers.hlsli""


            D2D_PS_ENTRY(PSMain)
            {
                float4 color = D2DGetInput(0);
                float3 rgb = saturate(1.0 - color.rgb);
                return float4(rgb, 1);
            }";

        ReadOnlyMemory<byte> bytecode = D2D1ShaderCompiler.Compile(
            source,
            "Execute",
            D2D1ShaderProfile.PixelShader40Level93,
            D2D1CompileOptions.Default);

        Assert.IsTrue(bytecode.Length > 0);
    }

    [TestMethod]
    [ExpectedException(typeof(FxcCompilationException))]
    public unsafe void CompileInvertEffectWithErrors()
    {
        const string source = @"
            #define D2D_INPUT_COUNT 1
            #define D2D_INPUT0_SIMPLE

            #include ""d2d1effecthelpers.hlsli""


            D2D_PS_ENTRY(PSMain)
            {
                float4 color = D2DGetInput(99);
                float3 rgb2 = saturate(1.0 - color.rgb);
                return float4(rgb, 1, 4.0);
            }";

        ReadOnlyMemory<byte> bytecode = D2D1ShaderCompiler.Compile(
            source,
            "PSMain",
            D2D1ShaderProfile.PixelShader40Level93,
            D2D1CompileOptions.Default);

        Assert.IsTrue(bytecode.Length > 0);
    }

    [TestMethod]
    public unsafe void CompilePixelateEffectWithDefaultOptions()
    {
        const string source = @"
            #define D2D_INPUT_COUNT 1
            #define D2D_INPUT0_COMPLEX
            #define D2D_REQUIRES_SCENE_POSITION

            #include ""d2d1effecthelpers.hlsli""

            struct ComputeSharp_D2D1_Tests_Effects_PixelateEffect_Shader_Constants
            {
                int inputWidth;
                int inputHeight;
                int cellSize;
            };

            ComputeSharp_D2D1_Tests_Effects_PixelateEffect_Shader_Constants constants;

            D2D_PS_ENTRY(PSMain)
            {
                float2 scenePos = D2DGetScenePosition().xy;
                uint x = (uint)floor(scenePos.x);
                uint y = (uint)floor(scenePos.y);
                int cellX = (int)floor(x / constants.cellSize);
                int cellY = (int)floor(y / constants.cellSize);
                int x0 = cellX * constants.cellSize;
                int y0 = cellY * constants.cellSize;
                int x1 = min(constants.inputWidth, x0 + constants.cellSize) - 1;
                int y1 = min(constants.inputHeight, y0 + constants.cellSize) - 1;
                float4 sample0 = D2DSampleInputAtPosition(0, int2(x0, y0));
                float4 sample1 = D2DSampleInputAtPosition(0, int2(x1, y0));
                float4 sample2 = D2DSampleInputAtPosition(0, int2(x0, y1));
                float4 sample3 = D2DSampleInputAtPosition(0, int2(x1, y1));
                float4 color = (sample0 + sample1 + sample2 + sample3) / 4;
                return color;
            }";

        ReadOnlyMemory<byte> bytecode = D2D1ShaderCompiler.Compile(
            source,
            "PSMain",
            D2D1ShaderProfile.PixelShader40,
            D2D1CompileOptions.Default);

        Assert.IsTrue(bytecode.Length > 0);
    }
}