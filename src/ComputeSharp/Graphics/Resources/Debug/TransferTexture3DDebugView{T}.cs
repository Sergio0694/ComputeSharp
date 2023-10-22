using System.Diagnostics;

namespace ComputeSharp.Resources.Debug;

/// <summary>
/// A debug proxy used to display items in a <see cref="TransferTexture3D{T}"/> instance.
/// </summary>
/// <typeparam name="T">The type of items to display.</typeparam>
/// <param name="texture">The input <see cref="TransferTexture3D{T}"/> instance with the items to display.</param>
internal sealed class TransferTexture3DDebugView<T>(TransferTexture3D<T>? texture)
    where T : unmanaged
{
    /// <summary>
    /// Gets the items to display for the current instance.
    /// </summary>
    [DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
    public T[,,]? Items { get; } = texture?.View.ToArray();
}