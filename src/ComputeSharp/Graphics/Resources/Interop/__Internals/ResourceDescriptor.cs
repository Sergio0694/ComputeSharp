using System;
using System.ComponentModel;
using TerraFX.Interop.DirectX;
using static TerraFX.Interop.DirectX.D3D12_DESCRIPTOR_RANGE_FLAGS;

#pragma warning disable IDE0052, IDE1006, CS0414

namespace ComputeSharp.__Internals;

/// <summary>
/// An opaque type describing a captured resource to be used in a shader.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
[Obsolete("This type is not intended to be used directly by user code")]
public struct ResourceDescriptor
{
    /// <inheritdoc cref="D3D12_DESCRIPTOR_RANGE1.RangeType"/>
    private D3D12_DESCRIPTOR_RANGE_TYPE RangeType;

    /// <inheritdoc cref="D3D12_DESCRIPTOR_RANGE1.NumDescriptors"/>
    private uint NumDescriptors;

    /// <inheritdoc cref="D3D12_DESCRIPTOR_RANGE1.BaseShaderRegister"/>
    private uint BaseShaderRegister;

    /// <inheritdoc cref="D3D12_DESCRIPTOR_RANGE1.RegisterSpace"/>
    private uint RegisterSpace;

    /// <inheritdoc cref="D3D12_DESCRIPTOR_RANGE1.Flags"/>
    private D3D12_DESCRIPTOR_RANGE_FLAGS Flags;

    /// <inheritdoc cref="D3D12_DESCRIPTOR_RANGE1.OffsetInDescriptorsFromTableStart"/>
    private uint OffsetInDescriptorsFromTableStart;

    /// <summary>
    /// Initializes a new <see cref="ResourceDescriptor"/> instance.
    /// </summary>
    /// <param name="type">The type of resource to create the descriptor for.</param>
    /// <param name="offset">The offset in the resource from the start of the shader mapping.</param>
    /// <param name="descriptor">The resulting <see cref="ResourceDescriptor"/> instance.</param>
    public static void Create(int type, uint offset, out ResourceDescriptor descriptor)
    {
        descriptor.RangeType = (D3D12_DESCRIPTOR_RANGE_TYPE)type;
        descriptor.NumDescriptors = 1;
        descriptor.BaseShaderRegister = offset;
        descriptor.RegisterSpace = 0;
        descriptor.Flags = D3D12_DESCRIPTOR_RANGE_FLAG_NONE;
        descriptor.OffsetInDescriptorsFromTableStart = D3D12.D3D12_DESCRIPTOR_RANGE_OFFSET_APPEND;
    }
}