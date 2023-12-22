using System;
using ComputeSharp.D2D1.Interop;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.D2D1.Tests;

[TestClass]
public partial class D2D1ShaderCompilerTests
{
    [TestMethod]
    public void CompileInvertEffectWithDefaultOptions()
    {
        const string source = """
            #define D2D_INPUT_COUNT 1
            #define D2D_INPUT0_SIMPLE

            #include "d2d1effecthelpers.hlsli"

            D2D_PS_ENTRY(PSMain)
            {
                float4 color = D2DGetInput(0);
                float3 rgb = saturate(1.0 - color.rgb);
                return float4(rgb, 1);
            }
            """;

        ReadOnlyMemory<byte> bytecode = D2D1ShaderCompiler.Compile(
            source.AsSpan(),
            "PSMain".AsSpan(),
            D2D1ShaderProfile.PixelShader40Level93,
            D2D1CompileOptions.Default);

        Assert.IsTrue(bytecode.Length > 0);
    }

    [TestMethod]
    public void CompileInvertEffectWithDefaultOptions_Utf8()
    {
        ReadOnlySpan<byte> sourceUtf8 = """
            #define D2D_INPUT_COUNT 1
            #define D2D_INPUT0_SIMPLE

            #include "d2d1effecthelpers.hlsli"

            D2D_PS_ENTRY(PSMain)
            {
                float4 color = D2DGetInput(0);
                float3 rgb = saturate(1.0 - color.rgb);
                return float4(rgb, 1);
            }
            """u8;

        ReadOnlyMemory<byte> bytecode = D2D1ShaderCompiler.Compile(
            sourceUtf8,
            "PSMain"u8,
            D2D1ShaderProfile.PixelShader40Level93,
            D2D1CompileOptions.Default);

        Assert.IsTrue(bytecode.Length > 0);
    }

    [TestMethod]
    public void CompileInvertEffectWithDefaultOptionsAndLinking()
    {
        const string source = """
            #define D2D_INPUT_COUNT 1
            #define D2D_INPUT0_SIMPLE

            #include "d2d1effecthelpers.hlsli"

            D2D_PS_ENTRY(PSMain)
            {
                float4 color = D2DGetInput(0);
                float3 rgb = saturate(1.0 - color.rgb);
                return float4(rgb, 1);
            }
            """;

        ReadOnlyMemory<byte> bytecode = D2D1ShaderCompiler.Compile(
            source.AsSpan(),
            "PSMain".AsSpan(),
            D2D1ShaderProfile.PixelShader40Level93,
            D2D1CompileOptions.Default);

        ReadOnlyMemory<byte> bytecodeWithLinking = D2D1ShaderCompiler.Compile(
            source.AsSpan(),
            "PSMain".AsSpan(),
            D2D1ShaderProfile.PixelShader40Level93,
            D2D1CompileOptions.Default | D2D1CompileOptions.EnableLinking);

        Assert.IsTrue(bytecode.Length > 800);
        Assert.IsTrue(bytecodeWithLinking.Length > 1600);
        Assert.IsTrue(bytecodeWithLinking.Length > bytecode.Length);
        Assert.IsTrue((bytecodeWithLinking.Length - bytecode.Length) > 800);
    }

    [TestMethod]
    public void CompileInvertEffectWithDefaultOptionsAndStripReflectionData()
    {
        const string source = """
            #define D2D_INPUT_COUNT 1
            #define D2D_INPUT0_SIMPLE

            #include "d2d1effecthelpers.hlsli"

            D2D_PS_ENTRY(PSMain)
            {
                float4 color = D2DGetInput(0);
                float3 rgb = saturate(1.0 - color.rgb);
                return float4(rgb, 1);
            }
            """;

        ReadOnlyMemory<byte> bytecode = D2D1ShaderCompiler.Compile(
            source.AsSpan(),
            "PSMain".AsSpan(),
            D2D1ShaderProfile.PixelShader40Level93,
            D2D1CompileOptions.Default);

        ReadOnlyMemory<byte> bytecodeWithStripReflectionData = D2D1ShaderCompiler.Compile(
            source.AsSpan(),
            "PSMain".AsSpan(),
            D2D1ShaderProfile.PixelShader40Level93,
            D2D1CompileOptions.Default | D2D1CompileOptions.StripReflectionData);

        Assert.IsTrue(bytecode.Length > 800);
        Assert.IsTrue(bytecodeWithStripReflectionData.Length > 500);
        Assert.IsTrue(bytecode.Length > bytecodeWithStripReflectionData.Length);
        Assert.IsTrue((bytecode.Length - bytecodeWithStripReflectionData.Length) > 200);
    }

