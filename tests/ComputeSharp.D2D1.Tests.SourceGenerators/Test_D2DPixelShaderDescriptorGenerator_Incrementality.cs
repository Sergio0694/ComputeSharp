using ComputeSharp.D2D1.SourceGenerators;
using ComputeSharp.Tests.SourceGenerators.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.D2D1.Tests.SourceGenerators;

[TestClass]
public class Test_D2DPixelShaderDescriptorGenerator_Incrementality
{
    [TestMethod]
    public void ModifiedStatement_ModifiesOutput()
    {
        const string source = """
            using ComputeSharp;
            using ComputeSharp.D2D1;
            using float4 = global::ComputeSharp.Float4;

            [D2DInputCount(0)]
            [D2DGeneratedPixelShaderDescriptor]
            internal readonly partial struct MyShader : ID2D1PixelShader
            {
                public float4 Execute()
                {
                    return 0;
                }
            }
            """;

        const string updatedSource = """
            using ComputeSharp;
            using ComputeSharp.D2D1;
            using float4 = global::ComputeSharp.Float4;

            [D2DInputCount(0)]
            [D2DGeneratedPixelShaderDescriptor]
            internal readonly partial struct MyShader : ID2D1PixelShader
            {
                public float4 Execute()
                {
                    // Some dummy comment
                    return 42;
                }
            }
            """;

        CSharpGeneratorTest<D2DPixelShaderDescriptorGenerator>.VerifyIncrementalSteps(
            source,
            updatedSource,
            executeReason: IncrementalStepRunReason.Modified,
            diagnosticsReason: null,
            outputReason: IncrementalStepRunReason.Modified,
            diagnosticsSourceReason: null,
            sourceReason: IncrementalStepRunReason.Modified);
    }

    [TestMethod]
    public void AddedComment_DoesNotModifyOutput()
    {
        const string source = """
            using ComputeSharp;
            using ComputeSharp.D2D1;
            using float4 = global::ComputeSharp.Float4;

            [D2DInputCount(0)]
            [D2DGeneratedPixelShaderDescriptor]
            internal readonly partial struct MyShader : ID2D1PixelShader
            {
                public float4 Execute()
                {
                    return 0;
                }
            }
            """;

        const string updatedSource = """
            using ComputeSharp;
            using ComputeSharp.D2D1;
            using float4 = global::ComputeSharp.Float4;

            [D2DInputCount(0)]
            [D2DGeneratedPixelShaderDescriptor]
            internal readonly partial struct MyShader : ID2D1PixelShader
            {
                public float4 Execute()
                {
                    // Some dummy comment
                    return 0;
                }
            }
            """;

        CSharpGeneratorTest<D2DPixelShaderDescriptorGenerator>.VerifyIncrementalSteps(
            source,
            updatedSource,
            executeReason: IncrementalStepRunReason.Unchanged,
            diagnosticsReason: null,
            outputReason: IncrementalStepRunReason.Cached,
            diagnosticsSourceReason: null,
            sourceReason: IncrementalStepRunReason.Cached);
    }

    [TestMethod]
    public void AddedRequiresDoublePrecisionSupport_DoesNotModifyOutput()
    {
        const string source = """
            using ComputeSharp;
            using ComputeSharp.D2D1;
            using float4 = global::ComputeSharp.Float4;
            using double4 = global::ComputeSharp.Double4;

            [D2DInputCount(1)]
            [D2DShaderProfile(D2D1ShaderProfile.PixelShader50)]
            [D2DGeneratedPixelShaderDescriptor]
            internal readonly partial struct MyShader(double factor) : ID2D1PixelShader
            {
                public float4 Execute()
                {
                    return (float4)((double4)D2D.GetInput(0) * factor);
                }
            }
            """;

        const string updatedSource = """
            using ComputeSharp;
            using ComputeSharp.D2D1;
            using float4 = global::ComputeSharp.Float4;
            using double4 = global::ComputeSharp.Double4;

            [D2DInputCount(1)]
            [D2DRequiresDoublePrecisionSupport]
            [D2DShaderProfile(D2D1ShaderProfile.PixelShader50)]
            [D2DGeneratedPixelShaderDescriptor]
            internal readonly partial struct MyShader(double factor) : ID2D1PixelShader
            {
                public float4 Execute()
                {
                    return (float4)((double4)D2D.GetInput(0) * factor);
                }
            }
            """;

        CSharpGeneratorTest<D2DPixelShaderDescriptorGenerator>.VerifyIncrementalSteps(
            source,
            updatedSource,
            executeReason: IncrementalStepRunReason.Modified,
            diagnosticsReason: IncrementalStepRunReason.Modified,
            outputReason: IncrementalStepRunReason.Unchanged,
            diagnosticsSourceReason: IncrementalStepRunReason.Removed,
            sourceReason: IncrementalStepRunReason.Cached);
    }

    [TestMethod]
    public void RemovedRequiresDoublePrecisionSupport_DoesNotModifyOutputAndProducesDiagnostics()
    {
        const string source = """
            using ComputeSharp;
            using ComputeSharp.D2D1;
            using float4 = global::ComputeSharp.Float4;
            using double4 = global::ComputeSharp.Double4;

            [D2DInputCount(1)]
            [D2DRequiresDoublePrecisionSupport]
            [D2DShaderProfile(D2D1ShaderProfile.PixelShader50)]
            [D2DGeneratedPixelShaderDescriptor]
            internal readonly partial struct MyShader(double factor) : ID2D1PixelShader
            {
                public float4 Execute()
                {
                    return (float4)((double4)D2D.GetInput(0) * factor);
                }
            }
            """;

        const string updatedSource = """
            using ComputeSharp;
            using ComputeSharp.D2D1;
            using float4 = global::ComputeSharp.Float4;
            using double4 = global::ComputeSharp.Double4;

            [D2DInputCount(1)]
            [D2DShaderProfile(D2D1ShaderProfile.PixelShader50)]
            [D2DGeneratedPixelShaderDescriptor]
            internal readonly partial struct MyShader(double factor) : ID2D1PixelShader
            {
                public float4 Execute()
                {
                    return (float4)((double4)D2D.GetInput(0) * factor);
                }
            }
            """;

        CSharpGeneratorTest<D2DPixelShaderDescriptorGenerator>.VerifyIncrementalSteps(
            source,
            updatedSource,
            executeReason: IncrementalStepRunReason.Modified,
            diagnosticsReason: IncrementalStepRunReason.Modified,
            outputReason: IncrementalStepRunReason.Unchanged,
            diagnosticsSourceReason: IncrementalStepRunReason.New,
            sourceReason: IncrementalStepRunReason.Cached);
    }
}