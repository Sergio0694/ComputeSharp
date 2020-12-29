using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.Core.Extensions;
using ComputeSharp.Exceptions;
using Microsoft.Toolkit.Diagnostics;
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
            string
                dxcompilerPath = Path.GetFullPath(@$"runtimes\win-x64\native\dxcompiler.dll"),
                dxilPath = Path.GetFullPath(@$"runtimes\win-x64\native\dxil.dll");

            if (File.Exists(dxcompilerPath))
            {
                FX.ResolveLibrary += ResolveLibrary;
            }

            if (File.Exists(dxilPath))
            {
                fixed (char* p = @"runtimes\win-x64\native")
                {
                    int result = FX.SetDllDirectoryW((ushort*)p);

                    if (result == 0) ThrowHelper.ThrowWin32Exception();
                }
            }
        }

        /// <summary>
        /// A custom resolver to override the default library resolution behavior for the DXC and DXIL libraries.
        /// </summary>
        /// <inheritdoc cref="DllImportResolver"/>
        private static IntPtr ResolveLibrary(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
        {
            if (libraryName != "dxcompiler") return IntPtr.Zero;

            string libraryPath = Path.GetFullPath(@$"runtimes\win-x64\native\{libraryName}.dll");

            if (NativeLibrary.TryLoad(libraryPath, out IntPtr handle))
            {
                return handle;
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
