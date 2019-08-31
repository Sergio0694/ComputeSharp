using BenchmarkDotNet.Running;

namespace ComputeSharp.Benchmark
{
    public class Program
    {
        public static void Main()
        {
            /* =================
             * BenchmarkDotNet
             * ================
             * In order to run this benchmark, compile this project in Release mode,
             * then go to its bin\Release\netcoreapp3.0 folder, open a cmd prompt
             * and type "dotnet ComputeSharp.Benchmark.dll */
            BenchmarkRunner.Run<DnnBenchmark>();
        }
    }
}
