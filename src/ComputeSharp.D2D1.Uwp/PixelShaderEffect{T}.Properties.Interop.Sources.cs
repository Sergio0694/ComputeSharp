using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.Microsoft.Graphics.Canvas;
using ComputeSharp.D2D1.Extensions;
using ComputeSharp.D2D1.Helpers;
using ComputeSharp.D2D1.Uwp.Buffers;
using ComputeSharp.D2D1.Uwp.Extensions;
using ComputeSharp.Interop;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using Windows.Graphics.Effects;
using static ABI.Microsoft.Graphics.Canvas.WIN2D_GET_D2D_IMAGE_FLAGS;
using static TerraFX.Interop.DirectX.D2D1_BORDER_MODE;
using static TerraFX.Interop.DirectX.D2D1_DPICOMPENSATION_INTERPOLATION_MODE;
using static TerraFX.Interop.DirectX.D2D1_DPICOMPENSATION_PROP;
using static TerraFX.Interop.DirectX.D2D1_PROPERTY_TYPE;
using Win32 = TerraFX.Interop.Windows.Windows;

namespace ComputeSharp.D2D1.Uwp;

/// <inheritdoc/>
unsafe partial class PixelShaderEffect<T>
{
    /// <summary>
    /// Gets the marshalled value for <see cref="Sources"/>.
    /// </summary>
    /// <param name="index">The index of the <see cref="Sources"/> source to get or set.</param>
    /// <returns>The marshalled value for <see cref="Sources"/>.</returns>
    private IGraphicsEffectSource? GetSource(int index)
    {
        using ReferenceTracker.Lease _0 = GetReferenceTracker().GetLease();

        lock (this.lockObject)
        {
            return this.d2D1Effect.Get() switch
            {
                not null => GetD2DInput(index),
                _ => Sources.Storage[index].GetWrapper()
            };
        }
    }

    /// <summary>
    /// Sets the marshalled value for <see cref="Sources"/>.
    /// </summary>
    /// <param name="value">The value to set for <see cref="Sources"/>.</param>
    /// <param name="index">The index of the <see cref="IGraphicsEffectSource"/> source to get or set.</param>
    private void SetSource(IGraphicsEffectSource? value, int index)
    {
        using ReferenceTracker.Lease _0 = GetReferenceTracker().GetLease();

        lock (this.lockObject)
        {
            ref SourceReference source = ref Sources.Storage[index];

            if (this.d2D1Effect.Get() is not null)
            {
                const WIN2D_GET_D2D_IMAGE_FLAGS flags =
                    WIN2D_GET_D2D_IMAGE_FLAGS_MINIMAL_REALIZATION |
                    WIN2D_GET_D2D_IMAGE_FLAGS_ALLOW_NULL_EFFECT_INPUTS |
                    WIN2D_GET_D2D_IMAGE_FLAGS_UNREALIZE_ON_FAILURE;

                // The WIN2D_GET_D2D_IMAGE_FLAGS_UNREALIZE_ON_FAILURE flag is passed, so we just let the effect
                // unrealize in case of failure and take no other action (including throwing) if that happens.
                _ = SetD2DInput(index, value, flags, targetDpi: 0.0f, d2D1DeviceContext: null);
            }
            else
            {
                source.SetWrapper(value);
            }
        }
    }

    /// <remarks>This method assumes a lock is taken and the effect is already realized.</remarks>
    /// <inheritdoc cref="GetSource"/>
    private IGraphicsEffectSource? GetD2DInput(int index)
    {
        using ComPtr<ID2D1Image> d2D1Image = default;

        // Get the ID2D1Image input from the effect
        this.d2D1Effect.Get()->GetInput(
            index: (uint)index,
            input: d2D1Image.GetAddressOf());

        ref SourceReference sourceReference = ref Sources.Storage[index];

        // Check whether the input had a DPI compensation effect added to it, and skip it if so
        if (sourceReference.HasDpiCompensationEffect)
        {
            // Since a realized effect is the only source of authority for the effect state, we also
            // need to check that the actual source is in fact the same that was previous set by the
            // managed wrapper. If it is, we get the input and return that (ie. the underlying image).
            // If not, we just return it directly and reset the locally stored DPI compensation effect.
            if (d2D1Image.Get()->IsSameInstance(sourceReference.GetDpiCompensationEffect()))
            {
                sourceReference.GetDpiCompensationEffect()->GetInput(0, d2D1Image.ReleaseAndGetAddressOf());
            }
            else
            {
                sourceReference.SetDpiCompensationEffect(null);
            }
        }

        // Get or create a wrapper for the input image
        return sourceReference.GetOrCreateWrapper(this.canvasDevice.Get(), d2D1Image.Get());
    }

