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
    /// Gets a unique dispatch identifier for the shader.
    /// </summary>
    /// <returns>The unique dispatch identifier for the shader.</returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("This method is not intended to be used directly by user code")]
    int GetDispatchId();

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
    /// <param name="builder">The resulting <see cref="ArrayPoolStringBuilder"/> instance containing the HLSL source.</param>
    /// <param name="threadsX">The number of threads in each thread group for the X axis.</param>
    /// <param name="threadsY">The number of threads in each thread group for the Y axis.</param>
    /// <param name="threadsZ">The number of threads in each thread group for the Z axis.</param>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("This method is not intended to be called directly by user code")]
    void BuildHlslSource(out ArrayPoolStringBuilder builder, int threadsX, int threadsY, int threadsZ);

    /// <summary>
    /// Loads the bytecode for the current shader.
    /// </summary>
    /// <typeparam name="TLoader">The type of bytecode loader being used.</typeparam>
    /// <param name="loader">The <typeparamref name="TLoader"/> instance to use to load the bytecode.</param>
    /// <param name="threadsX">The number of threads in each thread group for the X axis.</param>
    /// <param name="threadsY">The number of threads in each thread group for the Y axis.</param>
    /// <param name="threadsZ">The number of threads in each thread group for the Z axis.</param>
    /// <exception cref="NotSupportedException">Thrown if the bytecode required runtime compilation and it wasn't supported on the current configuration.</exception>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("This method is not intended to be used directly by user code")]
    void LoadBytecode<TLoader>(ref TLoader loader, int threadsX, int threadsY, int threadsZ)
        where TLoader : struct, IBytecodeLoader;
}
