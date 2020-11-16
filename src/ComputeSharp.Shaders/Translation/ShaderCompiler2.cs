using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
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
        /// Initializes <see cref="DxcCompiler"/>, <see cref="DxcLibrary"/> and <see cref="DxcIncludeHandler"/>
        /// </summary>
        static ShaderCompiler2()
        {
            using ComPtr<IDxcCompiler> dxcCompiler = default;
            using ComPtr<IDxcLibrary> dxcLibrary = default;
            using ComPtr<IDxcIncludeHandler> dxcIncludeHandler = default;

            Guid dxcCompilerCLGuid = FX.CLSID_DxcCompiler;
            Guid dxcCompilerGuid = FX.IID_IDxcCompiler;
            Guid dxcLibraryCLGuid = FX.CLSID_DxcLibrary;
            Guid dxcLibraryGuid = FX.IID_IDxcLibrary;

            int result = FX.DxcCreateInstance(&dxcCompilerCLGuid, &dxcCompilerGuid, dxcCompiler.GetVoidAddressOf());

            ThrowHelper.ThrowIfFailed(result);

            result = FX.DxcCreateInstance(&dxcLibraryCLGuid, &dxcLibraryGuid, dxcLibrary.GetVoidAddressOf());

            ThrowHelper.ThrowIfFailed(result);

            result = dxcLibrary.Get()->CreateIncludeHandler(dxcIncludeHandler.GetAddressOf());

            ThrowHelper.ThrowIfFailed(result);

            DxcCompiler = dxcCompiler;
            DxcLibrary = dxcLibrary;
            DxcIncludeHandler = dxcIncludeHandler;
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

            int result;

            // Get the encoded blob from the source code
            fixed (char* p = source)
            {
                result = DxcLibrary.Get()->CreateBlobWithEncodingOnHeapCopy(p, (uint)source.Length * 2, 1200, dxcBlobEncoding.GetAddressOf());

                ThrowHelper.ThrowIfFailed(result);
            }

            // Try to compile the new compute shader
            fixed (char* entryPoint = "CSMain")
            fixed (char* shaderProfile = "cs_6_1")
            fixed (char* optimization = "-O3")
            {
                char nullTerminator = '\0';

                result = DxcCompiler.Get()->Compile(
                    dxcBlobEncoding.Upcast<IDxcBlobEncoding, IDxcBlob>().Get(),
                    (ushort*)&nullTerminator,
                    (ushort*)entryPoint,
                    (ushort*)shaderProfile,
                    (ushort**)&optimization,
                    1,
                    null,
                    0,
                    DxcIncludeHandler.Get(),
                    dxcOperationResult.GetAddressOf());

                ThrowHelper.ThrowIfFailed(result);
            }

            int status;

            result = dxcOperationResult.Get()->GetStatus(&status);

            ThrowHelper.ThrowIfFailed(result);

            // The compilation was successful, so we can extract the shader bytecode
            if (status == 0)
            {
                result = dxcOperationResult.Get()->GetResult(dxcBlobBytecode.GetAddressOf());

                ThrowHelper.ThrowIfFailed(result);

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
        private static ComPtr<IDxcBlob> ThrowHslsCompilationException(ComPtr<IDxcOperationResult> dxcOperationResult)
        {
            using ComPtr<IDxcBlobEncoding> dxcBlobEncodingError = default;

            int result = dxcOperationResult.Get()->GetErrorBuffer(dxcBlobEncodingError.GetAddressOf());

            ThrowHelper.ThrowIfFailed(result);

            using ComPtr<IDxcBlobUtf16> dxcBlobUtf16Error = default;

            result = DxcLibrary.Get()->GetBlobAsUtf16(
                dxcBlobEncodingError.Upcast<IDxcBlobEncoding, IDxcBlob>().Get(),
                dxcBlobUtf16Error.Upcast<IDxcBlobUtf16, IDxcBlobEncoding>().GetAddressOf());

            ThrowHelper.ThrowIfFailed(result);

            string message = new string(
                (char*)dxcBlobUtf16Error.Get()->GetStringPointer(),
                0,
                checked((int)dxcBlobUtf16Error.Get()->GetStringLength()));

            throw new HlslCompilationException(message);
        }
    }
}
