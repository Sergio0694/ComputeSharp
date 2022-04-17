using System.Drawing;
using System.Runtime.CompilerServices;
using TerraFX.Interop.Windows;

namespace ComputeSharp.D2D1.Extensions;

/// <summary>
/// Helper methods for <see cref="Rectangle"/>.
/// </summary>
internal static class RectangleExtensions
{
    /// <summary>
    /// Creates a <see cref="RECT"/> from a <see cref="Rectangle"/> value.
    /// </summary>
    /// <param name="rectangle">The input <see cref="Rectangle"/> value.</param>
    /// <returns>The resulting <see cref="RECT"/> value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RECT ToRECT(this in Rectangle rectangle)
    {
        RECT result;

        result.left = rectangle.Left;
        result.top = rectangle.Top;
        result.right = rectangle.Right;
        result.bottom = rectangle.Bottom;

        return result;
    }
}
