using System.Drawing;
using System.Runtime.CompilerServices;
using TerraFX.Interop.Windows;

namespace ComputeSharp.D2D1.Extensions;

/// <summary>
/// Helper methods for <see cref="RECT"/>.
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
        return Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);
    }
}
