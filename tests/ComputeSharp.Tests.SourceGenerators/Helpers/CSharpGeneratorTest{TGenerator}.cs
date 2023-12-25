extern alias Core;

#if D2D1_TESTS
extern alias D2D1;
#else
extern alias D3D12;
#endif

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
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
        RunGenerator(source, out Compilation compilation, out ImmutableArray<Diagnostic> diagnostics);

        Dictionary<string, Diagnostic> diagnosticMap = diagnostics.DistinctBy(diagnostic => diagnostic.Id).ToDictionary(diagnostic => diagnostic.Id);

        // Check that the diagnostics match
        Assert.IsTrue(diagnosticMap.Keys.ToHashSet().SetEquals(diagnosticsIds), $"Diagnostics didn't match. {string.Join(", ", diagnosticMap.Values)}");

        // If the compilation was supposed to succeed, ensure that no further errors were generated
        if (diagnosticsIds.Length == 0)
        {
            // Compute diagnostics for the final compiled output (just include errors)
            List<Diagnostic> outputCompilationDiagnostics = compilation.GetDiagnostics().Where(diagnostic => diagnostic.Severity == DiagnosticSeverity.Error).ToList();

            Assert.IsTrue(outputCompilationDiagnostics.Count == 0, $"resultingIds: {string.Join(", ", outputCompilationDiagnostics)}");
        }
    }

    /// <summary>
    /// Verifies the resulting sources produced by a source generator.
    /// </summary>
    /// <param name="source">The input source to process.</param>
    /// <param name="result">The expected source to be generated.</param>
    public static void VerifySources(string source, (string Filename, string Source) result)
    {
        RunGenerator(source, out Compilation compilation, out ImmutableArray<Diagnostic> diagnostics);

        // Ensure that no diagnostics were generated
        CollectionAssert.AreEquivalent(Array.Empty<Diagnostic>(), diagnostics);

        // Update the assembly version using the version from the assembly of the input generators.
        // This allows the tests to not need updates whenever the version of the MVVM Toolkit changes.
        string expectedText = result.Source.Replace("<ASSEMBLY_VERSION>", $"\"{typeof(TGenerator).Assembly.GetName().Version}\"");
        string actualText = compilation.SyntaxTrees.Single(tree => Path.GetFileName(tree.FilePath) == result.Filename).ToString();

        Assert.AreEqual(expectedText, actualText);
    }

    /// <summary>
    /// Runs a generator and gathers the output results.
    /// </summary>
    /// <param name="source">The input source to process.</param>
    /// <param name="compilation"><inheritdoc cref="GeneratorDriver.RunGeneratorsAndUpdateCompilation" path="/param[@name='outputCompilation']/node()"/></param>
    /// <param name="diagnostics"><inheritdoc cref="GeneratorDriver.RunGeneratorsAndUpdateCompilation" path="/param[@name='diagnostics']/node()"/></param>
    private static void RunGenerator(
        string source,
        out Compilation compilation,
        out ImmutableArray<Diagnostic> diagnostics)
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
        CSharpCompilation originalCompilation = CSharpCompilation.Create(
            "original",
            [sourceTree],
            metadataReferences,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary, allowUnsafe: true));

        // Create the generator driver with the D2D shader generator
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new TGenerator()).WithUpdatedParseOptions((CSharpParseOptions)sourceTree.Options);

        // Run all source generators on the input source code
        _ = driver.RunGeneratorsAndUpdateCompilation(originalCompilation, out compilation, out diagnostics);
    }
}