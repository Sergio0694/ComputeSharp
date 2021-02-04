using System.Diagnostics;
using ComputeSharp.Graphics.Resources.Enums;
using ComputeSharp.Resources;
using ComputeSharp.Resources.Debug;

namespace ComputeSharp
{
    /// <summary>
    /// A <see langword="class"/> representing a typed 3D texture stored on CPU memory, that can be used to transfer back from the GPU.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    [DebuggerTypeProxy(typeof(TransferTexture3DDebugView<>))]
    [DebuggerDisplay("{ToString(),raw}")]
    public sealed class ReadBackTexture3D<T> : TransferTexture3D<T>
        where T : unmanaged
    {
        /// <summary>
        /// Creates a new <see cref="ReadBackTexture3D{T}"/> instance with the specified parameters.
        /// </summary>
        /// <param name="device">The <see cref="GraphicsDevice"/> associated with the current instance.</param>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <param name="depth">The depth of the texture.</param>
        /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
        internal ReadBackTexture3D(GraphicsDevice device, int width, int height, int depth, AllocationMode allocationMode)
            : base(device, width, height, depth, ResourceType.ReadBack, allocationMode)
        {
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"ComputeSharp.ReadBackTexture3D<{typeof(T)}>[{Width}, {Height}, {Depth}]";
        }
    }
}
