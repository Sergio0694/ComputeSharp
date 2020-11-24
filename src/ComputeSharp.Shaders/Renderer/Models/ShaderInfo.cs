using System.Collections.Generic;
using ComputeSharp.Shaders.Renderer.Models.Fields;
using ComputeSharp.Shaders.Renderer.Models.Fields.Abstract;
using ComputeSharp.Shaders.Renderer.Models.Functions;

#pragma warning disable CS8618

namespace ComputeSharp.Shaders.Renderer.Models
{
    /// <summary>
    /// A <see langword="class"/> that contains info to render a shader source to compile.
    /// </summary>
    internal sealed class ShaderInfo
    {
        /// <summary>
        /// Gets or sets the list of captured buffers being present in the current shader.
        /// </summary>
        public IReadOnlyList<HlslBufferInfo> BuffersList { get; init; }

        /// <summary>
        /// Gets or sets the list of captured variables being present in the current shader.
        /// </summary>
        public IReadOnlyList<CapturedFieldInfo> FieldsList { get; init; }

        /// <summary>
        /// Gets or sets the number of threads in the X group.
        /// </summary>
        public int NumThreadsX { get; init; }

        /// <summary>
        /// Gets or sets the number of threads in the Y group.
        /// </summary>
        public int NumThreadsY { get; init; }

        /// <summary>
        /// Gets or sets the number of threads in the Z group.
        /// </summary>
        public int NumThreadsZ { get; init; }

        /// <summary>
        /// Gets or sets the name for the threads id variable used in the shader.
        /// </summary>
        public string ThreadsIdsVariableName { get; init; }

        /// <summary>
        /// Gets or sets the entry point of the shader.
        /// </summary>
        public string EntryPoint { get; init; }

        /// <summary>
        /// Gets or sets the list of static functions used by the shader.
        /// </summary>
        public IReadOnlyList<FunctionInfo> FunctionsList { get; init; }

        /// <summary>
        /// Gets or sets the list of local (implicit) functions used by the shader.
        /// </summary>
        public IReadOnlyList<LocalFunctionInfo> LocalFunctionsList { get; init; }
    }
}
