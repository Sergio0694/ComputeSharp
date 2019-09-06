using ComputeSharp.Graphics.Extensions;
using System;

namespace ComputeSharp.Shaders.Renderer.Models.Fields
{
    /// <summary>
    /// A <see langword="class"/> that contains info on a shader field
    /// </summary>
    internal class CapturedFieldInfo
    {
        /// <summary>
        /// Gets the type of the current field in the C# source
        /// </summary>
        public string FieldCsharpType { get; }

        /// <summary>
        /// Gets the type of the current field in the HLSL shader
        /// </summary>
        public string FieldHlslType { get; }

        /// <summary>
        /// Gets the name to use for the current field
        /// </summary>
        public string FieldName { get; }

        /// <summary>
        /// Creates a new <see cref="CapturedFieldInfo"/> instance with the specified parameters
        /// </summary>
        /// <param name="fieldCsharpType">The type of the current field in the C# source</param>
        /// <param name="fieldHlslType">The type of the current field in the HLSL shader</param>
        /// <param name="fieldName">The name to use for the current field</param>
        public CapturedFieldInfo(Type fieldCsharpType, string fieldHlslType, string fieldName)
        {
            FieldCsharpType = fieldCsharpType.ToFriendlyString();
            FieldHlslType = fieldHlslType;
            FieldName = fieldName;
        }
    }
}
