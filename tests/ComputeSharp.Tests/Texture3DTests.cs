using System;
using System.Linq;
using ComputeSharp.Graphics.Buffers.Abstract;
using ComputeSharp.Tests.Extensions;
using Microsoft.Toolkit.HighPerformance.Extensions;
using Microsoft.Toolkit.HighPerformance.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests
{
    [TestClass]
    [TestCategory("Texture3D")]
    public partial class Texture3DTests
    {
        [TestMethod]
        [DataRow(typeof(ReadOnlyTexture3D<>))]
        [DataRow(typeof(ReadWriteTexture3D<>))]
        public void Allocate_Uninitialized_Ok(Type textureType)
        {
            using Texture3D<float> texture = Gpu.Default.AllocateTexture3D<float>(textureType, 64, 64, 12);

            Assert.IsNotNull(texture);
            Assert.AreEqual(texture.Width, 64);
            Assert.AreEqual(texture.Height, 64);
            Assert.AreEqual(texture.Depth, 12);
            Assert.AreSame(texture.GraphicsDevice, Gpu.Default);
        }

        [TestMethod]
        [DataRow(typeof(ReadOnlyTexture3D<>), 128, -14253, 4)]
        [DataRow(typeof(ReadOnlyTexture3D<>), 128, -1, 4)]
        [DataRow(typeof(ReadOnlyTexture3D<>), 0, -4314, 4)]
        [DataRow(typeof(ReadOnlyTexture3D<>), -14, -53, 4)]
        [DataRow(typeof(ReadOnlyTexture3D<>), 12, 12, -1)]
        [DataRow(typeof(ReadOnlyTexture3D<>), 2, 4, ushort.MaxValue + 1)]
        [DataRow(typeof(ReadWriteTexture3D<>), 128, -14253, 4)]
        [DataRow(typeof(ReadWriteTexture3D<>), 128, -1, 4)]
        [DataRow(typeof(ReadWriteTexture3D<>), 0, -4314, 4)]
        [DataRow(typeof(ReadWriteTexture3D<>), -14, -53, 4)]
        [DataRow(typeof(ReadWriteTexture3D<>), 12, 12, -1)]
        [DataRow(typeof(ReadWriteTexture3D<>), 2, 4, ushort.MaxValue + 1)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Allocate_Uninitialized_Fail(Type textureType, int width, int height, int depth)
        {
            using Texture3D<float> texture = Gpu.Default.AllocateTexture3D<float>(textureType, width, height, depth);
        }

        [TestMethod]
        [DataRow(typeof(ReadOnlyTexture3D<>), 12, 12, 4)]
        [DataRow(typeof(ReadOnlyTexture3D<>), 2, 12, 4)]
        [DataRow(typeof(ReadOnlyTexture3D<>), 64, 64, 1)]
        [DataRow(typeof(ReadOnlyTexture3D<>), 64, 27, 3)]
        [DataRow(typeof(ReadWriteTexture3D<>), 12, 12, 4)]
        [DataRow(typeof(ReadWriteTexture3D<>), 2, 12, 4)]
        [DataRow(typeof(ReadWriteTexture3D<>), 64, 64, 1)]
        [DataRow(typeof(ReadWriteTexture3D<>), 64, 27, 3)]
        public void Allocate_FromArray(Type textureType, int width, int height, int depth)
        {
            float[] data = Enumerable.Range(0, width * height * depth).Select(static i => (float)i).ToArray();

            using Texture3D<float> texture = Gpu.Default.AllocateTexture3D(textureType, data, width, height, depth);

            Assert.IsNotNull(texture);
            Assert.AreEqual(texture.Width, width);
            Assert.AreEqual(texture.Height, height);
            Assert.AreEqual(texture.Depth, depth);
            Assert.AreSame(texture.GraphicsDevice, Gpu.Default);

            float[,,] result = texture.GetData();

            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetLength(0), depth);
            Assert.AreEqual(result.GetLength(1), height);
            Assert.AreEqual(result.GetLength(2), width);
            Assert.AreEqual(data.Length, result.Length);
            Assert.IsTrue(data.SequenceEqual(result.Cast<float>()));

            Array.Clear(result, 0, result.Length);

            float[] flat = new float[result.Length];

            texture.GetData(flat);

            CollectionAssert.AreEqual(flat, data);
        }

        [TestMethod]
        [DataRow(typeof(ReadOnlyTexture3D<>))]
        [DataRow(typeof(ReadWriteTexture3D<>))]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void UsageAfterDispose(Type textureType)
        {
            using Texture3D<float> texture = Gpu.Default.AllocateTexture3D<float>(textureType, 10, 10, 4);

            texture.Dispose();

            _ = texture.GetData();
        }

        [TestMethod]
        [DataRow(typeof(ReadOnlyTexture3D<>), 0, 0, 0, 64, 64, 3)]
        [DataRow(typeof(ReadOnlyTexture3D<>), 0, 14, 0, 64, 50, 3)]
        [DataRow(typeof(ReadOnlyTexture3D<>), 0, 14, 1, 64, 50, 2)]
        [DataRow(typeof(ReadOnlyTexture3D<>), 14, 0, 1, 50, 64, 2)]
        [DataRow(typeof(ReadOnlyTexture3D<>), 10, 10, 2, 54, 54, 1)]
        [DataRow(typeof(ReadOnlyTexture3D<>), 20, 20, 0, 32, 27, 3)]
        [DataRow(typeof(ReadOnlyTexture3D<>), 60, 0, 0, 4, 64, 3)]
        [DataRow(typeof(ReadOnlyTexture3D<>), 0, 60, 2, 64, 4, 1)]
        [DataRow(typeof(ReadOnlyTexture3D<>), 63, 2, 0, 1, 60, 2)]
        [DataRow(typeof(ReadOnlyTexture3D<>), 2, 63, 1, 60, 1, 2)]
        [DataRow(typeof(ReadWriteTexture3D<>), 0, 0, 0, 64, 64, 3)]
        [DataRow(typeof(ReadWriteTexture3D<>), 0, 14, 0, 64, 50, 3)]
        [DataRow(typeof(ReadWriteTexture3D<>), 0, 14, 1, 64, 50, 2)]
        [DataRow(typeof(ReadWriteTexture3D<>), 14, 0, 1, 50, 64, 2)]
        [DataRow(typeof(ReadWriteTexture3D<>), 10, 10, 2, 54, 54, 1)]
        [DataRow(typeof(ReadWriteTexture3D<>), 20, 20, 0, 32, 27, 3)]
        [DataRow(typeof(ReadWriteTexture3D<>), 60, 0, 0, 4, 64, 3)]
        [DataRow(typeof(ReadWriteTexture3D<>), 0, 60, 2, 64, 4, 1)]
        [DataRow(typeof(ReadWriteTexture3D<>), 63, 2, 0, 1, 60, 2)]
        [DataRow(typeof(ReadWriteTexture3D<>), 2, 63, 1, 60, 1, 2)]
        public void GetData_RangeToVoid_Ok(Type textureType, int x, int y, int z, int width, int height, int depth)
        {
            int[,,] array = new int[3, 64, 64];

            foreach (var item in array.AsSpan().Enumerate())
                item.Value = item.Index;

            using Texture3D<int> texture = Gpu.Default.AllocateTexture3D(textureType, array);

            int[,,] result = new int[depth, height, width];

            texture.GetData(result.AsSpan(), x, y, z, width, height, depth);

            for (int k = 0; k < depth; k++)
            {
                Span2D<int>
                    source = array.AsSpan2D(z + k).Slice(y, x, height, width),
                    destination = result.AsSpan2D(k);

                CollectionAssert.AreEqual(source.ToArray(), destination.ToArray());
            }
        }

        [TestMethod]
        [DataRow(typeof(ReadOnlyTexture3D<>), 0, -1, 0, 50, 50, 2)]
        [DataRow(typeof(ReadOnlyTexture3D<>), -1, 0, 0, 50, 50, 3)]
        [DataRow(typeof(ReadOnlyTexture3D<>), 12, 0, 0, -1, 50, 1)]
        [DataRow(typeof(ReadOnlyTexture3D<>), 12, 0, 0, 20, 50, 0)]
        [DataRow(typeof(ReadOnlyTexture3D<>), 12, 0, 1, 20, -1, 2)]
        [DataRow(typeof(ReadOnlyTexture3D<>), 12, 20, 2, 20, 50, 1)]
        [DataRow(typeof(ReadOnlyTexture3D<>), 12, 20, 0, 60, 20, 3)]
        [DataRow(typeof(ReadOnlyTexture3D<>), 80, 20, 2, 40, 20, 1)]
        [DataRow(typeof(ReadOnlyTexture3D<>), 0, 80, 2, 40, 20, 1)]
        [DataRow(typeof(ReadWriteTexture3D<>), 0, -1, 0, 50, 50, 2)]
        [DataRow(typeof(ReadWriteTexture3D<>), -1, 0, 0, 50, 50, 3)]
        [DataRow(typeof(ReadWriteTexture3D<>), 12, 0, 0, -1, 50, 1)]
        [DataRow(typeof(ReadWriteTexture3D<>), 12, 0, 0, 20, 50, 0)]
        [DataRow(typeof(ReadWriteTexture3D<>), 12, 0, 1, 20, -1, 2)]
        [DataRow(typeof(ReadWriteTexture3D<>), 12, 20, 2, 20, 50, 1)]
        [DataRow(typeof(ReadWriteTexture3D<>), 12, 20, 0, 60, 20, 3)]
        [DataRow(typeof(ReadWriteTexture3D<>), 80, 20, 2, 40, 20, 1)]
        [DataRow(typeof(ReadWriteTexture3D<>), 0, 80, 2, 40, 20, 1)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetData_RangeToVoid_Fail(Type textureType, int x, int y, int z, int width, int height, int depth)
        {
            float[] array = Enumerable.Range(0, 64 * 64 * 3).Select(static i => (float)i).ToArray();

            using Texture3D<float> texture = Gpu.Default.AllocateTexture3D(textureType, array, 64, 64, 3);

            float[] result = new float[array.Length];

            texture.GetData(result, x, y, z, width, height, depth);
        }

        [TestMethod]
        public void Dispatch_ReadOnlyTexture3D()
        {
            int[] data = Enumerable.Range(0, 32 * 32 * 3).ToArray();

            using ReadOnlyTexture3D<int> source = Gpu.Default.AllocateReadOnlyTexture3D(data, 32, 32, 3);
            using ReadWriteBuffer<int> destination = Gpu.Default.AllocateReadWriteBuffer<int>(data.Length);

            Gpu.Default.For(source.Width, source.Height, source.Depth, new ReadOnlyTexture3DKernel(source, destination));

            int[] result = destination.GetData();

            CollectionAssert.AreEqual(data, result);
        }

        [AutoConstructor]
        private readonly partial struct ReadOnlyTexture3DKernel : IComputeShader
        {
            public readonly ReadOnlyTexture3D<int> source;
            public readonly ReadWriteBuffer<int> destination;

            public void Execute(ThreadIds ids)
            {
                destination[ids.Z * 32 * 32 + ids.Y * 32 + ids.X] = source[ids.XYZ];
            }
        }

        [TestMethod]
        public void Dispatch_ReadWriteTexture2D()
        {
            int[] data = Enumerable.Range(0, 32 * 32 * 3).ToArray();

            using ReadWriteTexture3D<int> source = Gpu.Default.AllocateReadWriteTexture3D(data, 32, 32, 3);
            using ReadWriteTexture3D<int> destination = Gpu.Default.AllocateReadWriteTexture3D<int>(32, 32, 3);

            Gpu.Default.For(source.Width, source.Height, source.Depth, new ReadWriteTexture3DKernel(source, destination));

            int[] result = new int[data.Length];

            destination.GetData(result);

            CollectionAssert.AreEqual(data, result);
        }

        [AutoConstructor]
        private readonly partial struct ReadWriteTexture3DKernel : IComputeShader
        {
            public readonly ReadWriteTexture3D<int> source;
            public readonly ReadWriteTexture3D<int> destination;

            public void Execute(ThreadIds ids)
            {
                destination[ids.XYZ] = source[ids.XYZ];
            }
        }
    }
}
