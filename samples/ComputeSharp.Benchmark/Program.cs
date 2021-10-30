using System.Reflection;
using BenchmarkDotNet.Running;

// =================
// BenchmarkDotNet
// ================
// In order to run this benchmark, switch to Release mode and either run select
// it as the startup project for the solution and run it without the debugger
// (CTRL + F5), or compile it and then go to its bin\Release\net6.0 folder,
// open a cmd prompt and run "dotnet ComputeSharp.Benchmark.dll".
BenchmarkSwitcher.FromAssembly(Assembly.GetExecutingAssembly()).Run(args);
