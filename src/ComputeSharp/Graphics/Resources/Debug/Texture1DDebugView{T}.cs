using System.Diagnostics;

namespace ComputeSharp.Resources.Debug;

/// <summary>
/// A debug proxy used to display items in a <see cref="Texture1D{T}"/> instance.
/// </summary>
/// <typeparam name="T">The type of items to display.</typeparam>
internal sealed class Texture1DDebugView<T>
    where T : unmanaged
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Texture1DDebugView{T}"/> class with the specified parameters.
    /// </summary>
    /// <param name="texture">The input <see cref="Texture1D{T}"/> instance with the items to display.</param>
    public Texture1DDebugView(Texture1D<T>? texture)
    {
        if (texture is not null)
        {
            T[] items = new T[texture.Width];

            texture.CopyTo(ref items[0], items.Length, 0, texture.Width);

            Items = items;
        }
    }

    /// <summary>
    /// Gets the items to display for the current instance.
    /// </summary>
    [DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
    public T[]? Items { get; }
}