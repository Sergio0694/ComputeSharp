using System;
using System.Drawing;
using ComputeSharp.D2D1.Interop;

namespace ComputeSharp.D2D1.Shaders.Interop.Effects.TransformMappers;

/// <summary>
/// A base type to support non-generic <see cref="D2D1DrawTransformMapper{T}"/> uses.
/// </summary>
internal unsafe interface ID2D1DrawTransformMapperInterop
{
    /// <inheritdoc cref="D2D1DrawTransformMapper{T}.MapInputsToOutput"/>
    /// <param name="d2D1DrawInfoUpdateContext">The <see cref="ID2D1DrawInfoUpdateContext"/> instance associated with the current effect.</param>
    /// <param name="inputs">The input rectangles to be mapped to the output rectangle. This parameter is always equal to the input bounds.</param>
    /// <param name="opaqueInputs">The input rectangles to be mapped to the opaque output rectangle.</param>
    /// <param name="output">The output rectangle that maps to the corresponding input rectangle.</param>
    /// <param name="opaqueOutput">The output rectangle that maps to the corresponding opaque input rectangle.</param>
    void MapInputsToOutput(
        ID2D1DrawInfoUpdateContext* d2D1DrawInfoUpdateContext,
        ReadOnlySpan<Rectangle> inputs,
        ReadOnlySpan<Rectangle> opaqueInputs,
        out Rectangle output,
        out Rectangle opaqueOutput);

    /// <inheritdoc cref="D2D1DrawTransformMapper{T}.MapOutputToInputs"/>
    void MapOutputToInputs(in Rectangle output, Span<Rectangle> inputs);

    /// <inheritdoc cref="D2D1DrawTransformMapper{T}.MapInvalidOutput"/>
    void MapInvalidOutput(int inputIndex, in Rectangle invalidInput, out Rectangle invalidOutput);
}