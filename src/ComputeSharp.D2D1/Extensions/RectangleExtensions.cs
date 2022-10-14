using System.Drawing;
using System.Runtime.CompilerServices;

namespace ComputeSharp.D2D1;

/// <summary>
/// Helper methods for <see cref="Rectangle"/>.
/// </summary>
public static class RectangleExtensions
{
    /// <summary>
    /// Modifies a target <see cref="Rectangle"/> instance to be a D2D1 infinite rectangle.
    /// </summary>
    /// <param name="rectangle">The target <see cref="Rectangle"/> instance to modify.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ToD2D1Infinite(this ref Rectangle rectangle)
    {
        rectangle = new Rectangle(
            int.MinValue / 2,
            int.MinValue / 2,
            int.MaxValue,
            int.MaxValue);
    }

    /// <summary>
    /// Checks whether a given <see cref="Rectangle"/> instance represents a D2D1 infinite rectangle.
    /// </summary>
    /// <param name="rectangle">The input <see cref="Rectangle"/> instance to check.</param>
    /// <returns>Whether or not <paramref name="rectangle"/> represents a D2D1 infinite rectangle.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsD2D1Infinite(this in Rectangle rectangle)
    {
        return rectangle == new Rectangle(
            int.MinValue / 2,
            int.MinValue / 2,
            int.MaxValue,
            int.MaxValue);
    }
}