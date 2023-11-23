using System.Threading.Tasks;
using ComputeSharp.D2D1.SourceGenerators;
using ComputeSharp.Tests.SourceGenerators.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.D2D1.Tests.SourceGenerators;

[TestClass]
public class Test_Analyzers
{
    [TestMethod]
    public async Task NotReadonlyShaderType()
    {
        const string source = """
            using ComputeSharp;
            using ComputeSharp.D2D1;

            namespace MyFancyApp.Sample;

            internal partial struct {|CMPSD2D0070:MyShader|} : ID2D1PixelShader
            {
                public Float4 Execute()
                {
                    return default;
                }
            }
            """;

        await CSharpAnalyzerWithLanguageVersionTest<NotReadonlyPixelShaderTypeAnalyzer>.VerifyAnalyzerAsync(source);
    }
}