extern alias Core;
extern alias D2D1;

using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharpCodeFixTest = Microsoft.CodeAnalysis.CSharp.Testing.CSharpCodeFixTest<
    ComputeSharp.D2D1.SourceGenerators.MissingPixelShaderDescriptorOnPixelShaderAnalyzer,
    ComputeSharp.D2D1.CodeFixers.MissingPixelShaderDescriptorOnPixelShaderCodeFixer,
    Microsoft.CodeAnalysis.Testing.Verifiers.MSTestVerifier>;

namespace ComputeSharp.D2D1.Tests.SourceGenerators;

[TestClass]
public class Test_MissingPixelShaderDescriptorOnPixelShaderCodeFixer
{
    [TestMethod]
    public async Task FileContainsComputeSharpD2D1UsingDirective()
    {
        string original = """
            using ComputeSharp.D2D1;

            partial struct {|CMPSD2D0065:MyShader|} : ID2D1PixelShader
            {
                public ComputeSharp.Float4 Execute() => 0;
            }
            """;

        string @fixed = """
            using ComputeSharp.D2D1;

            [D2DGeneratedPixelShaderDescriptor]
            partial struct MyShader : ID2D1PixelShader
            {
                public ComputeSharp.Float4 Execute() => 0;
            }
            """;

        CSharpCodeFixTest test = new()
        {
            TestCode = original,
            FixedCode = @fixed,
            ReferenceAssemblies = ReferenceAssemblies.Net.Net80
        };

        test.TestState.AdditionalReferences.Add(typeof(Core::ComputeSharp.Float4).Assembly);
        test.TestState.AdditionalReferences.Add(typeof(D2D1::ComputeSharp.D2D1.ID2D1PixelShader).Assembly);
        test.TestState.AnalyzerConfigFiles.Add(("/.editorconfig", "[*]\nend_of_line = lf"));

        await test.RunAsync();
    }

    [TestMethod]
    public async Task FileContainsComputeSharpD2D1UsingDirective_MultipleOccurrences()
    {
        string original = """
            using System;
            using ComputeSharp.D2D1;

            partial struct {|CMPSD2D0065:MyShader1|} : ID2D1PixelShader
            {
                public ComputeSharp.Float4 Execute() => 0;
            }

            [D2DInputCount(0)]
            [TestAttribute]
            partial struct {|CMPSD2D0065:MyShader2|} : ID2D1PixelShader
            {
                public ComputeSharp.Float4 Execute() => 0;
            }

            public class TestAttribute : Attribute
            {
            }
            """;

        string @fixed = """
            using System;
            using ComputeSharp.D2D1;

            [D2DGeneratedPixelShaderDescriptor]
            partial struct MyShader1 : ID2D1PixelShader
            {
                public ComputeSharp.Float4 Execute() => 0;
            }

            [D2DInputCount(0)]
            [D2DGeneratedPixelShaderDescriptor]
            [TestAttribute]
            partial struct MyShader2 : ID2D1PixelShader
            {
                public ComputeSharp.Float4 Execute() => 0;
            }

            public class TestAttribute : Attribute
            {
            }
            """;

        CSharpCodeFixTest test = new()
        {
            TestCode = original,
            FixedCode = @fixed,
            ReferenceAssemblies = ReferenceAssemblies.Net.Net80
        };

        test.TestState.AdditionalReferences.Add(typeof(Core::ComputeSharp.Float4).Assembly);
        test.TestState.AdditionalReferences.Add(typeof(D2D1::ComputeSharp.D2D1.ID2D1PixelShader).Assembly);
        test.TestState.AnalyzerConfigFiles.Add(("/.editorconfig", "[*]\nend_of_line = lf"));

        await test.RunAsync();
    }

    [TestMethod]
    public async Task FileDoesNotContainComputeSharpD2D1UsingDirective()
    {
        string original = """
            partial struct {|CMPSD2D0065:MyShader|} : ComputeSharp.D2D1.ID2D1PixelShader
            {
                public ComputeSharp.Float4 Execute() => 0;
            }
            """;

        string @fixed = """
            using ComputeSharp.D2D1;

            [D2DGeneratedPixelShaderDescriptor]
            partial struct MyShader : ComputeSharp.D2D1.ID2D1PixelShader
            {
                public ComputeSharp.Float4 Execute() => 0;
            }
            """;

        CSharpCodeFixTest test = new()
        {
            TestCode = original,
            FixedCode = @fixed,
            ReferenceAssemblies = ReferenceAssemblies.Net.Net80
        };

        test.TestState.AdditionalReferences.Add(typeof(Core::ComputeSharp.Float4).Assembly);
        test.TestState.AdditionalReferences.Add(typeof(D2D1::ComputeSharp.D2D1.ID2D1PixelShader).Assembly);
        test.TestState.AnalyzerConfigFiles.Add(("/.editorconfig", "[*]\nend_of_line = lf"));

        await test.RunAsync();
    }

