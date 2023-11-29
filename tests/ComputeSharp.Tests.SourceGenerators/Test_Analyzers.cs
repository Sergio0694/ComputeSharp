using System.Threading.Tasks;
using ComputeSharp.SourceGenerators;
using ComputeSharp.Tests.SourceGenerators.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests.SourceGenerators;

[TestClass]
[TestCategory("Analyzers")]
public class Test_Analyzers
{
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

        await CSharpAnalyzerWithLanguageVersionTest<MissingComputeShaderDescriptorOnComputeShaderAnalyzer>.VerifyAnalyzerAsync(source);
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

        await CSharpAnalyzerWithLanguageVersionTest<MissingComputeShaderDescriptorOnComputeShaderAnalyzer>.VerifyAnalyzerAsync(source);
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

        await CSharpAnalyzerWithLanguageVersionTest<MissingComputeShaderDescriptorOnComputeShaderAnalyzer>.VerifyAnalyzerAsync(source);
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

        await CSharpAnalyzerWithLanguageVersionTest<NotAccessibleGeneratedComputeShaderDescriptorAttributeTargetAnalyzer>.VerifyAnalyzerAsync(source);
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

        await CSharpAnalyzerWithLanguageVersionTest<NotAccessibleFieldTypeInGeneratedShaderDescriptorAttributeTargetAnalyzer>.VerifyAnalyzerAsync(source);
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

        await CSharpAnalyzerWithLanguageVersionTest<NotReadOnlyComputeShaderTypeWithFieldsAnalyzer>.VerifyAnalyzerAsync(source);
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

        await CSharpAnalyzerWithLanguageVersionTest<InvalidGloballyCoherentFieldDeclarationAnalyzer>.VerifyAnalyzerAsync(source);
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

        await CSharpAnalyzerWithLanguageVersionTest<InvalidGloballyCoherentFieldDeclarationAnalyzer>.VerifyAnalyzerAsync(source);
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

        await CSharpAnalyzerWithLanguageVersionTest<InvalidGloballyCoherentFieldDeclarationAnalyzer>.VerifyAnalyzerAsync(source);
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

        await CSharpAnalyzerWithLanguageVersionTest<InvalidGloballyCoherentFieldDeclarationAnalyzer>.VerifyAnalyzerAsync(source);
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

        await CSharpAnalyzerWithLanguageVersionTest<InvalidGroupSharedFieldDeclarationAnalyzer>.VerifyAnalyzerAsync(source);
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

        await CSharpAnalyzerWithLanguageVersionTest<InvalidGroupSharedFieldDeclarationAnalyzer>.VerifyAnalyzerAsync(source);
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

        await CSharpAnalyzerWithLanguageVersionTest<InvalidGroupSharedFieldDeclarationAnalyzer>.VerifyAnalyzerAsync(source);
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

        await CSharpAnalyzerWithLanguageVersionTest<InvalidGroupSharedFieldDeclarationAnalyzer>.VerifyAnalyzerAsync(source);
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

        await CSharpAnalyzerWithLanguageVersionTest<InvalidGeneratedComputeShaderDescriptorAttributeTargetAnalyzer>.VerifyAnalyzerAsync(source);
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

        await CSharpAnalyzerWithLanguageVersionTest<InvalidGeneratedComputeShaderDescriptorAttributeTargetAnalyzer>.VerifyAnalyzerAsync(source);
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

        await CSharpAnalyzerWithLanguageVersionTest<InvalidGeneratedComputeShaderDescriptorAttributeTargetAnalyzer>.VerifyAnalyzerAsync(source);
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

        await CSharpAnalyzerWithLanguageVersionTest<MultipleComputeShaderInterfacesOnShaderTypeAnalyzer>.VerifyAnalyzerAsync(source);
    }
}