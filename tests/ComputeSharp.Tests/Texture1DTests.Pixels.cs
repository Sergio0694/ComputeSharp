using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using CommunityToolkit.HighPerformance;
using ComputeSharp.Resources;
using ComputeSharp.Tests.Attributes;
using ComputeSharp.Tests.Extensions;
using ComputeSharp.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SixLabors.ImageSharp;
using ImageSharpRgba32 = SixLabors.ImageSharp.PixelFormats.Rgba32;

namespace ComputeSharp.Tests;

partial class Texture1DTests
{
    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture1D<,>))]
    [Resource(typeof(ReadWriteTexture1D<,>))]
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
            using Texture1D<T> texture = device.Get().AllocateTexture1D<T, TPixel>(textureType, 128);

            Assert.IsNotNull(texture);
            Assert.AreEqual(texture.Width, 128);
            Assert.AreSame(texture.GraphicsDevice, device.Get());

            if (textureType == typeof(ReadOnlyTexture1D<,>))
            {
                Assert.IsTrue(texture is ReadOnlyTexture1D<T, TPixel>);
            }
            else
            {
                Assert.IsTrue(texture is ReadWriteTexture1D<T, TPixel>);
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

        using Image<ImageSharpRgba32> original = Image.Load<ImageSharpRgba32>(Path.Combine(imagingPath, "city.jpg"));
        using Image<ImageSharpRgba32> sampled = Image.Load<ImageSharpRgba32>(Path.Combine(assetsPath, "CityAfter1024x1024Sampling.png"));

        if (!original.DangerousTryGetSinglePixelMemory(out Memory<ImageSharpRgba32> originalMemory))
        {
            Assert.Inconclusive();
        }

        Span<Rgba32> row = MemoryMarshal.Cast<ImageSharpRgba32, Rgba32>(originalMemory.Span.Slice(0, original.Width));

        using ReadOnlyTexture1D<Rgba32, float4> source = device.Get().AllocateReadOnlyTexture1D<Rgba32, float4>(row);
        using ReadWriteTexture1D<Rgba32, float4> destination = device.Get().AllocateReadWriteTexture1D<Rgba32, float4>(sampled.Width);

        device.Get().For(sampled.Width, new SamplingComputeShader(source, destination));

        Rgba32[] processed = destination.ToArray();

        if (!sampled.DangerousTryGetSinglePixelMemory(out Memory<ImageSharpRgba32> sampledMemory))
        {
            Assert.Inconclusive();
        }

        using Image<ImageSharpRgba32> sampledRow = Image.WrapMemory(sampledMemory.Slice(0, sampled.Width), sampled.Width, 1);
        using Image<ImageSharpRgba32> processedRow = Image.WrapMemory(processed.AsMemory().Cast<Rgba32, ImageSharpRgba32>(), sampled.Width, 1);

        TolerantImageComparer.AssertEqual(sampledRow, processedRow, 0.0000017f);
    }

    [AutoConstructor]
    public readonly partial struct SamplingComputeShader : IComputeShader
    {
        public readonly IReadOnlyNormalizedTexture1D<float4> source;
        public readonly IReadWriteNormalizedTexture1D<float4> destination;

        public void Execute()
        {
            destination[ThreadIds.X] = source.Sample(ThreadIds.Normalized.X);
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
            using ReadWriteTexture1D<T, TPixel> texture = device.Get().AllocateReadWriteTexture1D<T, TPixel>(64);

            using (ComputeContext context = device.Get().CreateComputeContext())
            {
                context.Transition(texture, ResourceState.ReadOnly);

                IReadOnlyNormalizedTexture1D<TPixel> wrapper1 = texture.AsReadOnly();

                Assert.IsNotNull(wrapper1);

                IReadOnlyNormalizedTexture1D<TPixel> wrapper2 = texture.AsReadOnly();

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
            using ReadWriteTexture1D<T, TPixel> texture = device.Get().AllocateReadWriteTexture1D<T, TPixel>(64);

            _ = texture.AsReadOnly();

            Assert.Fail();
        }

        TestHelper.Run(Test<Rgba32, float4>, t, tPixel, device);
    }
}