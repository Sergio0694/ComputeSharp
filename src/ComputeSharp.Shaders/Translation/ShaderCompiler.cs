using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.Core.Extensions;
using ComputeSharp.Exceptions;
using TerraFX.Interop;
using FX = TerraFX.Interop.Windows;

namespace ComputeSharp.Shaders.Translation
{
    /// <summary>
    /// A <see langword="class"/> that uses the DXC APIs to compile compute shaders.
    /// </summary>
    internal static unsafe class ShaderCompiler
    {
        /// <summary>
        /// The <see cref="IDxcCompiler"/> instance to use to create the bytecode for HLSL sources.
        /// </summary>
        private static readonly ComPtr<IDxcCompiler> DxcCompiler;

        /// <summary>
        /// The <see cref="IDxcLibrary"/> instance to use to work with <see cref="DxcCompiler"/>.
        /// </summary>
        private static readonly ComPtr<IDxcLibrary> DxcLibrary;

        /// <summary>
        /// The <see cref="IDxcIncludeHandler"/> instance used to compile shaders with <see cref="DxcCompiler"/>.
        /// </summary>
        private static readonly ComPtr<IDxcIncludeHandler> DxcIncludeHandler;

        /// <summary>
        /// Initializes <see cref="DxcCompiler"/>, <see cref="DxcLibrary"/> and <see cref="DxcIncludeHandler"/>.
        /// </summary>
        static ShaderCompiler()
        {
            InitializeDxcLibrariesLoading();

            using ComPtr<IDxcCompiler> dxcCompiler = default;
            using ComPtr<IDxcLibrary> dxcLibrary = default;
            using ComPtr<IDxcIncludeHandler> dxcIncludeHandler = default;

            Guid dxcCompilerCLGuid = FX.CLSID_DxcCompiler;
            Guid dxcLibraryCLGuid = FX.CLSID_DxcLibrary;

            FX.DxcCreateInstance(&dxcCompilerCLGuid, FX.__uuidof<IDxcCompiler>(), dxcCompiler.GetVoidAddressOf()).Assert();
            FX.DxcCreateInstance(&dxcLibraryCLGuid, FX.__uuidof<IDxcLibrary>(), dxcLibrary.GetVoidAddressOf()).Assert();

            dxcLibrary.Get()->CreateIncludeHandler(dxcIncludeHandler.GetAddressOf()).Assert();

            DxcCompiler = dxcCompiler.Move();
            DxcLibrary = dxcLibrary.Move();
            DxcIncludeHandler = dxcIncludeHandler.Move();
        }

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
            FX.ResolveLibrary += (n, a, s) => ResolveLibrary(n, a, s, isNuGetRuntimeLibrariesDirectoryPresent);
        }

        /// <summary>
        /// A custom resolver to override the default library resolution behavior for the DXC and DXIL libraries.
        /// This either loads the libraries from the NuGet directory, or just ensures that DXIL is always loaded.
        /// </summary>
        /// <inheritdoc cref="DllImportResolver"/>
        private static IntPtr ResolveLibrary(string libraryName, Assembly assembly, DllImportSearchPath? searchPath, bool isNuGetRuntimeLibrariesDirectoryPresent)
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
        }

        /// <summary>
        /// Compiles a new HLSL shader from the input source code.
        /// </summary>
        /// <param name="source">The HLSL source code to compile.</param>
        /// <returns>The bytecode for the compiled shader.</returns>
        [Pure]
        public static ComPtr<IDxcBlob> CompileShader(ReadOnlySpan<char> source)
        {
            using ComPtr<IDxcBlobEncoding> dxcBlobEncoding = default;
            using ComPtr<IDxcOperationResult> dxcOperationResult = default;
            using ComPtr<IDxcBlob> dxcBlobBytecode = default;

            // Get the encoded blob from the source code
            fixed (char* p = source)
            {
                DxcLibrary.Get()->CreateBlobWithEncodingOnHeapCopy(
                    p,
                    (uint)source.Length * 2,
                    1200,
                    dxcBlobEncoding.GetAddressOf()).Assert();
            }

            // Try to compile the new compute shader
            fixed (char* shaderName = "")
            fixed (char* entryPoint = nameof(IComputeShader.Execute))
            fixed (char* shaderProfile = "cs_6_0")
            fixed (char* optimization = "-O3")
            {
                DxcCompiler.Get()->Compile(
                    dxcBlobEncoding.Upcast<IDxcBlobEncoding, IDxcBlob>().Get(),
                    (ushort*)shaderName,
                    (ushort*)entryPoint,
                    (ushort*)shaderProfile,
                    (ushort**)&optimization,
                    1,
                    null,
                    0,
                    DxcIncludeHandler.Get(),
                    dxcOperationResult.GetAddressOf()).Assert();
            }

            int status;

            dxcOperationResult.Get()->GetStatus(&status).Assert();

            // The compilation was successful, so we can extract the shader bytecode
            if (status == 0)
            {
                dxcOperationResult.Get()->GetResult(dxcBlobBytecode.GetAddressOf()).Assert();

                return dxcBlobBytecode.Move();
            }

            return ThrowHslsCompilationException(dxcOperationResult);
        }

        /// <summary>
        /// Throws an exception when a shader compilation fails.
        /// </summary>
        /// <param name="dxcOperationResult">The input (faulting) operation.</param>
        /// <returns>This method always throws and never actually returs.</returns>
        [DoesNotReturn]
        [MethodImpl(MethodImplOptions.NoInlining)]
        private static ComPtr<IDxcBlob> ThrowHslsCompilationException(IDxcOperationResult* dxcOperationResult)
        {
            using ComPtr<IDxcBlobEncoding> dxcBlobEncodingError = default;

            dxcOperationResult->GetErrorBuffer(dxcBlobEncodingError.GetAddressOf()).Assert();

            string message = new((sbyte*)dxcBlobEncodingError.Get()->GetBufferPointer());

            throw new HlslCompilationException(message);
        }
    }
}
