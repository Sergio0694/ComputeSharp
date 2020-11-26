namespace ComputeSharp.Shaders.Renderer.Models
{
    /// <summary>
    /// A model that contains info on a shader buffer field.
    /// </summary>
    /// <param name="FieldType">The type of the current field in the HLSL shader.</param>
    /// <param name="FieldName">The name to use for the current field.</param>
    internal abstract record HlslBufferInfo(string FieldType, string FieldName, int BufferIndex)
        : CapturedFieldInfo(FieldType, FieldName)
    {
        /// <inheritdoc/>
        /// <summary>
        /// A model that represents a captured constant buffer in a shader.
        /// </summary>
        public sealed record Constant(string FieldType, string FieldName, int BufferIndex)
            : HlslBufferInfo(FieldType, FieldName, BufferIndex);

        /// <inheritdoc/>
        /// <summary>
        /// A model that represents a captured readonly buffer in a shader.
        /// </summary>
        public sealed record ReadOnly(string FieldType, string FieldName, int BufferIndex)
            : HlslBufferInfo(FieldType, FieldName, BufferIndex);

        /// <inheritdoc/>
        /// <summary>
        /// A model that represents a captured writeable buffer in a shader.
        /// </summary>
        public sealed record ReadWrite(string FieldType, string FieldName, int BufferIndex)
            : HlslBufferInfo(FieldType, FieldName, BufferIndex);
    }
}
