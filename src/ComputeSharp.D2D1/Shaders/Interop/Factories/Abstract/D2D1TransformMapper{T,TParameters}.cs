using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using ComputeSharp.D2D1.Descriptors;
using ComputeSharp.D2D1.Interop;
using ComputeSharp.D2D1.Shaders.Interop.Helpers;

namespace ComputeSharp.D2D1.Shaders.Interop.Factories.Abstract;

/// <summary>
/// A base type for a transform mapper to be used in a D2D1 pixel shader effect.
/// </summary>
/// <typeparam name="T">The type of D2D1 pixel shader associated to the transform mapper.</typeparam>
/// <typeparam name="TParameters">The type of parameters that the transform mapper will use.</typeparam>
internal abstract class D2D1DrawTransformMapper<T, TParameters> : D2D1DrawTransformMapper<T>
    where T : unmanaged, ID2D1PixelShader, ID2D1PixelShaderDescriptor<T>
    where TParameters : unmanaged
{
    /// <summary>
    /// The parameters to use in this transform mapper.
    /// </summary>
    private TParameters parameters;

    /// <inheritdoc/>
    public sealed override void MapInputsToOutput(
        D2D1DrawInfoUpdateContext<T> drawInfoUpdateContext,
        ReadOnlySpan<Rectangle> inputs,
        ReadOnlySpan<Rectangle> opaqueInputs,
        out Rectangle output,
        out Rectangle opaqueOutput)
    {
        this.parameters = GetParameters(drawInfoUpdateContext);

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

            TransformInputToOutput(in this.parameters, ref output64);

            output = output64.ToRectangleWithD2D1LogicallyInfiniteClamping();
            opaqueOutput = Rectangle.Empty;
        }
    }

    /// <inheritdoc/>
    public sealed override void MapOutputToInputs(in Rectangle output, Span<Rectangle> inputs)
    {
        Rectangle64 output64 = Rectangle64.FromRectangle(output);

        TransformOutputToInput(in this.parameters, ref output64);

        inputs.Fill(output64.ToRectangleWithD2D1LogicallyInfiniteClamping());
    }

    /// <inheritdoc/>
    public sealed override void MapInvalidOutput(int inputIndex, in Rectangle invalidInput, out Rectangle invalidOutput)
    {
        Rectangle64 output64 = Rectangle64.FromRectangle(invalidInput);

        TransformInputToOutput(in this.parameters, ref output64);

        invalidOutput = output64.ToRectangleWithD2D1LogicallyInfiniteClamping();
    }

    /// <summary>
    /// Gets the <typeparamref name="TParameters"/> values for the current transform mapper.
    /// </summary>
    /// <param name="drawInfoUpdateContext">The input <see cref="D2D1DrawInfoUpdateContext{T}"/> value, which can be used to access and modify shader data.</param>
    protected abstract TParameters GetParameters(D2D1DrawInfoUpdateContext<T> drawInfoUpdateContext);

    /// <summary>
    /// Transforms an input rectangle to an output rectangle.
    /// </summary>
    /// <param name="parameters">The parameters to be used to inform the transformation.</param>
    /// <param name="rectangle">The input rectangle to transform to output.</param>
    protected abstract void TransformInputToOutput(in TParameters parameters, ref Rectangle64 rectangle);

    /// <summary>
    /// Transforms an output rectangle to an input rectangle.
    /// </summary>
    /// <param name="parameters">The parameters to be used to inform the transformation.</param>
    /// <param name="rectangle">The output rectangle to transform to input.</param>
    protected abstract void TransformOutputToInput(in TParameters parameters, ref Rectangle64 rectangle);
}