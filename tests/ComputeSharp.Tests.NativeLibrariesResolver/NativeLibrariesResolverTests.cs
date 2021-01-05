using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;

[assembly: Parallelize(Workers = 0, Scope = ExecutionScope.ClassLevel)]
namespace ComputeSharp.Tests.NativeLibrariesResolver
{
    [TestClass]
    public class NativeLibrariesResolverTests
    {
        [AssemblyInitialize]
        public static void Initialize(TestContext context)
        {
            string path = Path.GetDirectoryName(typeof(NativeLibrariesResolverTests).Assembly.Location);

            while (Path.GetFileName(path) is not "ComputeSharp")
            {
                path = Path.GetDirectoryName(path);
            }

            SampleProjectDirectory = Path.Combine(path, "samples", "ComputeSharp.Sample.NuGet");

            string
                sampleProjectPath = Path.Combine(SampleProjectDirectory, "ComputeSharp.Sample.NuGet.csproj"),
                packagingProjectPath = Path.Combine(path, "src", "ComputeSharp.Package", "ComputeSharp.Package.msbuildproj");

            // Run dotnet pack and on the packaging project, to ensure the local NuGet package is available.
            Process.Start("dotnet", $"pack {packagingProjectPath} -c Release").WaitForExit();
        }

        /// <summary>
        /// Gets the directory of the ComputeSharp.Sample.NuGet project.
        /// </summary>
        private static string SampleProjectDirectory { get; set; }

        [TestMethod]
        [DataRow(Configuration.Debug, RID.None)]
        [DataRow(Configuration.Debug, RID.Win_x64)]
        [DataRow(Configuration.Release, RID.None)]
        [DataRow(Configuration.Release, RID.Win_x64)]
        public void DotnetRunWorks(Configuration configuration, RID rid)
        {
            Assert.AreEqual(0, Exec(SampleProjectDirectory, "dotnet", $"run -c {configuration} {ToOption(rid)}"));
        }

        [TestMethod]
        [DataRow(Configuration.Debug, RID.None)]
        [DataRow(Configuration.Debug, RID.Win_x64)]
        [DataRow(Configuration.Release, RID.None)]
        [DataRow(Configuration.Release, RID.Win_x64)]
        public void DotnetBuildWithRunningDotnetHostFromProjectDirectoryWorks(Configuration configuration, RID rid)
        {
            BuildSampleProject(configuration, rid);

            string realtivePathToDll = Path.Combine("bin", $"{configuration}", "net5.0", $"{ToDirectory(rid)}", "ComputeSharp.Sample.NuGet.dll");

            Assert.AreEqual(0, Exec(SampleProjectDirectory, "dotnet", realtivePathToDll));
        }

        [TestMethod]
        [DataRow(Configuration.Debug, RID.None)]
        [DataRow(Configuration.Debug, RID.Win_x64)]
        [DataRow(Configuration.Release, RID.None)]
        [DataRow(Configuration.Release, RID.Win_x64)]
        public void DotnetBuildWithRunningDotnetHostDirectlyWorks(Configuration configuration, RID rid)
        {
            BuildSampleProject(configuration, rid);

            string pathToDllDirectory = Path.Combine(SampleProjectDirectory, "bin", $"{configuration}", "net5.0", $"{ToDirectory(rid)}");

            Assert.AreEqual(0, Exec(pathToDllDirectory, "dotnet", "ComputeSharp.Sample.NuGet.dll"));
        }

        [TestMethod]
        [DataRow(Configuration.Debug, RID.None)]
        [DataRow(Configuration.Debug, RID.Win_x64)]
        [DataRow(Configuration.Release, RID.None)]
        [DataRow(Configuration.Release, RID.Win_x64)]
        public void DotnetBuildWithRunningAppHostFromProjectDirectoryWorks(Configuration configuration, RID rid)
        {
            BuildSampleProject(configuration, rid);

            string relativePathToAppHost = Path.Combine("bin", $"{configuration}", "net5.0", $"{ToDirectory(rid)}", "ComputeSharp.Sample.NuGet.exe");

            Assert.AreEqual(0, Exec(SampleProjectDirectory, relativePathToAppHost, ""));
        }

        [TestMethod]
        [DataRow(Configuration.Debug, RID.None)]
        [DataRow(Configuration.Debug, RID.Win_x64)]
        [DataRow(Configuration.Release, RID.None)]
        [DataRow(Configuration.Release, RID.Win_x64)]
        public void DotnetBuildWithRunningAppHostDirectlyWorks(Configuration configuration, RID rid)
        {
            BuildSampleProject(configuration, rid);

            string pathToAppHostDirectory = Path.Combine(SampleProjectDirectory, "bin", $"{configuration}", "net5.0", $"{ToDirectory(rid)}");

            Assert.AreEqual(0, Exec(pathToAppHostDirectory, "ComputeSharp.Sample.NuGet.exe", ""));
        }


