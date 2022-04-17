using System;
using System.Drawing;

namespace ComputeSharp.D2D1;

/// <summary>
/// An <see langword="interface"/> providing support for custom, stateful draw transform mappings for shaders.
/// </summary>
public interface ID2D1DrawTransformMapper
{
    /// <summary>
    /// Allows a transform to state how it would map a rectangle requested on its output to a set of sample rectangles on its input.
    /// </summary>
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
    void MapOutputToInputs(in Rectangle output, Span<Rectangle> inputs);

    /// <summary>
    /// Performs the inverse mapping of <see cref="MapOutputToInputs(in Rectangle, Span{Rectangle})"/>.
    /// </summary>
    /// <param name="inputs">The input rectangles to be mapped to the output rectangle. This parameter is always equal to the input bounds.</param>
    /// <param name="opaqueInputs">The input rectangles to be mapped to the opaque output rectangle.</param>
    /// <param name="output">The output rectangle that maps to the corresponding input rectangle.</param>
    /// <param name="opaqueOutput">The output rectangle that maps to the corresponding opaque input rectangle.</param>
    /// <remarks>
    /// <para>
    /// Unlike <see cref="MapOutputToInputs(in Rectangle, Span{Rectangle})"/> and <see cref="MapInvalidOutput(int, Rectangle, out Rectangle)"/>, this
    /// method is explicitly called by the renderer at a determined place in its rendering algorithm. The transform implementation may change its state
    /// based on the input rectangles and use this information to control its rendering information. This method is always called before the
    /// <see cref="MapInvalidOutput(int, Rectangle, out Rectangle)"/> and <see cref="MapOutputToInputs(in Rectangle, Span{Rectangle})"/> methods.
    /// </para>
    /// <para>
    /// For more info, see <see href="https://docs.microsoft.com/windows/win32/api/d2d1effectauthor/nf-d2d1effectauthor-id2d1transform-mapinputrectstooutputrect"/>.
    /// </para>
    /// </remarks>
    void MapInputsToOutput(ReadOnlySpan<Rectangle> inputs, ReadOnlySpan<Rectangle> opaqueInputs, out Rectangle output, out Rectangle opaqueOutput);

    /// <summary>
    /// Sets the input rectangles for this rendering pass into the transform.
    /// </summary>
    /// <param name="inputIndex">The index of the input rectangle.</param>
    /// <param name="invalidInput">The invalid input rectangle.</param>
    /// <param name="invalidOutput">The output rectangle to which the input rectangle must be mapped.</param>
    /// <remarks>
    /// <para>
    /// The transform implementation must regard this method as purely functional. The transform implementation can base the mapped input rectangle on
    /// the transform implementation's current state as specified by the encapsulating effect properties. But the transform implementation can't change
    /// its own state in response to a call to <see cref="MapInvalidOutput(int, Rectangle, out Rectangle)"/>. Direct2D can call this method at any time
    /// and in any sequence following a call to <see cref="MapInputsToOutput(ReadOnlySpan{Rectangle}, ReadOnlySpan{Rectangle}, out Rectangle, out Rectangle)"/>.
    /// </para>
    /// <para>
    /// For more info, see <see href="https://docs.microsoft.com/windows/win32/api/d2d1effectauthor/nf-d2d1effectauthor-id2d1transform-mapinvalidrect"/>.
    /// </para>
    /// </remarks>
    void MapInvalidOutput(int inputIndex, Rectangle invalidInput, out Rectangle invalidOutput);
}
