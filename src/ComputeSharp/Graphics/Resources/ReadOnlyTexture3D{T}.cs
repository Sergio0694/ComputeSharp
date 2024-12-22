using System.Diagnostics;
using ComputeSharp.Graphics.Resources.Enums;
using ComputeSharp.Resources;
using ComputeSharp.Resources.Debug;
using static ComputeSharp.Win32.D3D12_FORMAT_SUPPORT1;

#pragma warning disable IDE0022

namespace ComputeSharp;

/// <summary>
/// A <see langword="class"/> representing a typed readonly 3D texture stored on GPU memory.
/// </summary>
/// <typeparam name="T">The type of items stored on the texture.</typeparam>
[DebuggerTypeProxy(typeof(Texture3DDebugView<>))]
[DebuggerDisplay("{ToString(),raw}")]
public sealed class ReadOnlyTexture3D<T> : Texture3D<T>, IReadOnlyTexture3D<T>
    where T : unmanaged
{
    /// <summary>
    /// Creates a new <see cref="ReadOnlyTexture3D{T}"/> instance with the specified parameters.
    /// </summary>
    /// <param name="device">The <see cref="GraphicsDevice"/> associated with the current instance.</param>
    /// <param name="width">The width of the texture.</param>
    /// <param name="height">The height of the texture.</param>
    /// <param name="depth">The depth of the texture.</param>
    /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
    internal ReadOnlyTexture3D(GraphicsDevice device, int width, int height, int depth, AllocationMode allocationMode)
        : base(device, width, height, depth, ResourceType.ReadOnly, allocationMode, D3D12_FORMAT_SUPPORT1_TEXTURE3D)
    {
    }

    /// <inheritdoc/>
    public ref readonly T this[int x, int y, int z] => throw new InvalidExecutionContextException($"{typeof(ReadOnlyTexture3D<T>)}[{typeof(int)}, {typeof(int)}, {typeof(int)}]");

    /// <inheritdoc/>
    public ref readonly T this[Int3 xyz] => throw new InvalidExecutionContextException($"{typeof(ReadOnlyTexture3D<T>)}[{typeof(Int3)}]");

    /// <inheritdoc/>
    public ref readonly T Sample(float u, float v, float w) => throw new InvalidExecutionContextException($"{typeof(ReadOnlyTexture3D<T>)}.{nameof(Sample)}({typeof(float)}, {typeof(float)}, {typeof(float)})");

    /// <inheritdoc/>
    public ref readonly T Sample(Float3 uvw) => throw new InvalidExecutionContextException($"{typeof(ReadOnlyTexture3D<T>)}.{nameof(Sample)}({typeof(Float3)})");

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"ComputeSharp.ReadOnlyTexture3D<{typeof(T)}>[{Width}, {Height}, {Depth}]";
    }
}