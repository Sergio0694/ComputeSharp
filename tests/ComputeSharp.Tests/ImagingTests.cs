using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using ComputeSharp.Tests.Attributes;
using ComputeSharp.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SixLabors.ImageSharp;
using ImageSharpRgba32 = SixLabors.ImageSharp.PixelFormats.Rgba32;
using ImageSharpL8 = SixLabors.ImageSharp.PixelFormats.L8;

namespace ComputeSharp.Tests
{
    [TestClass]
    [TestCategory("Imaging")]
    public class ImagingTests
    {
        [CombinatorialTestMethod]
        [AllDevices]
        public void LoadAsRgba32FromFile(Device device)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Imaging", "city.jpg");

            using ReadOnlyTexture2D<Rgba32, Float4> texture = device.Get().AllocateReadOnlyTexture2D<Rgba32, Float4>(path);

            using Image<ImageSharpRgba32> loaded = texture.ToImage();
            using Image<ImageSharpRgba32> original = Image.Load<ImageSharpRgba32>(path);

            TolerantImageComparer.AssertEqual(original, loaded, 0.0000032f);
        }

        [CombinatorialTestMethod]
        [AllDevices]
        public void LoadAsRgba32FromBuffer(Device device)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Imaging", "city.jpg");

            byte[] buffer = File.ReadAllBytes(path);

            using ReadOnlyTexture2D<Rgba32, Float4> texture = device.Get().AllocateReadOnlyTexture2D<Rgba32, Float4>(buffer);

            using Image<ImageSharpRgba32> loaded = texture.ToImage();
            using Image<ImageSharpRgba32> original = Image.Load<ImageSharpRgba32>(path);

            TolerantImageComparer.AssertEqual(original, loaded, 0.0000032f);
        }

        [CombinatorialTestMethod]
        [AllDevices]
        public void SaveRgba32AsJpeg(Device device)
        {
            string
                path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Imaging"),
                expectedPath = Path.Combine(path, "city.jpg"),
                actualPath = Path.Combine(path, "city_rgba32_saved.jpg");

            using ReadOnlyTexture2D<Rgba32, Float4> texture = device.Get().AllocateReadOnlyTexture2D<Rgba32, Float4>(expectedPath);

            texture.Save(actualPath);

            TolerantImageComparer.AssertEqual(expectedPath, actualPath, 0.00001023f);
        }

        [CombinatorialTestMethod]
        [AllDevices]
        public void SaveBgra32AsJpeg(Device device)
        {
            string
                path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Imaging"),
                expectedPath = Path.Combine(path, "city.jpg"),
                actualPath = Path.Combine(path, "city_bgra32_saved.jpg");

            using ReadOnlyTexture2D<Bgra32, Float4> texture = device.Get().AllocateReadOnlyTexture2D<Bgra32, Float4>(expectedPath);

            texture.Save(actualPath);

            TolerantImageComparer.AssertEqual(expectedPath, actualPath, 0.00001023f);
        }

        [CombinatorialTestMethod]
        [AllDevices]
        public void SaveR8AsJpeg(Device device)
        {
            string
                path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Imaging"),
                sourcePath = Path.Combine(path, "city.jpg"),
                expectedPath = Path.Combine(path, "city_r8_reference.jpg"),
                actualPath = Path.Combine(path, "city_r8_saved.jpg");

            using ReadOnlyTexture2D<R8, float> texture = device.Get().AllocateReadOnlyTexture2D<R8, float>(sourcePath);

            texture.Save(actualPath);

            using Image<ImageSharpL8> original = Image.Load<ImageSharpL8>(sourcePath);

            original.Save(expectedPath);

            TolerantImageComparer.AssertEqual(expectedPath, actualPath, 0.00004037f);
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

                AssertEqual(expected, actual, threshold);
            }

            /// <summary>
            /// Asserts that two images are equal.
            /// </summary>
            /// <param name="expected">The reference image.</param>
            /// <param name="actual">The expected image.</param>
            /// <param name="threshold">The allowed difference threshold for the normalized delta.</param>
            public static void AssertEqual(Image<ImageSharpRgba32> expected, Image<ImageSharpRgba32> actual, float threshold)                
            {
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
