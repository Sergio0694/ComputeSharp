using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using ComputeSharp.Graphics.Buffers.Abstract;

namespace ComputeSharp.Graphics.Buffers.Views
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

                buffer.GetData(ref MemoryMarshal.GetArrayDataReference(items), buffer.Length, 0);

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
