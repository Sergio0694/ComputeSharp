using System;
using System.ComponentModel;

namespace ComputeSharp.D2D1.__Internals;

/// <summary>
/// A base <see langword="interface"/> representing a loader for resource texture descriptions in a D2D1 shader.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
[Obsolete("This interface is not intended to be used directly by user code")]
public interface ID2D1ResourceTextureDescriptionsLoader
{
    /// <summary>
    /// Loads the available resource texture descriptions for the shader.
    /// </summary>
    /// <param name="data">The sequence of serialized resource texture descriptions.</param>
    void LoadResourceTextureDescriptions(ReadOnlySpan<byte> data);
}