        [TestMethod]
        [DataRow(PublishMode.SelfContained, DeploymentMode.Multiassembly, NativeLibrariesDeploymentMode.NotApplicable)]
        [DataRow(PublishMode.FrameworkDependent, DeploymentMode.Multiassembly, NativeLibrariesDeploymentMode.NotApplicable)]
        [DataRow(PublishMode.SelfContained, DeploymentMode.SingleFile, NativeLibrariesDeploymentMode.BundleWithApplication)]
        [DataRow(PublishMode.SelfContained, DeploymentMode.SingleFile, NativeLibrariesDeploymentMode.ExtractToTemporaryDirectory)]
        [DataRow(PublishMode.FrameworkDependent, DeploymentMode.SingleFile, NativeLibrariesDeploymentMode.BundleWithApplication)]
        [DataRow(PublishMode.FrameworkDependent, DeploymentMode.SingleFile, NativeLibrariesDeploymentMode.ExtractToTemporaryDirectory)]
        public void DotnetPublishWorks(PublishMode publishMode, DeploymentMode deploymentMode, NativeLibrariesDeploymentMode nativeLibsDeploymentMode)
        {
            // Publishing without specifying a RID is not supported
            // We do not test Debug builds as it was determined that they are not an important scenario
            Exec(SampleProjectDirectory, "dotnet", $"publish -c Release -r win-x64 {ToOption(publishMode)} {ToOption(deploymentMode)} {ToOption(nativeLibsDeploymentMode)}");
            string pathToAppHost = Path.Combine("bin", $"Release", "net5.0", "win-x64", "publish", "ComputeSharp.Sample.NuGet.exe");
            Assert.AreEqual(0, Exec(SampleProjectDirectory, pathToAppHost, ""));
        }

        /// <summary>
        /// Builds the sample project with a specific configuration and target runtime.
        /// </summary>
        /// <param name="configuration">The configuration to use to build the project.</param>
        /// <param name="rid">The RID to use to build the project.</param>
        private static void BuildSampleProject(Configuration configuration, RID rid)
        {
            Exec(SampleProjectDirectory, "dotnet", $"build -c {configuration} {ToOption(rid)}");
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

            using Process process = Process.Start(startInfo);

            process.WaitForExit();

            return process.ExitCode;
        }

        /// <summary>
        /// Gets a text representation of a cmd option for the dotnet tool, for the given <see cref="RID"/>.
        /// </summary>
        /// <param name="rid">The input <see cref="RID"/> value.</param>
        /// <returns>A text representation for <paramref name="rid"/>.</returns>
        private static string ToOption(RID rid) => rid switch
        {
            RID.None => "",
            RID.Win_x64 => "-r win-x64",
            _ => throw new InvalidEnumArgumentException(nameof(rid), (int)rid, typeof(RID))
        };

        private static string ToOption(PublishMode publishMode) => publishMode switch
        {
            PublishMode.FrameworkDependent => "--self-contained false",
            PublishMode.SelfContained => "--self-contained true",
            _ => throw new InvalidEnumArgumentException(nameof(publishMode), (int)publishMode, typeof(PublishMode))
        };

        private static string ToOption(DeploymentMode deploymentMode) => deploymentMode switch
        {
            DeploymentMode.Multiassembly => "/p:PublishSingleFile=false",
            DeploymentMode.SingleFile => "/p:PublishSingleFile=true",
            _ => throw new InvalidEnumArgumentException(nameof(deploymentMode), (int)deploymentMode, typeof(DeploymentMode))
        };

        private static string ToOption(NativeLibrariesDeploymentMode deploymentMode) => deploymentMode switch
        {
            NativeLibrariesDeploymentMode.NotApplicable => "",
            NativeLibrariesDeploymentMode.BundleWithApplication => "/p:IncludeNativeLibrariesForSelfExtract=false",
            NativeLibrariesDeploymentMode.ExtractToTemporaryDirectory => "/p:IncludeNativeLibrariesForSelfExtract=true",
            _ => throw new InvalidEnumArgumentException(nameof(deploymentMode), (int)deploymentMode, typeof(NativeLibrariesDeploymentMode))
        };

        /// <summary>
        /// Gets a text representation of the build folder for the given <see cref="RID"/>.
        /// </summary>
        /// <param name="rid">The input <see cref="RID"/> value.</param>
        /// <returns>A text representation for <paramref name="rid"/>.</returns>
        private static string ToDirectory(RID rid) => rid switch
        {
            RID.None => "",
            RID.Win_x64 => "win-x64",
            _ => throw new InvalidEnumArgumentException(nameof(rid), (int)rid, typeof(RID))
        };

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

        public enum PublishMode
        {
            FrameworkDependent,
            SelfContained
        }

        public enum DeploymentMode
        {
            Multiassembly,
            SingleFile
        }

        public enum NativeLibrariesDeploymentMode
        {
            NotApplicable,
            ExtractToTemporaryDirectory,
            BundleWithApplication
        }
    }
}
