using System;
using System.Linq;
using ComputeSharp.Resources;
using ComputeSharp.Tests.Attributes;
using ComputeSharp.Tests.Extensions;
using Microsoft.Toolkit.HighPerformance;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests
{
    [TestClass]
    [TestCategory("Texture2D")]
    public partial class Texture2DTests
    {
        [CombinatorialTestMethod]
        [AllDevices]
        [Resource(typeof(ReadOnlyTexture2D<>))]
        [Resource(typeof(ReadWriteTexture2D<>))]
        [Data(AllocationMode.Default)]
        [Data(AllocationMode.Clear)]
        public void Allocate_Uninitialized_Ok(Device device, Type textureType, AllocationMode allocationMode)
        {
            using Texture2D<float> texture = device.Get().AllocateTexture2D<float>(textureType, 128, 128, allocationMode);

            Assert.IsNotNull(texture);
            Assert.AreEqual(texture.Width, 128);
            Assert.AreEqual(texture.Height, 128);
            Assert.AreSame(texture.GraphicsDevice, device.Get());

            if (allocationMode == AllocationMode.Clear)
            {
                foreach (float x in texture.ToArray())
                {
                    Assert.AreEqual(x, 0f);
                }
            }
        }

        [CombinatorialTestMethod]
        [AllDevices]
        [Resource(typeof(ReadOnlyTexture2D<>))]
        [Resource(typeof(ReadWriteTexture2D<>))]
        [Data(128, -14253)]
        [Data(128, -1)]
        [Data(0, -4314)]
        [Data(-14, -53)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Allocate_Uninitialized_Fail(Device device, Type textureType, int width, int height)
        {
            using Texture2D<float> texture = device.Get().AllocateTexture2D<float>(textureType, width, height);
        }

        [CombinatorialTestMethod]
        [AllDevices]
        [Resource(typeof(ReadOnlyTexture2D<>))]
        [Resource(typeof(ReadWriteTexture2D<>))]
        public void Allocate_FromArray(Device device, Type textureType)
        {
            float[] data = Enumerable.Range(0, 128 * 128).Select(static i => (float)i).ToArray();

            using Texture2D<float> texture = device.Get().AllocateTexture2D(textureType, data, 128, 128);

            Assert.IsNotNull(texture);
            Assert.AreEqual(texture.Width, 128);
            Assert.AreEqual(texture.Height, 128);
            Assert.AreSame(texture.GraphicsDevice, device.Get());

            float[,] result = texture.ToArray();

            Assert.IsNotNull(result);
            Assert.AreEqual(data.Length, result.Length);
            Assert.IsTrue(data.SequenceEqual(result.Cast<float>()));
        }

        [CombinatorialTestMethod]
        [AllDevices]
        [Resource(typeof(ReadOnlyTexture2D<>))]
        [Resource(typeof(ReadWriteTexture2D<>))]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void UsageAfterDispose(Device device, Type textureType)
        {
            using Texture2D<float> texture = device.Get().AllocateTexture2D<float>(textureType, 10, 10);

            texture.Dispose();

            _ = texture.ToArray();
        }

        [CombinatorialTestMethod]
        [AllDevices]
        [Resource(typeof(ReadOnlyTexture2D<>))]
        [Resource(typeof(ReadWriteTexture2D<>))]
        [Data(0, 0, 64, 64)]
        [Data(0, 14, 64, 50)]
        [Data(14, 0, 50, 64)]
        [Data(10, 10, 54, 54)]
        [Data(20, 20, 32, 27)]
        [Data(60, 0, 4, 64)]
        [Data(0, 60, 64, 4)]
        [Data(63, 2, 1, 60)]
        [Data(2, 63, 60, 1)]
        public void GetData_RangeToVoid_Ok(Device device, Type textureType, int x, int y, int width, int height)
        {
            float[] array = Enumerable.Range(0, 4096).Select(static i => (float)i).ToArray();

            using Texture2D<float> texture = device.Get().AllocateTexture2D(textureType, array, 64, 64);

            float[] result = new float[width * height];

            texture.CopyTo(result, x, y, width, height);

            Span2D<float>
                expected = new Span2D<float>(array, 64, 64).Slice(y, x, height, width),
                data = new(result, height, width);

            CollectionAssert.AreEqual(expected.ToArray(), data.ToArray());
        }

        [CombinatorialTestMethod]
        [AllDevices]
        [Resource(typeof(ReadOnlyTexture2D<>))]
        [Resource(typeof(ReadWriteTexture2D<>))]
        [Data(0, -1, 50, 50)]
        [Data(-1, 0, 50, 50)]
        [Data(12, 0, -1, 50)]
        [Data(12, 0, 20, -1)]
        [Data(12, 20, 20, 50)]
        [Data(12, 20, 60, 20)]
        [Data(80, 20, 40, 20)]
        [Data(0, 80, 40, 20)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetData_RangeToVoid_Fail(Device device, Type textureType, int x, int y, int width, int height)
        {
            float[] array = Enumerable.Range(0, 4096).Select(static i => (float)i).ToArray();

            using Texture2D<float> texture = device.Get().AllocateTexture2D(textureType, array, 64, 64);

            float[] result = new float[4096];

            texture.CopyTo(result, x, y, width, height);
        }

        [CombinatorialTestMethod]
        [AllDevices]
        public void Dispatch_ReadOnlyTexture2D(Device device)
        {
            int[] data = Enumerable.Range(0, 32 * 32).ToArray();

            using ReadOnlyTexture2D<int> source = device.Get().AllocateReadOnlyTexture2D(data, 32, 32);
            using ReadWriteBuffer<int> destination = device.Get().AllocateReadWriteBuffer<int>(data.Length);

            device.Get().For(source.Width, source.Height, new ReadOnlyTexture2DKernel(source, destination));

            int[] result = destination.ToArray();

            CollectionAssert.AreEqual(data, result);
        }

        [AutoConstructor]
        internal readonly partial struct ReadOnlyTexture2DKernel : IComputeShader
        {
            public readonly ReadOnlyTexture2D<int> source;
            public readonly ReadWriteBuffer<int> destination;

            public void Execute()
            {
                destination[ThreadIds.Y * 32 + ThreadIds.X] = source[ThreadIds.XY];
            }
        }

        [CombinatorialTestMethod]
        [AllDevices]
        public void Dispatch_ReadWriteTexture2D(Device device)
        {
            int[] data = Enumerable.Range(0, 32 * 32).ToArray();

            using ReadWriteTexture2D<int> source = device.Get().AllocateReadWriteTexture2D(data, 32, 32);
            using ReadWriteTexture2D<int> destination = device.Get().AllocateReadWriteTexture2D<int>(32, 32);

            device.Get().For(source.Width, source.Height, new ReadWriteTexture2DKernel(source, destination));

            int[] result = new int[data.Length];

            destination.CopyTo(result);

            CollectionAssert.AreEqual(data, result);
        }

        [AutoConstructor]
        internal readonly partial struct ReadWriteTexture2DKernel : IComputeShader
        {
            public readonly ReadWriteTexture2D<int> source;
            public readonly ReadWriteTexture2D<int> destination;

            public void Execute()
            {
                destination[ThreadIds.XY] = source[ThreadIds.XY];
            }
        }
    }
}
