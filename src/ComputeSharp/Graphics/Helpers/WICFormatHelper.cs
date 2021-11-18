using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using Microsoft.Toolkit.Diagnostics;
using TerraFX.Interop.Windows;
#if NET6_0_OR_GREATER
using Path = System.IO.Path;
#else
using Path = ComputeSharp.NetStandard.System.IO.Path;
#endif

namespace ComputeSharp.Graphics.Helpers;

/// <summary>
/// A helper type with utility methods for WIC formats.
/// </summary>
internal static class WICFormatHelper
{
    /// <summary>
    /// Gets the appropriate WIC format <see cref="Guid"/> value for the input type argument.
    /// </summary>
    /// <typeparam name="T">The input type argument to get the corresponding WIC format <see cref="Guid"/>.</typeparam>
    /// <returns>The WIC format <see cref="Guid"/> value corresponding to <typeparamref name="T"/>.</returns>
    /// <exception cref="ArgumentException">Thrown when the input type <typeparamref name="T"/> is not supported.</exception>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Guid GetForType<T>()
        where T : unmanaged
    {
        if (typeof(T) == typeof(Bgra32)) return GUID.GUID_WICPixelFormat32bppBGRA;
        else if (typeof(T) == typeof(Rgba32)) return GUID.GUID_WICPixelFormat32bppRGBA;
        else if (typeof(T) == typeof(Rgba64)) return GUID.GUID_WICPixelFormat64bppRGBA;
        else if (typeof(T) == typeof(R8)) return GUID.GUID_WICPixelFormat8bppGray;
        else if (typeof(T) == typeof(R16)) return GUID.GUID_WICPixelFormat16bppGray;
        else return ThrowHelper.ThrowArgumentException<Guid>("Invalid texture type");
    }

