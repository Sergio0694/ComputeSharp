using System.Numerics;
using System.Runtime.InteropServices;
using ABI.Microsoft.Graphics.Canvas.Effects;
using ComputeSharp.Win32;
using Windows.Foundation;

namespace ABI.Microsoft.Graphics.Canvas;

/// <summary>
/// Native APIs from <c>Microsoft.Graphics.Canvas</c> (ie. Win2D).
/// </summary>
internal static unsafe class Win2D
{
    /// <summary>
    /// Calculates the bounds for a given <see cref="ICanvasImageInterop"/> instance.
    /// </summary>
    /// <param name="resourceCreator">The <see cref="ICanvasResourceCreator"/> object in use.</param>
    /// <param name="image">The input <see cref="ICanvasImageInterop"/> instance to compute the bounds for.</param>
    /// <param name="transform">The transform to use (optional).</param>
    /// <param name="rect">The resulting bounds.</param>
    /// <returns>The <see cref="HRESULT"/> for the operation.</returns>
    [DllImport("Microsoft.Graphics.Canvas.dll", PreserveSig = true, ExactSpelling = true)]
    [return: NativeTypeName("HRESULT")]
    public static extern int GetBoundsForICanvasImageInterop(
        ICanvasResourceCreator* resourceCreator,
        ICanvasImageInterop* image,
        [NativeTypeName("Windows::Foundation::Numerics::Matrix3x2 const*")] Matrix3x2* transform,
        Rect* rect);

    /// <summary>
    /// Implements support for <see cref="global::Microsoft.Graphics.Canvas.Effects.ICanvasEffect.InvalidateSourceRectangle"/> for an external <see cref="ICanvasImageInterop"/> instance.
    /// </summary>
    /// <param name="resourceCreator">The input <see cref="ICanvasResourceCreatorWithDpi"/> instance to use to perform the operation.</param>
    /// <param name="image">The target <see cref="ICanvasImageInterop"/> instance to execute the operation upon.</param>
    /// <param name="sourceIndex">The input index to invalidate.</param>
    /// <param name="invalidRectangle">The rectangle to invalidate.</param>
    /// <returns>The <see cref="HRESULT"/> for the operation.</returns>
    [DllImport("Microsoft.Graphics.Canvas.dll", PreserveSig = true, ExactSpelling = true)]
    [return: NativeTypeName("HRESULT")]
    public static extern int InvalidateSourceRectangleForICanvasImageInterop(
        ICanvasResourceCreatorWithDpi* resourceCreator,
        ICanvasImageInterop* image,
        uint sourceIndex,
        [NativeTypeName("Windows::Foundation::Rect const*")] Rect* invalidRectangle);

    /// <summary>
    /// Implements support for <see cref="global::Microsoft.Graphics.Canvas.Effects.ICanvasEffect.GetInvalidRectangles"/> for an external <see cref="ICanvasImageInterop"/> instance.
    /// </summary>
    /// <param name="resourceCreator">The input <see cref="ICanvasResourceCreatorWithDpi"/> instance to use to perform the operation.</param>
    /// <param name="image">The target <see cref="ICanvasImageInterop"/> instance to execute the operation upon.</param>
    /// <param name="valueCount">The resulting length of the COM array returned via <paramref name="valueElements"/>.</param>
    /// <param name="valueElements">The COM array with the sequence of invalid rectangles for the effect.</param>
    /// <returns>The <see cref="HRESULT"/> for the operation.</returns>
    [DllImport("Microsoft.Graphics.Canvas.dll", PreserveSig = true, ExactSpelling = true)]
    [return: NativeTypeName("HRESULT")]
    public static extern int GetInvalidRectanglesForICanvasImageInterop(
        ICanvasResourceCreatorWithDpi* resourceCreator,
        ICanvasImageInterop* image,
        uint* valueCount,
        Rect** valueElements);

    /// <summary>
    /// Implements support for <see cref="global::Microsoft.Graphics.Canvas.Effects.ICanvasEffect.GetRequiredSourceRectangle"/> and
    /// <see cref="global::Microsoft.Graphics.Canvas.Effects.ICanvasEffect.GetRequiredSourceRectangles"/> for an external <see cref="ICanvasImageInterop"/> instance.
    /// </summary>
    /// <param name="resourceCreator">The input <see cref="ICanvasResourceCreatorWithDpi"/> instance to use to perform the operation.</param>
    /// <param name="image">The target <see cref="ICanvasImageInterop"/> instance to execute the operation upon.</param>
    /// <param name="outputRectangle">The portion of the output image whose inputs are being inspected.</param>
    /// <param name="sourceEffectCount">The length of the sequence of input effects.</param>
    /// <param name="sourceEffects">The sequence of input effects whose rectangles are being queries.</param>
    /// <param name="sourceIndexCount">The length of the sequence of indices for the input effects.</param>
    /// <param name="sourceIndices">The sequence of indices for the input effects.</param>
    /// <param name="sourceBoundsCount">The length of the sequence of bounds to consider.</param>
    /// <param name="sourceBounds">The sequence of bounds to consider in the resulting output.</param>
    /// <param name="valueCount">The number of elements provided in <paramref name="valueElements"/>.</param>
    /// <param name="valueElements">The sequence of resulting required rectangles to compute.</param>
    /// <returns>The <see cref="HRESULT"/> for the operation.</returns>
    [DllImport("Microsoft.Graphics.Canvas.dll", PreserveSig = true, ExactSpelling = true)]
    [return: NativeTypeName("HRESULT")]
    public static extern int GetRequiredSourceRectanglesForICanvasImageInterop(
        ICanvasResourceCreatorWithDpi* resourceCreator,
        ICanvasImageInterop* image,
        [NativeTypeName("Windows::Foundation::Rect const*")] Rect* outputRectangle,
        uint sourceEffectCount,
        [NativeTypeName("ABI::Microsoft::Graphics::Canvas::Effects::ICanvasEffect* const*")] ICanvasEffect** sourceEffects,
        uint sourceIndexCount,
        [NativeTypeName("uint32_t const*")] uint* sourceIndices,
        uint sourceBoundsCount,
        [NativeTypeName("Windows::Foundation::Rect const*")] Rect* sourceBounds,
        uint valueCount,
        Rect* valueElements);
}