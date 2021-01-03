using System;
using System.Linq;
using ComputeSharp.Graphics.Buffers.Abstract;
using ComputeSharp.Tests.Extensions;
using Microsoft.Toolkit.HighPerformance.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests
{
    [TestClass]
    [TestCategory("Texture2D")]
    public partial class Texture2DTests
    {
        [TestMethod]
        [DataRow(typeof(ReadOnlyTexture2D<>))]
        [DataRow(typeof(ReadWriteTexture2D<>))]
        public void Allocate_Uninitialized_Ok(Type textureType)
        {
            using Texture2D<float> texture = Gpu.Default.AllocateTexture2D<float>(textureType, 128, 128);

            Assert.IsNotNull(texture);
            Assert.AreEqual(texture.Width, 128);
            Assert.AreEqual(texture.Height, 128);
            Assert.AreSame(texture.GraphicsDevice, Gpu.Default);
        }

        [TestMethod]
        [DataRow(typeof(ReadOnlyTexture2D<>), 128, -14253)]
        [DataRow(typeof(ReadOnlyTexture2D<>), 128, -1)]
        [DataRow(typeof(ReadOnlyTexture2D<>), 0, -4314)]
        [DataRow(typeof(ReadOnlyTexture2D<>), -14, -53)]
        [DataRow(typeof(ReadWriteTexture2D<>), 128, -14253)]
        [DataRow(typeof(ReadWriteTexture2D<>), 128, -1)]
        [DataRow(typeof(ReadWriteTexture2D<>), 0, -4314)]
        [DataRow(typeof(ReadWriteTexture2D<>), -14, -53)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Allocate_Uninitialized_Fail(Type textureType, int width, int height)
        {
            using Texture2D<float> texture = Gpu.Default.AllocateTexture2D<float>(textureType, width, height);
        }

        [TestMethod]
        [DataRow(typeof(ReadOnlyTexture2D<>))]
        [DataRow(typeof(ReadWriteTexture2D<>))]
        public void Allocate_FromArray(Type textureType)
        {
            float[] data = Enumerable.Range(0, 128 * 128).Select(static i => (float)i).ToArray();

            using Texture2D<float> texture = Gpu.Default.AllocateTexture2D(textureType, data, 128, 128);

            Assert.IsNotNull(texture);
            Assert.AreEqual(texture.Width, 128);
            Assert.AreEqual(texture.Height, 128);
            Assert.AreSame(texture.GraphicsDevice, Gpu.Default);

            float[,] result = texture.GetData();

            Assert.IsNotNull(result);
            Assert.AreEqual(data.Length, result.Length);
            Assert.IsTrue(data.SequenceEqual(result.Cast<float>()));
        }

        [TestMethod]
        [DataRow(typeof(ReadOnlyTexture2D<>))]
        [DataRow(typeof(ReadWriteTexture2D<>))]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void UsageAfterDispose(Type textureType)
        {
            using Texture2D<float> texture = Gpu.Default.AllocateTexture2D<float>(textureType, 10, 10);

            texture.Dispose();

            _ = texture.GetData();
        }

        [TestMethod]
        [DataRow(typeof(ReadOnlyTexture2D<>), 0, 0, 64, 64)]
        [DataRow(typeof(ReadOnlyTexture2D<>), 0, 14, 64, 50)]
        [DataRow(typeof(ReadOnlyTexture2D<>), 14, 0, 50, 64)]
        [DataRow(typeof(ReadOnlyTexture2D<>), 10, 10, 54, 54)]
        [DataRow(typeof(ReadOnlyTexture2D<>), 20, 20, 32, 27)]
        [DataRow(typeof(ReadOnlyTexture2D<>), 60, 0, 4, 64)]
        [DataRow(typeof(ReadOnlyTexture2D<>), 0, 60, 64, 4)]
        [DataRow(typeof(ReadOnlyTexture2D<>), 63, 2, 1, 60)]
        [DataRow(typeof(ReadOnlyTexture2D<>), 2, 63, 60, 1)]
        [DataRow(typeof(ReadWriteTexture2D<>), 0, 0, 64, 64)]
        [DataRow(typeof(ReadWriteTexture2D<>), 0, 14, 64, 50)]
        [DataRow(typeof(ReadWriteTexture2D<>), 14, 0, 50, 64)]
        [DataRow(typeof(ReadWriteTexture2D<>), 10, 10, 54, 54)]
        [DataRow(typeof(ReadWriteTexture2D<>), 20, 20, 32, 27)]
        [DataRow(typeof(ReadWriteTexture2D<>), 60, 0, 4, 64)]
        [DataRow(typeof(ReadWriteTexture2D<>), 0, 60, 64, 4)]
        [DataRow(typeof(ReadWriteTexture2D<>), 63, 2, 1, 60)]
        [DataRow(typeof(ReadWriteTexture2D<>), 2, 63, 60, 1)]
        public void GetData_RangeToVoid_Ok(Type textureType, int x, int y, int width, int height)
        {
            float[] array = Enumerable.Range(0, 4096).Select(static i => (float)i).ToArray();

            using Texture2D<float> texture = Gpu.Default.AllocateTexture2D(textureType, array, 64, 64);

            float[] result = new float[width * height];

            texture.GetData(result, x, y, width, height);

            Span2D<float>
                expected = new Span2D<float>(array, 64, 64).Slice(y, x, height, width),
                data = new(result, height, width);

            CollectionAssert.AreEqual(expected.ToArray(), data.ToArray());
        }

        [TestMethod]
        [DataRow(typeof(ReadOnlyTexture2D<>), 0, -1, 50, 50)]
        [DataRow(typeof(ReadOnlyTexture2D<>), -1, 0, 50, 50)]
        [DataRow(typeof(ReadOnlyTexture2D<>), 12, 0, -1, 50)]
        [DataRow(typeof(ReadOnlyTexture2D<>), 12, 0, 20, -1)]
        [DataRow(typeof(ReadOnlyTexture2D<>), 12, 20, 20, 50)]
        [DataRow(typeof(ReadOnlyTexture2D<>), 12, 20, 60, 20)]
        [DataRow(typeof(ReadOnlyTexture2D<>), 80, 20, 40, 20)]
        [DataRow(typeof(ReadOnlyTexture2D<>), 0, 80, 40, 20)]
        [DataRow(typeof(ReadWriteTexture2D<>), 0, -1, 50, 50)]
        [DataRow(typeof(ReadWriteTexture2D<>), -1, 0, 50, 50)]
        [DataRow(typeof(ReadWriteTexture2D<>), 12, 0, -1, 50)]
        [DataRow(typeof(ReadWriteTexture2D<>), 12, 0, 20, -1)]
        [DataRow(typeof(ReadWriteTexture2D<>), 12, 20, 20, 50)]
        [DataRow(typeof(ReadWriteTexture2D<>), 12, 20, 60, 20)]
        [DataRow(typeof(ReadWriteTexture2D<>), 80, 20, 40, 20)]
        [DataRow(typeof(ReadWriteTexture2D<>), 0, 80, 40, 20)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetData_RangeToVoid_Fail(Type textureType, int x, int y, int width, int height)
        {
            float[] array = Enumerable.Range(0, 4096).Select(static i => (float)i).ToArray();

            using Texture2D<float> texture = Gpu.Default.AllocateTexture2D(textureType, array, 64, 64);

            float[] result = new float[4096];

            texture.GetData(result, x, y, width, height);
        }

        [TestMethod]
        public void Dispatch_ReadOnlyTexture2D()
        {
            int[] data = Enumerable.Range(0, 32 * 32).ToArray();

            using ReadOnlyTexture2D<int> source = Gpu.Default.AllocateReadOnlyTexture2D(data, 32, 32);
            using ReadWriteBuffer<int> destination = Gpu.Default.AllocateReadWriteBuffer<int>(data.Length);

            Gpu.Default.For(source.Width, source.Height, new ReadOnlyTexture2DKernel(source, destination));

            int[] result = destination.GetData();

            CollectionAssert.AreEqual(data, result);
        }

        [AutoConstructor]
        internal readonly partial struct ReadOnlyTexture2DKernel : IComputeShader
        {
            public readonly ReadOnlyTexture2D<int> source;
            public readonly ReadWriteBuffer<int> destination;

            public void Execute(ThreadIds ids)
            {
                destination[ids.Y * 32 + ids.X] = source[ids.XY];
            }
        }

        [TestMethod]
        public void Dispatch_ReadWriteTexture2D()
        {
            int[] data = Enumerable.Range(0, 32 * 32).ToArray();

            using ReadWriteTexture2D<int> source = Gpu.Default.AllocateReadWriteTexture2D(data, 32, 32);
            using ReadWriteTexture2D<int> destination = Gpu.Default.AllocateReadWriteTexture2D<int>(32, 32);

            Gpu.Default.For(source.Width, source.Height, new ReadWriteTexture2DKernel(source, destination));

            int[] result = new int[data.Length];

            destination.GetData(result);

            CollectionAssert.AreEqual(data, result);
        }

        [AutoConstructor]
        internal readonly partial struct ReadWriteTexture2DKernel : IComputeShader
        {
            public readonly ReadWriteTexture2D<int> source;
            public readonly ReadWriteTexture2D<int> destination;

            public void Execute(ThreadIds ids)
            {
                destination[ids.XY] = source[ids.XY];
            }
        }
    }
}
