using System;
using BenchmarkDotNet.Attributes;

namespace ComputeSharp.Benchmark.Blas;

/// <summary>
/// A <see langword="class"/> that benchmarks the shader dispatch times.
/// </summary>
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
        Buffer = GraphicsDevice.Default.AllocateReadWriteBuffer<float>(128);
    }

    /// <inheritdoc/>
    [GlobalCleanup]
    public void Dispose()
    {
        Buffer!.Dispose();
    }

    /// <summary>
    /// Invokes two empty compute shaders one by one.
    /// </summary>
    [Benchmark(Baseline = true)]
    public void Compute_Single()
    {
        GraphicsDevice.Default.For(64, new TestShader(Buffer!));
        GraphicsDevice.Default.For(64, new TestShader(Buffer!));
    }

    /// <summary>
    /// Invokes two empty compute shaders in a batch.
    /// </summary>
    [Benchmark]
    public void Compute_Batched()
    {
        using var context = GraphicsDevice.Default.CreateComputeContext();

        context.For(64, new TestShader(Buffer!));
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
