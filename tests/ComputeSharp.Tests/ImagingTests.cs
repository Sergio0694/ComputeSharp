using System;
using System.Buffers;
using System.IO;
using System.Reflection;
using CommunityToolkit.HighPerformance.Buffers;
#if USE_D3D12MA
using ComputeSharp.D3D12MemoryAllocator;
using ComputeSharp.Interop;
#endif
using ComputeSharp.Resources;
using ComputeSharp.Tests.Attributes;
using ComputeSharp.Tests.Extensions;
using ComputeSharp.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SixLabors.ImageSharp;
using ImageSharpRgba32 = SixLabors.ImageSharp.PixelFormats.Rgba32;
using ImageSharpBgra32 = SixLabors.ImageSharp.PixelFormats.Bgra32;
using ImageSharpL8 = SixLabors.ImageSharp.PixelFormats.L8;

namespace ComputeSharp.Tests;

[TestClass]
[TestCategory("Imaging")]
public class ImagingTests
{
    [AssemblyInitialize]
    public static void ConfigureImageSharp(TestContext _)
    {
        Configuration.Default.PreferContiguousImageBuffers = true;

#if USE_D3D12MA
        // If requested by the test runner, configure D3D12MA
        AllocationServices.ConfigureAllocatorFactory(new D3D12MemoryAllocatorFactory());
#endif
    }

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

        TolerantImageComparer.AssertEqual(expectedPath, actualPath, 0.00004038f);
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
}