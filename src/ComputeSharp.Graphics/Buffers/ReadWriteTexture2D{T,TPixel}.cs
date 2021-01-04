using System.Diagnostics;
using ComputeSharp.__Internals;
using ComputeSharp.Exceptions;
using ComputeSharp.Graphics;
using ComputeSharp.Graphics.Buffers.Abstract;
using ComputeSharp.Graphics.Buffers.Enums;
using ComputeSharp.Graphics.Buffers.Views;

#pragma warning disable CS0618

namespace ComputeSharp
{
    /// <summary>
    /// A <see langword="class"/> representing a typed read write 2D texture stored on GPU memory.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
    [DebuggerTypeProxy(typeof(Texture2DDebugView<>))]
    [DebuggerDisplay("{ToString(),raw}")]
    public sealed class ReadWriteTexture2D<T, TPixel> : Texture2D<T>
        where T : unmanaged, IUnorm<TPixel>
        where TPixel : unmanaged
    {
        /// <summary>
        /// Creates a new <see cref="ReadWriteTexture2D{T,TPixel}"/> instance with the specified parameters.
        /// </summary>
        /// <param name="device">The <see cref="GraphicsDevice"/> associated with the current instance.</param>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        internal ReadWriteTexture2D(GraphicsDevice device, int width, int height)
            : base(device, width, height, ResourceType.ReadWrite)
        {
        }

        /// <summary>
        /// Gets a single <typeparamref name="T"/> value from the current writeable texture.
        /// </summary>
        /// <param name="x">The horizontal offset of the value to get.</param>
        /// <param name="y">The vertical offset of the value to get.</param>
        /// <remarks>This API can only be used from a compute shader, and will always throw if used anywhere else.</remarks>
        public ref TPixel this[int x, int y] => throw new InvalidExecutionContextException($"{nameof(ReadWriteTexture2D<T, TPixel>)}<T>[int,int]");

        /// <summary>
        /// Gets or sets a single <typeparamref name="T"/> value from the current writeable texture.
        /// </summary>
        /// <param name="xy">The coordinates of the value to get.</param>
        /// <remarks>This API can only be used from a compute shader, and will always throw if used anywhere else.</remarks>
        public ref TPixel this[Int2 xy] => throw new InvalidExecutionContextException($"{nameof(ReadWriteTexture2D<T, TPixel>)}<T>[Int2]");

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"ComputeSharp.ReadWriteTexture2D<{typeof(T)},{typeof(TPixel)}>[{Width}, {Height}]";
        }
    }
}
