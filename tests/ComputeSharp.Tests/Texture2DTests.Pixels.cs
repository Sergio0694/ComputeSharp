using System;
using System.IO;
using System.Numerics;
using System.Reflection;
using ComputeSharp.__Internals;
using ComputeSharp.Resources;
using ComputeSharp.Tests.Attributes;
using ComputeSharp.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SixLabors.ImageSharp;
using ImageSharpRgba32 = SixLabors.ImageSharp.PixelFormats.Rgba32;

#pragma warning disable CS0618

namespace ComputeSharp.Tests
{
    public partial class Texture2DTests
    {
        [CombinatorialTestMethod]
        [AllDevices]
        [Resource(typeof(ReadOnlyTexture2D<,>))]
        [Resource(typeof(ReadWriteTexture2D<,>))]
        [Data(typeof(Bgra32), typeof(Vector4))]
        [Data(typeof(Bgra32), typeof(Float4))]
        [Data(typeof(R16), typeof(float))]
        [Data(typeof(R8), typeof(float))]
        [Data(typeof(Rg16), typeof(Vector2))]
        [Data(typeof(Rg16), typeof(Float2))]
        [Data(typeof(Rg32), typeof(Vector2))]
        [Data(typeof(Rg32), typeof(Float2))]
        [Data(typeof(Rgba32), typeof(Vector4))]
        [Data(typeof(Rgba32), typeof(Float4))]
        [Data(typeof(Rgba64), typeof(Vector4))]
        [Data(typeof(Rgba64), typeof(Float4))]
        public void Allocate_Uninitialized_Pixel_Ok(Device device, Type textureType, Type t, Type tPixel)
        {
            static void Test<T, TPixel>(Device device, Type textureType)
                where T : unmanaged, IUnorm<TPixel>
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

            try
            {
                new Action<Device, Type>(Test<Rgba32, Vector4>).Method.GetGenericMethodDefinition().MakeGenericMethod(t, tPixel).Invoke(null, new object[] { device, textureType });
            }
            catch (TargetInvocationException e) when (e.InnerException is not null)
            {
                throw e.InnerException;
            }
        }

        [CombinatorialTestMethod]
        [AllDevices]
        public void SampleFromSourceTexture_ComputeShader(Device device)
        {
            _ = device.Get();

            string imagingPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Imaging");
            string assetsPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Assets");

            using var sampled = Image.Load<ImageSharpRgba32>(Path.Combine(assetsPath, "CityAfter1024x1024Sampling.png"));

            using ReadOnlyTexture2D<Rgba32, Float4> source = device.Get().LoadTexture(Path.Combine(imagingPath, "city.jpg"));
            using ReadWriteTexture2D<Rgba32, Float4> destination = device.Get().AllocateReadWriteTexture2D<Rgba32, Float4>(sampled.Width, sampled.Height);

            device.Get().For(sampled.Width, sampled.Height, new SamplingComputeShader(source, destination));

            using var processed = destination.ToImage();

            ImagingTests.TolerantImageComparer.AssertEqual(sampled, processed, 0.00000086f);
        }

        [AutoConstructor]
        public readonly partial struct SamplingComputeShader : IComputeShader
        {
            public readonly IReadOnlyTexture2D<Float4> source;
            public readonly IReadWriteTexture2D<Float4> destination;

            public void Execute()
            {
                destination[ThreadIds.XY] = source[ThreadIds.Normalized.XY];
            }
        }

        [CombinatorialTestMethod]
        [AllDevices]
        public void SampleFromSourceTexture_PixelShader(Device device)
        {
            _ = device.Get();

            string imagingPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Imaging");
            string assetsPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Assets");

            using var sampled = Image.Load<ImageSharpRgba32>(Path.Combine(assetsPath, "CityAfter1024x1024Sampling.png"));

            using ReadOnlyTexture2D<Rgba32, Float4> source = device.Get().LoadTexture(Path.Combine(imagingPath, "city.jpg"));
            using ReadWriteTexture2D<Rgba32, Float4> destination = device.Get().AllocateReadWriteTexture2D<Rgba32, Float4>(sampled.Width, sampled.Height);

            device.Get().ForEach(destination, new SamplingPixelShader(source));

            using var processed = destination.ToImage();

            ImagingTests.TolerantImageComparer.AssertEqual(sampled, processed, 0.00000086f);
        }

        [AutoConstructor]
        public readonly partial struct SamplingPixelShader : IPixelShader<Float4>
        {
            public readonly IReadOnlyTexture2D<Float4> texture;

            public Float4 Execute()
            {
                return texture[ThreadIds.Normalized.XY];
            }
        }
    }
}
