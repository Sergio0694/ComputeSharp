using System.Collections.Generic;
using DirectX12GameEngine.Shaders.Renderer.Models.Abstract;

#pragma warning disable CS8618 // Non-nullable field is uninitialized

namespace DirectX12GameEngine.Shaders.Renderer.Models
{
    /// <summary>
    /// A <see langword="class"/> that contains info to render a shader source to compile
    /// </summary>
    public sealed class ShaderInfo
    {
        /// <summary>
        /// Gets the list of fields being present in the current shader
        /// </summary>
        public List<FieldInfo> FieldsList { get; } = new List<FieldInfo>();

        /// <summary>
        /// Gets or sets the number of threads in the X group
        /// </summary>
        public int ThreadsX { get; set; }

        /// <summary>
        /// Gets or sets the number of threads in the Y group
        /// </summary>
        public int ThreadsY { get; set; }

        /// <summary>
        /// Gets or sets the number of threads in the Z group
        /// </summary>
        public int ThreadsZ { get; set; }

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
