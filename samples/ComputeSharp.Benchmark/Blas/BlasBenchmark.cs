using System;
using System.Buffers;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;

#pragma warning disable CA1063

namespace ComputeSharp.Benchmark.Blas;

/// <summary>
/// A <see langword="class"/> that benchmarks the APIs in the <see cref="BlasHelpers"/> class, on both CPU and GPU.
/// </summary>
[SimpleJob(RunStrategy.Monitoring)]
public class BlasBenchmark : IDisposable
{
    /// <summary>
    /// The number of samples.
    /// </summary>
    private const int C = 128;

    /// <summary>
    /// The nummber of rows in the <see cref="x"/> matrix.
    /// </summary>
    private const int N = 512;

    /// <summary>
    /// The number of columns in the <see cref="x"/> matrix (same as the number of rows in the <see cref="w"/> matrix)
    /// </summary>
    private const int M = 512;

    /// <summary>
    /// The number of columns in the <see cref="w"/> matrix.
    /// </summary>
    private const int P = 256;

    /// <summary>
    /// The input tensor.
    /// </summary>
    private float[]? x;

    /// <summary>
    /// The weights tensor.
    /// </summary>
    private float[]? w;

    /// <summary>
    /// The bias tensor.
    /// </summary>
    private float[]? b;

    /// <summary>
    /// The result tensor.
    /// </summary>
    private float[]? y;

    /// <summary>
    /// The input tensor (GPU).
    /// </summary>
    private ReadOnlyBuffer<float>? bufferX;

    /// <summary>
    /// The weights tensor (GPU).
    /// </summary>
    private ReadOnlyBuffer<float>? bufferW;

    /// <summary>
    /// The bias tensor (GPU).
    /// </summary>
    private ReadOnlyBuffer<float>? bufferB;

    /// <summary>
    /// The result tensor (GPU).
    /// </summary>
    private ReadWriteBuffer<float>? bufferY;

    /// <summary>
    /// A <see cref="Random"/> instance to initialize the tensors.
    /// </summary>
    private readonly Random random = new();

    /// <summary>
    /// Initial setup for a benchmarking session.
    /// </summary>
    [GlobalSetup]
    public void Setup()
    {
        // Creates a new random normalized float[] array of a given size
        float[] CreateRandomArray(int size)
        {
            float[] array = ArrayPool<float>.Shared.Rent(size);

            foreach (ref float x in array.AsSpan())
            {
                x = (float)this.random.NextDouble();
            }

            return array;
        }

        this.x = CreateRandomArray(C * N * M);
        this.w = CreateRandomArray(M * P);
        this.b = CreateRandomArray(P);
        this.y = CreateRandomArray(C * N * P);

        this.bufferX = GraphicsDevice.GetDefault().AllocateReadOnlyBuffer(this.x);
        this.bufferW = GraphicsDevice.GetDefault().AllocateReadOnlyBuffer(this.w);
        this.bufferB = GraphicsDevice.GetDefault().AllocateReadOnlyBuffer(this.b);
        this.bufferY = GraphicsDevice.GetDefault().AllocateReadWriteBuffer(this.y);

        Cpu();
        GpuWithNoTemporaryBuffers();
        GpuWithTemporaryBuffers();
    }

    /// <inheritdoc/>
    [GlobalCleanup]
    public void Dispose()
    {
        ArrayPool<float>.Shared.Return(this.x!);
        ArrayPool<float>.Shared.Return(this.w!);
        ArrayPool<float>.Shared.Return(this.b!);
        ArrayPool<float>.Shared.Return(this.y!);

        this.bufferX!.Dispose();
        this.bufferW!.Dispose();
        this.bufferB!.Dispose();
        this.bufferY!.Dispose();
    }

    /// <summary>
    /// Runs a fully connected forward operation on the CPU.
    /// </summary>
    [Benchmark(Baseline = true)]
    public void Cpu()
    {
        BlasHelpers.FullyConnectedForwardCpu(C, N, M, P, this.x!, this.w!, this.b!, this.y!);
    }

    /// <summary>
    /// Runs a fully connected forward operation on the GPU.
    /// </summary>
    [Benchmark]
    public void GpuWithNoTemporaryBuffers()
    {
        BlasHelpers.FullyConnectedForwardGpu(GraphicsDevice.GetDefault(), C, N, M, P, this.bufferX!, this.bufferW!, this.bufferB!, this.bufferY!);
    }

    /// <summary>
    /// Runs a fully connected forward operation on the GPU, creating temporary GPU buffers to perform the operations.
    /// </summary>
    [Benchmark]
    public void GpuWithTemporaryBuffers()
    {
        using ReadOnlyBuffer<float> x = GraphicsDevice.GetDefault().AllocateReadOnlyBuffer(this.x!);
        using ReadOnlyBuffer<float> w = GraphicsDevice.GetDefault().AllocateReadOnlyBuffer(this.w!);
        using ReadOnlyBuffer<float> b = GraphicsDevice.GetDefault().AllocateReadOnlyBuffer(this.b!);
        using ReadWriteBuffer<float> y = GraphicsDevice.GetDefault().AllocateReadWriteBuffer<float>(this.y!.Length);

        BlasHelpers.FullyConnectedForwardGpu(GraphicsDevice.GetDefault(), C, N, M, P, x, w, b, y);

        y.CopyTo(this.y);
    }
}