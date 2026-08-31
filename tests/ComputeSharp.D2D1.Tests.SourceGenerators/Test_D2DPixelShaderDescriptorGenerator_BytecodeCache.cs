using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using ComputeSharp.D2D1.SourceGenerators;
using ComputeSharp.Tests.SourceGenerators.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.D2D1.Tests.SourceGenerators;

[TestClass]
public class Test_D2DPixelShaderDescriptorGenerator_BytecodeCache
{
    [TestMethod]
    public void PersistentCache_WritesEntry_AndLoadsItOnCacheMiss()
    {
        const string source = """
            using ComputeSharp;
            using ComputeSharp.D2D1;
            using float4 = global::ComputeSharp.Float4;

            [D2DInputCount(0)]
            [D2DShaderProfile(D2D1ShaderProfile.PixelShader50)]
            [D2DGeneratedPixelShaderDescriptor]
            internal readonly partial struct MyCachedShader : ID2D1PixelShader
            {
                public float4 Execute()
                {
                    return new float4(0.1f, 0.2f, 0.3f, 1.0f);
                }
            }
            """;

        string cacheDirectory = CreateTemporaryCacheDirectory();

        try
        {
            // Run the generator with the persistent cache enabled, and verify an entry was written
            string firstRunSource = RunGeneratorWithBytecodeCache(source, cacheDirectory);

            string entryPath = Directory.GetFiles(cacheDirectory, "*.bin").Single();

            // Drop the in-memory cache entries, then run again: the bytecode must now come from disk,
            // and the generated source must be identical to the one produced by the fresh compilation
            CollectInMemoryCacheEntries();

            string secondRunSource = RunGeneratorWithBytecodeCache(source, cacheDirectory);

            Assert.AreEqual(firstRunSource, secondRunSource);

            // Tamper with the bytecode payload in the cache entry (leaving the header valid). Running
            // again must now produce different generated source, embedding the tampered bytecode: this
            // proves the bytecode was actually loaded from the persistent cache, not recompiled.
            byte[] entryData = File.ReadAllBytes(entryPath);

            for (int i = 32; i < entryData.Length; i++)
            {
                entryData[i] ^= 0x5A;
            }

            File.WriteAllBytes(entryPath, entryData);

            CollectInMemoryCacheEntries();

            string tamperedRunSource = RunGeneratorWithBytecodeCache(source, cacheDirectory);

            Assert.AreNotEqual(firstRunSource, tamperedRunSource);
        }
        finally
        {
            Directory.Delete(cacheDirectory, recursive: true);
        }
    }

    [TestMethod]
    public void PersistentCache_InvalidEntry_IsIgnoredAndRewritten()
    {
        const string source = """
            using ComputeSharp;
            using ComputeSharp.D2D1;
            using float4 = global::ComputeSharp.Float4;

            [D2DInputCount(0)]
            [D2DShaderProfile(D2D1ShaderProfile.PixelShader50)]
            [D2DGeneratedPixelShaderDescriptor]
            internal readonly partial struct MyCorruptedCacheShader : ID2D1PixelShader
            {
                public float4 Execute()
                {
                    return new float4(0.4f, 0.5f, 0.6f, 1.0f);
                }
            }
            """;

        string cacheDirectory = CreateTemporaryCacheDirectory();

        try
        {
            string firstRunSource = RunGeneratorWithBytecodeCache(source, cacheDirectory);

            string entryPath = Directory.GetFiles(cacheDirectory, "*.bin").Single();
            long validEntryLength = new FileInfo(entryPath).Length;

            // Corrupt the cache entry (invalid header and length)
            File.WriteAllBytes(entryPath, [1, 2, 3]);

            // Run again with no in-memory cache: the corrupt entry must be ignored (falling back to a
            // fresh compilation, producing identical source) and replaced with a new valid entry
            CollectInMemoryCacheEntries();

            string secondRunSource = RunGeneratorWithBytecodeCache(source, cacheDirectory);

            Assert.AreEqual(firstRunSource, secondRunSource);

            string rewrittenEntryPath = Directory.GetFiles(cacheDirectory, "*.bin").Single();

            Assert.AreEqual(entryPath, rewrittenEntryPath);
            Assert.AreEqual(validEntryLength, new FileInfo(rewrittenEntryPath).Length);
        }
        finally
        {
            Directory.Delete(cacheDirectory, recursive: true);
        }
    }

