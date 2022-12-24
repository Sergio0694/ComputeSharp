using System.Runtime.CompilerServices;

namespace ComputeSharp.SwapChain.Core.Converters;

/// <summary>
/// A class with some static converters for <see cref="bool"/> values.
/// </summary>
public static class BoolConverter
{
    /// <summary>
    /// Negates an input <see cref="bool"/> value.
    /// </summary>
    /// <param name="value">The <see cref="bool"/> value to negate.</param>
    /// <returns>The negated value of <paramref name="value"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Not(bool value)
    {
        return !value;
    }
}