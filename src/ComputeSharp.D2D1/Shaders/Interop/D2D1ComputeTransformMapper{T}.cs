using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.D2D1.Shaders.Interop.Effects.TransformMappers;
using ComputeSharp.D2D1.Shaders.Interop.Helpers;
using TerraFX.Interop.Windows;

#pragma warning disable CA1033

namespace ComputeSharp.D2D1.Interop;

/// <summary>
/// Provides an <c>ID2D1TransformMapper</c> implementation, which can be used to customize the draw transform logic in
/// an effect created with <see cref="D2D1ComputeShaderEffect"/> and also shared across multiple instances of a given effect.
/// </summary>
/// <typeparam name="T">The type of shader the transform will interact with.</typeparam>
/// <remarks>
/// The underlying implementation for this class is analogous to that of <see cref="D2D1DrawTransformMapper{T}"/>, see remarks there.
/// </remarks>
public abstract unsafe partial class D2D1ComputeTransformMapper<T> : ICustomQueryInterface, ID2D1TransformMapperInterop
    where T : unmanaged, ID2D1ComputeShader
{
    /// <summary>
    /// The <see cref="D2D1TransformMapperImpl"/> object wrapped by the current instance.
    /// </summary>
    private ComPtr<D2D1TransformMapperImpl> d2D1TransformMapperImpl;

    /// <summary>
    /// Creates a new <see cref="D2D1DrawTransformMapper{T}"/> instance.
    /// </summary>
    public D2D1ComputeTransformMapper()
    {
        fixed (D2D1TransformMapperImpl** d2D1TransformMapperImpl = this.d2D1TransformMapperImpl)
        {
            D2D1TransformMapperImpl.Factory(this, d2D1TransformMapperImpl);
        }
    }

    /// <summary>
    /// Releases the underlying <c>ID2D1TransformMapper</c> object.
    /// </summary>
    ~D2D1ComputeTransformMapper()
    {
        this.d2D1TransformMapperImpl.Dispose();
    }

    /// <summary>
    /// <para>
    /// Allows a transform to state how it would map a a set of sample rectangles on its input to an output rectangle.
    /// </para>
    /// <para>
    /// This is effectively the inverse mapping of <see cref="MapOutputToInputs"/>.
    /// </para>
    /// </summary>
    /// <param name="computeInfoUpdateContext">The input <see cref="D2D1ComputeInfoUpdateContext{T}"/> value, which can be used to access and modify shader data.</param>
    /// <param name="inputs"><inheritdoc cref="D2D1DrawTransformMapper{T}.MapInputsToOutput" path="/param[@name='inputs']/node()"/></param>
    /// <param name="opaqueInputs"><inheritdoc cref="D2D1DrawTransformMapper{T}.MapInputsToOutput" path="/param[@name='opaqueInputs']/node()"/></param>
    /// <param name="output"><inheritdoc cref="D2D1DrawTransformMapper{T}.MapInputsToOutput" path="/param[@name='output']/node()"/></param>
    /// <param name="opaqueOutput"><inheritdoc cref="D2D1DrawTransformMapper{T}.MapInputsToOutput" path="/param[@name='opaqueOutput']/node()"/></param>
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
    public abstract void MapInputsToOutput(
        D2D1ComputeInfoUpdateContext<T> computeInfoUpdateContext,
        ReadOnlySpan<Rectangle> inputs,
        ReadOnlySpan<Rectangle> opaqueInputs,
        out Rectangle output,
        out Rectangle opaqueOutput);

    /// <inheritdoc cref="D2D1DrawTransformMapper{T}.MapOutputToInputs"/>
    public abstract void MapOutputToInputs(in Rectangle output, Span<Rectangle> inputs);

    /// <summary><inheritdoc cref="D2D1DrawTransformMapper{T}.MapInvalidOutput" path="/summary/node()"/></summary>
    /// <param name="inputIndex"><inheritdoc cref="D2D1DrawTransformMapper{T}.MapInvalidOutput" path="/param[@name='inputIndex']/node()"/></param>
    /// <param name="invalidInput"><inheritdoc cref="D2D1DrawTransformMapper{T}.MapInvalidOutput" path="/param[@name='invalidInput']/node()"/></param>
    /// <param name="invalidOutput"><inheritdoc cref="D2D1DrawTransformMapper{T}.MapInvalidOutput" path="/param[@name='invalidOutput']/node()"/></param>
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
    public abstract void MapInvalidOutput(int inputIndex, in Rectangle invalidInput, out Rectangle invalidOutput);

    /// <inheritdoc cref="D2D1DrawTransformMapper{T}.GetD2D1TransformMapper"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void GetD2D1TransformMapper(ID2D1TransformMapper** transformMapper)
    {
        D2D1TransformMapper.GetD2D1TransformMapper(this.d2D1TransformMapperImpl.Get(), this, transformMapper);
    }

    /// <inheritdoc/>
    CustomQueryInterfaceResult ICustomQueryInterface.GetInterface(ref Guid iid, out IntPtr ppv)
    {
        return D2D1TransformMapper.GetInterface(this.d2D1TransformMapperImpl.Get(), this, ref iid, out ppv);
    }

    /// <inheritdoc/>
    void ID2D1TransformMapperInterop.MapInputsToOutput(
        ID2D1RenderInfoUpdateContext* d2D1DrawInfoUpdateContext,
        ReadOnlySpan<Rectangle> inputs,
        ReadOnlySpan<Rectangle> opaqueInputs,
        out Rectangle output,
        out Rectangle opaqueOutput)
    {
        MapInputsToOutput(
            new D2D1ComputeInfoUpdateContext<T>(d2D1DrawInfoUpdateContext),
            inputs,
            opaqueInputs,
            out output,
            out opaqueOutput);
    }

    /// <inheritdoc/>
    void ID2D1TransformMapperInterop.MapOutputToInputs(in Rectangle output, Span<Rectangle> inputs)
    {
        MapOutputToInputs(in output, inputs);
    }

    /// <inheritdoc/>
    void ID2D1TransformMapperInterop.MapInvalidOutput(int inputIndex, in Rectangle invalidInput, out Rectangle invalidOutput)
    {
        MapInvalidOutput(inputIndex, in invalidInput, out invalidOutput);
    }
}