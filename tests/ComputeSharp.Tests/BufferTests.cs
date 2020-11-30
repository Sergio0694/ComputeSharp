using System;
using System.Linq;
using ComputeSharp.Graphics.Buffers.Abstract;
using ComputeSharp.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests
{
    [TestClass]
    [TestCategory("Buffers")]
    public class BufferTests
    {
        [TestMethod]
        [DataRow(typeof(ConstantBuffer<>))]
        [DataRow(typeof(ReadOnlyBuffer<>))]
        [DataRow(typeof(ReadWriteBuffer<>))]
        public void AllocateBuffer_Uninitialized(Type bufferType)
        {
            using Buffer<float> buffer = Gpu.Default.AllocateBuffer<float>(bufferType, 128);

            Assert.IsNotNull(buffer);
            Assert.AreEqual(buffer.Length, 128);
            Assert.AreSame(buffer.GraphicsDevice, Gpu.Default);
        }

        [TestMethod]
        [DataRow(typeof(ConstantBuffer<>))]
        [DataRow(typeof(ReadOnlyBuffer<>))]
        [DataRow(typeof(ReadWriteBuffer<>))]
        public void AllocateBuffer_FromArray(Type bufferType)
        {
            float[] data = Enumerable.Range(0, 128).Select(static i => (float)i).ToArray();

            using Buffer<float> buffer = Gpu.Default.AllocateBuffer(bufferType, data);

            Assert.IsNotNull(buffer);
            Assert.AreEqual(buffer.Length, 128);
            Assert.AreSame(buffer.GraphicsDevice, Gpu.Default);

            float[] result = buffer.GetData();

            Assert.IsNotNull(result);
            Assert.AreEqual(data.Length, result.Length);
            Assert.IsTrue(data.SequenceEqual(result));
        }

        [TestMethod]
        [DataRow(typeof(ConstantBuffer<>), typeof(ConstantBuffer<>))]
        [DataRow(typeof(ConstantBuffer<>), typeof(ReadOnlyBuffer<>))]
        [DataRow(typeof(ConstantBuffer<>), typeof(ReadWriteBuffer<>))]
        [DataRow(typeof(ReadOnlyBuffer<>), typeof(ConstantBuffer<>))]
        [DataRow(typeof(ReadOnlyBuffer<>), typeof(ReadOnlyBuffer<>))]
        [DataRow(typeof(ReadOnlyBuffer<>), typeof(ReadWriteBuffer<>))]
        [DataRow(typeof(ReadWriteBuffer<>), typeof(ConstantBuffer<>))]
        [DataRow(typeof(ReadWriteBuffer<>), typeof(ReadOnlyBuffer<>))]
        [DataRow(typeof(ReadWriteBuffer<>), typeof(ReadWriteBuffer<>))]
        public void AllocateBuffer_FromBuffer(Type sourceType, Type destinationType)
        {
            float[] data = Enumerable.Range(0, 128).Select(static i => (float)i).ToArray();

            using Buffer<float> source = Gpu.Default.AllocateBuffer(sourceType, data);
            using Buffer<float> destination = Gpu.Default.AllocateBuffer(destinationType, source);

            Assert.IsNotNull(destination);
            Assert.AreEqual(destination.Length, 128);
            Assert.AreSame(destination.GraphicsDevice, Gpu.Default);

            float[] result = destination.GetData();

            Assert.IsNotNull(result);
            Assert.AreEqual(data.Length, result.Length);
            Assert.IsTrue(data.SequenceEqual(result));
        }

        [TestMethod]
        [DataRow(typeof(ConstantBuffer<>))]
        [DataRow(typeof(ReadOnlyBuffer<>))]
        [DataRow(typeof(ReadWriteBuffer<>))]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void UsageAfterDispose(Type bufferType)
        {
            using Buffer<float> buffer = Gpu.Default.AllocateBuffer<float>(bufferType, 128);

            buffer.Dispose();

            _ = buffer.GetData();
        }

        [TestMethod]
        [DataRow(typeof(ConstantBuffer<>), 0, 4096)]
        [DataRow(typeof(ConstantBuffer<>), 128, 512)]
        [DataRow(typeof(ConstantBuffer<>), 2048, 2048)]
        [DataRow(typeof(ReadOnlyBuffer<>), 0, 4096)]
        [DataRow(typeof(ReadOnlyBuffer<>), 128, 512)]
        [DataRow(typeof(ReadOnlyBuffer<>), 2048, 2048)]
        [DataRow(typeof(ReadWriteBuffer<>), 0, 4096)]
        [DataRow(typeof(ReadWriteBuffer<>), 128, 512)]
        [DataRow(typeof(ReadWriteBuffer<>), 2048, 2048)]
        public void GetData_RangeToArray_Ok(Type bufferType, int offset, int count)
        {
            float[] array = Enumerable.Range(0, 4096).Select(i => (float)i).ToArray();

            using Buffer<float> buffer = Gpu.Default.AllocateBuffer(bufferType, array);

            float[] result = buffer.GetData(offset, count);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Length, count);
            Assert.IsTrue(array.AsSpan(offset, count).SequenceEqual(result));
        }

        [TestMethod]
        [DataRow(typeof(ConstantBuffer<>), -1, 128)]
        [DataRow(typeof(ConstantBuffer<>), 8192, 128)]
        [DataRow(typeof(ConstantBuffer<>), 512, 4096)]
        [DataRow(typeof(ConstantBuffer<>), 512, -128)]
        [DataRow(typeof(ReadOnlyBuffer<>), -1, 128)]
        [DataRow(typeof(ReadOnlyBuffer<>), 8192, 128)]
        [DataRow(typeof(ReadOnlyBuffer<>), 512, 4096)]
        [DataRow(typeof(ReadOnlyBuffer<>), 512, -128)]
        [DataRow(typeof(ReadWriteBuffer<>), -1, 128)]
        [DataRow(typeof(ReadWriteBuffer<>), 8192, 128)]
        [DataRow(typeof(ReadWriteBuffer<>), 512, 4096)]
        [DataRow(typeof(ReadWriteBuffer<>), 512, -128)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetData_RangeToArray_Fail(Type bufferType, int offset, int count)
        {
            float[] array = Enumerable.Range(0, 4096).Select(i => (float)i).ToArray();

            using Buffer<float> buffer = Gpu.Default.AllocateBuffer(bufferType, array);

            _ = buffer.GetData(offset, count);
        }

        [TestMethod]
        [DataRow(typeof(ConstantBuffer<>), 0, 0, 4096)]
        [DataRow(typeof(ConstantBuffer<>), 512, 0, 2048)]
        [DataRow(typeof(ConstantBuffer<>), 0, 512, 2048)]
        [DataRow(typeof(ConstantBuffer<>), 127, 1024, 587)]
        [DataRow(typeof(ReadOnlyBuffer<>), 0, 0, 4096)]
        [DataRow(typeof(ReadOnlyBuffer<>), 512, 0, 2048)]
        [DataRow(typeof(ReadOnlyBuffer<>), 0, 512, 2048)]
        [DataRow(typeof(ReadOnlyBuffer<>), 127, 1024, 587)]
        [DataRow(typeof(ReadWriteBuffer<>), 0, 0, 4096)]
        [DataRow(typeof(ReadWriteBuffer<>), 512, 0, 2048)]
        [DataRow(typeof(ReadWriteBuffer<>), 0, 512, 2048)]
        [DataRow(typeof(ReadWriteBuffer<>), 127, 1024, 587)]
        public void GetData_RangeToVoid_Ok(Type bufferType, int destinationOffset, int bufferOffset, int count)
        {
            float[] array = Enumerable.Range(0, 4096).Select(i => (float)i).ToArray();

            using Buffer<float> buffer = Gpu.Default.AllocateBuffer(bufferType, array);

            float[] result = new float[4096];

            buffer.GetData(result, destinationOffset, bufferOffset, count);

            Assert.IsTrue(array.AsSpan(bufferOffset, count).SequenceEqual(result.AsSpan(destinationOffset, count)));
        }

        [TestMethod]
        [DataRow(typeof(ConstantBuffer<>), 0, 0, 8196)]
        [DataRow(typeof(ConstantBuffer<>), -12, 0, 1024)]
        [DataRow(typeof(ConstantBuffer<>), 0, -56, 1024)]
        [DataRow(typeof(ConstantBuffer<>), 512, 0, 4096)]
        [DataRow(typeof(ConstantBuffer<>), 12, 1024, 3600)]
        [DataRow(typeof(ConstantBuffer<>), 12, 1024, -2096)]
        [DataRow(typeof(ReadOnlyBuffer<>), 0, 0, 8196)]
        [DataRow(typeof(ReadOnlyBuffer<>), -12, 0, 1024)]
        [DataRow(typeof(ReadOnlyBuffer<>), 0, -56, 1024)]
        [DataRow(typeof(ReadOnlyBuffer<>), 512, 0, 4096)]
        [DataRow(typeof(ReadOnlyBuffer<>), 12, 1024, 3600)]
        [DataRow(typeof(ReadOnlyBuffer<>), 12, 1024, -2096)]
        [DataRow(typeof(ReadWriteBuffer<>), 0, 0, 8196)]
        [DataRow(typeof(ReadWriteBuffer<>), -12, 0, 1024)]
        [DataRow(typeof(ReadWriteBuffer<>), 0, -56, 1024)]
        [DataRow(typeof(ReadWriteBuffer<>), 512, 0, 4096)]
        [DataRow(typeof(ReadWriteBuffer<>), 12, 1024, 3600)]
        [DataRow(typeof(ReadWriteBuffer<>), 12, 1024, -2096)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetData_RangeToVoid_Fail(Type bufferType, int destinationOffset, int bufferOffset, int count)
        {
            float[] array = Enumerable.Range(0, 4096).Select(i => (float)i).ToArray();

            using Buffer<float> buffer = Gpu.Default.AllocateBuffer(bufferType, array);

            float[] result = new float[4096];

            buffer.GetData(result, destinationOffset, bufferOffset, count);
        }
    }
}
