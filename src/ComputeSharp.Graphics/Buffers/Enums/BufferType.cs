namespace ComputeSharp.Graphics.Buffers.Enums
{
    /// <summary>
    /// An <see langword="enum"/> that indicates the type of a given HLSL buffer
    /// </summary>
    internal enum BufferType
    {
        /// <summary>
        /// A constant buffer, with items aligned to 16 bytes, used for individual values or very small arrays, mapped to <see cref="Vortice.DirectX.Direct3D12.HeapType.Upload"/>
        /// </summary>
        Constant,

        /// <summary>
        /// A readonly buffer, that can store arbitrary arrays of values, mapped to <see cref="Vortice.DirectX.Direct3D12.HeapType.Default"/>
        /// </summary>
        ReadOnly,

        /// <summary>
        /// A read write buffer, that can store arbitrary arrays with both read and write access, mapped to <see cref="Vortice.DirectX.Direct3D12.HeapType.Default"/>
        /// </summary>
        ReadWrite,

        /// <summary>
        /// A readback buffer, used as temporary buffer to read data back from buffers in the default heap, mapped to <see cref="Vortice.DirectX.Direct3D12.HeapType.Readback"/>
        /// </summary>
        ReadBack,

        /// <summary>
        /// A transfer buffer, used as a temporary buffer to set data to buffers in the default heap, mapped to <see cref="Vortice.DirectX.Direct3D12.HeapType.Upload"/>
        /// </summary>
        Transfer
    }
}
