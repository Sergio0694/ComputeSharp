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

        await CSharpAnalyzerWithLanguageVersionTest<InvalidGeneratedCanvasEffectPropertyDeclarationAnalyzer>.VerifyAnalyzerAsync(source, languageVersion: LanguageVersion.Preview);
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

        await CSharpAnalyzerWithLanguageVersionTest<InvalidGeneratedCanvasEffectPropertyDeclarationAnalyzer>.VerifyAnalyzerAsync(source, languageVersion: LanguageVersion.Preview);
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

        await CSharpAnalyzerWithLanguageVersionTest<InvalidGeneratedCanvasEffectPropertyDeclarationAnalyzer>.VerifyAnalyzerAsync(source, languageVersion: LanguageVersion.Preview);
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

        await CSharpAnalyzerWithLanguageVersionTest<InvalidGeneratedCanvasEffectPropertyDeclarationAnalyzer>.VerifyAnalyzerAsync(source, languageVersion: LanguageVersion.Preview);
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

        await CSharpAnalyzerWithLanguageVersionTest<InvalidGeneratedCanvasEffectPropertyDeclarationAnalyzer>.VerifyAnalyzerAsync(source, languageVersion: LanguageVersion.Preview);
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

        await CSharpAnalyzerWithLanguageVersionTest<InvalidGeneratedCanvasEffectPropertyDeclarationAnalyzer>.VerifyAnalyzerAsync(source, languageVersion: LanguageVersion.Preview);
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

        await CSharpAnalyzerWithLanguageVersionTest<InvalidGeneratedCanvasEffectPropertyDeclarationAnalyzer>.VerifyAnalyzerAsync(source, languageVersion: LanguageVersion.Preview);
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

        await CSharpAnalyzerWithLanguageVersionTest<InvalidGeneratedCanvasEffectPropertyDeclarationAnalyzer>.VerifyAnalyzerAsync(source, languageVersion: LanguageVersion.Preview);
    }

    [TestMethod]
    public async Task RequireCSharpLanguageVersionPreviewAnalyzer_LanguageVersionIsNotPreview_Warns()
    {
        const string source = """
            using ComputeSharp.D2D1.WinUI;

            public abstract partial class MyEffect : CanvasEffect
            {
                [{|CMPSD2DWINUI0007:GeneratedCanvasEffectProperty|}]
                public int Number { get; set; }
            }
            """;

        await CSharpAnalyzerWithLanguageVersionTest<RequireCSharpLanguageVersionPreviewAnalyzer>.VerifyAnalyzerAsync(source);
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

        await CSharpAnalyzerWithLanguageVersionTest<RequireCSharpLanguageVersionPreviewAnalyzer>.VerifyAnalyzerAsync(source, languageVersion: LanguageVersion.Preview);
    }

    [TestMethod]
    public async Task InvalidPropertyNonNullableDeclarationAnalyzer_NoAttribute_DoesNotWarn()
    {
        const string source = """
            using ComputeSharp.D2D1.WinUI;

            #nullable enable

            public abstract partial class MyEffect : CanvasEffect
            {
                public string Name { get; set; }
            }
            """;

        await CSharpAnalyzerWithLanguageVersionTest<InvalidGeneratedCanvasEffectPropertyNonNullableDeclarationAnalyzer>.VerifyAnalyzerAsync(source, languageVersion: LanguageVersion.Preview);
    }

    [TestMethod]
    [DataRow("int")]
    [DataRow("int?")]
    [DataRow("string?")]
    public async Task InvalidPropertyNonNullableDeclarationAnalyzer_NullableOrNotApplicableType_DoesNotWarn(string propertyType)
    {
        string source = $$"""
            using ComputeSharp.D2D1.WinUI;

            #nullable enable

            public abstract partial class MyEffect : CanvasEffect
            {
                [GeneratedCanvasEffectProperty]
                public partial {{propertyType}} {|CS9248:Name|} { get; set; }
            }
            """;

        await CSharpAnalyzerWithLanguageVersionTest<InvalidGeneratedCanvasEffectPropertyNonNullableDeclarationAnalyzer>.VerifyAnalyzerAsync(source, languageVersion: LanguageVersion.Preview);
    }

    [TestMethod]
    public async Task InvalidPropertyNonNullableDeclarationAnalyzer_NotNullableType_WithMaybeNullAttribute_DoesNotWarn()
    {
        string source = $$"""
            using System.Diagnostics.CodeAnalysis;
            using ComputeSharp.D2D1.WinUI;

            #nullable enable

            public abstract partial class MyEffect : CanvasEffect
            {
                [GeneratedCanvasEffectProperty]
                [MaybeNull]
                public partial string {|CS9248:Name|} { get; set; }
            }
            """;

        await CSharpAnalyzerWithLanguageVersionTest<InvalidGeneratedCanvasEffectPropertyNonNullableDeclarationAnalyzer>.VerifyAnalyzerAsync(source, languageVersion: LanguageVersion.Preview);
    }

    [TestMethod]
    public async Task InvalidPropertyNonNullableDeclarationAnalyzer_NotNullableType_Required_DoesNotWarn()
    {
        string source = $$"""
            using ComputeSharp.D2D1.WinUI;

            #nullable enable

            public abstract partial class MyEffect : CanvasEffect
            {
                [GeneratedCanvasEffectProperty]
                public required partial string {|CS9248:Name|} { get; set; }
            }
            """;

        await CSharpAnalyzerWithLanguageVersionTest<InvalidGeneratedCanvasEffectPropertyNonNullableDeclarationAnalyzer>.VerifyAnalyzerAsync(source, languageVersion: LanguageVersion.Preview);
    }

    [TestMethod]
    public async Task InvalidPropertyNonNullableDeclarationAnalyzer_NotNullableType_NullableDisabled_DoesNotWarn()
    {
        string source = $$"""
            using ComputeSharp.D2D1.WinUI;

            public abstract partial class MyEffect : CanvasEffect
            {
                [GeneratedCanvasEffectProperty]
                public required partial string {|CS9248:Name|} { get; set; }
            }
            """;

        await CSharpAnalyzerWithLanguageVersionTest<InvalidGeneratedCanvasEffectPropertyNonNullableDeclarationAnalyzer>.VerifyAnalyzerAsync(source, languageVersion: LanguageVersion.Preview);
    }

    [TestMethod]
    public async Task InvalidPropertyNonNullableDeclarationAnalyzer_NotNullableType_Warns()
    {
        string source = $$"""
            using ComputeSharp.D2D1.WinUI;

            #nullable enable

            public abstract partial class MyEffect : CanvasEffect
            {
                [{|CMPSD2DWINUI0008:GeneratedCanvasEffectProperty|}]
                public partial string {|CS9248:Name|} { get; set; }
            }
            """;

        await CSharpAnalyzerWithLanguageVersionTest<InvalidGeneratedCanvasEffectPropertyNonNullableDeclarationAnalyzer>.VerifyAnalyzerAsync(source, languageVersion: LanguageVersion.Preview);
    }
}