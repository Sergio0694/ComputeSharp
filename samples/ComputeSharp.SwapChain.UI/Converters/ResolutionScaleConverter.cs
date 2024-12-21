namespace ComputeSharp.SwapChain.Core.Converters;

/// <summary>
/// A class with some static converters for resolution scale settings.
/// </summary>
public static class ResolutionScaleConverter
{
    /// <summary>
    /// Converts a resolution scale to a formatted string.
    /// </summary>
    /// <param name="value">The input resolution scale (in percentage).</param>
    /// <returns>A formatted percentage string for <paramref name="value"/>.</returns>
    public static string ConvertPercentageToFormattedString(int value)
    {
        return $"{value}%";
    }

    /// <summary>
    /// Converts a percentage value to a scaled value in the [0, 1] range.
    /// </summary>
    /// <param name="value">The input percentage value to scale.</param>
    /// <returns>The input percentage scaled to the [0, 1] range.</returns>
    public static double ConvertPercentageToScale(int value)
    {
        return value / 100.0;
    }
}