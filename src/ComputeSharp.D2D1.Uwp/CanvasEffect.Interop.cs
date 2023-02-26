using System;
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
    public void Dispose()
    {
        lock (this.lockObject)
        {
            this.canvasImage?.Dispose();
            this.canvasImage = null;
            this.isDisposed = true;
        }
    }

    /// <inheritdoc/>
    unsafe int ICanvasImageInterop.Interface.GetDevice(ICanvasDevice** device, WIN2D_GET_DEVICE_ASSOCIATION_TYPE* type)
    {
        using ComPtr<ICanvasImageInterop> canvasImageInterop = default;

        GetCanvasImageInterop(canvasImageInterop.GetAddressOf());

        return canvasImageInterop.Get()->GetDevice(device, type);
    }

    /// <inheritdoc/>
    unsafe int ICanvasImageInterop.Interface.GetD2DImage(ICanvasDevice* device, ID2D1DeviceContext* deviceContext, WIN2D_GET_D2D_IMAGE_FLAGS flags, float targetDpi, float* realizeDpi, ID2D1Image** ppImage)
    {
        using ComPtr<ICanvasImageInterop> canvasImageInterop = default;

        GetCanvasImageInterop(canvasImageInterop.GetAddressOf());

        return canvasImageInterop.Get()->GetD2DImage(device, deviceContext, flags, targetDpi, realizeDpi, ppImage);
    }

    /// <summary>
    /// Gets or creates the current <see cref="ICanvasImage"/> instance.
    /// </summary>
    /// <returns>The current <see cref="ICanvasImage"/> instance.</returns>
    /// <exception cref="ObjectDisposedException">Thrown if the current instance is disposed.</exception>
    private ICanvasImage GetCanvasImage()
    {
        lock (this.lockObject)
        {
            default(ObjectDisposedException).ThrowIf(this.isDisposed, this);

            this.canvasImage ??= CreateCanvasImage();

            if (this.isInvalidated)
            {
                ConfigureCanvasImage();

                this.isInvalidated = false;
            }

            return this.canvasImage;
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