using System.Diagnostics.Contracts;
using ComputeSharp.Exceptions;
using Vortice.Direct3D12;
using Vortice.Dxc;

namespace ComputeSharp.Shaders.Translation
{
    /// <summary>
    /// A <see langword="class"/> that uses the <see cref="DxcCompiler"/> APIs to compile compute shaders
    /// </summary>
    internal static class ShaderCompiler
    {
        /// <summary>
        /// The <see cref="IDxcLibrary"/> instance to use to create the bytecode for HLSL sources
        /// </summary>
        private static readonly IDxcLibrary Library = Dxc.CreateDxcLibrary();

        /// <summary>
        /// Compiles a new HLSL shader from the input source code
        /// </summary>
        /// <param name="source">The HLSL source code to compile</param>
        /// <returns>The bytecode for the compiled shader</returns>
        [Pure]
        public static ShaderBytecode CompileShader(string source)
        {
            var result = DxcCompiler.Compile(DxcShaderStage.ComputeShader, source, "CSMain", string.Empty, new DxcCompilerOptions { ShaderModel = DxcShaderModel.Model6_1 });

            // Get the compiled bytecode in case of success
            if (result.GetStatus() == 0)
            {
                IDxcBlob blob = result.GetResult();
                byte[] bytecode = Dxc.GetBytesFromBlob(blob);

                return bytecode;
            }

            // Compile error
            string resultText = Dxc.GetStringFromBlob(Library, result.GetErrors());
            throw new HlslCompilationException(resultText);
        }
    }
}
