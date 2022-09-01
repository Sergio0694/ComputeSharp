using System.Diagnostics;
using ComputeSharp.Graphics.Resources.Enums;
using ComputeSharp.Resources;
using ComputeSharp.Resources.Debug;

namespace ComputeSharp;

/// <summary>
/// A <see langword="class"/> representing a typed 1D texture stored on CPU memory, that can be used to transfer back from the GPU.
/// </summary>
/// <typeparam name="T">The type of items stored on the texture.</typeparam>
[DebuggerTypeProxy(typeof(TransferTexture1DDebugView<>))]
[DebuggerDisplay("{ToString(),raw}")]
public sealed class ReadBackTexture1D<T> : TransferTexture1D<T>
    where T : unmanaged
{
    /// <summary>
    /// Creates a new <see cref="ReadBackTexture1D{T}"/> instance with the specified parameters.
    /// </summary>
    /// <param name="device">The <see cref="GraphicsDevice"/> associated with the current instance.</param>
    /// <param name="width">The width of the texture.</param>
    /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
    internal ReadBackTexture1D(GraphicsDevice device, int width, AllocationMode allocationMode)
        : base(device, width, ResourceType.ReadBack, allocationMode)
    {
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"ComputeSharp.ReadBackTexture1D<{typeof(T)}>[{Width}]";
    }
}
