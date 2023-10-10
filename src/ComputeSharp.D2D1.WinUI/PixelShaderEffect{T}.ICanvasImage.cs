using System.Numerics;
using ABI.Microsoft.Graphics.Canvas;
using ComputeSharp.D2D1.Extensions;
using ComputeSharp.D2D1.WinUI.Helpers;
using ComputeSharp.Interop;
using TerraFX.Interop.Windows;
using Windows.Foundation;
using ICanvasResourceCreator = Microsoft.Graphics.Canvas.ICanvasResourceCreator;

namespace ComputeSharp.D2D1.WinUI;

/// <inheritdoc/>
unsafe partial class PixelShaderEffect<T>
{
    /// <inheritdoc/>
    public Rect GetBounds(ICanvasResourceCreator resourceCreator)
    {
        return GetBounds(resourceCreator, null);
    }

    /// <inheritdoc/>
    public Rect GetBounds(ICanvasResourceCreator resourceCreator, Matrix3x2 transform)
    {
        return GetBounds(resourceCreator, &transform);
    }

    /// <inheritdoc cref="Microsoft.Graphics.Canvas.ICanvasImage.GetBounds(ICanvasResourceCreator, Matrix3x2)"/>
    private Rect GetBounds(ICanvasResourceCreator resourceCreator, Matrix3x2* transform)
    {
        using ReferenceTracker.Lease _0 = GetReferenceTracker().GetLease();

        using ComPtr<ABI.Microsoft.Graphics.Canvas.ICanvasResourceCreator> resourceCreatorAbi = default;

        // Get the ABI.Microsoft.Graphics.Canvas.ICanvasResourceCreator object from the input interface
        RcwMarshaller.GetNativeInterface(resourceCreator, resourceCreatorAbi.GetAddressOf()).Assert();

        using ComPtr<ICanvasImageInterop> canvasImageInterop = default;

        // Get the ICanvasImageInterop object from the current instance
        RcwMarshaller.GetNativeInterface(this, canvasImageInterop.GetAddressOf()).Assert();

        Rect bounds;

        // Forward the actual logic to Win2D to compute the image bounds (it needs the internal context)
        Win2D.GetBoundsForICanvasImageInterop(
            resourceCreator: resourceCreatorAbi.Get(),
            image: canvasImageInterop.Get(),
            transform: transform,
            rect: &bounds).Assert();

        return bounds;
    }
}