using System.Diagnostics;
using ComputeSharp.Graphics.Resources.Enums;
using ComputeSharp.Resources;
using ComputeSharp.Resources.Debug;
using static TerraFX.Interop.DirectX.D3D12_FORMAT_SUPPORT1;

namespace ComputeSharp;

/// <summary>
/// A <see langword="class"/> representing a typed read write 1D texture stored on GPU memory.
/// </summary>
/// <typeparam name="T">The type of items stored on the texture.</typeparam>
[DebuggerTypeProxy(typeof(Texture1DDebugView<>))]
[DebuggerDisplay("{ToString(),raw}")]
public sealed partial class ReadWriteTexture1D<T> : Texture1D<T>
    where T : unmanaged
{
    /// <summary>
    /// Creates a new <see cref="ReadWriteTexture1D{T}"/> instance with the specified parameters.
    /// </summary>
    /// <param name="device">The <see cref="GraphicsDevice"/> associated with the current instance.</param>
    /// <param name="width">The width of the texture.</param>
    /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
    internal ReadWriteTexture1D(GraphicsDevice device, int width, AllocationMode allocationMode)
        : base(device, width, ResourceType.ReadWrite, allocationMode, D3D12_FORMAT_SUPPORT1_TEXTURE1D | D3D12_FORMAT_SUPPORT1_TYPED_UNORDERED_ACCESS_VIEW)
    {
    }

    /// <summary>
    /// Gets a single <typeparamref name="T"/> value from the current writeable texture.
    /// </summary>
    /// <param name="x">The horizontal offset of the value to get.</param>
    /// <remarks>This API can only be used from a compute shader, and will always throw if used anywhere else.</remarks>
    public ref T this[int x] => throw new InvalidExecutionContextException($"{typeof(ReadWriteTexture1D<T>)}[{typeof(int)}]");

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"ComputeSharp.ReadWriteTexture1D<{typeof(T)}>[{Width}]";
    }
}