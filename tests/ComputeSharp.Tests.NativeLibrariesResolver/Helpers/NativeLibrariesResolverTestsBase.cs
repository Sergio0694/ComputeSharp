using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests.NativeLibrariesResolver.Helpers;

/// <summary>
/// A base class to implement native libraries tests.
/// </summary>
public abstract class NativeLibrariesResolverTestsBase
{
    /// <summary>
    /// Gets the name of the test project.
    /// </summary>
    protected abstract string SampleProjectName { get; }

    /// <summary>
    /// Gets the directory containing the current test project.
    /// </summary>
    private string SampleProjectDirectory
    {
        get
        {
            string path = Path.GetDirectoryName(typeof(NativeLibrariesResolverTestsBase).Assembly.Location)!;

            while (Path.GetFileName(path) is not "tests")
            {
                path = Path.GetDirectoryName(path)!;
            }

            return Path.Combine(path, SampleProjectName);
        }
    }

    [TestMethod]
    [DataRow(Configuration.Debug, RID.None)]
    [DataRow(Configuration.Debug, RID.Win_x64)]
    [DataRow(Configuration.Release, RID.None)]
    [DataRow(Configuration.Release, RID.Win_x64)]
    public void DotnetRunWorks(Configuration configuration, RID rid)
    {
        CleanSampleProject(configuration, rid);

        Assert.AreEqual(0, Exec(SampleProjectDirectory, "dotnet", $"run -c {configuration} -f net6.0 {ToOption(rid)}"));
    }

    [TestMethod]
    [DataRow(Configuration.Debug, RID.None)]
    [DataRow(Configuration.Debug, RID.Win_x64)]
    [DataRow(Configuration.Release, RID.None)]
    [DataRow(Configuration.Release, RID.Win_x64)]
    public void DotnetBuildWithRunningDotnetHostFromProjectDirectoryWorks(Configuration configuration, RID rid)
    {
        CleanSampleProject(configuration, rid);
        BuildSampleProject(configuration, rid);

        string realtivePathToDll = Path.Combine("bin", $"{configuration}", "net6.0", $"{ToDirectory(rid)}", $"{SampleProjectName}.dll");

        Assert.AreEqual(0, Exec(SampleProjectDirectory, "dotnet", realtivePathToDll));
    }

    [TestMethod]
    [DataRow(Configuration.Debug, RID.None)]
    [DataRow(Configuration.Debug, RID.Win_x64)]
    [DataRow(Configuration.Release, RID.None)]
    [DataRow(Configuration.Release, RID.Win_x64)]
    public void DotnetBuildWithRunningDotnetHostDirectlyWorks(Configuration configuration, RID rid)
    {
        CleanSampleProject(configuration, rid);
        BuildSampleProject(configuration, rid);

        string pathToDllDirectory = Path.Combine(SampleProjectDirectory, "bin", $"{configuration}", "net6.0", $"{ToDirectory(rid)}");

        Assert.AreEqual(0, Exec(pathToDllDirectory, "dotnet", $"{SampleProjectName}.dll"));
    }

    [TestMethod]
    [DataRow(Configuration.Debug, RID.None)]
    [DataRow(Configuration.Debug, RID.Win_x64)]
    [DataRow(Configuration.Release, RID.None)]
    [DataRow(Configuration.Release, RID.Win_x64)]
    public void DotnetBuildWithRunningAppHostFromProjectDirectoryWorks(Configuration configuration, RID rid)
    {
        CleanSampleProject(configuration, rid);
        BuildSampleProject(configuration, rid);

        string relativePathToAppHost = Path.Combine("bin", $"{configuration}", "net6.0", $"{ToDirectory(rid)}", $"{SampleProjectName}.exe");

        Assert.AreEqual(0, Exec(SampleProjectDirectory, relativePathToAppHost, ""));
    }

    [TestMethod]
    [DataRow(Configuration.Debug, RID.None)]
    [DataRow(Configuration.Debug, RID.Win_x64)]
    [DataRow(Configuration.Release, RID.None)]
    [DataRow(Configuration.Release, RID.Win_x64)]
    public void DotnetBuildWithRunningAppHostDirectlyWorks(Configuration configuration, RID rid)
    {
        CleanSampleProject(configuration, rid);
        BuildSampleProject(configuration, rid);

        string pathToAppHostDirectory = Path.Combine(SampleProjectDirectory, "bin", $"{configuration}", "net6.0", $"{ToDirectory(rid)}");

        Assert.AreEqual(0, Exec(pathToAppHostDirectory, $"{SampleProjectName}.exe", ""));
    }


