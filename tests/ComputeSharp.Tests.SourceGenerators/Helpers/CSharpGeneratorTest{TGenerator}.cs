extern alias Core;

#if D2D1_TESTS
extern alias D2D1;
#else
extern alias D3D12;
#endif

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Basic.Reference.Assemblies;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests.SourceGenerators.Helpers;

/// <summary>
/// A helper type to run source generator tests.
/// </summary>
/// <typeparam name="TGenerator">The type of generator to test.</typeparam>
internal static class CSharpGeneratorTest<TGenerator>
    where TGenerator : IIncrementalGenerator, new()
{
    /// <summary>
    /// Verifies the resulting diagnostics from a source generator.
    /// </summary>
    /// <param name="source">The input source to process.</param>
    /// <param name="diagnosticsIds">The expected diagnostics ids to be generated.</param>
    public static void VerifyDiagnostics(string source, params string[] diagnosticsIds)
    {
        // Get all assembly references for the .NET TFM and ComputeSharp
        IEnumerable<MetadataReference> metadataReferences =
        [
            .. Net80.References.All,
            MetadataReference.CreateFromFile(typeof(Core::ComputeSharp.Hlsl).Assembly.Location),
#if D2D1_TESTS
            MetadataReference.CreateFromFile(typeof(D2D1::ComputeSharp.D2D1.ID2D1PixelShader).Assembly.Location)
#else
            MetadataReference.CreateFromFile(typeof(D3D12::ComputeSharp.IComputeShader).Assembly.Location)
#endif
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
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new TGenerator()).WithUpdatedParseOptions((CSharpParseOptions)sourceTree.Options);

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
