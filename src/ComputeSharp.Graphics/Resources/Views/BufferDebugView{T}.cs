using System.Diagnostics;
#if NET5_0
using GC = System.GC;
#else
using GC = Polyfills.GC;
#endif

namespace ComputeSharp.Resources.Views
{
    /// <summary>
    /// A debug proxy used to display items in a <see cref="Buffer{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items to display.</typeparam>
    internal sealed class BufferDebugView<T>
        where T : unmanaged
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BufferDebugView{T}"/> class with the specified parameters.
        /// </summary>
        /// <param name="buffer">The input <see cref="Buffer{T}"/> instance with the items to display.</param>
        public BufferDebugView(Buffer<T>? buffer)
        {
            if (buffer is not null)
            {
                var items = GC.AllocateUninitializedArray<T>(buffer.Length);

                buffer.GetData(ref items[0], buffer.Length, 0);

                Items = items;
            }
        }

        /// <summary>
        /// Gets the items to display for the current instance.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
        public T[]? Items { get; }
    }
}
