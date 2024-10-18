using ComputeSharp.D2D1.WinUI.SourceGenerators;
using ComputeSharp.Tests.SourceGenerators.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.D2D1.WinUI.Tests.SourceGenerators;

[TestClass]
public class Test_CanvasEffectPropertyGenerator_Incrementality
{
    [TestMethod]
    public void ModifiedOptions_ModifiesOutput()
    {
        const string source = """"
            using ComputeSharp.D2D1.WinUI;

            abstract partial class MyEffect : CanvasEffect
            {
                [GeneratedCanvasEffectProperty]
                public partial int Number { get; set; }
            }
            """";

        const string updatedSource = """"
            using ComputeSharp.D2D1.WinUI;

            abstract partial class MyEffect : CanvasEffect
            {
                [GeneratedCanvasEffectProperty(CanvasEffectInvalidationType.Creation)]
                public partial int Number { get; set; }
            }
            """";

        CSharpGeneratorTest<CanvasEffectPropertyGenerator>.VerifyIncrementalSteps(
            source,
            updatedSource,
            executeReason: IncrementalStepRunReason.Modified,
            diagnosticsReason: null,
            outputReason: IncrementalStepRunReason.Modified,
            diagnosticsSourceReason: null,
            sourceReason: IncrementalStepRunReason.Modified,
            languageVersion: LanguageVersion.Preview);
    }

    [TestMethod]
    public void AddedLeadingTrivia_DoesNotModifyOutput()
    {
        const string source = """"
            using ComputeSharp.D2D1.WinUI;

            abstract partial class MyEffect : CanvasEffect
            {
                [GeneratedCanvasEffectProperty]
                public partial int Number { get; set; }
            }
            """";

        const string updatedSource = """"
            using ComputeSharp.D2D1.WinUI;

            abstract partial class MyEffect : CanvasEffect
            {
                /// <summary>
                /// This is some property.
                /// </summary>
                [GeneratedCanvasEffectProperty]
                public partial int Number { get; set; }
            }
            """";

        CSharpGeneratorTest<CanvasEffectPropertyGenerator>.VerifyIncrementalSteps(
            source,
            updatedSource,
            executeReason: IncrementalStepRunReason.Unchanged,
            diagnosticsReason: null,
            outputReason: IncrementalStepRunReason.Cached,
            diagnosticsSourceReason: null,
            sourceReason: IncrementalStepRunReason.Cached,
            languageVersion: LanguageVersion.Preview);
    }

    [TestMethod]
    public void AddedOtherMember_DoesNotModifyOutput()
    {
        const string source = """"
            using ComputeSharp.D2D1.WinUI;

            abstract partial class MyEffect : CanvasEffect
            {
                [GeneratedCanvasEffectProperty]
                public partial int Number { get; set; }
            }
            """";

        const string updatedSource = """"
            using ComputeSharp.D2D1.WinUI;

            abstract partial class MyEffect : CanvasEffect
            {
                public void Foo()
                {
                }

                [GeneratedCanvasEffectProperty]
                public partial int Number { get; set; }
            }
            """";

        CSharpGeneratorTest<CanvasEffectPropertyGenerator>.VerifyIncrementalSteps(
            source,
            updatedSource,
            executeReason: IncrementalStepRunReason.Unchanged,
            diagnosticsReason: null,
            outputReason: IncrementalStepRunReason.Cached,
            diagnosticsSourceReason: null,
            sourceReason: IncrementalStepRunReason.Cached,
            languageVersion: LanguageVersion.Preview);
    }
}