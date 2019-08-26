using System;
using System.Diagnostics.Contracts;
using Vortice.DirectX.Direct3D12;
using Vortice.Dxc;

namespace ComputeSharp.Shaders.Translation
{
    /// <summary>
    /// A <see langword="class"/> that uses the <see cref="DxcCompiler"/> APIs to compile compute shaders
    /// </summary>
    internal sealed class ShaderCompiler
    {
        /// <summary>
        /// Gets the singleton <see cref="ShaderCompiler"/> instance to use
        /// </summary>
        public static ShaderCompiler Instance { get; } = new ShaderCompiler();

        /// <summary>
        /// The <see cref="IDxcLibrary"/> instance to use to create the bytecode for HLSL sources
        /// </summary>
        private readonly IDxcLibrary Library = Dxc.CreateDxcLibrary();

        /// <summary>
        /// Compiles a new HLSL shader from the input source code
        /// </summary>
        /// <param name="source">The HLSL source code to compile</param>
        /// <returns>The bytecode for the compiled shader</returns>
        [Pure]
        public ShaderBytecode CompileShader(string source)
        {
            var result = DxcCompiler.Compile(DxcShaderStage.ComputeShader, source, "Main", string.Empty, new DxcCompilerOptions { ShaderModel = DxcShaderModel.Model6_1 });

            // Get the compiled bytecode in case of success
            if (result.GetStatus() == 0)
            {
                IDxcBlob blob = result.GetResult();
                byte[] bytecode = Dxc.GetBytesFromBlob(blob);

                return bytecode;
            }

            // Compile error
            string resultText = Dxc.GetStringFromBlob(Library, result.GetErrors());
            throw new Exception(resultText);
        }
    }
}
