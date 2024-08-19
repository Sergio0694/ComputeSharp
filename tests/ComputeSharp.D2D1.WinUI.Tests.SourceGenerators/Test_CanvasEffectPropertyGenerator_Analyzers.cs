using System.Threading.Tasks;
using ComputeSharp.D2D1.WinUI.SourceGenerators;
using ComputeSharp.Tests.SourceGenerators.Helpers;
using Microsoft.CodeAnalysis.CSharp;
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

            public partial class MyEffect
            {
                [{|CMPSD2DWINUI0001:GeneratedCanvasEffectProperty|}]
                public partial int {|CS9248:Number|} { get; set; }
            }
            """;

        await CSharpAnalyzerWithLanguageVersionTest<InvalidGeneratedCanvasEffectPropertyContainingTypeAnalyzer>.VerifyAnalyzerAsync(source, languageVersion: LanguageVersion.Preview);
    }

    [TestMethod]
    public async Task InvalidGeneratedCanvasEffectPropertyAccessorsAnalyzer_ReadOnlyProperty_Warns()
    {
        const string source = """
            using ComputeSharp.D2D1.WinUI;

            public partial class MyEffect
            {
                [{|CMPSD2DWINUI0002:GeneratedCanvasEffectProperty|}]
                public partial int {|CS9248:Number|} { get; }
            }
            """;

        await CSharpAnalyzerWithLanguageVersionTest<InvalidGeneratedCanvasEffectPropertyAccessorsAnalyzer>.VerifyAnalyzerAsync(source, languageVersion: LanguageVersion.Preview);
    }

    [TestMethod]
    public async Task InvalidGeneratedCanvasEffectPropertyAccessorsAnalyzer_InitOnlyProperty_Warns()
    {
        const string source = """
            using ComputeSharp.D2D1.WinUI;

            public abstract partial class MyEffect : CanvasEffect
            {
                [{|CMPSD2DWINUI0002:GeneratedCanvasEffectProperty|}]
                public partial int {|CS9248:Number|} { get; init; }
            }
            """;

        await CSharpAnalyzerWithLanguageVersionTest<InvalidGeneratedCanvasEffectPropertyAccessorsAnalyzer>.VerifyAnalyzerAsync(source, languageVersion: LanguageVersion.Preview);
    }
}