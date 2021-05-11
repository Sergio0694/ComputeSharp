using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using Microsoft.Toolkit.Diagnostics;
using FX = TerraFX.Interop.Windows;

namespace ComputeSharp.Graphics.Helpers
{
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
            if (typeof(T) == typeof(Bgra32)) return FX.GUID_WICPixelFormat32bppBGRA;
            else if (typeof(T) == typeof(Rgba32)) return FX.GUID_WICPixelFormat32bppRGBA;
            else if (typeof(T) == typeof(Rgba64)) return FX.GUID_WICPixelFormat64bppRGBA;
            else if (typeof(T) == typeof(R8)) return FX.GUID_WICPixelFormat8bppGray;
            else if (typeof(T) == typeof(R16)) return FX.GUID_WICPixelFormat16bppGray;
            else return ThrowHelper.ThrowArgumentException<Guid>("Invalid texture type");
        }
    }
}
