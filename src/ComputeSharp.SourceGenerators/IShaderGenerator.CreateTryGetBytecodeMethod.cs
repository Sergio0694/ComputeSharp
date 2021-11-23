using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace ComputeSharp.SourceGenerators;

/// <inheritdoc/>
public sealed partial class IShaderGenerator
{
    /// <summary>
    /// Extracts and loads the <c>dxcompiler.dll</c> and <c>dxil.dll</c> libraries.
    /// </summary>
    /// <exception cref="NotSupportedException">Thrown if the CPU architecture is not supported.</exception>
    /// <exception cref="Win32Exception">Thrown if a library fails to load.</exception>
    private static void LoadNativeDxcLibraries()
    {
        string rid = RuntimeInformation.ProcessArchitecture switch
        {
            Architecture.X64 => "win-x64",
            Architecture.Arm64 => "win-arm64",
            _ => throw new NotSupportedException("Invalid process architecture")
        };

        // Extracts a specified native library for a given runtime identifier
        static string ExtractLibrary(string rid, string name)
        {
            string sourceFilename = $"ComputeSharp.SourceGenerators.Libraries.{rid}.{name}.dll";
            string targetFilename = Path.Combine("%TEMP%", "ComputeSharp.SourceGenerators", rid, $"{name}.dll");

            using Stream sourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(sourceFilename);

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
        static unsafe void LoadLibrary(string filename)
        {
            [DllImport("kernel32", ExactSpelling = true, SetLastError = true)]
            static extern void* LoadLibraryW(ushort* lpLibFileName);

            fixed (char* p = filename)
            {
                if (LoadLibraryW((ushort*)p) is null)
                {
                    int hresult = Marshal.GetLastWin32Error();

                    throw new Win32Exception(hresult, $"Failed to load {Path.GetFileName(filename)}.");
                }
            }
        }

        LoadLibrary(ExtractLibrary(rid, "dxil"));
        LoadLibrary(ExtractLibrary(rid, "dxcompiler"));
    }
}
