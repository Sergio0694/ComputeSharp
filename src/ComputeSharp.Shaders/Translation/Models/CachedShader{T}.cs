using System.Runtime.CompilerServices;
using ComputeSharp.Graphics.Commands;
using ComputeSharp.Shaders.Translation.Interop;

namespace ComputeSharp.Shaders.Translation.Models
{
    /// <summary>
    /// A <see langword="struct"/> that contains info on a cached shader.
    /// </summary>
    /// <typeparam name="T">The type of compute shader in use.</typeparam>
    internal readonly struct CachedShader<T>
        where T : struct, IComputeShader
    {
        /// <summary>
        /// The <see cref="ShaderLoader{T}"/> instance with the shader metadata.
        /// </summary>
        public readonly ShaderLoader<T> Loader;

        /// <summary>
        /// The compiled shader bytecode.
        /// </summary>
        public readonly IDxcBlobObject Bytecode;

        /// <summary>
        /// The map of cached <see cref="PipelineData"/> instances for each GPU in use.
        /// </summary>
        public readonly ConditionalWeakTable<GraphicsDevice, PipelineData> CachedPipelines;

        /// <summary>
        /// Creates a new <see cref="CachedShader{T}"/> instance with the specified parameters.
        /// </summary>
        /// <param name="loader">The <see cref="ShaderLoader{T}"/> instance with the shader metadata.</param>
        /// <param name="bytecode">The compiled shader bytecode.</param>
        public CachedShader(ShaderLoader<T> loader, IDxcBlobObject bytecode)
        {
            Loader = loader;
            Bytecode = bytecode;
            CachedPipelines = new();
        }
    }
}