    [TestMethod]
    [ExpectedException(typeof(FxcCompilationException))]
    public void CompileInvertEffectWithInvalidEntryPoint()
    {
        const string source = """
            #define D2D_INPUT_COUNT 1
            #define D2D_INPUT0_SIMPLE

            #include "d2d1effecthelpers.hlsli"

            D2D_PS_ENTRY(PSMain)
            {
                float4 color = D2DGetInput(0);
                float3 rgb = saturate(1.0 - color.rgb);
                return float4(rgb, 1);
            }
            """;

        _ = D2D1ShaderCompiler.Compile(
            source.AsSpan(),
            "Execute".AsSpan(),
            D2D1ShaderProfile.PixelShader40Level93,
            D2D1CompileOptions.Default);

        Assert.Fail();
    }

    [TestMethod]
    [ExpectedException(typeof(FxcCompilationException))]
    public void CompileInvertEffectWithErrors()
    {
        const string source = """
            #define D2D_INPUT_COUNT 1
            #define D2D_INPUT0_SIMPLE

            #include "d2d1effecthelpers.hlsli"

            D2D_PS_ENTRY(PSMain)
            {
                float4 color = D2DGetInput(99);
                float3 rgb2 = saturate(1.0 - color.rgb);
                return float4(rgb, 1, 4.0);
            }
            """;

        _ = D2D1ShaderCompiler.Compile(
            source.AsSpan(),
            "PSMain".AsSpan(),
            D2D1ShaderProfile.PixelShader40Level93,
            D2D1CompileOptions.Default);

        Assert.Fail();
    }

    [TestMethod]
    public void CompilePixelateEffectWithDefaultOptions()
    {
        const string source = """
            #define D2D_INPUT_COUNT 1
            #define D2D_INPUT0_COMPLEX
            #define D2D_REQUIRES_SCENE_POSITION

            #include "d2d1effecthelpers.hlsli"

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
            }
            """;

        ReadOnlyMemory<byte> bytecode = D2D1ShaderCompiler.Compile(
            source.AsSpan(),
            "PSMain".AsSpan(),
            D2D1ShaderProfile.PixelShader40,
            D2D1CompileOptions.Default);

        Assert.IsTrue(bytecode.Length > 0);
    }

    [TestMethod]
    [ExpectedException(typeof(FxcCompilationException))]
    public void CompileShaderWithWarning()
    {
        const string source = """
            #define D2D_INPUT_COUNT 0

            #include "d2d1effecthelpers.hlsli"

            int a;

            D2D_PS_ENTRY(Execute)
            {
                return a / 4;
            }
            """;

        _ = D2D1ShaderCompiler.Compile(
            source.AsSpan(),
            "Execute".AsSpan(),
            D2D1ShaderProfile.PixelShader40,
            D2D1CompileOptions.Default);

        Assert.Fail();
    }

    // See https://github.com/Sergio0694/ComputeSharp/issues/647
    [TestMethod]
    public void CompileShaderWithWarning_Suppressed()
    {
        const string source = """
            #define D2D_INPUT_COUNT 0

            #include "d2d1effecthelpers.hlsli"

            int a;

            D2D_PS_ENTRY(Execute)
            {
                return a / 4;
            }
            """;

        ReadOnlyMemory<byte> bytecode = D2D1ShaderCompiler.Compile(
            source.AsSpan(),
            "Execute".AsSpan(),
            D2D1ShaderProfile.PixelShader40,
            D2D1CompileOptions.Default & ~D2D1CompileOptions.WarningsAreErrors);

        Assert.IsTrue(bytecode.Length > 0);
    }
}