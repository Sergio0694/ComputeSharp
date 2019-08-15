using ComputeSharp.Shaders.Renderer.Models.Fields.Abstract;

namespace ComputeSharp.Shaders.Renderer.Models.Fields
{
    /// <summary>
    /// A <see langword="class"/> that contains info on a constant buffer field
    /// </summary>
    internal sealed class ConstantBufferFieldInfo : HlslBufferInfo
    {
        /// <summary>
        /// Gets whether or not the current <see cref="FieldInfo"/> instance represents a constant buffer (always <see langword="true"/>)
        /// </summary>
        public bool IsConstantBuffer { get; } = true;

        /// <summary>
        /// Gets whether or not this constant buffer actually contains a single, explicit variable
        /// </summary>
        public bool IsSingleValue { get; }

        /// <summary>
        /// Creates a new <see cref="ConstantBufferFieldInfo"/> instance with the specified parameters
        /// </summary>
        /// <param name="fieldType">The type of the current field</param>
        /// <param name="fieldName">The name of the current field</param>
        /// <param name="bufferIndex">The index of the current constant buffer field</param>
        /// <param name="singleValue">Whether or not this constant buffer actually contains a single, explicit variable</param>
        public ConstantBufferFieldInfo(string fieldType, string fieldName, int bufferIndex, bool singleValue) : base(fieldType, fieldName, bufferIndex)
        {
            IsSingleValue = singleValue;
        }
    }
}
