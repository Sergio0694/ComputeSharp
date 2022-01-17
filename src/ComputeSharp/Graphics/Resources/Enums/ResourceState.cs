namespace ComputeSharp;

/// <summary>
/// An <see langword="enum"/> that indicates the state of a resource after a given transition.
/// </summary>
public enum ResourceState
{
    /// <summary>
    /// A readonly resource, that can only be read from by the GPU and supports texture sampling.
    /// </summary>
    ReadOnly,

    /// <summary>
    /// A read write resource, with both read and write access for the GPU, but without support for texture sampling.
    /// </summary>
    ReadWrite
}
