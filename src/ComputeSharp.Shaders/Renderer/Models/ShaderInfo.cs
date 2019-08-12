using System.Collections.Generic;
using ComputeSharp.Shaders.Renderer.Models.Fields.Abstract;

#pragma warning disable CS8618 // Non-nullable field is uninitialized

namespace ComputeSharp.Shaders.Renderer.Models
{
    /// <summary>
    /// A <see langword="class"/> that contains info to render a shader source to compile
    /// </summary>
    public sealed class ShaderInfo
    {
        /// <summary>
        /// Gets the list of fields being present in the current shader
        /// </summary>
        public IReadOnlyList<FieldInfoBase> FieldsList { get; set; }

        /// <summary>
        /// Gets or sets the number of threads in the X group
        /// </summary>
        public int NumThreadsX { get; set; }

        /// <summary>
        /// Gets or sets the number of threads in the Y group
        /// </summary>
        public int NumThreadsY { get; set; }

        /// <summary>
        /// Gets or sets the number of threads in the Z group
        /// </summary>
        public int NumThreadsZ { get; set; }

        /// <summary>
        /// Gets or sets the name for the threads id variable used in the shader
        /// </summary>
        public string ThreadsIdsVariableName { get; set; }

        /// <summary>
        /// Gets or sets the shader body to compile
        /// </summary>
        public string ShaderBody { get; set; }
    }
}
