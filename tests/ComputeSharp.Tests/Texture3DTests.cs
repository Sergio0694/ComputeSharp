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
    [TestCategory("Texture3D")]
    public partial class Texture3DTests
    {
        [CombinatorialTestMethod]
        [AllDevices]
        [Resource(typeof(ReadOnlyTexture3D<>))]
        [Resource(typeof(ReadWriteTexture3D<>))]
        [Data(AllocationMode.Default)]
        [Data(AllocationMode.Clear)]
        public void Allocate_Uninitialized_Ok(Device device, Type textureType, AllocationMode allocationMode)
        {
            using Texture3D<float> texture = device.Get().AllocateTexture3D<float>(textureType, 64, 64, 12, allocationMode);

            Assert.IsNotNull(texture);
            Assert.AreEqual(texture.Width, 64);
            Assert.AreEqual(texture.Height, 64);
            Assert.AreEqual(texture.Depth, 12);
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
        [Resource(typeof(ReadOnlyTexture3D<>))]
        [Resource(typeof(ReadWriteTexture3D<>))]
        [Data(128, -14253, 4)]
        [Data(128, -1, 4)]
        [Data(0, -4314, 4)]
        [Data(-14, -53, 4)]
        [Data(12, 12, -1)]
        [Data(2, 4, ushort.MaxValue + 1)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Allocate_Uninitialized_Fail(Device device, Type textureType, int width, int height, int depth)
        {
            using Texture3D<float> texture = device.Get().AllocateTexture3D<float>(textureType, width, height, depth);
        }

        [CombinatorialTestMethod]
        [AllDevices]
        [Resource(typeof(ReadOnlyTexture3D<>))]
        [Resource(typeof(ReadWriteTexture3D<>))]
        [Data(12, 12, 4)]
        [Data(2, 12, 4)]
        [Data(64, 64, 1)]
        [Data(64, 27, 3)]
        public void Allocate_FromArray(Device device, Type textureType, int width, int height, int depth)
        {
            float[] data = Enumerable.Range(0, width * height * depth).Select(static i => (float)i).ToArray();

            using Texture3D<float> texture = device.Get().AllocateTexture3D(textureType, data, width, height, depth);

            Assert.IsNotNull(texture);
            Assert.AreEqual(texture.Width, width);
            Assert.AreEqual(texture.Height, height);
            Assert.AreEqual(texture.Depth, depth);
            Assert.AreSame(texture.GraphicsDevice, device.Get());

            float[,,] result = texture.ToArray();

            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetLength(0), depth);
            Assert.AreEqual(result.GetLength(1), height);
            Assert.AreEqual(result.GetLength(2), width);
            Assert.AreEqual(data.Length, result.Length);
            Assert.IsTrue(data.SequenceEqual(result.Cast<float>()));

            Array.Clear(result, 0, result.Length);

            float[] flat = new float[result.Length];

            texture.CopyTo(flat);

            CollectionAssert.AreEqual(flat, data);
        }

        [CombinatorialTestMethod]
        [AllDevices]
        [Resource(typeof(ReadOnlyTexture3D<>))]
        [Resource(typeof(ReadWriteTexture3D<>))]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void UsageAfterDispose(Device device, Type textureType)
        {
            using Texture3D<float> texture = device.Get().AllocateTexture3D<float>(textureType, 10, 10, 4);

            texture.Dispose();

            _ = texture.ToArray();
        }

        [CombinatorialTestMethod]
        [AllDevices]
        [Resource(typeof(ReadOnlyTexture3D<>))]
        [Resource(typeof(ReadWriteTexture3D<>))]
        [Data(0, 0, 0, 64, 64, 3)]
        [Data(0, 14, 0, 64, 50, 3)]
        [Data(0, 14, 1, 64, 50, 2)]
        [Data(14, 0, 1, 50, 64, 2)]
        [Data(10, 10, 2, 54, 54, 1)]
        [Data(20, 20, 0, 32, 27, 3)]
        [Data(60, 0, 0, 4, 64, 3)]
        [Data(0, 60, 2, 64, 4, 1)]
        [Data(63, 2, 0, 1, 60, 2)]
        [Data(2, 63, 1, 60, 1, 2)]
        public void GetData_RangeToVoid_Ok(Device device, Type textureType, int x, int y, int z, int width, int height, int depth)
        {
            int[,,] array = new int[3, 64, 64];

            foreach (var item in array.AsSpan().Enumerate())
                item.Value = item.Index;

            using Texture3D<int> texture = device.Get().AllocateTexture3D(textureType, array);

            int[,,] result = new int[depth, height, width];

            texture.CopyTo(result.AsSpan(), x, y, z, width, height, depth);

            for (int k = 0; k < depth; k++)
            {
                Span2D<int>
                    source = array.AsSpan2D(z + k).Slice(y, x, height, width),
                    destination = result.AsSpan2D(k);

                CollectionAssert.AreEqual(source.ToArray(), destination.ToArray());
            }
        }

        [CombinatorialTestMethod]
        [AllDevices]
        [Resource(typeof(ReadOnlyTexture3D<>))]
        [Resource(typeof(ReadWriteTexture3D<>))]
        [Data(0, -1, 0, 50, 50, 2)]
        [Data(-1, 0, 0, 50, 50, 3)]
        [Data(12, 0, 0, -1, 50, 1)]
        [Data(12, 0, 0, 20, 50, 0)]
        [Data(12, 0, 1, 20, -1, 2)]
        [Data(12, 20, 2, 20, 50, 1)]
        [Data(12, 20, 0, 60, 20, 3)]
        [Data(80, 20, 2, 40, 20, 1)]
        [Data(0, 80, 2, 40, 20, 1)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetData_RangeToVoid_Fail(Device device, Type textureType, int x, int y, int z, int width, int height, int depth)
        {
            float[] array = Enumerable.Range(0, 64 * 64 * 3).Select(static i => (float)i).ToArray();

            using Texture3D<float> texture = device.Get().AllocateTexture3D(textureType, array, 64, 64, 3);

            float[] result = new float[array.Length];

            texture.CopyTo(result, x, y, z, width, height, depth);
        }

        [CombinatorialTestMethod]
        [AllDevices]
        public void Dispatch_ReadOnlyTexture3D(Device device)
        {
            int[] data = Enumerable.Range(0, 32 * 32 * 3).ToArray();

            using ReadOnlyTexture3D<int> source = device.Get().AllocateReadOnlyTexture3D(data, 32, 32, 3);
            using ReadWriteBuffer<int> destination = device.Get().AllocateReadWriteBuffer<int>(data.Length);

            device.Get().For(source.Width, source.Height, source.Depth, new ReadOnlyTexture3DKernel(source, destination));

            int[] result = destination.ToArray();

            CollectionAssert.AreEqual(data, result);
        }

        [AutoConstructor]
        internal readonly partial struct ReadOnlyTexture3DKernel : IComputeShader
        {
            public readonly ReadOnlyTexture3D<int> source;
            public readonly ReadWriteBuffer<int> destination;

            public void Execute()
            {
                destination[ThreadIds.Z * 32 * 32 + ThreadIds.Y * 32 + ThreadIds.X] = source[ThreadIds.XYZ];
            }
        }

        [CombinatorialTestMethod]
        [AllDevices]
        public void Dispatch_ReadWriteTexture2D(Device device)
        {
            int[] data = Enumerable.Range(0, 32 * 32 * 3).ToArray();

            using ReadWriteTexture3D<int> source = device.Get().AllocateReadWriteTexture3D(data, 32, 32, 3);
            using ReadWriteTexture3D<int> destination = device.Get().AllocateReadWriteTexture3D<int>(32, 32, 3);

            device.Get().For(source.Width, source.Height, source.Depth, new ReadWriteTexture3DKernel(source, destination));

            int[] result = new int[data.Length];

            destination.CopyTo(result);

            CollectionAssert.AreEqual(data, result);
        }

        [AutoConstructor]
        internal readonly partial struct ReadWriteTexture3DKernel : IComputeShader
        {
            public readonly ReadWriteTexture3D<int> source;
            public readonly ReadWriteTexture3D<int> destination;

            public void Execute()
            {
                destination[ThreadIds.XYZ] = source[ThreadIds.XYZ];
            }
        }
    }
}
