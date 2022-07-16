using System;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace ComputeSharp.Benchmark.Blas;

/// <summary>
/// A <see langword="class"/> that benchmarks the shader dispatch times.
/// </summary>
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[MemoryDiagnoser]
public partial class DispatchingBenchmark : IDisposable
{
    /// <summary>
    /// The test buffer.
    /// </summary>
    private ReadWriteBuffer<float>? Buffer;

    /// <summary>
    /// Initial setup for a benchmarking session.
    /// </summary>
    [GlobalSetup]
    public void Setup()
    {
        Buffer = GraphicsDevice.GetDefault().AllocateReadWriteBuffer<float>(128);
    }

    /// <inheritdoc/>
    [GlobalCleanup]
    public void Dispose()
    {
        Buffer!.Dispose();
    }

    /// <summary>
    /// Invokes a single shader, manually.
    /// </summary>
    [Benchmark(Baseline = true)]
    [BenchmarkCategory("SINGLE")]
    public void Compute_Single()
    {
        GraphicsDevice.GetDefault().For(64, new TestShader(Buffer!));
    }

    /// <summary>
    /// Invokes a single shader through a compute context.
    /// </summary>
    [Benchmark]
    [BenchmarkCategory("SINGLE")]
    public void Compute_Single_WithContext()
    {
        using var context = GraphicsDevice.GetDefault().CreateComputeContext();

        context.For(64, new TestShader(Buffer!));
    }

    /// <summary>
    /// Invokes a single shader through a compute context, asynchronously.
    /// </summary>
    [Benchmark]
    [BenchmarkCategory("SINGLE")]
    public async Task Compute_Single_WithContext_Async()
    {
        await using var context = GraphicsDevice.GetDefault().CreateComputeContext();

        context.For(64, new TestShader(Buffer!));
    }

    /// <summary>
    /// Invokes two empty compute shaders one by one.
    /// </summary>
    [Benchmark(Baseline = true)]
    [BenchmarkCategory("MULTIPLE")]
    public void Compute_Multiple()
    {
        GraphicsDevice.GetDefault().For(64, new TestShader(Buffer!));
        GraphicsDevice.GetDefault().For(64, new TestShader(Buffer!));
    }

    /// <summary>
    /// Invokes two empty compute shaders in a batch.
    /// </summary>
    [Benchmark]
    [BenchmarkCategory("MULTIPLE")]
    public void Compute_Multiple_WithContext()
    {
        using var context = GraphicsDevice.GetDefault().CreateComputeContext();

        context.For(64, new TestShader(Buffer!));
        context.Barrier(Buffer!);
        context.For(64, new TestShader(Buffer!));
    }

    /// <summary>
    /// Invokes two empty compute shaders in a batch, asynchronously.
    /// </summary>
    [Benchmark]
    [BenchmarkCategory("MULTIPLE")]
    public async Task Compute_Multiple_WithContext_Async()
    {
        await using var context = GraphicsDevice.GetDefault().CreateComputeContext();

        context.For(64, new TestShader(Buffer!));
        context.Barrier(Buffer!);
        context.For(64, new TestShader(Buffer!));
    }

    [AutoConstructor]
    internal readonly partial struct TestShader : IComputeShader
    {
        public readonly ReadWriteBuffer<float> buffer;

        /// <inheritdoc/>
        public void Execute()
        {
        }
    }
}
