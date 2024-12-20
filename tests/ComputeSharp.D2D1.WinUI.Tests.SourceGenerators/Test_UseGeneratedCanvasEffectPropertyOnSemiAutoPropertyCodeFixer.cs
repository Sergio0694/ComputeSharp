extern alias Core;
extern alias D2D1_WinUI;
extern alias D2D1;

using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.UI.Xaml.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using CSharpCodeFixTest = ComputeSharp.Tests.SourceGenerators.Helpers.CSharpCodeFixTest<
    ComputeSharp.D2D1.WinUI.SourceGenerators.UseGeneratedCanvasEffectPropertyOnSemiAutoPropertyAnalyzer,
    ComputeSharp.D2D1.WinUI.SourceGenerators.UseGeneratedCanvasEffectPropertyOnSemiAutoPropertyCodeFixer>;

namespace ComputeSharp.D2D1.WinUI.Tests.SourceGenerators;

[TestClass]
public class Test_UseGeneratedCanvasEffectPropertyOnSemiAutoPropertyCodeFixer
{
    [TestMethod]
    [DataRow("get;")]
    [DataRow("get => field;")]
    public async Task SimpleProperty_ImplicitInvalidationType(string getAccessor)
    {
        string original = $$"""
            using ComputeSharp.D2D1.WinUI;

            namespace MyApp;

            public abstract class MyEffect : CanvasEffect
            {
                public string? [|Name|]
                {
                    {{getAccessor}}
                    set => SetPropertyAndInvalidateEffectGraph(ref field, value);
                }
            }
            """;

        const string @fixed = """
            using ComputeSharp.D2D1.WinUI;

            namespace MyApp;

            public abstract partial class MyEffect : CanvasEffect
            {
                [GeneratedCanvasEffectProperty]
                public partial string? {|CS9248:Name|} { get; set; }
            }
            """;

        CSharpCodeFixTest test = new(LanguageVersion.Preview)
        {
            TestCode = original,
            FixedCode = @fixed,
            ReferenceAssemblies = ReferenceAssemblies.Net.Net80,
            TestState = { AdditionalReferences =
            {
                MetadataReference.CreateFromFile(typeof(Point).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(ApplicationView).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Button).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(D2D1_WinUI::ComputeSharp.D2D1.WinUI.CanvasEffect).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(D2D1::ComputeSharp.D2D1.ID2D1PixelShader).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Core::ComputeSharp.Float4).Assembly.Location)
            } }
        };

        await test.RunAsync();
    }

    [TestMethod]
    [DataRow("get;")]
    [DataRow("get => field;")]
    public async Task SimpleProperty_InvalidationTypeUpdate(string getAccessor)
    {
        string original = $$"""
            using ComputeSharp.D2D1.WinUI;

            namespace MyApp;

            public abstract class MyEffect : CanvasEffect
            {
                public string? [|Name|]
                {
                    {{getAccessor}}
                    set => SetPropertyAndInvalidateEffectGraph(ref field, value, CanvasEffectInvalidationType.Update);
                }
            }
            """;

        const string @fixed = """
            using ComputeSharp.D2D1.WinUI;

            namespace MyApp;

            public abstract partial class MyEffect : CanvasEffect
            {
                [GeneratedCanvasEffectProperty]
                public partial string? {|CS9248:Name|} { get; set; }
            }
            """;

        CSharpCodeFixTest test = new(LanguageVersion.Preview)
        {
            TestCode = original,
            FixedCode = @fixed,
            ReferenceAssemblies = ReferenceAssemblies.Net.Net80,
            TestState = { AdditionalReferences =
            {
                MetadataReference.CreateFromFile(typeof(Point).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(ApplicationView).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Button).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(D2D1_WinUI::ComputeSharp.D2D1.WinUI.CanvasEffect).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(D2D1::ComputeSharp.D2D1.ID2D1PixelShader).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Core::ComputeSharp.Float4).Assembly.Location)
            } }
        };

        await test.RunAsync();
    }

    [TestMethod]
    [DataRow("get;")]
    [DataRow("get => field;")]
    public async Task SimpleProperty_InvalidationTypeCreation(string getAccessor)
    {
        string original = $$"""
            using ComputeSharp.D2D1.WinUI;

            namespace MyApp;

            public abstract class MyEffect : CanvasEffect
            {
                public string? [|Name|]
                {
                    {{getAccessor}}
                    set => SetPropertyAndInvalidateEffectGraph(ref field, value, CanvasEffectInvalidationType.Creation);
                }
            }
            """;

        const string @fixed = """
            using ComputeSharp.D2D1.WinUI;

            namespace MyApp;

            public abstract partial class MyEffect : CanvasEffect
            {
                [GeneratedCanvasEffectProperty(CanvasEffectInvalidationType.Creation)]
                public partial string? {|CS9248:Name|} { get; set; }
            }
            """;

        CSharpCodeFixTest test = new(LanguageVersion.Preview)
        {
            TestCode = original,
            FixedCode = @fixed,
            ReferenceAssemblies = ReferenceAssemblies.Net.Net80,
            TestState = { AdditionalReferences =
            {
                MetadataReference.CreateFromFile(typeof(Point).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(ApplicationView).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Button).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(D2D1_WinUI::ComputeSharp.D2D1.WinUI.CanvasEffect).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(D2D1::ComputeSharp.D2D1.ID2D1PixelShader).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Core::ComputeSharp.Float4).Assembly.Location)
            } }
        };

        await test.RunAsync();
    }

    [TestMethod]
    public async Task SimpleProperty_WithMissingUsingDirective()
    {
        const string original = """
            namespace MyApp;

            public abstract class MyEffect : ComputeSharp.D2D1.WinUI.CanvasEffect
            {
                public string? [|Name|]
                {
                    get;
                    set => SetPropertyAndInvalidateEffectGraph(ref field, value);
                }
            }
            """;

        const string @fixed = """
            using ComputeSharp.D2D1.WinUI;

            namespace MyApp;

            public abstract partial class MyEffect : ComputeSharp.D2D1.WinUI.CanvasEffect
            {
                [GeneratedCanvasEffectProperty]
                public partial string? {|CS9248:Name|} { get; set; }
            }
            """;

        CSharpCodeFixTest test = new(LanguageVersion.Preview)
        {
            TestCode = original,
            FixedCode = @fixed,
            ReferenceAssemblies = ReferenceAssemblies.Net.Net80,
            TestState = { AdditionalReferences =
            {
                MetadataReference.CreateFromFile(typeof(Point).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(ApplicationView).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Button).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(D2D1_WinUI::ComputeSharp.D2D1.WinUI.CanvasEffect).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(D2D1::ComputeSharp.D2D1.ID2D1PixelShader).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Core::ComputeSharp.Float4).Assembly.Location)
            } }
        };

        await test.RunAsync();
    }

    [TestMethod]
    public async Task SimpleProperty_WithLeadingTrivia()
    {
        const string original = """
            using ComputeSharp.D2D1.WinUI;

            namespace MyApp;

            public abstract class MyEffect : CanvasEffect
            {
                /// <summary>
                /// This is a property.
                /// </summary>
                public string? [|Name|]
                {
                    get;
                    set => SetPropertyAndInvalidateEffectGraph(ref field, value);
                }
            }
            """;

        const string @fixed = """
            using ComputeSharp.D2D1.WinUI;

            namespace MyApp;

            public abstract partial class MyEffect : CanvasEffect
            {
                /// <summary>
                /// This is a property.
                /// </summary>
                [GeneratedCanvasEffectProperty]
                public partial string? {|CS9248:Name|} { get; set; }
            }
            """;

        CSharpCodeFixTest test = new(LanguageVersion.Preview)
        {
            TestCode = original,
            FixedCode = @fixed,
            ReferenceAssemblies = ReferenceAssemblies.Net.Net80,
            TestState = { AdditionalReferences =
            {
                MetadataReference.CreateFromFile(typeof(Point).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(ApplicationView).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Button).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(D2D1_WinUI::ComputeSharp.D2D1.WinUI.CanvasEffect).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(D2D1::ComputeSharp.D2D1.ID2D1PixelShader).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Core::ComputeSharp.Float4).Assembly.Location)
            } }
        };

        await test.RunAsync();
    }

    [TestMethod]
    public async Task SimpleProperty_WithLeadingTrivia_AndAttribute()
    {
        const string original = """
            using System;
            using ComputeSharp.D2D1.WinUI;

            namespace MyApp;

            public abstract class MyEffect : CanvasEffect
            {
                /// <summary>
                /// This is a property.
                /// </summary>
                [Test("Targeting property")]
                [field: Test("Targeting field")]
                public string? [|Name|]
                {
                    get;
                    set => SetPropertyAndInvalidateEffectGraph(ref field, value);
                }
            }

            public class TestAttribute(string text) : Attribute;
            """;

        const string @fixed = """
            using System;
            using ComputeSharp.D2D1.WinUI;

            namespace MyApp;

            public abstract partial class MyEffect : CanvasEffect
            {
                /// <summary>
                /// This is a property.
                /// </summary>
                [GeneratedCanvasEffectProperty]
                [Test("Targeting property")]
                [field: Test("Targeting field")]
                public partial string? {|CS9248:Name|} { get; set; }
            }

            public class TestAttribute(string text) : Attribute;
            """;

        CSharpCodeFixTest test = new(LanguageVersion.Preview)
        {
            TestCode = original,
            FixedCode = @fixed,
            ReferenceAssemblies = ReferenceAssemblies.Net.Net80,
            TestState = { AdditionalReferences =
            {
                MetadataReference.CreateFromFile(typeof(Point).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(ApplicationView).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Button).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(D2D1_WinUI::ComputeSharp.D2D1.WinUI.CanvasEffect).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(D2D1::ComputeSharp.D2D1.ID2D1PixelShader).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Core::ComputeSharp.Float4).Assembly.Location)
            } }
        };

        await test.RunAsync();
    }

    [TestMethod]
    public async Task SimpleProperty_Multiple()
    {
        const string original = """
            using ComputeSharp.D2D1.WinUI;

            namespace MyApp;

            public abstract class MyEffect : CanvasEffect
            {
                public string? [|FirstName|]
                {
                    get;
                    set => SetPropertyAndInvalidateEffectGraph(ref field, value);
                }

                public string? [|LastName|]
                {
                    get;
                    set => SetPropertyAndInvalidateEffectGraph(ref field, value);
                }
            }
            """;

        const string @fixed = """
            using ComputeSharp.D2D1.WinUI;

            namespace MyApp;

            public abstract partial class MyEffect : CanvasEffect
            {
                [GeneratedCanvasEffectProperty]
                public partial string? {|CS9248:FirstName|} { get; set; }

                [GeneratedCanvasEffectProperty]
                public partial string? {|CS9248:LastName|} { get; set; }
            }
            """;

        CSharpCodeFixTest test = new(LanguageVersion.Preview)
        {
            TestCode = original,
            FixedCode = @fixed,
            ReferenceAssemblies = ReferenceAssemblies.Net.Net80,
            TestState = { AdditionalReferences =
            {
                MetadataReference.CreateFromFile(typeof(Point).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(ApplicationView).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Button).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(D2D1_WinUI::ComputeSharp.D2D1.WinUI.CanvasEffect).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(D2D1::ComputeSharp.D2D1.ID2D1PixelShader).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Core::ComputeSharp.Float4).Assembly.Location)
            } }
        };

        await test.RunAsync();
    }

    [TestMethod]
    public async Task SimpleProperty_Multiple_OnlyTriggersOnFirstOne()
    {
        const string original = """
            using ComputeSharp.D2D1.WinUI;

            namespace MyApp;

            public abstract class MyEffect : CanvasEffect
            {
                public string? [|FirstName|]
                {
                    get;
                    set => SetPropertyAndInvalidateEffectGraph(ref field, value);
                }

                private string? lastName;

                public string? LastName
                {
                    get => this.lastName;
                    set => SetPropertyAndInvalidateEffectGraph(ref this.lastName, value);
                }
            }
            """;

        const string @fixed = """
            using ComputeSharp.D2D1.WinUI;

            namespace MyApp;

            public abstract partial class MyEffect : CanvasEffect
            {
                [GeneratedCanvasEffectProperty]
                public partial string? {|CS9248:FirstName|} { get; set; }

                private string? lastName;

                public string? LastName
                {
                    get => this.lastName;
                    set => SetPropertyAndInvalidateEffectGraph(ref this.lastName, value);
                }
            }
            """;

        CSharpCodeFixTest test = new(LanguageVersion.Preview)
        {
            TestCode = original,
            FixedCode = @fixed,
            ReferenceAssemblies = ReferenceAssemblies.Net.Net80,
            TestState = { AdditionalReferences =
            {
                MetadataReference.CreateFromFile(typeof(Point).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(ApplicationView).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Button).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(D2D1_WinUI::ComputeSharp.D2D1.WinUI.CanvasEffect).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(D2D1::ComputeSharp.D2D1.ID2D1PixelShader).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Core::ComputeSharp.Float4).Assembly.Location)
            } }
        };

        await test.RunAsync();
    }

    [TestMethod]
    public async Task SimpleProperty_Multiple_OnlyTriggersOnSecondOne()
    {
        const string original = """
            using ComputeSharp.D2D1.WinUI;

            namespace MyApp;

            public abstract class MyEffect : CanvasEffect
            {
                private string? firstName;

                public string? FirstName
                {
                    get => this.firstName;
                    set => SetPropertyAndInvalidateEffectGraph(ref this.firstName, value);
                }

                public string? [|LastName|]
                {
                    get;
                    set => SetPropertyAndInvalidateEffectGraph(ref field, value);
                }
            }
            """;

        const string @fixed = """
            using ComputeSharp.D2D1.WinUI;

            namespace MyApp;

            public abstract partial class MyEffect : CanvasEffect
            {
                private string? firstName;

                public string? FirstName
                {
                    get => this.firstName;
                    set => SetPropertyAndInvalidateEffectGraph(ref this.firstName, value);
                }

                [GeneratedCanvasEffectProperty]
                public partial string? {|CS9248:LastName|} { get; set; }
            }
            """;

        CSharpCodeFixTest test = new(LanguageVersion.Preview)
        {
            TestCode = original,
            FixedCode = @fixed,
            ReferenceAssemblies = ReferenceAssemblies.Net.Net80,
            TestState = { AdditionalReferences =
            {
                MetadataReference.CreateFromFile(typeof(Point).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(ApplicationView).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Button).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(D2D1_WinUI::ComputeSharp.D2D1.WinUI.CanvasEffect).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(D2D1::ComputeSharp.D2D1.ID2D1PixelShader).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Core::ComputeSharp.Float4).Assembly.Location)
            } }
        };

        await test.RunAsync();
    }

    [TestMethod]
    public async Task SimpleProperty_WithinPartialType()
    {
        const string original = """
            using ComputeSharp.D2D1.WinUI;

            namespace MyApp;

            public abstract partial class MyEffect : CanvasEffect
            {
                public string? [|Name|]
                {
                    get;
                    set => SetPropertyAndInvalidateEffectGraph(ref field, value);
                }
            }
            """;

        const string @fixed = """
            using ComputeSharp.D2D1.WinUI;

            namespace MyApp;

            public abstract partial class MyEffect : CanvasEffect
            {
                [GeneratedCanvasEffectProperty]
                public partial string? {|CS9248:Name|} { get; set; }
            }
            """;

        CSharpCodeFixTest test = new(LanguageVersion.Preview)
        {
            TestCode = original,
            FixedCode = @fixed,
            ReferenceAssemblies = ReferenceAssemblies.Net.Net80,
            TestState = { AdditionalReferences =
            {
                MetadataReference.CreateFromFile(typeof(Point).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(ApplicationView).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Button).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(D2D1_WinUI::ComputeSharp.D2D1.WinUI.CanvasEffect).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(D2D1::ComputeSharp.D2D1.ID2D1PixelShader).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Core::ComputeSharp.Float4).Assembly.Location)
            } }
        };

        await test.RunAsync();
    }

    [TestMethod]
    public async Task SimpleProperty_WithMissingUsingDirective_Multiple()
    {
        const string original = """
            namespace MyApp;

            public abstract class MyEffect : ComputeSharp.D2D1.WinUI.CanvasEffect
            {
                public string? [|FirstName|]
                {
                    get;
                    set => SetPropertyAndInvalidateEffectGraph(ref field, value);
                }

                public string? [|LastName|]
                {
                    get;
                    set => SetPropertyAndInvalidateEffectGraph(ref field, value);
                }
            }
            """;

        const string @fixed = """
            using ComputeSharp.D2D1.WinUI;

            namespace MyApp;

            public abstract partial class MyEffect : ComputeSharp.D2D1.WinUI.CanvasEffect
            {
                [GeneratedCanvasEffectProperty]
                public partial string? {|CS9248:FirstName|} { get; set; }

                [GeneratedCanvasEffectProperty]
                public partial string? {|CS9248:LastName|} { get; set; }
            }
            """;

        CSharpCodeFixTest test = new(LanguageVersion.Preview)
        {
            TestCode = original,
            FixedCode = @fixed,
            ReferenceAssemblies = ReferenceAssemblies.Net.Net80,
            TestState = { AdditionalReferences =
            {
                MetadataReference.CreateFromFile(typeof(Point).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(ApplicationView).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Button).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(D2D1_WinUI::ComputeSharp.D2D1.WinUI.CanvasEffect).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(D2D1::ComputeSharp.D2D1.ID2D1PixelShader).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Core::ComputeSharp.Float4).Assembly.Location)
            } }
        };

        await test.RunAsync();
    }

    [TestMethod]
    public async Task SimpleProperty_Multiple_MixedScenario()
    {
        const string original = """
            using System;
            using ComputeSharp.D2D1.WinUI;

            namespace MyApp;

            public abstract class MyEffect : CanvasEffect
            {
                [Test("This is an attribute")]
                public string [|Prop1|]
                {
                    get => field;
                    set => SetPropertyAndInvalidateEffectGraph(ref field, value);
                }

                // Single comment
                public string [|Prop2|]
                {
                    get => field;
                    set => SetPropertyAndInvalidateEffectGraph(ref field, value);
                }

                /// <summary>
                /// This is a property.
                /// </summary>
                public string [|Prop3|]
                {
                    get => field;
                    set => SetPropertyAndInvalidateEffectGraph(ref field, value);
                }

                /// <summary>
                /// This is another property.
                /// </summary>
                [Test("Another attribute")]
                public string [|Prop4|]
                {
                    get => field;
                    set => SetPropertyAndInvalidateEffectGraph(ref field, value);
                }

                // Some other single comment
                [Test("Yet another attribute")]
                public string [|Prop5|]
                {
                    get => field;
                    set => SetPropertyAndInvalidateEffectGraph(ref field, value);
                }

                [Test("Attribute without trivia")]
                public string [|Prop6|]
                {
                    get => field;
                    set => SetPropertyAndInvalidateEffectGraph(ref field, value);
                }

                public string [|Prop7|]
                {
                    get => field;
                    set => SetPropertyAndInvalidateEffectGraph(ref field, value);
                }
            }

            public class TestAttribute(string text) : Attribute;
            """;

        const string @fixed = """            
            using System;
            using ComputeSharp.D2D1.WinUI;

            namespace MyApp;

            public abstract partial class MyEffect : CanvasEffect
            {
                [GeneratedCanvasEffectProperty]
                [Test("This is an attribute")]
                public partial string {|CS9248:Prop1|} { get; set; }

                // Single comment
                [GeneratedCanvasEffectProperty]
                public partial string {|CS9248:Prop2|} { get; set; }

                /// <summary>
                /// This is a property.
                /// </summary>
                [GeneratedCanvasEffectProperty]
                public partial string {|CS9248:Prop3|} { get; set; }

                /// <summary>
                /// This is another property.
                /// </summary>
                [GeneratedCanvasEffectProperty]
                [Test("Another attribute")]
                public partial string {|CS9248:Prop4|} { get; set; }

                // Some other single comment
                [GeneratedCanvasEffectProperty]
                [Test("Yet another attribute")]
                public partial string {|CS9248:Prop5|} { get; set; }

                [GeneratedCanvasEffectProperty]
                [Test("Attribute without trivia")]
                public partial string {|CS9248:Prop6|} { get; set; }

                [GeneratedCanvasEffectProperty]
                public partial string {|CS9248:Prop7|} { get; set; }
            }

            public class TestAttribute(string text) : Attribute;
            """;

        CSharpCodeFixTest test = new(LanguageVersion.Preview)
        {
            TestCode = original,
            FixedCode = @fixed,
            ReferenceAssemblies = ReferenceAssemblies.Net.Net80,
            TestState = { AdditionalReferences =
            {
                MetadataReference.CreateFromFile(typeof(Point).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(ApplicationView).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Button).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(D2D1_WinUI::ComputeSharp.D2D1.WinUI.CanvasEffect).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(D2D1::ComputeSharp.D2D1.ID2D1PixelShader).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Core::ComputeSharp.Float4).Assembly.Location)
            } }
        };

        await test.RunAsync();
    }
}
