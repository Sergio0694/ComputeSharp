namespace ComputeSharp.Shaders.Renderer.Models.Fields
{
    /// <summary>
    /// A <see langword="class"/> that contains info on a shader field
    /// </summary>
    internal class CapturedFieldInfo
    {
        /// <summary>
        /// Gets or sets the type of the current field
        /// </summary>
        public string FieldType { get; set; }

        /// <summary>
        /// Gets or sets the name to use for the current field
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// Creates a new <see cref="CapturedFieldInfo"/> instance with the specified parameters
        /// </summary>
        /// <param name="fieldType">The type of the current field</param>
        /// <param name="fieldName">The name of the current field</param>
        public CapturedFieldInfo(string fieldType, string fieldName)
        {
            FieldType = fieldType;
            FieldName = fieldName;
        }
    }
}
