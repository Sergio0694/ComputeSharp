using DirectX12GameEngine.Shaders.Renderer.Models.Abstract;

namespace DirectX12GameEngine.Shaders.Renderer.Models.Fields
{
    /// <summary>
    /// A <see langword="class"/> that contains info on a read write buffer field
    /// </summary>
    public sealed class ReadWriteBufferFieldInfo : FieldInfo
    {
        /// <summary>
        /// Gets whether or not the current <see cref="FieldInfo"/> instance represents a read write buffer (always <see langword="true"/>)
        /// </summary>
        public bool IsConstantBuffer { get; } = true;

        /// <summary>
        /// Gets the index of the current read write buffer field
        /// </summary>
        public int ReadWriteBufferIndex { get; set; }
    }
}
