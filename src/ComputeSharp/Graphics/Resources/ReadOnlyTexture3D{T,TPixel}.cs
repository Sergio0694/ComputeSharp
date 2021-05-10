using System.Diagnostics;
using ComputeSharp.__Internals;
using ComputeSharp.Exceptions;
using ComputeSharp.Graphics.Resources.Enums;
using ComputeSharp.Resources;
using ComputeSharp.Resources.Debug;
using static TerraFX.Interop.D3D12_FORMAT_SUPPORT1;

#pragma warning disable CS0618

namespace ComputeSharp
{
    /// <summary>
    /// A <see langword="class"/> representing a typed readonly 3D texture stored on GPU memory.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
    [DebuggerTypeProxy(typeof(Texture3DDebugView<>))]
    [DebuggerDisplay("{ToString(),raw}")]
    public sealed class ReadOnlyTexture3D<T, TPixel> : Texture3D<T>, IReadOnlyTexture3D<TPixel>
        where T : unmanaged, IUnorm<TPixel>
        where TPixel : unmanaged
    {
        /// <summary>
        /// Creates a new <see cref="ReadOnlyTexture3D{T,TPixel}"/> instance with the specified parameters.
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
        public TPixel this[int x, int y, int z] => throw new InvalidExecutionContextException($"{typeof(ReadOnlyTexture3D<T, TPixel>)}[{typeof(int)}, {typeof(int)}, {typeof(int)}]");

        /// <inheritdoc/>
        public TPixel this[Int3 xyz] => throw new InvalidExecutionContextException($"{typeof(ReadOnlyTexture3D<T, TPixel>)}[{typeof(Int3)}]");

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"ComputeSharp.ReadOnlyTexture3D<{typeof(T)}, {nameof(TPixel)}>[{Width}, {Height}, {Depth}]";
        }
    }
}
