using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;

namespace ComputeSharp.Tests.NativeLibrariesResolver
{
    [TestClass]
    public class NativeLibrariesResolverTests
    {
        static NativeLibrariesResolverTests()
        {
            var path = Path.GetDirectoryName(typeof(NativeLibrariesResolverTests).Assembly.Location);
            while (Path.GetFileName(path) is not "ComputeSharp")
            {
                path = Path.GetDirectoryName(path);
            }

            SampleProjectDirectory = Path.Combine(path, "samples", "ComputeSharp.Sample.NuGet");
            var sampleProjectPath = Path.Combine(SampleProjectDirectory, "ComputeSharp.Sample.NuGet.csproj");

            var packagingProjectPath = Path.Combine(path, "src", "ComputeSharp.Package", "ComputeSharp.Package.msbuildproj");
            Process.Start("dotnet", $"pack {packagingProjectPath} -c Release").WaitForExit();
            // We use "--no-restore" where possible to speed up the tests
            Process.Start("dotnet", $"restore {sampleProjectPath}").WaitForExit();
        }

        private static string SampleProjectDirectory { get; }

        [TestMethod]
        [DataRow(Configuration.Debug, RID.None)]
        [DataRow(Configuration.Debug, RID.Win_x64)]
        [DataRow(Configuration.Release, RID.None)]
        [DataRow(Configuration.Release, RID.Win_x64)]
        public void DotnetRunWorks(Configuration configuration, RID rid)
        {
            // "dotnet run" fails with "--no-restore"
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
            var realtivePathToDll = Path.Combine("bin", $"{configuration}", "net5.0", $"{ToDirectory(rid)}", "ComputeSharp.Sample.NuGet.dll");
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
            var pathToDllDirectory = Path.Combine(SampleProjectDirectory, "bin", $"{configuration}", "net5.0", $"{ToDirectory(rid)}");
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
            var relativePathToAppHost = Path.Combine("bin", $"{configuration}", "net5.0", $"{ToDirectory(rid)}", "ComputeSharp.Sample.NuGet.exe");
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
            var pathToAppHostDirectory = Path.Combine(SampleProjectDirectory, "bin", $"{configuration}", "net5.0", $"{ToDirectory(rid)}");
            Assert.AreEqual(0, Exec(pathToAppHostDirectory, "ComputeSharp.Sample.NuGet.exe", ""));
        }

        [TestMethod]
        [DataRow(Configuration.Debug)]
        [DataRow(Configuration.Release)]
        public void DotnetPublishWorks(Configuration configuration)
        {
            // We do not support publishing without the RID
            Exec(SampleProjectDirectory, "dotnet", $"publish -c {configuration} -r win-x64 --no-restore");
            var pathToAppHost = Path.Combine("bin", $"{configuration}", "net5.0", "win-x64", "publish", "ComputeSharp.Sample.NuGet.exe");
            Assert.AreEqual(0, Exec(SampleProjectDirectory, pathToAppHost, ""));
        }

        private static void BuildSampleProject(Configuration configuration, RID rid)
        {
            Exec(SampleProjectDirectory, "dotnet", $"build -c {configuration} {ToOption(rid)} --no-restore");
        }

        private static int Exec(string workingDirectory, string filePath, string arguments)
        {
            var startInfo = new ProcessStartInfo
            {
                UseShellExecute = true,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = filePath,
                Arguments = arguments,
                WorkingDirectory = workingDirectory
            };

            var process = Process.Start(startInfo);
            process.WaitForExit();

            return process.ExitCode;
        }

        private static string ToOption(RID rid) => rid switch
        {
            RID.None => "",
            RID.Win_x64 => "-r win-x64",
            _ => throw new InvalidEnumArgumentException(nameof(rid), (int)rid, typeof(RID))
        };

        private static string ToDirectory(RID rid) => rid switch
        {
            RID.None => "",
            RID.Win_x64 => "win-x64",
            _ => throw new InvalidEnumArgumentException(nameof(rid), (int)rid, typeof(RID))
        };

        public enum Configuration
        {
            Debug,
            Release
        }

        public enum RID
        {
            None,
            Win_x64,
        }
    }
}
