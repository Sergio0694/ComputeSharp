using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;

namespace ComputeSharp.Tests.NativeLibrariesResolver
{
    [TestClass]
    public class NativeLibrariesResolverTests
    {
        static NativeLibrariesResolverTests()
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

            // Run dotnet pack and on the packaging project, to ensure the local NuGet package is
            // available. Then run dotnet restore on the sample project. This is done so that
            // other tests can skip the restore step to run faster, as there's lots of them.
            Process.Start("dotnet", $"pack {packagingProjectPath} -c Release").WaitForExit();
            Process.Start("dotnet", $"restore {sampleProjectPath}").WaitForExit();
        }

        /// <summary>
        /// Gets the directory of the ComputeSharp.Sample.NuGet project.
        /// </summary>
        private static string SampleProjectDirectory { get; }

        [TestMethod]
        [DataRow(Configuration.Debug, RID.None)]
        [DataRow(Configuration.Debug, RID.Win_x64)]
        [DataRow(Configuration.Release, RID.None)]
        [DataRow(Configuration.Release, RID.Win_x64)]
        public void DotnetRunWorks(Configuration configuration, RID rid)
        {
            // "dotnet run" fails with "--no-restore", so in this case we restore packages as well
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
        [DataRow(Configuration.Debug)]
        [DataRow(Configuration.Release)]
        public void DotnetPublishWorks(Configuration configuration)
        {
            // Publishing without specifying an RID is not supported
            Exec(SampleProjectDirectory, "dotnet", $"publish -c {configuration} -r win-x64 --no-restore");

            string pathToAppHost = Path.Combine("bin", $"{configuration}", "net5.0", "win-x64", "publish", "ComputeSharp.Sample.NuGet.exe");

            Assert.AreEqual(0, Exec(SampleProjectDirectory, pathToAppHost, ""));
        }

        /// <summary>
        /// Builds the sample project with a specific configuration and target runtime.
        /// </summary>
        /// <param name="configuration">The configuration to use to build the project.</param>
        /// <param name="rid">The RID to use to build the project.</param>
        private static void BuildSampleProject(Configuration configuration, RID rid)
        {
            Exec(SampleProjectDirectory, "dotnet", $"build -c {configuration} {ToOption(rid)} --no-restore");
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

            Process process = Process.Start(startInfo);

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
            Win_x64,
        }
    }
}
