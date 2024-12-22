using System;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using ABI.Microsoft.Graphics.Canvas;
using ComputeSharp.D2D1.Extensions;
#if WINDOWS_UWP
using ComputeSharp.D2D1.Uwp.Helpers;
#else
using ComputeSharp.D2D1.WinUI.Helpers;
#endif
using ComputeSharp.Win32;
using Microsoft.Graphics.Canvas;
using Windows.Foundation;
using ICanvasImageInterop = Microsoft.Graphics.Canvas.ICanvasImageInterop;
using ICanvasResourceCreator = Microsoft.Graphics.Canvas.ICanvasResourceCreator;

#if WINDOWS_UWP
namespace ComputeSharp.D2D1.Uwp;
#else
namespace ComputeSharp.D2D1.WinUI;
#endif

/// <inheritdoc/>
partial class CanvasEffect
{
    /// <inheritdoc/>
    public Rect GetBounds(ICanvasResourceCreator resourceCreator)
    {
        return GetCanvasImage().GetBounds(resourceCreator);
    }

    /// <inheritdoc/>
    public Rect GetBounds(ICanvasResourceCreator resourceCreator, Matrix3x2 transform)
    {
        return GetCanvasImage().GetBounds(resourceCreator, transform);
    }

    /// <inheritdoc/>
    unsafe int ICanvasImageInterop.GetDevice(ICanvasDevice** device, WIN2D_GET_DEVICE_ASSOCIATION_TYPE* type)
    {
        using ComPtr<ABI.Microsoft.Graphics.Canvas.ICanvasImageInterop> canvasImageInterop = default;

        RcwMarshaller.GetNativeInterface(GetCanvasImage(), canvasImageInterop.GetAddressOf()).Assert();

        return canvasImageInterop.Get()->GetDevice(device, type);
    }

    /// <inheritdoc/>
    unsafe int ICanvasImageInterop.GetD2DImage(
        ICanvasDevice* device,
        ID2D1DeviceContext* deviceContext,
        WIN2D_GET_D2D_IMAGE_FLAGS flags,
        float targetDpi,
        float* realizeDpi,
        ID2D1Image** ppImage)
    {
        using ComPtr<ABI.Microsoft.Graphics.Canvas.ICanvasImageInterop> canvasImageInterop = default;

        RcwMarshaller.GetNativeInterface(GetCanvasImage(), canvasImageInterop.GetAddressOf()).Assert();

        return canvasImageInterop.Get()->GetD2DImage(device, deviceContext, flags, targetDpi, realizeDpi, ppImage);
    }

    /// <summary>
    /// Gets or creates the current <see cref="ICanvasImage"/> instance.
    /// </summary>
    /// <returns>The current <see cref="ICanvasImage"/> instance.</returns>
    /// <exception cref="ObjectDisposedException">Thrown if the current instance is disposed.</exception>
    [MemberNotNull(nameof(CanvasImage))]
    private ICanvasImage GetCanvasImage()
    {
        lock (this.TransformNodes)
        {
            default(ObjectDisposedException).ThrowIf(this.isDisposed, this);

            // Build the effect graph (the output node might not have been set yet)
            if (this.InvalidationType == CanvasEffectInvalidationType.Creation)
            {
                DisposeEffectGraph();
                BuildEffectGraph(new CanvasEffectGraph(this));

                // We successfully got past the effect graph creation, so update the current
                // invalidation state. This ensures that even if the configuration failed, the
                // next time the effect is drawn again the graph won't be created again too.
                this.InvalidationType = CanvasEffectInvalidationType.Update;
            }

            // Configure the effect graph, if the effect is invalidated
            if (this.InvalidationType == CanvasEffectInvalidationType.Update)
            {
                ConfigureEffectGraph(new CanvasEffectGraph(this));

                // The effect graph is now ready to go: no further work will be done before drawing
                // unless it is explicitly invalidated again, using either creation or update mode.
                this.InvalidationType = default;
            }

            // At this point, there must be an output canvas image being set.
            // If not, it means a derived type has forgot to set an output node.
            default(InvalidOperationException).ThrowIf(this.CanvasImage is null, "No output node is set in the effect graph.");

            return this.CanvasImage;
        }
    }

    /// <summary>
    /// Disposes and clears the current effect graph, if any.
    /// </summary>
    private void DisposeEffectGraph()
    {
        // Dispose all registered canvas images
        foreach (ICanvasImage canvasImage in this.TransformNodes.Values)
        {
            canvasImage.Dispose();
        }

        // Also clear the current effect graph. Note that the canvas image used as output
        // node for the effect graph does not need to be explicitly disposed here, as that
        // object is guaranteed to have been part of the dictionary of transform nodes.
        this.TransformNodes.Clear();
        this.CanvasImage = null;
    }
}