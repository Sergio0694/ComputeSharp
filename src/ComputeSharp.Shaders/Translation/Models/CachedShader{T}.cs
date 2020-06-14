using Vortice.Direct3D12;

namespace ComputeSharp.Shaders.Translation.Models
{
    /// <summary>
    /// A <see langword="struct"/> that contains info on a cached shader
    /// </summary>
    /// <typeparam name="T">The type of compute shader in use</typeparam>
    internal readonly struct CachedShader<T>
        where T : struct, IComputeShader
    {
        /// <summary>
        /// The <see cref="ShaderLoader"/> instance with the shader metadata
        /// </summary>
        public readonly ShaderLoader<T> Loader;

        /// <summary>
        /// The compiled shader bytecode
        /// </summary>
        public readonly ShaderBytecode Bytecode;

        /// <summary>
        /// Creates a new <see cref="CachedShader"/> instance with the specified parameters
        /// </summary>
        /// <param name="loader">The <see cref="ShaderLoader"/> instance with the shader metadata</param>
        /// <param name="bytecode">The compiled shader bytecode</param>
        public CachedShader(ShaderLoader<T> loader, ShaderBytecode bytecode)
        {
            Loader = loader;
            Bytecode = bytecode;
        }
    }
}