    [TestMethod]
    [DataRow(PublishMode.SelfContained, DeploymentMode.Multiassembly, NativeLibrariesDeploymentMode.NotApplicable)]
    [DataRow(PublishMode.FrameworkDependent, DeploymentMode.Multiassembly, NativeLibrariesDeploymentMode.NotApplicable)]
    [DataRow(PublishMode.SelfContained, DeploymentMode.SingleFile, NativeLibrariesDeploymentMode.CopyToApplicationDirectory)]
    [DataRow(PublishMode.SelfContained, DeploymentMode.SingleFile, NativeLibrariesDeploymentMode.ExtractToTemporaryDirectory)]
    [DataRow(PublishMode.FrameworkDependent, DeploymentMode.SingleFile, NativeLibrariesDeploymentMode.CopyToApplicationDirectory)]
    [DataRow(PublishMode.FrameworkDependent, DeploymentMode.SingleFile, NativeLibrariesDeploymentMode.ExtractToTemporaryDirectory)]
    public void DotnetPublishWorks(PublishMode publishMode, DeploymentMode deploymentMode, NativeLibrariesDeploymentMode nativeLibsDeploymentMode)
    {
        // Publishing without specifying a RID is not supported.
        // Furthermore, only publishing in Release mode is tested.
        CleanSampleProject(Configuration.Release, RID.Win_x64);

        _ = Exec(SampleProjectDirectory, "dotnet", $"publish -c Release -f net6.0 -r win-x64 {ToOption(publishMode)} {ToOption(deploymentMode)} {ToOption(nativeLibsDeploymentMode)} /bl");

        string pathToAppHost = Path.Combine("bin", $"Release", "net6.0", "win-x64", "publish", $"{SampleProjectName}.exe");

        Assert.AreEqual(0, Exec(SampleProjectDirectory, pathToAppHost, ""));
    }

    /// <summary>
    /// Cleans the sample project's artifacts for a specific configuration and target runtime.
    /// This method is called at the start of each test method to work around bugs around some up-to-date checks
    /// Causing certain targets to not run and effectively corrupting the build output.
    /// </summary>
    /// <param name="configuration">The configuration for which the output is cleaned.</param>
    /// <param name="rid">The RID for which the output is cleaned.</param>
    private void CleanSampleProject(Configuration configuration, RID rid)
    {
        _ = Exec(SampleProjectDirectory, "dotnet", $"clean -c {configuration} {ToOption(rid)}");
    }

    /// <summary>
    /// Builds the sample project with a specific configuration and target runtime.
    /// </summary>
    /// <param name="configuration">The configuration to use to build the project.</param>
    /// <param name="rid">The RID to use to build the project.</param>
    private void BuildSampleProject(Configuration configuration, RID rid)
    {
        _ = Exec(SampleProjectDirectory, "dotnet", $"build -c {configuration} {ToOption(rid)}");
    }

    /// <summary>
    /// Executes a specified process from the command line and returns the exit code.
    /// </summary>
    /// <param name="workingDirectory">The working directory to execute the process.</param>
    /// <param name="filePath">The target file path for the process to execute.</param>
    /// <param name="arguments">The arguments to invoke the process.</param>
    /// <returns>The exit code for the executed process.</returns>
    private static int Exec(string workingDirectory, string filePath, string arguments)
    {
        ProcessStartInfo startInfo = new()
        {
            UseShellExecute = true,
            CreateNoWindow = true,
            WindowStyle = ProcessWindowStyle.Hidden,
            FileName = filePath,
            Arguments = arguments,
            WorkingDirectory = workingDirectory
        };

        using Process process = Process.Start(startInfo)!;

        process.WaitForExit();

        return process.ExitCode;
    }

