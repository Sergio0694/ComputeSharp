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
using Microsoft.Graphics.Canvas;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using Windows.Foundation;
using ICanvasResourceCreator = Microsoft.Graphics.Canvas.ICanvasResourceCreator;

#if WINDOWS_UWP
namespace ComputeSharp.D2D1.Uwp;
#else
namespace ComputeSharp.D2D1.WinUI;
#endif

/// <inheritdoc/>
unsafe partial class CanvasEffect
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
    unsafe int ICanvasImageInterop.Interface.GetDevice(ICanvasDevice** device, WIN2D_GET_DEVICE_ASSOCIATION_TYPE* type)
    {
        using ComPtr<ICanvasImageInterop> canvasImageInterop = default;

        RcwMarshaller.GetNativeInterface(GetCanvasImage(), canvasImageInterop.GetAddressOf()).Assert();

        return canvasImageInterop.Get()->GetDevice(device, type);
    }

    /// <inheritdoc/>
    unsafe int ICanvasImageInterop.Interface.GetD2DImage(
        ICanvasDevice* device,
        ID2D1DeviceContext* deviceContext,
        WIN2D_GET_D2D_IMAGE_FLAGS flags,
        float targetDpi,
        float* realizeDpi,
        ID2D1Image** ppImage)
    {
        using ComPtr<ICanvasImageInterop> canvasImageInterop = default;

        RcwMarshaller.GetNativeInterface(GetCanvasImage(), canvasImageInterop.GetAddressOf()).Assert();

        return canvasImageInterop.Get()->GetD2DImage(device, deviceContext, flags, targetDpi, realizeDpi, ppImage);
    }

    /// <summary>
    /// Gets or creates the current <see cref="ICanvasImage"/> instance.
    /// </summary>
    /// <returns>The current <see cref="ICanvasImage"/> instance.</returns>
    /// <exception cref="ObjectDisposedException">Thrown if the current instance is disposed.</exception>
    [MemberNotNull(nameof(canvasImage))]
    private ICanvasImage GetCanvasImage()
    {
        lock (this.transformNodes)
        {
            default(ObjectDisposedException).ThrowIf(this.isDisposed, this);

            // Build the effect graph and get the output node, if missing
            if (this.canvasImage is null)
            {
                this.isBuildingEffectGraph = true;

                DisposeEffectGraph();
                BuildEffectGraph(new EffectGraph(this));

                this.isBuildingEffectGraph = false;

                default(InvalidOperationException).ThrowIf(this.canvasImage is null);
            }

            // Configure the effect graph, if the effect is invalidated
            if (this.isInvalidated)
            {
                ConfigureEffectGraph(new EffectGraph(this));

                this.isInvalidated = false;
            }

            return this.canvasImage;
        }
    }

    /// <summary>
    /// Disposes and clears the current effect graph, if any.
    /// </summary>
    private void DisposeEffectGraph()
    {
        // Dispose all registered canvas images
        foreach (ICanvasImage canvasImage in this.transformNodes.Values)
        {
            canvasImage.Dispose();
        }

        // Also clear the current effect graph. Note that the canvas image used as output
        // node for the effect graph does not need to be explicitly disposed here, as that
        // object is guaranteed to have been part of the dictionary of transform nodes.
        this.transformNodes.Clear();
        this.canvasImage = null;
    }
}