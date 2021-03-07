using System.Diagnostics;

namespace ComputeSharp.Resources.Debug
{
    /// <summary>
    /// A debug proxy used to display items in a <see cref="Texture2D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items to display.</typeparam>
    internal sealed class Texture2DDebugView<T>
        where T : unmanaged
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Texture2DDebugView{T}"/> class with the specified parameters.
        /// </summary>
        /// <param name="texture">The input <see cref="Texture2D{T}"/> instance with the items to display.</param>
        public Texture2DDebugView(Texture2D<T>? texture)
        {
            if (texture is not null)
            {
                var items = new T[texture.Height, texture.Width];

                texture.CopyTo(ref items[0, 0], items.Length, 0, 0, texture.Width, texture.Height);

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
