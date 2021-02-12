using System;
using System.IO;
using System.Runtime.InteropServices;
using FX = TerraFX.Interop.Windows;

namespace ComputeSharp.Shaders.Translation
{
    /// <inheritdoc cref="ShaderCompiler"/>
    internal sealed unsafe partial class ShaderCompiler
    {
        /// <summary>
        /// Initializes the DLL resolvers for the dxcompiler.dll and dxil.dll libraries.
        /// </summary>
        private static void InitializeDxcLibrariesLoading()
        {
            // Test whether the native libraries are present in the same folder of the executable
            // (which is the case when the program was built with a runtime identifier), or whether
            // they are in the "runtimes\win-x64\native" folder in the executable directory.
            string nugetNativeLibsPath = Path.Combine(AppContext.BaseDirectory, @"runtimes\win-x64\native");
            bool isNuGetRuntimeLibrariesDirectoryPresent = Directory.Exists(nugetNativeLibsPath);

            // Register a custom library resolver for the two DXC libraries. We need to either manually load the two
            // libraries from the NuGet directory, if an RID is not in use, or we need to ensure that dxil.dll is
            // loaded correctly in case the program was executed with the host being in another directory.
            // This happens when doing eg. "dotnet bin\Debug\net5.0\MyApp.dll", which would crash at runtime.
            FX.ResolveLibrary += (libraryName, assembly, searchPath) =>
            {
                if (libraryName is not "dxcompiler") return IntPtr.Zero;

                if (isNuGetRuntimeLibrariesDirectoryPresent)
                {
                    string
                        dxcompilerPath = Path.Combine(AppContext.BaseDirectory, @"runtimes\win-x64\native\dxcompiler.dll"),
                        dxilPath = Path.Combine(AppContext.BaseDirectory, @"runtimes\win-x64\native\dxil.dll");

                    // Load DXIL first so that DXC doesn't fail to load it, and then DXIL, both from the NuGet path
                    if (NativeLibrary.TryLoad(dxilPath, out _) && NativeLibrary.TryLoad(dxcompilerPath, out IntPtr handle))
                    {
                        return handle;
                    }
                }
                else
                {
                    // Even when the two libraries are correctly copied next to the executable in use, we load them
                    // manually to ensure the operation is successful. This is to avoid failures in cases such as when
                    // doing "dotnet bin\MyApp.dll", ie. when the host is in another path than the executable in use.
                    // This is probably because DXIL is a native dependency for DXC, but the way Windows loads these
                    // libraries doesn't take into account the .NET concepts of "app directory": neither the current "bin"
                    // directory nor the "process directory", which is "C:\Program Files\dotnet", actually contain the
                    // native library we need, hence the runtime crash. Manually loading the library this way solves this.
                    if (NativeLibrary.TryLoad("dxil", assembly, searchPath, out _) &&
                        NativeLibrary.TryLoad("dxcompiler", assembly, searchPath, out IntPtr handle))
                    {
                        return handle;
                    }
                }

                return IntPtr.Zero;
            };
        }
    }
}
