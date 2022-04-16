using System;
using System.ComponentModel;

namespace ComputeSharp.D2D1.__Internals;

/// <summary>
/// A base <see langword="interface"/> representing a data loader for a D2D1 shader being dispatched.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
[Obsolete("This interface is not intended to be used directly by user code")]
public interface ID2D1DispatchDataLoader
{
    /// <summary>
    /// Loads the captured values used by the D2D1 shader to be dispatched.
    /// </summary>
    /// <param name="data">The sequence of serialized captured values.</param>
    void LoadConstantBuffer(ReadOnlySpan<uint> data);
}
