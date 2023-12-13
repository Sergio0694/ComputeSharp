using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.Win32;

#pragma warning disable CS0649, IDE0055

namespace ComputeSharp.D2D1.Shaders.Interop.Effects.TransformMappers;

/// <summary>
/// The transform mapper manager type to use with built-in effects.
/// </summary>
internal unsafe struct ID2D1DrawTransformMapper : IComObject
{
    /// <inheritdoc/>
    static Guid* IComObject.IID
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            ReadOnlySpan<byte> data =
            [
                0x8D, 0xD4, 0xE6, 0x02,
                0x92, 0xB8,
                0xBC, 0x4F,
                0xAA, 0x54,
                0x11,
                0x92,
                0x03,
                0xBA,
                0xB8,
                0x02
            ];

            return (Guid*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(data));
        }
    }

    /// <summary>
    /// The vtable for the current instance.
    /// </summary>
    private readonly void** lpVtbl;

    /// <summary>
    /// Performs the inverse mapping to <see cref="MapOutputRectToInputRects(RECT*, RECT*, uint)"/>.
    /// </summary>
    /// <param name="updateContext">The input <see cref="ID2D1DrawInfoUpdateContext"/> instance.</param>
    /// <param name="inputRects">An array of input rectangles to be mapped to the output rectangle.</param>
    /// <param name="inputOpaqueSubRects">An array of input rectangles to be mapped to the opaque output rectangle.</param>
    /// <param name="inputRectCount">The number of inputs specified.</param>
    /// <param name="outputRect">The output rectangle that maps to the corresponding input rectangle.</param>
    /// <param name="outputOpaqueSubRect">The output rectangle that maps to the corresponding opaque input rectangle.</param>
    /// <returns>An <see cref="HRESULT"/> for the operation.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public HRESULT MapInputRectsToOutputRect(
        ID2D1DrawInfoUpdateContext* updateContext,
        RECT* inputRects,
        RECT* inputOpaqueSubRects,
        uint inputRectCount,
        RECT* outputRect,
        RECT* outputOpaqueSubRect)
    {
        return ((delegate* unmanaged[MemberFunction]<ID2D1DrawTransformMapper*, ID2D1DrawInfoUpdateContext*, RECT*, RECT*, uint, RECT*, RECT*, int>)this.lpVtbl[3])(
            (ID2D1DrawTransformMapper*)Unsafe.AsPointer(ref this),
            updateContext,
            inputRects,
            inputOpaqueSubRects,
            inputRectCount,
            outputRect,
            outputOpaqueSubRect);
    }

    /// <summary>
    /// Allows a transform to state how it would map a rectangle requested on its output to a set of sample rectangles on its input.
    /// </summary>
    /// <param name="outputRect">The output rectangle from which the inputs must be mapped.</param>
    /// <param name="inputRects">The corresponding set of inputs. The inputs will directly correspond to the transform inputs.</param>
    /// <param name="inputRectsCount">The number of inputs specified.</param>
    /// <returns>An <see cref="HRESULT"/> for the operation.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public HRESULT MapOutputRectToInputRects(
        RECT* outputRect,
        RECT* inputRects,
        uint inputRectsCount)
    {
        return ((delegate* unmanaged[MemberFunction]<ID2D1DrawTransformMapper*, RECT*, RECT*, uint, int>)this.lpVtbl[4])(
            (ID2D1DrawTransformMapper*)Unsafe.AsPointer(ref this),
            outputRect,
            inputRects,
            inputRectsCount);
    }

    /// <summary>
    /// Sets the input rectangles for this rendering pass into the transform.
    /// </summary>
    /// <param name="inputIndex">The index of the input rectangle.</param>
    /// <param name="invalidInputRect">The invalid input rectangle.</param>
    /// <param name="invalidOutputRect">The output rectangle to which the input rectangle must be mapped.</param>
    /// <returns>An <see cref="HRESULT"/> for the operation.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public HRESULT MapInvalidRect(
        uint inputIndex,
        RECT invalidInputRect,
        RECT* invalidOutputRect)
    {
        return ((delegate* unmanaged[MemberFunction]<ID2D1DrawTransformMapper*, uint, RECT, RECT*, int>)this.lpVtbl[5])(
            (ID2D1DrawTransformMapper*)Unsafe.AsPointer(ref this),
            inputIndex,
            invalidInputRect,
            invalidOutputRect);
    }
}