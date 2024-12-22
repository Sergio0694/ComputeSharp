using System.Diagnostics;
using ComputeSharp.Graphics.Resources.Enums;
using ComputeSharp.Resources;
using ComputeSharp.Resources.Debug;
using static ComputeSharp.Win32.D3D12_FORMAT_SUPPORT1;

#pragma warning disable IDE0022

namespace ComputeSharp;

/// <summary>
/// A <see langword="class"/> representing a typed readonly 1D texture stored on GPU memory.
/// </summary>
/// <typeparam name="T">The type of items stored on the texture.</typeparam>
[DebuggerTypeProxy(typeof(Texture1DDebugView<>))]
[DebuggerDisplay("{ToString(),raw}")]
public sealed class ReadOnlyTexture1D<T> : Texture1D<T>, IReadOnlyTexture1D<T>
    where T : unmanaged
{
    /// <summary>
    /// Creates a new <see cref="ReadOnlyTexture1D{T}"/> instance with the specified parameters.
    /// </summary>
    /// <param name="device">The <see cref="GraphicsDevice"/> associated with the current instance.</param>
    /// <param name="width">The width of the texture.</param>
    /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
    internal ReadOnlyTexture1D(GraphicsDevice device, int width, AllocationMode allocationMode)
        : base(device, width, ResourceType.ReadOnly, allocationMode, D3D12_FORMAT_SUPPORT1_TEXTURE1D)
    {
    }

    /// <inheritdoc/>
    public ref readonly T this[int x] => throw new InvalidExecutionContextException($"{typeof(ReadOnlyTexture1D<T>)}[{typeof(int)}]");

    /// <inheritdoc/>
    public ref readonly T Sample(float u) => throw new InvalidExecutionContextException($"{typeof(ReadOnlyTexture1D<T>)}.{nameof(Sample)}({typeof(float)})");

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"ComputeSharp.ReadOnlyTexture1D<{typeof(T)}>[{Width}]";
    }
}