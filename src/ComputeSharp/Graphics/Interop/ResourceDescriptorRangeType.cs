using static ComputeSharp.Win32.D3D12_DESCRIPTOR_RANGE_TYPE;

namespace ComputeSharp.Interop;

/// <summary>
/// A type containing info on a descriptor type for a given D3D12 shader resource range.
/// </summary>
/// <remarks>
/// This type exposes the available values in
/// <see href="https://learn.microsoft.com/windows/win32/api/d3d12/ne-d3d12-d3d12_descriptor_range_type"><c>D3D12_DESCRIPTOR_RANGE_TYPE</c></see>.
/// </remarks>
public enum ResourceDescriptorRangeType
{
    /// <summary>
    /// Specifies a range of SRVs.
    /// </summary>
    ShaderResourceView = D3D12_DESCRIPTOR_RANGE_TYPE_SRV,

    /// <summary>
    /// Specifies a range of unordered-access views (UAVs).
    /// </summary>
    UnorderedAccessView = D3D12_DESCRIPTOR_RANGE_TYPE_UAV,

    /// <summary>
    /// Specifies a range of constant-buffer views (CBVs).
    /// </summary>
    ConstantBufferView = D3D12_DESCRIPTOR_RANGE_TYPE_CBV
}