namespace ComputeSharp.Graphics.Buffers.Enums
{
    /// <summary>
    /// An <see langword="enum"/> that indicates the type of a given HLSL buffer.
    /// </summary>
    internal enum BufferType
    {
        /// <summary>
        /// A constant buffer, with items aligned to 16 bytes, used for individual values or very small arrays.
        /// This is mapped to a <see langword="cbuffer"/> in <see cref="TerraFX.Interop.D3D12_HEAP_TYPE.D3D12_HEAP_TYPE_UPLOAD"/>.
        /// </summary>
        Constant,

        /// <summary>
        /// A readonly buffer, that can store arbitrary arrays of values and can only be read from by the GPU.
        /// This is mapped to a <see langword="StructuredBuffer&lt;T&gt;"/> in <see cref="TerraFX.Interop.D3D12_HEAP_TYPE.D3D12_HEAP_TYPE_DEFAULT"/>.
        /// </summary>
        ReadOnly,

        /// <summary>
        /// A read write buffer, that can store arbitrary arrays with both read and write access for the GPU.
        /// This is mapped to a <see langword="RWStructuredBuffer&lt;T&gt;"/> in <see cref="TerraFX.Interop.D3D12_HEAP_TYPE.D3D12_HEAP_TYPE_DEFAULT"/>.
        /// </summary>
        ReadWrite,

        /// <summary>
        /// A readback buffer, used as temporary buffer to read data back from <see cref="ReadOnly"/> and <see cref="ReadWrite"/> buffers.
        /// This has no mapping in HLSL, and is allocated in <see cref="TerraFX.Interop.D3D12_HEAP_TYPE.D3D12_HEAP_TYPE_READBACK"/>.
        /// </summary>
        ReadBack,

        /// <summary>
        /// A transfer buffer, used as a temporary buffer to set data to <see cref="ReadOnly"/> and <see cref="ReadWrite"/> buffers.
        /// This has no mapping in HLSL, and is allocated in <see cref="TerraFX.Interop.D3D12_HEAP_TYPE.D3D12_HEAP_TYPE_UPLOAD"/>.
        /// </summary>
        Upload
    }
}
