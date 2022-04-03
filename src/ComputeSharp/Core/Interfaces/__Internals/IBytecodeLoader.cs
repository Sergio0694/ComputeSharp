using System;
using System.ComponentModel;

namespace ComputeSharp.__Internals;

/// <summary>
/// A base <see langword="interface"/> representing a bytecode loader for a shader being dispatched.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
[Obsolete("This interface is not intended to be used directly by user code")]
public interface IBytecodeLoader
{
    /// <summary>
    /// Loads a dynamic shader bytecode.
    /// </summary>
    /// <param name="handle">An opaque handle to the shader bytecode.</param>
    /// <exception cref="InvalidOperationException">Thrown if the shader has already been initialized.</exception>
    /// <exception cref="NotSupportedException">Thrown if <paramref name="handle"/> is <see langword="null"/> (indicating an unsupported configuration).</exception>
    void LoadDynamicBytecode(IntPtr handle);

    /// <summary>
    /// Loads the embedded shader bytecode for the shader being dispatched.
    /// </summary>
    /// <param name="bytecode">The shader bytecode.</param>
    /// <exception cref="InvalidOperationException">Thrown if the shader has already been initialized.</exception>
    void LoadEmbeddedBytecode(ReadOnlySpan<byte> bytecode);
}
