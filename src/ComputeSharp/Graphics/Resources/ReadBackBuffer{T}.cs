using System.Diagnostics;
using ComputeSharp.Graphics.Resources.Enums;
using ComputeSharp.Resources;
using ComputeSharp.Resources.Debug;

namespace ComputeSharp
{
    /// <summary>
    /// A <see langword="class"/> representing a typed buffer stored on CPU memory, that can be used to transfer back from the GPU.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the buffer.</typeparam>
    [DebuggerTypeProxy(typeof(TransferBufferDebugView<>))]
    [DebuggerDisplay("{ToString(),raw}")]
    public sealed class ReadBackBuffer<T> : TransferBuffer<T>
        where T : unmanaged
    {
        /// <summary>
        /// Creates a new <see cref="ReadBackBuffer{T}"/> instance with the specified parameters.
        /// </summary>
        /// <param name="device">The <see cref="GraphicsDevice"/> associated with the current instance.</param>
        /// <param name="length">The number of items to store in the current buffer.</param>
        /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
        internal ReadBackBuffer(GraphicsDevice device, int length, AllocationMode allocationMode)
            : base(device, length, ResourceType.ReadBack, allocationMode)
        {
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"ComputeSharp.ReadBackBuffer<{typeof(T)}>[{Length}]";
        }
    }
}
