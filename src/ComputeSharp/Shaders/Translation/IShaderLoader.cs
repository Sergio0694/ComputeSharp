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
        string EntryPoint { get; }

        /// <summary>
        /// Gets the collection of <see cref="Renderer.Models.HlslResourceInfo"/> items for the shader fields.
        /// </summary>
        IReadOnlyList<HlslResourceInfo> HlslResourceInfo { get; }

        /// <summary>
        /// Gets the collection of <see cref="CapturedFieldInfo"/> items for the shader fields.
        /// </summary>
        IReadOnlyList<CapturedFieldInfo> FieldsInfo { get; }

        /// <summary>
        /// Gets the collection of methods to be used in the shader.
        /// </summary>
        IReadOnlyCollection<string> MethodsInfo { get; }

        /// <summary>
        /// Gets the collection of constants to be declared in the shader.
        /// </summary>
        IReadOnlyDictionary<string, string> ConstantsInfo { get; }

        /// <summary>
        /// Gets the collection of declared types for the shader.
        /// </summary>
        IReadOnlyCollection<string> DeclaredTypes { get; }

        /// <summary>
        /// Gets the collection of discovered constants.
        /// </summary>
        IReadOnlyDictionary<string, (string Type, int? Count)> SharedBuffers { get; }
    }
}