    [TestMethod]
    public void PersistentCache_Disabled_WritesNoEntries()
    {
        const string source = """
            using ComputeSharp;
            using ComputeSharp.D2D1;
            using float4 = global::ComputeSharp.Float4;

            [D2DInputCount(0)]
            [D2DShaderProfile(D2D1ShaderProfile.PixelShader50)]
            [D2DGeneratedPixelShaderDescriptor]
            internal readonly partial struct MyUncachedShader : ID2D1PixelShader
            {
                public float4 Execute()
                {
                    return new float4(0.7f, 0.8f, 0.9f, 1.0f);
                }
            }
            """;

        string cacheDirectory = CreateTemporaryCacheDirectory();

        try
        {
            // Run with no cache directory configured (ie. the default), and verify nothing was written
            _ = RunGeneratorWithBytecodeCache(source, cacheDirectory: null);

            Assert.AreEqual(0, Directory.GetFiles(cacheDirectory).Length);
        }
        finally
        {
            Directory.Delete(cacheDirectory, recursive: true);
        }
    }

    /// <summary>
    /// Creates a new (empty) temporary directory to use as a persistent cache.
    /// </summary>
    /// <returns>The path of the new directory.</returns>
    private static string CreateTemporaryCacheDirectory()
    {
        string path = Path.Combine(Path.GetTempPath(), "ComputeSharp.Tests.BytecodeCache", Guid.NewGuid().ToString("N"));

        _ = Directory.CreateDirectory(path);

        return path;
    }

    /// <summary>
    /// Runs the <see cref="D2DPixelShaderDescriptorGenerator"/> on a given source, with the persistent
    /// bytecode cache pointing at a given directory (or disabled, if the directory is <see langword="null"/>).
    /// </summary>
    /// <param name="source">The input source to process.</param>
    /// <param name="cacheDirectory">The directory for the persistent bytecode cache, if enabled.</param>
    /// <returns>The text of the single generated source file.</returns>
    /// <remarks>
    /// This method is deliberately not inlined and returns only the generated text, so that after it
    /// returns (and a GC is performed), no references to the run remain that could keep the shared
    /// in-memory cache entries alive (see <see cref="CollectInMemoryCacheEntries"/>).
    /// </remarks>
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static string RunGeneratorWithBytecodeCache(string source, string? cacheDirectory)
    {
        CSharpCompilation compilation = CSharpGeneratorTest<D2DPixelShaderDescriptorGenerator>.CreateCompilation(source);

        GeneratorDriver driver = CSharpGeneratorDriver.Create(
            generators: [new D2DPixelShaderDescriptorGenerator().AsSourceGenerator()],
            parseOptions: (CSharpParseOptions)compilation.SyntaxTrees.First().Options,
            optionsProvider: new BytecodeCacheConfigOptionsProvider(cacheDirectory));

        driver = driver.RunGeneratorsAndUpdateCompilation(compilation, out _, out ImmutableArray<Diagnostic> diagnostics);

        CollectionAssert.AreEquivalent(Array.Empty<Diagnostic>(), diagnostics);

        return driver.GetRunResult().Results.Single().GeneratedSources.Single().SourceText.ToString();
    }

    /// <summary>
    /// Drops all entries in the shared in-memory bytecode cache used by the generator. The cache holds its
    /// keys weakly (entries are kept alive by the incremental state tables referencing the keys), so once
    /// no references to previous generator runs exist, collecting is enough to make all lookups miss. This
    /// is needed so that following generator runs are forced to go through the persistent cache on disk.
    /// </summary>
    private static void CollectInMemoryCacheEntries()
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
    }

    /// <summary>
    /// An <see cref="AnalyzerConfigOptionsProvider"/> implementation exposing the persistent bytecode cache property.
    /// </summary>
    /// <param name="cacheDirectory">The directory for the persistent bytecode cache, if enabled.</param>
    private sealed class BytecodeCacheConfigOptionsProvider(string? cacheDirectory) : AnalyzerConfigOptionsProvider
    {
        /// <inheritdoc/>
        public override AnalyzerConfigOptions GlobalOptions { get; } = new BytecodeCacheConfigOptions(cacheDirectory);

        /// <inheritdoc/>
        public override AnalyzerConfigOptions GetOptions(SyntaxTree tree)
        {
            return GlobalOptions;
        }

        /// <inheritdoc/>
        public override AnalyzerConfigOptions GetOptions(AdditionalText textFile)
        {
            return GlobalOptions;
        }

        /// <summary>
        /// An <see cref="AnalyzerConfigOptions"/> implementation exposing the persistent bytecode cache property.
        /// </summary>
        /// <param name="cacheDirectory">The directory for the persistent bytecode cache, if enabled.</param>
        private sealed class BytecodeCacheConfigOptions(string? cacheDirectory) : AnalyzerConfigOptions
        {
            /// <inheritdoc/>
            public override bool TryGetValue(string key, out string value)
            {
                if (key == "build_property.ComputeSharpShaderBytecodeCachePath" && cacheDirectory is not null)
                {
                    value = cacheDirectory;

                    return true;
                }

                value = null!;

                return false;
            }
        }
    }
}
