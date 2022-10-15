using System;
using System.ComponentModel;

namespace ComputeSharp.__Internals;

/// <summary>
/// A base <see langword="interface"/> representing a data loader for a shader being dispatched.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
[Obsolete("This interface is not intended to be used directly by user code")]
public interface IDispatchDataLoader
{
    /// <summary>
    /// Loads the captured values used by the shader to be dispatched.
    /// </summary>
    /// <param name="data">The sequence of serialized captured values.</param>
    void LoadCapturedValues(ReadOnlySpan<uint> data);

    /// <summary>
    /// Loads the captured resources used by the shader to be dispatched.
    /// </summary>
    /// <param name="data">The sequence of serialized resources.</param>
    void LoadCapturedResources(ReadOnlySpan<ulong> data);
}