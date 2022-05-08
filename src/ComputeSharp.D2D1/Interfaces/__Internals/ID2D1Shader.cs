using System;
using System.ComponentModel;

namespace ComputeSharp.D2D1.__Internals;

/// <summary>
/// A base <see langword="interface"/> representing a given shader that can be dispatched.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
[Obsolete("This interface is not intended to be used directly by user code")]
public interface ID2D1Shader
{
    /// <summary>
    /// Initializes the current shader from a buffer with the serialized dispatch data.
    /// </summary>
    /// <param name="data">The input buffer with the serialized dispatch data.</param>
    /// <remarks>The input buffer must be retrieved from <see cref="LoadDispatchData"/>.</remarks>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("This method is not intended to be called directly by user code")]
    void InitializeFromDispatchData(ReadOnlySpan<byte> data);

    /// <summary>
    /// Gets the pixel options for the current shader.
    /// </summary>
    /// <returns>The pixel options for the current shader.</returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("This method is not intended to be used directly by user code")]
    uint GetPixelOptions();

    /// <summary>
    /// Gets the number of inputs for the current shader.
    /// </summary>
    /// <returns>The number of inputs for the current shader.</returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("This method is not intended to be used directly by user code")]
    uint GetInputCount();

    /// <summary>
    /// Gets the input type for the input at the input index.
    /// </summary>
    /// <param name="index">The index of the shader input to get the type for.</param>
    /// <returns>The type of input at the specified index.</returns>
    /// <remarks>The return value if the input is out of range is undefined.</remarks>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("This method is not intended to be used directly by user code")]
    uint GetInputType(uint index);

    /// <summary>
    /// Loads the input descriptions for the shader, if any.
    /// </summary>
    /// <typeparam name="TLoader">The type of input descriptions loader being used.</typeparam>
    /// <param name="loader">The <typeparamref name="TLoader"/> instance to use to load the input descriptions.</param>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("This method is not intended to be used directly by user code")]
    void LoadInputDescriptions<TLoader>(ref TLoader loader)
        where TLoader : struct, ID2D1InputDescriptionsLoader;

    /// <summary>
    /// Gets the output buffer precision and depth for the shader.
    /// </summary>
    /// <param name="precision">The output buffer precision.</param>
    /// <param name="depth">The output buffer channel depth.</param>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("This method is not intended to be used directly by user code")]
    void GetOutputBuffer(out uint precision, out uint depth);

    /// <summary>
    /// Loads the dispatch data for the shader.
    /// </summary>
    /// <typeparam name="TLoader">The type of data loader being used.</typeparam>
    /// <param name="loader">The <typeparamref name="TLoader"/> instance to use to load the data.</param>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("This method is not intended to be called directly by user code")]
    void LoadDispatchData<TLoader>(ref TLoader loader)
        where TLoader : struct, ID2D1DispatchDataLoader;

    /// <summary>
    /// Builds the HLSL source code for the current shader instance.
    /// </summary>
    /// <param name="hlslSource">The resulting HLSL source.</param>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("This method is not intended to be called directly by user code")]
    void BuildHlslSource(out string hlslSource);

    /// <summary>
    /// Loads the bytecode for the current shader.
    /// </summary>
    /// <typeparam name="TLoader">The type of bytecode loader being used.</typeparam>
    /// <param name="loader">The <typeparamref name="TLoader"/> instance to use to load the bytecode.</param>
    /// <param name="shaderProfile">The shader profile to use to get the shader bytecode (if <see langword="null"/>, the precompiled shader will be used).</param>
    /// <exception cref="InvalidOperationException">Thrown if a precompiled bytecode was requested (<paramref name="shaderProfile"/> is <see langword="null"/>), but it wasn't availablle.</exception>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("This method is not intended to be used directly by user code")]
    void LoadBytecode<TLoader>(ref TLoader loader, D2D1ShaderProfile? shaderProfile)
        where TLoader : struct, ID2D1BytecodeLoader;
}
