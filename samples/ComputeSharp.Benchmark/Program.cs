using BenchmarkDotNet.Running;

namespace ComputeSharp.Benchmark
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // =================
            // BenchmarkDotNet
            // ================
            // In order to run this benchmark, compile this project in Release mode,
            // then go to its bin\Release\netcoreapp3.0 folder, open a cmd prompt
            // and type "dotnet ComputeSharp.Benchmark.dll" to execute it.
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
        }
    }
}
