using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using ComputeSharp.BokehBlur.Processors;
using ComputeSharp.Tests.Attributes;
using ComputeSharp.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using ImageSharpRgba32 = SixLabors.ImageSharp.PixelFormats.Rgba32;

namespace ComputeSharp.Tests
{
    [TestClass]
    [TestCategory("Imaging")]
    public class ImagingTests
    {
        [CombinatorialTestMethod]
        [AllDevices]
        public void BokehBlur(Device device)
        {
            // Early test to ensure the device is available. This saves time when running the
            // unit test if the target device is not available, as we skip the preprocessing.
            _ = device.Get();

            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Imaging");

            using var original = Image.Load<ImageSharpRgba32>(Path.Combine(path, "city.jpg"));
            using var cpu = original.Clone(c => c.BokehBlur(80, 2, 3));
            using var gpu = original.Clone(c => c.ApplyProcessor(new HlslBokehBlurProcessor(device.Get(), 80, 2)));

            string
                expectedPath = Path.Combine(path, "city_bokeh_cpu.jpg"),
                actualPath = Path.Combine(path, "city_bokeh_gpu.jpg");

            cpu.Save(expectedPath);
            gpu.Save(actualPath);

            TolerantImageComparer.AssertEqual(expectedPath, actualPath, 0.000009f);
        }

        [CombinatorialTestMethod]
        [AllDevices]
        public void GaussianBlur(Device device)
        {
            _ = device.Get();

            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Imaging");

            using var original = Image.Load<ImageSharpRgba32>(Path.Combine(path, "city.jpg"));
            using var cpu = original.Clone(c => c.GaussianBlur(30f));
            using var gpu = original.Clone(c => c.ApplyProcessor(new HlslGaussianBlurProcessor(device.Get(), 90)));

            string
                expectedPath = Path.Combine(path, "city_gaussian_cpu.jpg"),
                actualPath = Path.Combine(path, "city_gaussian_gpu.jpg");

            cpu.Save(expectedPath);
            gpu.Save(actualPath);

            TolerantImageComparer.AssertEqual(expectedPath, actualPath, 0.000003f);
        }

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
                using var expected = Image.Load<ImageSharpRgba32>(expectedPath);
                using var actual = Image.Load<ImageSharpRgba32>(actualPath);

                if (expected.Size() != actual.Size())
                {
                    Assert.Fail($"The input images have different sizes: {expected.Size()} and {actual.Size()}");
                }

                if (expected.Frames.Count != actual.Frames.Count ||
                    expected.Frames.Count != 1 ||
                    actual.Frames.Count != 1)
                {
                    Assert.Fail("The two input images must have 1 frame each");
                }

                int width = actual.Width;

                float totalDifference = 0F;

                var differences = new List<PixelDifference>(20);

                for (int y = 0; y < actual.Height; y++)
                {
                    Span<ImageSharpRgba32> aSpan = expected.GetPixelRowSpan(y);
                    Span<ImageSharpRgba32> bSpan = actual.GetPixelRowSpan(y);

                    for (int x = 0; x < width; x++)
                    {
                        [MethodImpl(MethodImplOptions.AggressiveInlining)]
                        static int GetManhattanDistanceInRgbaSpace(ref ImageSharpRgba32 a, ref ImageSharpRgba32 b)
                        {
                            [MethodImpl(MethodImplOptions.AggressiveInlining)]
                            static int Diff(ushort a, ushort b) => Math.Abs(a - b);

                            return Diff(a.R, b.R) + Diff(a.G, b.G) + Diff(a.B, b.B) + Diff(a.A, b.A);
                        }

                        int d = GetManhattanDistanceInRgbaSpace(ref aSpan[x], ref bSpan[x]);

                        if (d > 0)
                        {
                            if (differences.Count < 20)
                            {
                                var diff = new PixelDifference(new Point(x, y), aSpan[x], bSpan[x]);
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
                    builder.AppendLine($"The input images don't match. Normalized delta: {normalizedDifference}%");
                    builder.AppendLine($"First {differences.Count} pixel deltas:");

                    foreach (var delta in differences)
                    {
                        builder.AppendLine(delta.ToString());
                    }

                    Assert.Fail(builder.ToString());
                }
            }

            /// <summary>
            /// A simple model repressenting info about a difference between two pixels.
            /// </summary>
            private readonly struct PixelDifference
            {
                public PixelDifference(Point position, ImageSharpRgba32 expected, ImageSharpRgba32 actual)
                {
                    this.Position = position;
                    this.RedDifference = actual.R - expected.R;
                    this.GreenDifference = actual.G - expected.G;
                    this.BlueDifference = actual.B - expected.B;
                    this.AlphaDifference = actual.A - expected.A;
                }

                public readonly Point Position;

                public readonly int RedDifference;

                public readonly int GreenDifference;

                public readonly int BlueDifference;

                public readonly int AlphaDifference;

                /// <inheritdoc/>
                public override string ToString()
                {
                    return $"[delta({RedDifference},{GreenDifference},{BlueDifference},{AlphaDifference}) @ ({Position.X},{Position.Y})]";
                }
            }
        }
    }
}
