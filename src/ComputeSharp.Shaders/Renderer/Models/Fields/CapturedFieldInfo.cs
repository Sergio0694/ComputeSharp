namespace ComputeSharp.Shaders.Renderer.Models.Fields
{
    /// <summary>
    /// A <see langword="class"/> that contains info on a shader field.
    /// </summary>
    internal class CapturedFieldInfo
    {
        /// <summary>
        /// Creates a new <see cref="CapturedFieldInfo"/> instance with the specified parameters.
        /// </summary>
        /// <param name="fieldType">The type of the current field in the HLSL shader.</param>
        /// <param name="fieldName">The name to use for the current field.</param>
        public CapturedFieldInfo(string fieldType, string fieldName)
        {
            FieldType = fieldType;
            FieldName = fieldName;
        }

        /// <summary>
        /// Gets the type of the current field in the HLSL shader.
        /// </summary>
        public string FieldType { get; }

        /// <summary>
        /// Gets the name to use for the current field.
        /// </summary>
        public string FieldName { get; }
    }
}
