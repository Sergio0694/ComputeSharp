using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.Microsoft.Graphics.Canvas;
using ComputeSharp.D2D1.Extensions;
using ComputeSharp.D2D1.Helpers;
using ComputeSharp.D2D1.Interop;
using ComputeSharp.D2D1.Shaders.Interop.Effects.ResourceManagers;
using ComputeSharp.D2D1.Shaders.Interop.Effects.TransformMappers;
using ComputeSharp.D2D1.Uwp.Buffers;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using Windows.Graphics.Effects;
using Win32 = TerraFX.Interop.Windows.Windows;

#pragma warning disable CS0618

namespace ComputeSharp.D2D1.Uwp.Extensions;

/// <summary>
/// A <see langword="class"/> with extensions for the <see cref="ID2D1Effect"/> type.
/// </summary>
internal static unsafe class ID2D1EffectExtensions
{
    /// <summary>
    /// Gets the constant buffer of type <typeparamref name="T"/> from a given <see cref="ID2D1Effect"/> object.
    /// </summary>
    /// <typeparam name="T">The type of shader being used.</typeparam>
    /// <param name="d2D1Effect">The input <see cref="ID2D1Effect"/> instance.</param>
    /// <returns>The constant buffer of type <typeparamref name="T"/> for <paramref name="d2D1Effect"/>.</returns>
    public static T GetConstantBuffer<T>(this ref ID2D1Effect d2D1Effect)
        where T : unmanaged, ID2D1PixelShader
    {
        int constantBufferSize = D2D1PixelShader.GetConstantBufferSize<T>();
        byte[] buffer = ArrayPool<byte>.Shared.Rent(constantBufferSize);

        fixed (byte* p = buffer)
        {
            // Get the raw constant buffer from the effect
            d2D1Effect.GetValue(
                index: D2D1PixelShaderEffectProperty.ConstantBuffer,
                type: D2D1_PROPERTY_TYPE.D2D1_PROPERTY_TYPE_BLOB,
                data: p,
                dataSize: (uint)constantBufferSize).Assert();
        }

        Unsafe.SkipInit(out T shader);

        shader.InitializeFromDispatchData(buffer.AsSpan(0, constantBufferSize));

        return shader;
    }

    /// <summary>
    /// Gets the <see cref="D2D1TransformMapper{T}"/> instance from a given <see cref="ID2D1Effect"/> object.
    /// </summary>
    /// <typeparam name="T">The type of shader being used.</typeparam>
    /// <param name="d2D1Effect">The input <see cref="ID2D1Effect"/> instance.</param>
    /// <returns>The <see cref="D2D1TransformMapper{T}"/> instance for <paramref name="d2D1Effect"/>.</returns>
    public static D2D1TransformMapper<T>? GetTransformMapper<T>(this ref ID2D1Effect d2D1Effect)
        where T : unmanaged, ID2D1PixelShader
    {
        using ComPtr<ID2D1TransformMapper> d2D1TransformMapper = default;

        // Get the ID2D1TransformMapper object from the effect
        d2D1Effect.GetValue(
            index: D2D1PixelShaderEffectProperty.TransformMapper,
            type: D2D1_PROPERTY_TYPE.D2D1_PROPERTY_TYPE_IUNKNOWN,
            data: (byte*)d2D1TransformMapper.GetAddressOf(),
            dataSize: (uint)sizeof(void*)).Assert();

        // If there is no transform, just return null (this is the default state)
        if (d2D1TransformMapper.Get() is null)
        {
            return null;
        }

        // Otherwise, check if it's an internal CCW and retrieve the managed wrapper
        using ComPtr<ID2D1TransformMapperInternal> d2D1TransformMapperInternal = default;

        d2D1TransformMapper.CopyTo(d2D1TransformMapperInternal.GetAddressOf()).Assert();

        IntPtr handlePtr;

        // Get the underlying GCHandle value
        d2D1TransformMapperInternal.Get()->GetManagedWrapperHandle((void**)&handlePtr).Assert();

        // Retrieve the managed wrapper from the GCHandle
        return (D2D1TransformMapper<T>)GCHandle.FromIntPtr(handlePtr).Target!;
    }

