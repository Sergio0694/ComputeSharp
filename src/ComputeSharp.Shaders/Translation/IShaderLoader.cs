using System.Collections.Generic;
using ComputeSharp.Shaders.Renderer.Models;

namespace ComputeSharp.Shaders.Translation
{
    /// <summary>
    /// A <see langword="class"/> responsible for loading and processing shaders of any type.
    /// </summary>
    internal interface IShaderLoader
    {
        /// <summary>
        /// Gets the generated source code for the method in the current shader.
        /// </summary>
        public string EntryPoint { get; }

        /// <summary>
        /// Gets the collection of <see cref="HlslBufferInfo"/> items for the shader fields.
        /// </summary>
        public IReadOnlyList<HlslBufferInfo> HslsBuffersInfo { get; }

        /// <summary>
        /// Gets the collection of <see cref="CapturedFieldInfo"/> items for the shader fields.
        /// </summary>
        public IReadOnlyList<CapturedFieldInfo> FieldsInfo { get; }

        /// <summary>
        /// Gets the collection of methods to be used in the shader.
        /// </summary>
        public IReadOnlyCollection<string> MethodsInfo { get; }

        /// <summary>
        /// Gets the collection of declared types for the shader.
        /// </summary>
        public IReadOnlyCollection<string> DeclaredTypes { get; }
    }
}
