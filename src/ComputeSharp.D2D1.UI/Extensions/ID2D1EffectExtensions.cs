using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.D2D1.Descriptors;
using ComputeSharp.D2D1.Extensions;
using ComputeSharp.D2D1.Interop;
using ComputeSharp.D2D1.Shaders.Interop.Effects.ResourceManagers;
using ComputeSharp.D2D1.Shaders.Interop.Effects.TransformMappers;
using ComputeSharp.Win32;

namespace ComputeSharp.D2D1.WinUI.Extensions;

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
        where T : unmanaged, ID2D1PixelShader, ID2D1PixelShaderDescriptor<T>
    {
        int constantBufferSize = T.ConstantBufferSize;

        // Special cases the constant buffer type being empty. If that's the case, there is no
        // need to try to retrieve the constant buffer from the underlying effect anyway, and
        // most importantly, the ID2D1EffectImpl from ComputeSharp.D2D1 will return E_INVALIDARG.
        if (constantBufferSize == 0)
        {
            return default;
        }

        byte[]? bufferArray = null;

        Span<byte> buffer = constantBufferSize <= 64
            ? stackalloc byte[64]
            : bufferArray = ArrayPool<byte>.Shared.Rent(constantBufferSize);

        fixed (byte* p = buffer)
        {
            // Get the raw constant buffer from the effect
            d2D1Effect.GetValue(
                index: D2D1PixelShaderEffectProperty.ConstantBuffer,
                type: D2D1_PROPERTY_TYPE.D2D1_PROPERTY_TYPE_BLOB,
                data: p,
                dataSize: (uint)constantBufferSize).Assert();
        }

        T shader = T.CreateFromConstantBuffer(buffer);

        if (bufferArray is not null)
        {
            ArrayPool<byte>.Shared.Return(bufferArray);
        }

        return shader;
    }

    /// <summary>
    /// Sets the constant buffer of type <typeparamref name="T"/> from a given <see cref="ID2D1Effect"/> object.
    /// </summary>
    /// <typeparam name="T">The type of shader being used.</typeparam>
    /// <param name="d2D1Effect">The input <see cref="ID2D1Effect"/> instance.</param>
    /// <param name="value">The constant buffer value to set.</param>
    public static void SetConstantBuffer<T>(this ref ID2D1Effect d2D1Effect, in T value)
        where T : unmanaged, ID2D1PixelShader, ID2D1PixelShaderDescriptor<T>
    {
        // Same as above, simply do nothing for empty types. This case is still handled to
        // avoid crashing when someone's just setting an empty constant buffer, which is
        // unnecessary but still valid (eg. a stateless shader). This could be the case if
        // someone was eg. binding or using some other automated system to set constant buffer.
        if (T.ConstantBufferSize == 0)
        {
            return;
        }

        D2D1PixelShaderEffect.SetConstantBufferForD2D1Effect((ID2D1Effect*)Unsafe.AsPointer(ref d2D1Effect), in value);
    }

    /// <summary>
    /// Gets the <see cref="D2D1DrawTransformMapper{T}"/> instance from a given <see cref="ID2D1Effect"/> object.
    /// </summary>
    /// <typeparam name="T">The type of shader being used.</typeparam>
    /// <param name="d2D1Effect">The input <see cref="ID2D1Effect"/> instance.</param>
    /// <returns>The <see cref="D2D1DrawTransformMapper{T}"/> instance for <paramref name="d2D1Effect"/>.</returns>
    public static D2D1DrawTransformMapper<T>? GetTransformMapper<T>(this ref ID2D1Effect d2D1Effect)
        where T : unmanaged, ID2D1PixelShader, ID2D1PixelShaderDescriptor<T>
    {
        using ComPtr<ID2D1DrawTransformMapper> d2D1TransformMapper = default;

        // Get the ID2D1DrawTransformMapper object from the effect
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
        using ComPtr<ID2D1DrawTransformMapperInternal> d2D1TransformMapperInternal = default;

        d2D1TransformMapper.CopyTo(d2D1TransformMapperInternal.GetAddressOf()).Assert();

        IntPtr handlePtr;

        // Get the underlying GCHandle value
        d2D1TransformMapperInternal.Get()->GetManagedWrapperHandle((void**)&handlePtr).Assert();

        // Retrieve the managed wrapper from the GCHandle
        return (D2D1DrawTransformMapper<T>)GCHandle.FromIntPtr(handlePtr).Target!;
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
        return default(InvalidOperationException).Throw<D2D1ResourceTextureManager?>();
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