    /// <summary>
    /// Gets the <see cref="IGraphicsEffectSource"/> source from a given <see cref="ID2D1Effect"/> object, at a specified index.
    /// </summary>
    /// <param name="d2D1Effect">The input <see cref="ID2D1Effect"/> instance.</param>
    /// <param name="canvasDevice">The realization device currently in use.</param>
    /// <param name="source">The input <see cref="SourceReference"/> object for the target index.</param>
    /// <param name="index">The index for the source to retrieve.</param>
    /// <returns>The <see cref="IGraphicsEffectSource"/> source from a given <see cref="ID2D1Effect"/> object, at a specified index.</returns>
    public static IGraphicsEffectSource? GetSource(
        this ref ID2D1Effect d2D1Effect,
        ICanvasDevice* canvasDevice,
        ref SourceReference source,
        int index)
    {
        using ComPtr<ID2D1Image> d2D1Image = default;

        // Get the ID2D1Image input from the effect
        d2D1Effect.GetInput(
            index: (uint)index,
            input: d2D1Image.GetAddressOf());

        // Check whether the input had a DPI compensation effect added to it, and skip it if so
        if (source.HasDpiCompensationEffect)
        {
            // Since a realized effect is the only source of authority for the effect state, we also
            // need to check that the actual source is in fact the same that was previous set by the
            // managed wrapper. If it is, we get the input and return that (ie. the underlying image).
            // If not, we just return it directly and reset the locally stored DPI compensation effect.
            if (d2D1Image.Get()->IsSameInstance(source.GetDpiCompensationEffect()))
            {
                source.GetDpiCompensationEffect()->GetInput(0, d2D1Image.ReleaseAndGetAddressOf());
            }
            else
            {
                source.SetDpiCompensationEffect(null);
            }
        }

        // Get or create a wrapper for the input image
        return source.GetOrCreateWrapper(null, d2D1Image.Get());
    }

    /// <summary>
    /// Sets the <see cref="IGraphicsEffectSource"/> source for a given <see cref="ID2D1Effect"/> object, at a specified index.
    /// </summary>
    /// <param name="d2D1Effect">The input <see cref="ID2D1Effect"/> instance.</param>
    /// <param name="canvasDevice">The realization device currently in use.</param>
    /// <param name="deviceContext">The device context currently in use.</param>
    /// <param name="flags">The realization flags to use.</param>
    /// <param name="targetDpi">The target DPI value for the source.</param>
    /// <param name="value">The input <see cref="IGraphicsEffectSource"/> value to set.</param>
    /// <param name="source">The target <see cref="SourceReference"/> value, for caching.</param>
    /// <param name="index">The index of the source to set.</param>
    /// <returns>Whether the operation succeeded (if it didn't the effect should be unrealized).</returns>
    public static bool TrySetSource(
        this ref ID2D1Effect d2D1Effect,
        ICanvasDevice* canvasDevice,
        ID2D1DeviceContext* deviceContext,
        GetD2DImageFlags flags,
        float targetDpi,
        IGraphicsEffectSource? value,
        ref SourceReference source,
        int index)
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
                // If unrealization isn't requested for failures, just throw
                if ((flags & GetD2DImageFlags.UnrealizeOnFailure) == GetD2DImageFlags.None)
                {
                    ThrowHelper.ThrowArgumentException(nameof(value), "The effect source is not valid (it must implement ICanvasImageInterop).");
                }

