using System.Drawing;
using System.Runtime.CompilerServices;
using TerraFX.Interop.Windows;
#if NET6_0_OR_GREATER
using Math = System.Math;
#else
using Math = ComputeSharp.D2D1.NetStandard.System.Math;
#endif

namespace ComputeSharp.D2D1.Extensions;

/// <summary>
/// Helper methods for <see cref="RECT"/> and <see cref="Rectangle"/>.
/// </summary>
internal static class RECTExtensions
{
    /// <summary>
    /// Creates a <see cref="Rectangle"/> from a <see cref="RECT"/> value.
    /// </summary>
    /// <param name="rect">The input <see cref="RECT"/> value.</param>
    /// <returns>The resulting <see cref="Rectangle"/> value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Rectangle ToRectangle(this in RECT rect)
    {
        int left = Math.Clamp(rect.left, int.MinValue / 2, int.MaxValue / 2);
        int top = Math.Clamp(rect.top, int.MinValue / 2, int.MaxValue / 2);
        int right = Math.Clamp(rect.right, int.MinValue / 2, int.MaxValue / 2);
        int bottom = Math.Clamp(rect.bottom, int.MinValue / 2, int.MaxValue / 2);

        int minX = Math.Min(left, right);
        int minY = Math.Min(top, bottom);
        int maxX = Math.Max(left, right);
        int maxY = Math.Max(top, bottom);

        return new(minX, minY, maxX - minX, maxY - minY);
    }

    /// <summary>
    /// Creates a <see cref="RECT"/> from a <see cref="Rectangle"/> value.
    /// </summary>
    /// <param name="rectangle">The input <see cref="Rectangle"/> value.</param>
    /// <returns>The resulting <see cref="RECT"/> value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RECT ToRECT(this in Rectangle rectangle)
    {
        if (rectangle.IsD2D1Infinite())
        {
            return new()
            {
                left = int.MinValue,
                top = int.MinValue,
                right = int.MaxValue,
                bottom = int.MaxValue
            };
        }

        return new()
        {
            left = rectangle.Left,
            top = rectangle.Top,
            right = rectangle.Right,
            bottom = rectangle.Bottom
        };
    }
}
