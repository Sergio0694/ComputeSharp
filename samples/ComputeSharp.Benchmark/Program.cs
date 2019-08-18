using System;
using System.Diagnostics;

namespace ComputeSharp.Benchmark
{
    class Program
    {
        static void Main()
        {
            using var benchmark = new FullyConnectedLayerBenchmark();

            Console.WriteLine("======== WARMUP ========");
            if (benchmark.EnsureImplementationsMatch()) throw new InvalidOperationException("CPU and GPU code don't match");

            Console.WriteLine();
            Console.WriteLine("======== GPU with temporary buffer ========");
            Stopwatch timer = Stopwatch.StartNew();
            TimeSpan cpuTime = TimeSpan.Zero, gpuTime = TimeSpan.Zero;

            // GPU with temporary buffers
            for (int i = 0; i < 10; i++)
            {
                benchmark.Cpu();
                cpuTime += timer.Elapsed;
                Console.WriteLine($"CPU: {timer.Elapsed:g}");
                timer.Restart();

                benchmark.GpuWithTemporaryBuffers();
                gpuTime += timer.Elapsed;
                Console.WriteLine($"GPU: {timer.Elapsed:g}");
                timer.Restart();
            }

            Console.WriteLine();
            Console.WriteLine("======== AVERAGE ========");
            Console.WriteLine($"CPU: {cpuTime / 10:g}");
            Console.WriteLine($"GPU: {gpuTime / 10:g}");
            Console.WriteLine("=========================");
            Console.WriteLine();

            Console.WriteLine("======== GPU with no temporary buffer ========");
            timer = Stopwatch.StartNew();
            cpuTime = gpuTime = TimeSpan.Zero;

            // GPU with no temporary buffers
            for (int i = 0; i < 10; i++)
            {
                benchmark.Cpu();
                cpuTime += timer.Elapsed;
                Console.WriteLine($"CPU: {timer.Elapsed:g}");
                timer.Restart();

                benchmark.GpuWithNoTemporaryBuffers();
                gpuTime += timer.Elapsed;
                Console.WriteLine($"GPU: {timer.Elapsed:g}");
                timer.Restart();
            }

            Console.WriteLine();
            Console.WriteLine("======== AVERAGE ========");
            Console.WriteLine($"CPU: {cpuTime / 10:g}");
            Console.WriteLine($"GPU: {gpuTime / 10:g}");
        }
    }
}
