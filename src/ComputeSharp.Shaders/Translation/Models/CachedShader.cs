using Vortice.Direct3D12;

namespace ComputeSharp.Shaders.Translation.Models
{
    /// <summary>
    /// A <see langword="struct"/> that contains info on a cached shader
    /// </summary>
    internal readonly struct CachedShader
    {
        /// <summary>
        /// The <see cref="ShaderLoader"/> instance with the shader metadata
        /// </summary>
        public readonly ShaderLoader Loader;

        /// <summary>
        /// The compiled shader bytecode
        /// </summary>
        public readonly ShaderBytecode Bytecode;

        /// <summary>
        /// Creates a new <see cref="CachedShader"/> instance with the specified parameters
        /// </summary>
        /// <param name="loader">The <see cref="ShaderLoader"/> instance with the shader metadata</param>
        /// <param name="bytecode">The compiled shader bytecode</param>
        public CachedShader(ShaderLoader loader, ShaderBytecode bytecode)
        {
            Loader = loader;
            Bytecode = bytecode;
        }
    }
}
