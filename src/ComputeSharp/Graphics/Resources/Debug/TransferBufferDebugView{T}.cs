using System;
using System.Diagnostics;

namespace ComputeSharp.Resources.Debug
{
    /// <summary>
    /// A debug proxy used to display items in a <see cref="TransferBuffer{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items to display.</typeparam>
    internal sealed class TransferBufferDebugView<T>
        where T : unmanaged
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransferBufferDebugView{T}"/> class with the specified parameters.
        /// </summary>
        /// <param name="buffer">The input <see cref="TransferBuffer{T}"/> instance with the items to display.</param>
        public TransferBufferDebugView(TransferBuffer<T>? buffer)
        {
            if (buffer is not null)
            {
                var items = GC.AllocateUninitializedArray<T>(buffer.Length);

                buffer.Span.CopyTo(items);

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
