namespace ComputeSharp.Shaders.Renderer.Models
{
    /// <summary>
    /// A model that contains info on a shader resource field.
    /// </summary>
    /// <param name="FieldType">The type of the current field in the HLSL shader.</param>
    /// <param name="FieldName">The name to use for the current field.</param>
    /// <param name="RegisterIndex">The register index to bind the resource to.</param>
    internal abstract record HlslResourceInfo(string FieldType, string FieldName, int RegisterIndex)
        : CapturedFieldInfo(FieldType, FieldName)
    {
        /// <summary>
        /// A model that represents a captured constant buffer in a shader.
        /// </summary>
        /// <inheritdoc/>
        public sealed record Constant(string FieldType, string FieldName, int RegisterIndex)
            : HlslResourceInfo(FieldType, FieldName, RegisterIndex);

        /// <summary>
        /// A model that represents a captured readonly typed resource in a shader.
        /// Maps to either <see cref="ComputeSharp.ReadOnlyBuffer{T}"/> or <see cref="ComputeSharp.ReadOnlyTexture2D{T}"/>.
        /// </summary>
        /// <inheritdoc/>
        public sealed record ReadOnly(string FieldType, string FieldName, int RegisterIndex)
            : HlslResourceInfo(FieldType, FieldName, RegisterIndex);

        /// <summary>
        /// A model that represents a captured writeable buffer in a shader.
        /// Maps to either <see cref="ComputeSharp.ReadWriteBuffer{T}"/> or <see cref="ComputeSharp.ReadWriteTexture2D{T}"/>.
        /// </summary>
        /// <inheritdoc/>
        public sealed record ReadWrite(string FieldType, string FieldName, int RegisterIndex)
            : HlslResourceInfo(FieldType, FieldName, RegisterIndex);

        /// <summary>
        /// A model that represents a captured sampler in a shader.
        /// This resource type doesn't have a mapping to a public API.
        /// </summary>
        /// <inheritdoc/>
        public sealed record Sampler(string FieldType, string FieldName, int RegisterIndex)
            : HlslResourceInfo(FieldType, FieldName, RegisterIndex);
    }
}
