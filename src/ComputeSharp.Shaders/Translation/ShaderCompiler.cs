using System;
using System.Diagnostics.Contracts;
using DotNetDxc;
using SharpDX.Direct3D12;

namespace ComputeSharp.Shaders.Translation
{
    /// <summary>
    /// A <see langword="class"/> that uses the <see cref="HlslDxcLib"/> APIs to compile compute shaders
    /// </summary>
    internal sealed class ShaderCompiler
    {
        /// <summary>
        /// Gets the singleton <see cref="ShaderCompiler"/> instance to use
        /// </summary>
        public static ShaderCompiler Instance { get; } = new ShaderCompiler();

        /// <summary>
        /// The <see cref="IDxcCompiler"/> instance to use to create new HLSL shaders
        /// </summary>
        private readonly IDxcCompiler Compiler;

        /// <summary>
        /// The <see cref="IDxcLibrary"/> instance to use to create the bytecode for HLSL sources
        /// </summary>
        private readonly IDxcLibrary Library;

        /// <summary>
        /// Creates a new <see cref="ShaderCompiler"/> instance
        /// </summary>
        private ShaderCompiler()
        {
            HlslDxcLib.DxcCreateInstanceFn = DefaultDxcLib.GetDxcCreateInstanceFn();
            Compiler = HlslDxcLib.CreateDxcCompiler();
            Library = HlslDxcLib.CreateDxcLibrary();
        }

        /// <summary>
        /// Compiles a new HLSL shader from the input source code
        /// </summary>
        /// <param name="source">The HLSL source code to compile</param>
        /// <returns>The bytecode for the compiled shader</returns>
        [Pure]
        public ShaderBytecode CompileShader(string source)
        {
            const uint CP_UTF16 = 1200;

            IDxcBlobEncoding sourceBlob = Library.CreateBlobWithEncodingOnHeapCopy(source, (uint)(source.Length * 2), CP_UTF16);
            IDxcOperationResult result = Compiler.Compile(sourceBlob, "", "CSMain", "cs_6_1", new[] { "-Zpr" }, 1, null, 0, Library.CreateIncludeHandler());

            // Get the compiled bytecode in case of success
            if (result.GetStatus() == 0)
            {
                IDxcBlob blob = result.GetResult();
                byte[] bytecode = GetBytesFromBlob(blob);

                return bytecode;
            }

            // Compile error
            string resultText = GetStringFromBlob(Library, result.GetErrors());
            throw new Exception(resultText);
        }

        /// <summary>
        /// Reads the buffer content from an input <see cref="IDxcBlob"/> instance
        /// </summary>
        /// <param name="blob">The input <see cref="IDxcBlob"/> instance to read data from</param>
        /// <returns>The read bytes buffer, as an array</returns>
        [Pure]
        private static unsafe byte[] GetBytesFromBlob(IDxcBlob blob)
        {
            byte* pMem = (byte*)blob.GetBufferPointer();
            uint size = blob.GetBufferSize();
            byte[] result = new byte[size];

            new Span<byte>(pMem, (int)size).CopyTo(result.AsSpan());

            return result;
        }

        /// <summary>
        /// Reads a <see cref="string"/> value from an input <see cref="IDxcBlob"/> instance
        /// </summary>
        /// <param name="library">The source <see cref="IDxcLibrary"/> to use to load the data</param>
        /// <param name="blob">The input <see cref="IDxcBlob"/> instance to read data from</param>
        /// <returns>The resulting <see cref="string"/> read from the input data buffer</returns>
        [Pure]
        private static unsafe string GetStringFromBlob(IDxcLibrary library, IDxcBlob blob)
        {
            blob = library.GetBlobAstUf16(blob);
            return new string(blob.GetBufferPointer(), 0, (int)(blob.GetBufferSize() / 2));
        }
    }
}
