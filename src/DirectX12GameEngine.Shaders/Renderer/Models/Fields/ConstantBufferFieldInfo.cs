using DirectX12GameEngine.Shaders.Renderer.Models.Abstract;

namespace DirectX12GameEngine.Shaders.Renderer.Models.Fields
{
    /// <summary>
    /// A <see langword="class"/> that contains info on a constant buffer field
    /// </summary>
    public sealed class ConstantBufferFieldInfo : FieldInfo
    {
        /// <summary>
        /// Gets whether or not the current <see cref="FieldInfo"/> instance represents a constant buffer (always <see langword="true"/>)
        /// </summary>
        public bool IsConstantBuffer { get; } = true;

        /// <summary>
        /// Gets the index of the current constant buffer field
        /// </summary>
        public int ConstantBufferIndex { get; set; }
    }
}
