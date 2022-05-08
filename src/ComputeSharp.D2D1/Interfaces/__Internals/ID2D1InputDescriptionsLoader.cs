using System;
using System.ComponentModel;

namespace ComputeSharp.D2D1.__Internals;

/// <summary>
/// A base <see langword="interface"/> representing a loader for input descriptions in a D2D1 shader.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
[Obsolete("This interface is not intended to be used directly by user code")]
public interface ID2D1InputDescriptionsLoader
{
    /// <summary>
    /// Loads the available input descriptions for the shader.
    /// </summary>
    /// <param name="data">The sequence of serialized input descriptions.</param>
    void LoadInputDescriptions(ReadOnlySpan<byte> data);
}
