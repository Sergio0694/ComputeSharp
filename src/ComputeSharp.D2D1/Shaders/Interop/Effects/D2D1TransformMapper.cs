using System;
using System.Drawing;
using System.Runtime.CompilerServices;

#pragma warning disable CS0618

namespace ComputeSharp.D2D1.Interop.Effects;

/// <summary>
/// A base type to support non-generic <see cref="ID2D1TransformMapper{T}"/> uses.
/// </summary>
internal abstract class D2D1TransformMapper
{
    /// <inheritdoc cref="ID2D1TransformMapper{T}.MapInputsToOutput"/>
    /// <param name="buffer">The buffer with the serialized shader dispatch data, if any.</param>
    /// <param name="inputs">The input rectangles to be mapped to the output rectangle. This parameter is always equal to the input bounds.</param>
    /// <param name="opaqueInputs">The input rectangles to be mapped to the opaque output rectangle.</param>
    /// <param name="output">The output rectangle that maps to the corresponding input rectangle.</param>
    /// <param name="opaqueOutput">The output rectangle that maps to the corresponding opaque input rectangle.</param>
    public abstract void MapInputsToOutput(
        ReadOnlySpan<byte> buffer,
        ReadOnlySpan<Rectangle> inputs,
        ReadOnlySpan<Rectangle> opaqueInputs,
        out Rectangle output,
        out Rectangle opaqueOutput);

    /// <inheritdoc cref="ID2D1TransformMapper{T}.MapOutputToInputs"/>
    /// <param name="output">The output rectangle from which the inputs must be mapped.</param>
    /// <param name="inputs">The corresponding set of inputs. The inputs will directly correspond to the transform inputs.</param>
    public abstract void MapOutputToInputs(in Rectangle output, Span<Rectangle> inputs);

    /// <inheritdoc cref="ID2D1TransformMapper{T}.MapInvalidOutput"/>
    /// <param name="inputIndex">The index of the input rectangle.</param>
    /// <param name="invalidInput">The invalid input rectangle.</param>
    /// <param name="invalidOutput">The output rectangle to which the input rectangle must be mapped.</param>
    public abstract void MapInvalidOutput(int inputIndex, Rectangle invalidInput, out Rectangle invalidOutput);

    /// <summary>
    /// A generic <see cref="D2D1TransformMapper"/> implementation for a specific shader type.
    /// </summary>
    /// <typeparam name="T">The target shader type.</typeparam>
    public sealed class For<T> : D2D1TransformMapper
        where T : unmanaged, ID2D1PixelShader
    {
        /// <summary>
        /// The <see cref="ID2D1TransformMapper{T}"/> instance to use.
        /// </summary>
        private readonly ID2D1TransformMapper<T> transformMapper;

        /// <summary>
        /// Creates a new <see cref="For{T}"/> instance with the specified parameters.
        /// </summary>
        /// <param name="transformMapper">The <see cref="ID2D1TransformMapper{T}"/> instance to use.</param>
        private For(ID2D1TransformMapper<T> transformMapper)
        {
            this.transformMapper = transformMapper;
        }

        /// <summary>
        /// Creates a new <see cref="For{T}"/> instance for a given transform mapper, if there is one.
        /// </summary>
        /// <param name="transformMapper">The <see cref="ID2D1TransformMapper{T}"/> instance to usse, if there is one.</param>
        /// <returns>A new <see cref="For{T}"/> instance wrapping <paramref name="transformMapper"/>, or <see langword="null"/>.</returns>
        public static For<T>? Get(ID2D1TransformMapper<T>? transformMapper)
        {
            if (transformMapper is null)
            {
                return null;
            }

            return new(transformMapper);
        }

        /// <inheritdoc/>
        public override void MapInputsToOutput(ReadOnlySpan<byte> buffer, ReadOnlySpan<Rectangle> inputs, ReadOnlySpan<Rectangle> opaqueInputs, out Rectangle output, out Rectangle opaqueOutput)
        {
            Unsafe.SkipInit(out T shader);

            shader.InitializeFromDispatchData(buffer);

            this.transformMapper.MapInputsToOutput(in shader, inputs, opaqueInputs, out output, out opaqueOutput);
        }

        /// <inheritdoc/>
        public override void MapOutputToInputs(in Rectangle output, Span<Rectangle> inputs)
        {
            this.transformMapper.MapOutputToInputs(in output, inputs);
        }

        /// <inheritdoc/>
        public override void MapInvalidOutput(int inputIndex, Rectangle invalidInput, out Rectangle invalidOutput)
        {
            this.transformMapper.MapInvalidOutput(inputIndex, invalidInput, out invalidOutput);
        }
    }
}
