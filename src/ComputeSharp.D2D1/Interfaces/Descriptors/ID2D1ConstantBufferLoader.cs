using System;

namespace ComputeSharp.D2D1.Descriptors;

/// <summary>
/// A type representing a data loader for the constant buffer of a D2D1 shader.
/// </summary>
public interface ID2D1ConstantBufferLoader
{
    /// <summary>
    /// Loads the constant buffer of a D2D1 shader.
    /// </summary>
    /// <param name="data">The constant buffer for the D2D1 shader.</param>
    void LoadConstantBuffer(ReadOnlySpan<byte> data);
}