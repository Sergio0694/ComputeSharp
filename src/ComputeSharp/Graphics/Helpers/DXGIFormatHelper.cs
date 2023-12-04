using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using ComputeSharp.Win32;
using static ComputeSharp.Win32.DXGI_FORMAT;

#pragma warning disable IDE0011

namespace ComputeSharp.Graphics.Helpers;

/// <summary>
/// A helper type with utility methods for <see cref="DXGI_FORMAT"/>.
/// </summary>
internal static class DXGIFormatHelper
{
    /// <summary>
    /// Gets the appropriate <see cref="DXGI_FORMAT"/> value for the input type argument.
    /// </summary>
    /// <typeparam name="T">The input type argument to get the corresponding <see cref="DXGI_FORMAT"/> for.</typeparam>
    /// <returns>The <see cref="DXGI_FORMAT"/> value corresponding to <typeparamref name="T"/>.</returns>
    /// <exception cref="ArgumentException">Thrown when the input type <typeparamref name="T"/> is not supported.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DXGI_FORMAT GetForType<T>()
        where T : unmanaged
    {
        if (typeof(T) == typeof(int)) return DXGI_FORMAT_R32_SINT;
        if (typeof(T) == typeof(Int2)) return DXGI_FORMAT_R32G32_SINT;
        if (typeof(T) == typeof(Int3)) return DXGI_FORMAT_R32G32B32_SINT;
        if (typeof(T) == typeof(Int4)) return DXGI_FORMAT_R32G32B32A32_SINT;
        if (typeof(T) == typeof(uint)) return DXGI_FORMAT_R32_UINT;
        if (typeof(T) == typeof(UInt2)) return DXGI_FORMAT_R32G32_UINT;
        if (typeof(T) == typeof(UInt3)) return DXGI_FORMAT_R32G32B32_UINT;
        if (typeof(T) == typeof(UInt4)) return DXGI_FORMAT_R32G32B32A32_UINT;
        if (typeof(T) == typeof(float)) return DXGI_FORMAT_R32_FLOAT;
        if (typeof(T) == typeof(Float2)) return DXGI_FORMAT_R32G32_FLOAT;
        if (typeof(T) == typeof(Float3)) return DXGI_FORMAT_R32G32B32_FLOAT;
        if (typeof(T) == typeof(Float4)) return DXGI_FORMAT_R32G32B32A32_FLOAT;
        if (typeof(T) == typeof(Vector2)) return DXGI_FORMAT_R32G32_FLOAT;
        if (typeof(T) == typeof(Vector3)) return DXGI_FORMAT_R32G32B32_FLOAT;
        if (typeof(T) == typeof(Vector4)) return DXGI_FORMAT_R32G32B32A32_FLOAT;
        if (typeof(T) == typeof(Bgra32)) return DXGI_FORMAT_B8G8R8A8_UNORM;
        if (typeof(T) == typeof(Rgba32)) return DXGI_FORMAT_R8G8B8A8_UNORM;
        if (typeof(T) == typeof(Rgba64)) return DXGI_FORMAT_R16G16B16A16_UNORM;
        if (typeof(T) == typeof(R8)) return DXGI_FORMAT_R8_UNORM;
        if (typeof(T) == typeof(R16)) return DXGI_FORMAT_R16_UNORM;
        if (typeof(T) == typeof(Rg16)) return DXGI_FORMAT_R8G8_UNORM;
        if (typeof(T) == typeof(Rg32)) return DXGI_FORMAT_R16G16_UNORM;

        return default(ArgumentException).Throw<DXGI_FORMAT>(nameof(T));
    }

    /// <summary>
    /// Gets whether or not the input type corresponds to a normalized format.
    /// </summary>
    /// <typeparam name="T">The input type argument to check.</typeparam>
    /// <returns>Whether or not the input type corresponds to a normalized format.</returns>
    /// <exception cref="ArgumentException">Thrown when the input type <typeparamref name="T"/> is not supported.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNormalizedType<T>()
        where T : unmanaged
    {
        if (typeof(T) == typeof(int) ||
            typeof(T) == typeof(Int2) ||
            typeof(T) == typeof(Int3) ||
            typeof(T) == typeof(Int4) ||
            typeof(T) == typeof(uint) ||
            typeof(T) == typeof(UInt2) ||
            typeof(T) == typeof(UInt3) ||
            typeof(T) == typeof(UInt4) ||
            typeof(T) == typeof(float) ||
            typeof(T) == typeof(Float2) ||
            typeof(T) == typeof(Float3) ||
            typeof(T) == typeof(Float4) ||
            typeof(T) == typeof(Vector2) ||
            typeof(T) == typeof(Vector3) ||
            typeof(T) == typeof(Vector4))
        {
            return false;
        }

        if (typeof(T) == typeof(Bgra32) ||
            typeof(T) == typeof(Rgba32) ||
            typeof(T) == typeof(Rgba64) ||
            typeof(T) == typeof(R8) ||
            typeof(T) == typeof(R16) ||
            typeof(T) == typeof(Rg16) ||
            typeof(T) == typeof(Rg32))
        {
            return true;
        }

        return default(ArgumentException).Throw<bool>(nameof(T));
    }

    /// <summary>
    /// Extends a given pixel type to its <see cref="Float4"/> equivalent.
    /// </summary>
    /// <typeparam name="T">The input pixel value to convert.</typeparam>
    /// <returns>The <see cref="Float4"/> equivalent value for <paramref name="value"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float4 ExtendToNormalizedValue<T>(T value)
        where T : unmanaged
    {
        if (typeof(T) == typeof(Float4))
        {
            return Unsafe.As<T, Float4>(ref value);
        }

        Float4 result = default;

        Unsafe.As<Float4, T>(ref result) = value;

        return result;
    }
}