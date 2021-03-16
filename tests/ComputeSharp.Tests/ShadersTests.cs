using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using ComputeSharp.SwapChain.Shaders;
using ComputeSharp.Tests.Attributes;
using ComputeSharp.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using ImageSharpRgba32 = SixLabors.ImageSharp.PixelFormats.Rgba32;

namespace ComputeSharp.Tests
{
    [TestClass]
    [TestCategory("Shaders")]
    public class ShadersTests
    {
        [CombinatorialTestMethod]
        [AllDevices]
        public void ColorfulInfinity(Device device)
        {
            RunAndCompareShader(device, static texture => new ColorfulInfinity(texture, 0), 0.0000004f);
        }

        [CombinatorialTestMethod]
        [AllDevices]
        public void FractalTiling(Device device)
        {
            RunAndCompareShader(device, static texture => new FractalTiling(texture, 0), 0.0000005f);
        }

        [CombinatorialTestMethod]
        [AllDevices]
        public void MengerJourney(Device device)
        {
            RunAndCompareShader(device, static texture => new MengerJourney(texture, 0), 0.000011f);
        }

        [CombinatorialTestMethod]
        [AllDevices]
        public void TwoTiledTruchet(Device device)
        {
            RunAndCompareShader(device, static texture => new TwoTiledTruchet(texture, 0), 0.00032f);
        }

        [CombinatorialTestMethod]
        [AllDevices]
        public void Octagrams(Device device)
        {
            RunAndCompareShader(device, static texture => new Octagrams(texture, 0), 0.0000006f);
        }

        [CombinatorialTestMethod]
        [AllDevices]
        public void ProteanClouds(Device device)
        {
            RunAndCompareShader(device, static texture => new ProteanClouds(texture, 0), 0.0000004f);
        }

        [CombinatorialTestMethod]
        [AllDevices]
        public void ExtrudedTruchetPattern(Device device)
        {
            RunAndCompareShader(device, static texture => new ExtrudedTruchetPattern(texture, 0), 0.00011f);
        }

        /// <summary>
        /// Executes a given test for a specified shader.
        /// </summary>
        /// <typeparam name="T">The type of shader to test.</typeparam>
        /// <param name="device">The device to use.</param>
        /// <param name="factory">A producer to create an instance of the shader to run.</param>
        /// <param name="delta">The comparison delta.</param>
        private static void RunAndCompareShader<T>(Device device, Func<ReadWriteTexture2D<Rgba32, Float4>, T> factory, float delta)
            where T : struct, IComputeShader
        {
            _ = device.Get();

            string expectedPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Assets", $"{typeof(T).Name}.png");

            IImageInfo imageInfo = Image.Identify(expectedPath);

            using Image<ImageSharpRgba32> image = new(imageInfo.Width, imageInfo.Height);

            using (ReadWriteTexture2D<Rgba32, Float4> texture = device.Get().AllocateReadWriteTexture2D<Rgba32, Float4>(imageInfo.Width, imageInfo.Height))
            {
                device.Get().For(texture.Width, texture.Height, factory(texture));

                _ = image.TryGetSinglePixelSpan(out Span<ImageSharpRgba32> span);

                texture.CopyTo(MemoryMarshal.Cast<ImageSharpRgba32, Rgba32>(span));
            }

            string actualPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Shaders", $"{typeof(T).Name}.png");

            _ = Directory.CreateDirectory(Path.GetDirectoryName(actualPath)!);

            image.SaveAsPng(actualPath, new PngEncoder() { CompressionLevel = PngCompressionLevel.BestCompression, ColorType = PngColorType.Rgb });

            ImagingTests.TolerantImageComparer.AssertEqual(expectedPath, actualPath, delta);
        }
    }
}
