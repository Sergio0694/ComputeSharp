using System;
using System.ComponentModel;

namespace ComputeSharp.__Internals;

/// <summary>
/// A base <see langword="interface"/> representing a given shader that can be dispatched.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
[Obsolete("This interface is not intended to be used directly by user code")]
public interface IShader
{
    /// <summary>
    /// The number of threads in each thread group for the X axis.
    /// </summary>
    int ThreadsX { get; }

    /// <summary>
    /// The number of threads in each thread group for the Y axis.
    /// </summary>
    int ThreadsY { get; }

    /// <summary>
    /// The number of threads in each thread group for the Z axis.
    /// </summary>
    int ThreadsZ { get; }

    /// <summary>
    /// Gets the HLSL bytecode for the current shader.
    /// </summary>
    ReadOnlyMemory<byte> HlslBytecode { get; }

    /// <summary>
    /// Loads the dispatch data for the shader.
    /// </summary>
    /// <typeparam name="TLoader">The type of data loader being used.</typeparam>
    /// <param name="loader">The <typeparamref name="TLoader"/> instance to use to load the data.</param>
    /// <param name="device">The device the shader is being dispatched on.</param>
    /// <param name="x">The number of iterations to run on the X axis.</param>
    /// <param name="y">The number of iterations to run on the Y axis.</param>
    /// <param name="z">The number of iterations to run on the Z axis.</param>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("This method is not intended to be called directly by user code")]
    void LoadDispatchData<TLoader>(ref TLoader loader, GraphicsDevice device, int x, int y, int z)
        where TLoader : struct, IDispatchDataLoader;

    /// <summary>
    /// Loads an opaque metadata handle from the metadata of the current shader.
    /// </summary>
    /// <typeparam name="TLoader">The type of data loader being used.</typeparam>
    /// <param name="loader">The <typeparamref name="TLoader"/> instance to use to load the data.</param>
    /// <param name="result">The resulting opaque metadata handle.</param>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("This method is not intended to be called directly by user code")]
    void LoadDispatchMetadata<TLoader>(ref TLoader loader, out IntPtr result)
        where TLoader : struct, IDispatchMetadataLoader;

    /// <summary>
    /// Builds the HLSL source code for the current shader instance.
    /// </summary>
    /// <param name="hlslSource">The resulting HLSL source.</param>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("This method is not intended to be called directly by user code")]
    void BuildHlslSource(out string hlslSource);
}