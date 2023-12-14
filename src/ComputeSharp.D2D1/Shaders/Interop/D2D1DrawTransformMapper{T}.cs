using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.D2D1.Descriptors;
using ComputeSharp.D2D1.Shaders.Interop.Effects.TransformMappers;
using ComputeSharp.Win32;

#pragma warning disable CA1033

namespace ComputeSharp.D2D1.Interop;

/// <summary>
/// Provides an <c>ID2D1DrawTransformMapper</c> implementation, which can be used to customize the draw transform logic in
/// an effect created with <see cref="D2D1PixelShaderEffect"/> and also shared across multiple instances of a given effect.
/// </summary>
/// <typeparam name="T">The type of shader the transform will interact with.</typeparam>
/// <remarks>
/// <para>
/// The built-in <see href="https://docs.microsoft.com/windows/win32/api/d2d1effectauthor/nn-d2d1effectauthor-id2d1effectimpl"><c>ID2D1EffectImpl</c></see>
/// implementation provided by <see cref="D2D1PixelShaderEffect"/> (which makes it possible to register and create
/// <see href="https://docs.microsoft.com/en-us/windows/win32/api/d2d1_1/nn-d2d1_1-id2d1effect"><c>ID2D1Effect</c></see>
/// instances that can be used to run D2D1 pixel shaders) uses draw transform objects to allow customizing the transform logic externally.
/// </para>
/// <para>
/// These managers are COM objects implementing the following interface:
/// <code language="cpp">
/// [uuid(02E6D48D-B892-4FBC-AA54-119203BAB802)]
/// interface ID2D1DrawTransformMapper : IUnknown
/// {
///     HRESULT MapInputRectsToOutputRect(
///         [in]  const ID2D1DrawInfoUpdateContext* updateContext,
///         [in]  const RECT*                       inputRects,
///         [in]  const RECT*                       inputOpaqueSubRects,
///               UINT32                            inputRectCount,
///         [out] RECT*                             outputRect,
///         [out] RECT*                             outputOpaqueSubRect);
/// 
///     HRESULT MapOutputRectToInputRects(
///         [in]  const RECT* outputRect,
///         [out] RECT*       inputRects,
///               UINT32      inputRectsCount);
/// 
///     HRESULT MapInvalidRect(
///               UINT32 inputIndex,
///               RECT   invalidInputRect,
///         [out] RECT*  invalidOutputRect);
/// };
/// </code>
/// </para>
/// <para>
/// These can be used to implement custom draw transform logic, assign it to an effect and also share these transforms over multiple effects.
/// </para>
/// <para>
/// For details of how these work, <c>MapInputRectsToOutputRect</c> maps to
/// <see href="https://learn.microsoft.com/windows/win32/api/d2d1effectauthor/nf-d2d1effectauthor-id2d1transform-mapinputrectstooutputrect"><c>ID2D1Transform::MapInputRectsToOutputRect</c></see>,
/// <c>MapOutputRectToInputRects</c> maps to <see href="https://learn.microsoft.com/windows/win32/api/d2d1effectauthor/nf-d2d1effectauthor-id2d1transform-mapoutputrecttoinputrects"><c>ID2D1Transform::MapOutputRectToInputRects</c></see>.
/// and <c>MapInvalidRect</c> maps to <see href="https://learn.microsoft.com/windows/win32/api/d2d1effectauthor/nf-d2d1effectauthor-id2d1transform-mapinvalidrect"><c>ID2D1Transform::MapInvalidRect</c></see>.
/// </para>
/// <para>
/// The main difference between these APIs and the ones in <c>ID2D1Transform</c> is the fact that this interface is standalone (ie. it doesn't inherit from <c>ID2D1TransformNode</c>),
/// and that it allows transform mappers to also read and update additional data tied to an effect instance (such as the shader constant buffer). This is done through the
/// <c>ID2D1DrawInfoUpdateContext</c> interface, that is passed to <c>MapInputRectsToOutputRect</c>. This interface is defined as follows:
/// <code>
/// [uuid(430C5B40-AE16-485F-90E6-4FA4915144B6)]
/// interface ID2D1DrawInfoUpdateContext : IUnknown
/// {
///     HRESULT GetConstantBufferSize([out] UINT32 *size);
/// 
///     HRESULT GetConstantBuffer(
///         [out] BYTE   *buffer,
///               UINT32 bufferCount);
/// 
///     HRESULT SetConstantBuffer(
///         [in] const BYTE *buffer,
///              UINT32     bufferCount);
/// }
/// </code>
/// </para>
/// <para>
/// That is, <c>ID2D1DrawInfoUpdateContext</c> allows a custom transform (an <c>ID2D1DrawTransformMapper</c> instance) to interact with the underlying <c>ID2D1DrawInfo</c> object
/// that is owned by the effect being used, in a safe way. For instance, it allows a transform to read and update the constant buffer, which can be used to allow a transform to
/// pass the exact dispatch area size to an input shader, without the consumer having to manually query that information beforehand (which might not be available either).
/// </para>
/// <para>
/// This interface is implemented by ComputeSharp.D2D1, and it can be used through the APIs in <see cref="D2D1DrawTransformMapper{T}"/>, in several ways.
/// That is, consumers can either implement a type inheriting from <see cref="D2D1DrawTransformMapper{T}"/> to implement their own fully customized transform
/// mapping logic, or they can use the helper methods exposed by <see cref="D2D1DrawTransformMapper{T}"/> to easily retrieve ready to use transforms.
/// </para>
/// <para>
/// A CCW (COM callable wrapper, see <see href="https://learn.microsoft.com/dotnet/standard/native-interop/com-callable-wrapper"/>) is also available for all
/// of these APIs, implemented via the same <see cref="D2D1DrawTransformMapper{T}"/> type. That is, a given instance can expose its underlying CCW through the
/// <see cref="ICustomQueryInterface"/> interface, and this can then be passed to an existing D2D1 effect instance.
/// </para>
/// </remarks>
public abstract unsafe partial class D2D1DrawTransformMapper<T> : ICustomQueryInterface, ID2D1DrawTransformMapperInterop
    where T : unmanaged, ID2D1PixelShader, ID2D1PixelShaderDescriptor<T>
{
    /// <summary>
    /// The <see cref="D2D1DrawTransformMapperImpl"/> object wrapped by the current instance.
    /// </summary>
    private ComPtr<D2D1DrawTransformMapperImpl> d2D1TransformMapperImpl;

    /// <summary>
    /// Creates a new <see cref="D2D1DrawTransformMapper{T}"/> instance.
    /// </summary>
    public D2D1DrawTransformMapper()
    {
        fixed (D2D1DrawTransformMapperImpl** d2D1TransformMapperImpl = this.d2D1TransformMapperImpl)
        {
            D2D1DrawTransformMapperImpl.Factory(this, d2D1TransformMapperImpl);
        }
    }

    /// <summary>
    /// Releases the underlying <c>ID2D1DrawTransformMapper</c> object.
    /// </summary>
    ~D2D1DrawTransformMapper()
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
    /// <param name="drawInfoUpdateContext">The input <see cref="D2D1DrawInfoUpdateContext{T}"/> value, which can be used to access and modify shader data.</param>
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
    public abstract void MapInputsToOutput(
        D2D1DrawInfoUpdateContext<T> drawInfoUpdateContext,
        ReadOnlySpan<Rectangle> inputs,
        ReadOnlySpan<Rectangle> opaqueInputs,
        out Rectangle output,
        out Rectangle opaqueOutput);

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
    public abstract void MapOutputToInputs(in Rectangle output, Span<Rectangle> inputs);

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
    /// its own state in response to a call to <see cref="MapInvalidOutput"/>. Direct2D can call this method at any time
    /// and in any sequence following a call to <see cref="MapInputsToOutput"/>.
    /// </para>
    /// <para>
    /// For more info, see <see href="https://docs.microsoft.com/windows/win32/api/d2d1effectauthor/nf-d2d1effectauthor-id2d1transform-mapinvalidrect"/>.
    /// </para>
    /// </remarks>
    public abstract void MapInvalidOutput(int inputIndex, in Rectangle invalidInput, out Rectangle invalidOutput);

    /// <summary>
    /// Gets the underlying <see cref="ID2D1DrawTransformMapper"/> object.
    /// </summary>
    /// <param name="transformMapper">The underlying <see cref="ID2D1DrawTransformMapper"/> object.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void GetD2D1TransformMapper(ID2D1DrawTransformMapper** transformMapper)
    {
        bool lockTaken = false;

        this.d2D1TransformMapperImpl.Get()->SpinLock.Enter(ref lockTaken);

        // Whenever the CCW is requested from this object, we also make sure that the current instance is tracked
        // in the GC handle stored in the CCW. This could've been reset in case there were no other references to
        // the CCW (see comments in D2D1TransformMapperImpl about this), so here we're assigning a reference to the
        // current object again to ensure the returned CCW object will keep the instance alive if needed.
        try
        {
            this.d2D1TransformMapperImpl.Get()->EnsureTargetIsTracked(this);
            this.d2D1TransformMapperImpl.Get()->CopyToWithNoLock(transformMapper);
        }
        finally
        {
            this.d2D1TransformMapperImpl.Get()->SpinLock.Exit();

            GC.KeepAlive(this);
        }
    }

    /// <inheritdoc/>
    CustomQueryInterfaceResult ICustomQueryInterface.GetInterface(ref Guid iid, out IntPtr ppv)
    {
        fixed (Guid* pIid = &iid)
        fixed (IntPtr* pPpv = &ppv)
        {
            int hresult;
            bool lockTaken = false;

            this.d2D1TransformMapperImpl.Get()->SpinLock.Enter(ref lockTaken);

            try
            {
                this.d2D1TransformMapperImpl.Get()->EnsureTargetIsTracked(this);

                hresult = this.d2D1TransformMapperImpl.Get()->QueryInterfaceWithNoLock(pIid, (void**)pPpv);
            }
            finally
            {
                this.d2D1TransformMapperImpl.Get()->SpinLock.Exit();

                GC.KeepAlive(this);
            }

            return hresult switch
            {
                S.S_OK => CustomQueryInterfaceResult.Handled,
                _ => CustomQueryInterfaceResult.Failed
            };
        }
    }

    /// <inheritdoc/>
    void ID2D1DrawTransformMapperInterop.MapInputsToOutput(
        ID2D1DrawInfoUpdateContext* d2D1DrawInfoUpdateContext,
        ReadOnlySpan<Rectangle> inputs,
        ReadOnlySpan<Rectangle> opaqueInputs,
        out Rectangle output,
        out Rectangle opaqueOutput)
    {
        MapInputsToOutput(
            new D2D1DrawInfoUpdateContext<T>(d2D1DrawInfoUpdateContext),
            inputs,
            opaqueInputs,
            out output,
            out opaqueOutput);
    }

    /// <inheritdoc/>
    void ID2D1DrawTransformMapperInterop.MapOutputToInputs(in Rectangle output, Span<Rectangle> inputs)
    {
        MapOutputToInputs(in output, inputs);
    }

    /// <inheritdoc/>
    void ID2D1DrawTransformMapperInterop.MapInvalidOutput(int inputIndex, in Rectangle invalidInput, out Rectangle invalidOutput)
    {
        MapInvalidOutput(inputIndex, in invalidInput, out invalidOutput);
    }
}