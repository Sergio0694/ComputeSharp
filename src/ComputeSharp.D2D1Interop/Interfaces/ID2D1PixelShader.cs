﻿using ComputeSharp.D2D1Interop.__Internals;

#pragma warning disable CS0618

namespace ComputeSharp.D2D1Interop;

/// <summary>
/// An <see langword="interface"/> representing a pixel shader.
/// </summary>
public interface IPixelShader<TPixel> : ID2D1Shader
    where TPixel : unmanaged
{
    /// <summary>
    /// Executes the current pixel shader.
    /// </summary>
    /// <returns>The pixel value for the current invocation.</returns>
    TPixel Execute();
}