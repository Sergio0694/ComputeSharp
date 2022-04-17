using System;
using System.Drawing;

namespace ComputeSharp.D2D1;

/// <summary>
/// An <see langword="interface"/> providing support for custom, stateful draw transform mappings for shaders.
/// </summary>
public interface ID2D1TransformMapper<T>
    where T : unmanaged, ID2D1PixelShader
{
    /// <summary>
    /// <para>
    /// Allows a transform to state how it would map a a set of sample rectangles on its input to an output rectangle.
    /// </para>
    /// <para>
    /// This is effectively the inverse mapping of <see cref="MapOutputToInputs"/>.
    /// </para>
    /// </summary>
    /// <param name="shader">The input D2D1 pixel shader being executed on the current effect (this can be used to retrieve shader properties).</param>
    /// <param name="inputs">The input rectangles to be mapped to the output rectangle. This parameter is always equal to the input bounds.</param>
    /// <param name="opaqueInputs">The input rectangles to be mapped to the opaque output rectangle.</param>
    /// <param name="output">The output rectangle that maps to the corresponding input rectangle.</param>
    /// <param name="opaqueOutput">The output rectangle that maps to the corresponding opaque input rectangle.</param>
    /// <remarks>
    /// <para>
    /// Unlike <see cref="MapOutputToInputs"/> and <see cref="MapInvalidOutput"/>, this method is explicitly called by the renderer at
    /// a determined place in its rendering algorithm. The transform implementation may change its state based on the input rectangles
    /// and use this information to control its rendering information. This method is always called before the
    /// <see cref="MapInvalidOutput"/> and <see cref="MapOutputToInputs"/> methods.
    /// </para>
    /// <para>
    /// For more info, see <see href="https://docs.microsoft.com/windows/win32/api/d2d1effectauthor/nf-d2d1effectauthor-id2d1transform-mapinputrectstooutputrect"/>.
    /// </para>
    /// </remarks>
    void MapInputsToOutput(
        in T shader,
        ReadOnlySpan<Rectangle> inputs,
        ReadOnlySpan<Rectangle> opaqueInputs,
        out Rectangle output,
        out Rectangle opaqueOutput);

    /// <summary>
    /// Allows a transform to state how it would map a rectangle requested on its output to a set of sample rectangles on its input.
    /// </summary>
    /// <param name="shader">The input D2D1 pixel shader being executed on the current effect (this can be used to retrieve shader properties).</param>
    /// <param name="output">The output rectangle from which the inputs must be mapped.</param>
    /// <param name="inputs">The corresponding set of inputs. The inputs will directly correspond to the transform inputs.</param>
    /// <remarks>
    /// <para>
    /// The transform implementation must regard this method as purely functional. It can base the mapped input and output rectangles on its
    /// current state as specified by the encapsulating effect properties. However, it must not change its own state in response to this method
    /// being invoked. The Direct2D renderer implementation reserves the right to call this method at any time and in any sequence.
    /// </para>
    /// <para>
    /// For more info, see <see href="https://docs.microsoft.com/windows/win32/api/d2d1effectauthor/nf-d2d1effectauthor-id2d1transform-mapoutputrecttoinputrects"/>.
    /// </para>
    /// </remarks>
    void MapOutputToInputs(in T shader, in Rectangle output, Span<Rectangle> inputs);

    /// <summary>
    /// Sets the input rectangles for this rendering pass into the transform.
    /// </summary>
    /// <param name="shader">The input D2D1 pixel shader being executed on the current effect (this can be used to retrieve shader properties).</param>
    /// <param name="inputIndex">The index of the input rectangle.</param>
    /// <param name="invalidInput">The invalid input rectangle.</param>
    /// <param name="invalidOutput">The output rectangle to which the input rectangle must be mapped.</param>
    /// <remarks>
    /// <para>
    /// The transform implementation must regard this method as purely functional. The transform implementation can base the mapped input rectangle on
    /// the transform implementation's current state as specified by the encapsulating effect properties. But the transform implementation can't change
    /// its own state in response to a call to <see cref="MapInvalidOutput"/>. Direct2D can call this method at any time
    /// and in any sequence following a call to <see cref="MapInputsToOutput"/>.
    /// </para>
    /// <para>
    /// For more info, see <see href="https://docs.microsoft.com/windows/win32/api/d2d1effectauthor/nf-d2d1effectauthor-id2d1transform-mapinvalidrect"/>.
    /// </para>
    /// </remarks>
    void MapInvalidOutput(in T shader, int inputIndex, Rectangle invalidInput, out Rectangle invalidOutput);
}
