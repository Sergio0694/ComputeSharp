using System.Diagnostics;

namespace ComputeSharp.Resources.Debug;

/// <summary>
/// A debug proxy used to display items in a <see cref="TransferTexture1D{T}"/> instance.
/// </summary>
/// <typeparam name="T">The type of items to display.</typeparam>
internal sealed class TransferTexture1DDebugView<T>
    where T : unmanaged
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TransferTexture1DDebugView{T}"/> class with the specified parameters.
    /// </summary>
    /// <param name="texture">The input <see cref="TransferTexture1D{T}"/> instance with the items to display.</param>
    public TransferTexture1DDebugView(TransferTexture1D<T>? texture)
    {
        Items = texture?.Span.ToArray();
    }

    /// <summary>
    /// Gets the items to display for the current instance.
    /// </summary>
    [DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
    public T[]? Items { get; }
}