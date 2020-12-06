using System.Diagnostics;
using ComputeSharp.Graphics.Buffers.Abstract;

namespace ComputeSharp.Graphics.Buffers.Views
{
    /// <summary>
    /// A debug proxy used to display items in a <see cref="Texture2D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items to display.</typeparam>
    internal sealed class BufferDebugView2D<T>
        where T : unmanaged
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BufferDebugView2D{T}"/> class with the specified parameters.
        /// </summary>
        /// <param name="texture">The input <see cref="Texture2D{T}"/> instance with the items to display.</param>
        public BufferDebugView2D(Texture2D<T>? texture)
        {
            if (texture is not null)
            {
                var items = new T[texture.Height, texture.Width];

                texture.GetData(ref items[0, 0], items.Length, 0, 0, texture.Height, texture.Width);

                Items = items;
            }
        }

        /// <summary>
        /// Gets the items to display for the current instance.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
        public T[,]? Items { get; }
    }
}
