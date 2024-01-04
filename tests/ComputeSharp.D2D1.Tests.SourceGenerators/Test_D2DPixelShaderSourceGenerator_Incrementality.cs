using ComputeSharp.D2D1.SourceGenerators;
using ComputeSharp.Tests.SourceGenerators.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.D2D1.Tests.SourceGenerators;

[TestClass]
public class Test_D2DPixelShaderSourceGenerator_Incrementality
{
    [TestMethod]
    public void ModifiedOptions_ModifiesOutput()
    {
        const string source = """"
            using System;
            using ComputeSharp;
            using ComputeSharp.D2D1;

            partial class C
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
                static partial ReadOnlySpan<byte> MyShader();
            }
            """";

        const string updatedSource = """"
            using System;
            using ComputeSharp;
            using ComputeSharp.D2D1;

            partial class C
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
                [D2DCompileOptions(D2D1CompileOptions.OptimizationLevel1)]
                static partial ReadOnlySpan<byte> MyShader();
            }
            """";

        CSharpGeneratorTest<D2DPixelShaderSourceGenerator>.VerifyIncrementalSteps(
            source,
            updatedSource,
            executeReason: IncrementalStepRunReason.Modified,
            diagnosticsReason: null,
            outputReason: IncrementalStepRunReason.Modified,
            diagnosticsSourceReason: null,
            sourceReason: IncrementalStepRunReason.Modified);
    }

    [TestMethod]
    public void AddedLeadingTrivia_DoesNotModifyOutput()
    {
        const string source = """"
            using System;
            using ComputeSharp;
            using ComputeSharp.D2D1;

            partial class C
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
                static partial ReadOnlySpan<byte> MyShader();
            }
            """";

        const string updatedSource = """"
            using System;
            using ComputeSharp;
            using ComputeSharp.D2D1;

            // Useless comment
            partial class C
            {
                // Useless method
                public static int AddOne(int x) => x + 1;

                // Hello
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
                static partial ReadOnlySpan<byte> MyShader();
            }
            """";

        CSharpGeneratorTest<D2DPixelShaderSourceGenerator>.VerifyIncrementalSteps(
            source,
            updatedSource,
            executeReason: IncrementalStepRunReason.Unchanged,
            diagnosticsReason: null,
            outputReason: IncrementalStepRunReason.Cached,
            diagnosticsSourceReason: null,
            sourceReason: IncrementalStepRunReason.Cached);
    }

    [TestMethod]
    public void AddedCompileError_ModifiesOutputAndProducesDiagnostics()
    {
        const string source = """"
            using System;
            using ComputeSharp;
            using ComputeSharp.D2D1;

            partial class C
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
                static partial ReadOnlySpan<byte> MyShader();
            }
            """";

        const string updatedSource = """"
            using System;
            using ComputeSharp;
            using ComputeSharp.D2D1;

            partial class C
            {
                [D2DPixelShaderSource("""
                    #define D2D_INPUT_COUNT 0
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
                static partial ReadOnlySpan<byte> MyShader();
            }
            """";

        CSharpGeneratorTest<D2DPixelShaderSourceGenerator>.VerifyIncrementalSteps(
            source,
            updatedSource,
            executeReason: IncrementalStepRunReason.Modified,
            diagnosticsReason: IncrementalStepRunReason.Modified,
            outputReason: IncrementalStepRunReason.Modified,
            diagnosticsSourceReason: IncrementalStepRunReason.New,
            sourceReason: IncrementalStepRunReason.Modified);
    }

    [TestMethod]
    public void FixedCompileError_ModifiesOutputAndRemovesDiagnostics()
    {
        const string source = """"
            using System;
            using ComputeSharp;
            using ComputeSharp.D2D1;

            partial class C
            {
                [D2DPixelShaderSource("""
                    #define D2D_INPUT_COUNT 0
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
                static partial ReadOnlySpan<byte> MyShader();
            }
            """";

        const string updatedSource = """"
            using System;
            using ComputeSharp;
            using ComputeSharp.D2D1;

            partial class C
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
                static partial ReadOnlySpan<byte> MyShader();
            }
            """";

        CSharpGeneratorTest<D2DPixelShaderSourceGenerator>.VerifyIncrementalSteps(
            source,
            updatedSource,
            executeReason: IncrementalStepRunReason.Modified,
            diagnosticsReason: IncrementalStepRunReason.Modified,
            outputReason: IncrementalStepRunReason.Modified,
            diagnosticsSourceReason: IncrementalStepRunReason.Removed,
            sourceReason: IncrementalStepRunReason.Modified);
    }
}