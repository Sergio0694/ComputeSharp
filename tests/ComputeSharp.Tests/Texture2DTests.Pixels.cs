using System;
using System.IO;
using System.Reflection;
using ComputeSharp.Resources;
using ComputeSharp.Tests.Attributes;
using ComputeSharp.Tests.Extensions;
using ComputeSharp.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SixLabors.ImageSharp;
using ImageSharpRgba32 = SixLabors.ImageSharp.PixelFormats.Rgba32;

namespace ComputeSharp.Tests;

partial class Texture2DTests
{
    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture2D<,>))]
    [Resource(typeof(ReadWriteTexture2D<,>))]
    [Data(typeof(Bgra32), typeof(float4))]
    [Data(typeof(R16), typeof(float))]
    [Data(typeof(R8), typeof(float))]
    [Data(typeof(Rg16), typeof(float2))]
    [Data(typeof(Rg32), typeof(float2))]
    [Data(typeof(Rgba32), typeof(float4))]
    [Data(typeof(Rgba64), typeof(float4))]
    public void Allocate_Uninitialized_Pixel_Ok(Device device, Type textureType, Type t, Type tPixel)
    {
        static void Test<T, TPixel>(Device device, Type textureType)
            where T : unmanaged, IPixel<T, TPixel>
            where TPixel : unmanaged
        {
            using Texture2D<T> texture = device.Get().AllocateTexture2D<T, TPixel>(textureType, 128, 128);

            Assert.IsNotNull(texture);
            Assert.AreEqual(texture.Width, 128);
            Assert.AreEqual(texture.Height, 128);
            Assert.AreSame(texture.GraphicsDevice, device.Get());

            if (textureType == typeof(ReadOnlyTexture2D<,>))
            {
                Assert.IsTrue(texture is ReadOnlyTexture2D<T, TPixel>);
            }
            else
            {
                Assert.IsTrue(texture is ReadWriteTexture2D<T, TPixel>);
            }
        }

        TestHelper.Run(Test<Rgba32, float4>, t, tPixel, device, textureType);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public void SampleFromSourceTexture_ComputeShader(Device device)
    {
        _ = device.Get();

        string imagingPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Imaging");
        string assetsPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Assets");

        using Image<ImageSharpRgba32> sampled = Image.Load<ImageSharpRgba32>(Path.Combine(assetsPath, "CityAfter1024x1024Sampling.png"));

        using ReadOnlyTexture2D<Rgba32, float4> source = device.Get().LoadReadOnlyTexture2D<Rgba32, float4>(Path.Combine(imagingPath, "city.jpg"));
        using ReadWriteTexture2D<Rgba32, float4> destination = device.Get().AllocateReadWriteTexture2D<Rgba32, float4>(sampled.Width, sampled.Height);

        device.Get().For(sampled.Width, sampled.Height, new SamplingComputeShader(source, destination));

        using Image<ImageSharpRgba32> processed = destination.ToImage<Rgba32, ImageSharpRgba32>();

        TolerantImageComparer.AssertEqual(sampled, processed, 0.0000017f);
    }

    [AutoConstructor]
    public readonly partial struct SamplingComputeShader : IComputeShader
    {
        public readonly IReadOnlyNormalizedTexture2D<float4> source;
        public readonly IReadWriteNormalizedTexture2D<float4> destination;

        public void Execute()
        {
            destination[ThreadIds.XY] = source.Sample(ThreadIds.Normalized.XY);
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public void SampleFromSourceTexture_PixelShader(Device device)
    {
        _ = device.Get();

        string imagingPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Imaging");
        string assetsPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Assets");

        using Image<ImageSharpRgba32> sampled = Image.Load<ImageSharpRgba32>(Path.Combine(assetsPath, "CityAfter1024x1024Sampling.png"));

        using ReadOnlyTexture2D<Rgba32, float4> source = device.Get().LoadReadOnlyTexture2D<Rgba32, float4>(Path.Combine(imagingPath, "city.jpg"));
        using ReadWriteTexture2D<Rgba32, float4> destination = device.Get().AllocateReadWriteTexture2D<Rgba32, float4>(sampled.Width, sampled.Height);

        device.Get().ForEach(destination, new SamplingPixelShader(source));

        using Image<ImageSharpRgba32> processed = destination.ToImage<Rgba32, ImageSharpRgba32>();

        TolerantImageComparer.AssertEqual(sampled, processed, 0.0000017f);
    }

    [AutoConstructor]
    public readonly partial struct SamplingPixelShader : IPixelShader<float4>
    {
        public readonly IReadOnlyNormalizedTexture2D<float4> texture;

        public float4 Execute()
        {
            return texture.Sample(ThreadIds.Normalized.XY);
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Data(typeof(Bgra32), typeof(float4))]
    [Data(typeof(R16), typeof(float))]
    [Data(typeof(R8), typeof(float))]
    [Data(typeof(Rg16), typeof(float2))]
    [Data(typeof(Rg32), typeof(float2))]
    [Data(typeof(Rgba32), typeof(float4))]
    [Data(typeof(Rgba64), typeof(float4))]
    public void AsReadOnly_MultipleCalls_SameInstance(Device device, Type t, Type tPixel)
    {
        static void Test<T, TPixel>(Device device)
            where T : unmanaged, IPixel<T, TPixel>
            where TPixel : unmanaged
        {
            using ReadWriteTexture2D<T, TPixel> texture = device.Get().AllocateReadWriteTexture2D<T, TPixel>(64, 64);

            using (ComputeContext context = device.Get().CreateComputeContext())
            {
                context.Transition(texture, ResourceState.ReadOnly);

                IReadOnlyNormalizedTexture2D<TPixel> wrapper1 = texture.AsReadOnly();

                Assert.IsNotNull(wrapper1);

                IReadOnlyNormalizedTexture2D<TPixel> wrapper2 = texture.AsReadOnly();

                Assert.IsNotNull(wrapper2);
                Assert.AreSame(wrapper1, wrapper2);

                context.Transition(texture, ResourceState.ReadWrite);
            }
        }

        TestHelper.Run(Test<Rgba32, float4>, t, tPixel, device);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Data(typeof(Bgra32), typeof(float4))]
    [Data(typeof(R16), typeof(float))]
    [Data(typeof(R8), typeof(float))]
    [Data(typeof(Rg16), typeof(float2))]
    [Data(typeof(Rg32), typeof(float2))]
    [Data(typeof(Rgba32), typeof(float4))]
    [Data(typeof(Rgba64), typeof(float4))]
    [ExpectedException(typeof(InvalidOperationException))]
    public void AsReadOnly_InvalidState_ThrowInvalidOperationException(Device device, Type t, Type tPixel)
    {
        static void Test<T, TPixel>(Device device)
            where T : unmanaged, IPixel<T, TPixel>
            where TPixel : unmanaged
        {
            using ReadWriteTexture2D<T, TPixel> texture = device.Get().AllocateReadWriteTexture2D<T, TPixel>(64, 64);

            _ = texture.AsReadOnly();

            Assert.Fail();
        }

        TestHelper.Run(Test<Rgba32, float4>, t, tPixel, device);
    }
}