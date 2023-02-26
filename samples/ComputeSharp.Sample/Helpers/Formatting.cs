using System;
using System.Globalization;
using System.Linq;

namespace ComputeSharp.Sample;

/// <summary>
/// Internal helpers for the sample to format values.
/// </summary>
internal static class Formatting
{
    /// <summary>
    /// Prints a matrix in a properly formatted way.
    /// </summary>
    /// <param name="array">The input <see langword="float"/> array representing the matrix to print.</param>
    /// <param name="width">The width of the array to print.</param>
    /// <param name="height">The height of the array to print.</param>
    /// <param name="name">The name of the matrix to print.</param>
    public static void PrintMatrix(float[] array, int width, int height, string name)
    {
        int pad = 48 - name.Length;
        string title = $"{new string('=', pad / 2)} {name} {new string('=', (pad + 1) / 2)}";

        Console.WriteLine(title);

        int numberWidth = Math.Max(array.Max().ToString(CultureInfo.InvariantCulture).Length, 4);

        for (int i = 0; i < height; i++)
        {
            float[] row = array.AsSpan(i * width, width).ToArray();
            string text = string.Join(",", row.Select(x => x.ToString(CultureInfo.InvariantCulture).PadLeft(numberWidth)));

            Console.WriteLine(text);
        }

        Console.WriteLine(new string('=', 50));
    }
}