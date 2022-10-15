namespace ComputeSharp.Graphics.Resources.Enums;

/// <summary>
/// An <see langword="enum"/> that indicates the type of a given HLSL resource.
/// </summary>
internal enum ResourceType
{
    /// <summary>
    /// A constant resource, with items aligned to 16 bytes, used for individual values or very small arrays.
    /// This is only supported for buffers and is mapped to a <see langword="cbuffer"/>.
    /// Resources of this type are located in <see cref="TerraFX.Interop.DirectX.D3D12_HEAP_TYPE.D3D12_HEAP_TYPE_UPLOAD"/>.
    /// </summary>
    Constant,

    /// <summary>
    /// A readonly resource, that can store arbitrary arrays of values and can only be read from by the GPU.
    /// This is mapped to either <see langword="StructuredBuffer&lt;T&gt;"/> or <see langword="TextureND&lt;T&gt;"/>.
    /// Resources of this type are located in <see cref="TerraFX.Interop.DirectX.D3D12_HEAP_TYPE.D3D12_HEAP_TYPE_DEFAULT"/>.
    /// </summary>
    ReadOnly,

    /// <summary>
    /// A read write resource, that can store arbitrary arrays with both read and write access for the GPU.
    /// This is mapped to either <see langword="RWStructuredBuffer&lt;T&gt;"/> or <see langword="RWTextureND&lt;T&gt;"/>.
    /// Resources of this type are located in <see cref="TerraFX.Interop.DirectX.D3D12_HEAP_TYPE.D3D12_HEAP_TYPE_DEFAULT"/>.
    /// </summary>
    ReadWrite,

    /// <summary>
    /// A readback resource, used temporarily to read data back from <see cref="ReadOnly"/> and <see cref="ReadWrite"/> resources.
    /// This has no direct mapping to a type in HLSL, as it's only used for copy operations.
    /// Resources of this type are located in <see cref="TerraFX.Interop.DirectX.D3D12_HEAP_TYPE.D3D12_HEAP_TYPE_READBACK"/>.
    /// </summary>
    ReadBack,

    /// <summary>
    /// A transfer resource, used temporarily to set data to <see cref="ReadOnly"/> and <see cref="ReadWrite"/> resources.
    /// This has no direct mapping to a type in HLSL, as it's only used for copy operations (like <see cref="ReadBack"/>).
    /// Resources of this type are located in <see cref="TerraFX.Interop.DirectX.D3D12_HEAP_TYPE.D3D12_HEAP_TYPE_UPLOAD"/>.
    /// </summary>
    Upload
}