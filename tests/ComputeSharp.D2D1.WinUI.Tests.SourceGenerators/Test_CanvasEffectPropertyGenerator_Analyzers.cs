using System.Threading.Tasks;
using ComputeSharp.D2D1.WinUI.SourceGenerators;
using ComputeSharp.Tests.SourceGenerators.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.D2D1.WinUI.Tests.SourceGenerators;

[TestClass]
public class Test_CanvasEffectPropertyGenerator_Analyzers
{
    [TestMethod]
    public async Task InvalidGeneratedCanvasEffectPropertyContainingTypeAnalyzer_NoBaseType_Warns()
    {
        const string source = """
            using ComputeSharp.D2D1.WinUI;

            public class MyEffect
            {
                [{|CMPSD2DWINUI0001:GeneratedCanvasEffectProperty|}]
                public int Number { get; set; }
            }
            """;

        await CSharpAnalyzerWithLanguageVersionTest<InvalidGeneratedCanvasEffectPropertyContainingTypeAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task InvalidGeneratedCanvasEffectPropertyContainingTypeAnalyzer_CorrectBaseType_DoesNotWarn()
    {
        const string source = """
            using ComputeSharp.D2D1.WinUI;

            public abstract class MyEffect : CanvasEffect
            {
                [GeneratedCanvasEffectProperty]
                public int Number { get; set; }
            }
            """;

        await CSharpAnalyzerWithLanguageVersionTest<InvalidGeneratedCanvasEffectPropertyContainingTypeAnalyzer>.VerifyAnalyzerAsync(source);
    }
}