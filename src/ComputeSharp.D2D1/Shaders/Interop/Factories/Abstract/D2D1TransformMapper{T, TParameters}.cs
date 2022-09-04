using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using ComputeSharp.D2D1.Shaders.Interop.Helpers;

namespace ComputeSharp.D2D1.Shaders.Interop.Factories.Abstract;

/// <summary>
/// A base type for a transform mapper to be used in a D2D1 pixel shader effect.
/// </summary>
/// <typeparam name="T">The type of D2D1 pixel shader associated to the transform mapper.</typeparam>
/// <typeparam name="TParameters">The type of parameters that the transform mapper will use.</typeparam>
internal abstract class D2D1TransformMapper<T, TParameters> : ID2D1TransformMapper<T>
    where T : unmanaged, ID2D1PixelShader
{
    /// <summary>
    /// The current pixel shader value, which can be used to extract parameters from.
    /// </summary>
    private T shader;

    /// <inheritdoc cref="D2D1TransformMapperFactory{T, TSelf, TParameters, TTransformMapper}.Parameters"/>
    public D2D1TransformMapperParametersAccessor<T, TParameters>? Parameters { get; init; }

    /// <summary>
    /// Transforms an input rectangle to an output rectangle.
    /// </summary>
    /// <param name="parameters">The parameters to be used to inform the transformation.</param>
    /// <param name="rectangle">The input rectangle to transform to output.</param>
    protected abstract void TransformInputToOutput(TParameters parameters, ref Rectangle64 rectangle);

    /// <summary>
    /// Transforms an output rectangle to an input rectangle.
    /// </summary>
    /// <param name="parameters">The parameters to be used to inform the transformation.</param>
    /// <param name="rectangle">The output rectangle to transform to input.</param>
    protected abstract void TransformOutputToInput(TParameters parameters, ref Rectangle64 rectangle);

    /// <inheritdoc/>
    void ID2D1TransformMapper<T>.MapInputsToOutput(in T shader, ReadOnlySpan<Rectangle> inputs, ReadOnlySpan<Rectangle> opaqueInputs, out Rectangle output, out Rectangle opaqueOutput)
    {
        this.shader = shader;

        if (inputs.IsEmpty)
        {
            Unsafe.SkipInit(out output);

            output.ToD2D1Infinite();
            opaqueOutput = Rectangle.Empty;
        }
        else
        {
            Rectangle64 output64 = Rectangle64.Empty;

            foreach (ref readonly Rectangle input in inputs)
            {
                output64.Union(Rectangle64.FromRectangle(input));
            }

            TransformInputToOutput(Parameters!.Get(in shader), ref output64);

            output = output64.ToRectangleWithD2D1LogicallyInfiniteClamping();
            opaqueOutput = Rectangle.Empty;
        }
    }

    /// <inheritdoc/>
    void ID2D1TransformMapper<T>.MapInvalidOutput(int inputIndex, Rectangle invalidInput, out Rectangle invalidOutput)
    {
        Rectangle64 output64 = Rectangle64.FromRectangle(invalidInput);

        TransformInputToOutput(Parameters!.Get(in shader), ref output64);

        invalidOutput = output64.ToRectangleWithD2D1LogicallyInfiniteClamping();
    }

    /// <inheritdoc/>
    void ID2D1TransformMapper<T>.MapOutputToInputs(in Rectangle output, Span<Rectangle> inputs)
    {
        Rectangle64 output64 = Rectangle64.FromRectangle(output);

        TransformOutputToInput(Parameters!.Get(in shader), ref output64);

        inputs.Fill(output64.ToRectangleWithD2D1LogicallyInfiniteClamping());
    }
}