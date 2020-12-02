namespace ComputeSharp.Graphics.Buffers.Enums
{
    /// <summary>
    /// An <see langword="enum"/> that indicates the type of a given HLSL texture.
    /// </summary>
    internal enum TextureType
    {
        /// <summary>
        /// A readonly texture, that can store arbitrary arrays of supported values and can only be read from by the GPU.
        /// This is mapped to a <see langword="TextureND&lt;T&gt;"/> in <see cref="TerraFX.Interop.D3D12_HEAP_TYPE.D3D12_HEAP_TYPE_DEFAULT"/>.
        /// </summary>
        ReadOnly,

        /// <summary>
        /// A read write buffer, that can store arbitrary arrays with both read and write access for the GPU.
        /// This is mapped to a <see langword="RWSTextureND&lt;T&gt;"/> in <see cref="TerraFX.Interop.D3D12_HEAP_TYPE.D3D12_HEAP_TYPE_DEFAULT"/>.
        /// </summary>
        ReadWrite
    }
}
