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
        string path = Path.GetDirectoryName(typeof(DynamicNativeLibrariesResolverTests).Assembly.Location)!;

        while (Path.GetFileName(path) is not "ComputeSharp")
        {
            path = Path.GetDirectoryName(path)!;
        }

        string corePackagingProjectPath = Path.Combine(path, "src", "ComputeSharp.Core", "ComputeSharp.Core.msbuildproj");
        string packagingProjectPath = Path.Combine(path, "src", "ComputeSharp.Package", "ComputeSharp.Package.msbuildproj");

        // Run dotnet pack and on the packaging projects, to ensure the local NuGet packages are available
        Process.Start("dotnet", $"pack {corePackagingProjectPath} -c Release").WaitForExit();
        Process.Start("dotnet", $"pack {packagingProjectPath} -c Release").WaitForExit();
    }
}
