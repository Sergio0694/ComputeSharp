using System.Threading.Tasks;
using ComputeSharp.D2D1.SourceGenerators;
using ComputeSharp.Tests.SourceGenerators.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.D2D1.Tests.SourceGenerators;

[TestClass]
public class Test_InvalidD2D1CompileOptionsEnableLinkingOnShaderTypeAnalyzer
{
    [TestMethod]
    public async Task NoCompileOptionsDoesNotWarn()
    {
        const string source = """
            using ComputeSharp;
            using ComputeSharp.D2D1;

            public partial class Foo
            {
                [D2DInputCount(0)]
                private partial struct MyShader : ID2D1PixelShader
                {
                    public Float4 Execute() => 0;
                }
            }
            """;

        await CSharpAnalyzerWithLanguageVersionTest<InvalidD2D1CompileOptionsEnableLinkingOnShaderTypeAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task ValidCompileOptionsDoesNotWarn()
    {
        const string source = """
            using ComputeSharp;
            using ComputeSharp.D2D1;

            public partial class Foo
            {
                [D2DInputCount(0)]
                [D2DCompileOptions(D2D1CompileOptions.PackMatrixRowMajor)]
                private partial struct MyShader : ID2D1PixelShader
                {
                    public Float4 Execute() => 0;
                }
            }
            """;

        await CSharpAnalyzerWithLanguageVersionTest<InvalidD2D1CompileOptionsEnableLinkingOnShaderTypeAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task EnableLinkingWithNoInputCountDoesNotWarn()
    {
        const string source = """
            using ComputeSharp;
            using ComputeSharp.D2D1;

            public partial class Foo
            {
                [D2DInputComplex(0)]
                [D2DCompileOptions(D2D1CompileOptions.EnableLinking)]
                private partial struct MyShader : ID2D1PixelShader
                {
                    public Float4 Execute() => 0;
                }
            }
            """;

        await CSharpAnalyzerWithLanguageVersionTest<InvalidD2D1CompileOptionsEnableLinkingOnShaderTypeAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task EnableLinkingWithImplicitComplexInputWarns()
    {
        const string source = """
            using ComputeSharp;
            using ComputeSharp.D2D1;

            public partial class Foo
            {
                [D2DInputCount(2)]
                [D2DInputSimple(1)]
                [{|CMPSD2D0069:D2DCompileOptions(D2D1CompileOptions.Default | D2D1CompileOptions.EnableLinking)|}]
                private partial struct MyShader : ID2D1PixelShader
                {
                    public Float4 Execute() => 0;
                }
            }
            """;

        await CSharpAnalyzerWithLanguageVersionTest<InvalidD2D1CompileOptionsEnableLinkingOnShaderTypeAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task EnableLinkingWithExplicitComplexInputWarns()
    {
        const string source = """
            using ComputeSharp;
            using ComputeSharp.D2D1;

            public partial class Foo
            {
                [D2DInputCount(2)]
                [D2DInputSimple(0)]
                [D2DInputComplex(1)]
                [{|CMPSD2D0069:D2DCompileOptions(D2D1CompileOptions.Default | D2D1CompileOptions.EnableLinking)|}]
                private partial struct MyShader : ID2D1PixelShader
                {
                    public Float4 Execute() => 0;
                }
            }
            """;

        await CSharpAnalyzerWithLanguageVersionTest<InvalidD2D1CompileOptionsEnableLinkingOnShaderTypeAnalyzer>.VerifyAnalyzerAsync(source);
    }
}