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

        await CSharpAnalyzerTest<InvalidGeneratedCanvasEffectPropertyContainingTypeAnalyzer>.VerifyAnalyzerAsync(source, languageVersion: LanguageVersion.Preview);
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

        await CSharpAnalyzerTest<InvalidGeneratedCanvasEffectPropertyAccessorsAnalyzer>.VerifyAnalyzerAsync(source, languageVersion: LanguageVersion.Preview);
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

        await CSharpAnalyzerTest<InvalidGeneratedCanvasEffectPropertyAccessorsAnalyzer>.VerifyAnalyzerAsync(source, languageVersion: LanguageVersion.Preview);
    }

    [TestMethod]
    public async Task InvalidGeneratedCanvasEffectPropertyDeclarationAnalyzer_StaticProperty_Warns()
    {
        const string source = """
            using ComputeSharp.D2D1.WinUI;

            public abstract partial class MyEffect : CanvasEffect
            {
                [{|CMPSD2DWINUI0003:GeneratedCanvasEffectProperty|}]
                public static partial int {|CS9248:Number|} { get; set; }
            }
            """;

        await CSharpAnalyzerTest<InvalidGeneratedCanvasEffectPropertyDeclarationAnalyzer>.VerifyAnalyzerAsync(source, languageVersion: LanguageVersion.Preview);
    }

    [TestMethod]
    public async Task InvalidGeneratedCanvasEffectPropertyDeclarationAnalyzer_NotPartialProperty_Warns()
    {
        const string source = """
            using ComputeSharp.D2D1.WinUI;

            public abstract partial class MyEffect : CanvasEffect
            {
                [{|CMPSD2DWINUI0004:GeneratedCanvasEffectProperty|}]
                public int Number { get; set; }
            }
            """;

        await CSharpAnalyzerTest<InvalidGeneratedCanvasEffectPropertyDeclarationAnalyzer>.VerifyAnalyzerAsync(source, languageVersion: LanguageVersion.Preview);
    }

    [TestMethod]
    public async Task InvalidGeneratedCanvasEffectPropertyDeclarationAnalyzer_PartialPropertyWithImplementation_Warns()
    {
        const string source = """
            using ComputeSharp.D2D1.WinUI;

            public abstract partial class MyEffect : CanvasEffect
            {
                [{|CMPSD2DWINUI0004:GeneratedCanvasEffectProperty|}]
                public partial int Number { get; set; }

                public partial int Number { get => 42; set { } }
            }
            """;

        await CSharpAnalyzerTest<InvalidGeneratedCanvasEffectPropertyDeclarationAnalyzer>.VerifyAnalyzerAsync(source, languageVersion: LanguageVersion.Preview);
    }

    [TestMethod]
    public async Task InvalidGeneratedCanvasEffectPropertyDeclarationAnalyzer_PartialPropertyWithImplementation_GeneratedByGenerator_DoesNotWarn()
    {
        const string source = """
            using System.CodeDom.Compiler;
            using ComputeSharp.D2D1.WinUI;

            public abstract partial class MyEffect : CanvasEffect
            {
                [GeneratedCanvasEffectProperty]
                public partial int Number { get; set; }

                [GeneratedCode("ComputeSharp.D2D1.WinUI.CanvasEffectPropertyGenerator", "1.0.0")]
                public partial int Number { get => 42; set { } }
            }
            """;

        await CSharpAnalyzerTest<InvalidGeneratedCanvasEffectPropertyDeclarationAnalyzer>.VerifyAnalyzerAsync(source, languageVersion: LanguageVersion.Preview);
    }

    [TestMethod]
    public async Task InvalidGeneratedCanvasEffectPropertyDeclarationAnalyzer_PartialPropertyImplementationPart_Warns()
    {
        const string source = """
            using ComputeSharp.D2D1.WinUI;

            public abstract partial class MyEffect : CanvasEffect
            {
                public partial int Number { get; set; }

                [{|CMPSD2DWINUI0004:GeneratedCanvasEffectProperty|}]
                public partial int Number { get => 42; set { } }
            }
            """;

        await CSharpAnalyzerTest<InvalidGeneratedCanvasEffectPropertyDeclarationAnalyzer>.VerifyAnalyzerAsync(source, languageVersion: LanguageVersion.Preview);
    }

    [TestMethod]
    public async Task InvalidGeneratedCanvasEffectPropertyDeclarationAnalyzer_ReturnsByRef_Warns()
    {
        const string source = """
            using ComputeSharp.D2D1.WinUI;

            public abstract partial class MyEffect : CanvasEffect
            {
                [{|CMPSD2DWINUI0005:GeneratedCanvasEffectProperty|}]
                public partial ref int {|CS9248:Number|} { get; {|CS8147:set|}; }
            }
            """;

        await CSharpAnalyzerTest<InvalidGeneratedCanvasEffectPropertyDeclarationAnalyzer>.VerifyAnalyzerAsync(source, languageVersion: LanguageVersion.Preview);
    }

    [TestMethod]
    public async Task InvalidGeneratedCanvasEffectPropertyDeclarationAnalyzer_ReturnsByRefReadonly_Warns()
    {
        const string source = """
            using ComputeSharp.D2D1.WinUI;

            public abstract partial class MyEffect : CanvasEffect
            {
                [{|CMPSD2DWINUI0005:GeneratedCanvasEffectProperty|}]
                public partial ref readonly int {|CS9248:Number|} { get; {|CS8147:set|}; }
            }
            """;

        await CSharpAnalyzerTest<InvalidGeneratedCanvasEffectPropertyDeclarationAnalyzer>.VerifyAnalyzerAsync(source, languageVersion: LanguageVersion.Preview);
    }

    [TestMethod]
    public async Task InvalidGeneratedCanvasEffectPropertyDeclarationAnalyzer_ReturnsRefStructType_Warns()
    {
        const string source = """
            using System;
            using ComputeSharp.D2D1.WinUI;

            public abstract partial class MyEffect : CanvasEffect
            {
                [{|CMPSD2DWINUI0006:GeneratedCanvasEffectProperty|}]
                public partial Span<int> {|CS9248:Number|} { get; set; }
            }
            """;

        await CSharpAnalyzerTest<InvalidGeneratedCanvasEffectPropertyDeclarationAnalyzer>.VerifyAnalyzerAsync(source, languageVersion: LanguageVersion.Preview);
    }

    [TestMethod]
    public async Task RequireCSharpLanguageVersionPreviewAnalyzer_LanguageVersionIsNotPreview_Warns()
    {
        const string source = """
            using System;
            using ComputeSharp.D2D1.WinUI;

            public abstract partial class MyEffect : CanvasEffect
            {
                [{|CMPSD2DWINUI0007:GeneratedCanvasEffectProperty|}]
                public int Number { get; set; }
            }
            """;

        await CSharpAnalyzerTest<RequireCSharpLanguageVersionPreviewAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task RequireCSharpLanguageVersionPreviewAnalyzer_LanguageVersionIsPreview_DoesNotWarn()
    {
        const string source = """
            using System;
            using ComputeSharp.D2D1.WinUI;

            public abstract partial class MyEffect : CanvasEffect
            {
                [GeneratedCanvasEffectProperty]
                public int Number { get; set; }
            }
            """;

        await CSharpAnalyzerTest<RequireCSharpLanguageVersionPreviewAnalyzer>.VerifyAnalyzerAsync(source, languageVersion: LanguageVersion.Preview);
    }

    [TestMethod]
    public async Task UseGeneratedCanvasEffectPropertyOnSemiAutoPropertyAnalyzer_NormalProperty_DoesNotWarn()
    {
        const string source = """
            using ComputeSharp.D2D1.WinUI;

            public abstract partial class SampleCanvasEffect : CanvasEffect
            {
                public string Name { get; set; }
            }
            """;

        await CSharpAnalyzerTest<UseGeneratedCanvasEffectPropertyOnSemiAutoPropertyAnalyzer>.VerifyAnalyzerAsync(source, languageVersion: LanguageVersion.Preview);
    }

    [TestMethod]
    public async Task UseGeneratedCanvasEffectPropertyOnSemiAutoPropertyAnalyzer_SimilarProperty_NotObservableObject_DoesNotWarn()
    {
        const string source = """
            using ComputeSharp.D2D1.WinUI;

            public abstract partial class SampleCanvasEffect : MyBaseCanvasEffect
            {
                public string Name
                {
                    get => field;
                    set => SetPropertyAndInvalidateEffectGraph(ref field, value);
                }
            }

            public abstract class MyBaseCanvasEffect
            {
                protected void SetPropertyAndInvalidateEffectGraph<T>(ref T location, T value, CanvasEffectInvalidationType invalidationType = CanvasEffectInvalidationType.Update)
                {
                }
            }
            """;

        await CSharpAnalyzerTest<UseGeneratedCanvasEffectPropertyOnSemiAutoPropertyAnalyzer>.VerifyAnalyzerAsync(source, languageVersion: LanguageVersion.Preview);
    }

    [TestMethod]
    public async Task UseGeneratedCanvasEffectPropertyOnSemiAutoPropertyAnalyzer_NoGetter_DoesNotWarn()
    {
        const string source = """
            using ComputeSharp.D2D1.WinUI;

            public abstract partial class SampleCanvasEffect : CanvasEffect
            {
                public string Name
                {
                    set => SetPropertyAndInvalidateEffectGraph(ref field, value);
                }
            }
            """;

        await CSharpAnalyzerTest<UseGeneratedCanvasEffectPropertyOnSemiAutoPropertyAnalyzer>.VerifyAnalyzerAsync(source, languageVersion: LanguageVersion.Preview);
    }

    [TestMethod]
    public async Task UseGeneratedCanvasEffectPropertyOnSemiAutoPropertyAnalyzer_NoSetter_DoesNotWarn()
    {
        const string source = """
            using ComputeSharp.D2D1.WinUI;

            public abstract partial class SampleCanvasEffect : CanvasEffect
            {
                public string Name
                {
                    get => field;
                }
            }
            """;

        await CSharpAnalyzerTest<UseGeneratedCanvasEffectPropertyOnSemiAutoPropertyAnalyzer>.VerifyAnalyzerAsync(source, languageVersion: LanguageVersion.Preview);
    }

    [TestMethod]
    public async Task UseGeneratedCanvasEffectPropertyOnSemiAutoPropertyAnalyzer_OtherLocation_DoesNotWarn()
    {
        const string source = """
            using ComputeSharp.D2D1.WinUI;

            public abstract partial class SampleCanvasEffect : CanvasEffect
            {
                public string Name
                {
                    get => field;
                    set
                    {
                        string test = field;

                        SetPropertyAndInvalidateEffectGraph(ref test, value);
                    }
                }
            }
            """;

        await CSharpAnalyzerTest<UseGeneratedCanvasEffectPropertyOnSemiAutoPropertyAnalyzer>.VerifyAnalyzerAsync(source, languageVersion: LanguageVersion.Preview);
    }

    [TestMethod]
    public async Task UseGeneratedCanvasEffectPropertyOnSemiAutoPropertyAnalyzer_OtherValue_DoesNotWarn()
    {
        const string source = """
            using ComputeSharp.D2D1.WinUI;

            public abstract partial class SampleCanvasEffect : CanvasEffect
            {
                public string Name
                {
                    get => field;
                    set
                    {
                        string test = "Bob";

                        SetPropertyAndInvalidateEffectGraph(ref field, test);
                    }
                }
            }
            """;

        await CSharpAnalyzerTest<UseGeneratedCanvasEffectPropertyOnSemiAutoPropertyAnalyzer>.VerifyAnalyzerAsync(source, languageVersion: LanguageVersion.Preview);
    }

    [TestMethod]
    public async Task UseGeneratedCanvasEffectPropertyOnSemiAutoPropertyAnalyzer_ValidProperty_WithGeneratedCanvasEffectProperty_DoesNotWarn()
    {
        const string source = """
            using ComputeSharp.D2D1.WinUI;

            public abstract partial class SampleCanvasEffect : CanvasEffect
            {
                [GeneratedCanvasEffectProperty]
                public string Name
                {
                    get => field;
                    set => SetPropertyAndInvalidateEffectGraph(ref field, value);
                }
            }
            """;

        await CSharpAnalyzerTest<UseGeneratedCanvasEffectPropertyOnSemiAutoPropertyAnalyzer>.VerifyAnalyzerAsync(source, languageVersion: LanguageVersion.Preview);
    }

    [TestMethod]
    public async Task UseGeneratedCanvasEffectPropertyOnSemiAutoPropertyAnalyzer_GetAccessorWithExpressionBody_DoesNotWarn()
    {
        const string source = """
            using ComputeSharp.D2D1.WinUI;

            public abstract partial class SampleCanvasEffect : CanvasEffect
            {
                public string Name
                {
                    get => "Hello world";
                    set => SetPropertyAndInvalidateEffectGraph(ref field, value);
                }
            }
            """;

        await CSharpAnalyzerTest<UseGeneratedCanvasEffectPropertyOnSemiAutoPropertyAnalyzer>.VerifyAnalyzerAsync(source, languageVersion: LanguageVersion.Preview);
    }

    [TestMethod]
    public async Task UseGeneratedCanvasEffectPropertyOnSemiAutoPropertyAnalyzer_ValidProperty_Warns()
    {
        const string source = """
            using ComputeSharp.D2D1.WinUI;

            public abstract partial class SampleCanvasEffect : CanvasEffect
            {
                public string {|CMPSD2DWINUI0008:Name|}
                {
                    get => field;
                    set => SetPropertyAndInvalidateEffectGraph(ref field, value);
                }
            }
            """;

        await CSharpAnalyzerTest<UseGeneratedCanvasEffectPropertyOnSemiAutoPropertyAnalyzer>.VerifyAnalyzerAsync(source, languageVersion: LanguageVersion.Preview);
    }

    [TestMethod]
    public async Task UseGeneratedCanvasEffectPropertyOnSemiAutoPropertyAnalyzer_ValidProperty_WithModifiers_Warns()
    {
        const string source = """
            using ComputeSharp.D2D1.WinUI;

            public abstract partial class SampleCanvasEffect : CanvasEffect
            {
                public new string {|CMPSD2DWINUI0008:Name|}
                {
                    get => field;
                    private set => SetPropertyAndInvalidateEffectGraph(ref field, value);
                }
            }
            """;

        await CSharpAnalyzerTest<UseGeneratedCanvasEffectPropertyOnSemiAutoPropertyAnalyzer>.VerifyAnalyzerAsync(source, languageVersion: LanguageVersion.Preview);
    }

    [TestMethod]
    public async Task UseGeneratedCanvasEffectPropertyOnSemiAutoPropertyAnalyzer_ValidProperty_WithBlocks_Warns()
    {
        const string source = """
            using ComputeSharp.D2D1.WinUI;

            public abstract partial class SampleCanvasEffect : CanvasEffect
            {
                public new string {|CMPSD2DWINUI0008:Name|}
                {
                    get
                    {
                        return field;
                    }
                    private set
                    {
                        SetPropertyAndInvalidateEffectGraph(ref field, value);
                    }
                }
            }
            """;

        await CSharpAnalyzerTest<UseGeneratedCanvasEffectPropertyOnSemiAutoPropertyAnalyzer>.VerifyAnalyzerAsync(source, languageVersion: LanguageVersion.Preview);
    }
}