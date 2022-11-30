using System;
using System.Threading;
using ABI.Microsoft.Graphics.Canvas;
using ComputeSharp.D2D1.Extensions;
using ComputeSharp.D2D1.Interop;
using ComputeSharp.D2D1.Interop.Effects;
using ComputeSharp.D2D1.Uwp.Extensions;
using ComputeSharp.Interop;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using Win32 = TerraFX.Interop.Windows.Windows;

namespace ComputeSharp.D2D1.Uwp;

/// <inheritdoc/>
unsafe partial class PixelShaderEffect<T>
{
    /// <inheritdoc/>
    int ICanvasImageInterop.Interface.GetDevice(ICanvasDevice** device)
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
    int ICanvasImageInterop.Interface.GetD2DImage(
        ICanvasDevice* device,
        ID2D1DeviceContext* deviceContext,
        GetD2DImageFlags flags,
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
                if ((flags & GetD2DImageFlags.ReadDpiFromDeviceContext) != GetD2DImageFlags.None)
                {
                    // If ReadDpiFromDeviceContext is used, the context cannot be null
                    if (deviceContext is null)
                    {
                        return E.E_POINTER;
                    }

                    if (deviceContext->HasCommandListTarget())
                    {
                        // Command lists are DPI dependent, so DPI compensation effects are always needed
                        flags |= GetD2DImageFlags.AlwaysInsertDpiCompensation;
                    }
                    else
                    {
                        // If the effect is not being drawn onto a command list, the DPIs can be read from the device context
                        targetDpi = deviceContext->GetDpi();
                    }

                    // ReadDpiFromDeviceContext has been processed, so it can be removed now
                    flags &= ~GetD2DImageFlags.ReadDpiFromDeviceContext;
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
                else if (!flags.HasFlag(GetD2DImageFlags.MinimalRealization))
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
    private void Unrealize()
    {
        // If there is no ID2D1Effect object, there is nothing left to do
        if (this.d2D1Effect.Get() is null)
        {
            return;
        }

        // Cache the last update values for the constant buffer, cache output, buffer precision properties, etc.
        // Note: the resource texture managers are deliberately not read back, as the only supported way to set
        // those is directly from the current effect instance. As such, they'd all either be the same instances
        // that are already stored locally, or trying to retrieve them would just throw an exception.
        this.value = this.d2D1Effect.Get()->GetConstantBuffer<T>();
        this.transformMapper = this.d2D1Effect.Get()->GetTransformMapper<T>();
        this.cacheOutput = this.d2D1Effect.Get()->GetCachedProperty();
        this.d2D1BufferPrecision = this.d2D1Effect.Get()->GetPrecisionProperty();

        // TODO: read back D2D sources from the effect

        // Finally release the effect as well
        this.d2D1Effect.Dispose();
    }

    /// <summary>
    /// Realizes the underlying <see cref="ID2D1Effect"/> for this object.
    /// </summary>
    /// <param name="flags">The current flags in use.</param>
    /// <param name="targetDpi">The target DPI in use.</param>
    /// <param name="deviceContext">The <see cref="ID2D1DeviceContext"/> instance in use.</param>
    private void Realize(GetD2DImageFlags flags, float targetDpi, ID2D1DeviceContext* deviceContext)
    {
        using ComPtr<ID2D1Factory> d2D1Factory = default;

        // Get the underlying ID2D1Factory from the input context, so we can register and create the effect
        deviceContext->GetFactory(d2D1Factory.GetAddressOf());

        using ComPtr<ID2D1Factory1> d2D1Factory1 = default;

        // D2D1PixelShaderEffect APIs specifically need an ID2D1Factory1 (as ID2D1Factory1::RegisterEffectFromString is used)
        d2D1Factory.CopyTo(d2D1Factory1.GetAddressOf()).Assert();

        fixed (ID2D1Effect** d2D1Effect = this.d2D1Effect)
        {
            Guid effectId = PixelShaderEffect.For<T>.Instance.Id;

            // Try to create an instance of the effect in use and store it in the current object
            HRESULT hresult = deviceContext->CreateEffect(effectId: &effectId, effect: d2D1Effect);

            // Check if creation failed due to the effect not being registered. In that case, register
            // it and then try again. This is much faster than check whether the effect is registered
            // manually every time, by enumerating all registered effects for the target device context.
            if (hresult == D2DERR.D2DERR_EFFECT_IS_NOT_REGISTERED)
            {
                // Register the effect with the factory (pass the same D2D1 draw transform mapper factory that was used before)
                D2D1PixelShaderEffect.RegisterForD2D1Factory1<T>(d2D1Factory1.Get(), out _);

                // Try to create the effect again
                hresult = deviceContext->CreateEffect(effectId: &effectId, effect: d2D1Effect);
            }

            // Rethrow any other errors
            hresult.Assert();
        }

        // Set the constant buffer for the effect
        D2D1PixelShaderEffect.SetConstantBufferForD2D1Effect(this.d2D1Effect.Get(), in this.value);

        // If there is a transform mapper, set it in the effect
        if (this.transformMapper is not null)
        {
            D2D1PixelShaderEffect.SetTransformMapperForD2D1Effect(this.d2D1Effect.Get(), this.transformMapper);
        }

        // If cache input has been set, forward that to the new effect
        if (this.cacheOutput)
        {
            this.d2D1Effect.Get()->SetCachedProperty(true);
        }

        // If the buffer precision isn't unknown, forward that as well
        if (this.d2D1BufferPrecision != D2D1_BUFFER_PRECISION.D2D1_BUFFER_PRECISION_UNKNOWN)
        {
            this.d2D1Effect.Get()->SetPrecisionProperty(this.d2D1BufferPrecision);
        }

        // Set all available resource texture managers (only set those that are not null, as they're all null by default anyway)
        for (int i = 0; i < 16; i++)
        {
            if (ResourceTextureManagers.Storage[i] is D2D1ResourceTextureManager resourceTextureManager)
            {
                D2D1PixelShaderEffect.SetResourceTextureManagerForD2D1Effect(this.d2D1Effect.Get(), resourceTextureManager, i);
            }
        }

        // TODO: port base CanvasEffect::Realize logic

        // TODO: transfer properties (eg. D2D1ResourceTextureManager-s)
    }

    /// <summary>
    /// Updates the draw state for the currently realized effect.
    /// </summary>
    /// <param name="flags">The current flags in use.</param>
    /// <param name="targetDpi">The target DPI in use.</param>
    /// <param name="deviceContext">The <see cref="ID2D1DeviceContext"/> instance in use.</param>
    private void RefreshInputs(GetD2DImageFlags flags, float targetDpi, ID2D1DeviceContext* deviceContext)
    {
        // TODO
    }
}