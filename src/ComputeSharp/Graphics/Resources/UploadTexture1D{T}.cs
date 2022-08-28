using System.Diagnostics;
using ComputeSharp.Graphics.Resources.Enums;
using ComputeSharp.Resources;
using ComputeSharp.Resources.Debug;

namespace ComputeSharp;

/// <summary>
/// A <see langword="class"/> representing a typed 1D texture stored on CPU memory, that can be used to transfer data to the GPU.
/// </summary>
/// <typeparam name="T">The type of items stored on the texture.</typeparam>
[DebuggerTypeProxy(typeof(TransferTexture1DDebugView<>))]
[DebuggerDisplay("{ToString(),raw}")]
public sealed class UploadTexture1D<T> : TransferTexture1D<T>
    where T : unmanaged
{
    /// <summary>
    /// Creates a new <see cref="UploadTexture1D{T}"/> instance with the specified parameters.
    /// </summary>
    /// <param name="device">The <see cref="GraphicsDevice"/> associated with the current instance.</param>
    /// <param name="width">The width of the texture.</param>
    /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
    internal UploadTexture1D(GraphicsDevice device, int width, AllocationMode allocationMode)
        : base(device, width, ResourceType.Upload, allocationMode)
    {
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"ComputeSharp.UploadTexture1D<{typeof(T)}>[{Width}]";
    }
}
