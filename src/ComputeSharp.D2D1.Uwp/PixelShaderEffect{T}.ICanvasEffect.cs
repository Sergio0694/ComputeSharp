using System;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using Windows.Foundation;

namespace ComputeSharp.D2D1.Uwp;

/// <inheritdoc/>
partial class PixelShaderEffect<T>
{
    /// <inheritdoc/>
    void ICanvasEffect.InvalidateSourceRectangle(ICanvasResourceCreatorWithDpi resourceCreator, uint sourceIndex, Rect invalidRectangle)
    {
        throw new NotSupportedException("ICanvasEffect.InvalidateSourceRectangle is not currently supported.");
    }

    /// <inheritdoc/>
    Rect[] ICanvasEffect.GetInvalidRectangles(ICanvasResourceCreatorWithDpi resourceCreator)
    {
        throw new NotSupportedException("ICanvasEffect.GetInvalidRectangles is not currently supported.");
    }

    /// <inheritdoc/>
    Rect ICanvasEffect.GetRequiredSourceRectangle(
        ICanvasResourceCreatorWithDpi resourceCreator,
        Rect outputRectangle,
        ICanvasEffect sourceEffect,
        uint sourceIndex,
        Rect sourceBounds)
    {
        throw new NotSupportedException("ICanvasEffect.GetRequiredSourceRectangle is not currently supported.");
    }

    /// <inheritdoc/>
    Rect[] ICanvasEffect.GetRequiredSourceRectangles(
        ICanvasResourceCreatorWithDpi resourceCreator,
        Rect outputRectangle,
        ICanvasEffect[] sourceEffects,
        uint[] sourceIndices,
        Rect[] sourceBounds)
    {
        throw new NotSupportedException("ICanvasEffect.GetRequiredSourceRectangles is not currently supported.");
    }
}