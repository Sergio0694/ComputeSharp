using System.IO;
using System.Reflection;
using ComputeSharp.BokehBlur.Processors;
using ComputeSharp.Tests.Attributes;
using ComputeSharp.Tests.Extensions;
using ComputeSharp.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using ImageSharpRgba32 = SixLabors.ImageSharp.PixelFormats.Rgba32;

namespace ComputeSharp.Tests;

[TestClass]
public class ImageProcessingTests
{
    [CombinatorialTestMethod]
    [AllDevices]
    public void BokehBlur(Device device)
    {
        // Early test to ensure the device is available. This saves time when running the
        // unit test if the target device is not available, as we skip the preprocessing.
        _ = device.Get();

        string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Imaging");

        using Image<ImageSharpRgba32> original = Image.Load<ImageSharpRgba32>(Path.Combine(path, "city.jpg"));

        original.Mutate(c => c.Resize(1920, 1080));

        using Image<ImageSharpRgba32> cpu = original.Clone(c => c.BokehBlur(40, 2, 3));
        using Image<ImageSharpRgba32> gpu = original.Clone(c => c.ApplyProcessor(new HlslBokehBlurProcessor(device.Get(), 40, 2)));

        string expectedPath = Path.Combine(path, "city_bokeh_cpu.jpg");
        string actualPath = Path.Combine(path, "city_bokeh_gpu.jpg");

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

        using Image<ImageSharpRgba32> original = Image.Load<ImageSharpRgba32>(Path.Combine(path, "city.jpg"));

        original.Mutate(c => c.Resize(1920, 1080));

        using Image<ImageSharpRgba32> cpu = original.Clone(c => c.GaussianBlur(30f));
        using Image<ImageSharpRgba32> gpu = original.Clone(c => c.ApplyProcessor(new HlslGaussianBlurProcessor(device.Get(), 90)));

        string expectedPath = Path.Combine(path, "city_gaussian_cpu.jpg");
        string actualPath = Path.Combine(path, "city_gaussian_gpu.jpg");

        cpu.Save(expectedPath);
        gpu.Save(actualPath);

        TolerantImageComparer.AssertEqual(expectedPath, actualPath, 0.0000046f);
    }
}