    /// <param name="index">The index of the <see cref="IGraphicsEffectSource"/> source to get or set.</param>
    /// <param name="value">The value to set for <see cref="Sources"/>.</param>
    /// <param name="flags">The <see cref="WIN2D_GET_D2D_IMAGE_FLAGS"/> flags for <paramref name="value"/>.</param>
    /// <param name="targetDpi">The target DPI for the realized image, if present.</param>
    /// <param name="d2D1DeviceContext">The input <see cref="ID2D1DeviceContext"/> object to use, if present.</param>
    /// <returns>Whether the input was set successfully or not.</returns>
    /// <remarks>This method assumes a lock is taken and the effect is already realized.</remarks>
    /// <inheritdoc cref="SetSource"/>
    private bool SetD2DInput(
        int index,
        IGraphicsEffectSource? value,
        WIN2D_GET_D2D_IMAGE_FLAGS flags,
        float targetDpi,
        ID2D1DeviceContext* d2D1DeviceContext)
    {
        using ComPtr<ID2D1Image> d2D1Image = default;

        float realizedDpi = 0;

        if (value is not null)
        {
            using ComPtr<IUnknown> canvasImageUnknown = default;

            // Get the underlying IUnknown object for the input source
            canvasImageUnknown.Attach((IUnknown*)Marshal.GetIUnknownForObject(value));

            using ComPtr<ICanvasImageInterop> canvasImageInterop = default;

            // Try to get the ICanvasImageInterop interface from the input source
            HRESULT hresult = canvasImageUnknown.CopyTo(canvasImageInterop.GetAddressOf());

            if (!Win32.SUCCEEDED(hresult))
            {
                // Only potentially handle E_NOINTERFACE, always throw in other cases
                if (hresult != E.E_NOINTERFACE)
                {
                    Marshal.ThrowExceptionForHR(hresult);
                }

                // If unrealization isn't requested for failures, just throw
                if ((flags & WIN2D_GET_D2D_IMAGE_FLAGS_UNREALIZE_ON_FAILURE) == WIN2D_GET_D2D_IMAGE_FLAGS_NONE)
                {
                    ThrowHelper.ThrowArgumentException(nameof(value), "The effect source is not valid (it must implement ICanvasImageInterop).");
                }

                Unrealize(value, index);

                return false;
            }

            using ComPtr<ICanvasDevice> sourceCanvasDevice = default;

            // Get the canvas device the source is realized on, if any
            canvasImageInterop.Get()->GetDevice(sourceCanvasDevice.GetAddressOf()).Assert();

            // If a device was retrieved, it must be the same as the input one. If the source has no device,
            // that is fine, as it just means that its underlying D2D image has not been realized yet.
            if (sourceCanvasDevice.Get() is not null && !this.canvasDevice.Get()->IsSameInstance(sourceCanvasDevice.Get()))
            {
                if ((flags & WIN2D_GET_D2D_IMAGE_FLAGS_UNREALIZE_ON_FAILURE) == WIN2D_GET_D2D_IMAGE_FLAGS_NONE)
                {
                    ThrowHelper.ThrowArgumentException(nameof(value), "The effect source is realized on a different canvas device.");
                }

                Unrealize(value, index);

                return false;
            }

            // Try to realize the image from the input source (this will recurse through the effect graph)
            hresult = canvasImageInterop.Get()->GetD2DImage(
                device: this.canvasDevice.Get(),
                deviceContext: d2D1DeviceContext,
                flags: flags,
                targetDpi: targetDpi,
                realizeDpi: &realizedDpi,
                ppImage: d2D1Image.GetAddressOf());

            // Unless requested, a failure to retrieve the image should just bubble up the HRESULT in an exception
            if ((flags & WIN2D_GET_D2D_IMAGE_FLAGS_UNREALIZE_ON_FAILURE) == WIN2D_GET_D2D_IMAGE_FLAGS_NONE)
            {
                hresult.Assert();
            }

            // If no realized image is available, unrealize the effect
            if (d2D1Image.Get() is null)
            {
                Unrealize(value, index);

                return false;
            }
        }
        else if ((flags & WIN2D_GET_D2D_IMAGE_FLAGS_ALLOW_NULL_EFFECT_INPUTS) == WIN2D_GET_D2D_IMAGE_FLAGS_NONE)
        {
            // If the source is null and this is not explicitly allowed, the invocation is not valid
            ThrowHelper.ThrowArgumentNullException(nameof(value), "The effect source cannot be null.");
        }

        // Save the managed wrapper and realized image
        Sources.Storage[index].SetWrapperAndResource(value, d2D1Image.Get());

        // Handle DPI compensation (the return is ignored here as whether DPI compensation was applied doesn't matter)
        _ = ApplyDpiCompensation(
            index: index,
            d2D1Image: ref *&d2D1Image,
            inputDpi: realizedDpi,
            flags: flags,
            targetDpi: targetDpi,
            d2D1DeviceContext: d2D1DeviceContext);

        // Set the actual effect input
        this.d2D1Effect.Get()->SetInput(index: (uint)index, input: d2D1Image.Get());

        return true;
    }

