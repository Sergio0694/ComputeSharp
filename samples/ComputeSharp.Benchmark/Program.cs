using System;
using System.Diagnostics;

namespace ComputeSharp.Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            var benchmark = new FullyConnectedLayerBenchmark();
            benchmark.Gpu(); // Warmup
            return;

            Stopwatch timer = new Stopwatch();
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
            Console.WriteLine($"CPU: {cpuTime:g}");
            Console.WriteLine($"GPU: {gpuTime:g}");

            Console.ReadKey();
        }
    }
}