    /// <summary>
    /// Gets a text representation of the command line argument for the given <see cref="RID"/>.
    /// </summary>
    /// <param name="rid">The input <see cref="RID"/> value.</param>
    /// <returns>A text representation for <paramref name="rid"/>.</returns>
    private static string ToOption(RID rid)
    {
        return rid switch
        {
            RID.None => "",
            RID.Win_x64 => "-r win-x64",
            _ => throw new InvalidEnumArgumentException(nameof(rid), (int)rid, typeof(RID))
        };
    }

    /// <summary>
    /// Gets a text representation of the command line argument for the given <see cref="PublishMode"/>.
    /// </summary>
    /// <param name="publishMode">The input <see cref="PublishMode"/> value.</param>
    /// <returns>A text representation for <paramref name="publishMode"/>.</returns>
    private static string ToOption(PublishMode publishMode)
    {
        return publishMode switch
        {
            PublishMode.FrameworkDependent => "--self-contained false",
            PublishMode.SelfContained => "--self-contained true",
            _ => throw new InvalidEnumArgumentException(nameof(publishMode), (int)publishMode, typeof(PublishMode))
        };
    }

    /// <summary>
    /// Gets a text representation of the command line argument for the given <see cref="DeploymentMode"/>.
    /// </summary>
    /// <param name="deploymentMode">The input <see cref="DeploymentMode"/> value.</param>
    /// <returns>A text representation for <paramref name="deploymentMode"/>.</returns>
    private static string ToOption(DeploymentMode deploymentMode)
    {
        return deploymentMode switch
        {
            DeploymentMode.Multiassembly => "/p:PublishSingleFile=false",
            DeploymentMode.SingleFile => "/p:PublishSingleFile=true",
            _ => throw new InvalidEnumArgumentException(nameof(deploymentMode), (int)deploymentMode, typeof(DeploymentMode))
        };
    }

    /// <summary>
    /// Gets a text representation of the command line argument for the given <see cref="NativeLibrariesDeploymentMode"/>.
    /// </summary>
    /// <param name="deploymentMode">The input <see cref="NativeLibrariesDeploymentMode"/> value.</param>
    /// <returns>A text representation for <paramref name="deploymentMode"/>.</returns>
    private static string ToOption(NativeLibrariesDeploymentMode deploymentMode)
    {
        return deploymentMode switch
        {
            NativeLibrariesDeploymentMode.NotApplicable => "",
            NativeLibrariesDeploymentMode.CopyToApplicationDirectory => "/p:IncludeNativeLibrariesForSelfExtract=false",
            NativeLibrariesDeploymentMode.ExtractToTemporaryDirectory => "/p:IncludeNativeLibrariesForSelfExtract=true",
            _ => throw new InvalidEnumArgumentException(nameof(deploymentMode), (int)deploymentMode, typeof(NativeLibrariesDeploymentMode))
        };
    }

    /// <summary>
    /// Gets a text representation of the build folder for the given <see cref="RID"/>.
    /// </summary>
    /// <param name="rid">The input <see cref="RID"/> value.</param>
    /// <returns>A text representation for <paramref name="rid"/>.</returns>
    private static string ToDirectory(RID rid)
    {
        return rid switch
        {
            RID.None => "",
            RID.Win_x64 => "win-x64",
            _ => throw new InvalidEnumArgumentException(nameof(rid), (int)rid, typeof(RID))
        };
    }
}

/// <summary>
/// A build configuration.
/// </summary>
public enum Configuration
{
    Debug,
    Release
}

/// <summary>
/// A runtime identifier.
/// </summary>
public enum RID
{
    None,
    Win_x64
}

/// <summary>
/// Indicates how should the application carry the framework with itself.
/// </summary>
public enum PublishMode
{
    FrameworkDependent,
    SelfContained
}

/// <summary>
/// Indicates how should the application be packaged. Notably, these tests employ .NET 6 style SingleFile,
/// aka SuperHost. It does not affect native libraries packaging in .NET 6, but may in the future.
/// </summary>
public enum DeploymentMode
{
    Multiassembly,
    SingleFile
}

/// <summary>
/// Indicates the deployment mode for application's native dependencies.
/// Not applicable to multiassembly deployment mode.
/// </summary>
public enum NativeLibrariesDeploymentMode
{
    NotApplicable,
    ExtractToTemporaryDirectory,
    CopyToApplicationDirectory
}