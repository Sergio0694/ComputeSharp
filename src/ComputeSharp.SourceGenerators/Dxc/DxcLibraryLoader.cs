using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;

#pragma warning disable RS1035

namespace ComputeSharp.SourceGenerators.Dxc;

/// <summary>
/// A <see langword="class"/> that handles loading the DXC libraries.
/// </summary>
internal sealed class DxcLibraryLoader
{
    /// <summary>
    /// An object to use to synchronize loading the DXC libraries.
    /// </summary>
    private static readonly Lock LoadingLock = new();

    /// <summary>
    /// Indicates whether the required <c>dxcompiler.dll</c> and <c>dxil.dll</c> libraries have been loaded.
    /// </summary>
    private static volatile bool areDxcLibrariesLoaded;

    /// <summary>
    /// Extracts and loads the <c>dxcompiler.dll</c> and <c>dxil.dll</c> libraries.
    /// </summary>
    /// <exception cref="NotSupportedException">Thrown if the CPU architecture is not supported.</exception>
    /// <exception cref="Win32Exception">Thrown if a library fails to load.</exception>
    public static void LoadNativeDxcLibraries()
    {
        // Extracts a specified native library for a given runtime identifier
        static string ExtractLibrary(string folder, string rid, string name)
        {
            string sourceFilename = $"ComputeSharp.SourceGenerators.ComputeSharp.Libraries.{rid}.{name}.dll";
            string targetFilename = Path.Combine(folder, rid, $"{name}.dll");

            _ = Directory.CreateDirectory(Path.GetDirectoryName(targetFilename)!);

            using Stream sourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(sourceFilename)!;

            try
            {
                using Stream destinationStream = File.Open(targetFilename, FileMode.CreateNew, FileAccess.Write);

                sourceStream.CopyTo(destinationStream);
            }
            catch (IOException)
            {
            }

            return targetFilename;
        }

        // Loads a target native library
        static void LoadLibrary(string filename)
        {
            try
            {
                _ = NativeLibrary.Load(filename);
            }
            catch (DllNotFoundException e)
            {
                // Rethrow as a Win32Exception, as that is what callers catch to report a diagnostic
                // rather than crashing. The message already includes the error from the OS loader.
                throw new Win32Exception(e.HResult, e.Message);
            }
        }

        if (areDxcLibrariesLoaded)
        {
            return;
        }

        lock (LoadingLock)
        {
            if (areDxcLibrariesLoaded)
            {
                return;
            }

            string rid = RuntimeInformation.ProcessArchitecture switch
            {
                Architecture.X64 => "x64",
                Architecture.Arm64 => "arm64",
                _ => throw new NotSupportedException("Invalid process architecture")
            };

            string folder = Path.Combine(Path.GetTempPath(), "ComputeSharp.SourceGenerators", Path.GetRandomFileName());

            LoadLibrary(ExtractLibrary(folder, rid, "dxil"));
            LoadLibrary(ExtractLibrary(folder, rid, "dxcompiler"));

            areDxcLibrariesLoaded = true;
        }
    }
}