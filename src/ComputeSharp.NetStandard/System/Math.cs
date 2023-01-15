using System.Runtime.CompilerServices;

namespace ComputeSharp.NetStandard;

/// <summary>
/// A polyfill type that mirrors some methods from <see cref="global::System.Math"/> on .NET 6.
/// </summary>
internal static class Math
{
    /// <summary>
    /// Returns <paramref name="value"/> clamped to the inclusive range of <paramref name="min"/> and <paramref name="max"/>.
    /// </summary>
    /// <param name="value">The value to be clamped.</param>
    /// <param name="min">The lower bound of the result.</param>
    /// <param name="max">The upper bound of the result.</param>
    /// <returns>The input value clamped to the inclusive range of <paramref name="min"/> and <paramref name="max"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Clamp(int value, int min, int max)
    {
        if (value < min)
        {
            return min;
        }

        if (value > max)
        {
            return max;
        }

        return value;
    }

    /// <inheritdoc cref="global::System.Math.Min(int, int)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Min(int val1, int val2)
    {
        return global::System.Math.Min(val1, val2);
    }

    /// <inheritdoc cref="global::System.Math.Max(int, int)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Max(int val1, int val2)
    {
        return global::System.Math.Max(val1, val2);
    }
}