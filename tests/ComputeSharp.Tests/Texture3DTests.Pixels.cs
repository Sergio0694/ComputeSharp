using System;
using System.Reflection;
using ComputeSharp.Resources;
using ComputeSharp.Tests.Attributes;
using ComputeSharp.Tests.Extensions;
using ComputeSharp.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests;

public partial class Texture3DTests
{
    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture3D<,>))]
    [Resource(typeof(ReadWriteTexture3D<,>))]
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
            using Texture3D<T> texture = device.Get().AllocateTexture3D<T, TPixel>(textureType, 128, 128, 2);

            Assert.IsNotNull(texture);
            Assert.AreEqual(texture.Width, 128);
            Assert.AreEqual(texture.Height, 128);
            Assert.AreEqual(texture.Depth, 2);
            Assert.AreSame(texture.GraphicsDevice, device.Get());

            if (textureType == typeof(ReadOnlyTexture3D<,>))
            {
                Assert.IsTrue(texture is ReadOnlyTexture3D<T, TPixel>);
            }
            else
            {
                Assert.IsTrue(texture is ReadWriteTexture3D<T, TPixel>);
            }
        }

        TestHelper.Run(Test<Rgba32, float4>, t, tPixel, device, textureType);
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
            using ReadWriteTexture3D<T, TPixel> texture = device.Get().AllocateReadWriteTexture3D<T, TPixel>(32, 32, 2);

            using (var context = device.Get().CreateComputeContext())
            {
                context.Transition(texture, ResourceState.ReadOnly);

                IReadOnlyTexture3D<TPixel> wrapper1 = texture.AsReadOnly();

                Assert.IsNotNull(wrapper1);

                IReadOnlyTexture3D<TPixel> wrapper2 = texture.AsReadOnly();

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
            using ReadWriteTexture3D<T, TPixel> texture = device.Get().AllocateReadWriteTexture3D<T, TPixel>(32, 32, 2);

            _ = texture.AsReadOnly();

            Assert.Fail();
        }

        TestHelper.Run(Test<Rgba32, float4>, t, tPixel, device);
    }
}
