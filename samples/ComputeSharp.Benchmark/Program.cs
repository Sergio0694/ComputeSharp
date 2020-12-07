using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.CsProj;
using BenchmarkDotNet.Toolchains.DotNetCli;

namespace ComputeSharp.Benchmark
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ManualConfig configuration =
                DefaultConfig.Instance.AddJob(
                    Job.Default.WithToolchain(
                        CsProjCoreToolchain.From(
                            new NetCoreAppSettings(
                                targetFrameworkMoniker: "net5.0-windows10.0.19041.0",
                                runtimeFrameworkVersion: null,
                                name: "5.0")))
                    .AsDefault());

            // =================
            // BenchmarkDotNet
            // ================
            // In order to run this benchmark, compile this project in Release mode,
            // then go to its bin\Release\netcoreapp3.0 folder, open a cmd prompt
            // and type "dotnet ComputeSharp.Benchmark.dll" to execute it.
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, configuration);
        }
    }
}
