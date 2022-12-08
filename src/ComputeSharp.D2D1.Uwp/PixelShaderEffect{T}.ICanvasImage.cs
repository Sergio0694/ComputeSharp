using System.Numerics;
using System.Runtime.InteropServices;
using ABI.Microsoft.Graphics.Canvas;
using ComputeSharp.D2D1.Extensions;
using ComputeSharp.Interop;
using Microsoft.Graphics.Canvas;
using TerraFX.Interop.Windows;
using Windows.Foundation;
using ICanvasResourceCreator = Microsoft.Graphics.Canvas.ICanvasResourceCreator;

namespace ComputeSharp.D2D1.Uwp;

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

    /// <inheritdoc cref="ICanvasImage.GetBounds(ICanvasResourceCreator, Matrix3x2)"/>
    private Rect GetBounds(ICanvasResourceCreator resourceCreator, Matrix3x2* transform)
    {
        using ReferenceTracker.Lease _0 = GetReferenceTracker().GetLease();

        using ComPtr<IUnknown> canvasResourceCreatorUnknown = default;
        using ComPtr<IUnknown> canvasImageInteropUnknown = default;

        // Get the IUnknown* references for the ICanvasResourceCreator and ICanvasImageInterop objects
        canvasResourceCreatorUnknown.Attach((IUnknown*)Marshal.GetIUnknownForObject(resourceCreator));
        canvasImageInteropUnknown.Attach((IUnknown*)Marshal.GetIUnknownForObject(this));

        using ComPtr<ABI.Microsoft.Graphics.Canvas.ICanvasResourceCreator> canvasResourceCreator = default;
        using ComPtr<ICanvasImageInterop> canvasImageInterop = default;

        // Get the ICanvasResourceCreator* and ICanvasImageInterop* pointers
        canvasResourceCreatorUnknown.CopyTo(canvasResourceCreator.GetAddressOf()).Assert();
        canvasImageInteropUnknown.CopyTo(canvasImageInterop.GetAddressOf()).Assert();

        Rect bounds;

        // Forward the actual logic to Win2D to compute the image bounds (it needs the internal context)
        Win2D.GetBoundsForICanvasImageInterop(
            resourceCreator: canvasResourceCreator.Get(),
            image: canvasImageInterop.Get(),
            transform: transform,
            rect: &bounds).Assert();

        return bounds;
    }
}