    /// <summary>
    /// Gets the appropriate WIC format <see cref="Guid"/> value for the input type argument.
    /// </summary>
    /// <typeparam name="T">The input type argument to get the corresponding WIC format <see cref="Guid"/>.</typeparam>
    /// <returns>The WIC format <see cref="Guid"/> value corresponding to <typeparamref name="T"/>.</returns>
    /// <exception cref="ArgumentException">Thrown when the input type <typeparamref name="T"/> is not supported.</exception>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryGetIntermediateFormatForType<T>(Guid containerFormat, out Guid intermediateFormat)
        where T : unmanaged
    {
        if (containerFormat == GUID.GUID_ContainerFormatBmp)
        {
            if (typeof(T) == typeof(Bgra32)) intermediateFormat = default;
            else if (typeof(T) == typeof(Rgba32)) intermediateFormat = GUID.GUID_WICPixelFormat32bppBGRA;
            else if (typeof(T) == typeof(Rgba64)) intermediateFormat = GUID.GUID_WICPixelFormat32bppBGRA;
            else if (typeof(T) == typeof(R8)) intermediateFormat = GUID.GUID_WICPixelFormat24bppBGR;
            else if (typeof(T) == typeof(R16)) intermediateFormat = GUID.GUID_WICPixelFormat24bppBGR;
            else intermediateFormat = ThrowHelper.ThrowArgumentException<Guid>("Invalid texture type");
        }
        else if (containerFormat == GUID.GUID_ContainerFormatPng)
        {
            if (typeof(T) == typeof(Bgra32)) intermediateFormat = default;
            else if (typeof(T) == typeof(Rgba32)) intermediateFormat = GUID.GUID_WICPixelFormat32bppBGRA;
            else if (typeof(T) == typeof(Rgba64)) intermediateFormat = default;
            else if (typeof(T) == typeof(R8)) intermediateFormat = default;
            else if (typeof(T) == typeof(R16)) intermediateFormat = GUID.GUID_WICPixelFormat8bppGray;
            else intermediateFormat = ThrowHelper.ThrowArgumentException<Guid>("Invalid texture type");
        }
        else if (containerFormat == GUID.GUID_ContainerFormatJpeg)
        {
            if (typeof(T) == typeof(Bgra32)) intermediateFormat = GUID.GUID_WICPixelFormat24bppBGR;
            else if (typeof(T) == typeof(Rgba32)) intermediateFormat = GUID.GUID_WICPixelFormat24bppBGR;
            else if (typeof(T) == typeof(Rgba64)) intermediateFormat = GUID.GUID_WICPixelFormat24bppBGR;
            else if (typeof(T) == typeof(R8)) intermediateFormat = default;
            else if (typeof(T) == typeof(R16)) intermediateFormat = GUID.GUID_WICPixelFormat8bppGray;
            else intermediateFormat = ThrowHelper.ThrowArgumentException<Guid>("Invalid texture type");
        }
        else if (containerFormat == GUID.GUID_ContainerFormatWmp)
        {
            if (typeof(T) == typeof(Bgra32)) intermediateFormat = default;
            else if (typeof(T) == typeof(Rgba32)) intermediateFormat = GUID.GUID_WICPixelFormat32bppBGRA;
            else if (typeof(T) == typeof(Rgba64)) intermediateFormat = default;
            else if (typeof(T) == typeof(R8)) intermediateFormat = default;
            else if (typeof(T) == typeof(R16)) intermediateFormat = default;
            else intermediateFormat = ThrowHelper.ThrowArgumentException<Guid>("Invalid texture type");
        }
        else if (containerFormat == GUID.GUID_ContainerFormatTiff)
        {
            if (typeof(T) == typeof(Bgra32)) intermediateFormat = default;
            else if (typeof(T) == typeof(Rgba32)) intermediateFormat = GUID.GUID_WICPixelFormat32bppBGRA;
            else if (typeof(T) == typeof(Rgba64)) intermediateFormat = default;
            else if (typeof(T) == typeof(R8)) intermediateFormat = default;
            else if (typeof(T) == typeof(R16)) intermediateFormat = default;
            else intermediateFormat = ThrowHelper.ThrowArgumentException<Guid>("Invalid texture type");
        }
        else if (containerFormat == GUID.GUID_ContainerFormatDds)
        {
            if (typeof(T) == typeof(Bgra32)) intermediateFormat = default;
            else if (typeof(T) == typeof(Rgba32)) intermediateFormat = GUID.GUID_WICPixelFormat32bppBGRA;
            else if (typeof(T) == typeof(Rgba64)) intermediateFormat = GUID.GUID_WICPixelFormat32bppBGRA;
            else if (typeof(T) == typeof(R8)) intermediateFormat = GUID.GUID_WICPixelFormat32bppBGRA;
            else if (typeof(T) == typeof(R16)) intermediateFormat = GUID.GUID_WICPixelFormat32bppBGRA;
            else intermediateFormat = ThrowHelper.ThrowArgumentException<Guid>("Invalid texture type");
        }
        else
        {
            intermediateFormat = ThrowHelper.ThrowArgumentException<Guid>("Invalid container format");
        }

        return intermediateFormat != default;
    }

    /// <summary>
    /// Gets the appropriate WIC container format <see cref="Guid"/> value for the input filename.
    /// </summary>
    /// <param name="filename">The target filename to get the container format for.</param>
    /// <returns>The WIC format container <see cref="Guid"/> value matching <paramref name="filename"/>.</returns>
    /// <exception cref="ArgumentException">Thrown when the input filename doesn't have a valid file extension.</exception>
    [Pure]
    public static Guid GetForFilename(ReadOnlySpan<char> filename)
    {
        return Path.GetExtension(filename).ToString() switch
        {
            ".bmp" => GUID.GUID_ContainerFormatBmp,
            ".png" => GUID.GUID_ContainerFormatPng,
            ".jpg" or
            ".jpeg" => GUID.GUID_ContainerFormatJpeg,
            ".jxr" or
            ".hdp" or
            ".wdp" or
            ".wmp" => GUID.GUID_ContainerFormatWmp,
            ".tiff" => GUID.GUID_ContainerFormatTiff,
            ".dds" => GUID.GUID_ContainerFormatDds,
            _ => ThrowHelper.ThrowArgumentException<Guid>("Invalid filename"),
        };
    }
}
