using System.Diagnostics;
using System.IO;
using ComputeSharp.Tests.NativeLibrariesResolver.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests.NativeLibrariesResolver;

[TestClass]
public class NativeLibrariesResolverTests : NativeLibrariesResolverTestsBase
{
    /// <inheritdoc/>
    protected override string SampleProjectName => "ComputeSharp.NuGet";

    /// <summary>
    /// Performs static initialization for the assembly before any unit tests are run.
    /// </summary>
    /// <param name="_">The <see cref="TestContext"/> for the test runner instance in use.</param>
    [ClassInitialize]
    public static void InitializeDynamicDependencies(TestContext _)
    {
        string path = Path.GetDirectoryName(typeof(DxcNativeLibrariesResolverTests).Assembly.Location)!;

        while (Path.GetFileName(path) is not "ComputeSharp")
        {
            path = Path.GetDirectoryName(path)!;
        }

        string coreProjectPath = Path.Combine(path, "src", "ComputeSharp.Core", "ComputeSharp.Core.csproj");
        string projectPath = Path.Combine(path, "src", "ComputeSharp", "ComputeSharp.csproj");

        Process.Start("dotnet", $"pack {coreProjectPath} -c Release").WaitForExit();
        Process.Start("dotnet", $"pack {projectPath} -c Release").WaitForExit();
    }
}