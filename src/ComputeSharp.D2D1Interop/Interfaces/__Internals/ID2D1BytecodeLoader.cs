using System;
using System.ComponentModel;

namespace ComputeSharp.D2D1Interop.__Internals;

/// <summary>
/// A base <see langword="interface"/> representing a bytecode loader for a D2D1 shader.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
[Obsolete("This interface is not intended to be used directly by user code")]
public interface ID2D1BytecodeLoader
{
    /// <summary>
    /// Loads a dynamic shader bytecode.
    /// </summary>
    /// <param name="handle">An opaque handle to the shader bytecode.</param>
    /// <exception cref="InvalidOperationException">Thrown if the shader has already been initialized or a precompiled bytecode was requested when not available.</exception>
    void LoadDynamicBytecode(IntPtr handle);

    /// <summary>
    /// Loads the embedded shader bytecode for the shader.
    /// </summary>
    /// <param name="bytecode">The shader bytecode.</param>
    /// <exception cref="InvalidOperationException">Thrown if the shader has already been initialized.</exception>
    void LoadEmbeddedBytecode(ReadOnlySpan<byte> bytecode);
}
