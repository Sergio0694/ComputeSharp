using System;
using System.Diagnostics.Contracts;
using ComputeSharp.Exceptions;
using ComputeSharp.Graphics.Helpers;
using TerraFX.Interop;
using FX = TerraFX.Interop.Windows;

namespace ComputeSharp.Shaders.Translation
{
    /// <summary>
    /// A <see langword="class"/> that uses the DXC APIs to compile compute shaders.
    /// </summary>
    internal static unsafe class ShaderCompiler2
    {
        /// <summary>
        /// The <see cref="IDxcCompiler"/> instance to use to create the bytecode for HLSL sources.
        /// </summary>
        private static readonly IDxcCompiler* DxcCompiler;

        /// <summary>
        /// The <see cref="IDxcLibrary"/> instance to use to work with <see cref="DxcCompiler"/>.
        /// </summary>
        private static readonly IDxcLibrary* DxcLibrary;

        /// <summary>
        /// The <see cref="IDxcIncludeHandler"/> instance used to compile shaders with <see cref="DxcCompiler"/>.
        /// </summary>
        private static readonly IDxcIncludeHandler* DxcIncludeHandler;

        /// <summary>
        /// Initializes <see cref="DxcCompiler"/>, <see cref="DxcLibrary"/> and <see cref="DxcIncludeHandler"/>
        /// </summary>
        static ShaderCompiler2()
        {
            Guid dxcCompilerCLGuid = FX.CLSID_DxcCompiler;
            Guid dxcCompilerGuid = FX.IID_IDxcCompiler;
            IDxcCompiler* dxcCompiler;

            int result = FX.DxcCreateInstance(&dxcCompilerCLGuid, &dxcCompilerGuid, (void**)&dxcCompiler);

            ThrowHelper.ThrowIfFailed(result);

            DxcCompiler = dxcCompiler;

            Guid dxcLibraryCLGuid = FX.CLSID_DxcLibrary;
            Guid dxcLibraryGuid = FX.IID_IDxcLibrary;
            IDxcLibrary* dxcLibrary;

            result = FX.DxcCreateInstance(&dxcLibraryCLGuid, &dxcLibraryGuid, (void**)&dxcLibrary);

            ThrowHelper.ThrowIfFailed(result);

            DxcLibrary = dxcLibrary;

            IDxcIncludeHandler* dxcIncludeHandler;

            result = dxcLibrary->CreateIncludeHandler(&dxcIncludeHandler);

            ThrowHelper.ThrowIfFailed(result);

            DxcIncludeHandler = dxcIncludeHandler;
        }

        /// <summary>
        /// Compiles a new HLSL shader from the input source code
        /// </summary>
        /// <param name="source">The HLSL source code to compile</param>
        /// <returns>The bytecode for the compiled shader</returns>
        [Pure]
        public static IDxcBlob* CompileShader(string source)
        {
            int result;
            IDxcBlobEncoding* dxcBlobEncoding;
            IDxcOperationResult* dxcOperationResult;

            // Get the encoded blob from the source code
            fixed (char* p = source)
            {
                result = DxcLibrary->CreateBlobWithEncodingOnHeapCopy(p, (uint)source.Length * 2, 1200, &dxcBlobEncoding);

                ThrowHelper.ThrowIfFailed(result);
            }

            fixed (char* entryPoint = "CSMain")
            fixed (char* shaderProfile = "cs_6_1")
            fixed (char* optimization = "-O3")
            {
                char nullTerminator = '\0';

                // Try to compile the new compute shader
                result = DxcCompiler->Compile(
                    (IDxcBlob*)dxcBlobEncoding,
                    (ushort*)&nullTerminator,
                    (ushort*)entryPoint,
                    (ushort*)shaderProfile,
                    (ushort**)&optimization,
                    1,
                    null,
                    0,
                    DxcIncludeHandler,
                    &dxcOperationResult);

                dxcBlobEncoding->Release();

                // Handle failures during the operation that are not external to the compiler itself.
                // That is, if this call is successful, the compiler might still have found an error
                // in the source code, but the operation itself would have been run correctly. Here
                // we handle the scenario where that is not the case, so we dispose if needed and throw.
                ThrowHelper.ThrowIfFailed(result, dxcOperationResult);
            }

            int status;

            // Check whether the actual compilation was executed successfully (exit code 0)
            result = dxcOperationResult->GetStatus(&status);

            ThrowHelper.ThrowIfFailed(result, dxcOperationResult);

            if (status != 0)
            {
                ThrowHslsCompilationException(dxcOperationResult);
            }

            IDxcBlob* dxcBlob;

            result = dxcOperationResult->GetResult(&dxcBlob);

            ThrowHelper.ThrowIfFailed(result);

            return dxcBlob;
        }

        private static void ThrowHslsCompilationException(IDxcOperationResult* dxcOperationResult)
        {
            IDxcBlobEncoding* dxcBlobEncodingError;

            int result = dxcOperationResult->GetErrorBuffer(&dxcBlobEncodingError);

            dxcOperationResult->Release();

            ThrowHelper.ThrowIfFailed(result, dxcBlobEncodingError);

            throw new HlslCompilationException("Shader compilation error");
        }
    }
}
