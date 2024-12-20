using System.Threading.Tasks;
using ComputeSharp.SourceGenerators;
using ComputeSharp.Tests.SourceGenerators.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests.SourceGenerators;

[TestClass]
public class Test_ComputeShaderDescriptorGenerator_Analyzers
{
    [TestMethod]
    public async Task AllowsUnsafeBlocksNotEnabled_Warns()
    {
        const string source = """
            using ComputeSharp;

            [ThreadGroupSize(DefaultThreadGroupSizes.X)]
            [{|CMPS0052:GeneratedComputeShaderDescriptor|}]
            internal readonly partial struct MyShader : IComputeShader
            {
                private readonly ReadWriteBuffer<float> buffer;

                public void Execute()
                {
                }
            }
            """;

        await CSharpAnalyzerTest<MissingAllowUnsafeBlocksCompilationOptionAnalyzer>.VerifyAnalyzerAsync(source, allowUnsafeBlocks: false);
    }

    [TestMethod]
    public async Task MissingComputeShaderDescriptor_ComputeShader()
    {
        const string source = """
            using ComputeSharp;

            internal partial struct {|CMPS0053:MyShader|} : IComputeShader
            {
                public ReadWriteBuffer<float> buffer;

                public void Execute()
                {
                }
            }
            """;

        await CSharpAnalyzerTest<MissingComputeShaderDescriptorOnComputeShaderAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task MissingComputeShaderDescriptor_ComputeShaderOfT()
    {
        const string source = """
            using ComputeSharp;
            using float4 = ComputeSharp.Float4;

            internal partial struct {|CMPS0053:MyShader|} : IComputeShader<float4>
            {
                public float4 Execute()
                {
                    return 0;
                }
            }
            """;

        await CSharpAnalyzerTest<MissingComputeShaderDescriptorOnComputeShaderAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task MissingComputeShaderDescriptor_ManuallyImplemented_DoesNotWarn()
    {
        const string source = """
            using System;
            using ComputeSharp;
            using ComputeSharp.Descriptors;
            using ComputeSharp.Interop;

            internal partial struct MyShader : IComputeShader, IComputeShaderDescriptor<MyShader>
            {
                public static int ThreadsX => throw new NotImplementedException();

                public static int ThreadsY => throw new NotImplementedException();

                public static int ThreadsZ => throw new NotImplementedException();

                public static int ConstantBufferSize => throw new NotImplementedException();

                public static bool IsStaticSamplerRequired => throw new NotImplementedException();

                public static ReadOnlyMemory<ResourceDescriptorRange> ResourceDescriptorRanges => throw new NotImplementedException();

                public static string HlslSource => throw new NotImplementedException();

                public static ReadOnlyMemory<byte> HlslBytecode => throw new NotImplementedException();

                public static void LoadConstantBuffer<TLoader>(in MyShader shader, ref TLoader loader, int x, int y, int z) where TLoader : struct, IConstantBufferLoader
                {
                    throw new NotImplementedException();
                }

                public static void LoadGraphicsResources<TLoader>(in MyShader shader, ref TLoader loader) where TLoader : struct, IGraphicsResourceLoader
                {
                    throw new NotImplementedException();
                }

                public void Execute()
                {
                }
            }
            """;

        await CSharpAnalyzerTest<MissingComputeShaderDescriptorOnComputeShaderAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task NotAccessibleNestedShaderType()
    {
        const string source = """
            using ComputeSharp;

            namespace MyFancyApp.Sample;

            public partial class Foo
            {
                [{|CMPS0055:GeneratedComputeShaderDescriptor|}]
                private partial struct MyShader : IComputeShader
                {
                    public ReadWriteBuffer<float> buffer;

                    public void Execute()
                    {
                    }
                }
            }
            """;

        await CSharpAnalyzerTest<NotAccessibleGeneratedComputeShaderDescriptorAttributeTargetAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task NotAccessibleShaderFieldType()
    {
        const string source = """
            using ComputeSharp;

            namespace MyFancyApp.Sample;

            public partial class Foo
            {
                [{|CMPS0056:GeneratedComputeShaderDescriptor|}]
                private partial struct MyShader : IComputeShader
                {
                    public ReadWriteBuffer<float> buffer;
                    public Bar bar;

                    public void Execute()
                    {
                    }
                }

                private struct Bar
                {
                    public int X;
                }
            }
            """;

        await CSharpAnalyzerTest<NotAccessibleFieldTypeInGeneratedShaderDescriptorAttributeTargetAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task NotReadOnlyShaderType()
    {
        const string source = """
            using ComputeSharp;

            namespace MyFancyApp.Sample;

            internal partial struct {|CMPS0057:MyShader|} : IComputeShader
            {
                public ReadWriteBuffer<float> buffer;

                public void Execute()
                {
                }
            }
            """;

        await CSharpAnalyzerTest<NotReadOnlyComputeShaderTypeWithFieldsAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task GloballyCoherentAttribute_NotWithinShaderType()
    {
        const string source = """
            using ComputeSharp;

            namespace MyFancyApp.Sample;

            internal partial struct MyType
            {
                [{|CMPS0058:GloballyCoherent|}]
                public ReadWriteBuffer<float> buffer;
            }
            """;

        await CSharpAnalyzerTest<InvalidGloballyCoherentFieldDeclarationAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task GloballyCoherentAttribute_IncorrectType()
    {
        const string source = """
            using ComputeSharp;

            namespace MyFancyApp.Sample;

            internal partial struct MyType : IComputeShader
            {
                [{|CMPS0058:GloballyCoherent|}]
                public ReadOnlyBuffer<float> buffer;

                public void Execute()
                {
                }
            }
            """;

        await CSharpAnalyzerTest<InvalidGloballyCoherentFieldDeclarationAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task GloballyCoherentAttribute_StaticField()
    {
        const string source = """
            using ComputeSharp;

            namespace MyFancyApp.Sample;

            internal partial struct MyType : IComputeShader
            {
                [{|CMPS0058:GloballyCoherent|}]
                public static ReadWriteBuffer<float> buffer;

                public void Execute()
                {
                }
            }
            """;

        await CSharpAnalyzerTest<InvalidGloballyCoherentFieldDeclarationAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task GloballyCoherentAttribute_ValidField_DoesNotWarn()
    {
        const string source = """
            using ComputeSharp;

            namespace MyFancyApp.Sample;

            internal partial struct MyType : IComputeShader
            {
                [GloballyCoherent]
                public ReadWriteBuffer<float> buffer;

                public void Execute()
                {
                }
            }
            """;

        await CSharpAnalyzerTest<InvalidGloballyCoherentFieldDeclarationAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task GroupSharedAttribute_NotWithinShaderType()
    {
        const string source = """
            using ComputeSharp;

            namespace MyFancyApp.Sample;

            internal partial struct MyType
            {
                [{|CMPS0004:GroupShared|}]
                public static int[] numbers;
            }
            """;

        await CSharpAnalyzerTest<InvalidGroupSharedFieldDeclarationAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task GroupSharedAttribute_IncorrectType()
    {
        const string source = """
            using ComputeSharp;

            namespace MyFancyApp.Sample;

            internal partial struct MyType : IComputeShader
            {
                [{|CMPS0004:GroupShared|}]
                public static ReadWriteBuffer<int> numbers;

                public void Execute()
                {
                }
            }
            """;

        await CSharpAnalyzerTest<InvalidGroupSharedFieldDeclarationAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task GroupSharedAttribute_InstanceField()
    {
        const string source = """
            using ComputeSharp;

            namespace MyFancyApp.Sample;

            internal partial struct MyType : IComputeShader
            {
                [{|CMPS0004:GroupShared|}]
                public int[] numbers;

                public void Execute()
                {
                }
            }
            """;

        await CSharpAnalyzerTest<InvalidGroupSharedFieldDeclarationAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task GroupSharedAttribute_ValidField_DoesNotWarn()
    {
        const string source = """
            using ComputeSharp;

            namespace MyFancyApp.Sample;

            internal partial struct MyType : IComputeShader
            {
                [GroupShared]
                public static int[] numbers;

                public void Execute()
                {
                }
            }
            """;

        await CSharpAnalyzerTest<InvalidGroupSharedFieldDeclarationAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task InvalidGeneratedComputeShaderDescriptorAttributeTarget_NoInterface()
    {
        const string source = """
            using ComputeSharp;

            [{|CMPS0054:GeneratedComputeShaderDescriptor|}]
            internal partial struct MyType
            {
            }
            """;

        await CSharpAnalyzerTest<InvalidGeneratedComputeShaderDescriptorAttributeTargetAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task InvalidGeneratedComputeShaderDescriptorAttributeTarget_GenericType()
    {
        const string source = """
            using ComputeSharp;

            [{|CMPS0054:GeneratedComputeShaderDescriptor|}]
            internal partial struct MyType<T> : IComputeShader
            {
                public void Execute()
                {
                }
            }
            """;

        await CSharpAnalyzerTest<InvalidGeneratedComputeShaderDescriptorAttributeTargetAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task InvalidGeneratedComputeShaderDescriptorAttributeTarget_TypeNestedInsideGenericType()
    {
        const string source = """
            using ComputeSharp;

            internal partial class Foo<T>
            {
                [{|CMPS0054:GeneratedComputeShaderDescriptor|}]
                internal partial struct MyType : IComputeShader
                {
                    public void Execute()
                    {
                    }
                }
            }
            """;

        await CSharpAnalyzerTest<InvalidGeneratedComputeShaderDescriptorAttributeTargetAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task MultipleComputeShaderInterfacesOnShader_TwoInterfaces()
    {
        const string source = """
            using ComputeSharp;

            [GeneratedComputeShaderDescriptor]
            internal partial struct {|CMPS0042:MyType|} : IComputeShader, IComputeShader<Float4>
            {
                public void Execute()
                {
                }

                Float4 IComputeShader<Float4>.Execute()
                {
                    return 0;
                }
            }
            """;

        await CSharpAnalyzerTest<MultipleComputeShaderInterfacesOnShaderTypeAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task MissingThreadGroupSizeAttribute_MissingAttribute_Warns()
    {
        const string source = """
            using ComputeSharp;

            internal partial struct {|CMPS0047:MyType|} : IComputeShader
            {
                public void Execute()
                {
                }
            }
            """;

        await CSharpAnalyzerTest<InvalidThreadGroupSizeAttributeUseAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task MissingThreadGroupSizeAttribute_AttributePresent_DoesNotWarn()
    {
        const string source = """
            using ComputeSharp;

            [ThreadGroupSize(8, 8, 8)]
            internal partial struct MyType : IComputeShader
            {
                public void Execute()
                {
                }
            }
            """;

        await CSharpAnalyzerTest<InvalidThreadGroupSizeAttributeUseAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task InvalidThreadGroupSizeAttributeDefaultThreadGroupSizes_Default_Warns()
    {
        const string source = """
            using ComputeSharp;

            [{|CMPS0048:ThreadGroupSize(default(DefaultThreadGroupSizes))|}]
            internal partial struct MyType : IComputeShader
            {
                public void Execute()
                {
                }
            }
            """;

        await CSharpAnalyzerTest<InvalidThreadGroupSizeAttributeUseAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task InvalidThreadGroupSizeAttributeDefaultThreadGroupSizes_OutOfRange_Warns()
    {
        const string source = """
            using ComputeSharp;

            [{|CMPS0048:ThreadGroupSize((DefaultThreadGroupSizes)1234)|}]
            internal partial struct MyType : IComputeShader
            {
                public void Execute()
                {
                }
            }
            """;

        await CSharpAnalyzerTest<InvalidThreadGroupSizeAttributeUseAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task InvalidThreadGroupSizeAttributeDefaultThreadGroupSizes_Valid_DoesNotWarn()
    {
        const string source = """
            using ComputeSharp;

            [ThreadGroupSize(DefaultThreadGroupSizes.XY)]
            internal partial struct MyType : IComputeShader
            {
                public void Execute()
                {
                }
            }
            """;

        await CSharpAnalyzerTest<InvalidThreadGroupSizeAttributeUseAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    [DataRow(-1, 1, 1)]
    [DataRow(1, -1, 1)]
    [DataRow(1, 1, -1)]
    [DataRow(1050, 1, 1)]
    [DataRow(1, 1050, 1)]
    [DataRow(1, 1, 70)]
    [DataRow(0, 123456, -12)]
    public async Task InvalidThreadGroupSizeAttributeValues_OutOfRange_Warns(int threadsX, int threadsY, int threadsZ)
    {
        string source = $$"""
            using ComputeSharp;

            [{|CMPS0044:ThreadGroupSize({{threadsX}}, {{threadsY}}, {{threadsZ}})|}]
            internal partial struct MyType : IComputeShader
            {
                public void Execute()
                {
                }
            }
            """;

        await CSharpAnalyzerTest<InvalidThreadGroupSizeAttributeUseAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task InvalidThreadGroupSizeAttributeValues_Valid_DoesNotWarn()
    {
        const string source = """
            using ComputeSharp;

            [ThreadGroupSize(8, 8, 8)]
            internal partial struct MyType : IComputeShader
            {
                public void Execute()
                {
                }
            }
            """;

        await CSharpAnalyzerTest<InvalidThreadGroupSizeAttributeUseAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task ExceededDispatchDataSize_ConstantBufferTooLarge_WithNestedTypes_Warns()
    {
        const string source = """
            using ComputeSharp;
            using float1x3 = ComputeSharp.Float1x3;
            using float2x2 = ComputeSharp.Float2x2;
            using float3x2 = ComputeSharp.Float3x2;
            using float4x4 = ComputeSharp.Float4x4;
            using float2 = ComputeSharp.Float2;
            using float3 = ComputeSharp.Float3;
            using float4 = ComputeSharp.Float4;

            public struct Data0
            {
                public float4x4 m0;
                public float4 v0;
                public float4 v1;
                public float s0;
                public float s1;
                public float s2;
                public float s3;
            }

            public struct Data1
            {
                public Data0 d0;
                public Data0 d1;
                public Data0 d2;
                public Data0 d3;
                public Data0 d4;
                public float3x2 m0;
                public float2x2 m1;
                public float3 v0;
                public float s0;
                public float s1;
            }

            public struct Data2
            {
                public Data1 d0;
                public Data1 d1;
                public Data1 d2;
                public Data1 d3;
                public float2x2 m0;
                public float1x3 m1;
                public float3 v0;
                public float4 v1;
                public float2 v2;
                public float s0;
            }

            internal partial struct {|CMPS0041:MyType|}(Data2 data2) : IComputeShader
            {
                ReadWriteBuffer<float> result;

                public void Execute()
                {
                    result[ThreadIds.X] = data2.s0;
                }
            }
            """;

        await CSharpAnalyzerTest<ExcedeedComputeShaderDispatchDataSizeAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task ExceededDispatchDataSize_ConstantBufferTooLarge_WithDoubles_Warns()
    {
        const string source = """
            using ComputeSharp;
            using double4x4 = ComputeSharp.Double4x4;
            using float4 = ComputeSharp.Float4;

            internal partial struct {|CMPS0041:MyType|} : IComputeShader
            {
                public readonly ReadWriteBuffer<float> buffer;
                public readonly double4x4 a;
                public readonly double4x4 b;
                public readonly int c;
                public readonly float4 d;

                public void Execute()
                {
                }
            }
            """;

        await CSharpAnalyzerTest<ExcedeedComputeShaderDispatchDataSizeAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task ExceededDispatchDataSize_ConstantBufferTooLarge_WithManyResources_Warns()
    {
        const string source = """
            using ComputeSharp;
            using float4x4 = ComputeSharp.Float4x4;
            using float4 = ComputeSharp.Float4;

            public struct Data0
            {
                public float4x4 m0;
                public float4 v0;
                public float4 v1;
                public float4 v2;
                public float s0;
                public float s1;
                public float s2;
                public float s3;
            }

            internal partial struct {|CMPS0041:MyType|}(Data0 data0) : IComputeShader
            {
                ReadWriteBuffer<float> result0;
                ReadWriteBuffer<float> result1;
                ReadWriteBuffer<float> result2;
                ReadWriteBuffer<float> result3;
                ReadWriteBuffer<float> result4;
                ReadWriteBuffer<float> result5;
                ReadWriteBuffer<float> result6;
                ReadWriteBuffer<float> result7;
                ReadWriteBuffer<float> result8;
                ReadWriteBuffer<float> result9;
                ReadWriteBuffer<float> result10;
                ReadWriteBuffer<float> result11;
                ReadWriteBuffer<float> result12;
                ReadWriteBuffer<float> result13;
                ReadWriteBuffer<float> result14;
                ReadWriteBuffer<float> result15;
                ReadWriteBuffer<float> result16;
                ReadWriteBuffer<float> result17;
                ReadWriteBuffer<float> result18;
                ReadWriteBuffer<float> result19;
                ReadWriteBuffer<float> result20;
                ReadWriteBuffer<float> result21;
                ReadWriteBuffer<float> result22;
                ReadWriteBuffer<float> result23;
                ReadWriteBuffer<float> result24;
                ReadWriteBuffer<float> result25;
                ReadWriteBuffer<float> result26;
                ReadWriteBuffer<float> result27;
                ReadWriteBuffer<float> result28;
                ReadWriteBuffer<float> result29;
                ReadWriteBuffer<float> result30;
                ReadWriteBuffer<float> result31;

                public void Execute()
                {
                    result0[ThreadIds.X] = data0.s0;
                }
            }
            """;

        await CSharpAnalyzerTest<ExcedeedComputeShaderDispatchDataSizeAnalyzer>.VerifyAnalyzerAsync(source);
    }
}