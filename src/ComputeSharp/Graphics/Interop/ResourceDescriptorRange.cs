using System.Runtime.InteropServices;

namespace ComputeSharp.Interop;

/// <summary>
/// A type containing info on a descriptor for a given D3D12 shader resource range.
/// </summary>
/// <remarks>
/// This type exposes a subset of values from
/// <see href="https://learn.microsoft.com/windows/win32/api/d3d12/ns-d3d12-d3d12_descriptor_range1"><c>D3D12_DESCRIPTOR_RANGE1</c></see>.
/// </remarks>
[StructLayout(LayoutKind.Sequential)]
public readonly struct ResourceDescriptorRange
{
    /// <summary>
    /// Creates a new <see cref="ResourceDescriptorRange"/> instance with the specified parameters.
    /// </summary>
    /// <param name="type">The type of the descriptor range.</param>
    /// <param name="register">The shader register in the range.</param>
    public ResourceDescriptorRange(ResourceDescriptorRangeType type, uint register)
    {
        Type = type;
        Register = register;
    }

    /// <summary>
    /// Gets the type of the descriptor range.
    /// </summary>
    public ResourceDescriptorRangeType Type { get; }

    /// <summary>
    /// Gets the shader register in the range.
    /// </summary>
    /// <remarks>
    /// For example, for shader-resource views (SRVs), 3 maps to <c>register(t3)</c> in HLSL.
    /// </remarks>
    public uint Register { get; }
}