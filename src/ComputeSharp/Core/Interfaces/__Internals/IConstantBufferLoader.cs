using System;

namespace ComputeSharp.__Internals;

/// <summary>
/// A type representing a data loader for the constant buffer of a compute shader.
/// </summary>
public interface IConstantBufferLoader
{
    /// <summary>
    /// Loads the constant buffer of a compute shader.
    /// </summary>
    /// <param name="data">The constant buffer for the compute shader (the size must be a multiple of the size of a DWORD value).</param>
    /// <exception cref="ArgumentException">Thrown if the size of <paramref name="data"/> is not a multiple of the size of a DWORD value).</exception>
    void LoadConstantBuffer(ReadOnlySpan<byte> data);
}