    /// <summary>
    /// Applies a DPI compensation effect onto a realized image, if needed.
    /// </summary>
    /// <param name="index">The index of the source being processed.</param>
    /// <param name="d2D1Image">A reference to the realized source (this might be replaced if DPI compensation is added).</param>
    /// <param name="inputDpi">The input DPU for <paramref name="d2D1Image"/>.</param>
    /// <param name="flags">The <see cref="WIN2D_GET_D2D_IMAGE_FLAGS"/> flags for <paramref name="d2D1Image"/>.</param>
    /// <param name="targetDpi">The target DPI for the realized image, if present.</param>
    /// <param name="d2D1DeviceContext">The input <see cref="ID2D1DeviceContext"/> object to use, if present.</param>
    /// <returns>Whether the DPI configuration has changed.</returns>
    private bool ApplyDpiCompensation(
        int index,
        ref ComPtr<ID2D1Image> d2D1Image,
        float inputDpi,
        WIN2D_GET_D2D_IMAGE_FLAGS flags,
        float targetDpi,
        ID2D1DeviceContext* d2D1DeviceContext)
    {
        ref SourceReference sourceReference = ref Sources.Storage[index];

        bool isDpiCompensationNeeded;

        // Detemine whether DPI compensation is needed (direct port of Win2D's CanvasEffect::ApplyDpiCompensation)
        if ((flags & WIN2D_GET_D2D_IMAGE_FLAGS_MINIMAL_REALIZATION) != WIN2D_GET_D2D_IMAGE_FLAGS_NONE)
        {
            // In minimal mode, we don't yet know the target DPI. For instance this occurs when setting an
            // effect as the source of an image brush. We'll fix up later when the brush is drawn to a device
            // context, at which point the real DPI is known. Since we don't yet know whether to include
            // compensation, we just preserve the existing state. This avoids repeated recreation during
            // set-draw-set-draw sequences.
            isDpiCompensationNeeded = sourceReference.HasDpiCompensationEffect && (inputDpi != 0);
        }
        else
        {
            bool neverCompensate = (flags & WIN2D_GET_D2D_IMAGE_FLAGS_NEVER_INSERT_DPI_COMPENSATION) != WIN2D_GET_D2D_IMAGE_FLAGS_NONE;
            bool alwaysCompensate = (flags & WIN2D_GET_D2D_IMAGE_FLAGS_ALWAYS_INSERT_DPI_COMPENSATION) != WIN2D_GET_D2D_IMAGE_FLAGS_NONE;

            // We need DPI compensation if:
            //  - The input has a fixed DPI
            //  - We were not told never to include it
            //  - Either we were told to always include it, or the input DPI is different from the target
            isDpiCompensationNeeded = (inputDpi != 0) && !neverCompensate && (alwaysCompensate || (inputDpi != targetDpi));

            // Early out if we are already in the right state
            if (isDpiCompensationNeeded == sourceReference.HasDpiCompensationEffect)
            {
                return false;
            }
        }

        // Apply the determined DPI compensation configuration
        if (isDpiCompensationNeeded)
        {
            using ComPtr<ID2D1Effect> d2D1EffectDpiCompensation = default;

            // Create an ID2D1Effect for the DPI compensation, if there isn't one already
            if (!sourceReference.HasDpiCompensationEffect)
            {
                // We need to create a DPI compensation effect, so a device context must be available
                if (d2D1DeviceContext is null)
                {
                    Marshal.ThrowExceptionForHR(E.E_POINTER);
                }

                // Create the DPI compensation effect
                d2D1DeviceContext->CreateEffect(
                    effectId: (Guid*)Unsafe.AsPointer(ref Unsafe.AsRef(in CLSID.CLSID_D2D1DpiCompensation)),
                    effect: d2D1EffectDpiCompensation.GetAddressOf()).Assert();

                D2D1_BORDER_MODE d2D1BorderMode = D2D1_BORDER_MODE_HARD;
                D2D1_DPICOMPENSATION_INTERPOLATION_MODE d2D1DpiCompensationInterpolationMode = D2D1_DPICOMPENSATION_INTERPOLATION_MODE_LINEAR;

                // Set the border mode to hard
                d2D1EffectDpiCompensation.Get()->SetValue(
                    index: (uint)D2D1_DPICOMPENSATION_PROP_BORDER_MODE,
                    type: D2D1_PROPERTY_TYPE_UNKNOWN,
                    data: (byte*)&d2D1BorderMode,
                    dataSize: sizeof(D2D1_BORDER_MODE)).Assert();

                // Set the interpolation mode to linear
                d2D1EffectDpiCompensation.Get()->SetValue(
                    index: (uint)D2D1_DPICOMPENSATION_PROP_INTERPOLATION_MODE,
                    type: D2D1_PROPERTY_TYPE_UNKNOWN,
                    data: (byte*)&d2D1DpiCompensationInterpolationMode,
                    dataSize: sizeof(D2D1_DPICOMPENSATION_INTERPOLATION_MODE)).Assert();

                // Store the DPI effect for later use
                sourceReference.SetDpiCompensationEffect(d2D1EffectDpiCompensation.Get());
            }
            else
            {
                // Just reuse the existing DPI effect otherwise
                d2D1EffectDpiCompensation.Attach(new ComPtr<ID2D1Effect>(sourceReference.GetDpiCompensationEffect()).Get());
            }

            // Set the input image as source for the DPI compensation effect
            sourceReference.GetDpiCompensationEffect()->SetInput(index: (uint)index, input: d2D1Image.Get());

            D2D_VECTOR_2F d2DVectorDpi;
            d2DVectorDpi.x = inputDpi;
            d2DVectorDpi.y = inputDpi;

            // Set the DPI setting
            sourceReference.GetDpiCompensationEffect()->SetValue(
                index: (uint)D2D1_DPICOMPENSATION_PROP_INPUT_DPI,
                type: D2D1_PROPERTY_TYPE_UNKNOWN,
                data: (byte*)&d2DVectorDpi,
                dataSize: (uint)sizeof(D2D_VECTOR_2F)).Assert();

            // Substitute the input image with the DPI compensation effect
            d2D1EffectDpiCompensation.CopyTo(ref d2D1Image).Assert();
        }
        else
        {
            // Ensure the existing effect is released, if present
            sourceReference.SetDpiCompensationEffect(null);
        }

        return true;
    }
}