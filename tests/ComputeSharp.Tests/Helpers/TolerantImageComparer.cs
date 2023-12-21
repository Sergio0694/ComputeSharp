using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Advanced;
using ImageSharpRgba32 = SixLabors.ImageSharp.PixelFormats.Rgba32;

namespace ComputeSharp.Tests.Helpers;

/// <summary>
/// An tolerant image comparer type, from <see href="https://github.com/SixLabors/ImageSharp">ImageSharp</see>.
/// </summary>
internal static class TolerantImageComparer
{
    /// <summary>
    /// Asserts that two images are equal.
    /// </summary>
    /// <param name="expectedPath">The path to the reference image.</param>
    /// <param name="actualPath">The path to the expected image.</param>
    /// <param name="threshold">The allowed difference threshold for the normalized delta.</param>
    public static void AssertEqual(string expectedPath, string actualPath, float threshold)
    {
        using Image<ImageSharpRgba32> expected = Image.Load<ImageSharpRgba32>(expectedPath);
        using Image<ImageSharpRgba32> actual = Image.Load<ImageSharpRgba32>(actualPath);

        AssertEqual(expected, actual, threshold);
    }

    /// <summary>
    /// Asserts that two images are equal.
    /// </summary>
    /// <typeparam name="TPixel">The type of image pixels to analyze.</typeparam>
    /// <param name="expected">The reference image.</param>
    /// <param name="actual">The expected image.</param>
    /// <param name="threshold">The allowed difference threshold for the normalized delta.</param>
    public static void AssertEqual<TPixel>(Image<TPixel> expected, Image<TPixel> actual, float threshold)
        where TPixel : unmanaged, IPixel<TPixel>
    {
        if (expected.Size != actual.Size)
        {
            Assert.Fail($"The input images have different sizes: {expected.Size} and {actual.Size}");
        }

        if (expected.Frames.Count != actual.Frames.Count ||
            expected.Frames.Count != 1 ||
            actual.Frames.Count != 1)
        {
            Assert.Fail("The two input images must have 1 frame each");
        }

        int width = actual.Width;

        float totalDifference = 0F;

        List<PixelDifference> differences = new(20);
        Span<ImageSharpRgba32> aBuffer = new ImageSharpRgba32[actual.Width];
        Span<ImageSharpRgba32> bBuffer = new ImageSharpRgba32[actual.Width];

        for (int y = 0; y < actual.Height; y++)
        {
            Memory<TPixel> aMemory = expected.DangerousGetPixelRowMemory(y);
            Memory<TPixel> bMemory = actual.DangerousGetPixelRowMemory(y);

            PixelOperations<TPixel>.Instance.ToRgba32(actual.Configuration, aMemory.Span, aBuffer);
            PixelOperations<TPixel>.Instance.ToRgba32(actual.Configuration, bMemory.Span, bBuffer);

            for (int x = 0; x < width; x++)
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                static int GetManhattanDistanceInRgbaSpace(ref ImageSharpRgba32 a, ref ImageSharpRgba32 b)
                {
                    [MethodImpl(MethodImplOptions.AggressiveInlining)]
                    static int Diff(ushort a, ushort b) => Math.Abs(a - b);

                    return Diff(a.R, b.R) + Diff(a.G, b.G) + Diff(a.B, b.B) + Diff(a.A, b.A);
                }

                int d = GetManhattanDistanceInRgbaSpace(ref aBuffer[x], ref bBuffer[x]);

                if (d > 0)
                {
                    if (differences.Count < 20)
                    {
                        PixelDifference diff = new(new Point(x, y), aBuffer[x], bBuffer[x]);

                        differences.Add(diff);
                    }

                    totalDifference += d;
                }
            }
        }

        float normalizedDifference = totalDifference / (actual.Width * (float)actual.Height);

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
    private readonly struct PixelDifference(Point position, ImageSharpRgba32 expected, ImageSharpRgba32 actual)
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