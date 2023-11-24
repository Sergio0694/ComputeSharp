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
}