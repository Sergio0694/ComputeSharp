using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using CommunityToolkit.HighPerformance.Buffers;
using ComputeSharp.Resources;
using ComputeSharp.Tests.Attributes;
using ComputeSharp.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.PixelFormats;
using ImageSharpRgba32 = SixLabors.ImageSharp.PixelFormats.Rgba32;
using ImageSharpBgra32 = SixLabors.ImageSharp.PixelFormats.Bgra32;
using ImageSharpL8 = SixLabors.ImageSharp.PixelFormats.L8;

namespace ComputeSharp.Tests;

[TestClass]
[TestCategory("Imaging")]
public class ImagingTests
{
    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture2D<,>))]
    [Resource(typeof(ReadWriteTexture2D<,>))]
    [Data(typeof(string))]
    [Data(typeof(ReadOnlySpan<char>))]
    public void LoadAsRgba32_FromFile(Device device, Type textureType, Type inputType)
    {
        string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Imaging", "city.jpg");

        using Texture2D<Rgba32> texture = device.Get().LoadTexture2D<Rgba32, float4>(textureType, inputType, path);

        using Image<ImageSharpRgba32> loaded = texture.ToImage<Rgba32, ImageSharpRgba32>();
        using Image<ImageSharpRgba32> original = Image.Load<ImageSharpRgba32>(path);

        TolerantImageComparer.AssertEqual(original, loaded, 0.0000032f);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture2D<>))]
    [Resource(typeof(ReadWriteTexture2D<>))]
    [Data(typeof(string))]
    [Data(typeof(ReadOnlySpan<char>))]
    public void LoadAsRgba32_FromFile_WithUploadTexture(Device device, Type textureType, Type inputType)
    {
        string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Imaging", "city.jpg");

        IImageInfo imageInfo = Image.Identify(path);

        using Texture2D<Rgba32> texture = device.Get().AllocateTexture2D<Rgba32>(textureType, imageInfo.Width, imageInfo.Height);
        using UploadTexture2D<Rgba32> upload = device.Get().AllocateUploadTexture2D<Rgba32>(imageInfo.Width, imageInfo.Height);

        upload.Load(inputType, path);
        upload.CopyTo(texture);

        using Image<ImageSharpRgba32> loaded = texture.ToImage<Rgba32, ImageSharpRgba32>();
        using Image<ImageSharpRgba32> original = Image.Load<ImageSharpRgba32>(path);

        TolerantImageComparer.AssertEqual(original, loaded, 0.0000032f);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture2D<,>))]
    [Resource(typeof(ReadWriteTexture2D<,>))]
    public void LoadAsRgba32_FromBuffer(Device device, Type textureType)
    {
        string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Imaging", "city.jpg");

        byte[] buffer = File.ReadAllBytes(path);

        using Texture2D<Rgba32> texture = device.Get().LoadTexture2D<Rgba32, float4>(textureType, buffer);

        using Image<ImageSharpRgba32> loaded = texture.ToImage<Rgba32, ImageSharpRgba32>();
        using Image<ImageSharpRgba32> original = Image.Load<ImageSharpRgba32>(path);

        TolerantImageComparer.AssertEqual(original, loaded, 0.0000032f);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture2D<>))]
    [Resource(typeof(ReadWriteTexture2D<>))]
    public void LoadAsRgba32_FromBuffer_WithUploadTexture(Device device, Type textureType)
    {
        string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Imaging", "city.jpg");

        IImageInfo imageInfo = Image.Identify(path);

        using Texture2D<Rgba32> texture = device.Get().AllocateTexture2D<Rgba32>(textureType, imageInfo.Width, imageInfo.Height);
        using UploadTexture2D<Rgba32> upload = device.Get().AllocateUploadTexture2D<Rgba32>(imageInfo.Width, imageInfo.Height);

        byte[] buffer = File.ReadAllBytes(path);

        upload.Load(buffer);
        upload.CopyTo(texture);

        using Image<ImageSharpRgba32> loaded = texture.ToImage<Rgba32, ImageSharpRgba32>();
        using Image<ImageSharpRgba32> original = Image.Load<ImageSharpRgba32>(path);

        TolerantImageComparer.AssertEqual(original, loaded, 0.0000032f);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture2D<,>))]
    [Resource(typeof(ReadWriteTexture2D<,>))]
    public void LoadAsRgba32_FromStream(Device device, Type textureType)
    {
        string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Imaging", "city.jpg");

        using Stream stream = File.OpenRead(path);

        using Texture2D<Rgba32> texture = device.Get().LoadTexture2D<Rgba32, float4>(textureType, stream);

        using Image<ImageSharpRgba32> loaded = texture.ToImage<Rgba32, ImageSharpRgba32>();
        using Image<ImageSharpRgba32> original = Image.Load<ImageSharpRgba32>(path);

        TolerantImageComparer.AssertEqual(original, loaded, 0.0000032f);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture2D<>))]
    [Resource(typeof(ReadWriteTexture2D<>))]
    public void LoadAsRgba32_FromStream_WithUploadTexture(Device device, Type textureType)
    {
        string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Imaging", "city.jpg");

        IImageInfo imageInfo = Image.Identify(path);

        using Texture2D<Rgba32> texture = device.Get().AllocateTexture2D<Rgba32>(textureType, imageInfo.Width, imageInfo.Height);
        using UploadTexture2D<Rgba32> upload = device.Get().AllocateUploadTexture2D<Rgba32>(imageInfo.Width, imageInfo.Height);

        using Stream stream = File.OpenRead(path);

        upload.Load(stream);
        upload.CopyTo(texture);

        using Image<ImageSharpRgba32> loaded = texture.ToImage<Rgba32, ImageSharpRgba32>();
        using Image<ImageSharpRgba32> original = Image.Load<ImageSharpRgba32>(path);

        TolerantImageComparer.AssertEqual(original, loaded, 0.0000032f);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Data(3840, 2562)]
    [Data(3839, 2560)]
    [Data(3844, 2564)]
    [ExpectedException(typeof(ArgumentException))]
    public void LoadAsRgba32_FromStream_WithUploadTexture_SizeMismatch(Device device, int width, int height)
    {
        string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Imaging", "city.jpg");

        using UploadTexture2D<Rgba32> upload = device.Get().AllocateUploadTexture2D<Rgba32>(width, height);

        upload.Load(path);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture2D<,>))]
    [Resource(typeof(ReadWriteTexture2D<,>))]
    [Data(typeof(string))]
    [Data(typeof(ReadOnlySpan<char>))]
    public void LoadAsBgra32_FromFile(Device device, Type textureType, Type inputType)
    {
        string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Imaging", "city.jpg");

        using Texture2D<Bgra32> texture = device.Get().LoadTexture2D<Bgra32, float4>(textureType, inputType, path);

        using Image<ImageSharpBgra32> loaded = texture.ToImage<Bgra32, ImageSharpBgra32>();
        using Image<ImageSharpBgra32> original = Image.Load<ImageSharpBgra32>(path);

        TolerantImageComparer.AssertEqual(original, loaded, 0.00000132f);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture2D<,>))]
    [Resource(typeof(ReadWriteTexture2D<,>))]
    public void LoadAsBgra32_FromStream(Device device, Type textureType)
    {
        string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Imaging", "city.jpg");

        using Stream stream = File.OpenRead(path);

        using Texture2D<Bgra32> texture = device.Get().LoadTexture2D<Bgra32, float4>(textureType, stream);

        using Image<ImageSharpBgra32> loaded = texture.ToImage<Bgra32, ImageSharpBgra32>();
        using Image<ImageSharpBgra32> original = Image.Load<ImageSharpBgra32>(path);

        TolerantImageComparer.AssertEqual(original, loaded, 0.00000132f);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture2D<,>))]
    [Resource(typeof(ReadWriteTexture2D<,>))]
    [Data(typeof(string))]
    [Data(typeof(ReadOnlySpan<char>))]
    public void LoadAsBgra32_FromFile_WithSameFormat(Device device, Type textureType, Type inputType)
    {
        string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Assets", "CityAfter1024x1024Sampling.png");

        using Texture2D<Bgra32> texture = device.Get().LoadTexture2D<Bgra32, float4>(textureType, inputType, path);

        using Image<ImageSharpBgra32> loaded = texture.ToImage<Bgra32, ImageSharpBgra32>();
        using Image<ImageSharpBgra32> original = Image.Load<ImageSharpBgra32>(path);

        TolerantImageComparer.AssertEqual(original, loaded, 0.00000132f);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture2D<,>))]
    [Resource(typeof(ReadWriteTexture2D<,>))]
    public void LoadAsBgra32_FromStream_WithSameFormat(Device device, Type textureType)
    {
        string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Assets", "CityAfter1024x1024Sampling.png");

        using Stream stream = File.OpenRead(path);

        using Texture2D<Bgra32> texture = device.Get().LoadTexture2D<Bgra32, float4>(textureType, stream);

        using Image<ImageSharpBgra32> loaded = texture.ToImage<Bgra32, ImageSharpBgra32>();
        using Image<ImageSharpBgra32> original = Image.Load<ImageSharpBgra32>(path);

        TolerantImageComparer.AssertEqual(original, loaded, 0.00000132f);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture2D<,>))]
    [Resource(typeof(ReadWriteTexture2D<,>))]
    [Data(typeof(string))]
    [Data(typeof(ReadOnlySpan<char>))]
    public void LoadAsR8_FromFile(Device device, Type textureType, Type inputType)
    {
        string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Imaging", "city.jpg");

        using Texture2D<R8> texture = device.Get().LoadTexture2D<R8, float>(textureType, inputType, path);

        using Image<ImageSharpL8> loaded = texture.ToImage<R8, ImageSharpL8>();
        using Image<ImageSharpL8> original = Image.Load<ImageSharpL8>(path);

        TolerantImageComparer.AssertEqual(original, loaded, 0.000039f);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture2D<,>))]
    [Resource(typeof(ReadWriteTexture2D<,>))]
    public void LoadAsR8_FromFile(Device device, Type textureType)
    {
        string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Imaging", "city.jpg");

        using Stream stream = File.OpenRead(path);

        using Texture2D<R8> texture = device.Get().LoadTexture2D<R8, float>(textureType, stream);

        using Image<ImageSharpL8> loaded = texture.ToImage<R8, ImageSharpL8>();
        using Image<ImageSharpL8> original = Image.Load<ImageSharpL8>(path);

        TolerantImageComparer.AssertEqual(original, loaded, 0.000039f);
    }

    [CombinatorialTestMethod]
    [Device(Device.Warp)]
    [Resource(typeof(ReadOnlyTexture2D<,>))]
    [Resource(typeof(ReadWriteTexture2D<,>))]
    [ExpectedException(typeof(ArgumentNullException))]
    public void LoadAsRgba32_WithNullPath(Device device, Type textureType)
    {
        using Texture2D<Rgba32> texture = device.Get().LoadTexture2D<Rgba32, float4>(textureType, typeof(string), null!);
    }

    [CombinatorialTestMethod]
    [Device(Device.Warp)]
    [Resource(typeof(ReadOnlyTexture2D<,>))]
    [Resource(typeof(ReadWriteTexture2D<,>))]
    [ExpectedException(typeof(ArgumentNullException))]
    public void LoadAsRgba32_WithNullStream(Device device, Type textureType)
    {
        using Texture2D<Rgba32> texture = device.Get().LoadTexture2D<Rgba32, float4>(textureType, (Stream)null!);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture2D<,>))]
    [Resource(typeof(ReadWriteTexture2D<,>))]
    [Data(typeof(string))]
    [Data(typeof(ReadOnlySpan<char>))]
    public void SaveRgba32AsJpeg_ToFile(Device device, Type textureType, Type inputType)
    {
        string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Imaging");
        string expectedPath = Path.Combine(path, "city.jpg");
        string actualPath = Path.Combine(path, "city_rgba32_saved.jpg");

        using Texture2D<Rgba32> texture = device.Get().LoadTexture2D<Rgba32, float4>(textureType, inputType, expectedPath);

        texture.Save(inputType, actualPath);

        TolerantImageComparer.AssertEqual(expectedPath, actualPath, 0.00001023f);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture2D<,>))]
    [Resource(typeof(ReadWriteTexture2D<,>))]
    public void SaveRgba32AsJpeg_ToStream(Device device, Type textureType)
    {
        string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Imaging");
        string expectedPath = Path.Combine(path, "city.jpg");
        string actualPath = Path.Combine(path, "city_rgba32_saved.jpg");

        using Texture2D<Rgba32> texture = device.Get().LoadTexture2D<Rgba32, float4>(textureType, typeof(string), expectedPath);

        using (Stream stream = File.OpenWrite(actualPath))
        {
            texture.Save(stream, ImageFormat.Jpeg);
        }

        TolerantImageComparer.AssertEqual(expectedPath, actualPath, 0.00001023f);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture2D<,>))]
    [Resource(typeof(ReadWriteTexture2D<,>))]
    public void SaveRgba32AsJpeg_ToBufferWriter(Device device, Type textureType)
    {
        string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Imaging");
        string expectedPath = Path.Combine(path, "city.jpg");
        string actualPath = Path.Combine(path, "city_rgba32_saved.jpg");

        using Texture2D<Rgba32> texture = device.Get().LoadTexture2D<Rgba32, float4>(textureType, typeof(string), expectedPath);

        using (ArrayPoolBufferWriter<byte> writer = new())
        {
            texture.Save(writer, ImageFormat.Jpeg);

            File.WriteAllBytes(actualPath, writer.WrittenSpan.ToArray());
        }

        TolerantImageComparer.AssertEqual(expectedPath, actualPath, 0.00001023f);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture2D<,>))]
    [Resource(typeof(ReadWriteTexture2D<,>))]
    [Data(typeof(string))]
    [Data(typeof(ReadOnlySpan<char>))]
    public void SaveRgba32AsJpeg_ToFile_WithReadBackTexture(Device device, Type textureType, Type inputType)
    {
        string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Imaging");
        string expectedPath = Path.Combine(path, "city.jpg");
        string actualPath = Path.Combine(path, "city_rgba32_saved.jpg");

        using Texture2D<Rgba32> texture = device.Get().LoadTexture2D<Rgba32, float4>(textureType, inputType, expectedPath);
        using ReadBackTexture2D<Rgba32> readback = device.Get().AllocateReadBackTexture2D<Rgba32>(texture.Width, texture.Height);

        texture.CopyTo(readback);

        readback.Save(inputType, actualPath);

        TolerantImageComparer.AssertEqual(expectedPath, actualPath, 0.00001023f);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture2D<,>))]
    [Resource(typeof(ReadWriteTexture2D<,>))]
    public void SaveRgba32AsJpeg_ToStream_WithReadBackTexture(Device device, Type textureType)
    {
        string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Imaging");
        string expectedPath = Path.Combine(path, "city.jpg");
        string actualPath = Path.Combine(path, "city_rgba32_saved.jpg");

        using Texture2D<Rgba32> texture = device.Get().LoadTexture2D<Rgba32, float4>(textureType, typeof(string), expectedPath);
        using ReadBackTexture2D<Rgba32> readback = device.Get().AllocateReadBackTexture2D<Rgba32>(texture.Width, texture.Height);

        texture.CopyTo(readback);

        using (Stream stream = File.OpenWrite(actualPath))
        {
            readback.Save(stream, ImageFormat.Jpeg);
        }

        TolerantImageComparer.AssertEqual(expectedPath, actualPath, 0.00001023f);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture2D<,>))]
    [Resource(typeof(ReadWriteTexture2D<,>))]
    public void SaveRgba32AsJpeg_ToBufferWriter_WithReadBackTexture(Device device, Type textureType)
    {
        string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Imaging");
        string expectedPath = Path.Combine(path, "city.jpg");
        string actualPath = Path.Combine(path, "city_rgba32_saved.jpg");

        using Texture2D<Rgba32> texture = device.Get().LoadTexture2D<Rgba32, float4>(textureType, typeof(string), expectedPath);
        using ReadBackTexture2D<Rgba32> readback = device.Get().AllocateReadBackTexture2D<Rgba32>(texture.Width, texture.Height);

        texture.CopyTo(readback);

        using (ArrayPoolBufferWriter<byte> writer = new())
        {
            readback.Save(writer, ImageFormat.Jpeg);

            File.WriteAllBytes(actualPath, writer.WrittenSpan.ToArray());
        }

        TolerantImageComparer.AssertEqual(expectedPath, actualPath, 0.00001023f);
    }

    [CombinatorialTestMethod]
    [Device(Device.Warp)]
    [Resource(typeof(ReadOnlyTexture2D<,>))]
    [Resource(typeof(ReadWriteTexture2D<,>))]
    [ExpectedException(typeof(ArgumentNullException))]
    public void SaveRgba32AsJpeg_ToFile_WithReadBackTexture_WithNullPath(Device device, Type textureType)
    {
        string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Imaging", "city.jpg");

        using Texture2D<Rgba32> texture = device.Get().LoadTexture2D<Rgba32, float4>(textureType, typeof(string), path);
        using ReadBackTexture2D<Rgba32> readback = device.Get().AllocateReadBackTexture2D<Rgba32>(texture.Width, texture.Height);

        texture.CopyTo(readback);

        readback.Save(typeof(string), null!);
    }

    [CombinatorialTestMethod]
    [Device(Device.Warp)]
    [Resource(typeof(ReadOnlyTexture2D<,>))]
    [Resource(typeof(ReadWriteTexture2D<,>))]
    [ExpectedException(typeof(ArgumentNullException))]
    public void SaveRgba32AsJpeg_ToStream_WithReadBackTexture_WithNullStream(Device device, Type textureType)
    {
        string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Imaging", "city.jpg");

        using Texture2D<Rgba32> texture = device.Get().LoadTexture2D<Rgba32, float4>(textureType, typeof(string), path);
        using ReadBackTexture2D<Rgba32> readback = device.Get().AllocateReadBackTexture2D<Rgba32>(texture.Width, texture.Height);

        texture.CopyTo(readback);

        readback.Save((Stream)null!, ImageFormat.Jpeg);
    }

    [CombinatorialTestMethod]
    [Device(Device.Warp)]
    [Resource(typeof(ReadOnlyTexture2D<,>))]
    [Resource(typeof(ReadWriteTexture2D<,>))]
    [ExpectedException(typeof(ArgumentNullException))]
    public void SaveRgba32AsJpeg_ToBufferWriter_WithReadBackTexture_WithNullBufferWriter(Device device, Type textureType)
    {
        string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Imaging", "city.jpg");

        using Texture2D<Rgba32> texture = device.Get().LoadTexture2D<Rgba32, float4>(textureType, typeof(string), path);
        using ReadBackTexture2D<Rgba32> readback = device.Get().AllocateReadBackTexture2D<Rgba32>(texture.Width, texture.Height);

        texture.CopyTo(readback);

        readback.Save((IBufferWriter<byte>)null!, ImageFormat.Jpeg);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture2D<,>))]
    [Resource(typeof(ReadWriteTexture2D<,>))]
    [Data(typeof(string))]
    [Data(typeof(ReadOnlySpan<char>))]
    public void SaveBgra32AsJpeg_ToFile(Device device, Type textureType, Type inputType)
    {
        string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Imaging");
        string expectedPath = Path.Combine(path, "city.jpg");
        string actualPath = Path.Combine(path, "city_bgra32_saved.jpg");

        using Texture2D<Bgra32> texture = device.Get().LoadTexture2D<Bgra32, float4>(textureType, inputType, expectedPath);

        texture.Save(inputType, actualPath);

        TolerantImageComparer.AssertEqual(expectedPath, actualPath, 0.00001023f);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture2D<,>))]
    [Resource(typeof(ReadWriteTexture2D<,>))]
    [Data(typeof(string))]
    [Data(typeof(ReadOnlySpan<char>))]
    public void SaveR8AsJpeg_ToFile(Device device, Type textureType, Type inputType)
    {
        string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Imaging");
        string sourcePath = Path.Combine(path, "city.jpg");
        string expectedPath = Path.Combine(path, "city_r8_reference.jpg");
        string actualPath = Path.Combine(path, "city_r8_saved.jpg");

        using Texture2D<R8> texture = device.Get().LoadTexture2D<R8, float>(textureType, inputType, sourcePath);

        texture.Save(inputType, actualPath);

        using Image<ImageSharpL8> original = Image.Load<ImageSharpL8>(sourcePath);

        original.Save(expectedPath);

        TolerantImageComparer.AssertEqual(expectedPath, actualPath, 0.00004037f);
    }

    [CombinatorialTestMethod]
    [Device(Device.Warp)]
    [Resource(typeof(ReadOnlyTexture2D<,>))]
    [Resource(typeof(ReadWriteTexture2D<,>))]
    [ExpectedException(typeof(ArgumentNullException))]
    public void SaveRgba32AsJpeg_ToFile_WithNullPath(Device device, Type textureType)
    {
        string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Imaging", "city.jpg");

        using Texture2D<Rgba32> texture = device.Get().LoadTexture2D<Rgba32, float4>(textureType, typeof(string), path);

        texture.Save(typeof(string), null!);
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
        /// <typeparam name="TPixel">The type of image pixels to analyze.</typeparam>
        /// <param name="expected">The reference image.</param>
        /// <param name="actual">The expected image.</param>
        /// <param name="threshold">The allowed difference threshold for the normalized delta.</param>
        public static void AssertEqual<TPixel>(Image<TPixel> expected, Image<TPixel> actual, float threshold)
            where TPixel : unmanaged, IPixel<TPixel>
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
            Span<ImageSharpRgba32> aBuffer = new ImageSharpRgba32[actual.Width];
            Span<ImageSharpRgba32> bBuffer = new ImageSharpRgba32[actual.Width];

            for (int y = 0; y < actual.Height; y++)
            {
                Span<TPixel> aSpan = expected.GetPixelRowSpan(y);
                Span<TPixel> bSpan = actual.GetPixelRowSpan(y);

                PixelOperations<TPixel>.Instance.ToRgba32(actual.GetConfiguration(), aSpan, aBuffer);
                PixelOperations<TPixel>.Instance.ToRgba32(actual.GetConfiguration(), bSpan, bBuffer);

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
                            var diff = new PixelDifference(new Point(x, y), aBuffer[x], bBuffer[x]);
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