                return false;
            }

            using ComPtr<ICanvasDevice> sourceCanvasDevice = default;

            // Get the canvas device the source is realized on, if any
            canvasImageInterop.Get()->GetDevice(sourceCanvasDevice.GetAddressOf()).Assert();

            // If a device was retrieved, it must be the same as the input one. If the source has no device,
            // that is fine, as it just means that its underlying D2D image has not been realized yet.
            if (sourceCanvasDevice.Get() is not null && !canvasDevice->IsSameInstance(sourceCanvasDevice.Get()))
            {
                if ((flags & GetD2DImageFlags.UnrealizeOnFailure) == GetD2DImageFlags.None)
                {
                    ThrowHelper.ThrowArgumentException(nameof(value), "The effect source is realized on a different canvas device.");
                }

                return false;
            }

            // Try to realize the image from the input source
            hresult = canvasImageInterop.Get()->GetD2DImage(
                device: canvasDevice,
                deviceContext: deviceContext,
                flags: flags,
                targetDpi: targetDpi,
                realizeDpi: &realizedDpi,
                ppImage: d2D1Image.GetAddressOf());

            // Unless requested, a failure to retrieve the image should just bubble up the HRESULT in an exception
            if ((flags & GetD2DImageFlags.UnrealizeOnFailure) == GetD2DImageFlags.None)
            {
                hresult.Assert();
            }
        }
        else if ((flags & GetD2DImageFlags.AllowNullEffectInputs) == GetD2DImageFlags.None)
        {
            // If the source is null and this is not explicitly allowed, the invocation is not valid
            ThrowHelper.ThrowArgumentNullException(nameof(value), "The effect source cannot be null.");
        }

        // Save the managed wrapper and realized image
        source.SetWrapperAndResource(value, d2D1Image.Get());

        // TODO: handle DPI compensation

        // Set the actual effect input
        d2D1Effect.SetInput(index: (uint)index, input: d2D1Image.Get());

        return true;
    }

    /// <summary>
    /// Gets the <see cref="D2D1ResourceTextureManager"/> instance from a given <see cref="ID2D1Effect"/> object.
    /// </summary>
    /// <param name="d2D1Effect">The input <see cref="ID2D1Effect"/> instance.</param>
    /// <param name="source">The current cached source instance.</param>
    /// <param name="index">The index of the resource texture manager to retrieve.</param>
    /// <returns>The <see cref="D2D1ResourceTextureManager"/> instance for <paramref name="d2D1Effect"/>.</returns>
    public static D2D1ResourceTextureManager? GetResourceTextureManager(
        this ref ID2D1Effect d2D1Effect,
        D2D1ResourceTextureManager? source,
        int index)
    {
        using ComPtr<ID2D1ResourceTextureManager> d2D1ResourceTextureManager = default;

        // Get the ID2D1ResourceTextureManager object from the effect
        d2D1Effect.GetValue(
            index: (uint)(D2D1PixelShaderEffectProperty.ResourceTextureManager0 + index),
            type: D2D1_PROPERTY_TYPE.D2D1_PROPERTY_TYPE_IUNKNOWN,
            data: (byte*)d2D1ResourceTextureManager.GetAddressOf(),
            dataSize: (uint)sizeof(void*)).Assert();

        // If there is no resource texture manager, just return null (this is the default state)
        if (d2D1ResourceTextureManager.Get() is null)
        {
            return null;
        }

        // Otherwise, the source must not be null and wrapping the same CCW
        if (source is not null)
        {
            using ComPtr<ID2D1ResourceTextureManager> d2D1ResourceTextureManagerSource = default;

            source.GetD2D1ResourceTextureManager(d2D1ResourceTextureManagerSource.GetAddressOf());

            // Check if the instances match, and return the managed wrapper we already have
            if (d2D1ResourceTextureManager.Get()->IsSameInstance(d2D1ResourceTextureManagerSource.Get()))
            {
                return source;
            }
        }

        // There was no way to retrieve the correct managed wrapper
        ThrowHelper.ThrowInvalidOperationException("Cannot retrieve a D2D1ResourceTextureManager instance wrapping a native object that was set externally.");

        return null;
    }

    /// <summary>
    /// Gets the <see cref="D2D1_PROPERTY.D2D1_PROPERTY_CACHED"/> property for a given <see cref="ID2D1Effect"/> object.
    /// </summary>
    /// <param name="d2D1Effect">The input <see cref="ID2D1Effect"/> instance.</param>
    /// <returns>The <see cref="D2D1_PROPERTY.D2D1_PROPERTY_CACHED"/> value for <paramref name="d2D1Effect"/>.</returns>
    public static bool GetCachedProperty(this ref ID2D1Effect d2D1Effect)
    {
        int cacheOutput;

        d2D1Effect.GetValue(
            index: (uint)D2D1_PROPERTY.D2D1_PROPERTY_CACHED,
            type: D2D1_PROPERTY_TYPE.D2D1_PROPERTY_TYPE_BOOL,
            data: (byte*)&cacheOutput,
            dataSize: sizeof(int)).Assert();

        return cacheOutput != 0;
    }

    /// <summary>
    /// Sets the <see cref="D2D1_PROPERTY.D2D1_PROPERTY_CACHED"/> property for a given <see cref="ID2D1Effect"/> object.
    /// </summary>
    /// <param name="d2D1Effect">The input <see cref="ID2D1Effect"/> instance.</param>
    /// <param name="value">The property value to set.</param>
    public static void SetCachedProperty(this ref ID2D1Effect d2D1Effect, bool value)
    {
        int cacheOutput = value ? 1 : 0;

        d2D1Effect.SetValue(
            index: (uint)D2D1_PROPERTY.D2D1_PROPERTY_CACHED,
            type: D2D1_PROPERTY_TYPE.D2D1_PROPERTY_TYPE_BOOL,
            data: (byte*)&cacheOutput,
            dataSize: sizeof(int)).Assert();
    }

    /// <summary>
    /// Gets the <see cref="D2D1_PROPERTY.D2D1_PROPERTY_PRECISION"/> property for a given <see cref="ID2D1Effect"/> object.
    /// </summary>
    /// <param name="d2D1Effect">The input <see cref="ID2D1Effect"/> instance.</param>
    /// <returns>The <see cref="D2D1_PROPERTY.D2D1_PROPERTY_PRECISION"/> value for <paramref name="d2D1Effect"/>.</returns>
    public static D2D1_BUFFER_PRECISION GetPrecisionProperty(this ref ID2D1Effect d2D1Effect)
    {
        D2D1_BUFFER_PRECISION d2D1BufferPrecision;

        d2D1Effect.GetValue(
            index: (uint)D2D1_PROPERTY.D2D1_PROPERTY_PRECISION,
            type: D2D1_PROPERTY_TYPE.D2D1_PROPERTY_TYPE_UNKNOWN,
            data: (byte*)&d2D1BufferPrecision,
            dataSize: sizeof(D2D1_BUFFER_PRECISION)).Assert();

        return d2D1BufferPrecision;
    }

    /// <summary>
    /// Sets the <see cref="D2D1_PROPERTY.D2D1_PROPERTY_PRECISION"/> property for a given <see cref="ID2D1Effect"/> object.
    /// </summary>
    /// <param name="d2D1Effect">The input <see cref="ID2D1Effect"/> instance.</param>
    /// <param name="value">The property value to set.</param>
    public static void SetPrecisionProperty(this ref ID2D1Effect d2D1Effect, D2D1_BUFFER_PRECISION value)
    {
        d2D1Effect.SetValue(
            index: (uint)D2D1_PROPERTY.D2D1_PROPERTY_PRECISION,
            type: D2D1_PROPERTY_TYPE.D2D1_PROPERTY_TYPE_UNKNOWN,
            data: (byte*)&value,
            dataSize: sizeof(D2D1_BUFFER_PRECISION)).Assert();
    }
}