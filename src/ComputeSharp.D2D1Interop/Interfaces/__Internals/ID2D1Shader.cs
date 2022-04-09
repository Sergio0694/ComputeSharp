using System;
using System.ComponentModel;
using ComputeSharp.__Internals;

namespace ComputeSharp.D2D1Interop.__Internals;

/// <summary>
/// A base <see langword="interface"/> representing a given shader that can be dispatched.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
[Obsolete("This interface is not intended to be used directly by user code")]
public interface ID2D1Shader
{
    /// <summary>
    /// Loads the dispatch data for the shader.
    /// </summary>
    /// <typeparam name="TLoader">The type of data loader being used.</typeparam>
    /// <param name="loader">The <typeparamref name="TLoader"/> instance to use to load the data.</param>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("This method is not intended to be called directly by user code")]
    void LoadDispatchData<TLoader>(ref TLoader loader)
        where TLoader : struct, ID2D1DispatchDataLoader
#if NET6_0_OR_GREATER
    {
        throw new NotImplementedException();
    }
#else
    ;
#endif

    /// <summary>
    /// Builds the HLSL source code for the current shader instance.
    /// </summary>
    /// <param name="hlslSource">The resulting HLSL source.</param>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("This method is not intended to be called directly by user code")]
    void BuildHlslSource(out string hlslSource)
#if NET6_0_OR_GREATER
    {
        throw new NotImplementedException();
    }
#else
    ;
#endif

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
        where TLoader : struct, ID2D1BytecodeLoader
#if NET6_0_OR_GREATER
    {
        throw new NotImplementedException();
    }
#else
    ;
#endif
}
