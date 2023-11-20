using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Graphics.Canvas;
using Color = Windows.UI.Color;

namespace ComputeSharp.D2D1.WinUI.Tests.Helpers;

/// <summary>
/// An tolerant image comparer type, inspired from <see href="https://github.com/SixLabors/ImageSharp">ImageSharp</see>.
/// </summary>
internal static class TolerantImageComparer
{
    /// <summary>
    /// Asserts that two images are equal.
    /// </summary>
    /// <param name="expected">The reference image.</param>
    /// <param name="actual">The expected image.</param>
    /// <param name="threshold">The allowed difference threshold for the normalized delta.</param>
    public static void AssertEqual(CanvasBitmap expected, CanvasBitmap actual, float threshold)
    {
        if (expected.SizeInPixels.Width != actual.SizeInPixels.Width ||
            expected.SizeInPixels.Height != actual.SizeInPixels.Height)
        {
            Assert.Fail($"The input images have different sizes: {expected.SizeInPixels} and {actual.SizeInPixels}");
        }

        Color[] expectedColors = expected.GetPixelColors();
        Color[] actualColors = actual.GetPixelColors();

        float totalDifference = 0F;

        List<PixelDifference> differences = new(20);

        for (int i = 0; i < expectedColors.Length; i++)
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static int GetManhattanDistanceInRgbaSpace(ref Color a, ref Color b)
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                static int Diff(ushort a, ushort b) => Math.Abs(a - b);

                return Diff(a.R, b.R) + Diff(a.G, b.G) + Diff(a.B, b.B) + Diff(a.A, b.A);
            }

            int d = GetManhattanDistanceInRgbaSpace(ref expectedColors[i], ref actualColors[i]);

            if (d > 0)
            {
                if (differences.Count < 20)
                {
                    int offsetY = (int)(i / expected.SizeInPixels.Width);
                    int offsetX = (int)(i % expected.SizeInPixels.Width);

                    PixelDifference diff = new(new Point(offsetX, offsetY), expectedColors[i], actualColors[i]);

                    differences.Add(diff);
                }

                totalDifference += d;
            }
        }

        float normalizedDifference = totalDifference / (actual.SizeInPixels.Width * (float)actual.SizeInPixels.Height);

        normalizedDifference /= 4F * 65535F;

        if (normalizedDifference > threshold)
        {
            StringBuilder builder = new();

            _ = builder.AppendLine($"The input images don't match. Normalized delta: {normalizedDifference}%");
            _ = builder.AppendLine($"First {differences.Count} pixel deltas:");

            foreach (PixelDifference delta in differences)
            {
                _ = builder.AppendLine(delta.ToString());
            }

            Assert.Fail(builder.ToString());
        }
    }

    /// <summary>
    /// A simple model repressenting info about a difference between two pixels.
    /// </summary>
    private readonly struct PixelDifference(Point position, Color expected, Color actual)
    {
        private readonly int redDifference = actual.R - expected.R;
        private readonly int greenDifference = actual.G - expected.G;
        private readonly int blueDifference = actual.B - expected.B;
        private readonly int alphaDifference = actual.A - expected.A;

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[delta({this.redDifference},{this.greenDifference},{this.blueDifference},{this.alphaDifference}) @ ({position.X},{position.Y})]";
        }
    }
}