using System;
using System.Numerics;
using System.Threading;
using ABI.Microsoft.Graphics.Canvas;
using ComputeSharp.D2D1.Extensions;
using ComputeSharp.D2D1.Interop;
using ComputeSharp.D2D1.Uwp.Extensions;
using ComputeSharp.Interop;
using Microsoft.Graphics.Canvas;
using Windows.Foundation;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using Win32 = TerraFX.Interop.Windows.Windows;

namespace ComputeSharp.D2D1.Uwp;

/// <summary>
/// A custom <see cref="ICanvasImage"/> implementation powered by a supplied shader type.
/// </summary>
/// <typeparam name="T">The type of shader to use to render frames.</typeparam>
public sealed partial class PixelShaderEffect<T> : IReferenceTrackedObject, ICanvasImage, ICanvasImageInterop
    where T : unmanaged, ID2D1PixelShader
{
    /// <summary>
    /// The <see cref="ReferenceTracker"/> value for the current instance.
    /// </summary>
    private ReferenceTracker referenceTracker;

    /// <summary>
    /// Lock object used to synchronize calls to <see cref="ICanvasImageInterop"/> APIs.
    /// </summary>
    private readonly object lockObject = new();

    /// <summary>
    /// Flag to track whether a given call is recursively invoked by <see cref="ICanvasImageInterop.GetD2DImage"/>, to avoid graph cycles.
    /// </summary>
    private volatile int isInsideGetD2DImage;

    /// <summary>
    /// The current device the effect is realized on, if one is available (this is the underlying COM object for <see cref="CanvasDevice"/>).
    /// </summary>
    private ComPtr<ICanvasDevice> canvasDevice;

    /// <summary>
    /// The underlying D2D1 device the effect is realized on, if one is available (this is the D2D device backing <see cref="canvasDevice"/>).
    /// </summary>
    private ComPtr<ID2D1Device1> d2D1RealizationDevice;

    /// <summary>
    /// The current realized <see cref="ID2D1Effect"/>, if one is available.
    /// </summary>
    private ComPtr<ID2D1Effect> d2D1Effect;

    /// <summary>
    /// The current <typeparamref name="T"/> value in use.
    /// </summary>
    public T Value;

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

    /// <inheritdoc/>
    unsafe int ICanvasImageInterop.GetDevice(ICanvasDevice** device)
    {
        using ReferenceTracker.Lease _0 = GetReferenceTracker().TryGetLease(out bool leaseTaken);

        // Check for disposal
        if (!leaseTaken)
        {
            return RO.RO_E_CLOSED;
        }

        // Ensure we can take a lock without blocking. We can't access the realization device with no
        // lock, as that could lead to memory leaks or corruption if GetD2DImage was to manipulate that
        // field concurrently. We also cannot lock directly, because that would deadlock if someone was
        // to (incorrectly) try to use the same effect from two different threads. To protect against
        // that, we try to lock, and just return E_NOT_VALID_STATE if another thread holds the lock. If
        // this call had GetD2DImage in the stack, that would work fine because locks are recursive.
        if (!Monitor.TryEnter(this.lockObject))
        {
            return E.E_NOT_VALID_STATE;
        }

        try
        {
            // Copy the device over to the target
            return this.canvasDevice.CopyTo(device);
        }
        finally
        {
            Monitor.Exit(this.lockObject);
        }
    }

    /// <inheritdoc/>
    unsafe int ICanvasImageInterop.GetD2DImage(
        ICanvasDevice* device,
        ID2D1DeviceContext* deviceContext,
        CanvasImageGetD2DImageFlags flags,
        float targetDpi,
        float* realizeDpi,
        ID2D1Image** ppImage)
    {
        using ReferenceTracker.Lease _0 = GetReferenceTracker().TryGetLease(out bool leaseTaken);

        // Check for disposal
        if (!leaseTaken)
        {
            return RO.RO_E_CLOSED;
        }

        // Check for graph cycles
        if (Interlocked.CompareExchange(ref this.isInsideGetD2DImage, 1, 0) == 1)
        {
            return D2DERR.D2DERR_CYCLIC_GRAPH;
        }

        try
        {
            // Lock after checking for graph cycles (other public APIs also lock on this object)
            lock (this.lockObject)
            {
                // Process the ReadDpiFromDeviceContext flag (same logic as CanvasEffect::GetD2DImage)
                if ((flags & CanvasImageGetD2DImageFlags.ReadDpiFromDeviceContext) != CanvasImageGetD2DImageFlags.None)
                {
                    // If ReadDpiFromDeviceContext is used, the context cannot be null
                    if (deviceContext is null)
                    {
                        return E.E_POINTER;
                    }

                    if (deviceContext->HasCommandListTarget())
                    {
                        // Command lists are DPI dependent, so DPI compensation effects are always needed
                        flags |= CanvasImageGetD2DImageFlags.AlwaysInsertDpiCompensation;
                    }
                    else
                    {
                        // If the effect is not being drawn onto a command list, the DPIs can be read from the device context
                        targetDpi = deviceContext->GetDpi();
                    }

                    // ReadDpiFromDeviceContext has been processed, so it can be removed now
                    flags &= ~CanvasImageGetD2DImageFlags.ReadDpiFromDeviceContext;
                }

                using ComPtr<ID2D1Device1> d2D1Device1 = default;

                // Get the underlying ID2D1Device1 instance in use
                device->GetD2DDevice(d2D1Device1.GetAddressOf()).Assert();

                // Check that the input D2D device is the same instance as the current realization device, if any.
                // If not and the effect has already been realized, unrealize it and store the new device to use.
                if (!d2D1Device1.IsSameInstance(in this.d2D1RealizationDevice))
                {
                    Unrealize();

                    // Store the input realization device and canvas device for future uses
                    d2D1Device1.CopyTo(ref this.d2D1RealizationDevice).Assert();

                    fixed (ICanvasDevice** canvasDevice = this.canvasDevice)
                    {
                        device->QueryInterface(Win32.__uuidof<ICanvasDevice>(), (void**)canvasDevice).Assert();
                    }
                }

                // Create the effect, if it doesn't exist already
                if (this.d2D1Effect.Get() is null)
                {
                    Realize(flags, targetDpi, deviceContext);
                }
                else if (!flags.HasFlag(CanvasImageGetD2DImageFlags.MinimalRealization))
                {
                    // Recurse through the effect graph and ensure all nodes are initialized
                    RefreshInputs(flags, targetDpi, deviceContext);
                }

                // The realized DPIs are always 0 for pixel shader effects
                if (realizeDpi is not null)
                {
                    *realizeDpi = 0;
                }

                this.d2D1Effect.CopyTo(ppImage).Assert();

                return S.S_OK;
            }
        }
        catch (Exception e)
        {
            *ppImage = null;

            return e.HResult;
        }
        finally
        {
            // Ensure that reset this flag so that this method can be called again
            this.isInsideGetD2DImage = 0;
        }
    }

    /// <summary>
    /// Unrealizes the currently realized <see cref="ID2D1Effect"/> object.
    /// </summary>
    private unsafe void Unrealize()
    {
        // If there is no ID2D1Effect object, there is nothing left to do
        if (this.d2D1Effect.Get() is null)
        {
            return;
        }

        // TODO
    }

    /// <summary>
    /// Realizes the underlying <see cref="ID2D1Effect"/> for this object.
    /// </summary>
    /// <param name="flags">The current flags in use.</param>
    /// <param name="targetDpi">The target DPI in use.</param>
    /// <param name="deviceContext">The <see cref="ID2D1DeviceContext"/> instance in use.</param>
    private unsafe void Realize(CanvasImageGetD2DImageFlags flags, float targetDpi, ID2D1DeviceContext* deviceContext)
    {
        using ComPtr<ID2D1Factory> d2D1Factory = default;

        // Get the underlying ID2D1Factory from the input context, so we can register and create the effect
        deviceContext->GetFactory(d2D1Factory.GetAddressOf());

        using ComPtr<ID2D1Factory1> d2D1Factory1 = default;

        // D2D1PixelShaderEffect APIs specifically need an ID2D1Factory1 (as ID2D1Factory1::RegisterEffectFromString is used)
        d2D1Factory.CopyTo(d2D1Factory1.GetAddressOf()).Assert();

        // Register the effect with the factory (TODO: only do this if needed)
        D2D1PixelShaderEffect.RegisterForD2D1Factory1<T>(d2D1Factory1.Get(), out _);

        fixed (ID2D1Effect** d2D1Effect = this.d2D1Effect)
        {
            // Create an instance of the effect in use and store it in the current object
            D2D1PixelShaderEffect.CreateFromD2D1DeviceContext<T>(deviceContext, (void**)d2D1Effect);
        }

        // TODO: port base CanvasEffect::Realize logic

        // Also set the current constants for the effect
        D2D1PixelShaderEffect.SetConstantBufferForD2D1Effect(in this.Value, this.d2D1Effect.Get());
    }

    /// <summary>
    /// Updates the draw state for the currently realized effect.
    /// </summary>
    /// <param name="flags">The current flags in use.</param>
    /// <param name="targetDpi">The target DPI in use.</param>
    /// <param name="deviceContext">The <see cref="ID2D1DeviceContext"/> instance in use.</param>
    private unsafe void RefreshInputs(CanvasImageGetD2DImageFlags flags, float targetDpi, ID2D1DeviceContext* deviceContext)
    {
        D2D1PixelShaderEffect.SetConstantBufferForD2D1Effect(in this.Value, this.d2D1Effect.Get());

        // TODO
    }
}