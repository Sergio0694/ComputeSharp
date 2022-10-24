using System;
using System.Numerics;
using Microsoft.Graphics.Canvas;
using Windows.Foundation;

namespace ComputeSharp.D2D1.Uwp;

/// <inheritdoc/>
partial class PixelShaderEffect<T>
{
    /// <inheritdoc/>
    Rect ICanvasImage.GetBounds(ICanvasResourceCreator resourceCreator)
    {
        throw new NotSupportedException();
    }

    /// <inheritdoc/>
    Rect ICanvasImage.GetBounds(ICanvasResourceCreator resourceCreator, Matrix3x2 transform)
    {
        throw new NotSupportedException();
    }
}