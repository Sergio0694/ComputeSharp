using System;
using System.Diagnostics;

namespace ComputeSharp.Benchmark
{
    class Program
    {
        static void Main()
        {
            var benchmark = new FullyConnectedLayerBenchmark();
            benchmark.Gpu(); // Warmup

            Stopwatch timer = Stopwatch.StartNew();
            TimeSpan cpuTime = TimeSpan.Zero, gpuTime = TimeSpan.Zero;

            for (int i = 0; i < 10; i++)
            {
                benchmark.Cpu();
                cpuTime += timer.Elapsed;
                Console.WriteLine($"CPU: {timer.Elapsed:g}");
                timer.Restart();
                benchmark.Gpu();
                gpuTime += timer.Elapsed;
                Console.WriteLine($"GPU: {timer.Elapsed:g}");
                timer.Restart();
            }

            Console.WriteLine("======== AVERAGE ========");
            Console.WriteLine($"CPU: {cpuTime / 10:g}");
            Console.WriteLine($"GPU: {gpuTime / 10:g}");
        }
    }
}