    [TestMethod]
    public async Task FileDoesNotContainComputeSharpD2D1UsingDirective_MultipleOccurrences()
    {
        string original = """
            using System;

            partial struct {|CMPSD2D0065:MyShader1|} : ComputeSharp.D2D1.ID2D1PixelShader
            {
                public ComputeSharp.Float4 Execute() => 0;
            }

            [TestAttribute]
            partial struct {|CMPSD2D0065:MyShader2|} : ComputeSharp.D2D1.ID2D1PixelShader
            {
                public ComputeSharp.Float4 Execute() => 0;
            }

            public class TestAttribute : Attribute
            {
            }
            """;

        string @fixed = """
            using System;
            using ComputeSharp.D2D1;

            [D2DGeneratedPixelShaderDescriptor]
            partial struct MyShader1 : ComputeSharp.D2D1.ID2D1PixelShader
            {
                public ComputeSharp.Float4 Execute() => 0;
            }

            [D2DGeneratedPixelShaderDescriptor]
            [TestAttribute]
            partial struct MyShader2 : ComputeSharp.D2D1.ID2D1PixelShader
            {
                public ComputeSharp.Float4 Execute() => 0;
            }

            public class TestAttribute : Attribute
            {
            }
            """;

        CSharpCodeFixTest test = new()
        {
            TestCode = original,
            FixedCode = @fixed,
            ReferenceAssemblies = ReferenceAssemblies.Net.Net80
        };

        test.TestState.AdditionalReferences.Add(typeof(Core::ComputeSharp.Float4).Assembly);
        test.TestState.AdditionalReferences.Add(typeof(D2D1::ComputeSharp.D2D1.ID2D1PixelShader).Assembly);
        test.TestState.AnalyzerConfigFiles.Add(("/.editorconfig", "[*]\nend_of_line = lf"));

        await test.RunAsync();
    }

    [TestMethod]
    public async Task InsertsAttributeAfterAllOtherD2DAttributes()
    {
        string original = """
            using ComputeSharp.D2D1;

            [D2DInputCount(1)]
            [D2DInputSimple(0)]
            partial struct {|CMPSD2D0065:MyShader|} : ID2D1PixelShader
            {
                public ComputeSharp.Float4 Execute() => 0;
            }
            """;

        string @fixed = """
            using ComputeSharp.D2D1;

            [D2DInputCount(1)]
            [D2DInputSimple(0)]
            [D2DGeneratedPixelShaderDescriptor]
            partial struct MyShader : ID2D1PixelShader
            {
                public ComputeSharp.Float4 Execute() => 0;
            }
            """;

        CSharpCodeFixTest test = new()
        {
            TestCode = original,
            FixedCode = @fixed,
            ReferenceAssemblies = ReferenceAssemblies.Net.Net80
        };

        test.TestState.AdditionalReferences.Add(typeof(Core::ComputeSharp.Float4).Assembly);
        test.TestState.AdditionalReferences.Add(typeof(D2D1::ComputeSharp.D2D1.ID2D1PixelShader).Assembly);
        test.TestState.AnalyzerConfigFiles.Add(("/.editorconfig", "[*]\nend_of_line = lf"));

        await test.RunAsync();
    }

    [TestMethod]
    public async Task InsertsAttributeAfterAllOtherD2DAttributes_WithOtherNonD2DAttribute()
    {
        string original = """
            using System;
            using ComputeSharp.D2D1;

            [D2DInputCount(1)]
            [D2DInputSimple(0)]
            [Test]
            partial struct {|CMPSD2D0065:MyShader|} : ID2D1PixelShader
            {
                public ComputeSharp.Float4 Execute() => 0;
            }

            public class TestAttribute : Attribute
            {
            }
            """;

        string @fixed = """
            using System;
            using ComputeSharp.D2D1;

            [D2DInputCount(1)]
            [D2DInputSimple(0)]
            [D2DGeneratedPixelShaderDescriptor]
            [Test]
            partial struct MyShader : ID2D1PixelShader
            {
                public ComputeSharp.Float4 Execute() => 0;
            }

            public class TestAttribute : Attribute
            {
            }
            """;

        CSharpCodeFixTest test = new()
        {
            TestCode = original,
            FixedCode = @fixed,
            ReferenceAssemblies = ReferenceAssemblies.Net.Net80
        };

        test.TestState.AdditionalReferences.Add(typeof(Core::ComputeSharp.Float4).Assembly);
        test.TestState.AdditionalReferences.Add(typeof(D2D1::ComputeSharp.D2D1.ID2D1PixelShader).Assembly);
        test.TestState.AnalyzerConfigFiles.Add(("/.editorconfig", "[*]\nend_of_line = lf"));

        await test.RunAsync();
    }

    [TestMethod]
    public async Task InsertsAttributeAfterAllOtherD2DAttributes_WithFakeAttributes()
    {
        string original = """
            using System;
            using ComputeSharp.D2D1;

            [D2DInputCount(2)]
            [D2DInputSimple(0)]
            [ComputeSharp.D2D1.D2DInputSimple(1)]
            [D2DFake]
            [Test]
            partial struct {|CMPSD2D0065:MyShader|} : ID2D1PixelShader
            {
                public ComputeSharp.Float4 Execute() => 0;
            }

            public class TestAttribute : Attribute
            {
            }

            public class D2DFakeAttribute : Attribute
            {
            }
            """;

        string @fixed = """
            using System;
            using ComputeSharp.D2D1;

            [D2DInputCount(2)]
            [D2DInputSimple(0)]
            [ComputeSharp.D2D1.D2DInputSimple(1)]
            [D2DGeneratedPixelShaderDescriptor]
            [D2DFake]
            [Test]
            partial struct {|CMPSD2D0065:MyShader|} : ID2D1PixelShader
            {
                public ComputeSharp.Float4 Execute() => 0;
            }

            public class TestAttribute : Attribute
            {
            }

            public class D2DFakeAttribute : Attribute
            {
            }
            """;

        CSharpCodeFixTest test = new()
        {
            TestCode = original,
            FixedCode = @fixed,
            ReferenceAssemblies = ReferenceAssemblies.Net.Net80
        };

        test.TestState.AdditionalReferences.Add(typeof(Core::ComputeSharp.Float4).Assembly);
        test.TestState.AdditionalReferences.Add(typeof(D2D1::ComputeSharp.D2D1.ID2D1PixelShader).Assembly);
        test.TestState.AnalyzerConfigFiles.Add(("/.editorconfig", "[*]\nend_of_line = lf"));

        await test.RunAsync();
    }
}