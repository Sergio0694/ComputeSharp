using ComputeSharp.SourceGenerators;
using ComputeSharp.Tests.SourceGenerators.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests.SourceGenerators;

[TestClass]
public class Test_ComputeShaderDescriptorGenerator_Incrementality
{
    [TestMethod]
    public void AddedStatement_ModifiesOutput()
    {
        const string source = """
            using ComputeSharp;

            [ThreadGroupSize(DefaultThreadGroupSizes.X)]
            [GeneratedComputeShaderDescriptor]
            internal readonly partial struct MyShader : IComputeShader
            {
                private readonly ReadWriteBuffer<float> buffer;

                public void Execute()
                {
                }
            }
            """;

        const string updatedSource = """
            using ComputeSharp;

            [ThreadGroupSize(DefaultThreadGroupSizes.X)]
            [GeneratedComputeShaderDescriptor]
            internal readonly partial struct MyShader : IComputeShader
            {
                private readonly ReadWriteBuffer<float> buffer;

                public void Execute()
                {
                    buffer[ThreadIds.X] *= 2.0;
                }
            }
            """;

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyIncrementalSteps(
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

            [ThreadGroupSize(DefaultThreadGroupSizes.X)]
            [GeneratedComputeShaderDescriptor]
            internal readonly partial struct MyShader : IComputeShader
            {
                private readonly ReadWriteBuffer<float> buffer;

                public void Execute()
                {
                }
            }
            """;

        const string updatedSource = """
            using ComputeSharp;

            [ThreadGroupSize(DefaultThreadGroupSizes.X)]
            [GeneratedComputeShaderDescriptor]
            internal readonly partial struct MyShader : IComputeShader
            {
                private readonly ReadWriteBuffer<float> buffer;

                public void Execute()
                {
                    // Some dummy comment
                }
            }
            """;

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyIncrementalSteps(
            source,
            updatedSource,
            executeReason: IncrementalStepRunReason.Unchanged,
            diagnosticsReason: null,
            outputReason: IncrementalStepRunReason.Cached,
            diagnosticsSourceReason: null,
            sourceReason: IncrementalStepRunReason.Cached);
    }
}