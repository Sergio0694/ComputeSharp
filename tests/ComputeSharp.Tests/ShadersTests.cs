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
        [Device(Device.Discrete)]
        [Data(typeof(ColorfulInfinity))]
        [Data(typeof(SwapChain.Shaders.Compute.ColorfulInfinity))]
        public void ColorfulInfinity(Device device, Type shaderType)
        {
            RunAndCompareShader(device, shaderType, 0.0000004f);
        }

        [CombinatorialTestMethod]
        [AllDevices]
        [Data(typeof(FractalTiling))]
        [Data(typeof(SwapChain.Shaders.Compute.FractalTiling))]
        public void FractalTiling(Device device, Type shaderType)
        {
            RunAndCompareShader(device, shaderType, 0.0000005f);
        }

        [CombinatorialTestMethod]
        [AllDevices]
        [Data(typeof(MengerJourney))]
        [Data(typeof(SwapChain.Shaders.Compute.MengerJourney))]
        public void MengerJourney(Device device, Type shaderType)
        {
            RunAndCompareShader(device, shaderType, 0.000011f);
        }

        [CombinatorialTestMethod]
        [AllDevices]
        [Data(typeof(TwoTiledTruchet))]
        [Data(typeof(SwapChain.Shaders.Compute.TwoTiledTruchet))]
        public void TwoTiledTruchet(Device device, Type shaderType)
        {
            RunAndCompareShader(device, shaderType, 0.00032f);
        }

        [CombinatorialTestMethod]
        [AllDevices]
        [Data(typeof(Octagrams))]
        [Data(typeof(SwapChain.Shaders.Compute.Octagrams))]
        public void Octagrams(Device device, Type shaderType)
        {
            RunAndCompareShader(device, shaderType, 0.0000006f);
        }

        [CombinatorialTestMethod]
        [AllDevices]
        [Data(typeof(ProteanClouds))]
        [Data(typeof(SwapChain.Shaders.Compute.ProteanClouds))]
        public void ProteanClouds(Device device, Type shaderType)
        {
            RunAndCompareShader(device, shaderType, 0.0000004f);
        }

        [CombinatorialTestMethod]
        [AllDevices]
        [Data(typeof(ExtrudedTruchetPattern))]
        [Data(typeof(SwapChain.Shaders.Compute.ExtrudedTruchetPattern))]
        public void ExtrudedTruchetPattern(Device device, Type shaderType)
        {
            RunAndCompareShader(device, shaderType, 0.00011f);
        }

        [CombinatorialTestMethod]
        [AllDevices]
        [Data(typeof(PyramidPattern))]
        [Data(typeof(SwapChain.Shaders.Compute.PyramidPattern))]
        public void PyramidPattern(Device device, Type shaderType)
        {
            RunAndCompareShader(device, shaderType, 0.00021f);
        }

        [CombinatorialTestMethod]
        [AllDevices]
        [Data(typeof(TriangleGridContouring))]
        [Data(typeof(SwapChain.Shaders.Compute.TriangleGridContouring))]
        public void TriangleGridContouring(Device device, Type shaderType)
        {
            RunAndCompareShader(device, shaderType, 0.0006f);
        }

        /// <summary>
        /// Executes a given test for a specified shader.
        /// </summary>
        /// <typeparam name="T">The type of shader to test.</typeparam>
        /// <param name="device">The device to use.</param>
        /// <param name="delta">The comparison delta.</param>
        private static void RunAndCompareShader(Device device, Type shaderType, float delta)
        {
            _ = device.Get();

            string expectedPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Assets", $"{shaderType.Name}.png");

            IImageInfo imageInfo = Image.Identify(expectedPath);

            using Image<ImageSharpRgba32> image = new(imageInfo.Width, imageInfo.Height);

            using (ReadWriteTexture2D<Rgba32, Float4> texture = device.Get().AllocateReadWriteTexture2D<Rgba32, Float4>(imageInfo.Width, imageInfo.Height))
            {
                if (shaderType.IsAssignableTo(typeof(IComputeShader)))
                {
                    static void RunComputeShader<T>(ReadWriteTexture2D<Rgba32, Float4> texture)
                        where T : struct, IComputeShader
                    {
                        texture.GraphicsDevice.For(texture.Width, texture.Height, (T)Activator.CreateInstance(typeof(T), texture, 0f)!);
                    }

                    var action = new Action<ReadWriteTexture2D<Rgba32, Float4>>(RunComputeShader<SwapChain.Shaders.Compute.ColorfulInfinity>);

                    action.Method.GetGenericMethodDefinition().MakeGenericMethod(shaderType).Invoke(null, new[] { texture });
                }
                else
                {
                    static void RunPixelShader<T>(ReadWriteTexture2D<Rgba32, Float4> texture)
                        where T : struct, IPixelShader<Float4>
                    {
                        texture.GraphicsDevice.ForEach(texture, (T)Activator.CreateInstance(typeof(T), 0f)!);
                    }

                    var action = new Action<ReadWriteTexture2D<Rgba32, Float4>>(RunPixelShader<ColorfulInfinity>);

                    action.Method.GetGenericMethodDefinition().MakeGenericMethod(shaderType).Invoke(null, new[] { texture });
                }

                _ = image.TryGetSinglePixelSpan(out Span<ImageSharpRgba32> span);

                texture.CopyTo(MemoryMarshal.Cast<ImageSharpRgba32, Rgba32>(span));
            }

            string actualPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Shaders", $"{shaderType.Name}.png");

            _ = Directory.CreateDirectory(Path.GetDirectoryName(actualPath)!);

            image.SaveAsPng(actualPath, new PngEncoder() { CompressionLevel = PngCompressionLevel.BestCompression, ColorType = PngColorType.Rgb });

            ImagingTests.TolerantImageComparer.AssertEqual(expectedPath, actualPath, delta);
        }
    }
}
