using System.Numerics;
using System.Runtime.InteropServices;
using ABI.Microsoft.Graphics.Canvas;
using ComputeSharp.D2D1.Extensions;
using Microsoft.Graphics.Canvas;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using Windows.Foundation;
using ICanvasResourceCreator = Microsoft.Graphics.Canvas.ICanvasResourceCreator;

#pragma warning disable CA1063

namespace ComputeSharp.D2D1.Uwp;

/// <summary>
/// A base type to implement packaged and easy to use <see cref="ICanvasImage"/>-based effects that can be used with Win2D.
/// </summary>
public abstract unsafe class CanvasEffect : ICanvasImage, ICanvasImageInterop.Interface
{
    /// <summary>
    /// Lock object used to synchronize calls to <see cref="ICanvasImageInterop"/> APIs.
    /// </summary>
    private readonly object lockObject = new();

    /// <summary>
    /// The current cached result for <see cref="GetCanvasImage"/>, if available.
    /// </summary>
    private ICanvasImage? canvasImage;

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
    public void Dispose()
    {
    }

    /// <inheritdoc/>
    unsafe int ICanvasImageInterop.Interface.GetDevice(ICanvasDevice** device)
    {
        using ComPtr<ICanvasImageInterop> canvasImageInterop = default;

        GetCanvasImageInterop(canvasImageInterop.GetAddressOf());

        return canvasImageInterop.Get()->GetDevice(device);
    }

    /// <inheritdoc/>
    unsafe int ICanvasImageInterop.Interface.GetD2DImage(ICanvasDevice* device, ID2D1DeviceContext* deviceContext, WIN2D_GET_D2D_IMAGE_FLAGS flags, float targetDpi, float* realizeDpi, ID2D1Image** ppImage)
    {
        using ComPtr<ICanvasImageInterop> canvasImageInterop = default;

        GetCanvasImageInterop(canvasImageInterop.GetAddressOf());

        return canvasImageInterop.Get()->GetD2DImage(device, deviceContext, flags, targetDpi, realizeDpi, ppImage);
    }

    /// <summary>
    /// Creates the resulting <see cref="ICanvasImage"/> representing the output node of the effect graph for this <see cref="CanvasEffect"/> instance.
    /// </summary>
    /// <returns>The resulting <see cref="ICanvasImage"/> representing the output node of the effect graph for this <see cref="CanvasEffect"/> instance.</returns>
    /// <remarks>
    /// This method is only called once and its result is cached until explicitly invalidated by calling <see cref="InvalidateCanvasImage"/>.
    /// </remarks>
    protected abstract ICanvasImage CreateCanvasImage();

    /// <summary>
    /// Invalidates the last returned result from <see cref="GetCanvasImage"/>.
    /// </summary>
    /// <remarks>
    /// This method is used to signal when the entire effect graph should be updated.
    /// When called, the internal cache for <see cref="GetCanvasImage"/> will be reset.
    /// </remarks>
    protected void InvalidateCanvasImage()
    {
        lock (this.lockObject)
        {
            this.canvasImage?.Dispose();

            this.canvasImage = null;
        }
    }

    /// <summary>
    /// Gets or creates the current <see cref="ICanvasImage"/> instance.
    /// </summary>
    /// <returns>The current <see cref="ICanvasImage"/> instance.</returns>
    private ICanvasImage GetCanvasImage()
    {
        lock (this.lockObject)
        {
            return this.canvasImage ??= CreateCanvasImage();
        }
    }

    /// <summary>
    /// Gets the <see cref="ICanvasImageInterop"/> object currently in use.
    /// </summary>
    /// <param name="canvasImageInterop">A pointer to the <see cref="ICanvasImageInterop"/> object currently in use.</param>
    private void GetCanvasImageInterop(ICanvasImageInterop** canvasImageInterop)
    {
        using ComPtr<IUnknown> canvasImageUnknown = default;

        canvasImageUnknown.Attach((IUnknown*)Marshal.GetIUnknownForObject(GetCanvasImage()));

        canvasImageUnknown.CopyTo(canvasImageInterop).Assert();
    }
}