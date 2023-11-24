using System.Threading.Tasks;
using ComputeSharp.D2D1.SourceGenerators;
using ComputeSharp.Tests.SourceGenerators.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.D2D1.Tests.SourceGenerators;

[TestClass]
public class Test_Analyzers
{
    [TestMethod]
    public async Task NotReadOnlyShaderType_WithFields_Warns()
    {
        const string source = """
            using ComputeSharp;
            using ComputeSharp.D2D1;

            namespace MyFancyApp.Sample;

            internal partial struct {|CMPSD2D0070:MyShader|} : ID2D1PixelShader
            {
                public float number;

                public Float4 Execute()
                {
                    return this.number;
                }
            }
            """;

        await CSharpAnalyzerWithLanguageVersionTest<NotReadOnlyPixelShaderTypeWithFieldsAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task NotReadOnlyShaderType_WithNoFields_DoesNotWarn()
    {
        const string source = """
            using ComputeSharp;
            using ComputeSharp.D2D1;

            namespace MyFancyApp.Sample;

            internal partial struct MyShader : ID2D1PixelShader
            {
                public Float4 Execute()
                {
                    return default;
                }
            }
            """;

        await CSharpAnalyzerWithLanguageVersionTest<NotReadOnlyPixelShaderTypeWithFieldsAnalyzer>.VerifyAnalyzerAsync(source);
    }
}