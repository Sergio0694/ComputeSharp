using System.Diagnostics;
using ComputeSharp.Exceptions;
using ComputeSharp.Graphics.Resources.Enums;
using ComputeSharp.Resources;
using ComputeSharp.Resources.Debug;
using static TerraFX.Interop.DirectX.D3D12_FORMAT_SUPPORT1;

namespace ComputeSharp;

/// <summary>
/// A <see langword="class"/> representing a typed readonly 3D texture stored on GPU memory.
/// </summary>
/// <typeparam name="T">The type of items stored on the texture.</typeparam>
[DebuggerTypeProxy(typeof(Texture3DDebugView<>))]
[DebuggerDisplay("{ToString(),raw}")]
public sealed partial class ReadWriteTexture3D<T> : Texture3D<T>
    where T : unmanaged
{
    /// <summary>
    /// Creates a new <see cref="ReadOnlyTexture2D{T}"/> instance with the specified parameters.
    /// </summary>
    /// <param name="device">The <see cref="GraphicsDevice"/> associated with the current instance.</param>
    /// <param name="width">The width of the texture.</param>
    /// <param name="height">The height of the texture.</param>
    /// <param name="depth">The depth of the texture.</param>
    /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
    internal ReadWriteTexture3D(GraphicsDevice device, int width, int height, int depth, AllocationMode allocationMode)
        : base(device, width, height, depth, ResourceType.ReadWrite, allocationMode, D3D12_FORMAT_SUPPORT1_TEXTURE3D | D3D12_FORMAT_SUPPORT1_TYPED_UNORDERED_ACCESS_VIEW)
    {
    }

    /// <summary>
    /// Gets a single <typeparamref name="T"/> value from the current writeable texture.
    /// </summary>
    /// <param name="x">The horizontal offset of the value to get.</param>
    /// <param name="y">The vertical offset of the value to get.</param>
    /// <param name="z">The depthwise offset of the value to get.</param>
    /// <remarks>This API can only be used from a compute shader, and will always throw if used anywhere else.</remarks>
    public ref T this[int x, int y, int z] => throw new InvalidExecutionContextException($"{typeof(ReadWriteTexture3D<T>)}[{typeof(int)}, {typeof(int)}, {typeof(int)}]");

    /// <summary>
    /// Gets a single <typeparamref name="T"/> value from the current writeable texture.
    /// </summary>
    /// <param name="xyz">The coordinates of the value to get.</param>
    /// <remarks>This API can only be used from a compute shader, and will always throw if used anywhere else.</remarks>
    public ref T this[Int3 xyz] => throw new InvalidExecutionContextException($"{typeof(ReadWriteTexture3D<T>)}[{typeof(Int3)}]");

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"ComputeSharp.ReadWriteTexture3D<{typeof(T)}>[{Width}, {Height}, {Depth}]";
    }
}