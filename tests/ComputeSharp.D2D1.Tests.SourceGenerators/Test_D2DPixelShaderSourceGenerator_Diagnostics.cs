extern alias Core;
extern alias D2D1;

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Basic.Reference.Assemblies;
using ComputeSharp.D2D1.SourceGenerators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.D2D1.Tests.SourceGenerators;

[TestClass]
public class Test_D2DPixelShaderSourceGenerator_DIagnostics
{
    [TestMethod]
    public void MissingD2DRequiresDoublePrecisionSupportAttribute()
    {
        const string source = """
            using ComputeSharp;
            using ComputeSharp.D2D1;
            using float4 = global::ComputeSharp.Float4;

            namespace MyNamespace;

            [D2DInputCount(0)]
            [D2DShaderProfile(D2D1ShaderProfile.PixelShader50)]
            [D2DGeneratedPixelShaderDescriptor]
            internal readonly partial struct MyShader : ID2D1PixelShader
            {
                private readonly float time;

                public float4 Execute()
                {
                    return (float)(time * 2.0);
                }
            }
            """;

        VerifyGeneratedDiagnostics(source, "CMPSD2D0080");
    }

    [TestMethod]
    public void UnnecessaryD2DRequiresDoublePrecisionSupportAttribute()
    {
        const string source = """
            using ComputeSharp;
            using ComputeSharp.D2D1;
            using float4 = global::ComputeSharp.Float4;

            namespace MyNamespace;

            [D2DInputCount(0)]
            [D2DShaderProfile(D2D1ShaderProfile.PixelShader50)]
            [D2DRequiresDoublePrecisionSupport]
            [D2DGeneratedPixelShaderDescriptor]
            internal readonly partial struct MyShader : ID2D1PixelShader
            {
                private readonly float time;

                public float4 Execute()
                {
                    return (float)(time * 2.0f);
                }
            }
            """;

        VerifyGeneratedDiagnostics(source, "CMPSD2D0081");
    }

    /// <summary>
    /// Verifies the output of a source generator.
    /// </summary>
    /// <param name="source">The input source to process.</param>
    /// <param name="diagnosticsIds">The expected diagnostics ids to be generated.</param>
    /// <returns>The task for the operation.</returns>
    private static void VerifyGeneratedDiagnostics(string source, params string[] diagnosticsIds)
    {
        // Get all assembly references for the .NET TFM and ComputeSharp
        IEnumerable<MetadataReference> metadataReferences =
        [
            .. Net80.References.All,
            MetadataReference.CreateFromFile(typeof(Core::ComputeSharp.Hlsl).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(D2D1::ComputeSharp.D2D1.ID2D1PixelShader).Assembly.Location)
        ];

        // Parse the source text (C# 12)
        SyntaxTree sourceTree = CSharpSyntaxTree.ParseText(
            source,
            CSharpParseOptions.Default.WithLanguageVersion(LanguageVersion.CSharp12));

        // Create the original compilation
        CSharpCompilation compilation = CSharpCompilation.Create(
            "original",
            [sourceTree],
            metadataReferences,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary, allowUnsafe: true));

        // Create the generator driver with the D2D shader generator
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new D2DPixelShaderDescriptorGenerator()).WithUpdatedParseOptions((CSharpParseOptions)sourceTree.Options);

        // Run all source generators on the input source code
        _ = driver.RunGeneratorsAndUpdateCompilation(compilation, out Compilation outputCompilation, out ImmutableArray<Diagnostic> diagnostics);

        Dictionary<string, Diagnostic> diagnosticMap = diagnostics.DistinctBy(diagnostic => diagnostic.Id).ToDictionary(diagnostic => diagnostic.Id);

        // Check that the diagnostics match
        Assert.IsTrue(diagnosticMap.Keys.ToHashSet().SetEquals(diagnosticsIds), $"Diagnostics didn't match. {string.Join(", ", diagnosticMap.Values)}");

        // If the compilation was supposed to succeed, ensure that no further errors were generated
        if (diagnosticsIds.Length == 0)
        {
            // Compute diagnostics for the final compiled output (just include errors)
            List<Diagnostic> outputCompilationDiagnostics = outputCompilation.GetDiagnostics().Where(diagnostic => diagnostic.Severity == DiagnosticSeverity.Error).ToList();

            Assert.IsTrue(outputCompilationDiagnostics.Count == 0, $"resultingIds: {string.Join(", ", outputCompilationDiagnostics)}");
        }
    }
}